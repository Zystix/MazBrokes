using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;


namespace MazBrokes
{
    class Calculations
    {
        public double CalcFairPrice(Events Game)
        {
            double FairPrice = 0;
            int counter = 0;
            double[] fairPriceArr = new double[] { };
            foreach (var wc in Game.Outcomes)
            {
                fairPriceArr[counter] = wc.probDecimal * Convert.ToDouble(wc.mPayout); 
                counter++; 
            }

            for (int i = 0; i < fairPriceArr.Length; i++)
            {
                 FairPrice += fairPriceArr[i];
            }
            //Wassup
            return FairPrice;
        }

    }
}