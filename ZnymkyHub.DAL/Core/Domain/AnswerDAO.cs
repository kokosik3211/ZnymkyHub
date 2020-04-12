using System;

namespace ZnymkyHub.DAL.Core.Domain
{
    public class AnswerDAO
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public string Body { get; set; }
        public int Score { get; set; }
        public bool Deleted { get; set; }

        public Guid QuestionId { get; set; }
        public virtual QuestionDAO Question { get; set; }
    }
}
