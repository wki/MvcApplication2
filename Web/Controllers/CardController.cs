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
        private RepositoryContext context;
        public static ILog Log = LogManager.GetCurrentClassLogger();
        private ICollectService collectService;

        public CardController(ICollectService collectService)
        {
            Log.Debug("Constructor");

            this.collectService = collectService;
            this.context = new RepositoryContext();

            // simple SQL Logging
            // this.context.Database.Log = Log.Debug;
        }

        // GET: api/Card
        public IEnumerable<BusinessCardState> Get()
        {
            Log.Debug("GET all");
            context.Configuration.LazyLoadingEnabled = false;
            return context.BusinessCards.ToList<BusinessCardState>();
        }

        // GET: api/Card/5
        public BusinessCardState Get(int id)
        {
            Log.Debug("GET id");

            BusinessCardState card = context.BusinessCards.SingleOrDefault(b => b.Id == id);

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
        // Test with: (Caution: Power-shell needs "" quotes.)
        // curl.exe -vXPOST -H "Content-Type: application/json" -d "{Name: 'howey5', EmployeeId: 2, Status: 1}" http://localhost:49230/api/card
        public void Post([FromBody] BusinessCardState newCard)
        {
            Log.Debug("Post");

            context.BusinessCards.Add(newCard);
            context.SaveChanges();
        }

        // PUT: api/Card/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Card/5
        public void Delete(int id)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Log.Debug("Disposing db Context");
                context.Dispose();
            }
            
            base.Dispose(disposing);
        }
    }
}
