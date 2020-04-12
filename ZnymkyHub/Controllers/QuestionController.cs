using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ZnymkyHub.DAL.Core;
using ZnymkyHub.DAL.Core.Domain;
using ZnymkyHub.Hubs;
using ZnymkyHub.Models.Question;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ZnymkyHub.Controllers
{
    [Route("api/[controller]")]
    public class QuestionController : BaseController
    {
        private readonly IHubContext<QuestionHub, IQuestionHub> hubContext;
        private readonly ClaimsPrincipal _caller;
        protected readonly IMapper _mapper;

        public QuestionController(IHubContext<QuestionHub, IQuestionHub> questionHub,
            IHttpContextAccessor httpContextAccessor,
            IUnitOfWork unitOfWork,
            IMapper mapper)
            :base(unitOfWork)
        {
            this.hubContext = questionHub;
            _caller = httpContextAccessor.HttpContext.User;
            _mapper = mapper;
        }

        [HttpGet()]
        public async Task<IEnumerable> GetQuestionsAsync()
        {
            var questions = await _unitOfWork.Context.Questions.Select(q => new
            {
                Id = q.Id,
                CreatedBy = q.CreatedBy,
                Title = q.Title,
                Body = q.Body,
                Score = q.Score,
                AnswerCount = q.Answers.Count
            }).ToListAsync();

            return questions;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetQuestionAsync(Guid id)
        {
            var question = await _unitOfWork.Context.Questions.SingleOrDefaultAsync(t => t.Id == id && !t.Deleted);
            if (question == null) return NotFound();

            var questionViewModel = _mapper.Map<Question>(question);
            return new JsonResult(questionViewModel);
        }

        [HttpPost()]
        [Authorize]
        public async Task<Question> AddQuestion([FromBody]Question question)
        {
            var name = _caller.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            question.CreatedBy = name?.Value;
            question.Deleted = false;
            question.Answers = new List<Answer>();

            var questionDao = _mapper.Map<QuestionDAO>(question);
            await _unitOfWork.Context.Questions.AddAsync(questionDao);
            await _unitOfWork.CommitAsync();

            var questionViewModel = _mapper.Map<Question>(questionDao);

            await this.hubContext.Clients.All.QuestionAdded(questionViewModel);
            return questionViewModel;
        }

        [HttpPost("{id}/answer")]
        [Authorize]
        public async Task<ActionResult> AddAnswerAsync(Guid id, [FromBody]Answer answer)
        {
            var questionDao = await _unitOfWork.Context.Questions.FirstOrDefaultAsync(q => q.Id == id);
            if (questionDao == null) return NotFound();

            var name = _caller.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            answer.CreatedBy = name?.Value;
            answer.Deleted = false;

            var answerDao = _mapper.Map<AnswerDAO>(answer);
            answerDao.Question = questionDao;
            await _unitOfWork.Context.Answers.AddAsync(answerDao);
            await _unitOfWork.CommitAsync();

            var answerViewModel = _mapper.Map<Answer>(answerDao);

            // Notify anyone connected to the group for this answer
            await this.hubContext.Clients.Group(id.ToString()).AnswerAdded(answerViewModel);
            // Notify every client
            var questionId = questionDao.Id;
            var questionAnswersCount = questionDao.Answers.Count;
            await this.hubContext.Clients.All.AnswerCountChange(questionId, questionAnswersCount);

            return new JsonResult(answerViewModel);
        }

        [HttpPatch("{id}/upvote")]
        [Authorize]
        public async Task<ActionResult> UpvoteQuestionAsync(Guid id)
        {
            var questionDao = await _unitOfWork.Context.Questions.FirstOrDefaultAsync(q => q.Id == id);
            if (questionDao == null) return NotFound();

            questionDao.Score++;
            await _unitOfWork.CommitAsync();

            // Notify every client
            await this.hubContext.Clients.All.QuestionScoreChange(questionDao.Id, questionDao.Score);

            var questionViewModel = _mapper.Map<Question>(questionDao);

            return new JsonResult(questionViewModel);
        }

        [HttpPatch("{id}/downvote")]
        [Authorize]
        public async Task<ActionResult> DownvoteQuestionAsync(Guid id)
        {
            var questionDao = await _unitOfWork.Context.Questions.FirstOrDefaultAsync(q => q.Id == id);
            if (questionDao == null) return NotFound();

            questionDao.Score--;
            await _unitOfWork.CommitAsync();

            // Notify every client
            await this.hubContext.Clients.All.QuestionScoreChange(questionDao.Id, questionDao.Score);

            var questionViewModel = _mapper.Map<Question>(questionDao);

            return new JsonResult(questionViewModel);
        }
    }
}
