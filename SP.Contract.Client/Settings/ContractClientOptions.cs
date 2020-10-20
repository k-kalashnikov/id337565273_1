using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Contract.Client.Settings
{
    public class ContractClientOptions
    {
        public string Uri { get; set; }

        public int? MaxReceiveMessageSize { get; set; } = 300_000_000;

        public int? MaxSendMessageSize { get; set; } = 300_000_000;
    }
}
