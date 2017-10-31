using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using log4net;

namespace LoggingKata
{
    /// <summary>
    /// Parses a POI file to locate all the TacoBells
    /// </summary>
    public class TacoParser
    {
        public TacoParser()
        {

        }

        private static readonly ILog Logger =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ITrackable Parse(string line)
        {
            var cells = line.Split(',');

            if (cells.Length < 3)
            {
                Logger.Error("Must have at least three elements to parse into ITrackable");
                return null;
            }

            double lon;
            double lat;

            try
            {
                Logger.Debug("Attempting to Parse Longitude");
                lon = double.Parse(cells[0]);

                Logger.Debug("Attempting to Parse Latitude");
                lat = double.Parse(cells[1]);
            }
            catch (Exception e)
            {
                Logger.Error("Failed to parse the location", e);
                Console.WriteLine(e);
                return null;
            }


            return new TacoBell
            {
                Name = cells[2],
                Location = new Point(lat, lon)
            };
        }
    }
}