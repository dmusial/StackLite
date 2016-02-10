angular.module('stackLite',['ui.router','ngMaterial'])
.config(function($stateProvider,$urlRouterProvider){
	 $urlRouterProvider.otherwise("/main");

    $stateProvider
    .state('questions-list', {
      url: "/main",
      templateUrl: "questions.html"
    })
     .state('ask-question', {
      url: "/question/new",
      templateUrl: "askQuestion.html"
    })
    .state('question-details', {
      url: "/question/:id",
      templateUrl: "answers.html"
    });

})
.controller(function($scope){

});