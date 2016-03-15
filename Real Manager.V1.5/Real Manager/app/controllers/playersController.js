app.controller('playersCtrl', function ($scope, $http, $location, dataFactory) {
    $scope.sort = '';
    var team = dataFactory.getCtxTeam();
    var id = team.id;
    dataFactory.getNewTeamPlayersFromDB({ id: id, name: "", points: "" }).then(function (response) {
        $scope.ans = 'Player Request is Succeful';
        dataFactory.setPlayers(response.data);
        $scope.players = response.data;
          
        },
    function () {
        $scope.ans = 'Something Went Wrong';
        dataFactory.setTeams('Something went Wrong');
    });

   

    $scope.getSort = function () {
        return $scope.sort;
    }
    $scope.setSort = function (newSort) {
        $scope.sort = newSort;
        $location.path('/teams');
    }

    $scope.selected = false;

   


});