using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.IO;

namespace LoggingKata
{
    class Program
    {
        //Why do you think we use ILog?
        private static readonly ILog Logger =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("You must provide a filename as an argument");
                Logger.Fatal("Cannot import without filename specified as an argument");
                return;
            }

            Logger.Info("Log initialized");
            var csvPath = (Environment.CurrentDirectory + "\\Taco_Bell-US-AL-Alabama.csv");

             Logger.Debug("Created csvPath variable" + csvPath);

            var rows = File.ReadAllLines(csvPath);

            foreach (var line in rows)
            {
                Console.WriteLine("Line in file" + line);
            }

            if (rows.Length == 0)
            {
                Logger.Error("Our csv file is missing or empty of content");
            }
            else if (rows.Length == 1)
            {
                Logger.Warn("Can't compare there is only one element");
            }

            Console.ReadLine();

            var parser = new TacoParser();
            var locations = rows.Select(row => parser.Parse(row));


            for 
            //TODO:  Find the two TacoBells in Alabama that are the furthurest from one another.
            //HINT:  You'll need two nested forloops

        }
    }
}