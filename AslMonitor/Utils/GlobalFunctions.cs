using AslMonitor.DAL.Models;
using AslMonitor.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AslMonitor.Utils
{
    public class GlobalFunctions
    {


        public static string BaseUri = "https://localhost:7110/";
        public static bool CheckForInternetConnection(int timeoutMs = 10000)
        {
            try
            {
                string url = BaseUri + "api/connection";

                var request = (HttpWebRequest)WebRequest.Create(url);
                request.KeepAlive = false;
                request.Timeout = timeoutMs;
                using (var response = (HttpWebResponse)request.GetResponse())
                    return true;
            }
            catch
            {
                return false;
            }
        }

        public static CurrentUser GetCurrentUser(string token)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken tokenS = handler.ReadToken(token) as JwtSecurityToken;
            string userData = tokenS.Claims.First(claim => claim.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata").Value;
            UserInfo user = JsonConvert.DeserializeObject<UserInfo>(userData!);
            CurrentUser currentUser = new CurrentUser()
            {
                LoginID = user!.LoginID,
                UserName = user!.UserName,
                UserID = user!.UserID,
                UserInfoID = user!.UserInfoID,
                UserType = user!.UserType,
                //IPAddress = GlobalFunctions.IpAddress(),
                //UserPC = GlobalFunctions.UserPc(),
                Ltude = "",
            };
            return currentUser;
        }
    }
}
