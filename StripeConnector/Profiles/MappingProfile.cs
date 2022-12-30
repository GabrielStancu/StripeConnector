using AutoMapper;
using Stripe;
using StripeConnector.Commands;
using StripeConnector.Models;

namespace StripeConnector.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AddCustomer, TokenCardOptions>()
            .ForMember(dest => dest.Number, map => map.MapFrom(src => src.CreditCard.CardNumber))
            .ForMember(dest => dest.ExpYear, map => map.MapFrom(src => src.CreditCard.ExpirationYear))
            .ForMember(dest => dest.ExpMonth, map => map.MapFrom(src => src.CreditCard.ExpirationMonth))
            .ForMember(dest => dest.Cvc, map => map.MapFrom(src => src.CreditCard.Cvc));

        CreateMap<Customer, StripeCustomer>()
            .ForMember(dest => dest.CustomerId, map => map.MapFrom(src => src.Id));

        CreateMap<AddPayment, ChargeCreateOptions>()
            .ForMember(dest => dest.Customer, map => map.MapFrom(src => src.CustomerId));

        CreateMap<Charge, StripePayment>()
            .ForMember(dest => dest.PaymentId, map => map.MapFrom(src => src.Id));
    }
}