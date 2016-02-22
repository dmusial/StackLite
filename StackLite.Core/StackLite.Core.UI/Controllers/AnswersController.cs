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
   [Route("question/{questionId}/answers")]
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
        
        [HttpGet]
        public IActionResult For(Guid questionId)
        {
            var answers = _answersQuery.GetAllForQuestion(questionId);
            
            if (!answers.Any())
                return new ObjectResult(new string[0]);
            else
                return new ObjectResult(answers);
        }
        
        [HttpPost("")]
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
  
        
        [HttpPut]
        [Route("{answerId}/upvote")]
        public IActionResult Upvote(Guid answerId,[FromBody]UpvoteModel upvote)
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
        [Route("{answerId}/downvote")]
        public IActionResult Downvote(Guid answerId,[FromBody]DownvoteModel downvote)
        {
            var answer = _answerRepository.Get(downvote.AnswerId);
            
            if (answer == null)
                return HttpNotFound();
                
            User user = new User("dmusial");
            answer.Downvote(user);
            
            _answerRepository.Save(answer);
            
            return new ObjectResult(answer);
        }
        
              
        [HttpPut("{answerId}")]
        [Route("amend")]
        public IActionResult Amend(Guid answerId, [FromBody]AmendAnswerModel answerModel)
        {
            var answer = _answerRepository.Get(answerModel.AnswerId);
            
            if (answer == null)
                return HttpNotFound();
            
            answer.AmendContent(answerModel.AnswerContent);
            _answerRepository.Save(answer);
            
            return new ObjectResult(answer);
        }
    }
}
