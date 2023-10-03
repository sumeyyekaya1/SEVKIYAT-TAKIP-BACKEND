using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.Utilities.Security
{
    public struct SecurityParameters
    {
        public const string SifrelemeParametresi = "asdasdasdasdasdasdasdasdasdasdas";//32 karakter olmak zorunda

        public const string UserId = "uid";
        public const string UserFullName = "uname";
        public const string EPosta = "eposta";
        public const string LoginDate = "logindate";
        public const string Expires = "expires";
        public const string Rol = "rol";
        public const string UserType = "utype";

        public const string PersonelType = "personelType";
        public const string EntegreType = "entegreType";

        public const string User = UserFullName + EPosta + Rol + UserId;
    }
}
