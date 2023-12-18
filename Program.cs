using System;
using efcrash;

class Program 
{
    static void Main()
    {
        int choice = 0;

        do
        {
            ShowIntro();

            bool isValidChoice = int.TryParse(Console.ReadLine(), out choice);
            Console.WriteLine();

            if (!isValidChoice)
            {
                Console.WriteLine("Invalid Choice! Please try again...");
            }
            else 
            {
                RunSelection(choice);
            }
        } while (!HasQuit());
    }

    private static bool HasQuit()
    {
        bool isValidInput = false;
        bool hasQuit = false;

        do
        {
            Console.WriteLine("\nWould you like to continue?");
            Console.WriteLine("Press Y to continue, press N to exit..");
            string userInput = Console.ReadLine() ?? "";
            userInput = userInput.ToLower();

            if (userInput.Equals("y"))
            {
                isValidInput = true;
            }
            else if (userInput.Equals("n"))
            {
                Console.WriteLine("Thank you for using the blog console");
                Environment.Exit(0);
                isValidInput = true;
                hasQuit = true;
            }
            else
            {
                Console.WriteLine("Invalid input. Try again.");
            }
        } while (!isValidInput);

        return hasQuit;
    }

    private static void ShowIntro()
    {
        Console.Clear();
        Console.WriteLine("Welcome to the Blog Manager! What would you like to do?\n");
        Console.WriteLine("1. Add a Blog Site");
        Console.WriteLine("2. Get all Blogs");
        Console.WriteLine("3. Add a Post");
        Console.WriteLine("4. Get all Posts");
        Console.WriteLine("5. Update a Post");
        Console.WriteLine("6. Delete a Post");
        Console.WriteLine();
        Console.Write("Select a number: ");
    }

    private static void RunSelection(int choice)
    {
        Console.WriteLine($"You selected: {choice}\n");

        switch (choice)
        {
            case 1:
                DataManager.AddBlogSite();
                break;
            case 2:
                DataManager.GetAllBlogs();
                break;
            case 3:
                DataManager.AddPost();
                break;
            case 4:
                DataManager.GetPostBlogId();
                break;
            case 5:
                DataManager.UpdatePost();
                break;
            case 6:
                DataManager.DeletePost();
                break;
            default:
                Console.WriteLine("Please select a choice between 1-6.");
                break;
        }
    }
}