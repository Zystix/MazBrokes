using System.Drawing;
using System.Collections.Generic;
using System;
using Android.Media;
using Android.Graphics;

namespace MazBrokes
{
    [Serializable()]
    public class Outcome
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

    [Serializable()]
    public class Events
    {
        public List<Outcome> Outcomes;
        public string eventName;
        public string eventType;
        public string Description;

        public Events()
        {
        }

        public Events(string eventname, string eventtype, string description, List<Outcome> outcomes)
        { 
            eventName = eventname;
            eventType = eventtype;
            Description = description;
            Outcomes = outcomes;
        }

    }

   

}