using System;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using StackLite.Core.Domain.Questions;
using StackLite.Core.Domain.Users;
using StackLite.Core.FakeReportingStores;
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
        public List<QuestionData> Index()
        {
            return _questionsQuery.AllQuestions();
            
        }
        
       [HttpGet("info")]
       public string Info()
        {
            int questionsCount = _questionsQuery.AllQuestionsCount();
            return $"Welcome to StackLite! We're currently tracking {questionsCount} question(s)!";
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
