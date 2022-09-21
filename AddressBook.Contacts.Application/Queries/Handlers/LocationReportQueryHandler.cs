using AddressBook.Contacts.Domain.ContactsAggregate;
using AddressBook.Contacts.Infrastructure;
using AddressBook.Contacts.Infrastructure.Repositories.Base;
using AddressBook.Shared.Enums;
using AddressBook.Shared.Model.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AddressBook.Contacts.Application.Queries.Handlers
{
    public class LocationReportQueryHandler : IRequestHandler<LocationReportQuery, List<LocationReportResponse>>
    {

        private readonly IRepository<Contact> _contactRepository;
        private readonly ILogger<LocationReportQuery> _logger;
        private readonly AddressBookDbContext _context;

        public LocationReportQueryHandler(IRepository<Contact> contactRepository, ILogger<LocationReportQuery> logger, AddressBookDbContext context)
        {
            _contactRepository = contactRepository;
            _logger = logger;
            _context = context;
        }

        public async Task<List<LocationReportResponse>> Handle(LocationReportQuery request, CancellationToken cancellationToken)
        {
            var query = $"select COUNT(*) as 'Count',ci.Content as 'Location',(select COUNT(*) from ContactInformations where ContactId in (select ContactId from ContactInformations where Type = '{InformationType.Location.ToString()}' AND Content = ci.Content) and Type = '{InformationType.PhoneNumber.ToString()}') as 'PhoneNumberCount' from Contacts c INNER JOIN ContactInformations ci ON c.Id = ci.ContactId where Type = '{InformationType.Location.ToString()}' group By ci.Content order by 1 desc";


            var data = _context.LocationReports.FromSqlRaw(query).ToList().Select(x => new LocationReportResponse
            {
                Count = x.Count,
                Location = x.Location,
                PhoneNumberCount = x.PhoneNumberCount
            }).ToList();

            return data;
        }
    }
}
