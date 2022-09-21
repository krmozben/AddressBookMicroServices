using AddressBook.Contacts.Application.Clients.Redis;
using AddressBook.Contacts.Application.Services.Implementation;
using AddressBook.Contacts.Domain.ContactsAggregate;
using AddressBook.Shared.Model.Request;
using AddressBook.Shared.Model.Response;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using AddressBook.Contacts.Application.Queries;

namespace AddressBook.Contacts.Application.Services
{
    public class ContactService : IContactService
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ContactService> _logger;
        private readonly IBusControl _busControl;
        private readonly RedisClient _redisClient;

        public ContactService(IMediator mediator, ILogger<ContactService> logger, IBusControl busControl, RedisClient redisClient)
        {
            _mediator = mediator;
            _logger = logger;
            _busControl = busControl;
            _redisClient = redisClient;
        }

        public async Task CreateContact(CreateContactRequest request)
        {
            await _busControl.Publish(request);
            await UpdateAllContactsOnRedis();
        }

        public async Task RemoveContact(int id)
        {
            await _busControl.Publish<RemoveContactRequest>(new() { Id = id });
            await UpdateAllContactsOnRedis();
        }

        public async Task DeleteContact(int id)
        {
            await _busControl.Publish<DeleteContactRequest>(new() { Id = id });
            await UpdateAllContactsOnRedis();
        }

        public async Task UpdateContact(UpdateContactRequest request)
        {
            await _busControl.Publish(request);
            await UpdateAllContactsOnRedis();
        }

        public async Task AddContactInformation(AddContactInformationRequest request)
        {
            await _busControl.Publish(request);
        }

        public async Task RemoveContactInformation(RemoveContactInformationRequest request)
        {
            await _busControl.Publish(request);
        }

        public async Task<List<ContactResponse>> GetAllContacts()
        {
            var response = new List<ContactResponse>();

            var keyExists = await _redisClient.GetDb().KeyExistsAsync("AllContacts");

            if (!keyExists)
            {
                var contacts = await _mediator.Send(new GetAllContactQuery());

                await SetAllContactsOnRedis(contacts);

            }
            var cacheResult = await _redisClient.GetDb().StringGetAsync("AllContacts");

            response = JsonSerializer.Deserialize<List<ContactResponse>>(cacheResult);

            return response;
        }

        public async Task SetAllContactsOnRedis(List<Contact> contacts)
        {
            List<ContactResponse> data = contacts.Select(x => new ContactResponse
            {
                Firm = x.Firm.Name,
                LastName = x.LastName,
                Name = x.Name
            }).ToList();

            var status = await _redisClient.GetDb().StringSetAsync("AllContacts", JsonSerializer.Serialize(data));
        }

        public async Task<ContactInformationResponse> GetContact(int Id)
        {
            var response = new ContactInformationResponse();

            var result = await _mediator.Send(new GetContactQuery() { Id = Id });

            if (result == null)
            {
                _logger.LogWarning($"No contact for this id:{Id} could be found.");
                return null;
            }

            response = new ContactInformationResponse
            {
                ContactInformation = result.ContactInformation.Select(x => new InformationResponse() { Content = x.Content, Type = x.Type }).ToList(),
                Firm = result.Firm.Name,
                LastName = result.LastName,
                Name = result.Name
            };

            return response;
        }

        public async Task<List<LocationReportResponse>> GetLocationReport()
        {
           return await _mediator.Send(new LocationReportQuery());
        }

        private async Task UpdateAllContactsOnRedis()
        {
            await _busControl.Publish(new UpdateAllContactsOnRedisRequest());
        }
    }
}
