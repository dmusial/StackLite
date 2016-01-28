using System;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using StackLight.Core.Domain.Questions;
using StackLight.Core.Domain.Users;
using StackLight.Core.UI.Models;

namespace StackLight.Core.UI.Controllers
{
    [Route("questions")]
    public class QuestionsController : Controller
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly ILogger _logger;
        
        public QuestionsController(IQuestionRepository questionRepository, ILoggerFactory loggerFactory)
        {
            _questionRepository = questionRepository;
            _logger = loggerFactory.CreateLogger<QuestionsController>();
        }
        
        [HttpGet]
        public string Index()
        {
            int questionsCount = _questionRepository.GetAllQuestionsCount();
            return string.Format("Welcome to StackLite! We're currently tracking {0} question(s)!", questionsCount);
        }
        
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var question = _questionRepository.Get(id);
            if (question == null)
                return HttpNotFound();
            else
                return new ObjectResult(question);
        }
        
        [HttpPost]
        [RouteAttribute("ask")]
        public IActionResult Ask([FromBody]AskQuestionModel questionModel)
        {
            User user = new User("dmusial");
            Question question = user.Ask(questionModel.Content);
            
            _questionRepository.Add(question);
            _questionRepository.Save();
            
            _logger.LogInformation(string.Format("Question Asked: '{0}' by {1}. Question Id {2}", question.Content, question.AskedByUserName, question.Id));
            
            return new ObjectResult(question);
        }
    }
}
