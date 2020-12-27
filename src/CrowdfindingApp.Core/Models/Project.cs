using System;

namespace CrowdfindingApp.Core.Models
{
    public sealed class Project : BaseModel
    {
        public Guid OwnerId { get; set; }
        public Guid CategoryId { get; set; }
        public bool IsSketch { get; set; }
        public bool OnModeration { get; set; }
        public bool IsStopped { get; set; }
        public DateTime? LastModifiedDateTime { get; set; }
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
        public string AuthorPersonalNumber { get; set; }
        public string WhomGivenDocument { get; set; }
        public DateTime? WhenGivenDocument { get; set; }
        public string AuthorAddress { get; set; }
        public string AuthorPhone { get; set; }

    }
}
