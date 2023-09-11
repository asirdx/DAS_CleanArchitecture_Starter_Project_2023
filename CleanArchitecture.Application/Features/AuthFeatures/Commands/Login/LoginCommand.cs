using MediatR;

namespace CleanArchitecture.Application.Features.AuthFeatures.Commands.Login;

public sealed record LoginCommand(
    string UserNameOrEmail, //kullanıcı adı veya email ile login olma imkanı sağlar
    string Password) : IRequest<LoginCommandResponse>;
