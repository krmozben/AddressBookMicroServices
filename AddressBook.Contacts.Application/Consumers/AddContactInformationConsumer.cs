using AddressBook.Contacts.Application.Commands;
using AddressBook.Shared.Model.Request;
using MassTransit;
using MediatR;

namespace AddressBook.Contacts.Application.Consumers
{
    public class AddContactInformationConsumer : IConsumer<AddContactInformationRequest>
    {
        private readonly IMediator _mediatr;

        public AddContactInformationConsumer(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        public async Task Consume(ConsumeContext<AddContactInformationRequest> context)
        {
            await _mediatr.Send(new AddContactInformationCommand()
            {
               Type = context.Message.Type,
               Content = context.Message.Content,
               ContactId = context.Message.ContactId
            });
        }
    }
}
