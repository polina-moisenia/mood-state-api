using System;

namespace MoodStateApi.Models {
    public class StateModel {
        public States State { get; set; }
        public DateTime LastUpdated { get; set; }
        public string Color { get; set; }
    }

    public enum States {
        Neutral,
        Positive,
        Negative
    }
}