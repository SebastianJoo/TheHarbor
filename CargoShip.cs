using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace TheHarbor
{
    [XmlInclude(typeof(Boat))]
    public class CargoShip : Boat
    {
        public CargoShip()
        {
            BoatType = "Fraktfartyg";
            MinWeight = 3000;
            MaxWeight = 20000;
            BoatTypeMaxSpeed = 20;
            DaysInHarbor = 6;
            DockingSpotsUsed = 4;


            IdNumber = "L+" + GenerateBoatID();
            TopSpeed = GenerateMaxSpeed(BoatTypeMaxSpeed);
            UniqueProp = AddUniqueProperty();
            Weight = GenerateWeight(MinWeight, MaxWeight);
        }
        public override string AddUniqueProperty()
        {
            Random r = new Random();

            int containerAmount = r.Next(0, 500 + 1);

            return $"Containers: {containerAmount}.";
        }
        public override string PrintBoatProperties()
        {
            return $"{DockNumber}-{DockNumber + 1}\t{BoatType}\t{IdNumber}\t{Weight}kg \t{Convert.KnotToKph(TopSpeed)}Km/h    \t{UniqueProp}";
        }
    }
}
