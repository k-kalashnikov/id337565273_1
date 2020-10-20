using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SP.Contract.Application.Account.Commands.CreateOrUpdate;
using SP.Contract.Application.Common.Handlers;
using SP.Contract.Application.Common.Interfaces;
using SP.Contract.Application.Common.Response;
using SP.Contract.Domains.AggregatesModel.Contract.Entities;
using SP.FileStorage.Client.Models;
using SP.FileStorage.Client.Services;
using SP.Market.Identity.Common.Interfaces;

namespace SP.Contract.Application.ContractDocuments.Commands.LoadContractDocumentByContractId
{
    public class LoadContractDocumentByContractIdCommandHandler : HandlerBase<LoadContractDocumentByContractIdCommand, ProcessingResult<bool>>
    {
        private readonly IFileStorageClientService _fileStorageClientService;
        private readonly IMediator _mediator;

        public LoadContractDocumentByContractIdCommandHandler(IApplicationDbContext applicationDbContext, ICurrentUserService currentUserService, IFileStorageClientService fileStorageClientService, IMediator mediator)
            : base(applicationDbContext, currentUserService)
        {
            _fileStorageClientService = fileStorageClientService
                                             ?? throw new ArgumentNullException(nameof(fileStorageClientService));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public override async Task<ProcessingResult<bool>> Handle(LoadContractDocumentByContractIdCommand request, CancellationToken cancellationToken)
        {
            var extension = string.Concat(".", request.Data.FileName.Split(".").Last());
            var fileName = string.Concat(request.ContractId.ToString(), "-", request.Data.FileName);
            PutFileDto resultPut;

            using (var fileStream = request.Data.OpenReadStream())
            {
                var payload = Payload.Build(Resources.Resource.NameBucket_ContractDocuments, fileStream, fileName, extension, fileStream.Length);
                resultPut = await _fileStorageClientService.PutObjectInBucketAsync(payload, cancellationToken);
            }

            if (!string.IsNullOrEmpty(resultPut?.FullLink))
            {
                await _mediator.Send(
                    new CreateOrUpdateAccountCommand(
                        new List<long>
                        {
                            CurrentUserService.GetCurrentUser().Id
                        }), cancellationToken);

                var сontractDocument = await ContextDb
                    .Set<Domains.AggregatesModel.Contract.Entities.Contract>()
                    .Where(p => p.Id == request.ContractId).SingleOrDefaultAsync(cancellationToken);

                var contractDocument = new ContractDocument(
                    сontractDocument,
                    request.Name,
                    request.Data.FileName,
                    resultPut.Link);

                await ContextDb
                    .Set<ContractDocument>()
                    .AddAsync(contractDocument, cancellationToken);
                await ContextDb.SaveChangesAsync(cancellationToken);

                return ResultHelper.Success(true);
            }

            return new ProcessingResult<bool>(false, new[] { new string(Resources.Resource.LoadError_FaultLoad) });
        }
    }
}
