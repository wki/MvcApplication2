using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Web.Controllers
{
    public class InfoController : ApiController
    {
        // GET: api/Info
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Info/5
        public string Get(int id)
        {
            return String.Format("value: {0}", id);
        }

        // POST: api/Info
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Info/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Info/5
        public void Delete(int id)
        {
        }
    }
}
