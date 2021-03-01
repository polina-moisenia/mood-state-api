using System;
using System.Collections.Generic;
using MoodStateApi.Models;

namespace MoodStateApi.Services {

    public class Service : IService {
        private static readonly object Lock = new object();
        private readonly Dictionary<States, List<string>> colorsToMood = new Dictionary<States, List<string>> { { States.Neutral, new List<string> { "#707B7C", "#95A5A6", "#85929E" } },
            { States.Positive, new List<string> { "#FDEBD0", "#FAE5D3", "#D5F5E3" } },
            { States.Negative, new List<string> { "#2E4053", "#800000", "#512E5F" } }
        };
        private StateModel model;
        private readonly Random rand = new Random();

        public Service() {
            model = GetNeutral();
        }

        public StateModel Get() {
            if (DateTime.UtcNow.AddMinutes(-2) > model.LastUpdated) {
                model = GetNeutral();
            }

            return model;
        }

        public StateModel Update(States state) {
            lock (Lock) {
                model.State = state;
                model.LastUpdated = DateTime.UtcNow;
                model.Color = colorsToMood[state][rand.Next(0, colorsToMood[state].Count)];
            }

            return model;
        }

        private StateModel GetNeutral() {
            return new StateModel {
                State = States.Neutral,
                LastUpdated = DateTime.UtcNow,
                Color = colorsToMood[States.Neutral][rand.Next(0, colorsToMood[States.Neutral].Count)]
            };
        }
    }
}