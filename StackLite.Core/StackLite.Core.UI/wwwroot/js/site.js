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