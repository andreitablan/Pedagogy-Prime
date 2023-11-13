namespace PedagogyPrime.Core.Common
{
	using System.Text.Json.Serialization;

	public enum StatusCodes
	{
		Ok,
		BadRequest,
		NotFound,
		Forbidden,
		Created,
		InternalServerError
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

        public static BaseResponse<Guid> Created(Guid id)
        {
			return new BaseResponse<Guid>
			{
				Resource = id,
                StatusCode = StatusCodes.Created
            };
        }

        public static BaseResponse<T> NotFound(string resource)
		{
			return new BaseResponse<T>
			{
				StatusCode = StatusCodes.NotFound,
				Errors = new List<string>
				{
					$"resource not found"
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

		public static BaseResponse<T> InternalServerError()
		{
			return new BaseResponse<T>
			{
				StatusCode = StatusCodes.InternalServerError,
				Errors = new List<string>
				{
					"There was a problem with your request. Please contact the application admin for further details."
				}
			};
		}

		public static BaseResponse<T> InternalServerError(string message)
		{
			return new BaseResponse<T>
			{
				StatusCode = StatusCodes.InternalServerError,
				Errors = new List<string>
				{
					message
				}
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