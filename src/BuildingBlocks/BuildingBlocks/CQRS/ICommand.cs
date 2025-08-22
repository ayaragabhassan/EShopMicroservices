using MediatR;
namespace BuildingBlocks.CQRS;

public interface ICommand: IRequest<Unit> //Empty command result doesn't return any response 
{
}

public interface ICommand<out TResponse> : IRequest <TResponse>
{
}
