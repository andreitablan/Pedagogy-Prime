namespace PedagogyPrime.Infrastructure.Common
{
	using AutoMapper;
	using Core.Entities;
	using Models.Coverage;

	public static class GenericMapper<TSource, TResult>
		where TSource : class
		where TResult : class
	{
		public static TResult Map(TSource source)
		{
			var config = new MapperConfiguration(cfg =>
				{
					cfg.CreateMap<TSource, TResult>();
					cfg.CreateMap<Coverage, CoverageDetails>()
						.ForMember(dest => dest.GoodWords, opt => opt.MapFrom(src => src.GoodWords))
						.ForMember(dest => dest.BadWords, opt => opt.MapFrom(src => src.BadWords));
				}
			);

			var mapper = new Mapper(config);
			return mapper.Map<TResult>(source);
		}
	}
}