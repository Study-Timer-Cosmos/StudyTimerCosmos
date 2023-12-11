﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyTimer.Domain.Common
{
    public class EntityBase<TKey> : ICreatedOn, IModifiedOn, IDeletedOn
    {
        public TKey Id { get; set; }

        public string? CreatedByUserId { get; set; }
        public DateTime CreatedOn { get; set; }

        public string? ModifiedByUserId { get; set; }
       

        public string? DeletedByUserId { get; set; }
        public DateTime DeletedOn { get; set; }
        public bool IsDeleted { get; set; }
        DateTime? ModifiedOn { get; set; }
    }
}
