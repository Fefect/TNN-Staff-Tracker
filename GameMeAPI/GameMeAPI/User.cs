using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMeAPI
{
    class User
    {
        public String name { get; private set; }
        public double activity { get; private set; }
        public String time { get; private set; }
        public String country { get; private set; }

        public User(string name, double activity, int time, string country)
        {
            this.name = name;
            this.activity = activity;
            this.country = country;
            var ts = TimeSpan.FromSeconds(time);
            if (ts.Days > 0)
            {
                this.time = ts.ToString(@"d\d\,\ hh\:mm\:ss");
            }
        }
        public override string ToString()
        {
            return name;
        }
    }
}
