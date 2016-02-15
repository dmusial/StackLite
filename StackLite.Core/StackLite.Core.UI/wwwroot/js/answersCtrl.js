angular.module('stackLite')
    .controller('answersCtrl', function($scope, question, answerService) {
        $scope.question = question;
        $scope.answers = answerService.getAll();

        $scope.addAnswer = function(answerModel) {
            answerService.suggest(answerModel)
            .then(function() {
                    $scope.newAnswer = "";
                });
        }
        $scope.upvote = function(answer) {
            answerService.upvote(answerId);
        }
        $scope.downvote = function(answerId) {
            answerService.down(answerId);
        }
    })
    .service('questionAnswerService', function($resource) {
        return function(questionId) {
           
            var answers = $resource("question/"+questionId+"/answers/:answerId:suffix", {
                answerId: '@answerId',
                suffix:'@suffix'
            }, {
                update: {
                    method: 'PUT'
                },
                upvote:{
                    method:'PUT',
                    params:{
                        suffix:'/upvote'
                    }
                },
                downvote:{
                    method:'PUT',
                    params:{
                        suffix:'/downvote'
                    }
                }
            });



            return {
                getAll: answers.query,
                upvote:function(answersId) {
                    return answers.upvote({ answerId: answerId }).$promise;
                },
                downvote:function(answersId) {
                    return answers.downvote({ answerId: answerId }).$promise;
                },
                suggest: function(answerModel) {
                    return answers.save({ 'answerContent': answerModel }).$promise;
                },
                amend: function(answerModel) {
                    return answers.put({
                        answerId: answerModel.id
                    }, { 'answerContent': answerModel }).$promise;
                }
            }


        }

    });
