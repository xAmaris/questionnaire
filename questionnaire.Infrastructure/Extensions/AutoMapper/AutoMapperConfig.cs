using AutoMapper;
using questionnaire.Core.Domains.ImportFile;
using questionnaire.Core.Domains.Surveys;
using questionnaire.Infrastructure.DTO;
using questionnaire.Infrastructure.DTO.ImportFile;

namespace questionnaire.Infrastructure.Extensions.AutoMapper {
    public class AutoMapperConfig {
        public static IMapper Initialize () => new MapperConfiguration (cfg => {
                cfg.CreateMap<UnregisteredUser, UnregisteredUserDto> ();
            })
            .CreateMapper ();
    }
}