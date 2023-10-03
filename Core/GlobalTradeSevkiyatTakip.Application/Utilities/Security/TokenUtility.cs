using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.Utilities.Security
{
    public static class TokenUtility
    {
        public static int getUserId(string token)
        {
            if (token.Contains("Token") && token.StartsWith("Token"))
            {
                token = token.Substring("Token".Length).Trim();

                string seriUserData = EnDeCode.Decrypt(token, SecurityParameters.SifrelemeParametresi);

                var data = JsonConvert.DeserializeObject<UserData>(seriUserData);

                return Convert.ToInt32(data[SecurityParameters.UserId]);
            }
            else
            {
                throw new Exception("Token bulunamadı..");
            }
        }
        public static string getUserType(string token)
        {
            if (token.Contains("Token") && token.StartsWith("Token"))
            {
                token = token.Substring("Token".Length).Trim();

                string seriUserData = EnDeCode.Decrypt(token, SecurityParameters.SifrelemeParametresi);

                var data = JsonConvert.DeserializeObject<UserData>(seriUserData);

                return data[SecurityParameters.UserType];
            }
            else
            {
                throw new Exception("Token bulunamadı..");
            }
        }

        public static string getUserFullName(string token)
        {
            if (token.Contains("Token") && token.StartsWith("Token"))
            {
                token = token.Substring("Token".Length).Trim();

                string seriUserData = EnDeCode.Decrypt(token, SecurityParameters.SifrelemeParametresi);

                var data = JsonConvert.DeserializeObject<UserData>(seriUserData);

                return data[SecurityParameters.UserFullName];
            }
            else
            {
                throw new Exception("Token bulunamadı..");
            }
        }
        public static string getUser(string token)
        {
            if (token.Contains("Token") && token.StartsWith("Token"))
            {
                token = token.Substring("Token".Length).Trim();

                string seriUserData = EnDeCode.Decrypt(token, SecurityParameters.SifrelemeParametresi);

                var data = JsonConvert.DeserializeObject<UserData>(seriUserData);

                return data[SecurityParameters.User];
            }
            else
            {
                throw new Exception("Token bulunamadı..");
            }
        }
        public static string getTokenExpirationDate(string token)
        {
            if (token.Contains("Token") && token.StartsWith("Token"))
            {
                token = token.Substring("Token".Length).Trim();

                string seriUserData = EnDeCode.Decrypt(token, SecurityParameters.SifrelemeParametresi);

                var data = JsonConvert.DeserializeObject<UserData>(seriUserData);

                return data[SecurityParameters.Expires];
            }
            else
            {
                throw new Exception("Token bulunamadı..");
            }
        }
    }
}
