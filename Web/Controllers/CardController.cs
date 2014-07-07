using Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MvcApplication2.Domain.Measurement;
using MvcApplication2.Repository.EF;

namespace Web.Controllers
{
    // TODO: add authorization when time comes.
    //[Authorize]
    public class CardController : ApiController
    {
        public static ILog Log = LogManager.GetCurrentClassLogger();
        private ICollectService collectService;

        public CardController(ICollectService collectService)
        {
            this.collectService = collectService;
        }

        // GET: api/Card
        public IQueryable<BusinessCardState> Get()
        {
            using (RepositoryContext context = new RepositoryContext())
            {
                return context.Set<BusinessCardState>();
            }
        }

        // GET: api/Card/5
        public BusinessCardState Get(int id)
        {
            BusinessCardState card;

            using (RepositoryContext context = new RepositoryContext())
            {
                card = context.BusinessCards.SingleOrDefault(b => b.Id == id);
            }

            if (card == null)
            {
                throw new HttpResponseException(
                    new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Content = new StringContent("Card not found")
                    }
                );
            }

            return card;
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
