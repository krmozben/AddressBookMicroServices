using AddressBook.Contacts.Application.Commands;
using AddressBook.Shared.Model.Request;
using MassTransit;
using MediatR;

namespace AddressBook.Contacts.Application.Consumers
{
    public class RemoveContactConsumer : IConsumer<RemoveContactRequest>
    {
        private readonly IMediator _mediatr;

        public RemoveContactConsumer(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        public async Task Consume(ConsumeContext<RemoveContactRequest> context)
        {
            await _mediatr.Send(new RemoveContactCommand() { Id = context.Message.Id });
        }
    }
}
