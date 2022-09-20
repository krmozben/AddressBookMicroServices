using AddressBook.Contacts.Application.Clients.Redis;
using AddressBook.Contacts.Application.Services.Implementation;
using AddressBook.Contacts.Domain.ContactsAggregate;
using AddressBook.Shared.Model.Request;
using AddressBook.Shared.Model.Response;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.Json;

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
            await UpdateAllContactsOnRedis();
        }

        public async Task RemoveContactInformation(RemoveContactInformationRequest request)
        {
            await _busControl.Publish(request);
            await UpdateAllContactsOnRedis();
        }

        public async Task<List<ContactResponse>> GetAllContacts()
        {
            var response = new List<ContactResponse>();

            var cacheResult = await _redisClient.GetDb().StringGetAsync("AllContacts");

            if (String.IsNullOrEmpty(cacheResult))
            {
                await _busControl.Publish(new UpdateAllContactsOnRedisRequest());
                return response;
            }

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

        private async Task UpdateAllContactsOnRedis()
        {
            await _busControl.Publish(new UpdateAllContactsOnRedisRequest());
        }
    }
}
