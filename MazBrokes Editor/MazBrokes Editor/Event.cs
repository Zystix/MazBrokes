using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazBrokes_Editor
{
    [Serializable()]
    class Event
    {
        public List<Outcome> Outcomes;
        public string eventName;
        public string eventType;
        public string Description;

        public Event()
        {
        }

        public Event(string eventname, string eventtype, string description, List<Outcome> outcomes)
        {
            eventName = eventname;
            eventType = eventtype;
            Description = description;
            Outcomes = outcomes;
        }

        
    }
}
