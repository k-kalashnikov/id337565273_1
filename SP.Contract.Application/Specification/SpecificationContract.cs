using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SP.Contract.Application.Common.Exceptions;
using SP.Contract.Application.Common.Paging;
using SP.Contract.Application.Contract.Models;
using SP.Service.Common.Filters.Filters;
using SP.Service.Common.Filters.Specification;
using ContractEntty = SP.Contract.Domains.AggregatesModel.Contract.Entities.Contract;

namespace SP.Contract.Application.Specification
{
    public class SpecificationContract
    {
        private static readonly IDictionary<string, Expression<Func<ContractEntty, object>>> _sortExpressions =
            new Dictionary<string, Expression<Func<ContractEntty, object>>>()
            {
                { "number", c => c.Number },
                { "contractorOrganization", c => c.ContractorOrganization.Name },
                { "customerOrganization", c => c.CustomerOrganization.Name },
                { "contractType", c => c.ContractType.Name },
                { "contractStatus", c => c.ContractStatus.Name },
                { "startDate", c => c.StartDate },
                { "finishDate", c => c.FinishDate },
                { "createdBy", c => c.CreatedBy.FullName },
                { "created", c => c.Created }
            };

        public ISpecification<ContractEntty> CreatePaging(IPageContext<ContractFilter> pageContext)
        {
            if (!pageContext.IsValid())
            {
                throw new BadRequestException("Invalid page context.");
            }

            ISpecification<ContractEntty> specification = SpecificationBuilder<ContractEntty>.Create();
            specification = Filter(specification, pageContext.Filter);
            specification = Sort(specification, pageContext.ListSort);

            if (pageContext.PageIndex != 0)
            {
                specification = specification.Skip(pageContext.PageSize * (pageContext.PageIndex - 1));
            }

            if (pageContext.PageSize != 0)
            {
                specification = specification.Take(pageContext.PageSize);
            }

            return specification;
        }

        private ISpecification<ContractEntty> Filter(ISpecification<ContractEntty> specification, ContractFilter filter)
        {
            if (filter.Number.HasValue())
            {
                specification = specification.FilterByString(c => c.Number, filter.Number);
            }

            if (filter.ContractorName.HasValue())
            {
                specification = specification.FilterByString(c => c.ContractorOrganization.Name, filter.ContractorName);
            }

            if (filter.CustomerName.HasValue())
            {
                specification = specification.FilterByString(c => c.CustomerOrganization.Name, filter.CustomerName);
            }

            if (filter.ContractTypeName.HasValue())
            {
                specification = specification.FilterByString(c => c.ContractType.Name, filter.ContractTypeName);
            }

            if (filter.ContractStatusName.HasValue())
            {
                specification = specification.FilterByString(c => c.ContractStatus.Name, filter.ContractStatusName);
            }

            if (filter.StartDate.HasValue())
            {
                specification = specification.FilterByDate(c => c.StartDate, filter.StartDate);
            }

            if (filter.FinishDate.HasValue())
            {
                specification = specification.FilterByDate(c => c.FinishDate, filter.FinishDate);
            }

            if (filter.CreatedBy.HasValue())
            {
                specification = specification.And(specification.FilterByString(c => c.CreatedBy.LastName, filter.CreatedBy).Or(specification.FilterByString(c => c.CreatedBy.FirstName, filter.CreatedBy).Or(specification.FilterByString(c => c.CreatedBy.MiddleName, filter.CreatedBy))));
            }

            if (filter.Created.HasValue())
            {
                specification = specification.FilterByDate(c => c.Created, filter.Created);
            }

            if (filter.ContractorOrganizationId.HasValue && filter.ContractorOrganizationId > 0)
            {
                specification = specification.And(x => x.ContractorOrganization.Id == filter.ContractorOrganizationId);
            }

            if (filter.CustomerOrganizationId.HasValue && filter.CustomerOrganizationId > 0)
            {
                specification = specification.And(x => x.CustomerOrganization.Id == filter.CustomerOrganizationId);
            }

            if (filter.ContractStatusId.HasValue && filter.ContractStatusId > 0)
            {
                specification = specification.And(x => x.ContractStatus.Id == filter.ContractStatusId);
            }

            if (filter.ContractTypeId.HasValue && filter.ContractTypeId > 0)
            {
                specification = specification.And(x => x.ContractType.Id == filter.ContractTypeId);
            }

            return specification;
        }

        private ISpecification<ContractEntty> Sort(ISpecification<ContractEntty> specification, IEnumerable<SortDescriptor> sorts) =>
            sorts.Aggregate(specification, (spec, srt) => Sort(spec, srt));

        private ISpecification<ContractEntty> Sort(ISpecification<ContractEntty> specification, SortDescriptor sort) =>
            _sortExpressions.TryGetValue(sort.Field, out var se)
                ? (sort.Direction == EnumSortDirection.Desc ? specification.OrderByDesc(se) : specification.OrderBy(se))
                : throw new BadRequestException($"Invalid field name {sort.Field}.");
    }
}
