using AddressBook.Contacts.Domain.ContactsAggregate;
using AddressBook.Contacts.Infrastructure.Repositories.Base;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AddressBook.Contacts.Application.Commands.Handlers
{
    public class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand>
    {
        private readonly IRepository<Contact> _contactRepository;
        private readonly ILogger<DeleteContactCommandHandler> _logger;

        public DeleteContactCommandHandler(IRepository<Contact> contactRepository, ILogger<DeleteContactCommandHandler> logger)
        {
            _contactRepository = contactRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
        {
            var contact = await _contactRepository.GetAsync(x => x.Uuid == request.Uuid);

            if (contact == null)
            {
                _logger.LogWarning($"No contact for this id:{request.Uuid} could be found.");
                return Unit.Value;
            }

            await _contactRepository.DeleteAsync(contact);

            await _contactRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
