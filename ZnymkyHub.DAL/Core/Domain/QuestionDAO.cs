using System;
using System.Collections.Generic;

namespace ZnymkyHub.DAL.Core.Domain
{
    public class QuestionDAO
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int Score { get; set; }
        public bool Deleted { get; set; }
        public virtual List<AnswerDAO> Answers { get; set; }
    }
}
