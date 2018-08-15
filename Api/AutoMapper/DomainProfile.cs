using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using AutoMapper;
using Common.Dtos;
using Common.Enums;
using Common.ViewModels.Inventory;

namespace Api.AutoMapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Inventory, InventoryDto>()
                .ForMember(dto => dto.DistributionUnit, opt => opt.MapFrom(s => s.DistributionUnit.ToString()))
                .ForMember(dto => dto.InventoryType, opt => opt.MapFrom(s => s.InventoryType.ToString()))
                .ForMember(dto => dto.Rank, opt => opt.MapFrom(s => s.Rank.ToString()));

            CreateMap<InventoryCreateVm, Inventory>();

            CreateMap<InventoryEditVm, Inventory>()
                .ForMember(dto => dto.InventoryId, opt => opt.Ignore());
        }
    }
}
