using AddressBook.Contacts.Application.Commands;
using AddressBook.Shared.Model.Request;
using MassTransit;
using MediatR;

namespace AddressBook.Contacts.Application.Consumers
{
    public class DeleteContactConsumer : IConsumer<DeleteContactRequest>
    {
        private readonly IMediator _mediatr;

        public DeleteContactConsumer(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        public async Task Consume(ConsumeContext<DeleteContactRequest> context)
        {
            await _mediatr.Send(new DeleteContactCommand() { Id = context.Message.Id});
        }
    }
}
