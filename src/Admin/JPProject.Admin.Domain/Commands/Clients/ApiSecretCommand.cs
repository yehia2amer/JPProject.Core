using JPProject.Domain.Core.Commands;
using System;

namespace JPProject.Admin.Domain.Commands.Clients
{
    public abstract class ApiSecretCommand : Command
    {
        public string ResourceName { get; protected set; }
        public string Description { get; protected set; }

        public string Value { get; protected set; }
        public DateTime? Expiration { get; protected set; }

        public int Hash { get; protected set; } = 0;
        public string Type { get; protected set; }
    }
}