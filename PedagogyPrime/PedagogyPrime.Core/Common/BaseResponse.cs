namespace PedagogyPrime.Core.Common
{
	using System.Text.Json.Serialization;

	public enum StatusCodes
	{
		Ok,
		BadRequest,
		NotFound,
		Forbidden,
		Created
	}

	public class BaseResponse<T>
	{
		public T? Resource { get; set; }

		[JsonIgnore] public StatusCodes StatusCode { get; set; }

		public List<string> Errors { get; set; }

		public static BaseResponse<T> Ok(T resource = default)
		{
			return new BaseResponse<T>
			{
				Resource = resource,
				StatusCode = StatusCodes.Ok
			};
		}

		public static BaseResponse<T> Created()
		{
			return new BaseResponse<T>
			{
				StatusCode = StatusCodes.Created
			};
		}

		public static BaseResponse<T> NotFound()
		{
			return new BaseResponse<T>
			{
				StatusCode = StatusCodes.NotFound,
				Errors = new List<string>
				{
					$"{typeof(T).Name} not found"
				}
			};
		}

		public static BaseResponse<T> BadRequest(List<string> message)
		{
			return new BaseResponse<T>
			{
				StatusCode = StatusCodes.BadRequest,
				Errors = message
			};
		}

		public static BaseResponse<T> Forbbiden()
		{
			return new BaseResponse<T>()
			{
				StatusCode = StatusCodes.Forbidden,
				Errors = new List<string>
				{
					"Forbidden"
				}
			};
		}
	}
}