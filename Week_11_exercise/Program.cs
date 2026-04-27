using System;

namespace Week_11_exercise
{
    internal class Program
    {
        static void Main(string[] args)
        {
            EnemyNPC enemyNPC = new EnemyNPC("Skeleton Warrior", 15, 100);

            EnemyMovement enemyMovement = new EnemyMovement();
            EnemyLoot enemyLoot = new EnemyLoot();
            EnemyHealthBarRenderer enemyHealthBarRenderer = new EnemyHealthBarRenderer();

            enemyMovement.ChasePlayer(enemyNPC);
            enemyHealthBarRenderer.RenderHealthBar(enemyNPC);
            enemyLoot.DropLoot(enemyNPC);

            Console.ReadLine();
        }
    }

    public class EnemyNPC
    {
        public string Name { get; set; }
        public int AttackPower { get; set; }
        public int Health { get; set; }

        public EnemyNPC(string name, int attackPower, int health)
        {
            Name = name;
            AttackPower = attackPower;
            Health = health;
        }
    }

    public class EnemyMovement
    {
        public void ChasePlayer(EnemyNPC enemyNPC)
        {
            Console.WriteLine(enemyNPC.Name + " is chasing the player.");
        }
    }

    public class EnemyLoot
    {
        public void DropLoot(EnemyNPC enemyNPC)
        {
            Console.WriteLine(enemyNPC.Name + " dropped gold and items.");
        }
    }

    public class EnemyHealthBarRenderer
    {
        public void RenderHealthBar(EnemyNPC enemyNPC)
        {
            Console.WriteLine(enemyNPC.Name + " health: " + enemyNPC.Health);
        }
    }
}