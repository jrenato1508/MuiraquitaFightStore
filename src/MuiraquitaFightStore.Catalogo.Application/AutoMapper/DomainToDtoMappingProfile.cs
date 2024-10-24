using AutoMapper;
using MuiraquitaFightStore.Catalogo.Application.DTOs;
using MuiraquitaFightStore.Catalogo.Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuiraquitaFightStore.Catalogo.Application.AutoMapper
{
    public class DomainToDtoMappingProfile : Profile
    {
        public DomainToDtoMappingProfile()
        {
            CreateMap<Produto, ProdutoDto>()
                .ForMember(d => d.TamanhoNumeracao, o => o.MapFrom(s => s.Tamanho.TamanhoNumeracao))
                .ForMember(d => d.TamanhoCamisa, o => o.MapFrom(s => s.Tamanho.TamanhoCamisa))
                .ForMember(d => d.TamanhoShort, o => o.MapFrom(s => s.Tamanho.TamanhoShort))
                .ForMember(d => d.Peso, o => o.MapFrom(s => s.Tamanho.Peso));

            CreateMap<Categoria, CategoriaDto>();

            CreateMap<Marca,MarcaDto>();
        }
    }
}
