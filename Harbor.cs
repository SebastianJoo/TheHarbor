using System;
using System.Collections.Generic;
using System.Linq;

namespace TheHarbor
{
    public class Harbor
    {
        public static int rejectedBoats { get; set; }
        public static int daysPassed { get; set; }
        public static Harbor[] harbor = new Harbor[64];
        public Boat[] SpotToDock { get; set; } = new Boat[2];
        public int DockNumber { get; set; }
        public bool DockingSpot = true;
        public Harbor()
        {
            daysPassed = daysPassed;
        }
        public static void Create()
        {
            for (int i = 0; i < harbor.Length; i++) 
            {
                if (harbor[i] is null)
                {
                    harbor[i] = new Harbor();
                    harbor[i].DockNumber = i + 1;
                    harbor[i].DockingSpot = true;
                }
            }
        }
        public static void DockBoats(List<Boat> boats)
        {
            BoatsLeaving();
            foreach (var boat in boats)
            {
                IncomingBoats(boat);
            }
            boats.Clear();
            PrintBoats();
            daysPassed++;
        }
        private static void BoatsLeaving()
        {
            for (int i = 0;  i < harbor.Length; i++)
            {
                if (harbor[i].SpotToDock[0] != null)
                {
                    harbor[i].SpotToDock[0].DaysInHarbor--;
                    if (harbor[i].SpotToDock[0].DaysInHarbor == 0)
                    {
                        RemoveFromDock(harbor[i].SpotToDock[0], i);
                    }
                }
                if (harbor[i].SpotToDock[1] != null)
                {
                    harbor[i].SpotToDock[1].DaysInHarbor--;
                    if (harbor[i].SpotToDock[1].DaysInHarbor == 0)
                    {
                        harbor[i].SpotToDock[1] = null;
                        harbor[i].DockingSpot = true;
                    }
                }
            }
        }
        public static void IncomingBoats(Boat boat)
        {
            bool rejected = true;
            if (boat is CargoShip)
            {
                for (int i = harbor.Length - 1; i >= 4; i--)
                {
                    if (harbor[i].DockingSpot == true && harbor[i].SpotToDock[0] is null && harbor[i - 1].SpotToDock[0] is null && harbor[i - 2].SpotToDock[0] is null && harbor[i - 3].SpotToDock[0] is null)
                    {
                        harbor[i].SpotToDock[0] = boat;
                        boat.DockNumber = harbor[i].DockNumber;
                        PlaceInDock(boat, i);
                        rejected = false;
                        break;
                    }
                }
            }
            else
                for (int i = 0; i < harbor.Length; i++)
                {
                    if (boat is RowBoat)
                    {
                        if (harbor[i].DockingSpot == true && harbor[i].SpotToDock[0] is null)
                        {
                            harbor[i].SpotToDock[0] = boat;
                            boat.DockNumber = harbor[i].DockNumber;
                            PlaceInDock(boat, i);
                            rejected = false;
                            break;
                        }
                        if (harbor[i].SpotToDock[0] is RowBoat && harbor[i].SpotToDock[1] is null)
                        {
                            harbor[i].SpotToDock[1] = boat;
                            boat.DockNumber = harbor[i].DockNumber;
                            rejected = false;
                            break;
                        }
                    }
                    if (boat is MotorBoat)
                    {
                        if (harbor[i].DockingSpot == true && harbor[i].SpotToDock[0] is null)
                        {
                            harbor[i].SpotToDock[0] = boat;
                            boat.DockNumber = harbor[i].DockNumber;
                            PlaceInDock(boat, i);
                            rejected = false;
                            break;
                        }
                    }
                    if (boat is SailBoat)
                    {
                        if (i < harbor.Length - 1 && harbor[i].DockingSpot == true && harbor[i].SpotToDock[0] is null && harbor[i + 1].SpotToDock[0] is null)
                        {
                            harbor[i].SpotToDock[0] = boat;
                            boat.DockNumber = harbor[i].DockNumber;
                            PlaceInDock(boat, i);
                            rejected = false;
                            break;
                        }
                    }
                }
            if (rejected)
            {
                rejectedBoats++;
            }
        }
        public static void PrintBoats()
        {
            Console.Clear();
            Console.WriteLine($"Plats\tBåttyp\t\tNr\tVikt \tMaxhastighet  \tÖvrigt");
            Console.WriteLine();
            List<Boat> boatList = new List<Boat>();
            PrintBoatProperties(boatList);
            var q = boatList
                .Where(b => b != null)
                .Select(b => b.Weight)
                .Sum();

            var q2 = boatList
                .Where(b => b != null)
                .Select(b => b.TopSpeed)
                .Average();

            var q3 = boatList
                .Where(b => b != null)
                .GroupBy(b => b.BoatType);

            var q4 = harbor
                .Where(b => b.DockingSpot == true);
            Console.WriteLine();
            Console.WriteLine("Båtar i hamnen: ");
            PrintBoatAmount(q3);
            Console.WriteLine($"Passerade dagar:{daysPassed} Total vikt: {q} kg Medelhastighet:{Convert.KnotToKph(q2)} km/h  Avvisade båtar:{rejectedBoats} Lediga platser:{q4.Count()}");
        }

        private static void PrintBoatAmount(IEnumerable<IGrouping<string, Boat>> q3)
        {
            foreach (var boat in q3)
            {
                Console.WriteLine($"{boat.Key}: {boat.Count()} ");
            }
        }

        public static void PrintBoatProperties(List<Boat> boats)
        {
            for (int i = 0; i < harbor.Length; i++)
            {
                if (harbor[i].DockingSpot is true)
                    Console.WriteLine($"{harbor[i].DockNumber} Tom båtplats");
                if (harbor[i].DockingSpot is false)
                {
                    if (harbor[i].SpotToDock[0] != null)
                    {
                        boats.Add(harbor[i].SpotToDock[0]);
                        Console.WriteLine($"{harbor[i].SpotToDock[0].PrintBoatProperties()}");
                    }
                    if (harbor[i].SpotToDock[1] != null)
                    {
                        boats.Add(harbor[i].SpotToDock[1]);
                        Console.WriteLine($"{harbor[i].SpotToDock[1].PrintBoatProperties()}");
                    }
                }
            }
        }
        public static void RemoveFromDock(Boat boat, int dockNumber)
        {
            harbor[dockNumber].SpotToDock[0] = null;
            for (int i = 1; i <= boat.DockingSpotsUsed; i++)
            {
                if (boat is CargoShip)
                {
                    harbor[dockNumber].DockingSpot = true;
                    dockNumber--;
                    continue;
                }
                else
                    harbor[dockNumber].DockingSpot = true;
                dockNumber++;
            }
        }
        public static void PlaceInDock(Boat boat, int dockNumber)
        {
            for (int i = 1; i <= boat.DockingSpotsUsed; i++)
            {
                if (boat is CargoShip)
                {
                    harbor[dockNumber].DockingSpot = false;
                    dockNumber--;
                    continue;
                }
                else
                    harbor[dockNumber].DockingSpot = false;
                dockNumber++;
            }
        }
    }
}

