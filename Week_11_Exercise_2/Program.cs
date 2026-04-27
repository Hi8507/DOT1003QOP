using System;

namespace Week_11_Exercise_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Sword sword = new Sword();
            SniperRifle sniperRifle = new SniperRifle();

            sword.Attack();

            sniperRifle.Attack();
            sniperRifle.Reload();
            sniperRifle.AimDownSights();

            Console.ReadLine();
        }
    }

    public interface IAttackable
    {
        void Attack();
    }


    public interface IReloadable
    {
        void Reload();
    }

    public interface IAimable
    {
        void AimDownSights();
    }

    public class Sword : IAttackable
    {
        public void Attack()
        {
            Console.WriteLine("Swinging the sword!");
        }
    }

    public class SniperRifle : IAttackable, IReloadable, IAimable
    {
        public void Attack()
        {
            Console.WriteLine("Firing a high-caliber round!");
        }

        public void Reload()
        {
            Console.WriteLine("Loading a new magazine.");
        }

        public void AimDownSights()
        {
            Console.WriteLine("Looking through the 8x scope.");
        }
    }
}