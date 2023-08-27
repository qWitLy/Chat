using System;
using System.ServiceModel;

namespace wcf_chat
{
    internal class ServerUser
    {
        public Guid Id { get; set; }

        public String Name { get; set; }

        public OperationContext operationContext { get; set; }
    }
}
