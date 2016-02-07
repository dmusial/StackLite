using System;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using StackLite.Core.Domain.Questions;
using StackLite.Core.Domain.Users;
using StackLite.Core.Persistance;
using StackLite.Core.UI.Models;

namespace StackLite.Core.UI.Controllers
{
    [Route("questions")]
    public class QuestionsController : Controller
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IQuestionsQuery _questionsQuery;
        private readonly ILogger _logger;
        
        public QuestionsController(IQuestionRepository questionRepository, IQuestionsQuery questionsQuery, ILoggerFactory loggerFactory)
        {
            _questionRepository = questionRepository;
            _questionsQuery = questionsQuery;
            _logger = loggerFactory.CreateLogger<QuestionsController>();
        }
        
        [HttpGet]
        public string Index()
        {
            int questionsCount = _questionsQuery.AllQuestionsCount();
            return string.Format("Welcome to StackLite! We're currently tracking {0} question(s)!", questionsCount);
        }
        
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var question = _questionsQuery.GetQuestionDetails(id);
            if (question == null)
                return HttpNotFound();
            else
                return new ObjectResult(question);
        }
        
        [HttpPost]
        [Route("ask")]
        public IActionResult Ask([FromBody]AskQuestionModel questionModel)
        {
            User user = new User("dmusial");
            Question question = user.Ask(questionModel.Content);
            
            _questionRepository.Save(question);
            
            _logger.LogInformation(string.Format("Question Asked: '{0}' by {1}. Question Id {2}", question.Content, question.AskedByUserName, question.Id));
            
            return new ObjectResult(question);
        }
        
        [HttpPost]
        [Route("amend")]
        public IActionResult Amend([FromBody]AmendQuestionModel questionModel)
        {
            var question = _questionRepository.Get(questionModel.QuestionId);
            
            if (question == null)
                return HttpNotFound();
            
            question.AmendContent(questionModel.Content);
            _questionRepository.Save(question);
            
            return new ObjectResult(question);
        }
    }
}
