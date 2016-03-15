app.controller('teamsCtrl', function ($scope, $http, $location, $route, dataFactory) {
    $scope.ans = null;
    $scope.sort = '';
    $scope.selected = false;
    $scope.selectedTeamButton = null;
    dataFactory.getNewTeams().then(function(response) {
        dataFactory.setTeams(response.data);
            $scope.teams = response.data;
        },
    function () {
        $scope.ans = 'Something Went Wrong';
        dataFactory.setTeams('Something went Wrong');
    });
    $scope.getTeamPlayers = function (teamId) {
        $location.path('/players');
    }


    $scope.getSort = function () {
        return $scope.sort;
    };

    $scope.setSort = function (newSort) {
        $scope.ans = "";
        $scope.sort = newSort;
        $location.path("/teams");
    };
    $scope.addTeam = function () {
        dataFactory.setIsEdittingNewTeam(true);
        dataFactory.setCtxTeam(null);
        dataFactory.resetNewTeamPlayerList();
        $location.path("/editTeam");
    }

    $scope.editTeam = function () {
        dataFactory.setIsEdittingNewTeam(true);
        var team = {id: $scope.selectedTeamId};
        dataFactory.getNewTeamPlayersFromDB(team).then(function(response) {
            dataFactory.setNewTeamPlayerList(response.data);
            $location.path("/editTeam");
        });
        
    }
    $scope.select = function (team) {
        $scope.ans = "";
        var self = $(this);
        if ($scope.selected && self[0] === $scope.selectedTeamButton[0]) {
            self.removeClass('active');
            $scope.selected = false;
            $scope.selectedTeamId = null;
            $(".link").addClass("disabled");
            $scope.selectedTeamButton = null;
            //dataFactory.setSelctedTeamId(null);

        } else {
            if ($scope.selectedTeamButton != null) {
                $scope.selectedTeamButton.removeClass('active');
                dataFactory.setSelctedTeamId(null);
            }
            $scope.selectedTeamButton = self;
            self.addClass('active');
            $(".link").removeClass("disabled");
            //dataFactory.setSelctedTeamId(teamId);
            dataFactory.setCtxTeam(team);
            $scope.selected = true;
            $scope.selectedTeamId = team.id;
        }
    }

    $scope.deleteTeam = function () {
        $scope.ans = "";
        dataFactory.deleteTeamFromDB().then(function (response) {
            if (response.data !== null) {
                $scope.ans = "Delete Succesful";
                $route.reload();
            } else {
                 $scope.ans = "Deletion Unsuccesful";
            }
        });
    }
 

}); // end of file