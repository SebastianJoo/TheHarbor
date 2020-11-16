using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace TheHarbor
{
    [XmlInclude(typeof(Boat))]
    public class MotorBoat : Boat
    {
        public MotorBoat()
        {
            BoatType = "Motorbåt";
            MinWeight = 200;
            MaxWeight = 3000;
            BoatTypeMaxSpeed = 60;
            DaysInHarbor = 3;
            DockingSpotsUsed = 1;

            IdNumber = "M+" + GenerateBoatID();
            TopSpeed = GenerateMaxSpeed(BoatTypeMaxSpeed);
            UniqueProp = AddUniqueProperty();
            Weight = GenerateWeight(MinWeight, MaxWeight);
        }
        public override string AddUniqueProperty()
        {
            Random r = new Random();

            int horsePower = r.Next(10, 1000 + 1);

            return $"Antal hästkrafter: {horsePower} hk.";
        }
        public override string PrintBoatProperties()
        {
            return $"{DockNumber}-{DockNumber + 1}\t{BoatType}\t{IdNumber}\t{Weight}kg \t{Convert.KnotToKph(TopSpeed)}Km/h  \t{UniqueProp}";
        }
    }
} 
