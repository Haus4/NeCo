using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Neco.Client.Core
{
    public class GeoLocator : StateHandler
    {
        private IGeolocator locator;

        private Position position;

        public GeoLocator()
        {
            locator = CrossGeolocator.Current;
            locator.PositionChanged += PositionChanged;

            Listen();
        }

        public Position Position
        {
            get
            {
                return position;
            }
        }

        public async void Listen()
        {
            position = null;

            CurrentState = State.Unknown;
            if(locator.IsListening) await locator.StopListeningAsync();

            if (!locator.IsGeolocationEnabled || 
                !locator.IsGeolocationAvailable ||
                !await locator.StartListeningAsync(TimeSpan.FromSeconds(10), 100))
            {
                CurrentState = State.Error;
            }
        }

        private void PositionChanged(object obj, PositionEventArgs e)
        {
            position = e.Position;
            CurrentState = State.Success;
        }
    }
}
