using AutoMapper;

namespace Posterr.Api.Configuration.AutoMapper
{
    public class MapperConfigurationExpressionExtensions
    {
        public static MapperConfiguration ConfigureApplicationProfiles()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ViewModelToDomainProfile>();
                cfg.AddProfile<DomainToViewModelProfile>();
            });

            return config;
        }
    }
}
