using AddressBook.Contacts.Domain.ContactsAggregate;
using AddressBook.Contacts.Infrastructure.Repositories.Base;
using MediatR;

namespace AddressBook.Contacts.Application.Commands.Handlers
{
    public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand>
    {
        private readonly IRepository<Contact> _contactRepository;

        public CreateContactCommandHandler(IRepository<Contact> contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<Unit> Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            await _contactRepository.AddAsync(new Contact(request.Name, request.LastName,request.Uuid, new Firm(request.Firm)));

            await _contactRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
