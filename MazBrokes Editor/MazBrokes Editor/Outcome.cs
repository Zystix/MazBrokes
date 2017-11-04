using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazBrokes_Editor
{
    [Serializable()]
    class Outcome
    {
        public string Name;
        public string[] oddsArray;
        public string oddsFor;
        public string Prize;
        public string oddsAgainst;
        public double probDecimal;
        public double mPayout;
        public Outcome() { }

        public Outcome(string name, string probability, string prize, string payout)
        {
            oddsArray = probability.Split('/');
            oddsFor = Convert.ToInt16(oddsArray[1]) - Convert.ToInt16(oddsArray[0]) + ":" + Convert.ToInt16(oddsArray[0]);
            oddsAgainst = Convert.ToInt16(oddsArray[0]) + ":" + (Convert.ToInt16(oddsArray[1]) - Convert.ToInt16(oddsArray[0]));
            probDecimal = Convert.ToDouble(oddsArray[0]) / Convert.ToDouble(oddsArray[1]);
            mPayout = Convert.ToDouble(payout);
            Prize = prize;
            Name = name;
        }

        
    }
}
