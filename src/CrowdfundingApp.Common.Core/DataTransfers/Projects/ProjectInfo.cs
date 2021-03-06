﻿using System;
using System.Collections.Generic;
using CrowdfundingApp.Common.Core.DataTransfers.Questions;
using CrowdfundingApp.Common.Core.DataTransfers.Rewards;
using CrowdfundingApp.Common.Enums;

namespace CrowdfundingApp.Common.Core.DataTransfers.Projects
{
    public class ProjectInfo
    {
        public string Id { get; set; }
        public string CategoryId { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public string Location { get; set; }
        public string VideoUrl { get; set; }
        public string Image { get; set; }
        public DateTime? StartDateTime { get; set; }
        public int? Duration { get; set; }
        public decimal? Budget { get; set; }
        public string AuthorSurname { get; set; }
        public string AuthorName { get; set; }
        public DateTime? AuthorDateOfBirth { get; set; }
        public string AuthorMiddleName { get; set; }
        public string AuthorPersonalNo { get; set; }
        public string AuthorIdentificationNo { get; set; }
        public string WhomGivenDocument { get; set; }
        public DateTime? WhenGivenDocument { get; set; }
        public string AuthorAddress { get; set; }
        public string AuthorPhone { get; set; }
        public string OwnerId { get; set; }
        public ProjectStatus Status { get; set; }
        public List<RewardInfo> Rewards { get; set; }
        public List<QuestionInfo> Questions { get; set; }
    }
}
