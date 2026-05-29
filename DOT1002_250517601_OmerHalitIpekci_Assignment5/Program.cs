using System;
using System.Collections.Generic;

namespace DOT1002_250517601_OmerHalitIpekci_Assignment5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameManager game = new GameManager();

            game.StartGame();
            game.Player.ShowStatus();

            Console.WriteLine();
            Console.WriteLine("Assignment 5 draft code.");
            Console.WriteLine("Press any key to close...");
            Console.ReadKey();
        }
    }

    // Interfaces

    internal interface IExplorable
    {
        void Enter(Player player);
        void Explore(Player player);
    }

    internal interface IInteractable
    {
        void Interact(Player player);
    }

    internal interface ICollectible
    {
        void Collect(Player player);
    }

    internal interface ICheckable
    {
        bool PerformCheck(Player player);
    }

    // Main game classes

    internal class GameManager
    {
        private bool isRunning;
        private Location currentLocation;
        private Player player;
        private DiceRoller diceRoller;
        private EndingManager endingManager;
        private List<Location> locations;

        public bool IsRunning
        {
            get { return isRunning; }
            set { isRunning = value; }
        }

        public Location CurrentLocation
        {
            get { return currentLocation; }
            set { currentLocation = value; }
        }

        public Player Player
        {
            get { return player; }
            set { player = value; }
        }

        public DiceRoller DiceRoller
        {
            get { return diceRoller; }
            set { diceRoller = value; }
        }

        public EndingManager EndingManager
        {
            get { return endingManager; }
            set { endingManager = value; }
        }

        public List<Location> Locations
        {
            get { return locations; }
            set { locations = value; }
        }

        public GameManager()
        {
            isRunning = false;
            currentLocation = null;

            player = new Player();
            diceRoller = new DiceRoller();
            endingManager = new EndingManager();
            locations = new List<Location>();
        }

        public void StartGame()
        {
            isRunning = true;
            ShowOpening();

        }

        public void ChangeLocation(Location newLocation)
        {
            currentLocation = newLocation;
        }

        public void EndGame()
        {
            isRunning = false;
        }

        public void ShowOpening()
        {
            Console.WriteLine("=== TALE OF LOVER ===");
            Console.WriteLine("Orpheus enters the Underworld to save Eurydice.");
            Console.WriteLine("This is the draft OOP version of the project.");
        }
    }

    internal class DiceRoller
    {
        private Random random;

        public Random Random
        {
            get { return random; }
            set { random = value; }
        }

        public DiceRoller()
        {
            random = new Random();
        }

        public int RollD20()
        {
            return random.Next(1, 21);
        }

        public int GetModifier(int statValue)
        {
            return (statValue - 10) / 2;
        }
    }

    internal class EndingManager
    {
        public void GoodEnding()
        {
            Console.WriteLine("Good Ending");
        }

        public void BadEndingLookBack()
        {
            Console.WriteLine("Bad Ending");
        }

        public void DeathEnding()
        {
            Console.WriteLine("Death Ending");
        }

        public bool CheckFinalInput(string input)
        {
            if (input == null)
            {
                return false;
            }

            input = input.ToLower();

            if (input.Contains("look") || input.Contains("back"))
            {
                return false;
            }

            return true;
        }
    }

    internal class SkillCheck
    {
        private string checkName;
        private string statType;
        private int difficultyClass;
        private bool isHidden;

        public string CheckName
        {
            get { return checkName; }
            set { checkName = value; }
        }

        public string StatType
        {
            get { return statType; }
            set { statType = value; }
        }

        public int DifficultyClass
        {
            get { return difficultyClass; }
            set { difficultyClass = value; }
        }

        public bool IsHidden
        {
            get { return isHidden; }
            set { isHidden = value; }
        }

        public SkillCheck()
        {
            checkName = "Check";
            statType = "Strength";
            difficultyClass = 10;
            isHidden = false;
        }

        public bool Roll(Player player, DiceRoller diceRoller)
        {
            int statValue = player.GetStat(statType);
            int roll = diceRoller.RollD20();
            int modifier = diceRoller.GetModifier(statValue);
            int total = roll + modifier;

            if (isHidden == false)
            {
                ShowResult(roll, modifier, total);
            }

            if (total >= difficultyClass)
            {
                return true;
            }

            return false;
        }

        public void ShowResult(int roll, int modifier, int total)
        {
            Console.WriteLine(checkName);
            Console.WriteLine("Roll: " + roll + " Modifier: " + modifier + " Total: " + total);
            Console.WriteLine("DC: " + difficultyClass);
        }
    }

    // Character classes

    internal abstract class Character
    {
        protected string name;
        protected int maxHp;
        protected int currentHp;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int MaxHp
        {
            get { return maxHp; }
            set { maxHp = value; }
        }

        public int CurrentHp
        {
            get { return currentHp; }
            set { currentHp = value; }
        }

        protected Character()
        {
            name = "";
            maxHp = 0;
            currentHp = 0;
        }

        public void TakeDamage(int amount)
        {
            currentHp -= amount;

            if (currentHp < 0)
            {
                currentHp = 0;
            }
        }

        public void Heal(int amount)
        {
            currentHp += amount;

            if (currentHp > maxHp)
            {
                currentHp = maxHp;
            }
        }

        public bool IsAlive()
        {
            return currentHp > 0;
        }
    }

    internal class Player : Character
    {
        private int strength;
        private int dexterity;
        private int constitution;
        private int intelligence;
        private int wisdom;
        private int charisma;
        private Inventory inventory;
        private bool hasLookedBack;

        public int Strength
        {
            get { return strength; }
            set { strength = value; }
        }

        public int Dexterity
        {
            get { return dexterity; }
            set { dexterity = value; }
        }

        public int Constitution
        {
            get { return constitution; }
            set { constitution = value; }
        }

        public int Intelligence
        {
            get { return intelligence; }
            set { intelligence = value; }
        }

        public int Wisdom
        {
            get { return wisdom; }
            set { wisdom = value; }
        }

        public int Charisma
        {
            get { return charisma; }
            set { charisma = value; }
        }

        public Inventory Inventory
        {
            get { return inventory; }
            set { inventory = value; }
        }

        public bool HasLookedBack
        {
            get { return hasLookedBack; }
            set { hasLookedBack = value; }
        }

        public Player()
        {
            name = "Orpheus";
            maxHp = 32;
            currentHp = 32;

            strength = 10;
            dexterity = 14;
            constitution = 14;
            intelligence = 12;
            wisdom = 8;
            charisma = 16;

            inventory = new Inventory();
            hasLookedBack = false;
        }

        public void ShowStatus()
        {
            Console.WriteLine();
            Console.WriteLine("Player: " + name);
            Console.WriteLine("HP: " + currentHp + "/" + maxHp);
            Console.WriteLine("STR: " + strength);
            Console.WriteLine("DEX: " + dexterity);
            Console.WriteLine("CON: " + constitution);
            Console.WriteLine("INT: " + intelligence);
            Console.WriteLine("WIS: " + wisdom);
            Console.WriteLine("CHA: " + charisma);
        }

        public void AddItem(Item item)
        {
            inventory.AddItem(item);
        }

        public void IncreaseStat(string statType, int amount)
        {
            if (statType == "Strength")
            {
                strength += amount;
            }
            else if (statType == "Dexterity")
            {
                dexterity += amount;
            }
            else if (statType == "Constitution")
            {
                constitution += amount;
            }
            else if (statType == "Intelligence")
            {
                intelligence += amount;
            }
            else if (statType == "Wisdom")
            {
                wisdom += amount;
            }
            else if (statType == "Charisma")
            {
                charisma += amount;
            }
        }

        public int GetStat(string statType)
        {
            if (statType == "Strength")
            {
                return strength;
            }
            else if (statType == "Dexterity")
            {
                return dexterity;
            }
            else if (statType == "Constitution")
            {
                return constitution;
            }
            else if (statType == "Intelligence")
            {
                return intelligence;
            }
            else if (statType == "Wisdom")
            {
                return wisdom;
            }
            else if (statType == "Charisma")
            {
                return charisma;
            }

            return 0;
        }
    }

    internal abstract class NPC : Character, IInteractable
    {
        protected List<string> dialogue;
        protected bool hasBeenMet;

        public List<string> Dialogue
        {
            get { return dialogue; }
            set { dialogue = value; }
        }

        public bool HasBeenMet
        {
            get { return hasBeenMet; }
            set { hasBeenMet = value; }
        }

        protected NPC()
        {
            dialogue = new List<string>();
            hasBeenMet = false;
        }

        public void Interact(Player player)
        {
            hasBeenMet = true;
        }

        public void Speak(string line)
        {
            Console.WriteLine(name + ": " + line);
        }
    }

    internal abstract class Enemy : Character
    {
        protected int damageAmount;

        public int DamageAmount
        {
            get { return damageAmount; }
            set { damageAmount = value; }
        }

        protected Enemy()
        {
            damageAmount = 0;
        }

        public void Attack(Player player)
        {
            player.TakeDamage(damageAmount);
        }

        public void Stun()
        {
        }
    }

    // Inventory and item classes

    internal class Inventory
    {
        private List<Item> items;

        public List<Item> Items
        {
            get { return items; }
            set { items = value; }
        }

        public Inventory()
        {
            items = new List<Item>();
        }

        public void AddItem(Item item)
        {
            items.Add(item);
        }

        public bool HasItem(string itemName)
        {
            foreach (Item item in items)
            {
                if (item.ItemName == itemName)
                {
                    return true;
                }
            }

            return false;
        }

        public void ShowItems()
        {
            Console.WriteLine("Inventory:");

            foreach (Item item in items)
            {
                Console.WriteLine("- " + item.ItemName);
            }
        }
    }

    internal abstract class Item : ICollectible
    {
        protected string itemName;
        protected string description;
        protected bool alreadyCollected;

        public string ItemName
        {
            get { return itemName; }
            set { itemName = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public bool AlreadyCollected
        {
            get { return alreadyCollected; }
            set { alreadyCollected = value; }
        }

        protected Item()
        {
            itemName = "";
            description = "";
            alreadyCollected = false;
        }

        public void Collect(Player player)
        {
            if (alreadyCollected == false)
            {
                alreadyCollected = true;
                player.AddItem(this);
                ApplyEffect(player);
            }
        }

        public virtual void ApplyEffect(Player player)
        {
        }
    }

    // Location, encounter and puzzle classes

    internal abstract class Location : IExplorable
    {
        protected string locationName;
        protected bool isVisited;
        protected List<Encounter> encounters;
        protected List<NPC> npcs;
        protected List<Enemy> enemies;
        protected List<Puzzle> puzzles;
        protected List<Item> items;

        public string LocationName
        {
            get { return locationName; }
            set { locationName = value; }
        }

        public bool IsVisited
        {
            get { return isVisited; }
            set { isVisited = value; }
        }

        public List<Encounter> Encounters
        {
            get { return encounters; }
            set { encounters = value; }
        }

        public List<NPC> NPCs
        {
            get { return npcs; }
            set { npcs = value; }
        }

        public List<Enemy> Enemies
        {
            get { return enemies; }
            set { enemies = value; }
        }

        public List<Puzzle> Puzzles
        {
            get { return puzzles; }
            set { puzzles = value; }
        }

        public List<Item> Items
        {
            get { return items; }
            set { items = value; }
        }

        protected Location()
        {
            locationName = "";
            isVisited = false;

            encounters = new List<Encounter>();
            npcs = new List<NPC>();
            enemies = new List<Enemy>();
            puzzles = new List<Puzzle>();
            items = new List<Item>();
        }

        public void Enter(Player player)
        {
            isVisited = true;
        }

        public void Explore(Player player)
        {
        }

        public void ShowMenu()
        {
        }
    }

    internal abstract class Encounter : ICheckable
    {
        protected string encounterName;
        protected bool completed;
        protected int difficultyClass;

        public string EncounterName
        {
            get { return encounterName; }
            set { encounterName = value; }
        }

        public bool Completed
        {
            get { return completed; }
            set { completed = value; }
        }

        public int DifficultyClass
        {
            get { return difficultyClass; }
            set { difficultyClass = value; }
        }

        protected Encounter()
        {
            encounterName = "";
            completed = false;
            difficultyClass = 0;
        }

        public void Start(Player player)
        {
        }

        public bool PerformCheck(Player player)
        {
            return false;
        }

        public void Complete()
        {
            completed = true;
        }
    }

    internal class Puzzle
    {
        protected string puzzleName;
        protected bool isSolved;

        public string PuzzleName
        {
            get { return puzzleName; }
            set { puzzleName = value; }
        }

        public bool IsSolved
        {
            get { return isSolved; }
            set { isSolved = value; }
        }

        public Puzzle()
        {
            puzzleName = "";
            isSolved = false;
        }

        public bool Attempt(Player player)
        {
            return false;
        }
    }
}