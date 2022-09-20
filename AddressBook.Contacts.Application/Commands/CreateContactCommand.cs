using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Contacts.Application.Commands
{
    public class CreateContactCommand : IRequest
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Firm { get; set; }
    } 
}
