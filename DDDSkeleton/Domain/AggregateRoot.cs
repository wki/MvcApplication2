﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDSkeleton.Domain
{
    public class AggregateRoot<IdType> : Entity<IdType>
    {
        public AggregateRoot() : base()
        {
        }

        public AggregateRoot(IdType id) : base(id)
        {
        }
    }
}
