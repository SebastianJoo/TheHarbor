using System;
using System.Collections.Generic;
using System.Text;

namespace TheHarbor
{
    
    public class Boat
    {
        public string IdNumber { get; set; }
        public string BoatType { get; set; }
        public int BoatTypeMaxSpeed { get; set; }
        public int TopSpeed { get; set; }
        public int Weight { get; set; }
        public int MaxWeight { get; set; }
        public int MinWeight { get; set; }
        public string UniqueProp { get; set; }
        public int DaysInHarbor { get; set; }
        public int DockingSpotsUsed { get; set; }
        public int DockNumber { get; set; }

        public static string GenerateBoatID()
        {
            string[] IdArray = new string[3];
            string idNumber = "";

            Random r = new Random();

            for (int i = 0; i < 3; i++)
            {
                int number = r.Next(0, 26);
                char let = (char)('a' + number);
                string letters = let.ToString();
                IdArray[i] = letters;
            }
            foreach (var letter in IdArray)
            {
                idNumber += letter;
            }
            return idNumber.ToUpper();
        }
        public static int GenerateMaxSpeed(int boatTypeMaximumSpeed)
        {
            Random r = new Random();
            int topSpeed = r.Next(0, boatTypeMaximumSpeed + 1);
            return topSpeed;
        }
        public static int GenerateWeight(int minWeight, int maxWeight)
        {
            Random r = new Random();
            int weight = r.Next(minWeight, maxWeight + 1);
            return weight;
        }
        public virtual string AddUniqueProperty()
        {
            string s = "";
            return s;
        }
        public virtual string PrintBoatProperties()
        {
            return "";
        }
        public static void GenerateBoats(int dailyBoatsAmount, List<Boat> boats)
        {
            Random r = new Random();
            for (int i = 0; i < dailyBoatsAmount; i++)
            {
                int randomNumber = r.Next(1, 4 + 1);

                switch (randomNumber)
                {
                    case 1:
                        boats.Add(new RowBoat());
                        break;
                    case 2:
                        boats.Add(new MotorBoat());
                        break;
                    case 3:
                        boats.Add(new SailBoat());
                        break;
                    case 4:
                        boats.Add(new CargoShip());
                        break;
                }
            }
        }
    }
}
