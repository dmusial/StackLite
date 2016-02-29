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
