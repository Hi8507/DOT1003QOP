using System;
using System.Collections.Generic;

// ASCII Console RPG Starter for Visual Studio 2022
// C# 7.3 compatible version
//
// Controls:
//   Move: WASD or Arrow Keys
//   Talk / Inspect: E
//   Quit: Escape

class Program
{
    static Random rng = new Random();

    // -------------------- Player --------------------
    static int playerX = 10;
    static int playerY = 6;
    static int roomX = 1;
    static int roomY = 1;

    static int maxHp = 30;
    static int hp = 30;
    static int attack = 6;
    static int defense = 2;
    static int gold = 0;
    static int tonics = 2;

    static bool gameRunning = true;
    static string message = "You wake in the Old Tile Ruins. A tiny dot with a large destiny.";

    // -------------------- World Data --------------------
    static string[,] roomNames = new string[3, 3]
    {
        { "Dusty Library", "North Hall", "Mirror Shrine" },
        { "River Room", "Old Tile Ruins", "Mushroom Court" },
        { "Silent Cellar", "South Hall", "Broken Garden" }
    };

    static List<Npc> npcs = new List<Npc>()
    {
        new Npc(1, 1, 15, 5, 'O', "Old Guard", "Keep your courage polished. Monsters dislike confident dots."),
        new Npc(0, 1, 8, 8, 'F', "Ferryman", "The river remembers every step. Try speaking before fighting."),
        new Npc(2, 1, 12, 4, 'M', "Mushroom Kid", "I am not a table. I am a citizen."),
        new Npc(0, 0, 20, 7, 'S', "Scholar", "Legends say the @ symbol is a retired hero."),
        new Npc(2, 0, 7, 6, 'R', "Reflection", "You look small, but small things fit through locked stories.")
    };

    static List<MonsterOnMap> monsters = new List<MonsterOnMap>()
    {
        new MonsterOnMap(1, 1, 26, 9, 's', "Dust Slime", 18, 5, 1, 4),
        new MonsterOnMap(1, 0, 18, 5, 'b', "Book Bat", 16, 6, 1, 5),
        new MonsterOnMap(2, 1, 22, 8, 'g', "Garden Goblin", 22, 7, 2, 8),
        new MonsterOnMap(0, 2, 10, 6, 'w', "Whisper", 20, 6, 2, 7)
    };

    static List<Treasure> treasures = new List<Treasure>()
    {
        new Treasure(0, 0, 6, 5, "You found a Heart Tonic.", "tonic"),
        new Treasure(2, 0, 24, 5, "You found 8 gold inside the mirror frame.", "gold8"),
        new Treasure(2, 2, 15, 8, "You found an Old Charm. Defense increased by 1.", "defense")
    };

    // -------------------- Main Method --------------------
    static void Main(string[] args)
    {
        Console.CursorVisible = false;
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        while (gameRunning)
        {
            DrawGame();

            ConsoleKeyInfo key = Console.ReadKey(true);
            HandleInput(key.Key);

            if (hp <= 0)
            {
                Console.Clear();
                Console.WriteLine("You fall down...");
                Console.WriteLine("But this is an RPG, so you get one more chance next run.");
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey(true);
                gameRunning = false;
            }
        }

        Console.CursorVisible = true;
    }

    // -------------------- Input --------------------
    static void HandleInput(ConsoleKey key)
    {
        int newX = playerX;
        int newY = playerY;

        if (key == ConsoleKey.Escape)
        {
            gameRunning = false;
            return;
        }

        if (key == ConsoleKey.UpArrow || key == ConsoleKey.W)
        {
            newY--;
        }
        else if (key == ConsoleKey.DownArrow || key == ConsoleKey.S)
        {
            newY++;
        }
        else if (key == ConsoleKey.LeftArrow || key == ConsoleKey.A)
        {
            newX--;
        }
        else if (key == ConsoleKey.RightArrow || key == ConsoleKey.D)
        {
            newX++;
        }
        else if (key == ConsoleKey.E)
        {
            Interact();
            return;
        }
        else
        {
            return;
        }

        TryMove(newX, newY);
    }

    static void TryMove(int newX, int newY)
    {
        // Left wall: only pass through if the player is at the left door.
        if (newX < 1)
        {
            if (playerY == 5 && roomX > 0)
            {
                roomX--;
                playerX = 28;
                message = "You enter the " + roomNames[roomY, roomX] + ".";
            }
            else
            {
                message = "You bump into a wall.";
            }

            return;
        }

        // Right wall: only pass through if the player is at the right door.
        if (newX > 28)
        {
            if (playerY == 5 && roomX < 2)
            {
                roomX++;
                playerX = 1;
                message = "You enter the " + roomNames[roomY, roomX] + ".";
            }
            else
            {
                message = "You bump into a wall.";
            }

            return;
        }

        // Top wall: only pass through if the player is at the top door.
        if (newY < 1)
        {
            if (playerX == 14 && roomY > 0)
            {
                roomY--;
                playerY = 10;
                message = "You enter the " + roomNames[roomY, roomX] + ".";
            }
            else
            {
                message = "You bump into a wall.";
            }

            return;
        }

        // Bottom wall: only pass through if the player is at the bottom door.
        if (newY > 10)
        {
            if (playerX == 14 && roomY < 2)
            {
                roomY++;
                playerY = 1;
                message = "You enter the " + roomNames[roomY, roomX] + ".";
            }
            else
            {
                message = "You bump into a wall.";
            }

            return;
        }

        if (IsBlockedByNpc(newX, newY))
        {
            message = "Someone is standing there. Press E nearby to talk.";
            return;
        }

        MonsterOnMap monster = GetMonsterAt(newX, newY);
        if (monster != null)
        {
            StartBattle(monster);
            return;
        }

        Treasure treasure = GetTreasureAt(newX, newY);
        if (treasure != null)
        {
            OpenTreasure(treasure);
            return;
        }

        playerX = newX;
        playerY = newY;
        message = "";
    }

    // -------------------- Interaction --------------------
    static void Interact()
    {
        Npc npc = GetNearbyNpc();
        if (npc != null)
        {
            message = npc.Name + ": \"" + npc.Dialogue + "\"";
            return;
        }

        Treasure nearbyTreasure = GetNearbyTreasure();
        if (nearbyTreasure != null)
        {
            OpenTreasure(nearbyTreasure);
            return;
        }

        if (IsNearMapTile('*'))
        {
            message = "You touch the strange plant. It softly glows, then becomes quiet again.";
            return;
        }

        if (IsNearMapTile('~'))
        {
            message = "The water is dark and slow. Something may be watching from below.";
            return;
        }

        if (IsNearMapTile('|'))
        {
            message = "Your reflection looks back a second too late.";
            return;
        }

        if (IsNearMapTile('='))
        {
            message = "You inspect it carefully. It looks old, but still useful.";
            return;
        }

        message = "You inspect the air. It seems emotionally unavailable.";
    }

    static void OpenTreasure(Treasure treasure)
    {
        if (treasure.Opened)
        {
            message = "The chest is empty.";
            return;
        }

        treasure.Opened = true;

        if (treasure.Reward == "tonic")
        {
            tonics++;
        }
        else if (treasure.Reward == "gold8")
        {
            gold += 8;
        }
        else if (treasure.Reward == "defense")
        {
            defense++;
        }

        message = treasure.Message;
    }

    // -------------------- Combat --------------------
    static void StartBattle(MonsterOnMap mapMonster)
    {
        int enemyHp = mapMonster.MaxHp;
        bool battleRunning = true;
        bool spared = false;

        string battleMessage = "A " + mapMonster.Name + " blocks your path!";

        while (battleRunning)
        {
            DrawBattle(mapMonster, enemyHp, battleMessage);

            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.NumPad1)
            {
                int damage = Math.Max(1, attack + rng.Next(0, 4) - mapMonster.Defense);
                enemyHp -= damage;
                battleMessage = "You attack for " + damage + " damage.";
            }
            else if (key.Key == ConsoleKey.D2 || key.Key == ConsoleKey.NumPad2)
            {
                int chance = rng.Next(1, 101);

                if (chance <= 55)
                {
                    spared = true;
                    battleMessage = "You talk gently. The " + mapMonster.Name + " does not want to fight anymore.";
                }
                else
                {
                    battleMessage = "You try to talk, but the " + mapMonster.Name + " is still upset.";
                }
            }
            else if (key.Key == ConsoleKey.D3 || key.Key == ConsoleKey.NumPad3)
            {
                if (tonics > 0)
                {
                    tonics--;
                    int heal = 12;
                    hp = Math.Min(maxHp, hp + heal);
                    battleMessage = "You drink a Heart Tonic and recover " + heal + " HP.";
                }
                else
                {
                    battleMessage = "You reach for a Heart Tonic, but your pocket is empty.";
                }
            }
            else if (key.Key == ConsoleKey.D4 || key.Key == ConsoleKey.NumPad4)
            {
                if (rng.Next(1, 101) <= 45)
                {
                    message = "You escaped from the " + mapMonster.Name + ".";
                    return;
                }
                else
                {
                    battleMessage = "You try to run, but trip over the soundtrack.";
                }
            }
            else
            {
                continue;
            }

            if (enemyHp <= 0)
            {
                gold += mapMonster.GoldReward;
                monsters.Remove(mapMonster);

                message = "You defeated the " + mapMonster.Name + " and found " + mapMonster.GoldReward + " gold.";
                battleRunning = false;
            }
            else if (spared)
            {
                int spareGold = mapMonster.GoldReward / 2;

                gold += spareGold;
                monsters.Remove(mapMonster);

                message = "You spared the " + mapMonster.Name + ". It leaves behind " + spareGold + " gold.";
                battleRunning = false;
            }
            else
            {
                int enemyDamage = Math.Max(1, mapMonster.Attack + rng.Next(0, 3) - defense);
                hp -= enemyDamage;

                battleMessage += "\nThe " + mapMonster.Name + " hits you for " + enemyDamage + " damage.";

                if (hp <= 0)
                {
                    battleRunning = false;
                }
            }
        }
    }

    // -------------------- Drawing --------------------
    static void DrawGame()
    {
        Console.Clear();

        char[,] map = BuildRoomMap();

        foreach (Treasure treasure in treasures)
        {
            if (treasure.RoomX == roomX && treasure.RoomY == roomY && !treasure.Opened)
            {
                map[treasure.Y, treasure.X] = 'C';
            }
        }

        foreach (Npc npc in npcs)
        {
            if (npc.RoomX == roomX && npc.RoomY == roomY)
            {
                map[npc.Y, npc.X] = npc.Symbol;
            }
        }

        foreach (MonsterOnMap monster in monsters)
        {
            if (monster.RoomX == roomX && monster.RoomY == roomY)
            {
                map[monster.Y, monster.X] = monster.Symbol;
            }
        }

        map[playerY, playerX] = '.';

        Console.WriteLine("== " + roomNames[roomY, roomX] + " ==");
        Console.WriteLine("HP: " + hp + "/" + maxHp +
                          "   ATK: " + attack +
                          "   DEF: " + defense +
                          "   Gold: " + gold +
                          "   Tonics: " + tonics);
        Console.WriteLine();

        for (int y = 0; y < 12; y++)
        {
            for (int x = 0; x < 30; x++)
            {
                Console.Write(map[y, x]);
            }

            Console.WriteLine();
        }

        Console.WriteLine();
        Console.WriteLine("Move: WASD/Arrows   Talk/Inspect: E   Quit: Esc");
        Console.WriteLine(message);
    }

    static char[,] BuildRoomMap()
    {
        char[,] map = new char[12, 30];

        for (int y = 0; y < 12; y++)
        {
            for (int x = 0; x < 30; x++)
            {
                bool edge = y == 0 || y == 11 || x == 0 || x == 29;

                if (edge)
                {
                    map[y, x] = '#';
                }
                else
                {
                    map[y, x] = ' ';
                }
            }
        }

        // Draw doors only where another room exists.
        // D means door.
        if (roomY > 0)
        {
            map[0, 14] = 'D';
        }

        if (roomY < 2)
        {
            map[11, 14] = 'D';
        }

        if (roomX > 0)
        {
            map[5, 0] = 'D';
        }

        if (roomX < 2)
        {
            map[5, 29] = 'D';
        }

        // Room decorations
        if (roomNames[roomY, roomX].Contains("Library"))
        {
            map[3, 5] = '=';
            map[3, 6] = '=';
            map[3, 7] = '=';
            map[8, 22] = '=';
            map[8, 23] = '=';
        }
        else if (roomNames[roomY, roomX].Contains("River"))
        {
            for (int x = 3; x < 27; x++)
            {
                map[7, x] = '~';
            }

            map[7, 14] = '=';
            map[7, 15] = '=';
        }
        else if (roomNames[roomY, roomX].Contains("Garden"))
        {
            map[4, 8] = '*';
            map[6, 19] = '*';
            map[8, 12] = '*';
        }
        else if (roomNames[roomY, roomX].Contains("Shrine"))
        {
            map[5, 14] = '|';
            map[6, 14] = '|';
        }

        return map;
    }

    static void DrawBattle(MonsterOnMap enemy, int enemyHp, string battleMessage)
    {
        Console.Clear();

        Console.WriteLine("=== Battle ===");
        Console.WriteLine();
        Console.WriteLine("      " + enemy.Symbol + "    " + enemy.Name);
        Console.WriteLine("Enemy HP: " + enemyHp + "/" + enemy.MaxHp);
        Console.WriteLine();
        Console.WriteLine("Your HP: " + hp + "/" + maxHp);
        Console.WriteLine();
        Console.WriteLine(battleMessage);
        Console.WriteLine();
        Console.WriteLine("1. Attack");
        Console.WriteLine("2. Talk / Act");
        Console.WriteLine("3. Heart Tonic");
        Console.WriteLine("4. Run");
    }

    // -------------------- Helper Checks --------------------
    static bool IsBlockedByNpc(int x, int y)
    {
        foreach (Npc npc in npcs)
        {
            if (npc.RoomX == roomX &&
                npc.RoomY == roomY &&
                npc.X == x &&
                npc.Y == y)
            {
                return true;
            }
        }

        return false;
    }

    static Npc GetNearbyNpc()
    {
        foreach (Npc npc in npcs)
        {
            int distance = Math.Abs(npc.X - playerX) + Math.Abs(npc.Y - playerY);

            if (npc.RoomX == roomX &&
                npc.RoomY == roomY &&
                distance <= 1)
            {
                return npc;
            }
        }

        return null;
    }

    static MonsterOnMap GetMonsterAt(int x, int y)
    {
        foreach (MonsterOnMap monster in monsters)
        {
            if (monster.RoomX == roomX &&
                monster.RoomY == roomY &&
                monster.X == x &&
                monster.Y == y)
            {
                return monster;
            }
        }

        return null;
    }

    static Treasure GetTreasureAt(int x, int y)
    {
        foreach (Treasure treasure in treasures)
        {
            // Opened treasures should not block movement anymore.
            if (treasure.RoomX == roomX &&
                treasure.RoomY == roomY &&
                treasure.X == x &&
                treasure.Y == y &&
                !treasure.Opened)
            {
                return treasure;
            }
        }

        return null;
    }

    static Treasure GetNearbyTreasure()
    {
        foreach (Treasure treasure in treasures)
        {
            int distance = Math.Abs(treasure.X - playerX) + Math.Abs(treasure.Y - playerY);

            if (treasure.RoomX == roomX &&
                treasure.RoomY == roomY &&
                distance <= 1 &&
                !treasure.Opened)
            {
                return treasure;
            }
        }

        return null;
    }

    static bool IsNearMapTile(char target)
    {
        char[,] map = BuildRoomMap();

        int[,] directions = new int[,]
        {
            { 0, -1 },
            { 0, 1 },
            { -1, 0 },
            { 1, 0 },
            { 0, 0 }
        };

        for (int i = 0; i < directions.GetLength(0); i++)
        {
            int checkX = playerX + directions[i, 0];
            int checkY = playerY + directions[i, 1];

            if (checkX >= 0 && checkX < 30 && checkY >= 0 && checkY < 12)
            {
                if (map[checkY, checkX] == target)
                {
                    return true;
                }
            }
        }

        return false;
    }
}

// -------------------- Data Classes --------------------
class Npc
{
    public int RoomX;
    public int RoomY;
    public int X;
    public int Y;
    public char Symbol;
    public string Name;
    public string Dialogue;

    public Npc(int roomX, int roomY, int x, int y, char symbol, string name, string dialogue)
    {
        RoomX = roomX;
        RoomY = roomY;
        X = x;
        Y = y;
        Symbol = symbol;
        Name = name;
        Dialogue = dialogue;
    }
}

class MonsterOnMap
{
    public int RoomX;
    public int RoomY;
    public int X;
    public int Y;
    public char Symbol;
    public string Name;
    public int MaxHp;
    public int Attack;
    public int Defense;
    public int GoldReward;

    public MonsterOnMap(int roomX, int roomY, int x, int y, char symbol, string name, int maxHp, int attack, int defense, int goldReward)
    {
        RoomX = roomX;
        RoomY = roomY;
        X = x;
        Y = y;
        Symbol = symbol;
        Name = name;
        MaxHp = maxHp;
        Attack = attack;
        Defense = defense;
        GoldReward = goldReward;
    }
}

class Treasure
{
    public int RoomX;
    public int RoomY;
    public int X;
    public int Y;
    public string Message;
    public string Reward;
    public bool Opened;

    public Treasure(int roomX, int roomY, int x, int y, string message, string reward)
    {
        RoomX = roomX;
        RoomY = roomY;
        X = x;
        Y = y;
        Message = message;
        Reward = reward;
        Opened = false;
    }
}