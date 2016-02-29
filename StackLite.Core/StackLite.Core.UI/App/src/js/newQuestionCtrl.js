angular.module('stackLite')
    .controller('newQuestionCtrl', function ($scope, $state,questionsService) {
        $scope.addQuestion = function (questionModel) {
            questionsService.ask({ 'Content': questionModel })
            .then(function() {
                $state.go('questions-list');
                });
        }
    });
