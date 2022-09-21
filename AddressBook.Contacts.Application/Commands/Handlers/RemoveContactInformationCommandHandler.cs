using AddressBook.Contacts.Domain.ContactsAggregate;
using AddressBook.Contacts.Infrastructure.Repositories.Base;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AddressBook.Contacts.Application.Commands.Handlers
{
    public class RemoveContactInformationCommandHandler : IRequestHandler<RemoveContactInformationCommand>
    {
        private readonly IRepository<Contact> _contactRepository;
        private readonly ILogger<RemoveContactInformationCommandHandler> _logger;

        public RemoveContactInformationCommandHandler(IRepository<Contact> contactRepository, ILogger<RemoveContactInformationCommandHandler> logger)
        {
            _contactRepository = contactRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(RemoveContactInformationCommand request, CancellationToken cancellationToken)
        {
            var contact = await _contactRepository.GetAsync(x => x.Uuid == request.Uuid);

            if (contact == null)
            {
                _logger.LogWarning($"No contact for this id:{request.Uuid} could be found.");
                return Unit.Value;
            }

            contact.RemoveContactInformation(request.ContactInformationId);

            await _contactRepository.UpdateAsync(contact);

            await _contactRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
