using AutoMapper;
using WsGetCludrByAddress.WebService.Entities;

namespace WsGetCludrByAddress.WebService {
    public class ResponseAddressMapper : Profile {
        public ResponseAddressMapper() {
            CreateMap<Response, Address>()
                .ForMember(a=>a.Country,opt=>opt.MapFrom(r=>r.country))
                .ForMember(a => a.Kladr, opt => opt.MapFrom(r => r.city_kladr_id))
                .ForMember(a=>a.City,opt=>opt.MapFrom(r=>r.city))
                .ForMember(a=>a.Street,opt=>opt.MapFrom(r=>r.street))
                .ForMember(a=>a.HouseNumber,opt=>opt.MapFrom(r=>r.house));
        }
    }
}
