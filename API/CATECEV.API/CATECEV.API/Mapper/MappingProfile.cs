using AutoMapper;

namespace CATECEV.API.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region User
            CreateMap<Models.AMPECO.resource.users.User, CATECEV.Models.Entity.AMPECO.Resources.User.User>()
                .ForMember(destination => destination.AMPECOId, opt => opt.MapFrom(src => src.Id))
                .ForMember(destination => destination.Id, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(destination => destination.Id, opt => opt.MapFrom(src => src.AMPECOId));

            CreateMap<Models.AMPECO.resource.users.UserOptions, CATECEV.Models.Entity.AMPECO.Resources.User.UserOptions>().ReverseMap();
            #endregion

            #region Charge Point
            CreateMap<Models.AMPECO.resource.ChargePoint.ChargePoint, CATECEV.Models.Entity.AMPECO.Resources.ChargePoint.ChargePointEntity>()
                .ForMember(destination => destination.AMPECOId, opt => opt.MapFrom(src => src.Id))
                .ForMember(destination => destination.SubscriptionPlanIds, opt => opt.MapFrom(src => src.Subscription.SubscriptionPlanIds))
                .ForMember(destination => destination.SubscriptionRequired, opt => opt.MapFrom(src => src.Subscription.SubscriptionRequired))
                .ForMember(destination => destination.Id, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(destination => destination.Id, opt => opt.MapFrom(src => src.AMPECOId))
                .ForMember(destination => destination.Subscription.SubscriptionRequired, opt => opt.MapFrom(src => src.SubscriptionRequired))
                .ForMember(destination => destination.Subscription.SubscriptionPlanIds, opt => opt.MapFrom(src => src.SubscriptionPlanIds));

            CreateMap<Models.AMPECO.resource.ChargePoint.Evse, CATECEV.Models.Entity.AMPECO.Resources.ChargePoint.EvseEntity>()
                .ForMember(destination => destination.AMPECOId, opt => opt.MapFrom(src => src.Id))
                .ForMember(destination => destination.RoamingPhysicalReference, opt => opt.MapFrom(src => src.Roaming.PhysicalReference))
                .ForMember(destination => destination.Capabilities, opt => opt.MapFrom(src => src.Roaming.Capabilities))
                .ForMember(destination => destination.TariffIds, opt => opt.MapFrom(src => src.Roaming.TariffIds))
                .ForMember(destination => destination.RoamingStatus, opt => opt.MapFrom(src => src.Roaming.Status))
                .ForMember(destination => destination.Id, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(destination => destination.Id, opt => opt.MapFrom(src => src.AMPECOId))
                .ForMember(destination => destination.Roaming.PhysicalReference, opt => opt.MapFrom(src => src.RoamingPhysicalReference))
                .ForMember(destination => destination.Roaming.Capabilities, opt => opt.MapFrom(src => src.Capabilities))
                .ForMember(destination => destination.Roaming.TariffIds, opt => opt.MapFrom(src => src.TariffIds))
                .ForMember(destination => destination.Roaming.Status, opt => opt.MapFrom(src => src.RoamingStatus));

            CreateMap<Models.AMPECO.resource.ChargePoint.Connector, CATECEV.Models.Entity.AMPECO.Resources.ChargePoint.ConnectorEntity>()
                .ForMember(destination => destination.AMPECOId, opt => opt.MapFrom(src => src.Id))
                .ForMember(destination => destination.Id, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(destination => destination.Id, opt => opt.MapFrom(src => src.AMPECOId));
            #endregion

            #region Charge session
            CreateMap<Models.AMPECO.resource.Session.ChargingSession, CATECEV.Models.Entity.AMPECO.Resources.Session.ChargingSessionEntity>()
                .ForMember(destination => destination.AMPECOId, opt => opt.MapFrom(src => src.Id))
                .ForMember(destination => destination.TotalAmountWithTax, opt => opt.MapFrom(src => src.TotalAmount.WithTax))
                .ForMember(destination => destination.TotalAmountWithoutTax, opt => opt.MapFrom(src => src.TotalAmount.WithoutTax))
                .ForMember(destination => destination.TaxId, opt => opt.MapFrom(src => src.Tax.TaxId))
                .ForMember(destination => destination.TaxPercentage, opt => opt.MapFrom(src => src.Tax.TaxPercentage))
                .ForMember(destination => destination.TotalEnergyConsumption, opt => opt.MapFrom(src => src.EnergyConsumption.Total))
                .ForMember(destination => destination.EnergyConsumptionGrid, opt => opt.MapFrom(src => src.EnergyConsumption.Grid))
                .ForMember(destination => destination.Id, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(destination => destination.Id, opt => opt.MapFrom(src => src.AMPECOId))
                .ForMember(destination => destination.TotalAmount.WithTax, opt => opt.MapFrom(src => src.TotalAmountWithTax))
                .ForMember(destination => destination.TotalAmount.WithoutTax, opt => opt.MapFrom(src => src.TotalAmountWithoutTax))
                .ForMember(destination => destination.Tax.TaxId, opt => opt.MapFrom(src => src.TaxId))
                .ForMember(destination => destination.Tax.TaxPercentage, opt => opt.MapFrom(src => src.TaxPercentage))
                .ForMember(destination => destination.EnergyConsumption.Total, opt => opt.MapFrom(src => src.TotalEnergyConsumption))
                .ForMember(destination => destination.EnergyConsumption.Grid, opt => opt.MapFrom(src => src.EnergyConsumptionGrid));
            #endregion

            #region Tax
            CreateMap<Models.AMPECO.resource.Tax.Tax, CATECEV.Models.Entity.AMPECO.Resources.Tax.TaxEntity>()
                .ForMember(destination => destination.AMPECOId, opt => opt.MapFrom(src => src.Id))
                .ForMember(destination => destination.Id, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(destination => destination.Id, opt => opt.MapFrom(src => src.AMPECOId));

            CreateMap<Models.AMPECO.resource.Tax.DisplayName, CATECEV.Models.Entity.AMPECO.Resources.Tax.TaxDisplayNameEntity>().ReverseMap();
            #endregion
        }
    }
}
