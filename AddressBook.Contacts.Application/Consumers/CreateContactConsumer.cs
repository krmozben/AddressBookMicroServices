using AddressBook.Contacts.Application.Commands;
using AddressBook.Shared.Model.Request;
using MassTransit;
using MediatR;

namespace AddressBook.Contacts.Application.Consumers
{
    public class CreateContactConsumer : IConsumer<CreateContactRequest>
    {
        private readonly IMediator _mediatr;

        public CreateContactConsumer(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        public async Task Consume(ConsumeContext<CreateContactRequest> context)
        {
            await _mediatr.Send(new CreateContactCommand()
            {
                Name = context.Message.Name,
                Firm = context.Message.Firm,
                LastName = context.Message.LastName
            });
        }
    }
}
