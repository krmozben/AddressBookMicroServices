using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Person.Service.Model.Request
{
    public class CreatePersonRequest
    {
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}
