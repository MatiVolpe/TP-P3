using Domain.Interfaces;
using Application.Interfaces;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Application.Models.Request;

namespace Infrastructure.Service
{
    public class AuthenticationService : ICustomAuthenticationService
    {
        private readonly IPersonRepository _personRepository;
        private readonly AuthenticationServiceOptions _options;

        public AuthenticationService(IPersonRepository userRepository, IOptions<AuthenticationServiceOptions> options, IPersonRepository personRepository)
        {
            _personRepository = personRepository;
            _options = options.Value;
        }
        private Person? ValidateUser(AuthenticationRequest authenticationRequest)
        {
            if (string.IsNullOrEmpty(authenticationRequest.Email) || string.IsNullOrEmpty(authenticationRequest.Password))
                throw new UnauthorizedAccessException("Email and password are required.");

            var person = _personRepository.GetByEmail(authenticationRequest.Email);

            if (person == null || person.Password != authenticationRequest.Password)
                throw new UnauthorizedAccessException("Invalid credentials.");

            return person;
        }


        public string Autenticar(AuthenticationRequest authenticationRequest)
        {
            //Paso 1: Validamos las credenciales
            var user = ValidateUser(authenticationRequest);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            //Paso 2: Crear el token
            var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("thisisthesecretforgeneratingakey(mustbeatleast32bitlong)")); //Traemos la SecretKey del Json. agregar antes: using Microsoft.IdentityModel.Tokens;
            var credentials = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            var claimsForToken = new List<Claim>
            {
                new Claim("sub", user.IdPerson.ToString()),
                new Claim("email", user.Email),
                new Claim("role", user.Role.ToString()),
                new Claim("name", user.Name.ToString()),
            };

            var jwtSecurityToken = new JwtSecurityToken(
                _options.Issuer,
                _options.Audience,
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                credentials
            );

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return tokenToReturn.ToString();


        }

        public class AuthenticationServiceOptions
        {
            public const string AuthenticationService = "AuthenticationService";

            public required string Issuer { get; set; }
            public required string Audience { get; set; }
            public required string SecretForKey { get; set; }
        }

    }
}
