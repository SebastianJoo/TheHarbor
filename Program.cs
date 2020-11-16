using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Xml.Serialization;

namespace TheHarbor
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.SetWindowSize(200, 50);
            int dailyBoatsAmount = 5;

            string harborData = "Harbor.xml";

            Harbor.Create();
            List<Boat> incomingBoats = new List<Boat>();
            bool simulationOn = true;
            while (simulationOn)
            {
                Harbor.harbor = DeserializeXML(Harbor.harbor, harborData);
                Boat.GenerateBoats(dailyBoatsAmount, incomingBoats);
                Harbor.DockBoats(incomingBoats);
                Harbor.PrintBoats();
                SerializeXML(Harbor.harbor, harborData);
                Thread.Sleep(5000);
            }
        }
        private static void SerializeXML(Harbor[] harbors, string filePath)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Harbor[]), new Type[] { typeof(MotorBoat), typeof(RowBoat), typeof(SailBoat), typeof(CargoShip) });
            FileStream writer = new FileStream(filePath, FileMode.Create);
            ser.Serialize(writer, harbors);
            writer.Close();
        }
        private static Harbor[] DeserializeXML(Harbor[] harborinfo, string filePath)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Harbor[]), new Type[] { typeof(MotorBoat), typeof(RowBoat), typeof(SailBoat), typeof(CargoShip) });
            if (File.Exists(filePath))
            {
                TextReader textReader = new StreamReader(filePath);
                harborinfo = (Harbor[])xmlSerializer.Deserialize(textReader);
                textReader.Close();

            }
            return harborinfo;
        }
    }
}



