using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Application.Utilities.Security
{
    public class UserData
    {
        public List<KeyValuePair<string, string>> Items { get; private set; }
        public UserData()
        {
            Items = new List<KeyValuePair<string, string>>();
        }
        public void Add(string key, string data)
        {
            Items.Add(new KeyValuePair<string, string>(key, data));
        }

        public string GetData(string key)
        {
            return Items.FirstOrDefault(x => x.Key == key).Value;
        }

        public string this[string key]
        {
            get { return GetData(key); }
        }
    }
}
