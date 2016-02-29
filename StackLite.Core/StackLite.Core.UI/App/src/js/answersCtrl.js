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
