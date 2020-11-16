using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace TheHarbor
{
    [XmlInclude(typeof(Boat))]
    public class RowBoat : Boat
    {
        public RowBoat()
        {
            BoatType = "Roddbåt";
            MinWeight = 100;
            MaxWeight = 300;
            BoatTypeMaxSpeed = 3;
            DaysInHarbor = 1;
            DockingSpotsUsed = 1;
            
            IdNumber = "R+" + GenerateBoatID();
            TopSpeed = GenerateMaxSpeed(BoatTypeMaxSpeed);
            UniqueProp = AddUniqueProperty();
            Weight = GenerateWeight(MinWeight,MaxWeight) ;
        }
        public override string AddUniqueProperty()
        {
            Random r = new Random();

            int passengerAmount = r.Next(0, 6+1);

            return $"Max antal pers: {passengerAmount}.";
        }
        public override string PrintBoatProperties()
        {
            return $"{DockNumber}-{DockNumber + 1}\t{BoatType}\t\t{IdNumber}\t{Weight}kg \t{Convert.KnotToKph(TopSpeed)}Km/h  \t\t{UniqueProp}";
        }
    }
}
