using System;
using System.Linq;
using Microsoft.AspNet.Mvc;
using StackLite.Core.Domain.Answers;
using StackLite.Core.Domain.Questions;
using StackLite.Core.Domain.Users;
using StackLite.Core.Persistance;
using StackLite.Core.UI.Models;

namespace StackLite.Core.UI.Controllers
{
    [Route("answers")]
    public class AnswersController : Controller
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IAnswersQuery _answersQuery;
        
        public AnswersController(IAnswerRepository answerRepository, IQuestionRepository questionRepository, IAnswersQuery answersQuery)
        {
            _answerRepository = answerRepository;
            _questionRepository = questionRepository;
            _answersQuery = answersQuery;
        }
        
        [HttpGet("{id}")]
        [Route("for/{id}")]
        public IActionResult For(Guid id)
        {
            var answers = _answersQuery.GetAllForQuestion(id);
            
            if (!answers.Any())
                return new ObjectResult("No answers for this question so far.");
            else
                return new ObjectResult(answers);
        }
        
        [HttpPost]
        [Route("suggest")]
        public IActionResult Suggest([FromBody]SuggestAnswerModel answerModel)
        {
            var question = _questionRepository.Get(answerModel.QuestionId);
            
            if (question == null)
                return HttpNotFound();
            
            User user = new User("dmusial");
            var answer = user.SuggestAnswerTo(question, answerModel.AnswerContent);
            _answerRepository.Save(answer);
            
            return new ObjectResult(answer);
        }
        
        [HttpPost]
        [Route("amend")]
        public IActionResult Amend([FromBody]AmendAnswerModel answerModel)
        {
            var answer = _answerRepository.Get(answerModel.AnswerId);
            
            if (answer == null)
                return HttpNotFound();
            
            answer.AmendContent(answerModel.AnswerContent);
            _answerRepository.Save(answer);
            
            return new ObjectResult(answer);
        }
        
        [HttpPost]
        [Route("upvote")]
        public IActionResult Upvote([FromBody]UpvoteModel upvote)
        {
            var answer = _answerRepository.Get(upvote.AnswerId);
            
            if (answer == null)
                return HttpNotFound();
                
            User user = new User("dmusial");
            answer.Upvote(user);
            
            _answerRepository.Save(answer);
            
            return new ObjectResult(answer);
        }
        
        [HttpPost]
        [Route("downvote")]
        public IActionResult Downvote([FromBody]DownvoteModel downvote)
        {
            var answer = _answerRepository.Get(downvote.AnswerId);
            
            if (answer == null)
                return HttpNotFound();
                
            User user = new User("dmusial");
            answer.Downvote(user);
            
            _answerRepository.Save(answer);
            
            return new ObjectResult(answer);
        }
    }
}
