﻿using System;
using System.Collections.Generic;

namespace CrowdfindingApp.Common.Data.Filters
{
    public class OrderFilter
    {
        public List<Guid> Id { get; set; }
        public List<Guid> RewardId { get; set; }
        public List<Guid> UserId { get; set; }
    }
}
