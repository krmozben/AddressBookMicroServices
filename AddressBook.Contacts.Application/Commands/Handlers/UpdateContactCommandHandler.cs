using AddressBook.Contacts.Domain.ContactsAggregate;
using AddressBook.Contacts.Infrastructure.Repositories.Base;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AddressBook.Contacts.Application.Commands.Handlers
{
    public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand>
    {
        private readonly IRepository<Contact> _contactRepository;
        private readonly ILogger<UpdateContactCommandHandler> _logger;

        public UpdateContactCommandHandler(IRepository<Contact> contactRepository, ILogger<UpdateContactCommandHandler> logger)
        {
            _contactRepository = contactRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
        {
            var contact = await _contactRepository.GetAsync(x => x.Id == request.Id);

            if (contact == null)
            {
                _logger.LogWarning($"No contact for this id:{request.Id} could be found.");
                return Unit.Value;
            }

            contact.UpdateContact(request.Name, request.LastName, request.Firm);

            await _contactRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
