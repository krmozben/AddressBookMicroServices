using AddressBook.Contacts.Application.Commands;
using AddressBook.Shared.Model.Request;
using MassTransit;
using MediatR;

namespace AddressBook.Contacts.Application.Consumers
{
    public class RemoveContactInformationConsumer : IConsumer<RemoveContactInformationRequest>
    {
        private readonly IMediator _mediatr;

        public RemoveContactInformationConsumer(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        public async Task Consume(ConsumeContext<RemoveContactInformationRequest> context)
        {
            await _mediatr.Send(new RemoveContactInformationCommand()
            {
                ContactId = context.Message.ContactId,
                ContactInformationId = context.Message.ContactInformationId
            });
        }
    }
}
