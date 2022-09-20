using AddressBook.Contacts.Application.Services.Implementation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AddressBook.Contacts.Application.Services
{
    public class ContactService : IContactService
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ContactService> _logger;

        public ContactService(IMediator mediator, ILogger<ContactService> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
    }
}
