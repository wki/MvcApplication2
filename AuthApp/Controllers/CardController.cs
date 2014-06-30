using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Web.Controllers
{
    [Authorize]
    public class CardController : ApiController
    {
        // GET: api/Card
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Card/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Card
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Card/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Card/5
        public void Delete(int id)
        {
        }
    }
}
