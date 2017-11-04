using Newtonsoft.Json;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Android.App;

namespace MazBrokes
{
    class configParser
    {
        public configParser()
        { }

        public static void SerializeParse()
        {
            Stream stream = MainActivity.assets.Open("MazBrokesData.json");
            var serializer = new JsonSerializer();

            using (var sr = new StreamReader(stream))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                var EventList = serializer.Deserialize<List<Events>>(reader);
                foreach (var item in EventList)
                {
                    SlidingTabFragment.UpcomingEvents.mEvents.Add(item);
                }

            }         

        }


    }

}