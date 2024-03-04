using Figgle;
using System.Diagnostics;

namespace Word_Dictionary_Manager
{
    internal class Program
    {
        private static MyDictionary<string, Node> dictionary = new MyDictionary<string, Node>();

        static void MainTitle()
        {
            string titleText = "Word Dictionary Manager";
            string asciiArt = FiggleFonts.Standard.Render(titleText);
            Console.Write(asciiArt + "COMP605 - Data Structures and Algorithms\n" + "v1.0\n" + "By Hamish Getty\n");

            PrintLineBreak();

            Console.Write("This application can:\n" +
                "- Load File: Import data from specified text files into the dictionary.\n" +
                "- Insert Word: Add new words to the dictionary, avoiding duplicates.\n" +
                "- Find Word: Search for a specific word within the dictionary and retrieve its details.\n" +
                "- Delete: Choose to remove a particular word or clear the entire dictionary (caution: irreversible).\n" +
                "- Print Dictionary: Display the current contents of the dictionary.\n" +
                "- Exit: Close the application.\n\n");

            Console.Write("Press any key to get started: ");
        }

        static void MainMenu()
        {
            while (true)
            {
                Console.ReadKey();
                Console.Clear();

                PrintLineBreak();

                Console.Write("1. Load File\n" +
                    "2. Insert Word\n" +
                    "3. Find Word\n" +
                    "4. Delete\n" +
                    "5. Print Dictionary\n" +
                    "6. Exit\n" +
                    "Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        FileMenu(dictionary);
                        break;

                    case "2":
                        InsertMenu(dictionary);
                        break;

                    case "3":
                        FindMenu(dictionary);
                        break;

                    case "4":
                        DeleteMenu(dictionary);
                        break;

                    case "5":
                        PrintMenu(dictionary);
                        break;

                    case "6":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        Console.Write("Press any key to continue: ");
                        break;
                }
            }
        }

        static void PrintLineBreak()
        {
            int consoleWidth = Console.WindowWidth;
            string dashes = new string('-', consoleWidth);
            Console.Write(dashes);
        }

        static void Main(string[] args)
        {
            MainTitle();
            MainMenu();
        }

        static void FileMenu(MyDictionary<string, Node> dictionary)
        {
            PrintLineBreak();

            Console.WriteLine("Choose a folder to access:");
            Console.WriteLine("1. Ordered");
            Console.WriteLine("2. Random");

            Console.Write("Select an option: ");
            string folderChoice = Console.ReadLine();

            string folderPath = "";

            switch (folderChoice)
            {
                case "1":
                    folderPath = "ordered";
                    break;
                case "2":
                    folderPath = "random";
                    break;
                default:
                    Console.WriteLine("Invalid choice. Returning to the main menu.");
                    Console.Write("Press any key to continue: ");
                    MainMenu();
                    return;
            }

            PrintLineBreak();

            Console.WriteLine($"You selected the {folderPath} folder.");

            Console.WriteLine("Choose a file to insert into the dictionary:");

            string[] files = Directory.GetFiles(folderPath);

            for (int i = 0; i < files.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {Path.GetFileName(files[i])}");
            }

            Console.Write("Select an option: ");
            string fileChoice = Console.ReadLine();

            int selectedFileIndex;
            if (int.TryParse(fileChoice, out selectedFileIndex) && selectedFileIndex >= 1 && selectedFileIndex <= files.Length)
            {
                string filePath = files[selectedFileIndex - 1];
                PrintLineBreak();

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                Console.WriteLine($"Clearing the current dictionary...");

                dictionary.Clear();  // Clear the existing dictionary before inserting from the file

                Console.WriteLine($"Inserting words from {filePath} into the dictionary...");

                ReadFile(filePath, dictionary);

                stopwatch.Stop();
                TimeSpan elapsedTime = stopwatch.Elapsed;

                Console.WriteLine($"Time taken: {elapsedTime.TotalMilliseconds} milliseconds");
            }
            else
            {
                PrintLineBreak();
                Console.WriteLine("Invalid choice. Returning to the main menu.");
            }

            Console.Write("Press any key to continue: ");
            MainMenu();
        }

        static void ReadFile(string filePath, MyDictionary<string, Node> dictionary)
        {
            int wordsInserted = 0;

            int duplicateWords = 0;

            try
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    if (!line.StartsWith("#") && !string.IsNullOrWhiteSpace(line))
                    {
                        if (!dictionary.ContainsKey(line))
                        {
                            Node node = new Node(line);
                            dictionary.Add(line, node);

                            wordsInserted++;

                        }
                        else
                        {
                            duplicateWords++;
                        }
                    }
                }
                PrintLineBreak();
                Console.WriteLine($"Duplicate keys found: {duplicateWords}. Skipped insertions.");
                Console.WriteLine($"{wordsInserted} words inserted into the dictionary successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        static void InsertMenu(MyDictionary<string, Node> dictionary)
        {
            PrintLineBreak();

            Console.Write("Enter a word to insert: ");
            string wordToInsert = Console.ReadLine();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start(); 

            if (!string.IsNullOrWhiteSpace(wordToInsert) && wordToInsert.All(char.IsLetter))
            {
                if (!dictionary.ContainsKey(wordToInsert))
                {
                    Node node = new Node(wordToInsert);
                    dictionary.Add(wordToInsert, node);

                    stopwatch.Stop(); 
                    TimeSpan elapsedTime = stopwatch.Elapsed;

                    Console.WriteLine($"Time taken for insertion: {elapsedTime.TotalMilliseconds} milliseconds");

                    Console.WriteLine($"Word '{wordToInsert}' inserted into the dictionary.");
                }
                else
                {
                    Console.WriteLine($"Word '{wordToInsert}' already exists in the dictionary. Skipping insertion.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a non-empty word containing only alphabetic characters.");
            }

            Console.Write("Press any key to continue: ");
            MainMenu();
        }

        static void FindMenu(MyDictionary<string, Node> dictionary)
        {
            PrintLineBreak();

            Console.Write("Enter the word to find: ");
            string wordToFind = Console.ReadLine();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start(); 

            var foundNode = dictionary.Find(wordToFind);

            stopwatch.Stop(); 
            TimeSpan elapsedTime = stopwatch.Elapsed;

            if (foundNode != null)
            {
                Console.WriteLine($"Word '{wordToFind}' found. Details: {foundNode.Word}, {foundNode.Length}");
            }
            else
            {
                Console.WriteLine($"Word '{wordToFind}' not found in the dictionary.");
            }

            
            Console.WriteLine($"Time taken for find operation: {elapsedTime.TotalMilliseconds} milliseconds");

            Console.Write("Press any key to continue: ");
            MainMenu();
        }

        static void DeleteMenu(MyDictionary<string, Node> dictionary) 
        {
            PrintLineBreak();

            Console.WriteLine("What would you like to delete?");
            Console.WriteLine("1. A particular word");
            Console.WriteLine("2. Entire dictionary");

            Console.Write("Select an option: ");
            string deleteChoice = Console.ReadLine();

            string deletePath = "";

            switch (deleteChoice)
            {
                case "1":
                    PrintLineBreak();
                    Console.Write("Enter the word to delete: ");
                    string wordToDelete = Console.ReadLine();
                    dictionary.Delete(wordToDelete);
                    break;
                case "2":
                    PrintLineBreak();
                    Console.WriteLine("Are you sure you would like to delete the entire dictionary? This action cannot be undone.");
                    Console.Write("Y/N: ");

                    string confirmDelete = Console.ReadLine().ToLower();

                    if (confirmDelete == "y" || confirmDelete == "yes")
                    {
                        dictionary.Clear();
                        Console.WriteLine("Entire dictionary deleted.");
                    }
                    else if (confirmDelete == "n" || confirmDelete == "no")
                    {
                        Console.WriteLine("Returning to the main menu.");
                        Console.Write("Press any key to continue: ");
                        MainMenu();
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice. Returning to the main menu.");
                        Console.Write("Press any key to continue: ");
                        MainMenu();
                        return;
                    }
                    break;

                default:
                    Console.WriteLine("Invalid choice. Returning to the main menu.");
                    Console.Write("Press any key to continue: ");
                    MainMenu();
                    return;
            }
            Console.Write("Press any key to continue: ");
            MainMenu();
        }

        public static void PrintMenu(MyDictionary<string, Node> dictionary)
        {
            PrintLineBreak();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.WriteLine("Printing Dictionary Contents:");

            List<KeyValuePair<string, Node>> entries = dictionary.GetEntries();

            if (entries.Count > 0)
            {
                dictionary.Print();
            }
            else
            {
                Console.WriteLine("Dictionary is empty.");
            }

            stopwatch.Stop();
            TimeSpan elapsedTime = stopwatch.Elapsed;

            PrintLineBreak();

            Console.WriteLine($"Time taken: {elapsedTime.TotalMilliseconds} milliseconds");

            Console.Write("Press any key to continue: ");
            MainMenu();
        }
    }
}