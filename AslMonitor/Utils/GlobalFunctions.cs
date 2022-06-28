using AslMonitor.DAL;
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
    ///<summary>
    ///A collection of common functions and properties.
    ///</summary
    public class GlobalFunctions
    {

        ///<summary>
        ///Base url of the server app.
        ///</summary>
        public static string BaseUri = "https://localhost:7110/";
        ///<summary>
        ///Screenshots will be saved on this directory.
        ///</summary
        public static string LocalImagePath = @"D:\SS";

        public static int SsIntervalInSeconds = 30;

        public static string DatabaseLocation = "D:\\VS Repos\\AslMonitor\\AslMonitor\\TestDatabase1.db";


        ///<summary>
        ///Checks Internet connection on both client and server by hitting the server api.
        ///</summary>
        public static bool CheckForInternetConnection(int timeoutMs = 2000)
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


        ///<summary>
        ///Extract User data from the jwt token.
        ///</summary>
        public static CurrentUser GetCurrentUser(string token)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken tokenS = handler.ReadToken(token) as JwtSecurityToken;
            string userData = tokenS.Claims.First(claim => claim.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata").Value;
            UserInfo? user = JsonConvert.DeserializeObject<UserInfo>(userData!);
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

        /// <summary>
        /// Gets the token from the database and extract user data from it. Return part of the data as CurrentUser Object
        /// </summary>
        /// <returns>CurrentUser Object</returns>
        internal static CurrentUser? CurrentUserS()
        {
            using DatabaseContext _dbContext = new DatabaseContext();
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            if (!_dbContext.LoginTokens.Any()) return null;
            JwtSecurityToken tokenS = handler.ReadToken(_dbContext.LoginTokens.FirstOrDefault().Token) as JwtSecurityToken;
            string userData = tokenS.Claims.First(claim => claim.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata").Value;
            UserInfo? user = JsonConvert.DeserializeObject<UserInfo>(userData!);
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

        /// <summary>
        /// Returns local Ip Address
        /// </summary>
        /// <returns></returns>
        public static string IpAddress()
        {
            //var feature = HttpHelper.HttpContext.Features.Get<IHttpConnectionFeature>();
            //string LocalIPAddr = feature?.LocalIpAddress?.ToString();
            //return LocalIPAddr;

            //string a =  HttpHelper.HttpContext.Connection.RemoteIpAddress.ToString();
            //return a;

            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            return ipAddress.ToString();
        }

        /// <summary>
        /// Return Local Pc Name
        /// </summary>
        /// <returns></returns>
        public static string UserPc()
        {
            return Dns.GetHostName();
        }


    }


}
