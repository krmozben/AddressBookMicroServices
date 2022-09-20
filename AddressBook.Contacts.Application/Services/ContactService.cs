using AddressBook.Contacts.Application.Services.Implementation;
using AddressBook.Shared.Model.Request;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AddressBook.Contacts.Application.Services
{
    public class ContactService : IContactService
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ContactService> _logger;
        private readonly IBusControl _busControl;

        public ContactService(IMediator mediator, ILogger<ContactService> logger, IBusControl busControl)
        {
            _mediator = mediator;
            _logger = logger;
            _busControl = busControl;
        }

        public async Task CreateContact(CreateContactRequest request) => await _busControl.Publish(request);

        public async Task RemoveContact(int id) => await _busControl.Publish<RemoveContactRequest>(new() { Id = id });

        public async Task DeleteContact(int id) => await _busControl.Publish<DeleteContactRequest>(new() { Id = id });

        public async Task UpdateContact(UpdateContactRequest request) => await _busControl.Publish(request);

        public async Task AddContactInformation(AddContactInformationRequest request) => await _busControl.Publish(request);

        public async Task RemoveContactInformation(RemoveContactInformationRequest request) => await _busControl.Publish(request);
    }
}
