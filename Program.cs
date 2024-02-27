using Figgle;

namespace Assessment_1
{
    internal class Program
    {
        static void MainTitle()
        {
            string titleText = "Word Dictionary Manager";
            string asciiArt = FiggleFonts.Standard.Render(titleText);
            Console.Write(asciiArt + "COMP605 - Data Structures and Algorithms\n" + "v1.0\n" + "By Hamish Getty\n");

            PrintLineBreak();

            Console.Write("This application can:\n" +
                "- Read data from the given text files.\n" +
                "- Insert that data into a dictionary.\n" +
                "- Find data from within the dictionary contents.\n" +
                "- Print the dictionary contents.\n" +
                "- Perform other functionalities.\n\n");

            Console.Write("Press any key to get started: ");
        }

        // Declare the dictionary at the class level
        private static MyDictionary<string, Node> dictionary = new MyDictionary<string, Node>();

        static void MainMenu()
        {
            Console.ReadKey();

            Console.Clear();

            PrintLineBreak();

            Console.Write("1. Insert\n" +
                "2. Find\n" +
                "3. Delete\n" +
                "4. Print\n" +
                "5. Exit\n" +
                "Select an option: ");

            // Get the user's choice
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    InsertMenu(dictionary);
                    break;

                case "2":
                    FindMenu(dictionary);
                    break;

                case "4":
                    PrintMenu(dictionary);
                    break;

                case "5":
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    Console.Write("Press any key to continue: ");
                    MainMenu();
                    break;
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

        static void ReadFileAndInsertWords(string filePath, MyDictionary<string, Node> dictionary)
        {
            int wordsInserted = 0;

            try
            {
                // Read all lines from the file
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    // Ignore lines starting with "#" and empty lines
                    if (!line.StartsWith("#") && !string.IsNullOrWhiteSpace(line))
                    {
                        // Check if the key already exists
                        if (!dictionary.ContainsKey(line))
                        {
                            // Create a Node for each word and insert into the dictionary
                            Node node = new Node(line);
                            dictionary.Add(line, node);

                            // Increment the counter for each word inserted
                            wordsInserted++;

                            // Print the data after insertion in the insert menu
                            Console.WriteLine($"Inserted: Word: {node.Word}, Length: {node.Length}");
                        }
                        else
                        {
                            Console.WriteLine($"Duplicate key found: {line}. Skipping insertion.");
                        }
                    }
                }

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
                // Add more cases for other folders if needed
                // ...

                default:
                    Console.WriteLine("Invalid choice. Returning to the main menu.");
                    Console.Write("Press any key to continue: ");
                    MainMenu();
                    return;
            }

            PrintLineBreak();

            Console.WriteLine($"You selected the {folderPath} folder.");

            Console.WriteLine("Choose a file to insert into the dictionary:");

            // Get all files in the selected folder
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
                // Subtract 1 because the user input is 1-based, but the array is 0-based
                string filePath = files[selectedFileIndex - 1];
                PrintLineBreak();
                Console.WriteLine($"Inserting words from {filePath} into the dictionary...");

                // Call the method to read and insert words
                ReadFileAndInsertWords(filePath, dictionary);
            }
            else
            {
                PrintLineBreak();
                Console.WriteLine("Invalid choice. Returning to the main menu.");
            }

            Console.Write("Press any key to continue: ");
            MainMenu();
        }

        static void FindMenu(MyDictionary<string, Node> dictionary)
        {
            PrintLineBreak();

            Console.Write("Enter the word to find: ");
            string wordToFind = Console.ReadLine();

            var foundNode = dictionary.Find(wordToFind);

            if (foundNode != null)
            {
                Console.WriteLine($"Word '{wordToFind}' found. Details: {foundNode.Word}, {foundNode.Length}");
            }
            else
            {
                Console.WriteLine($"Word '{wordToFind}' not found in the dictionary.");
            }

            Console.Write("Press any key to continue: ");
            MainMenu();
        }

        public static void PrintMenu(MyDictionary<string, Node> dictionary)
        {
            PrintLineBreak();

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

            Console.Write("Press any key to continue: ");
            MainMenu();
        }
    }
}