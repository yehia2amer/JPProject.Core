using JPProject.Domain.Core.Events;

namespace JPProject.Admin.Domain.Events.IdentityResource
{
    public class IdentityResourceRemovedEvent : Event
    {
        public IdentityResourceRemovedEvent(string name)
            : base(EventTypes.Success)
        {
            AggregateId = name;
        }
    }
}