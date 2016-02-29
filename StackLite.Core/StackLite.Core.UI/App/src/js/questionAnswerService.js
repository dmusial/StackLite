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
