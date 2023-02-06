using System;
using System.Collections.Generic;

class Program
{
    public static void Main(string[] args)
    {
        List<Lot> auction = new List<Lot>();
        Driver driver = new Driver();
        driver.mainMenu(auction);
    }

    public class Lot
    {
        private static int lot = 1001;
        private int lotNumber;
        private string description;
        private int currentBid;
        private int bidIncrement;
        private Boolean sold;

        public Lot()
        {
            lotNumber = lot;
            lotNumber++;
            description = "Unknown Item";
            currentBid = 0;
            bidIncrement = 0;
            sold = false;
        }

        public Lot(string description, int currentBid, int bidIncrement)
        {
            lotNumber = lot;
            lotNumber++;
            this.description = description;
            this.currentBid = currentBid;
            this.bidIncrement = bidIncrement;
            sold = false;
        }

        public void markSold()
        {
            sold = true;
        }

        public Boolean getSold()
        {
            return (sold);
        }

        public int getBidIncrement()
        {
            return (bidIncrement);
        }

        public int getCurrentBid()
        {
            return (currentBid);
        }

        public String getDescription()
        {
            return (description);
        }

        public void setCurrentBid(int Bid)
        {
            currentBid = Bid;
        }

        public void setBidIncrement(int Bid)
        {
            bidIncrement = Bid;
        }

        public int nextBid()
        {
            return (currentBid + bidIncrement);
        }

        public override String ToString()
        {
            if (sold == true)
            {
                return ("Lot " + lotNumber + ", " + description + " was sold for $" + currentBid);
            }
            else
            {
                return ("Lot " + lotNumber + ", " + description + " current bid $" + currentBid + " minimum bid $" + bidIncrement);
            }
        }

    }

    public class Driver
    {
        Lot currentLot = null;
        public Lot getNextLot(List<Lot> a)
        {
            if (a.Count == 0)
                return (new Lot());
            else
            {
                Lot temp = a[0];
                a.RemoveAt(0);
                return (temp);
            }
        }


        public void addItem(List<Lot> a)
        {
            Console.WriteLine("New Description?");
            string b = Console.ReadLine();
            Console.WriteLine("New Starting Bid?");
            int c = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Bid increment?");
            int d = Convert.ToInt32(Console.ReadLine());
            Lot add = new Lot(b, c, d);
            a.Add(add);

        }
        public void bid(Lot lot)
        {
            Console.WriteLine("Your Bid?");
            int userBid = Convert.ToInt32(Console.ReadLine());
            while (userBid < lot.getBidIncrement())
            {
                Console.WriteLine("Bid too low, your Bid?");
                userBid = Convert.ToInt32(Console.ReadLine());
            }
            lot.setCurrentBid(lot.getCurrentBid() + userBid);
        }
        public void markSold(Lot lot)
        {
            lot.markSold();
        }
        public void mainMenu(List<Lot> lots)
        {

            while (true)
            {

                if (currentLot == null || currentLot.getDescription().Equals("Unknown Item") || (currentLot.getSold() == true))
                {
                    Console.WriteLine("We are not currently bidding");
                }
                else
                {
                    Console.WriteLine("Current Lot: " + currentLot.ToString());
                }
                Console.WriteLine();

                Console.WriteLine("1. Add Lot to Auction\n2. Start bidding on next Lot\n3. Bid on current Lot\n4. Mark current Lot sold\n5. Quit");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    default:
                        System.Environment.Exit(0);
                        break;
                    case 1:
                        addItem(lots);
                        break;
                    case 2:
                        if (currentLot == null)
                        {
                            currentLot = getNextLot(lots);
                            break;
                        }
                        if (lots.Count == 0)
                        {
                            Console.WriteLine("There is nothing to bid on, add an item first");
                            currentLot = null;
                            break;
                        }
                        if (currentLot.getSold())
                        {
                            Console.WriteLine("There is nothing to bid on, add an item first");

                            break;
                        }
                        currentLot = getNextLot(lots);
                        break;

                    case 3:
                        if (currentLot == null || currentLot.getDescription().Equals("Unknown Item") || (currentLot.getSold() == true))
                        {
                            Console.WriteLine("You must first bring a Lot up for bidding");
                            break;
                        }
                        bid(currentLot);
                        break;

                    case 4:
                        if (currentLot == null || currentLot.getDescription().Equals("Unknown Item") || (currentLot.getSold() == true))
                        {
                            Console.WriteLine("You must first bring a Lot up for bidding");
                            break;
                        }
                        currentLot.markSold();
                        break;
                }
            }
        }
    }
}