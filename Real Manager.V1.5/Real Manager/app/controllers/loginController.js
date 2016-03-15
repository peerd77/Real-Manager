app.controller('loginCtrl', function ($scope, $http, $location, dataFactory) {
    $scope.login = function () {     
        var data = { email: $scope.email, password: $scope.password }
        dataFactory.login(data).then(
            function(response) {
                $scope.ans = 'Login Is Succeful';
                dataFactory.setToken(response.data.token);
                $location.path('/teams');
            },
            function() {
                $scope.ans = 'Something Went Wrong';
                dataFactory.setTeams('Something went Wrong');
            });
    };

});