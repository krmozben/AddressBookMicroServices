using AddressBook.Contacts.Domain.ContactsAggregate;
using AddressBook.Contacts.Infrastructure.Repositories.Base;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AddressBook.Contacts.Application.Queries.Handlers
{
    public class GetAllContactQueryHandler : IRequestHandler<GetAllContactQuery, List<Contact>>
    {

        private readonly IRepository<Contact> _contactRepository;
        private readonly ILogger<GetAllContactQueryHandler> _logger;

        public GetAllContactQueryHandler(IRepository<Contact> contactRepository, ILogger<GetAllContactQueryHandler> logger)
        {
            _contactRepository = contactRepository;
            _logger = logger;
        }

        public async Task<List<Contact>> Handle(GetAllContactQuery request, CancellationToken cancellationToken)
        {
            var result = await _contactRepository.GetListAsync(x => true);

            return result.ToList();
        }
    }
}
