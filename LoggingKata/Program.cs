using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.IO;
using Geolocation;

namespace LoggingKata
{
    class Program
    {
        //Why do you think we use ILog?
        private static readonly ILog Logger =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {

            var path = Environment.CurrentDirectory + "\\Taco_Bell-US-AL-Alabama.csv";
            if (path.Length == 0)
            {
                Console.WriteLine("You must provide a filename as an argument");
                Logger.Fatal("Cannot import without filename specified in our path variable");
                return;
            }

            Logger.Info("Log initialized");
            Logger.Debug("Created path variable" + path);

            var lines = File.ReadAllLines(path);

            switch (lines.Length)
            {
                case 0:
                    Logger.Error("No locations to check. Must have at least one location.");
                    break;
                case 1:
                    Logger.Warn("Only one location provided. Must have two to perform a check.");
                    break;
            }

            Console.ReadLine();

            var parser = new TacoParser();
            Logger.Debug("Initialized our Parser");

            var locations = lines.Select(line => parser.Parse(line))
                .OrderBy(loc => loc.Location.Longitude)
                .ThenBy(loc => loc.Location.Latitude)
                .ToArray();

            ITrackable a = null;
            ITrackable b = null;

            Logger.Info("Comparing all locations");

            double distance = 0;

            foreach (var locA in locations)
            {
                var origin = new Coordinate
                {
                    Latitude = locA.Location.Latitude,
                    Longitude = locA.Location.Longitude
                };

                foreach (var locB in locations)
                {
                    var destination = new Coordinate
                    {
                        Latitude = locB.Location.Latitude,
                        Longitude = locB.Location.Longitude
                    };

                    var ndist = GeoCalculator.GetDistance(origin, destination);

                    if (!(ndist > distance)) continue;
                    a = locA;
                    b = locB;
                    distance = ndist;
                }
            }
            Console.WriteLine("Finished foreach loops");

            if (a == null || b == null)
            {
                Logger.Error("Failed to find furthest locations");
                Console.WriteLine("Couldn't find the furthest location apart.");
                Console.ReadLine();
            }

            Console.WriteLine($"The Two Taco Bells that are furthest apart are: {a.Name} and: {b.Name}.");
            Console.WriteLine($"These two locations are {distance} miles away");
            Console.ReadLine();
        }
    }
}