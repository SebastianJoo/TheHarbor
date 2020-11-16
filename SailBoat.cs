using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace TheHarbor
{
    [XmlInclude(typeof(Boat))]
    public class SailBoat : Boat
    {
        public SailBoat()
        {
            BoatType = "Segelbåt";
            MinWeight = 800;
            MaxWeight = 6000;
            BoatTypeMaxSpeed = 12;
            DaysInHarbor = 4;
            DockingSpotsUsed = 2;

            IdNumber = "S+" + GenerateBoatID();
            TopSpeed = GenerateMaxSpeed(BoatTypeMaxSpeed);
            UniqueProp = AddUniqueProperty();
            Weight = GenerateWeight(MinWeight, MaxWeight);
        }
        public override string AddUniqueProperty()
        {
            Random r = new Random();

            int boatLength = r.Next(0, 60 + 1);

            return $"Båtlängd: {boatLength} fot.";
        }
        public override string PrintBoatProperties()
        {
            return $"{DockNumber}-{DockNumber + 1}\t{BoatType}\t{IdNumber}\t{Weight}kg \t{Convert.KnotToKph(TopSpeed)}Km/h    \t{UniqueProp}";
        }
    }
}
