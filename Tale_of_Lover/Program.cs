using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tale_of_Lover
{
    internal class Program
    {
        static Random random = new Random();

        // Stats
        static int strength = 10;
        static int dexterity = 14;
        static int constitution = 14;
        static int intelligence = 12;
        static int wisdom = 8;
        static int charisma = 16;

        // HP
        static int maxHp = 32;
        static int currentHp = 32;

        // Global state
        static bool gameEnded = false;
        static string nextArea = "cave";
        static string boatType = "";
        static bool enteredAshByBoat = false;

        // Collectibles
        static bool hasSongOfSilence = false;
        static bool hasTitanBone = false;
        static bool hasShadowCloak = false;
        static bool hasMemoryFragment = false;
        static bool hasSpiritCharm = false;
        static bool hasVitalCore = false;

        // Cave flags
        static bool didCerberus = false;
        static bool cerberusCalmed = false;
        static bool didExamineCave = false;


        // Catacombs flags
        static bool didPrometheus = false;
        static bool didPillar = false;
        static bool didWhispers = false;
        static bool helpedPrometheus = false;
        static bool didChaosGate = false;
        static bool foundHiddenPath = false;

        // Ash flags
        static bool didAshBattlefield = false;
        static bool didAshSearch = false;
        static bool didZagreus = false;
        static bool knowsAshPattern = false;
        static bool crossedAsh = false;

        // Oath flags
        static bool gotBurdenWord = false;
        static bool gotLoveWord = false;
        static bool gotLossWord = false;

        static bool didCronos = false;
        static bool didGarden = false;
        static bool didThanatos = false;

        static bool knowsCronos = false;
        static bool knowsGardenTruth = false;
        static bool knowsThanatos = false;

        // Hades flags
        static bool metHypnos = false;
        static int hadesFavor = 0;
        // Hades bonus usage flags
        static bool usedBonusCerberus = false;
        static bool usedBonusCharon = false;
        static bool usedBonusOdysseus = false;
        static bool usedBonusPrometheus = false;
        static bool usedBonusThanatos = false;
        static bool usedBonusGarden = false;
        static bool usedBonusCronos = false;

        // Lore flags
        static bool knowsPrometheus = false;

        static void Main(string[] args)
        {
            StartGame();

            while (gameEnded == false)
            {
                if (nextArea == "cave")
                {
                    CaveEntrance();
                }

                if (nextArea == "river")
                {
                    RiverOfEchoes();
                }

                if (nextArea == "catacombs")
                {
                    CatacombsArea();
                }

                if (nextArea == "ash")
                {
                    AshArea();
                }

                if (nextArea == "oath")
                {
                    OathArea();
                }
                if (nextArea == "hades")
                {
                    HadesArea();
                }

                if (nextArea == "end")
                {
                    gameEnded = true;
                }
            }

            Console.WriteLine();
            WriteLineSlow("Press any key to close...");
            Console.ReadKey();
        }

        static void StartGame()
        {
            Console.Clear();
            WriteLineSlow("=== ECHOES OF THE UNDERWORLD ===");
            Console.WriteLine();
            WriteLineSlow("You are Orpheus, a musician who has descended into the Underworld.");
            Console.WriteLine();
            WriteLineSlow("Your muse is lost beyond the veil of death.");
            WriteLineSlow("There is only one path forward.");
            Console.WriteLine();
            WriteLineSlow("You cannot return without her.");
            Console.WriteLine();
            WriteLineSlow("The air is cold.");
            WriteLineSlow("The silence is heavy.");
            WriteLineSlow("And something watches from the dark.");
            Console.WriteLine();
            ShowStatus();
            Console.WriteLine();
            WriteLineSlow("Press any key to begin...");
            Console.ReadKey();
        }

        static void ShowStatus()
        {
            WriteLineSlow("--------------------------------");
            WriteLineSlow("Your Stats:");
            WriteLineSlow($"Strength: {strength} ({GetModifier(strength):+0;-#;0})");
            WriteLineSlow($"Dexterity: {dexterity} ({GetModifier(dexterity):+0;-#;0})");
            WriteLineSlow($"Constitution: {constitution} ({GetModifier(constitution):+0;-#;0})");
            WriteLineSlow($"Intelligence: {intelligence} ({GetModifier(intelligence):+0;-#;0})");
            WriteLineSlow($"Wisdom: {wisdom} ({GetModifier(wisdom):+0;-#;0})");
            WriteLineSlow($"Charisma: {charisma} ({GetModifier(charisma):+0;-#;0})");
            Console.WriteLine();
            WriteLineSlow($"HP: {currentHp} / {maxHp}");
            WriteLineSlow("--------------------------------");
        }

        static int GetModifier(int statValue)
        {
            return (statValue - 10) / 2;
        }

        static int RollD20()
        {
            return random.Next(1, 21);
        }


        static void WriteSlow(string text, int delayMs = 25)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                System.Threading.Thread.Sleep(delayMs);
            }
        }

        static void WriteLineSlow(string text, int delayMs = 25)
        {
            WriteSlow(text, delayMs);
            Console.WriteLine();
        }

        static bool VisibleCheck(string checkName, int statValue, int dc)
        {
            int roll = RollD20();
            int modifier = GetModifier(statValue);
            int total = roll + modifier;

            Console.WriteLine();
            WriteLineSlow($"{checkName}");
            WriteLineSlow("Rolling...");
            WriteLineSlow($"Roll: {roll} {modifier:+0;-#;0} = {total}");
            WriteLineSlow($"DC: {dc}");

            if (total >= dc)
            {
                return true;
            }

            return false;
        }

        static bool HiddenCheck(int statValue, int dc)
        {
            int roll = RollD20();
            int modifier = GetModifier(statValue);
            int total = roll + modifier;

            if (total >= dc)
            {
                return true;
            }

            return false;
        }

        static void TakeDamage(int damageAmount)
        {
            currentHp -= damageAmount;

            if (currentHp < 0)
            {
                currentHp = 0;
            }

            Console.WriteLine();
            WriteLineSlow($"You take {damageAmount} damage.");
            WriteLineSlow($"HP: {currentHp} / {maxHp}");

            if (currentHp <= 0)
            {
                Console.WriteLine();
                WriteLineSlow("Your strength fades.");
                WriteLineSlow("The Underworld claims you.");
                WriteLineSlow("You are lost forever.");
                gameEnded = true;
                nextArea = "end";
            }
        }

        static void AddCollectible(string collectibleName)
        {
            Console.WriteLine();
            WriteLineSlow($"You found: {collectibleName}");
            Console.WriteLine();

            if (collectibleName == "Spirit Charm")
            {
                if (hasSpiritCharm == false)
                {
                    int oldWisdom = wisdom;
                    hasSpiritCharm = true;
                    wisdom += 2;
                    WriteLineSlow("Your wisdom increases.");
                    WriteLineSlow($"Wisdom: {oldWisdom} -> {wisdom}");
                }
                else
                {
                    WriteLineSlow("You already have this.");
                }
            }

            if (collectibleName == "Memory Fragment")
            {
                if (hasMemoryFragment == false)
                {
                    int oldIntelligence = intelligence;
                    hasMemoryFragment = true;
                    intelligence += 2;
                    WriteLineSlow("Your intelligence increases.");
                    WriteLineSlow($"Intelligence: {oldIntelligence} -> {intelligence}");
                }
                else
                {
                    WriteLineSlow("You already have this.");
                }
            }

            if (collectibleName == "Shadow Cloak")
            {
                if (hasShadowCloak == false)
                {
                    int oldDexterity = dexterity;
                    hasShadowCloak = true;
                    dexterity += 2;
                    WriteLineSlow("Your dexterity increases.");
                    WriteLineSlow($"Dexterity: {oldDexterity} -> {dexterity}");
                }
                else
                {
                    WriteLineSlow("You already have this.");
                }
            }

            if (collectibleName == "Titan Bone")
            {
                if (hasTitanBone == false)
                {
                    int oldStrength = strength;
                    hasTitanBone = true;
                    strength += 2;
                    WriteLineSlow("Your strength increases.");
                    WriteLineSlow($"Strength: {oldStrength} -> {strength}");
                }
                else
                {
                    WriteLineSlow("You already have this.");
                }
            }

            if (collectibleName == "Song of Silence")
            {
                if (hasSongOfSilence == false)
                {
                    int oldCharisma = charisma;
                    hasSongOfSilence = true;
                    charisma += 2;
                    WriteLineSlow("Your charisma increases.");
                    WriteLineSlow($"Charisma: {oldCharisma} -> {charisma}");
                }
                else
                {
                    WriteLineSlow("You already have this.");
                }
            }

            if (collectibleName == "Vital Core")
            {
                if (hasVitalCore == false)
                {
                    int oldConstitution = constitution;
                    int oldHp = currentHp;

                    hasVitalCore = true;
                    constitution += 2;
                    maxHp = 34;
                    currentHp += 7;

                    if (currentHp > maxHp)
                    {
                        currentHp = maxHp;
                    }

                    WriteLineSlow("A warm energy fills your body.");
                    WriteLineSlow($"Constitution: {oldConstitution} -> {constitution}");
                    WriteLineSlow($"HP: {oldHp} -> {currentHp}");
                    WriteLineSlow($"Max HP: {maxHp}");
                }
                else
                {
                    WriteLineSlow("You already have this.");
                }
            }
        }

        static int AskChoice(int minChoice, int maxChoice)
        {
            bool validChoice = false;
            int finalChoice = minChoice;

            while (validChoice == false)
            {
                Console.Write("Choice: ");
                string userInput = Console.ReadLine();

                if (int.TryParse(userInput, out finalChoice))
                {
                    if (finalChoice >= minChoice && finalChoice <= maxChoice)
                    {
                        validChoice = true;
                    }
                    else
                    {
                        WriteLineSlow("Please enter a valid number.");
                    }
                }
                else
                {
                    WriteLineSlow("Please enter a number.");
                }
            }

            return finalChoice;
        }

        static void Pause()
        {
            Console.WriteLine();
            WriteLineSlow("Press any key to continue...");
            Console.ReadKey();
        }

        // CAVE
        static void CaveEntrance()
        {
            Console.Clear();
            WriteLineSlow("// CAVE");
            Console.WriteLine();
            WriteLineSlow("You step into the mouth of the cave.");
            Console.WriteLine();
            WriteLineSlow("The world behind you fades into silence.");
            Console.WriteLine();
            WriteLineSlow("The cave walls are jagged, blackened like they have been burned long ago.");
            WriteLineSlow("Faint blue light seeps through cracks in the stone, pulsing like a heartbeat.");
            Console.WriteLine();
            WriteLineSlow("A low growl echoes through the chamber.");
            Console.WriteLine();
            WriteLineSlow("Ahead, the cave slopes deeper into darkness.");
            Console.WriteLine();
            WriteLineSlow("To your side, a massive figure rests in the shadows.");
            Console.WriteLine();
            WriteLineSlow("Three heads. Six glowing eyes.");
            Console.WriteLine();
            WriteLineSlow("Cerberus.");
            Console.WriteLine();

            bool stayInCave = true;

            while (stayInCave == true && gameEnded == false)
            {
                WriteLineSlow("What do you do?");
                WriteLineSlow("1. Turn back");
                WriteLineSlow("2. Approach the creature");
                WriteLineSlow("3. Play your lyre");
                WriteLineSlow("4. Move deeper into the cave");
                WriteLineSlow("5. Examine your surroundings");
                WriteLineSlow("6. Show status");

                int playerChoice = AskChoice(1, 6);
                Console.WriteLine();

                if (playerChoice == 1)
                {
                    WriteLineSlow("You turn toward the cave entrance.");
                    WriteLineSlow("A cold force stops you.");
                    WriteLineSlow("You feel it deep within you.");
                    WriteLineSlow("You cannot leave.");
                    WriteLineSlow("Not without her.");
                    Console.WriteLine();
                }

                if (playerChoice == 2)
                {
                    CerberusApproach();
                }

                if (playerChoice == 3)
                {
                    CerberusLyre();
                }

                if (playerChoice == 4)
                {
                    WriteLineSlow("You avoid the creature and move forward.");
                    WriteLineSlow("Its eyes follow you.");
                    WriteLineSlow("But it does not attack.");
                    Console.WriteLine();
                    WriteLineSlow("The darkness ahead swallows you.");
                    Console.WriteLine();
                    Pause();
                    nextArea = "river";
                    stayInCave = false;
                }

                if (playerChoice == 5)
                {
                    ExamineCave();
                }

                if (playerChoice == 6)
                {
                    ShowStatus();
                    Console.WriteLine();
                }
            }
        }

        static void CerberusApproach()
        {
            if (didCerberus == true)
            {
                WriteLineSlow("Cerberus watches you quietly.");
                WriteLineSlow("It does not react.");
                WriteLineSlow("You have already earned its trust.");
                Console.WriteLine();
                return;
            }

            WriteLineSlow("You step toward the beast.");
            WriteLineSlow("It rises immediately.");
            WriteLineSlow("A deep snarl shakes the cave.");
            Console.WriteLine();

            bool dodgeSuccess = VisibleCheck("You try to react.", dexterity, 8);
            Console.WriteLine();

            if (dodgeSuccess == true)
            {
                WriteLineSlow("You dodge the attack.");
                Console.WriteLine();
                WriteLineSlow("What do you do?");
                WriteLineSlow("1. Stand your ground");
                WriteLineSlow("2. Flee");

                int followChoice = AskChoice(1, 2);
                Console.WriteLine();

                if (followChoice == 2)
                {
                    WriteLineSlow("You seize the moment and escape.");
                    WriteLineSlow("Cerberus does not follow.");
                    Console.WriteLine();
                }

                if (followChoice == 1)
                {
                    WriteLineSlow("You remain in place, but the beast does not attack again.");
                    WriteLineSlow("Maybe there is another way.");
                    Console.WriteLine();
                }
            }

            if (dodgeSuccess == false)
            {
                WriteLineSlow("Cerberus lunges.");
                TakeDamage(5);
                Console.WriteLine();

                if (gameEnded == false)
                {
                    WriteLineSlow("If you keep getting attacked, you may have to find other ways to get things done.");
                    Console.WriteLine();
                }
            }
        }

        static void CerberusLyre()
        {
            if (didCerberus == true)
            {
                WriteLineSlow("Cerberus watches you quietly.");
                WriteLineSlow("It does not react.");
                WriteLineSlow("You have already earned its trust.");
                Console.WriteLine();
                return;
            }

            bool lyreSuccess = VisibleCheck("You gently pluck the strings of your lyre.", charisma, 8);
            Console.WriteLine();

            if (lyreSuccess == true)
            {
                WriteLineSlow("The melody echoes softly through the cave.");
                WriteLineSlow("Cerberus' growls fade.");
                WriteLineSlow("Its heads lower.");
                Console.WriteLine();
                WriteLineSlow("The beast slowly lies down.");
                WriteLineSlow("It is calm now.");
                Console.WriteLine();
                WriteLineSlow("You step closer.");
                WriteLineSlow("One of its heads nudges your hand.");
                WriteLineSlow("You carefully pet Cerberus.");
                WriteLineSlow("It lets out a deep, content rumble.");
                Console.WriteLine();

                didCerberus = true;
                cerberusCalmed = true;

                if (hasSpiritCharm == false)
                {
                    WriteLineSlow("Something glimmers beneath its paw.");
                    AddCollectible("Spirit Charm");
                }
                else
                {
                    WriteLineSlow("There is nothing else here you still need.");
                    Console.WriteLine();
                }
            }

            if (lyreSuccess == false)
            {
                WriteLineSlow("The melody falters.");
                WriteLineSlow("Cerberus remains alert.");
                Console.WriteLine();
            }
        }

        static void ExamineCave()
        {
            if (didExamineCave == true)
            {
                WriteLineSlow("You have already been here.");
                WriteLineSlow("There is nothing more to gain.");
                Console.WriteLine();
                return;
            }

            didExamineCave = true;

            bool examineSuccess = HiddenCheck(intelligence, 9);

            if (examineSuccess == true)
            {
                WriteLineSlow("You study the cave carefully.");
                WriteLineSlow("The cracks in the wall pulse with strange energy.");
                WriteLineSlow("Something catches your eye.");
                Console.WriteLine();
                AddCollectible("Memory Fragment");
            }

            if (examineSuccess == false)
            {
                WriteLineSlow("You look around, but the darkness hides its secrets.");
                WriteLineSlow("Nothing stands out.");
                Console.WriteLine();
            }
        }

        // RIVER
        static bool triedSteal = false;
        static bool triedCharonTalk = false;
        static bool triedCharonLyre = false;
        static bool usedOdysseus = false;

        static void RiverOfEchoes()
        {
            Console.Clear();
            WriteLineSlow("// RIVER");
            Console.WriteLine();
            WriteLineSlow("You arrive at a vast underground river.");
            WriteLineSlow("Dark waters stretch endlessly into shadow.");
            Console.WriteLine();
            WriteLineSlow("Shades stand in a long silent line.");
            WriteLineSlow("A cloaked ferryman waits beside a wooden boat.");
            Console.WriteLine();

            bool knowsCharon = HiddenCheck(wisdom, 7);

            if (knowsCharon == true)
            {
                WriteLineSlow("You recognize the ferryman.");
                WriteLineSlow("Charon.");
                WriteLineSlow("No soul crosses without payment.");
            }
            else
            {
                WriteLineSlow("You hear coins clinking.");
                WriteLineSlow("The souls whisper among themselves.");
                WriteLineSlow("They speak of payment… and a ferryman.");
            }

            Console.WriteLine();

            bool stayHere = true;

            while (stayHere == true && gameEnded == false)
            {
                WriteLineSlow("What do you do?");

                List<string> riverOptions = new List<string>();

                if (triedSteal == false)
                {
                    riverOptions.Add("steal");
                    WriteLineSlow($"{riverOptions.Count}. Try to steal coins from a soul");
                }

                if (triedCharonTalk == false)
                {
                    riverOptions.Add("charonTalk");
                    WriteLineSlow($"{riverOptions.Count}. Speak to Charon");
                }

                if (triedCharonLyre == false)
                {
                    riverOptions.Add("charonLyre");
                    WriteLineSlow($"{riverOptions.Count}. Play your lyre");
                }

                if (usedOdysseus == false)
                {
                    riverOptions.Add("odysseus");
                    WriteLineSlow($"{riverOptions.Count}. Wait and observe");
                }

                riverOptions.Add("status");
                WriteLineSlow($"{riverOptions.Count}. Show status");

                int choice = AskChoice(1, riverOptions.Count);
                string selectedOption = riverOptions[choice - 1];
                Console.WriteLine();

                if (selectedOption == "steal")
                {
                    triedSteal = true;
                    SkeletonEncounter();
                }

                if (selectedOption == "charonTalk")
                {
                    triedCharonTalk = true;

                    bool success = VisibleCheck("You try to persuade Charon.", charisma, 12);
                    Console.WriteLine();

                    if (success == true)
                    {
                        WriteLineSlow("Charon listens.");
                        WriteLineSlow("Then he gestures toward the boat.");
                        Console.WriteLine();
                        boatType = "charon";
                        stayHere = false;
                        NextAfterRiver();
                    }
                    else
                    {
                        WriteLineSlow("Charon remains still.");
                        Console.WriteLine();
                    }
                }

                if (selectedOption == "charonLyre")
                {
                    triedCharonLyre = true;

                    bool success = VisibleCheck("You play your lyre for Charon.", charisma, 10);
                    Console.WriteLine();

                    if (success == true)
                    {
                        WriteLineSlow("The melody drifts over the water.");
                        WriteLineSlow("Charon turns toward you.");
                        WriteLineSlow("Then he gestures toward the boat.");
                        Console.WriteLine();
                        boatType = "charon";
                        stayHere = false;
                        NextAfterRiver();
                    }
                    else
                    {
                        WriteLineSlow("Charon hums in disappointment.");
                        Console.WriteLine();
                    }
                }

                if (selectedOption == "odysseus")
                {
                    usedOdysseus = true;

                    bool odysseusSuccess = OdysseusEncounter();

                    if (odysseusSuccess == true)
                    {
                        stayHere = false;
                        NextAfterRiver();
                    }
                }

                if (selectedOption == "status")
                {
                    ShowStatus();
                    Console.WriteLine();
                }

                if (triedSteal == true && triedCharonTalk == true && triedCharonLyre == true && usedOdysseus == true && boatType == "")
                {
                    WriteLineSlow("You failed to cross the river.");
                    WriteLineSlow("There is nothing left.");
                    Console.WriteLine();
                    WriteLineSlow("The Underworld rejects you.");
                    Console.WriteLine();
                    gameEnded = true;
                    nextArea = "end";
                }
            }
        }

        // =========================
        // SKELETON COMBAT
        // =========================

        static void SkeletonEncounter()
        {
            WriteLineSlow("You reach for a coin.");
            Console.WriteLine();
            WriteLineSlow("A skeletal hand grabs your wrist.");
            WriteLineSlow("A skeleton rises from the line.");
            Console.WriteLine();

            bool escaped = false;

            while (escaped == false && gameEnded == false)
            {
                bool dodge = VisibleCheck("You try to dodge.", dexterity, 10);
                Console.WriteLine();

                if (dodge == true)
                {
                    WriteLineSlow("You avoid the attack.");
                    Console.WriteLine();
                    WriteLineSlow("1. Flee");
                    WriteLineSlow("2. Stay");

                    int choice = AskChoice(1, 2);
                    Console.WriteLine();

                    if (choice == 1)
                    {
                        WriteLineSlow("You escape the skeleton.");
                        Console.WriteLine();
                        escaped = true;
                    }

                    if (choice == 2)
                    {
                        WriteLineSlow("You remain, but the skeleton presses forward.");
                        Console.WriteLine();
                    }
                }
                else
                {
                    WriteLineSlow("The skeleton strikes you.");
                    TakeDamage(5);
                    Console.WriteLine();

                    if (gameEnded == false)
                    {
                        WriteLineSlow("You might need another approach.");
                        Console.WriteLine();
                    }
                }
            }
        }

        // =========================
        // ODYSSEUS
        // =========================

        static bool OdysseusEncounter()
        {
            WriteLineSlow("You wait.");
            Console.WriteLine();
            WriteLineSlow("In the distance, a ship appears.");
            WriteLineSlow("Voices echo across the river.");
            Console.WriteLine();
            WriteLineSlow("A man steps forward.");
            WriteLineSlow("He watches you carefully.");
            Console.WriteLine();
            WriteLineSlow("\"Another lost soul,\" he says.");
            Console.WriteLine();

            WriteLineSlow("1. Tell him you are not dead");
            WriteLineSlow("2. Play your lyre");

            int choice = AskChoice(1, 2);
            Console.WriteLine();

            if (choice == 1)
            {
                WriteLineSlow("He frowns.");
                WriteLineSlow("\"No living man walks here,\" he says.");
                Console.WriteLine();
                WriteLineSlow("You have failed.");
                Console.WriteLine();
                gameEnded = true;
                nextArea = "end";
                return false;
            }

            if (choice == 2)
            {
                bool success = VisibleCheck("You play your lyre for the crew.", charisma, 10);
                Console.WriteLine();

                if (success == true)
                {
                    WriteLineSlow("The crew falls silent.");
                    WriteLineSlow("The captain watches you closely.");
                    Console.WriteLine();
                    WriteLineSlow("\"You are no shade,\" he says.");
                    Console.WriteLine();
                    WriteLineSlow("He offers you passage.");
                    Console.WriteLine();

                    boatType = "odysseus";
                    return true;
                }
                else
                {
                    WriteLineSlow("The song fails.");
                    WriteLineSlow("They turn away.");
                    Console.WriteLine();
                    WriteLineSlow("You are left behind.");
                    Console.WriteLine();
                    gameEnded = true;
                    nextArea = "end";
                    return false;
                }
            }

            return false;
        }

        // =========================
        // CONNECTION
        // =========================

        // CATACOMBS
        static void CatacombsArea()
        {
            Console.Clear();
            WriteLineSlow("// CATACOMBS");
            Console.WriteLine();
            WriteLineSlow("You step deeper into the ruins.");
            WriteLineSlow("The air is colder here.");
            Console.WriteLine();
            WriteLineSlow("Stone corridors stretch endlessly, lined with broken statues and collapsed pillars.");
            WriteLineSlow("Faint whispers echo between the walls.");
            WriteLineSlow("Some are memories.");
            WriteLineSlow("Some are not.");
            Console.WriteLine();

            bool chaosGateAvailable = false;

            if (didChaosGate == false)
            {
                    chaosGateAvailable = HiddenCheck(intelligence, 10);
            }

            bool stayInCatacombs = true;

            while (stayInCatacombs == true && gameEnded == false)
            {
                WriteLineSlow("What do you do?");
                WriteLineSlow("1. Climb toward the distant mountain path");
                WriteLineSlow("2. Inspect the fallen structures");
                WriteLineSlow("3. Listen closely to the whispers");
                WriteLineSlow("4. Move forward carefully");

                if (chaosGateAvailable == true && didChaosGate == false)
                {
                    WriteLineSlow("5. Approach the strange gate");
                    WriteLineSlow("6. Show status");
                }
                else
                {
                    WriteLineSlow("5. Show status");
                }

                int maxChoice = 5;

                if (chaosGateAvailable == true && didChaosGate == false)
                {
                    maxChoice = 6;
                }

                int choice = AskChoice(1, maxChoice);
                Console.WriteLine();

                if (choice == 1)
                {
                    PrometheusEncounter();
                }

                if (choice == 2)
                {
                    PillarEncounter();
                }

                if (choice == 3)
                {
                    bool whisperMovedYou = WhispersEncounter();

                    if (whisperMovedYou == true)
                    {
                        Pause();
                        enteredAshByBoat = false;
                        nextArea = "ash";
                        stayInCatacombs = false;
                    }
                }

                if (choice == 4)
                {
                    WriteLineSlow("You choose not to linger.");
                    WriteLineSlow("You move deeper into the catacombs.");
                    WriteLineSlow("Whatever lies ahead—");
                    WriteLineSlow("You will face it.");
                    Console.WriteLine();
                    Pause();
                    enteredAshByBoat = false;
                    nextArea = "ash";
                    stayInCatacombs = false;
                }

                if (chaosGateAvailable == true && didChaosGate == false)
                {
                    if (choice == 5)
                    {
                        ChaosGateEncounter();
                    }

                    if (choice == 6)
                    {
                        ShowStatus();
                        Console.WriteLine();
                    }
                }
                else
                {
                    if (choice == 5)
                    {
                        ShowStatus();
                        Console.WriteLine();
                    }
                }
            }
        }

        static void PrometheusEncounter()
        {
            if (helpedPrometheus == true)
            {
                WriteLineSlow("You return to the mountain.");
                WriteLineSlow("Prometheus remains bound no longer.");
                WriteLineSlow("The eagle does not return.");
                WriteLineSlow("There is nothing more you can do here.");
                Console.WriteLine();
                return;
            }



            WriteLineSlow("You follow a narrow path upward.");
            WriteLineSlow("The air grows colder.");
            Console.WriteLine();
            WriteLineSlow("At the peak—");
            Console.WriteLine();
            WriteLineSlow("A figure is chained to the rock.");
            WriteLineSlow("An eagle circles above.");
            Console.WriteLine();

            knowsPrometheus = HiddenCheck(wisdom, 9);

            if (knowsPrometheus == true)
            {
                WriteLineSlow("The chains. The fire. The eagle.");
                WriteLineSlow("You remember the stories.");
                Console.WriteLine();
                WriteLineSlow("Prometheus.");
                Console.WriteLine();
            }
            else
            {
                WriteLineSlow("\"You do not recognize me,\" the figure says.");
                WriteLineSlow("\"I am Prometheus.\"");
                WriteLineSlow("\"Punished for giving fire to mankind.\"");
                Console.WriteLine();
                WriteLineSlow("He glances upward.");
                WriteLineSlow("\"The eagle comes every day.\"");
                WriteLineSlow("\"It does not forget.\"");
                Console.WriteLine();
            }

            WriteLineSlow("\"A mortal… alive.\"");
            WriteLineSlow("\"What are you doing in this place?\"");
            Console.WriteLine();
            WriteLineSlow("\"I am here for someone I love.\"");
            WriteLineSlow("\"I will not leave without her.\"");
            Console.WriteLine();
            WriteLineSlow("He studies you.");
            WriteLineSlow("\"Then you understand suffering.\"");
            Console.WriteLine();
            WriteLineSlow("He turns his gaze upward.");
            WriteLineSlow("\"If you would help me, do it now.\"");
            WriteLineSlow("\"The eagle comes, and it will not suffer your presence.\"");
            Console.WriteLine();
            WriteLineSlow("A dark shape cuts across the dim light.");
            WriteLineSlow("The eagle descends.");
            WriteLineSlow("Its cry splits the mountain air.");
            Console.WriteLine();
            WriteLineSlow("What do you do?");
            WriteLineSlow("1. Throw a rock");
            WriteLineSlow("2. Play your lyre");
            WriteLineSlow("3. Set a trap");

            int choice = AskChoice(1, 3);
            Console.WriteLine();

            bool success = false;

            if (choice == 1)
            {
                success = VisibleCheck("You throw a rock.", strength, 10);
            }

            if (choice == 2)
            {
                success = VisibleCheck("You play your lyre.", charisma, 9);
            }

            if (choice == 3)
            {
                success = VisibleCheck("You set a trap.", intelligence, 11);
            }

            Console.WriteLine();

            if (success == true)
            {
                helpedPrometheus = true;
                didPrometheus = true;

                WriteLineSlow("The eagle is driven back.");
                WriteLineSlow("It no longer blocks your path.");
                Console.WriteLine();
                WriteLineSlow("Prometheus looks at you.");
                WriteLineSlow("\"You have done what even gods could not.\"");
                Console.WriteLine();
                AddCollectible("Titan Bone");
            }
            else
            {
                WriteLineSlow("The eagle strikes.");
                TakeDamage(4);
                Console.WriteLine();
            }

            if (gameEnded == false)
            {
                WriteLineSlow("You step back from the mountain path.");
                WriteLineSlow("The catacombs stretch before you once more.");
                Console.WriteLine();
            }
        }

        static void PillarEncounter()
        {
            if (didPillar == true)
            {
                WriteLineSlow("You return to the fallen pillar.");
                WriteLineSlow("The space beneath it is empty.");
                WriteLineSlow("The soul you freed is gone.");
                WriteLineSlow("There is nothing left here.");
                Console.WriteLine();
                return;
            }



            WriteLineSlow("You move toward the collapsed ruins.");
            WriteLineSlow("A large stone pillar has fallen.");
            Console.WriteLine();
            WriteLineSlow("Beneath it—");
            Console.WriteLine();
            WriteLineSlow("A faint voice.");
            Console.WriteLine();
            WriteLineSlow("What do you do?");
            WriteLineSlow("1. Try to lift the pillar");
            WriteLineSlow("2. Leave");

            int choice = AskChoice(1, 2);
            Console.WriteLine();

            if (choice == 2)
            {

                WriteLineSlow("You step away from the ruins.");
                WriteLineSlow("The whispers of the catacombs surround you again.");
                Console.WriteLine();
                return;
            }

            didPillar = true;

            bool success = VisibleCheck("You try to lift the pillar.", strength, 11);
            Console.WriteLine();

            if (success == true)
            {
                WriteLineSlow("You push with everything you have.");
                WriteLineSlow("The stone shifts.");
                WriteLineSlow("The trapped soul is freed.");
                Console.WriteLine();

                if (hasVitalCore == false)
                {
                    WriteLineSlow("\"Thank you...\"");
                    Console.WriteLine();
                    WriteLineSlow("The soul begins to fade.");
                    WriteLineSlow("Before disappearing, it leaves something behind.");
                    Console.WriteLine();
                    AddCollectible("Vital Core");
                }
                else
                {
                    WriteLineSlow("The spirit looks at you quietly.");
                    WriteLineSlow("\"I am sorry, but I have nothing to give that you do not already carry.\"");
                    WriteLineSlow("\"Still... you have enough strength within you to keep going.\"");
                    Console.WriteLine();
                }
            }
            else
            {
                WriteLineSlow("You push against the stone.");
                WriteLineSlow("It does not move.");
                WriteLineSlow("The voice weakens.");
                WriteLineSlow("You are not strong enough.");
                Console.WriteLine();
            }

            WriteLineSlow("You step away from the ruins.");
            WriteLineSlow("The whispers of the catacombs surround you again.");
            Console.WriteLine();
        }

        static bool WhispersEncounter()
        {
            if (didWhispers == true)
            {
                WriteLineSlow("You listen again.");
                WriteLineSlow("But the voices no longer respond.");
                WriteLineSlow("They have already shown you the way.");
                Console.WriteLine();
                return foundHiddenPath;
            }

            WriteLineSlow("You stand still.");
            WriteLineSlow("The whispers surround you.");
            WriteLineSlow("They overlap.");
            WriteLineSlow("Too many to understand.");
            Console.WriteLine();
            WriteLineSlow("What do you do?");
            WriteLineSlow("1. Focus on one voice");
            WriteLineSlow("2. Step away");

            int firstChoice = AskChoice(1, 2);
            Console.WriteLine();

            if (firstChoice == 2)
            {
                WriteLineSlow("You step away from the whispers.");
                Console.WriteLine();
                return false;
            }

            bool firstSuccess = VisibleCheck("You focus on one voice.", wisdom, 11);
            Console.WriteLine();

            if (firstSuccess == false)
            {
                WriteLineSlow("The voices grow louder.");
                WriteLineSlow("You cannot separate them.");
                WriteLineSlow("You step back.");
                WriteLineSlow("But they remain.");
                WriteLineSlow("You can try again.");
                Console.WriteLine();
                return false;
            }

            WriteLineSlow("One voice becomes clear.");
            WriteLineSlow("\"Listen...\"");
            WriteLineSlow("The others fade.");
            Console.WriteLine();
            WriteLineSlow("What do you do?");
            WriteLineSlow("1. Speak to the voice");
            WriteLineSlow("2. Play your lyre");

            int secondChoice = AskChoice(1, 2);
            Console.WriteLine();

            if (secondChoice == 1)
            {
                didWhispers = true;
                foundHiddenPath = true;

                WriteLineSlow("You speak softly.");
                WriteLineSlow("The voice answers.");
                WriteLineSlow("\"There is a hidden way...\"");
                Console.WriteLine();
                WriteLineSlow("A path reveals itself.");
                WriteLineSlow("The whispers guide you forward.");
                WriteLineSlow("You do not need to search any longer.");
                Console.WriteLine();
                return true;
            }

            bool lyreSuccess = VisibleCheck("You play a quiet melody.", charisma, 9);
            Console.WriteLine();

            if (lyreSuccess == true)
            {
                didWhispers = true;
                foundHiddenPath = true;

                WriteLineSlow("The whispers stop.");
                WriteLineSlow("Completely.");
                Console.WriteLine();
                WriteLineSlow("For the first time—");
                WriteLineSlow("Silence.");
                Console.WriteLine();

                if (hasSongOfSilence == false)
                {
                    WriteLineSlow("A voice speaks clearly.");
                    WriteLineSlow("\"Take this.\"");
                    Console.WriteLine();
                    AddCollectible("Song of Silence");
                }
                else
                {
                    WriteLineSlow("You have already taken what this silence could give.");
                    Console.WriteLine();
                }

                WriteLineSlow("The path reveals itself.");
                WriteLineSlow("The whispers guide you forward.");
                WriteLineSlow("You do not need to search any longer.");
                Console.WriteLine();
                return true;
            }

            WriteLineSlow("The melody breaks.");
            WriteLineSlow("The whispers return.");
            WriteLineSlow("You lose the connection.");
            Console.WriteLine();
            return false;
        }

        static void ChaosGateEncounter()
        {
            if (didChaosGate == true)
            {
                WriteLineSlow("The gate is gone.");
                WriteLineSlow("As if it was never there.");
                Console.WriteLine();
                return;
            }

            

            WriteLineSlow("Something in the air feels diffirent.");
            WriteLineSlow("The walls bend in a way they should not.");
            WriteLineSlow("You notice a tear in reality itself.");
            Console.WriteLine();
            WriteLineSlow("A gate forms.");
            WriteLineSlow("Something ancient watches from beyond.");
            Console.WriteLine();
            WriteLineSlow("Chaos.");
            Console.WriteLine();
            WriteLineSlow("What do you do?");
            WriteLineSlow("1. Play your lyre");
            WriteLineSlow("2. Leave immediately");

            int choice = AskChoice(1, 2);
            Console.WriteLine();

            if (choice == 2)
            {
                WriteLineSlow("You step away.");
                WriteLineSlow("The gate closes behind you.");
                WriteLineSlow("As if it was never there.");
                Console.WriteLine();
                didChaosGate = true;
                return;
            }

            bool success = VisibleCheck("You play your lyre for Chaos.", charisma, 8);
            Console.WriteLine();

            if (success == true)
            {
                WriteLineSlow("The sound does not travel.");
                WriteLineSlow("It exists.");
                WriteLineSlow("Every note echoes in impossible directions.");
                WriteLineSlow("Chaos listens.");
                Console.WriteLine();
                WriteLineSlow("\"Take what was lost,\" a voice echoes.");
                Console.WriteLine();

                WriteLineSlow("Choose one:");
                WriteLineSlow("1. Shadow Cloak");
                WriteLineSlow("2. Spirit Charm");
                WriteLineSlow("3. Memory Fragment");

                int itemChoice = AskChoice(1, 3);
                Console.WriteLine();

                if (itemChoice == 1)
                {
                    if (hasShadowCloak == false)
                    {
                        AddCollectible("Shadow Cloak");
                    }
                    else
                    {
                        int oldDexterity = dexterity;
                        dexterity += 2;
                        WriteLineSlow("The object shifts.");
                        WriteLineSlow("Its power deepens.");
                        WriteLineSlow("Your connection grows stronger.");
                        WriteLineSlow($"Dexterity: {oldDexterity} -> {dexterity}");
                        Console.WriteLine();
                    }
                }

                if (itemChoice == 2)
                {
                    if (hasSpiritCharm == false)
                    {
                        AddCollectible("Spirit Charm");
                    }
                    else
                    {
                        int oldWisdom = wisdom;
                        wisdom += 2;
                        WriteLineSlow("The object shifts.");
                        WriteLineSlow("Its power deepens.");
                        WriteLineSlow("Your connection grows stronger.");
                        WriteLineSlow($"Wisdom: {oldWisdom} -> {wisdom}");
                        Console.WriteLine();
                    }
                }

                if (itemChoice == 3)
                {
                    if (hasMemoryFragment == false)
                    {
                        AddCollectible("Memory Fragment");
                    }
                    else
                    {
                        int oldIntelligence = intelligence;
                        intelligence += 2;
                        WriteLineSlow("The object shifts.");
                        WriteLineSlow("Its power deepens.");
                        WriteLineSlow("Your connection grows stronger.");
                        WriteLineSlow($"Intelligence: {oldIntelligence} -> {intelligence}");
                        Console.WriteLine();
                    }
                }
                didChaosGate = true;
            }
            else
            {
                WriteLineSlow("The sound fades into nothing.");
                WriteLineSlow("Chaos does not respond.");
                WriteLineSlow("The gate collapses.");
                Console.WriteLine();
                didChaosGate = true;
            }
        }

        // CONNECTION AFTER RIVER
        static bool gotBoatCollectible = false;
        static bool talkedToOdysseus = false;

        static void NextAfterRiver()
        {
            // Console.Clear();
            WriteLineSlow("The boat moves across the dark river.");
            WriteLineSlow("The waters shift beneath you.");
            WriteLineSlow("For now, you are safe.");
            Console.WriteLine();

            if (gotBoatCollectible == false)
            {
                gotBoatCollectible = true;
                WriteLineSlow("Something rests near your feet.");
                WriteLineSlow("A small object, forgotten.");
                Console.WriteLine();

                if (hasShadowCloak == false)
                {
                    AddCollectible("Shadow Cloak");
                }
                else if (hasSpiritCharm == false)
                {
                    AddCollectible("Spirit Charm");
                }
                else if (hasMemoryFragment == false)
                {
                    AddCollectible("Memory Fragment");
                }
            }

            if (boatType == "odysseus")
            {
                WriteLineSlow("Odysseus stands at the front of the ship.");
                WriteLineSlow("He glances back at you.");
                Console.WriteLine();
                WriteLineSlow("\"What brings a living man here?\"");
                Console.WriteLine();
                WriteLineSlow("\"My love is lost in this place.\"");
                WriteLineSlow("\"I will not leave without her.\"");
                Console.WriteLine();
                WriteLineSlow("Odysseus nods slowly.");
                WriteLineSlow("\"I have walked cursed lands before.\"");
                WriteLineSlow("\"If you are here, your journey will not be easy.\"");
                Console.WriteLine();
                WriteLineSlow("The ship slows beside a ruined dock of black stone.");
                WriteLineSlow("Beyond it, narrow steps descend into the forgotten catacombs.");
                Console.WriteLine();
                WriteLineSlow("\"We go no further,\" he says.");
                WriteLineSlow("\"This is where you must walk alone.\"");
                Console.WriteLine();

                talkedToOdysseus = true;
                Pause();
                nextArea = "catacombs";
            }

            if (boatType == "charon")
            {
                WriteLineSlow("Charon says nothing.");
                WriteLineSlow("The boat glides across the black water.");
                Console.WriteLine();
                WriteLineSlow("The boat slows.");
                Console.WriteLine();
                WriteLineSlow("Two paths lie ahead.");
                WriteLineSlow("One descends into ancient ruins.");
                WriteLineSlow("The other leads into a burning horizon.");
                Console.WriteLine();
                WriteLineSlow("Where do you go?");
                WriteLineSlow("1. Enter the ruins");
                WriteLineSlow("2. Walk toward the ash-covered fields");

                int destinationChoice = AskChoice(1, 2);
                Console.WriteLine();

                if (destinationChoice == 1)
                {
                    nextArea = "catacombs";
                }

                if (destinationChoice == 2)
                {
                    enteredAshByBoat = true;
                    nextArea = "ash";
                }
            }
        }
        // ASH
        static void AshArea()
        {
            Console.Clear();
            WriteLineSlow("// ASH");
            Console.WriteLine();

            if (enteredAshByBoat == true)
            {
                WriteLineSlow("You step off the boat.");
                Console.WriteLine();
                WriteLineSlow("Heat rolls across the land in waves.");
                WriteLineSlow("Ash drifts through the air like dead snow.");
                WriteLineSlow("The ground is cracked and dark, glowing faintly beneath your feet.");
                Console.WriteLine();
                WriteLineSlow("Nothing grows here.");
                WriteLineSlow("Nothing lives here.");
                Console.WriteLine();
                WriteLineSlow("And yet—something moves.");
            }
            else
            {
                WriteLineSlow("You leave the cold stone behind.");
                Console.WriteLine();
                WriteLineSlow("A wall of heat greets you at once.");
                WriteLineSlow("The air tastes of smoke and iron.");
                WriteLineSlow("Ash falls softly across the fields ahead.");
                Console.WriteLine();
                WriteLineSlow("The silence here is different.");
                WriteLineSlow("Not empty.");
                WriteLineSlow("Waiting.");
            }

            Console.WriteLine();

            bool stayInAsh = true;

            while (stayInAsh == true && gameEnded == false)
            {
                WriteLineSlow("What do you do?");
                WriteLineSlow("1. Walk toward the ruined battlefield");
                WriteLineSlow("2. Search the ash-covered ground");
                WriteLineSlow("3. Follow the moving figure in the distance");
                WriteLineSlow("4. Move toward the far gate");
                WriteLineSlow("5. Show status");

                int choice = AskChoice(1, 5);
                Console.WriteLine();

                if (choice == 1)
                {
                    AshBattlefieldEncounter();
                }

                if (choice == 2)
                {
                    AshSearchEncounter();
                }

                if (choice == 3)
                {
                    ZagreusEncounter();
                }

                if (choice == 4)
                {
                    bool passedAshTrial = AshEscapeTrial();

                    if (passedAshTrial == true)
                    {
                        crossedAsh = true;
                        nextArea = "oath";
                        stayInAsh = false;
                    }
                }

                if (choice == 5)
                {
                    ShowStatus();
                    Console.WriteLine();
                }
            }
        }
        static void AshBattlefieldEncounter()
        {
            if (didAshBattlefield == true)
            {
                WriteLineSlow("You return to the ruined battlefield.");
                WriteLineSlow("There is nothing more to see here.");
                Console.WriteLine();
                return;
            }

            didAshBattlefield = true;

            WriteLineSlow("You walk toward the broken weapons scattered across the field.");
            WriteLineSlow("Helmets, shields, shattered spears lie half-buried in ash.");
            Console.WriteLine();
            WriteLineSlow("Then you hear it.");
            Console.WriteLine();
            WriteLineSlow("Bones shifting.");
            Console.WriteLine();
            WriteLineSlow("Several skeletons pull themselves from the ground.");
            Console.WriteLine();

            WriteLineSlow("What do you do?");
            WriteLineSlow("1. Stand your ground");
            WriteLineSlow("2. Play your lyre");
            WriteLineSlow("3. Run");

            int choice = AskChoice(1, 3);
            Console.WriteLine();

            if (choice == 3)
            {
                WriteLineSlow("You turn and retreat before the skeletons fully surround you.");
                WriteLineSlow("The ash hides your steps.");
                WriteLineSlow("They do not follow.");
                Console.WriteLine();
            }

            if (choice == 2)
            {
                bool lyreSuccess = VisibleCheck("You play a sharp, commanding melody.", charisma, 10);
                Console.WriteLine();

                if (lyreSuccess == true)
                {
                    WriteLineSlow("The skeletons stop.");
                    WriteLineSlow("Their movements slow.");
                    WriteLineSlow("Then they collapse back into the ash.");
                    Console.WriteLine();
                }
                else
                {
                    WriteLineSlow("The melody is not enough.");
                    WriteLineSlow("The skeletons keep coming.");
                    Console.WriteLine();
                    AshSkeletonCombat();
                }
            }

            if (choice == 1)
            {
                AshSkeletonCombat();
            }
        }

        static void AshSkeletonCombat()
        {
            bool escaped = false;

            while (escaped == false && gameEnded == false)
            {
                bool dodge = VisibleCheck("You try to dodge.", dexterity, 11);
                Console.WriteLine();

                if (dodge == true)
                {
                    WriteLineSlow("You avoid the attack.");
                    Console.WriteLine();
                    WriteLineSlow("1. Flee");
                    WriteLineSlow("2. Stay");

                    int choice = AskChoice(1, 2);
                    Console.WriteLine();

                    if (choice == 1)
                    {
                        WriteLineSlow("You seize the moment and escape.");
                        WriteLineSlow("The enemy does not follow.");
                        Console.WriteLine();
                        escaped = true;
                    }

                    if (choice == 2)
                    {
                        WriteLineSlow("You remain, but the dead keep pressing forward.");
                        Console.WriteLine();
                    }
                }

                if (dodge == false)
                {
                    WriteLineSlow("You are too slow.");
                    WriteLineSlow("The skeletons strike.");
                    TakeDamage(5);
                    Console.WriteLine();

                    if (gameEnded == false)
                    {
                        WriteLineSlow("If you keep getting attacked, you may have to find other ways to get things done.");
                        Console.WriteLine();
                    }
                }
            }
        }

        static void AshSearchEncounter()
        {
            if (didAshSearch == true)
            {
                WriteLineSlow("You search the same ground again.");
                WriteLineSlow("But there is nothing more to learn here.");
                Console.WriteLine();
                return;
            }

            didAshSearch = true;

            WriteLineSlow("You kneel and brush the ash aside.");
            Console.WriteLine();
            WriteLineSlow("The ground beneath is warm.");
            WriteLineSlow("Too warm.");
            Console.WriteLine();
            WriteLineSlow("You pause.");
            WriteLineSlow("Something is wrong.");
            Console.WriteLine();

            bool success = VisibleCheck("You study the cracks carefully.", intelligence, 10);
            Console.WriteLine();

            if (success == true)
            {
                knowsAshPattern = true;
                WriteLineSlow("The cracks are not random.");
                WriteLineSlow("They form a pattern.");
                WriteLineSlow("A rhythm.");
                Console.WriteLine();
                WriteLineSlow("The ground erupts in bursts.");
                WriteLineSlow("Not all at once.");
                WriteLineSlow("Not everywhere.");
                Console.WriteLine();
                WriteLineSlow("There is a way through.");
                Console.WriteLine();
            }
            else
            {
                WriteLineSlow("You search for a while.");
                WriteLineSlow("But the ash reveals nothing.");
                WriteLineSlow("Only heat.");
                Console.WriteLine();
            }
        }

        static void ZagreusEncounter()
        {
            if (didZagreus == true)
            {
                WriteLineSlow("You look for the mysterious figure again.");
                WriteLineSlow("But he is nowhere to be found.");
                Console.WriteLine();
                return;
            }

            didZagreus = true;

            WriteLineSlow("You follow the figure through drifting ash.");
            WriteLineSlow("It moves quickly, almost casually.");
            Console.WriteLine();
            WriteLineSlow("Then it stops.");
            Console.WriteLine();
            WriteLineSlow("A young man turns toward you.");
            WriteLineSlow("He looks annoyed.");
            WriteLineSlow("But not surprised.");
            Console.WriteLine();
            WriteLineSlow("\"You're not supposed to be here,\" he says.");
            WriteLineSlow("\"...Actually, a lot of people here aren't supposed to be here.\"");
            Console.WriteLine();

            WriteLineSlow("What do you do?");
            WriteLineSlow("1. Ask who he is");
            WriteLineSlow("2. Tell him your story");
            WriteLineSlow("3. Ask about the path ahead");

            int choice = AskChoice(1, 3);
            Console.WriteLine();

            if (choice == 1)
            {
                WriteLineSlow("He folds his arms.");
                WriteLineSlow("\"Let's just say I know this place better than I should.\"");
                WriteLineSlow("\"And my father would definitely hate this conversation.\"");
                Console.WriteLine();
            }

            if (choice == 2)
            {
                WriteLineSlow("You tell him of your descent.");
                WriteLineSlow("Of your love.");
                WriteLineSlow("Of your promise not to leave alone.");
                Console.WriteLine();
                WriteLineSlow("He looks at you quietly.");
                WriteLineSlow("\"Right. So this is the dramatic kind of rescue.\"");
                WriteLineSlow("\"Fair enough.\"");
                Console.WriteLine();
            }

            if (choice == 3)
            {
                WriteLineSlow("He points through the haze.");
                WriteLineSlow("\"Keep going and you'll reach the Hall of Broken Oaths.\"");
                WriteLineSlow("\"If you're unlucky, you'll reach everything else first.\"");
                Console.WriteLine();
            }

            WriteLineSlow("He starts to walk away, then pauses.");
            WriteLineSlow("\"Father won’t like this,\" he says. \"Trust me.\"");
            Console.WriteLine();
            WriteLineSlow("Then he adds:");
            WriteLineSlow("\"Up. Up. Left. Right. Left. Right. Up. Up.\"");
            Console.WriteLine();

        }

        static bool AshEscapeTrial()
        {
            WriteLineSlow("You move toward the far end of the Ashen Fields.");
            WriteLineSlow("The ground trembles beneath your feet.");
            Console.WriteLine();
            WriteLineSlow("Cracks spread in every direction.");
            WriteLineSlow("Light bursts through them.");
            WriteLineSlow("Molten fire erupts into the air.");
            Console.WriteLine();
            WriteLineSlow("The entire field begins to collapse.");
            WriteLineSlow("There is only one way forward.");
            WriteLineSlow("You must move carefully.");
            WriteLineSlow("You must not turn back.");
            Console.WriteLine();
            if (knowsAshPattern == true)
            {
                WriteLineSlow("You remember the rhythm beneath the ash.");
                WriteLineSlow("The eruptions are not random.");
                Console.WriteLine();
            }

            string[] correctPattern = { "forward", "forward", "left", "right", "left", "right", "forward", "forward" };
            int currentStep = 0;

            while (currentStep < correctPattern.Length && gameEnded == false)
            {
                WriteLineSlow("Choose your movement:");
                WriteLineSlow("1. Move forward");
                WriteLineSlow("2. Move left");
                WriteLineSlow("3. Move right");
                WriteLineSlow("4. Turn back");

                int choice = AskChoice(1, 4);
                Console.WriteLine();

                string movement = "";

                if (choice == 1)
                {
                    movement = "forward";
                }

                if (choice == 2)
                {
                    movement = "left";
                }

                if (choice == 3)
                {
                    movement = "right";
                }

                if (choice == 4)
                {
                    WriteLineSlow("You turn instinctively—");
                    WriteLineSlow("Something stops you.");
                    WriteLineSlow("You cannot turn back!");
                    WriteLineSlow("Not now.");
                    WriteLineSlow("You did not move.");
                    Console.WriteLine();
                }

                if (choice != 4)
                {
                    if (movement != correctPattern[currentStep])
                    {
                        WriteLineSlow("You step in the wrong direction.");
                        WriteLineSlow("The ground shifts violently.");
                        WriteLineSlow("The pattern breaks.");
                        WriteLineSlow("You lose your sense of direction.");
                        Console.WriteLine();
                        WriteLineSlow("You find yourself back at the edge of the Ashen Fields.");
                        WriteLineSlow("The heat still rises ahead.");
                        WriteLineSlow("You must try again.");
                        Console.WriteLine();
                        return false;
                    }

                    bool success = VisibleCheck("You leap through the eruption.", dexterity, 10);
                    Console.WriteLine();

                    if (success == true)
                    {
                        WriteLineSlow("You move just before the ground erupts beneath you.");
                        Console.WriteLine();
                        currentStep += 1;
                    }
                    else
                    {
                        WriteLineSlow("You are too slow.");
                        WriteLineSlow("Flames burst upward.");
                        TakeDamage(3);
                        currentStep += 1;
                        Console.WriteLine();
                    }
                }
            }

            if (gameEnded == false)
            {
                WriteLineSlow("You take the final step.");
                WriteLineSlow("The ground stabilizes.");
                WriteLineSlow("The eruptions stop.");
                WriteLineSlow("The path is clear.");
                WriteLineSlow("You made it through.");
                Console.WriteLine();
                Pause();
                return true;
            }

            return false;
        }

        // OATH
        static void OathArea()
        {
            Console.Clear();
            WriteLineSlow("// OATH");
            Console.WriteLine();
            WriteLineSlow("You step into a vast hall.");
            Console.WriteLine();
            WriteLineSlow("Broken statues line the walls.");
            WriteLineSlow("Shadows stretch unnaturally across the ground.");
            Console.WriteLine();
            WriteLineSlow("Nothing moves.");
            WriteLineSlow("Yet everything feels watched.");
            Console.WriteLine();
            WriteLineSlow("At the far end stands a massive door.");
            WriteLineSlow("It does not open.");
            WriteLineSlow("Not yet.");
            Console.WriteLine();

            bool stayInOath = true;

            while (stayInOath == true && gameEnded == false)
            {
                WriteLineSlow("What do you do?");
                WriteLineSlow("1. Approach the ancient chained figure");
                WriteLineSlow("2. Walk toward the dark garden remains");
                WriteLineSlow("3. Follow the silent watcher in the shadows");
                WriteLineSlow("4. Approach the great door");
                WriteLineSlow("5. Show status");

                int choice = AskChoice(1, 5);
                Console.WriteLine();

                if (choice == 1)
                {
                    CronosEncounter();
                }

                if (choice == 2)
                {
                    GardenEncounter();
                }

                if (choice == 3)
                {
                    ThanatosEncounter();
                }

                if (choice == 4)
                {
                    bool openedDoor = GreatDoorEncounter();

                    if (openedDoor == true)
                    {
                        nextArea = "hades";
                        stayInOath = false;
                    }
                }

                if (choice == 5)
                {
                    ShowStatus();
                    Console.WriteLine();
                }
            }
        }

        static void CronosEncounter()
        {
            if (didCronos == true)
            {
                WriteLineSlow("You return to the chained figure.");
                WriteLineSlow("There is nothing more to learn here.");
                Console.WriteLine();
                return;
            }



            WriteLineSlow("You approach a massive, broken form.");
            WriteLineSlow("A being bound in time itself.");
            WriteLineSlow("Its presence feels ancient.");
            WriteLineSlow("Unstable.");
            Console.WriteLine();
            WriteLineSlow("A fragment of something far greater.");
            Console.WriteLine();

            knowsCronos = HiddenCheck(wisdom, 14);

            if (knowsCronos == true)
            {
                WriteLineSlow("You recognize the presence.");
                WriteLineSlow("Cronos.");
                WriteLineSlow("The devourer.");
                WriteLineSlow("The one who ruled before the gods.");
                WriteLineSlow("Now broken. Reduced.");
                Console.WriteLine();
            }
            else
            {
                WriteLineSlow("The being shifts slightly.");
                WriteLineSlow("\"I was… everything once.\"");
                WriteLineSlow("\"And now… I remain.\"");
                Console.WriteLine();
            }


            bool stayWithCronos = true;
            bool learnedBurden = false;

            while (stayWithCronos == true && gameEnded == false && learnedBurden == false)
            {
                WriteLineSlow("What do you do?");
                WriteLineSlow("1. Ask what happened to him");
                WriteLineSlow("2. Study the chains");
                WriteLineSlow("3. Touch the chains");
                WriteLineSlow("4. Play your lyre");
                WriteLineSlow("5. Leave");

                int choice = AskChoice(1, 5);
                Console.WriteLine();

                if (choice == 1)
                {
                    WriteLineSlow("\"They feared losing what they had.\"");
                    WriteLineSlow("\"So they took everything from me.\"");
                    WriteLineSlow("\"That is the cost of ruling.\"");
                    Console.WriteLine();
                    WriteLineSlow("What do you ask?");
                    WriteLineSlow("1. What do you still carry?");
                    WriteLineSlow("2. What was taken?");
                    WriteLineSlow("3. Leave");

                    int subChoice = AskChoice(1, 3);
                    Console.WriteLine();

                    if (subChoice == 1)
                    {
                        WriteLineSlow("\"Everything.\"");
                        WriteLineSlow("\"That is what a king learns too late.\"");
                        Console.WriteLine();
                        WriteLineSlow("The meaning becomes clear.");
                        WriteLineSlow("\"Burden\"");
                        Console.WriteLine();
                        gotBurdenWord = true;
                        learnedBurden = true;
                        didCronos = true;
                    }

                    if (subChoice == 2)
                    {
                        WriteLineSlow("\"More than power.\"");
                        WriteLineSlow("\"More than rule.\"");
                        WriteLineSlow("\"Everything that could not be kept.\"");
                        Console.WriteLine();
                    }
                }

                if (choice == 2)
                {
                    bool success = VisibleCheck("You study the chains.", intelligence, 11);
                    Console.WriteLine();

                    if (success == true)
                    {
                        WriteLineSlow("These chains are not force.");
                        WriteLineSlow("They are consequence.");
                        WriteLineSlow("Everything he ruled—");
                        WriteLineSlow("Now rests on him.");
                        Console.WriteLine();
                        WriteLineSlow("The meaning becomes clear.");
                        WriteLineSlow("\"Burden\"");
                        Console.WriteLine();
                        gotBurdenWord = true;
                        learnedBurden = true;
                        didCronos = true;
                    }
                    else
                    {
                        WriteLineSlow("You cannot fully understand what binds him.");
                        Console.WriteLine();
                    }
                }

                if (choice == 3)
                {
                    bool success = VisibleCheck("You touch the chains.", constitution, 12);
                    Console.WriteLine();

                    if (success == true)
                    {
                        WriteLineSlow("For a moment—you feel it.");
                        WriteLineSlow("The weight.");
                        WriteLineSlow("Not of chains—");
                        WriteLineSlow("Of everything he once held.");
                        Console.WriteLine();
                        WriteLineSlow("The meaning becomes clear.");
                        WriteLineSlow("\"Burden\"");
                        Console.WriteLine();
                        gotBurdenWord = true;
                        learnedBurden = true;
                        didCronos = true;
                    }
                    else
                    {
                        WriteLineSlow("The pressure rushes through you.");
                        TakeDamage(4);
                        Console.WriteLine();
                    }
                }

                if (choice == 4)
                {
                    WriteLineSlow("You play.");
                    WriteLineSlow("The sound does not echo.");
                    WriteLineSlow("It sinks into him.");
                    Console.WriteLine();
                    WriteLineSlow("\"To rule… is to carry everything.\"");
                    WriteLineSlow("\"Even what breaks you.\"");
                    WriteLineSlow("\"Even what you cannot keep.\"");
                    Console.WriteLine();
                    WriteLineSlow("The meaning becomes clear.");
                    WriteLineSlow("\"Burden\"");
                    Console.WriteLine();
                    gotBurdenWord = true;
                    learnedBurden = true;
                    didCronos = true;
                }

                if (choice == 5)
                {
                    stayWithCronos = false;
                }
            }
        }


        static void GardenEncounter()
        {
            if (didGarden == true)
            {
                WriteLineSlow("You return to the garden.");
                WriteLineSlow("Nothing grows here anymore.");
                Console.WriteLine();
                return;
            }



            WriteLineSlow("You step into what was once a garden.");
            WriteLineSlow("The soil is dry.");
            WriteLineSlow("The flowers are gone.");
            Console.WriteLine();
            WriteLineSlow("But something lingers.");
            WriteLineSlow("A faint presence.");
            Console.WriteLine();

            knowsGardenTruth = HiddenCheck(wisdom, 14);

            if (knowsGardenTruth == true)
            {
                WriteLineSlow("This place is not dead.");
                WriteLineSlow("It is waiting.");
                WriteLineSlow("She comes and goes.");
                WriteLineSlow("And when she leaves—");
                WriteLineSlow("Everything follows.");
                Console.WriteLine();
            }
            else
            {
                WriteLineSlow("A faint voice speaks:");
                WriteLineSlow("\"She is not gone.\"");
                WriteLineSlow("\"She simply isn't here.\"");
                Console.WriteLine();
            }

            bool stayInGarden = true;
            bool learnedLove = false;

            while (stayInGarden == true && gameEnded == false && learnedLove == false)
            {
                WriteLineSlow("What do you do?");
                WriteLineSlow("1. Ask about the garden");
                WriteLineSlow("2. Ask about the king");
                WriteLineSlow("3. Observe the place");
                WriteLineSlow("4. Play your lyre");
                WriteLineSlow("5. Leave");

                int choice = AskChoice(1, 5);
                Console.WriteLine();

                if (choice == 1)
                {
                    WriteLineSlow("\"It blooms when she walks here.\"");
                    WriteLineSlow("\"And fades when she leaves.\"");
                    Console.WriteLine();
                    WriteLineSlow("What do you ask?");
                    WriteLineSlow("1. Why does it wait?");
                    WriteLineSlow("2. Who is it waiting for?");
                    WriteLineSlow("3. Leave");

                    int subChoice = AskChoice(1, 3);
                    Console.WriteLine();

                    if (subChoice == 1)
                    {
                        WriteLineSlow("\"Because some places remember.\"");
                        Console.WriteLine();
                    }

                    if (subChoice == 2)
                    {
                        WriteLineSlow("\"For the one he loved before he was only a king.\"");
                        Console.WriteLine();
                        WriteLineSlow("You understand now.");
                        WriteLineSlow("\"Love\"");
                        Console.WriteLine();
                        gotLoveWord = true;
                        learnedLove = true;
                        didGarden = true;
                    }
                }

                if (choice == 2)
                {
                    WriteLineSlow("\"He changes when she returns.\"");
                    WriteLineSlow("\"Not a ruler then…\"");
                    WriteLineSlow("\"Just a man.\"");
                    Console.WriteLine();
                    WriteLineSlow("You understand now.");
                    WriteLineSlow("\"Love\"");
                    Console.WriteLine();
                    gotLoveWord = true;
                    learnedLove = true;
                    didGarden = true;
                }

                if (choice == 3)
                {
                    bool success = VisibleCheck("You study the garden.", intelligence, 10);
                    Console.WriteLine();

                    if (success == true)
                    {
                        WriteLineSlow("This place is not lifeless.");
                        WriteLineSlow("It is waiting.");
                        WriteLineSlow("For someone.");
                        Console.WriteLine();
                    }
                    else
                    {
                        WriteLineSlow("The garden reveals little more.");
                        Console.WriteLine();
                    }
                }

                if (choice == 4)
                {
                    WriteLineSlow("You play.");
                    WriteLineSlow("For a moment—the garden lives again.");
                    WriteLineSlow("Warmth returns.");
                    WriteLineSlow("Then fades.");
                    Console.WriteLine();
                    WriteLineSlow("\"He loved… before he ruled.\"");
                    Console.WriteLine();
                    WriteLineSlow("You understand now.");
                    WriteLineSlow("\"Love\"");
                    Console.WriteLine();
                    gotLoveWord = true;
                    learnedLove = true;
                    didGarden = true;
                }

                if (choice == 5)
                {
                    stayInGarden = false;
                }
            }
        }

        static void ThanatosEncounter()
        {
            if (didThanatos == true)
            {
                WriteLineSlow("You return to the shadows.");
                WriteLineSlow("The watcher remains silent.");
                Console.WriteLine();
                return;
            }



            WriteLineSlow("A figure stands in the shadows.");
            Console.WriteLine();
            WriteLineSlow("Still.");
            WriteLineSlow("Silent.");
            WriteLineSlow("Watching.");
            Console.WriteLine();

            knowsThanatos = HiddenCheck(wisdom, 14);

            if (knowsThanatos == true)
            {
                WriteLineSlow("You recognize him.");
                WriteLineSlow("Thanatos.");
                WriteLineSlow("The one who reaps the dead.");
                Console.WriteLine();
            }
            else
            {
                WriteLineSlow("He speaks first.");
                WriteLineSlow("\"I know you.\"");
                WriteLineSlow("\"I know why you came.\"");
                Console.WriteLine();
            }

            bool stayWithThanatos = true;
            bool learnedLoss = false;

            while (stayWithThanatos == true && gameEnded == false && learnedLoss == false)
            {
                WriteLineSlow("What do you do?");
                WriteLineSlow("1. Ask what he means");
                WriteLineSlow("2. Ask about Eurydice");
                WriteLineSlow("3. Step closer");
                WriteLineSlow("4. Play your lyre");
                WriteLineSlow("5. Leave");

                int choice = AskChoice(1, 5);
                Console.WriteLine();

                if (choice == 1)
                {
                    WriteLineSlow("\"I was there.\"");
                    WriteLineSlow("\"When her life ended.\"");
                    WriteLineSlow("\"I am always there.\"");
                    Console.WriteLine();
                    WriteLineSlow("What do you ask?");
                    WriteLineSlow("1. Did you take her?");
                    WriteLineSlow("2. Can death be stopped?");
                    WriteLineSlow("3. Leave");

                    int subChoice = AskChoice(1, 3);
                    Console.WriteLine();

                    if (subChoice == 1)
                    {
                        WriteLineSlow("\"I reap what must be reaped.\"");
                        WriteLineSlow("\"I do not choose. I arrive.\"");
                        Console.WriteLine();
                    }

                    if (subChoice == 2)
                    {
                        WriteLineSlow("\"No.\"");
                        WriteLineSlow("\"Death takes the ones you cherish.\"");
                        WriteLineSlow("\"And once it does, it does not yield them.\"");
                        Console.WriteLine();
                        WriteLineSlow("You understand.");
                        WriteLineSlow("\"Loss\"");
                        Console.WriteLine();
                        gotLossWord = true;
                        learnedLoss = true;
                        didThanatos = true;
                    }
                }

                if (choice == 2)
                {
                    WriteLineSlow("He looks at you.");
                    WriteLineSlow("\"I did not choose her.\"");
                    WriteLineSlow("\"I only reap what must be reaped.\"");
                    WriteLineSlow("\"That is what death is.\"");
                    Console.WriteLine();
                    WriteLineSlow("You understand.");
                    WriteLineSlow("\"Loss\"");
                    Console.WriteLine();
                    Console.WriteLine();
                    gotLossWord = true;
                    learnedLoss = true;
                    didThanatos = true;
                }

                if (choice == 3)
                {
                    int betterValue = dexterity;

                    if (constitution > dexterity)
                    {
                        betterValue = constitution;
                    }

                    bool success = VisibleCheck("You step closer.", betterValue, 12);
                    Console.WriteLine();

                    if (success == true)
                    {
                        WriteLineSlow("You stand your ground.");
                        WriteLineSlow("He does not move.");
                        WriteLineSlow("You feel the stillness of ending.");
                        Console.WriteLine();
                    }
                    else
                    {
                        WriteLineSlow("Your strength falters.");
                        TakeDamage(4);
                        Console.WriteLine();
                    }
                }

                if (choice == 4)
                {
                    WriteLineSlow("You play.");
                    WriteLineSlow("The sound is soft.");
                    WriteLineSlow("Final.");
                    Console.WriteLine();
                    WriteLineSlow("He closes his eyes.");
                    WriteLineSlow("\"All things end.\"");
                    WriteLineSlow("\"All things are reaped in time.\"");
                    WriteLineSlow("\"And when death takes what you love—\"");
                    WriteLineSlow("\"It leaves only loss behind.\"");
                    Console.WriteLine();
                    WriteLineSlow("You understand.");
                    WriteLineSlow("\"Loss\"");
                    Console.WriteLine();
                    gotLossWord = true;
                    learnedLoss = true;
                    didThanatos = true;
                }

                if (choice == 5)
                {
                    stayWithThanatos = false;
                }
            }
        }

        static bool GreatDoorEncounter()
        {
            WriteLineSlow("The great door stands before you.");
            WriteLineSlow("Three empty spaces appear.");
            WriteLineSlow("You feel their meaning.");
            WriteLineSlow("Not spoken—");
            WriteLineSlow("Remembered.");
            Console.WriteLine();

            if (gotBurdenWord == false || gotLoveWord == false || gotLossWord == false)
            {
                WriteLineSlow("You do not yet know all the words.");
                Console.WriteLine();
                return false;
            }

            Console.Write("Enter the first word: ");
            string firstWord = Console.ReadLine().Trim().ToLower();

            Console.Write("Enter the second word: ");
            string secondWord = Console.ReadLine().Trim().ToLower();

            Console.Write("Enter the third word: ");
            string thirdWord = Console.ReadLine().Trim().ToLower();

            Console.WriteLine();

            if (firstWord == "burden" && secondWord == "love" && thirdWord == "loss")
            {
                WriteLineSlow("The words carve themselves into the stone.");
                Console.WriteLine();
                WriteLineSlow("Burden.");
                WriteLineSlow("Love.");
                WriteLineSlow("Loss.");
                Console.WriteLine();
                WriteLineSlow("Heavy is the heart that carries everything.");
                WriteLineSlow("Even heavier—");
                WriteLineSlow("The heart that dares to love.");
                WriteLineSlow("And in the end—");
                WriteLineSlow("Even kings cannot escape loss.");
                Console.WriteLine();
                WriteLineSlow("The door trembles.");
                WriteLineSlow("Then slowly opens.");
                Console.WriteLine();
                WriteLineSlow("Darkness waits beyond.");
                WriteLineSlow("You feel its presence.");
                Console.WriteLine();
                WriteLineSlow("The King of Shades awaits.");
                Console.WriteLine();
                Pause();
                return true;
            }

            WriteLineSlow("Nothing happens.");
            WriteLineSlow("The door remains silent.");
            Console.WriteLine();
            return false;
        }
        // HADES
        static void HadesArea()
        {
            HypnosEncounter();

            if (gameEnded == false)
            {
                HadesThroneRoom();
            }
        }

        static void HypnosEncounter()
        {
            if (metHypnos == true)
            {
                return;
            }

            metHypnos = true;

            Console.Clear();
            WriteLineSlow("// HADES");
            Console.WriteLine();
            WriteLineSlow("You step into a vast chamber.");
            WriteLineSlow("Torches burn without flame.");
            Console.WriteLine();
            WriteLineSlow("At the entrance—");
            Console.WriteLine();
            WriteLineSlow("A figure leans lazily against a pillar.");
            WriteLineSlow("He looks at you.");
            WriteLineSlow("Yawns.");
            Console.WriteLine();
            WriteLineSlow("\"...Oh. Hey.\"");
            Console.WriteLine();
            WriteLineSlow("\"What's your name?\"");
            WriteLineSlow("1. Tell your name");
            WriteLineSlow("2. Stay silent");
            WriteLineSlow("3. Ask who he is");

            int firstChoice = AskChoice(1, 3);
            Console.WriteLine();

            if (firstChoice == 1)
            {
                WriteLineSlow("\"Orpheus. Huh. Nice name.\"");
                Console.WriteLine();
            }

            if (firstChoice == 2)
            {
                WriteLineSlow("\"Quiet type. Sure.\"");
                Console.WriteLine();
            }

            if (firstChoice == 3)
            {
                WriteLineSlow("\"Oh. Right. Hypnos.\"");
                WriteLineSlow("\"Sorry, I usually have more time for this.\"");
                Console.WriteLine();
            }

            WriteLineSlow("\"So... how'd you die?\"");
            WriteLineSlow("1. I didn't.");
            WriteLineSlow("2. I don't remember.");
            WriteLineSlow("3. Does it matter?");

            int secondChoice = AskChoice(1, 3);
            Console.WriteLine();

            if (secondChoice == 1)
            {
                WriteLineSlow("\"...Wait.\"");
                Console.WriteLine();
            }

            if (secondChoice == 2)
            {
                WriteLineSlow("\"That seems important.\"");
                WriteLineSlow("\"...Wait.\"");
                Console.WriteLine();
            }

            if (secondChoice == 3)
            {
                WriteLineSlow("\"Usually? Not really.\"");
                WriteLineSlow("\"...Wait.\"");
                Console.WriteLine();
            }

            WriteLineSlow("He leans forward.");
            WriteLineSlow("\"You're not dead.\"");
            WriteLineSlow("\"...You're really not dead.\"");
            Console.WriteLine();
            WriteLineSlow("He straightens up.");
            WriteLineSlow("\"Oh—he's gonna love this.\"");
            Console.WriteLine();
            WriteLineSlow("He steps aside.");
            WriteLineSlow("\"Go on. He's waiting.\"");
            Console.WriteLine();

            Pause();
        }

        static void HadesThroneRoom()
        {
            Console.Clear();
            WriteLineSlow("The chamber opens.");
            Console.WriteLine();
            WriteLineSlow("A throne of dark stone stands ahead.");
            Console.WriteLine();
            WriteLineSlow("A figure sits upon it.");
            Console.WriteLine();
            WriteLineSlow("Still.");
            WriteLineSlow("Silent.");
            WriteLineSlow("Watching.");
            Console.WriteLine();
            WriteLineSlow("\"I knew you would come.\"");
            Console.WriteLine();
            WriteLineSlow("The voice is heavy.");
            WriteLineSlow("Ancient.");
            Console.WriteLine();
            WriteLineSlow("\"You were seen the moment you entered my domain.\"");
            WriteLineSlow("\"I always knew you were here.\"");
            Console.WriteLine();
            WriteLineSlow("\"You seek something.\"");
            WriteLineSlow("\"Speak.\"");
            Console.WriteLine();

            hadesFavor = 0;

            usedBonusCerberus = false;
            usedBonusCharon = false;
            usedBonusOdysseus = false;
            usedBonusPrometheus = false;
            usedBonusThanatos = false;
            usedBonusGarden = false;
            usedBonusCronos = false;

            int rounds = 0;

            while (gameEnded == false && rounds < 4)
            {
                WriteLineSlow("What do you do?");
                WriteLineSlow("1. Speak calmly");
                WriteLineSlow("2. Challenge him");
                WriteLineSlow("3. Tell your story");
                WriteLineSlow("4. Speak of what you have done");

                int choice = AskChoice(1, 4);
                Console.WriteLine();

                if (choice == 1)
                {
                    HadesCalmPath();
                }

                if (choice == 2)
                {
                    HadesChallengePath();
                }

                if (choice == 3)
                {
                    HadesStoryPath();
                }

                if (choice == 4)
                {
                    HadesJourneyBonusPath();
                }

                rounds += 1;

                if (currentHp <= 0)
                {
                    gameEnded = true;
                    nextArea = "end";
                }

                if (gameEnded == false)
                {
                    WriteLineSlow("Hades studies you in silence.");
                    WriteLineSlow($"Favor with Hades: {hadesFavor}");
                    Console.WriteLine();
                }
            }

            if (gameEnded == false)
            {
                if (hadesFavor >= 5)
                {
                    FinalSongAndRequest();
                }
                else
                {
                    WriteLineSlow("\"You have come far,\" Hades says.");
                    WriteLineSlow("\"But you are not enough.\"");
                    Console.WriteLine();
                    WriteLineSlow("The hall closes around you.");
                    WriteLineSlow("You never leave.");
                    Console.WriteLine();
                    gameEnded = true;
                    nextArea = "end";
                }
            }
        }

        static void HadesCalmPath()
        {
            WriteLineSlow("Choose your words:");
            WriteLineSlow("1. Speak of love");
            WriteLineSlow("2. Speak of loss");
            WriteLineSlow("3. Speak of power");

            int choice = AskChoice(1, 3);
            Console.WriteLine();

            if (choice == 1)
            {
                WriteLineSlow("\"You know what it means to love and wait.\"");
                WriteLineSlow("\"You know what it means for a kingdom to dim when one person is gone.\"");
                WriteLineSlow("\"You know this better than I do.\"");
                Console.WriteLine();
                WriteLineSlow("Hades leans forward.");
                WriteLineSlow("\"You speak boldly of love.\"");
                WriteLineSlow("\"Love is not enough to defy this place.\"");
                WriteLineSlow("\"But it is enough to drag a man here.\"");
                Console.WriteLine();

                bool success = VisibleCheck("You speak carefully of love.", charisma, 9);
                Console.WriteLine();

                if (success == true)
                {
                    hadesFavor += 2;
                }
                else
                {
                    hadesFavor += 1;
                }
            }

            if (choice == 2)
            {
                WriteLineSlow("\"You know what it is to lose someone and still keep walking.\"");
                WriteLineSlow("\"You know what it is to carry grief farther than reason should allow.\"");
                Console.WriteLine();
                WriteLineSlow("His voice lowers.");
                WriteLineSlow("\"Then you understand why nothing is returned lightly.\"");
                Console.WriteLine();

                bool success = VisibleCheck("You speak of loss.", wisdom, 11);
                Console.WriteLine();

                if (success == true)
                {
                    hadesFavor += 2;
                }
                else
                {
                    hadesFavor += 1;
                }
            }

            if (choice == 3)
            {
                WriteLineSlow("\"You rule everything here. So you can give anything.\"");
                Console.WriteLine();
                WriteLineSlow("His gaze sharpens.");
                WriteLineSlow("\"That is the language of men who understand neither rule nor burden.\"");
                Console.WriteLine();

                TakeDamage(4);
                hadesFavor -= 1;
            }
        }

        static void HadesChallengePath()
        {
            WriteLineSlow("Choose your words:");
            WriteLineSlow("1. Demand Eurydice back");
            WriteLineSlow("2. Stand your ground silently");
            WriteLineSlow("3. Mock him");

            int choice = AskChoice(1, 3);
            Console.WriteLine();

            if (choice == 1)
            {
                WriteLineSlow("\"I came here for Eurydice.\"");
                WriteLineSlow("\"I crossed your domain.\"");
                WriteLineSlow("\"I am leaving with her.\"");
                Console.WriteLine();
                WriteLineSlow("Hades watches you without blinking.");
                Console.WriteLine();

                bool success = VisibleCheck("You stand against the King of Shades.", strength, 13);
                Console.WriteLine();

                if (success == true)
                {
                    WriteLineSlow("\"Bold,\" he says.");
                    WriteLineSlow("\"Or foolish.\"");
                    WriteLineSlow("\"Sometimes there is little difference.\"");
                    Console.WriteLine();
                    hadesFavor += 2;
                }
                else
                {
                    WriteLineSlow("\"Bold words are not enough.\"");
                    Console.WriteLine();
                }
            }

            if (choice == 2)
            {
                WriteLineSlow("You do not bow.");
                WriteLineSlow("You do not step back.");
                Console.WriteLine();
                WriteLineSlow("The room grows heavier.");
                Console.WriteLine();

                bool success = VisibleCheck("You refuse to bend.", strength, 13);
                Console.WriteLine();

                if (success == true)
                {
                    WriteLineSlow("\"You do not break easily.\"");
                    Console.WriteLine();
                    hadesFavor += 1;
                }
                else
                {
                    WriteLineSlow("\"Not enough.\"");
                    Console.WriteLine();
                }
            }

            if (choice == 3)
            {
                WriteLineSlow("You push too far.");
                Console.WriteLine();
                WriteLineSlow("The weight of his gaze crushes you.");
                Console.WriteLine();

                TakeDamage(4);
                hadesFavor -= 1;
            }
        }

        static void HadesStoryPath()
        {
            WriteLineSlow("Choose your words:");
            WriteLineSlow("1. Speak of your journey");
            WriteLineSlow("2. Speak of your love");
            WriteLineSlow("3. Speak of fate");

            int choice = AskChoice(1, 3);
            Console.WriteLine();

            if (choice == 1)
            {
                WriteLineSlow("Choose:");
                WriteLineSlow("1. Speak of Cerberus");
                WriteLineSlow("2. Speak of the river");
                WriteLineSlow("3. Speak of your own greatness");

                int subChoice = AskChoice(1, 3);
                Console.WriteLine();

                if (subChoice == 1)
                {
                    WriteLineSlow("\"I passed the guardian at your gates and did not strike him.\"");
                    WriteLineSlow("\"I earned passage without cruelty.\"");
                    Console.WriteLine();
                    WriteLineSlow("\"You passed my hound,\" Hades says.");
                    WriteLineSlow("\"It does not yield to the unworthy.\"");
                    Console.WriteLine();

                    if (didCerberus == true)
                    {
                        hadesFavor += 2;
                    }
                    else
                    {
                        hadesFavor += 1;
                    }
                }

                if (subChoice == 2)
                {
                    WriteLineSlow("\"I crossed the river of the dead and kept walking.\"");
                    WriteLineSlow("\"Even then, I did not turn back.\"");
                    Console.WriteLine();
                    WriteLineSlow("\"Then you were allowed,\" Hades says.");
                    WriteLineSlow("\"That was not an accident.\"");
                    Console.WriteLine();

                    if (boatType == "charon" || boatType == "odysseus")
                    {
                        hadesFavor += 2;
                    }
                    else
                    {
                        hadesFavor += 1;
                    }
                }

                if (subChoice == 3)
                {
                    WriteLineSlow("\"I alone was strong enough to come this far.\"");
                    Console.WriteLine();
                    WriteLineSlow("\"Then you have learned nothing,\" Hades says.");
                    Console.WriteLine();
                    TakeDamage(4);
                    hadesFavor -= 1;
                }
            }

            if (choice == 2)
            {
                WriteLineSlow("Choose:");
                WriteLineSlow("1. Speak of her voice");
                WriteLineSlow("2. Speak of her loss");
                WriteLineSlow("3. Speak of yourself");

                int subChoice = AskChoice(1, 3);
                Console.WriteLine();

                if (subChoice == 1)
                {
                    WriteLineSlow("\"I remember her voice more clearly than I remember the sun.\"");
                    Console.WriteLine();

                    bool success = VisibleCheck("You speak of Eurydice.", charisma, 10);
                    Console.WriteLine();

                    if (success == true)
                    {
                        hadesFavor += 2;
                    }
                    else
                    {
                        hadesFavor += 1;
                    }
                }

                if (subChoice == 2)
                {
                    WriteLineSlow("\"I know what it is to lose what I love and still keep walking.\"");
                    Console.WriteLine();

                    bool success = VisibleCheck("You speak of grief.", wisdom, 10);
                    Console.WriteLine();

                    if (success == true)
                    {
                        hadesFavor += 2;
                    }
                    else
                    {
                        hadesFavor += 1;
                    }
                }

                if (subChoice == 3)
                {
                    WriteLineSlow("\"This is not about me.\"");
                    Console.WriteLine();
                    WriteLineSlow("Hades gives you an overwhelming look.");
                    Console.WriteLine();

                    TakeDamage(4);
                }
            }

            if (choice == 3)
            {
                WriteLineSlow("Choose:");
                WriteLineSlow("1. Speak of choice");
                WriteLineSlow("2. Speak of burden");
                WriteLineSlow("3. Speak of doom");

                int subChoice = AskChoice(1, 3);
                Console.WriteLine();

                if (subChoice == 1)
                {
                    WriteLineSlow("\"Even here, every step was still a choice.\"");
                    Console.WriteLine();

                    bool success = VisibleCheck("You speak of the path you chose.", intelligence, 10);
                    Console.WriteLine();

                    if (success == true)
                    {
                        hadesFavor += 1;
                    }
                }

                if (subChoice == 2)
                {
                    WriteLineSlow("\"A king carries more than a crown.\"");
                    WriteLineSlow("\"He carries what cannot be dropped.\"");
                    Console.WriteLine();

                    bool success = VisibleCheck("You speak of burden.", intelligence, 10);
                    Console.WriteLine();

                    if (success == true)
                    {
                        hadesFavor += 2;
                    }
                    else
                    {
                        hadesFavor += 1;
                    }
                }

                if (subChoice == 3)
                {
                    WriteLineSlow("\"Then you came here already defeated,\" Hades says.");
                    Console.WriteLine();
                    TakeDamage(4);
                }
            }
        }

        static void HadesJourneyBonusPath()
        {
            List<string> bonusOptions = new List<string>();

            if (didCerberus == true && usedBonusCerberus == false)
            {
                bonusOptions.Add("cerberus");
            }

            if (boatType == "charon" && usedBonusCharon == false)
            {
                bonusOptions.Add("charon");
            }

            if (talkedToOdysseus == true && usedBonusOdysseus == false)
            {
                bonusOptions.Add("odysseus");
            }

            if (helpedPrometheus == true && usedBonusPrometheus == false)
            {
                bonusOptions.Add("prometheus");
            }

            if (gotLossWord == true && usedBonusThanatos == false)
            {
                bonusOptions.Add("thanatos");
            }

            if (gotLoveWord == true && usedBonusGarden == false)
            {
                bonusOptions.Add("garden");
            }

            if ((gotBurdenWord == true || knowsCronos == true) && usedBonusCronos == false)
            {
                bonusOptions.Add("cronos");
            }

            if (bonusOptions.Count == 0)
            {
                WriteLineSlow("You have nothing more to add.");
                Console.WriteLine();
                return;
            }

            WriteLineSlow("What will you speak of?");

            int i = 0;
            while (i < bonusOptions.Count)
            {
                WriteLineSlow($"{i + 1}. {bonusOptions[i]}");
                i += 1;
            }

            int choice = AskChoice(1, bonusOptions.Count);
            string selected = bonusOptions[choice - 1];
            Console.WriteLine();

            if (selected == "cerberus")
            {
                usedBonusCerberus = true;
                WriteLineSlow("\"I soothed the beast at your gate rather than striking him.\"");
                Console.WriteLine();
                WriteLineSlow("\"You passed my hound,\" Hades says.");
                WriteLineSlow("\"It does not yield to the unworthy.\"");
                Console.WriteLine();
                hadesFavor += 1;
            }

            if (selected == "charon")
            {
                usedBonusCharon = true;
                WriteLineSlow("\"I crossed because your world let me pass.\"");
                Console.WriteLine();
                WriteLineSlow("\"Nothing crosses my river without cause.\"");
                Console.WriteLine();
                hadesFavor += 1;
            }

            if (selected == "odysseus")
            {
                usedBonusOdysseus = true;
                WriteLineSlow("\"Even the living on dark waters heard my song.\"");
                Console.WriteLine();
                WriteLineSlow("\"And you still came here,\" Hades says.");
                WriteLineSlow("\"Interesting.\"");
                Console.WriteLine();
                hadesFavor += 1;
            }

            if (selected == "prometheus")
            {
                usedBonusPrometheus = true;
                WriteLineSlow("\"I helped one even the gods left chained.\"");
                Console.WriteLine();
                WriteLineSlow("\"You chose to help.\"");
                WriteLineSlow("\"That is... noted.\"");
                Console.WriteLine();
                hadesFavor += 1;
            }

            if (selected == "thanatos")
            {
                usedBonusThanatos = true;
                WriteLineSlow("\"I spoke with death and still kept walking.\"");
                Console.WriteLine();
                WriteLineSlow("\"That is not common,\" Hades says.");
                Console.WriteLine();
                hadesFavor += 1;
            }

            if (selected == "garden")
            {
                usedBonusGarden = true;
                WriteLineSlow("\"I saw what remains when she is absent.\"");
                Console.WriteLine();
                WriteLineSlow("\"...Then you saw enough.\"");
                Console.WriteLine();
                hadesFavor += 1;
            }

            if (selected == "cronos")
            {
                usedBonusCronos = true;
                WriteLineSlow("\"I saw what burden does to kings.\"");
                Console.WriteLine();
                WriteLineSlow("\"And yet you kept speaking.\"");
                Console.WriteLine();
                hadesFavor += 1;
            }
        }

        static void FinalSongAndRequest()
        {
            Console.Clear();
            WriteLineSlow("Hades watches you in silence.");
            WriteLineSlow("\"...You have walked far.\"");
            WriteLineSlow("\"But words are nothing.\"");
            WriteLineSlow("\"Show me.\"");
            Console.WriteLine();
            WriteLineSlow("You raise your lyre.");
            WriteLineSlow("Your hands tremble—");
            WriteLineSlow("But you begin.");
            Console.WriteLine();

            bool songSuccess = VisibleCheck("You begin your final song.", charisma, 5);
            Console.WriteLine();

            if (songSuccess == false)
            {
                WriteLineSlow("Your voice falters for a moment.");
                WriteLineSlow("The sound breaks—");
                WriteLineSlow("But you do not stop.");
                WriteLineSlow("You continue.");
                Console.WriteLine();
            }

            WriteLineSlow("A song of descent—");
            WriteLineSlow("Of a man who walked into the dark");
            WriteLineSlow("And did not turn away.");
            Console.WriteLine();

            if (didCerberus == true)
            {
                WriteLineSlow("Of the guardian with three heads—");
                WriteLineSlow("Who did not strike,");
                WriteLineSlow("But listened.");
                Console.WriteLine();
            }

            if (boatType == "charon")
            {
                WriteLineSlow("Of the silent ferryman—");
                WriteLineSlow("Who watched,");
                WriteLineSlow("And let him pass.");
                Console.WriteLine();
            }

            if (talkedToOdysseus == true)
            {
                WriteLineSlow("Of men still breathing in a world of the dead—");
                WriteLineSlow("Who carried him across dark water.");
                Console.WriteLine();
            }

            if (helpedPrometheus == true)
            {
                WriteLineSlow("Of a titan bound—");
                WriteLineSlow("Who still endured,");
                WriteLineSlow("And was not left alone.");
                Console.WriteLine();
            }

            if (hasVitalCore == true)
            {
                WriteLineSlow("Of a fading soul beneath fallen stone—");
                WriteLineSlow("Who was not abandoned,");
                WriteLineSlow("And left strength behind.");
                Console.WriteLine();
            }

            if (foundHiddenPath == true || didWhispers == true)
            {
                WriteLineSlow("Of whispers in the dark—");
                WriteLineSlow("Which yielded their silence");
                WriteLineSlow("And opened the hidden way.");
                Console.WriteLine();
            }

            if (knowsCronos == true || gotBurdenWord == true)
            {
                WriteLineSlow("Of a king before kings—");
                WriteLineSlow("Broken,");
                WriteLineSlow("Yet still carrying everything.");
                Console.WriteLine();
            }

            if (gotLossWord == true)
            {
                WriteLineSlow("Of death itself—");
                WriteLineSlow("Which reaps what is loved");
                WriteLineSlow("And leaves only loss behind.");
                Console.WriteLine();
            }

            if (gotLoveWord == true)
            {
                WriteLineSlow("Of a king who loved—");
                WriteLineSlow("Before he ruled—");
                WriteLineSlow("And never truly stopped.");
                Console.WriteLine();
            }

            if (crossedAsh == true)
            {
                WriteLineSlow("Of fire and ash—");
                WriteLineSlow("Of a path that could not be trusted—");
                WriteLineSlow("But was walked anyway.");
                Console.WriteLine();
            }

            WriteLineSlow("Of love that walks into darkness—");
            WriteLineSlow("Not to conquer—");
            WriteLineSlow("But to bring something back.");
            Console.WriteLine();
            WriteLineSlow("Of a man who had nothing left but song,");
            WriteLineSlow("And still refused to leave empty-handed.");
            Console.WriteLine();
            WriteLineSlow("And at the heart of it—");
            WriteLineSlow("Not a king.");
            WriteLineSlow("Not a throne.");
            WriteLineSlow("Only a man");
            WriteLineSlow("Who once saw the one he loved");
            WriteLineSlow("And was changed by it.");
            Console.WriteLine();
            WriteLineSlow("Silence.");
            Console.WriteLine();
            WriteLineSlow("The hall does not move.");
            Console.WriteLine();
            WriteLineSlow("Hades watches you.");
            Console.WriteLine();
            WriteLineSlow("\"What is it you ask of me?\"");
            Console.WriteLine();
            WriteLineSlow("\"I am not asking for power.\"");
            WriteLineSlow("\"I am not asking for mercy.\"");
            WriteLineSlow("\"I am asking for her.\"");
            WriteLineSlow("\"Give me Eurydice.\"");
            Console.WriteLine();
            WriteLineSlow("He closes his eyes.");
            WriteLineSlow("A single tear falls.");
            Console.WriteLine();
            WriteLineSlow("\"...You remind me.\"");
            WriteLineSlow("\"Of something I buried long ago.\"");
            Console.WriteLine();
            WriteLineSlow("\"You may take her.\"");
            WriteLineSlow("\"But understand this—\"");
            Console.WriteLine();
            WriteLineSlow("\"Do not look back.\"");
            WriteLineSlow("\"Not once.\"");
            WriteLineSlow("\"If you do—\"");
            WriteLineSlow("\"She is mine forever.\"");
            Console.WriteLine();

            Pause();
            EscapeSequence();
        }

        static void EscapeSequence()
        {
            Console.Clear();
            WriteLineSlow("You turn.");
            Console.WriteLine();
            WriteLineSlow("You begin to walk.");
            Console.WriteLine();
            WriteLineSlow("The hall stretches ahead.");
            Console.WriteLine();
            WriteLineSlow("You are not alone.");
            Console.WriteLine();
            WriteLineSlow("But you hear nothing.");
            WriteLineSlow("No footsteps.");
            WriteLineSlow("No breath.");
            Console.WriteLine();
            WriteLineSlow("Only silence—");
            WriteLineSlow("And the thought that you might be alone after all.");
            Console.WriteLine();

            Pause();

            EscapeStep(
                "The great doors open.\nThe presence of the King of Shades lingers behind you.\nHeavy. Watching.",
                "Move forward",
                "Turn back"
            );

            if (gameEnded == false)
            {
                WriteLineSlow("\"...Huh,\" Hypnos murmurs.");
                WriteLineSlow("\"You actually did it!\"");
                WriteLineSlow("\"Good luck on the way.\"");
                Console.WriteLine();
            }

            if (gameEnded == false)
            {
                EscapeStep(
                    "The Ashen Fields lie still.\nNo fire rises.\nA faint voice echoes:\n\"...Don't mess this up.\"",
                    "Move forward",
                    "Turn back"
                );
            }

            if (gameEnded == false)
            {
                EscapeStep(
                    "The catacombs are silent.\nNo whispers. No movement.\nA distant voice says:\n\"You carried the weight.\"\n\"Now finish it.\"",
                    "Move forward",
                    "Turn back"
                );
            }

            if (gameEnded == false)
            {
                EscapeStep(
                    "The river waits.\nA boat arrives without being called.\nCharon stands and gestures.\nThe shades part as if commanded.",
                    "Step into the boat",
                    "Turn back"
                );
            }

            if (gameEnded == false)
            {
                string cerberusLine = "The cave stands ahead.\nCerberus watches.\nIt does not move to stop you.";

                if (cerberusCalmed == true)
                {
                    cerberusLine = "The cave stands ahead.\nCerberus watches quietly.\nIt lowers its heads and lets you pass.";
                }

                EscapeStep(
                    cerberusLine,
                    "Move forward",
                    "Turn back"
                );
            }

            if (gameEnded == false)
            {
                Console.Clear();
                WriteLineSlow("Light spills into the cave.");
                WriteLineSlow("Real light.");
                Console.WriteLine();
                WriteLineSlow("You are close.");
                WriteLineSlow("Very close.");
                Console.WriteLine();
                WriteLineSlow("Your steps slow.");
                WriteLineSlow("Your chest tightens.");
                Console.WriteLine();
                WriteLineSlow("You have heard nothing.");
                WriteLineSlow("Not once.");
                WriteLineSlow("No proof.");
                WriteLineSlow("No sign.");
                WriteLineSlow("Nothing.");
                Console.WriteLine();
                WriteLineSlow("What if she is not there?");
                WriteLineSlow("What if you walked all this way—");
                WriteLineSlow("Alone?");
                Console.WriteLine();

                bool finalSave = HiddenCheck(charisma, 12);

                if (finalSave == false)
                {
                    BadEndingLookBack();
                }
                else
                {
                    WriteLineSlow("You reach the edge of the cave.");
                    WriteLineSlow("The light is just ahead.");
                    WriteLineSlow("You stop.");
                    Console.WriteLine();
                    WriteLineSlow("You feel nothing behind you.");
                    WriteLineSlow("Nothing.");
                    WriteLineSlow("Not even the air shifting.");
                    Console.WriteLine();
                    WriteLineSlow("Should you look back?");
                    WriteLineSlow("Type anything:");
                    string finalInput = Console.ReadLine().Trim().ToLower();
                    Console.WriteLine();

                    bool lookedBack = false;

                    if (finalInput.Contains("look"))
                    {
                        lookedBack = true;
                    }

                    if (finalInput.Contains("back"))
                    {
                        lookedBack = true;
                    }

                    if (finalInput.Contains("turn"))
                    {
                        lookedBack = true;
                    }

                    if (finalInput.Contains("check"))
                    {
                        lookedBack = true;
                    }

                    if (lookedBack == true)
                    {
                        BadEndingLookBack();
                    }
                    else
                    {
                        GoodEnding();
                    }
                }
            }
        }

        static void EscapeStep(string description, string forwardText, string backText)
        {
            Console.Clear();
            WriteLineSlow(description);
            Console.WriteLine();
            WriteLineSlow("What do you do?");
            WriteLineSlow($"1. {forwardText}");
            WriteLineSlow($"2. {backText}");

            int choice = AskChoice(1, 2);
            Console.WriteLine();

            if (choice == 2)
            {
                BadEndingLookBack();
            }
        }

        static void BadEndingLookBack()
        {
            Console.Clear();
            WriteLineSlow("You turn.");
            Console.WriteLine();
            WriteLineSlow("Just for a moment.");
            WriteLineSlow("Just to be sure.");
            Console.WriteLine();
            WriteLineSlow("She is there.");
            WriteLineSlow("Close.");
            WriteLineSlow("So close.");
            Console.WriteLine();
            WriteLineSlow("And then—");
            Console.WriteLine();
            WriteLineSlow("She is gone.");
            Console.WriteLine();
            WriteLineSlow("The darkness returns.");
            WriteLineSlow("You are alone.");
            WriteLineSlow("Forever.");
            Console.WriteLine();

            gameEnded = true;
            nextArea = "end";
        }

        static void GoodEnding()
        {
            Console.Clear();
            WriteLineSlow("You step forward.");
            Console.WriteLine();
            WriteLineSlow("Into the light.");
            Console.WriteLine();
            WriteLineSlow("You breathe.");
            Console.WriteLine();
            WriteLineSlow("For the first time—");
            WriteLineSlow("It feels real.");
            Console.WriteLine();
            WriteLineSlow("You stop.");
            Console.WriteLine();
            WriteLineSlow("Slowly—");
            WriteLineSlow("You turn.");
            Console.WriteLine();
            WriteLineSlow("She stands there.");
            Console.WriteLine();
            WriteLineSlow("Eurydice.");
            Console.WriteLine();
            WriteLineSlow("Alive.");
            Console.WriteLine();
            WriteLineSlow("She looks at you—");
            WriteLineSlow("As if she never left.");
            Console.WriteLine();
            WriteLineSlow("You run to her.");
            WriteLineSlow("You hold her.");
            WriteLineSlow("And she does not fade.");
            Console.WriteLine();
            WriteLineSlow("You did not look back.");
            WriteLineSlow("And this time—");
            WriteLineSlow("Love was enough.");
            Console.WriteLine();

            gameEnded = true;
            nextArea = "end";
        }

    }

}