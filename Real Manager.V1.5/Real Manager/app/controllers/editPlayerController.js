app.controller('editPlayerCtrl', function ($scope, $http, $location, $route, dataFactory) {


    var player = dataFactory.getCtxPlayer();
    if (player == null || player == undefined || !player.id) {
        if (player != null && player != undefined) {
            $scope.name = player.name;
            $scope.stars = player.stars;
            $scope.price = player.price;
            $scope.position = player.position;
        }
       // $scope.playerId = 0;
        $scope.isAdd = true;
        //$scope.players = dataFactory.getNewPlayerPlayerList();
    } else {
        $scope.playerId = player.id;
        $scope.name = player.name;
        $scope.stars = player.stars;
        $scope.price = player.price;
        $scope.position = player.position;
        $scope.isAdd = false;
        var player2 = { id: $scope.playerId }
        //dataFactory.getNewPlayerPlayersFromDB(player2).then(function (response) {
        //    $scope.players = response.data;
        //},
        //   function (response) {
        //       $scope.ans = response;
        //   });
    }

    $scope.savePlayer = function () {
        var newPlayer = { id: $scope.playerId, name: $scope.name, stars: $scope.stars, position: $scope.position, price: $scope.price, teamId: dataFactory.getCtxTeam().id };//save player ctx
        if (dataFactory.getCtxPlayer() === null) {// new player
            dataFactory.addToNewTeamPlayerList(newPlayer);
            $location.path('/editTeam');
        } else {// exiting player
            var oldPlayer = dataFactory.getCtxPlayer();
            dataFactory.removeFromNewTeamPlayerList(oldPlayer);
            dataFactory.addToNewTeamPlayerList(newPlayer);
            dataFactory.setCtxPlayer(null);
            $location.path('/editTeam');
        }
       
    }
    
    
}); // end of file