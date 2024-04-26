using MediatR;

namespace MyResume.Application.Abstractions
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
