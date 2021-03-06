using JPProject.Admin.Domain.Commands.Clients;
using JPProject.Domain.Core.Events;

namespace JPProject.Admin.Domain.Events.Client
{
    public class ClientUpdatedEvent : Event
    {
        public UpdateClientCommand Request { get; }

        public ClientUpdatedEvent(UpdateClientCommand request)
            : base(EventTypes.Success)
        {
            Request = request;
            AggregateId = request.Client.ClientId;
        }
    }
}