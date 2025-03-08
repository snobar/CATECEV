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
            CreateMap<Models.AMPECO.resource.ChargePoint.ChargePoint,
            CATECEV.Models.Entity.AMPECO.Resources.ChargePoint.ChargePointEntity>()
                .ForMember(dest => dest.AMPECOId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.SubscriptionPlanIds, opt => opt.MapFrom(src => src.Subscription.SubscriptionPlanIds))
                .ForMember(dest => dest.SubscriptionRequired, opt => opt.MapFrom(src => src.Subscription.SubscriptionRequired))
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<CATECEV.Models.Entity.AMPECO.Resources.ChargePoint.ChargePointEntity,
            Models.AMPECO.resource.ChargePoint.ChargePoint>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AMPECOId))
                .ForPath(dest => dest.Subscription.SubscriptionRequired, opt => opt.MapFrom(src => src.SubscriptionRequired))
                .ForPath(dest => dest.Subscription.SubscriptionPlanIds, opt => opt.MapFrom(src => src.SubscriptionPlanIds));

            CreateMap<Models.AMPECO.resource.ChargePoint.Evse,
            CATECEV.Models.Entity.AMPECO.Resources.ChargePoint.EvseEntity>()
                .ForMember(dest => dest.AMPECOId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.RoamingPhysicalReference, opt => opt.MapFrom(src => src.Roaming.PhysicalReference))
                .ForMember(dest => dest.Capabilities, opt => opt.MapFrom(src => src.Roaming.Capabilities))
                .ForMember(dest => dest.TariffIds, opt => opt.MapFrom(src => src.Roaming.TariffIds))
                .ForMember(dest => dest.RoamingStatus, opt => opt.MapFrom(src => src.Roaming.Status))
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<CATECEV.Models.Entity.AMPECO.Resources.ChargePoint.EvseEntity,
            Models.AMPECO.resource.ChargePoint.Evse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AMPECOId))
                .ForPath(dest => dest.Roaming.PhysicalReference, opt => opt.MapFrom(src => src.RoamingPhysicalReference))
                .ForPath(dest => dest.Roaming.Capabilities, opt => opt.MapFrom(src => src.Capabilities))
                .ForPath(dest => dest.Roaming.TariffIds, opt => opt.MapFrom(src => src.TariffIds))
                .ForPath(dest => dest.Roaming.Status, opt => opt.MapFrom(src => src.RoamingStatus));

            CreateMap<Models.AMPECO.resource.ChargePoint.Connector, CATECEV.Models.Entity.AMPECO.Resources.ChargePoint.ConnectorEntity>()
                .ForMember(destination => destination.AMPECOId, opt => opt.MapFrom(src => src.Id))
                .ForMember(destination => destination.Id, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(destination => destination.Id, opt => opt.MapFrom(src => src.AMPECOId));
            #endregion

            #region Charge session
            CreateMap<Models.AMPECO.resource.Session.ChargingSession,
            CATECEV.Models.Entity.AMPECO.Resources.Session.ChargingSessionEntity>()
                .ForMember(dest => dest.AMPECOId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TotalAmountWithTax, opt => opt.MapFrom(src => src.TotalAmount.WithTax))
                .ForMember(dest => dest.TotalAmountWithoutTax, opt => opt.MapFrom(src => src.TotalAmount.WithoutTax))
                .ForMember(dest => dest.TaxId, opt => opt.MapFrom(src => src.Tax.TaxId))
                .ForMember(dest => dest.TaxPercentage, opt => opt.MapFrom(src => src.Tax.TaxPercentage))
                .ForMember(dest => dest.TotalEnergyConsumption, opt => opt.MapFrom(src => src.EnergyConsumption.Total))
                .ForMember(dest => dest.EnergyConsumptionGrid, opt => opt.MapFrom(src => src.EnergyConsumption.Grid))
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<CATECEV.Models.Entity.AMPECO.Resources.Session.ChargingSessionEntity,
            Models.AMPECO.resource.Session.ChargingSession>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AMPECOId))
                .ForPath(dest => dest.TotalAmount.WithTax, opt => opt.MapFrom(src => src.TotalAmountWithTax))
                .ForPath(dest => dest.TotalAmount.WithoutTax, opt => opt.MapFrom(src => src.TotalAmountWithoutTax))
                .ForPath(dest => dest.Tax.TaxId, opt => opt.MapFrom(src => src.TaxId))
                .ForPath(dest => dest.Tax.TaxPercentage, opt => opt.MapFrom(src => src.TaxPercentage))
                .ForPath(dest => dest.EnergyConsumption.Total, opt => opt.MapFrom(src => src.TotalEnergyConsumption))
                .ForPath(dest => dest.EnergyConsumption.Grid, opt => opt.MapFrom(src => src.EnergyConsumptionGrid));
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
