using AddressBook.Shared.Model.Response;
using MediatR;

namespace AddressBook.Contacts.Application.Queries
{
    public class LocationReportQuery : IRequest<List<LocationReportResponse>>
    {
    }
}
