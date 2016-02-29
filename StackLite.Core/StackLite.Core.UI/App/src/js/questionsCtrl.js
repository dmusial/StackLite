angular.module('stackLite')
    .controller('questionsCtrl', function ($scope, questionsService) {
        $scope.questions = questionsService.getAll();
    });
