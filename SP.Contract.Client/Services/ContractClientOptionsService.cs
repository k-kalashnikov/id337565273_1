using System;
using System.Collections.Generic;
using System.Text;
using SP.Contract.Client.Interfaces;
using SP.Contract.Client.Settings;

namespace SP.Contract.Client.Services
{
    public class ContractClientOptionsService : IContractClientOptionsService
    {
        public ContractClientOptions ContractClientOptions { get; private set; }

        private const string ErrorMessage = "Parametr don`t be emty or null !";

        public ContractClientOptionsService(ContractClientOptions options)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options), ErrorMessage);
            }

            if (string.IsNullOrWhiteSpace(options.Uri))
            {
                throw new ArgumentNullException(nameof(options.Uri), ErrorMessage);
            }

            if (options.MaxReceiveMessageSize == 0)
            {
                options.MaxReceiveMessageSize = 300_000_000;
            }

            if (options.MaxSendMessageSize == 0)
            {
                options.MaxSendMessageSize = 300_000_000;
            }

            ContractClientOptions = options;
        }
    }
}
