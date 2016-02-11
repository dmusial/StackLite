using System;
using System.Linq;
using Microsoft.AspNet.Mvc;
using StackLite.Core.Domain.Answers;
using StackLite.Core.Domain.Questions;
using StackLite.Core.Domain.Users;
using StackLite.Core.UI.Models;

namespace StackLite.Core.UI.Controllers
    {
        [Route("question/{id}/answers")]
        public class AnswersController : Controller
        {
            private readonly IAnswerRepository _answerRepository;
            private readonly IQuestionRepository _questionRepository;

            public AnswersController(IAnswerRepository answerRepository, IQuestionRepository questionRepository)
            {
                _answerRepository = answerRepository;
            _questionRepository = questionRepository;
            }

            [HttpGet]
            public IActionResult For(Guid id)
            {
                var answers = _answerRepository.GetAllFor(id);

                if (!answers.Any())
                    return new ObjectResult("No answers for this question so far.");
                else
                    return new ObjectResult(answers);
            }

            [HttpPost]
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

            [HttpPut("{answerId}")]
            public IActionResult Amend(Guid answerId, AmendAnswerModel answerModel)
            {
                var answer = _answerRepository.Get(answerModel.AnswerId);

                if (answer == null)
                    return HttpNotFound();

                answer.AmendContent(answerModel.AnswerContent);
                _answerRepository.Save(answer);

                return new ObjectResult(answer);
            }

            [HttpPost]
            [Route("upvote/{answerId}")]
            public IActionResult Upvote(Guid answerId)
            {
                var answer = _answerRepository.Get(answerId);

                if (answer == null)
                    return HttpNotFound();

                User user = new User("dmusial");
                answer.Upvote(user);

                _answerRepository.Save(answer);

                return new ObjectResult(answer);
            }

            [HttpPost]
            [Route("downvote/{answerId}")]
            public IActionResult Downvote(Guid answerId)
            {
                var answer = _answerRepository.Get(answerId);

                if (answer == null)
                    return HttpNotFound();

                User user = new User("dmusial");
                answer.Downvote(user);

                _answerRepository.Save(answer);

                return new ObjectResult(answer);
            }
        }
    }

    