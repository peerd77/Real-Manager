app.controller('editTeamCtrl', function ($scope, $http, $location, $route, dataFactory) {
    $scope.players = dataFactory.getNewTeamPlayerList();
    var team = dataFactory.getCtxTeam();
    $scope.selectedPlayerButton = null;
    $scope.playerSelected = false;
    if (team === null || team === undefined || !team.id) {
        if (team != null && team != undefined) {
            $scope.name = team.name;
            $scope.points = team.points;
        }
        $scope.isAdd = true;
        $scope.teamId = null;
    }
    else {
        $scope.teamId = team.id;
        $scope.name = team.name;
        $scope.points = team.points;
        $scope.version = team.version;
        $scope.isAdd = false;
    }
    $scope.addPlayer = function () {
        dataFactory.setCtxPlayer(null);
        var team = { id: $scope.teamId, name: $scope.name, points: $scope.points, players: $scope.players, version: $scope.version };//saving typed data
        dataFactory.setCtxTeam(team);
       $location.path('/editPlayer');
    }
    $scope.teamCreated = false;
    $scope.saving = false;
    $scope.updateTeam = function () {
        $scope.saving = true;
        $scope.newTeam = { id: $scope.teamId, name: $scope.name, points: $scope.points, players: $scope.players, version: $scope.version }
        dataFactory.saveTeam($scope.newTeam).then(function (response) {
            if (response.data === null) {
                $scope.ans = "update not succeded try again"
            }
            dataFactory.setCtxTeam(response.data);
            dataFactory.setIsEdittingNewTeam(false);
            $scope.teamCreated = true;
            $scope.saving = false;
            },
            function(response) {
                $scope.ans = response;
            });
    }

    $scope.returnToTeamPage = function () {
        dataFactory.setIsEdittingNewTeam(false);
        dataFactory.setCtxTeam(undefined);
        $location.path("/teams");
    }

    $scope.playerSelect = function (player) {
        $scope.ans = "";
        var self = $(this);
        if ($scope.playerSelected && self[0] === $scope.selectedPlayerButton[0]) {
            self.removeClass('active');
            $scope.playerSelected = false;
            $(".link").addClass("disabled");
            $scope.selectedPlayerButton = null;
            dataFactory.setSelctedPlayerId(null);


        } else {
            if ($scope.selectedPlayerButton != null) {
                $scope.selectedPlayerButton.removeClass('active');
                dataFactory.setSelctedPlayerId(null);
            }
            $scope.selectedPlayerButton = self;
            self.addClass('active');
            $(".link").removeClass("disabled");
            dataFactory.setCtxPlayer(player);
            $scope.playerSelected = true;
        }
    }
    $scope.editPlayer = function () {

        $location.path("/editPlayer");
    }

    $scope.deletePlayer = function() {
        dataFactory.removeFromNewTeamPlayerList(dataFactory.getCtxPlayer());
        dataFactory.setCtxPlayer(null);
        $location.path("/editTeam");
    }
}); // end of file





