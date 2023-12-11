﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyTimer.Domain.Common
{
    public interface IDeletedOn
    {
        public string? DeletedByUserId { get; set; }
        public DateTime DeletedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
