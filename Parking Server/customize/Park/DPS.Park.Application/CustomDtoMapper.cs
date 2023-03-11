using AutoMapper;
using DPS.Park.Application.Shared.Dto.Card.Card;
using DPS.Park.Application.Shared.Dto.Card.CardType;
using DPS.Park.Application.Shared.Dto.Fare;
using DPS.Park.Application.Shared.Dto.History;
using DPS.Park.Application.Shared.Dto.Message;
using DPS.Park.Application.Shared.Dto.Resident;
using DPS.Park.Application.Shared.Dto.Resident.ResidentCard;
using DPS.Park.Application.Shared.Dto.Vehicle.VehicleType;
using DPS.Park.Core.Card;
using DPS.Park.Core.Fare;
using DPS.Park.Core.History;
using DPS.Park.Core.Message;
using DPS.Park.Core.Resident;
using DPS.Park.Core.Vehicle;

namespace DPS.Park.Application
{
    internal static class CustomDtoMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<HistoryDto, History>().ReverseMap();
            configuration.CreateMap<CreateOrEditHistoryDto, History>().ReverseMap();
            configuration.CreateMap<HistoryDto, CreateOrEditHistoryDto>().ReverseMap();
            
            configuration.CreateMap<CardTypeDto, CardType>().ReverseMap();
            configuration.CreateMap<CreateOrEditCardTypeDto, CardType>().ReverseMap();
            configuration.CreateMap<CardTypeDto, CreateOrEditCardTypeDto>().ReverseMap();
            
            configuration.CreateMap<VehicleTypeDto, VehicleType>().ReverseMap();
            configuration.CreateMap<CreateOrEditVehicleTypeDto, VehicleType>().ReverseMap();
            configuration.CreateMap<VehicleTypeDto, CreateOrEditVehicleTypeDto>().ReverseMap();
            
            configuration.CreateMap<FareDto, Fare>().ReverseMap();
            configuration.CreateMap<CreateOrEditFareDto, Fare>().ReverseMap();
            configuration.CreateMap<FareDto, CreateOrEditFareDto>().ReverseMap();
            
            configuration.CreateMap<CardDto, Card>().ReverseMap();
            configuration.CreateMap<CreateOrEditCardDto, Card>().ReverseMap();
            configuration.CreateMap<CardDto, CreateOrEditCardDto>().ReverseMap();
            
            configuration.CreateMap<CreateOrEditMessageDto, Message>().ReverseMap();
            
            configuration.CreateMap<ResidentDto, Resident>().ReverseMap();
            configuration.CreateMap<CreateOrEditResidentDto, Resident>().ReverseMap();
            configuration.CreateMap<ResidentDto, CreateOrEditResidentDto>().ReverseMap();
            configuration.CreateMap<ResidentCard, ResidentCardDto>().ReverseMap();
        }
    }
}