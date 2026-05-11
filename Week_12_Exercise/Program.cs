using System;

namespace Week_12_Exercise
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // TASK 1
            CalculateDamage(new int[] { 20, 40, 60 }, 1, 5);
            CalculateDamage(new int[] { 20, 40, 60 }, 1, 0);
            CalculateDamage(new int[] { 20, 40, 60 }, 5, 5);

            Console.WriteLine();

            // TASK 2
            GameManager gameManager = new GameManager();
            NetworkManager networkManager = new NetworkManager();

            networkManager.ParseServerPacket(new string[] { "A", "B", "C", "100" }, 2, gameManager);
            networkManager.ParseServerPacket(new string[] { "A", "B" }, 2, gameManager);
            networkManager.ParseServerPacket(new string[] { "A", "B", "C", "abc" }, 2, gameManager);
            networkManager.ParseServerPacket(new string[] { "A", "B", "C", "100" }, 0, gameManager);
            networkManager.ParseServerPacket(new string[] { "A", "B", "C", "100" }, 2, null);

            Console.WriteLine();

            // TASK 3
            Inventory playerInventory = new Inventory();

            try
            {
                playerInventory.AddItem("Sword");
                playerInventory.AddItem("Potion");
                playerInventory.AddItem("Shield");
                playerInventory.AddItem("Boots");
                playerInventory.AddItem("Wand");
                playerInventory.AddItem("Ring");
            }
            catch (InventoryFullException ex)
            {
                Console.WriteLine("[UI Message]: " + ex.Message);
            }

            Console.ReadKey();
        }

        static void CalculateDamage(int[] attackDamages, int attackIndex, int playerArmor)
        {
            Console.WriteLine("Starting damage calculation...");

            try
            {
                int incomingDamage = attackDamages[attackIndex];
                int netDamage = incomingDamage / playerArmor;

                Console.WriteLine("Player took " + netDamage + " damage!");
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("CRITICAL HIT: Armor is 0!");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("ERROR: Invalid attack index!");
            }
            finally
            {
                Console.WriteLine("Saving combat logs to file...");
            }
        }
    }

    public class NetworkManager
    {
        public void ParseServerPacket(string[] packetData, int uiScale, GameManager manager)
        {
            Console.WriteLine("Parsing incoming server packet...");

            try
            {
                string scoreText = packetData[3];
                int playerScore = int.Parse(scoreText);
                int scaledScore = playerScore / uiScale;

                manager.UpdateUI(scaledScore);

                Console.WriteLine("Packet parsed successfully!");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("ERROR: Incomplete packet received!");
            }
            catch (FormatException)
            {
                Console.WriteLine("ERROR: Packet data is corrupted!");
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("ERROR: Cannot scale UI by zero!");
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("ERROR: GameManager is missing in the scene!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("CRITICAL: Unknown error occurred " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Packet processing cycle finished.");
            }
        }
    }

    public class GameManager
    {
        public void UpdateUI(int score)
        {
            Console.WriteLine("UI updated with score: " + score);
        }
    }

    public class InventoryFullException : Exception
    {
        public InventoryFullException(string message) : base(message)
        {
        }
    }

    public class Inventory
    {
        private int maxCapacity = 5;
        private int currentItemCount = 0;

        public void AddItem(string itemName)
        {
            if (currentItemCount >= maxCapacity)
            {
                throw new InventoryFullException("Your bag is full! Cannot add item.");
            }

            currentItemCount++;
            Console.WriteLine(itemName + " was added.");
        }
    }
}