using CleanArcihtecture.Infrastructure.Authenticaton;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.WebApi.OptionsSetup;

public sealed class JwtOptionsSetup : IConfigureOptions<JwtOptions>
{
    private readonly IConfiguration _configuration;

    public JwtOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(JwtOptions options) //program.cs de bu register edildiğindne, program ayağa kalkarken appsettingsdeki Jwt sectionundaki değerleri JwtOptions classına bind edeceğiz.
    {
        _configuration.GetSection("Jwt").Bind(options);
    }
}
