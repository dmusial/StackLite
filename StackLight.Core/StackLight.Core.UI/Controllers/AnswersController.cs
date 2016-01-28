using System;
using System.Linq;
using Microsoft.AspNet.Mvc;
using StackLight.Core.Domain.Answers;
using StackLight.Core.Domain.Questions;
using StackLight.Core.Domain.Users;
using StackLight.Core.UI.Models;

namespace StackLight.Core.UI.Controllers
{
    [Route("answers")]
    public class AnswersController : Controller
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IQuestionRepository _questionRepository;
        
        public AnswersController(IAnswerRepository answerRepository, IQuestionRepository questionRepository)
        {
            _answerRepository = answerRepository;
            _questionRepository = questionRepository;
        }
        
        [HttpGet("{id}")]
        [Route("for/{id}")]
        public IActionResult For(Guid id)
        {
            var answers = _answerRepository.GetAllFor(id);
            
            if (!answers.Any())
                return new ObjectResult("No answers for this question so far.");
            else
                return new ObjectResult(answers);
        }
        
        [HttpPost]
        [RouteAttribute("suggest")]
        public IActionResult Suggest([FromBody]SuggestAnswerModel answerModel)
        {
            var question = _questionRepository.Get(answerModel.QuestionId);
            
            if (question == null)
                return HttpNotFound();
            
            User user = new User("dmusial");
            var answer = user.SuggestAnswerTo(question, answerModel.AnswerContent);
            _answerRepository.Add(answer);
            _answerRepository.Save();
            
            return new ObjectResult(answer);
        }
    }
}
