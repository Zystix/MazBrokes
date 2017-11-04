using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Input;
using Newtonsoft.Json;

namespace MazBrokes_Editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Event> mEvents;
        private List<string> mEventNames = new List<string>();

        private Dictionary<Event, Outcome> mOutcomesToRemove = null;

        private List<string> mOutcomeNames;
        private static string file = @"C:\Users\christian\Documents\Visual Studio 2017\Projects\MazBrokes\MazBrokes\Assets\MazBrokesData.json";
        private Event SelectedEvent = null;
        private Outcome SelectedOutcome = null;

        public MainWindow()
        {
            InitializeComponent();
            mEvents = new List<Event>();
           
        }


        private void btnEventAdd_Click(object sender, RoutedEventArgs e)
        {
            mEvents.Add(new Event(
                    txtEventName.Text,
                    txtEventType.Text,
                    txtEventDesc.Text,
                    new List<Outcome>()
                ));

        }

        private void btnAddOutcome_Click(object sender, RoutedEventArgs e)
        {
            if (txtEventOrigin.Text == "")
            {

              SelectedEvent.Outcomes.Add(new Outcome(txtName.Text, txtProbability.Text, txtPrizeDesc.Text, txtBetMultiplier.Text));
                
            }

            foreach (var item in mEvents)
            {
                
                if (item.eventName != txtEventOrigin.Text)
                {
                    continue;
                }
                
                else
                {
                    item.Outcomes.Add(new Outcome(txtName.Text, txtProbability.Text, txtPrizeDesc.Text, txtBetMultiplier.Text));
                }
            }
            Clear();
        }

        private void btnSerialize_Click(object sender, RoutedEventArgs e)
        {
            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter(file))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, mEvents);
                
            }

        }
            private void Deserialize(string jsonfile)
            {
            using (StreamReader stream = File.OpenText(jsonfile))
            {
                JsonSerializer serializer = new JsonSerializer();
                mEvents = (List<Event>)serializer.Deserialize(stream, typeof(List<Event>));
            }

            foreach (var item in mEvents)
            {
                mEventNames.Add(item.eventName);
            }



            lstDesirializedObjects.ItemsSource = mEventNames;
        }

            private void btnDeserialize_Click(object sender, RoutedEventArgs e)
            {
            using (StreamReader stream = File.OpenText(file))
            {
                JsonSerializer serializer = new JsonSerializer();
                mEvents = (List<Event>)serializer.Deserialize(stream, typeof(List<Event>));
            }

                foreach (var item in mEvents)
                {
                    mEventNames.Add(item.eventName);
                }

                

                lstDesirializedObjects.ItemsSource = mEventNames;
               
            }

            private void lstDesirializedObjects_MouseDoubleClick(object sender, MouseButtonEventArgs e)
            {
                    mOutcomeNames = new List<string>();
                
                   foreach (var item in mEvents)
                   {
                        if (lstDesirializedObjects.SelectedItem.ToString() != item.eventName)
                        {
                            continue;
                        }
                        else
                        {
                            txtEventName.Text = item.eventName;
                            txtEventDesc.Text = item.Description;
                            txtEventType.Text = item.eventType;
                            SelectedEvent = item;
                                foreach(var outcome in SelectedEvent.Outcomes)
                                {
                                    mOutcomeNames.Add(outcome.Name);
                                   
                                }
                                lstDesirializedOutcomes.ItemsSource = mOutcomeNames;

                        }
                   }
                
            }

        private void BtnEventEdit_Click(object sender, RoutedEventArgs e)
        {
            SelectedEvent.eventName = txtEventName.Text;
            SelectedEvent.Description = txtEventDesc.Text;
            SelectedEvent.eventType = txtEventType.Text;


        }

        private void lstDesirializedOutcomes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            foreach (var item in SelectedEvent.Outcomes)
            {
                if (lstDesirializedOutcomes.SelectedItem.ToString() != item.Name)
                {
                    continue;
                }
                else
                {

                    txtName.Text = item.Name;
                    txtProbability.Text = item.oddsArray[0] + "/" + item.oddsArray[1];
                    txtPrizeDesc.Text = item.Prize;
                    txtBetMultiplier.Text = Convert.ToString(item.mPayout);
                    SelectedOutcome = item;

                }
            }
        }

        private void btnEditOutcome_Click(object sender, RoutedEventArgs e)
        {
            SelectedOutcome.Name = txtName.Text;
            SelectedOutcome.oddsArray = txtProbability.Text.Split('/');
            SelectedOutcome.Prize = txtPrizeDesc.Text;
            SelectedOutcome.mPayout = Convert.ToDouble(txtBetMultiplier.Text);

            Clear();

        }

        private void btnDeleteOutcome_Click(object sender, RoutedEventArgs e)
        {
            mOutcomesToRemove = new Dictionary<Event, Outcome>();
            foreach (var Event in mEvents)
            {
                foreach (var outcome in Event.Outcomes)
                {
                    if (SelectedOutcome != outcome)
                    {
                        continue;
                    }
                    else
                    {
                        //Event.Outcomes.Remove(outcome);
                        mOutcomesToRemove.Add(Event, outcome);
                    }
                }
            }
            foreach (var item in mOutcomesToRemove)
            {
                item.Key.Outcomes.Remove(item.Value);
            }
        }

        private void DeleteOutcome(Event evvent, Outcome outcome)
        {
            evvent.Outcomes.Remove(outcome);
        }

        private void btnDeleteevent_Click(object sender, RoutedEventArgs e)
        {
            mEvents.Remove(SelectedEvent);
        }

        public void Clear()
        {
            txtEventOrigin.Text = "";
            txtName.Text = "";
            txtProbability.Text = "";
            txtPrizeDesc.Text = "";
            txtBetMultiplier.Text = "";
        }

    }
}
