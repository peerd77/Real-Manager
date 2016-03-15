using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Real_Manager.Models;

namespace Real_Manager.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [HttpPost]
        public async Task<ActionResult> GetTeams(TokenVM tokenn)
        {
            if (true)
            {
                var teams = await Manager.getTeams();
                var camelCaseFormater = new JsonSerializerSettings();
                camelCaseFormater.ContractResolver = new CamelCasePropertyNamesContractResolver();
                var jsonResult = new ContentResult
                {
                    Content = JsonConvert.SerializeObject(teams, camelCaseFormater),
                    ContentType = "application.json"
                };
                return jsonResult;
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public async Task<ActionResult> GetTeamPlayers(TokenVM tokenn, TeamVM team)
        {
            if (true)
            {
                var players = await Manager.getPlayers(team.Id);
                var camelCaseFormater = new JsonSerializerSettings();
                camelCaseFormater.ContractResolver = new CamelCasePropertyNamesContractResolver();
                var jsonResult = new ContentResult
                {
                    Content = JsonConvert.SerializeObject(players, camelCaseFormater),
                    ContentType = "application.json"
                };
                return jsonResult;
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginDataVM loginData)
        {
            if (ValidateRequest)
            {
                var token = new TokenVM
                {
                    Token = "a1B2c3D4e5"
                };
                return MakejsonResult(token);
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotAcceptable, "Email or Password is not correct");
        }

        private ContentResult MakejsonResult(object obj)
        {
            var camelCaseFormater = new JsonSerializerSettings();
            camelCaseFormater.ContractResolver = new CamelCasePropertyNamesContractResolver();
            var jsonResult = new ContentResult
            {
                Content = JsonConvert.SerializeObject(obj, camelCaseFormater),
                ContentType = "application/json"
            };
            return jsonResult;
        }


        [HttpPost]
        public async Task<ActionResult> SaveTeam(TokenVM tokenn, TeamVM team)
        {

            if (ValidateRequest)
            {
                if (team.Id == null)
                {
                    var teamAdded = await Manager.addTeam(team);
                    return MakejsonResult(teamAdded);
                }
                var teamUpdated = await Manager.updateTeam(team);
                return MakejsonResult(teamUpdated); //TODO: CHECK IF NULL AND SEND BACK ERROR MSG IF SO
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotAcceptable, "Email or Password is not correct");
        }

        [HttpPost]
        public async Task<ActionResult> DeleteTeam(TokenVM tokenn, TeamVM team)
        {
            if (ValidateRequest)
            {
                var ans = await Manager.deleteTeam(team);
                return MakejsonResult(ans); //TODO: MAYBE IF FALSE TO DO SOMTHING ELSE
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotAcceptable, "Email or Password is not correct");
        }
    }
}