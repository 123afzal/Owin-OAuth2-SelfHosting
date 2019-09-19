using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace testingOwin
{
    [Authorize]
    public class PeopleController : ApiController
    {
        private readonly List<Person> _people;

        public PeopleController()
        {
            _people = new List<Person> {
            new Person { Age = 15, Id = 1, Name = "John Doe" },
            new Person { Age = 22, Id = 2, Name = "Jane Doe" }
        };
        }

        [Authorize]
        public IHttpActionResult Get()
        {
            return Ok(_people);
        }

        public IHttpActionResult Get(int id)
        {
            var person = _people.Find(p => p.Id == id);
            return Ok(person);
        }
    }
}
