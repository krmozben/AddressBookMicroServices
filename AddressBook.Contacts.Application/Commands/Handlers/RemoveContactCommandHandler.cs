using AddressBook.Contacts.Domain.ContactsAggregate;
using AddressBook.Contacts.Infrastructure.Repositories.Base;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AddressBook.Contacts.Application.Commands.Handlers
{
    public class RemoveContactCommandHandler : IRequestHandler<RemoveContactCommand>
    {
        private readonly IRepository<Contact> _contactRepository;
        private readonly ILogger<RemoveContactCommandHandler> _logger;

        public RemoveContactCommandHandler(IRepository<Contact> shipmentRepository, ILogger<RemoveContactCommandHandler> logger)
        {
            _contactRepository = shipmentRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(RemoveContactCommand request, CancellationToken cancellationToken)
        {
            var contact = await _contactRepository.GetAsync(x => x.Id == request.Id);

            if (contact == null)
            {
                _logger.LogWarning($"No contact for this id:{request.Id} could be found.");
                return Unit.Value;
            }

            contact.SetPassife();

            await _contactRepository.UpdateAsync(contact);

            await _contactRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
