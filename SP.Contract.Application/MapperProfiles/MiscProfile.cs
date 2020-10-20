using SP.Contract.Application.Account.Models;
using SP.Contract.Application.Contract.Commands.Update;
using SP.Contract.Application.Contract.Models;
using SP.Contract.Application.ContractDocuments.Models;
using SP.Contract.Application.ContractShort.Models;
using SP.Contract.Application.ContractStatus.Models;
using SP.Contract.Application.ContractType.Models;
using SP.Contract.Domains.AggregatesModel.Contract.Notifications;
using SP.Contract.Events.Event.CreateContract;
using SP.Contract.Events.Event.UpdateContract;
using DMContract = SP.Contract.Domains.AggregatesModel.Contract.Entities;
using DMEntity = SP.Contract.Domains.AggregatesModel.Misc.Entities;
using EventModel = SP.Contract.Events.Models;
using Profile = AutoMapper.Profile;

namespace SP.Contract.Application.MapperProfiles
{
    public class MiscProfile : Profile
    {
        public MiscProfile()
        {
            CreateMap<DMContract.ContractType, ContractTypeDto>();
            CreateMap<DMContract.ContractStatus, ContractStatusDto>();
            CreateMap<DMEntity.Organization, OrganizationDto>();
            CreateMap<DMContract.Contract, ContractDto>()
                .ForMember(x => x.ParentId, opt => opt.MapFrom(so => so.Parent))
                .ForMember(x => x.ContractorOrganization, opt => opt.MapFrom(so => so.ContractorOrganization))
                .ForMember(x => x.CustomerOrganization, opt => opt.MapFrom(so => so.CustomerOrganization));

            CreateMap<DMContract.Contract, ContractShortDto>();
            CreateMap<DMContract.ContractDocument, ContractDocumentDto>()
                .ForMember(x => x.ContractId, opt => opt.MapFrom(so => so.Contract.Id));

            CreateMap<ContractStatusDto, EventModel.ContractStatusDto>();
            CreateMap<ContractTypeDto, EventModel.ContractTypeDto>();

            CreateMap<ContractDto, EventModel.ContractDto>()
                .ForMember(x => x.ContractorOrganizationId, opt => opt.MapFrom(so => so.ContractorOrganization.ID))
                .ForMember(x => x.CustomerOrganizationId, opt => opt.MapFrom(so => so.CustomerOrganization.ID));

            CreateMap<DMEntity.Account, AccountDto>()
                .ForMember(x => x.OrganizationId, opt => opt.MapFrom(so => so.Organization.Id));

            // Events
            CreateMap<CreateContractNotification, CreateContractEvent>();
            CreateMap<UpdateContractNotification, UpdateContractEvent>();
        }
    }
}
