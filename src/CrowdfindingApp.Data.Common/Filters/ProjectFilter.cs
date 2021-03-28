﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CrowdfindingApp.Data.Common.BusinessModels;

namespace CrowdfindingApp.Data.Common.Filters
{
    public class ProjectFilter
    {
        public List<Guid> Id { get; set; }
        public List<Guid> OwnerId { get; set; }
        public List<string> Title { get; set; }
        public List<Guid> CategoryId { get; set; }
        public List<int> Status { get; set; }
        public Expression<Func<Project, object>> OrderBy { get; set; }
        public bool DescendingOrder { get; set; }
    }
}
