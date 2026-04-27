using System;
using System.Collections.Generic;

namespace Lab_Quest_W9
{
    public interface IInteractable
    {
        void Interact();
    }

    public class Door : IInteractable
    {
        public void Interact()
        {
            Console.WriteLine("The wooden door opens with a creaking sound.");
        }
    }

    public class TreasureChest : IInteractable
    {
        private bool isOpened = false;

        public void Interact()
        {
            if (isOpened)
            {
                Console.WriteLine("The chest is already empty. Look elsewhere.");
            }
            else
            {
                Console.WriteLine("You opened the chest! You found 100 Gold and a Legendary Sword.");
                isOpened = true;
            }
        }
    }

    public class NPC : IInteractable
    {
        public void Interact()
        {
            Console.WriteLine("Greetings, traveler! I have a new quest for you.");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            IInteractable door = new Door();
            IInteractable chest = new TreasureChest();
            IInteractable npc = new NPC();

            List<IInteractable> interactables = new List<IInteractable>
            {
                door,
                chest,
                npc
            };

            foreach (IInteractable item in interactables)
            {
                item.Interact();
            }

            Console.WriteLine();
            Console.WriteLine("Interacting with the chest again:");
            chest.Interact();
        }
    }
}
