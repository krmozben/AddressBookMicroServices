using AddressBook.Contacts.Application.Commands;
using AddressBook.Shared.Model.Request;
using MassTransit;
using MediatR;

namespace AddressBook.Contacts.Application.Consumers
{
    public class UpdateContactConsumer : IConsumer<UpdateContactRequest>
    {
        private readonly IMediator _mediatr;

        public UpdateContactConsumer(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        public async Task Consume(ConsumeContext<UpdateContactRequest> context)
        {
            await _mediatr.Send(new UpdateContactCommand()
            {
                Id = context.Message.Id,
                Name = context.Message.Name,
                Firm = context.Message.Firm,
                LastName = context.Message.LastName
            });
        }
    }
}
