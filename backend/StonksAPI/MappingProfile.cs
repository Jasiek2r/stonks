namespace StonksAPI
{
    using AutoMapper;
    using StonksAPI.DTO;
    using StonksAPI.Entities;
    using StonksAPI.Utility;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            /*
             * This will allow AutoMapper to map from ApiResponse to our Data Transfer Object (DTO) of Quotations
             */
            CreateMap<ApiSeries, Quotations>()
                .ForMember(dest => dest.quotations, opt => opt.MapFrom(src =>
                    src.TimeSeries.Select(ts => new Quotation
                        {
                            TimeInterval = ts.Key,
                            Open = ts.Value.Open,
                            High = ts.Value.High,
                            Low = ts.Value.Low,
                            Close = ts.Value.Close
                        })
                    .ToList()
                ) 
            );

            CreateMap<CreateHoldingDto, Holding>()
                .ForMember(p => p.PurchaseDate, opt=>opt.MapFrom(s => DateTime.Now));

        }
    }
}
