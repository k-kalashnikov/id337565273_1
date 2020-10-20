using System;
using SP.Contract.Domains.AggregatesModel.Contract.Notifications;
using SP.Contract.Domains.AggregatesModel.Misc.Entities;
using SP.Contract.Domains.Common;

namespace SP.Contract.Domains.AggregatesModel.Contract.Entities
{
  public class Contract : Entity, IAggregateRoot
  {
    private int _contractStatusId;

    private int _contractTypeId;

    private long _currencyId;

    private long _customerOrganizationId;

    private long _contractorOrganizationId;

    private Contract()
    {
    }

    public Contract(Guid? parent, int contractTypeId, int? contractStatusId, long customerOrganizationId, long contractorOrganizationId, string number, DateTime startDate, DateTime finishDate)
    {
      Id = Guid.NewGuid();
      Parent = parent;
      _contractTypeId = contractTypeId;
      _customerOrganizationId = customerOrganizationId;
      _contractorOrganizationId = contractorOrganizationId;
      Number = number;
      StartDate = startDate;
      FinishDate = finishDate;

      _contractStatusId = contractStatusId.HasValue ? contractStatusId.Value : ContractStatus.Draft.Id;

      AddCreatedDomainEvent();
    }

    public void Update(in Guid? parentId, in int contractStatusId, in long customerOrganizationId, in long contractorOrganizationId, string number, in DateTime startDate, in DateTime finishDate)
    {
      Parent = parentId;
      _contractStatusId = contractStatusId;
      _customerOrganizationId = customerOrganizationId;
      _contractorOrganizationId = contractorOrganizationId;
      Number = number;
      StartDate = startDate;
      FinishDate = finishDate;

      AddUpdatedDomainEvent();
    }

    public override Guid Id { get; protected set; }

    public Guid? Parent { get; protected set; }

    public ContractStatus ContractStatus { get; protected set; }

    public ContractType ContractType { get; protected set; }

    public Organization CustomerOrganization { get; protected set; }

    public Organization ContractorOrganization { get; protected set; }

    public string Number { get; protected set; }

    public DateTime StartDate { get; protected set; }

    public DateTime FinishDate { get; protected set; }

    public bool? SignedByCustomer { get; protected set; }

    public bool? SignedByContractor { get; protected set; }

    private void AddCreatedDomainEvent()
    {
      AddDomainEvent(new CreateContractNotification(
          Id,
          Parent,
          _contractStatusId,
          _contractTypeId,
          _customerOrganizationId,
          _contractorOrganizationId,
          Number,
          StartDate,
          FinishDate,
          SignedByCustomer,
          SignedByContractor));
    }

    private void AddUpdatedDomainEvent()
    {
      AddDomainEvent(new UpdateContractNotification(
          Id,
          Parent,
          _contractStatusId,
          _contractTypeId,
          _customerOrganizationId,
          _contractorOrganizationId,
          Number,
          StartDate,
          FinishDate,
          SignedByCustomer,
          SignedByContractor));
    }
  }
}
