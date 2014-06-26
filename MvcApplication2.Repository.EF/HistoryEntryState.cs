using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcApplication2.Repository.EF
{
    public class HistoryEntryState
    {
        public HistoryEntryState()
        {
        }

        public int Id { get; set; }
        public DateTime OccuredOn { get; set; }
        public string Message { get; set; }

        virtual public BusinessCardState BusinessCardState { get; set; }
    }
}
