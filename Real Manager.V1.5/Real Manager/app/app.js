var app = angular.module('app', ['ngRoute'])
	.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
	    $routeProvider
			.when('/', {
			    controller: 'loginCtrl',
			    templateUrl: 'app/partials/login.html'
			})
            .when('/teams', {
                controller: 'teamsCtrl',
                templateUrl: 'app/partials/teams.html'
            })
            .when('/players', {
                controller: 'playersCtrl',
                templateUrl: 'app/partials/players.html'
            })
            .when('/editTeam', {
                controller: 'editTeamCtrl',
                templateUrl: 'app/partials/editTeam.html'
            })
            .when('/editPlayer', {
                controller: 'editPlayerCtrl',
                templateUrl: 'app/partials/editPlayer.html'
            })
			.otherwise({
			    redirectTo: '/'
			});
	    $locationProvider.html5Mode({
	        enabled: true,
	        requireBase: false
	    });
    }]);

app.factory('dataFactory', ['$http', function ($http) {
    var factory = {}
    var token = {};
    var teams = {};
    var players = {};
    var selectedTeamId = {};
    var newTeamPlayerList = [];
    var ctxTeam = {};
    var ctxPlayer = {};
    var isEdittingNewTeam = false;

   
    factory.setIsEdittingNewTeam = function (set) {
        isEdittingNewTeam = set;
    };
    factory.getIsEdittingNewTeam = function () {
        return IsEdittingNewTeam;
    };

    factory.setToken = function (newToken) {
        token = newToken;
    };
    factory.getToken = function () {
        return token;
    };

    factory.getTeams = function () {
        return teams;
    };
    factory.setTeams = function (newTeams) {
        teams = newTeams;
    };

    factory.setPlayers = function (newPlayers) {
        players = newPlayers;
    };

    factory.getTeamPlayers = function () {
        return players;
    };


    factory.login = function (data) {
        var req = {
            method: 'POST',
            url: 'http://localhost:15000/Home/Login',
            data: data
        }
        return $http(req);
    }

    factory.getNewTeams = function () {
        var token = factory.getToken();
        var req = {
            method: 'POST',
            url: 'http://localhost:15000/Home/GetTeams',
            data: {token: token}
        };
        return $http(req);
    };

    factory.getNewTeamPlayersFromDB = function (team) {
        var token = factory.getToken();
        var req = {
            method: 'POST',
            url: 'http://localhost:15000/Home/GetTeamPlayers',
            data: { token: token, team: team }
        };
        return $http(req);
    };
    factory.savePlayer = function (player) {
        var token = factory.getToken();
        var req = {
            method: 'POST',
            url: 'http://localhost:15000/Home/savePlayer',
            data: {token: token, player: player}
        }
        return $http(req);
    }

    factory.saveTeam = function (team) {
        var token = factory.getToken();
        var req = {
            method: 'POST',
            url: 'http://localhost:15000/Home/SaveTeam',
            data: {token: token, team: team}
        }
        return $http(req);
    }

    factory.deleteTeamFromDB = function () {
        var token = factory.getToken();
        var team = factory.getCtxTeam();
        var req = {
            method: 'POST',
            url: 'http://localhost:15000/Home/DeleteTeam',
            data: { token: token, team: team }
        }
        return $http(req);
    }

    //factory.deletePlayerFromDB = function (player) {
    //    var token = factory.getToken();
    //    var req = {
    //        method: 'POST',
    //        url: 'http://localhost:15000/Home/DeletePlayer',
    //        data: { token: token, player: player }
    //    }
    //    return $http(req);
    //}


    factory.setSelctedTeamId = function(teamId)
    {
        selectedTeamId = teamId;
    }

    factory.getSelctedTeamId = function() {
        return selectedTeamId;
    }

    factory.addToNewTeamPlayerList = function(newPlayer) {
        newTeamPlayerList.push(newPlayer);
    }

    factory.removeFromNewTeamPlayerList = function (player) {
        var index = newTeamPlayerList.indexOf(player);
        newTeamPlayerList.splice(index, 1);
    }

    factory.getNewTeamPlayerList = function () {
        return newTeamPlayerList;
    }

    factory.resetNewTeamPlayerList = function () {
       newTeamPlayerList = [];
    }

    factory.setNewTeamPlayerList = function (newPlayers) {
        if (newPlayers === "null") {
            newTeamPlayerList = [];
        } else {
            newTeamPlayerList = newPlayers;
        }
            
    }

    factory.setCtxTeam = function (team) {
        ctxTeam = team;
    }

    factory.getCtxTeam = function () {
        return ctxTeam;
    }

    factory.setCtxPlayer = function (player) {
        ctxPlayer = player;
    }

    factory.getCtxPlayer = function () {
        return ctxPlayer;
    }

    return factory;
    
}]);


