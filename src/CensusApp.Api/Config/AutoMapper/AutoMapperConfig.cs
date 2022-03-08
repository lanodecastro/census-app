using AutoMapper;
using CensusApp.Api.Core.Domain.Model;
using CensusApp.Api.Core.Infra.Data.Queries.ViewModels;
using System;

namespace CensusApp.Api.Config.AutoMapper
{
    public class AutoMapperConfig
    {
        public static IMapper InitializeMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Pessoa, PessoaViewModel>()
                .ForMember(dest => dest.Escolaridade, opt => opt.MapFrom(src => src.Escolaridade.Descricao))
                .ForMember(dest => dest.RacaCor, opt => opt.MapFrom(src => src.RacaCor.Descricao))
                .ForMember(dest => dest.Regiao, opt => opt.MapFrom(src => src.Regiao.Descricao));

                cfg.CreateMap<Pessoa, PessoaViewModelArvore>()
                .ForMember(dest => dest.Filhos, opt => opt.Ignore());
            });


            config.AssertConfigurationIsValid();

            return config.CreateMapper();
        }
    }
}
