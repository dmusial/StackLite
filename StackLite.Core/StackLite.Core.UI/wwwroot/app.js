angular.module('stackLite',['ui.router','ngResource','ngMaterial'])
.config(function($stateProvider,$urlRouterProvider){
	 $urlRouterProvider.otherwise("/main");

    $stateProvider
    .state('questions-list', {
      url: "/main",
      controller:'questionsCtrl',
      templateUrl: "questions.html"
    })
     .state('ask-question', {
      url: "/question/new",
      controller:'newQuestionCtrl',
      templateUrl: "askQuestion.html"
    })
    .state('question-details', {
      url: "/question/:id",
      controller:'answersCtrl',
      templateUrl: "answers.html",
      resolve:{
      	answerService:function(questionAnswerService,$stateParams){
      		return questionAnswerService($stateParams.id)
      	},
   		question:function(questionsService,$stateParams){
   			return questionsService.get($stateParams.id)
   		}
      }
    });

});
angular.module('stackLite')
    .controller('answersCtrl', function($scope, question, answerService) {
        $scope.question = question;
        $scope.answers = answerService.getAll();

        $scope.addAnswer = function(answerModel) {
            answerService.suggest(answerModel)
            .then(function() {
                    $scope.newAnswer = "";
                    $scope.answers = answerService.getAll();

                });
        }
        $scope.upvote = function(answer) {
            answerService.upvote(answer.Id).then(function(a){
                answer.Votes = a.VoteScore;
            });
            
        }
        $scope.downvote = function(answer) {
            answerService.downvote(answer.Id).then(function(a){
                answer.Votes = a.VoteScore;
            });
        }
    });

angular.module('stackLite')
    .controller('newQuestionCtrl', function ($scope, $state,questionsService) {
        $scope.addQuestion = function (questionModel) {
            questionsService.ask({ 'Content': questionModel })
            .then(function() {
                $state.go('questions-list');
                });
        }
    });

angular.module('stackLite')
    .service('questionAnswerService', function($resource) {
        return function(questionId) {
           
            var answers = $resource("question/"+questionId+"/answers/:answerId/:suffix", {
                answerId: '@answerId',
                suffix:'@suffix'
            }, {
                update: {
                    method: 'PUT'
                },
                upvote:{
                    method:'PUT',
                    params:{
                        suffix:'upvote'
                    }
                },
                downvote:{
                    method:'PUT',
                    params:{
                        suffix:'downvote'
                    }
                }
            });



            return {
                getAll: answers.query,
                upvote:function(answerId) {
                    return answers.upvote({ answerId: answerId }).$promise;
                },
                downvote:function(answerId) {
                    return answers.downvote({ answerId: answerId }).$promise;
                },
                suggest: function(answerModel) {
                    return answers.save({ 'answerContent': answerModel, QuestionId: questionId }).$promise;
                },
                amend: function(answerModel) {
                    return answers.put({
                        answerId: answerModel.id
                    }, { 'answerContent': answerModel }).$promise;
                }
            }


        }

    });

angular.module('stackLite')
    .controller('questionsCtrl', function ($scope, questionsService) {
        $scope.questions = questionsService.getAll();
    });

angular.module('stackLite')
    .service('questionsService', function ($resource) {
        var questionApi = "";
        var questions = $resource("questions/:id", {
            id: '@id'
        }, {
                update: {
                    method: 'PUT'
                }
            });
        return {
            get: function (questionId) {
                return questions.get({ id: questionId }).$promise;
            },
            getAll: questions.query,
            ask: function (questionModel) {
                return questions.save(questionModel).$promise;
            },
            amend: function (questionModel) {
                return questions.put({ id: questionModel.id }, questionModel).$promise;
            }
        }

    });
