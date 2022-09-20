using AddressBook.Contacts.Domain.ContactsAggregate;
using AddressBook.Contacts.Infrastructure.Repositories.Base;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AddressBook.Contacts.Application.Commands.Handlers
{
    public class AddContactInformationCommandHandler : IRequestHandler<AddContactInformationCommand>
    {
        private readonly IRepository<Contact> _contactRepository;
        private readonly ILogger<AddContactInformationCommandHandler> _logger;

        public AddContactInformationCommandHandler(IRepository<Contact> shipmentRepository, ILogger<AddContactInformationCommandHandler> logger)
        {
            _contactRepository = shipmentRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(AddContactInformationCommand request, CancellationToken cancellationToken)
        {
            var contact = await _contactRepository.GetAsync(x => x.Id == request.ContactId);

            if (contact == null)
            {
                _logger.LogWarning($"No contact for this id:{request.ContactId} could be found.");
                return Unit.Value;
            }

            contact.AddContactInformation(new ContactInformation(request.Type, request.Content));

            await _contactRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
