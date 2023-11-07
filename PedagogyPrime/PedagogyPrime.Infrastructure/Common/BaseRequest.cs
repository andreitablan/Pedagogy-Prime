using MediatR;
using System.Text.Json.Serialization;

namespace PedagogyPrime.Infrastructure.Common
{
	public class BaseRequest<TResponse> : IRequest<TResponse>
	{
		[JsonIgnore]
		public Guid UserId { get; set; }
	}
}