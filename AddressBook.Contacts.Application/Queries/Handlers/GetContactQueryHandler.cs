using AddressBook.Contacts.Domain.ContactsAggregate;
using AddressBook.Contacts.Infrastructure.Repositories.Base;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AddressBook.Contacts.Application.Queries.Handlers
{
    public class GetContactQueryHandler : IRequestHandler<GetContactQuery, Contact>
    {

        private readonly IRepository<Contact> _contactRepository;
        private readonly ILogger<GetContactQueryHandler> _logger;

        public GetContactQueryHandler(IRepository<Contact> contactRepository, ILogger<GetContactQueryHandler> logger)
        {
            _contactRepository = contactRepository;
            _logger = logger;
        }

        public async Task<Contact> Handle(GetContactQuery request, CancellationToken cancellationToken)
        {
            var result = await _contactRepository.GetAsync(x => x.Id == request.Id);

            return result;
        }
    }
}
