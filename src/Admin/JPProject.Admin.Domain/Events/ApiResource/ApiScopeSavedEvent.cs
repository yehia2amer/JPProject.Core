using JPProject.Admin.Domain.Commands.ApiResource;
using JPProject.Domain.Core.Events;

namespace JPProject.Admin.Domain.Events.ApiResource
{
    public class ApiScopeSavedEvent : Event
    {
        public SaveApiScopeCommand Scope { get; }

        public ApiScopeSavedEvent(string resourceName, SaveApiScopeCommand scope)
        {
            Scope = scope;
            AggregateId = resourceName;
        }
    }
}