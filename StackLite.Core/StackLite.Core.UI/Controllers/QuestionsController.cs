using System;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using StackLite.Core.Domain.Questions;
using StackLite.Core.Domain.Users;
using StackLite.Core.Persistance;
using StackLite.Core.UI.Models;
using StackLite.Core.Persistance.ReadModels;
using System.Linq;

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
        [Route("info")]
        public string Info()
        {
            int questionsCount = _questionsQuery.AllQuestionsCount();
            return string.Format("Welcome to StackLite! We're currently tracking {0} question(s)!", questionsCount);
        }
        
         [HttpGet]
        public IActionResult GetAll()
        {
            return new ObjectResult( _questionsQuery.AllQuestions());
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
        
        [HttpPut]
        public IActionResult Amend(Guid questinId, [FromBody]AmendQuestionModel questionModel)
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
