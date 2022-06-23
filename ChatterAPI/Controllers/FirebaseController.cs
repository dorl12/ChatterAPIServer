using Microsoft.AspNetCore.Mvc;
using ChatterAPI.Hubs;

namespace ChatterAPI.Controllers
{
    [ApiController]
    [Route("api/firebase")]
    public class FirebaseController : ControllerBase
    {
        public static Dictionary<String, String> firebaseTokens = new Dictionary<string, string>();

        public FirebaseController()
        {
        }

        [HttpPost]
        public IActionResult index([Bind("firebaseToken,username")] FirebaseData firebaseData)
        {
            if (firebaseTokens.ContainsKey(firebaseData.username))
            {
                firebaseTokens[firebaseData.username] = firebaseData.firebaseToken;
                return Ok("Updated!");
            }
            else
            {
                foreach (var element in firebaseTokens)
                {
                    if (element.Value == firebaseData.firebaseToken)
                    {
                        firebaseTokens.Remove(element.Key);
                        break;
                    }
                }
                firebaseTokens.Add(firebaseData.username, firebaseData.firebaseToken);
                return Ok("Added!");
            }
        }
    };
}