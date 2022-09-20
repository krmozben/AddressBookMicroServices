using AddressBook.Contacts.Application.Commands;
using AddressBook.Contacts.Application.Queries;
using AddressBook.Contacts.Application.Services.Implementation;
using AddressBook.Shared.Model.Request;
using MassTransit;
using MediatR;

namespace AddressBook.Contacts.Application.Consumers
{
    public class UpdateAllContactsOnRedisConsumer : IConsumer<UpdateAllContactsOnRedisRequest>
    {
        private readonly IMediator _mediatr;
        private readonly IContactService _contactService;

        public UpdateAllContactsOnRedisConsumer(IMediator mediatr, IContactService contactService)
        {
            _mediatr = mediatr;
            _contactService = contactService;
        }

        public async Task Consume(ConsumeContext<UpdateAllContactsOnRedisRequest> context)
        {
            var result = await _mediatr.Send(new GetAllContactQuery());

            await _contactService.SetAllContactsOnRedis(result);
        }
    }
}
