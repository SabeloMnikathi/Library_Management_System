using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystemConsoleAplication
{
    // Represents a book in the library
    class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsAvailable { get; set; } = true;
        public int BorrowerId { get; set; } = -1;  // -1 indicates not borrowed
    }

    // Represents a library user
    class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
    }

    // Represents the library system
    class Library
    {
        private List<Book> books = new List<Book>();
        private List<User> users = new List<User>();
        private int nextUserId = 1;

        // Add a new book to the library
        public void AddBook(string title, string author)
        {
            Book newBook = new Book { Title = title, Author = author };
            books.Add(newBook);
            Console.WriteLine("Book added successfully.");
        }

        // Display the list of available books
        public void ViewBooks()
        {
            Console.WriteLine("Available books:");
            foreach (Book book in books)
            {
                if (book.IsAvailable)
                {
                    Console.WriteLine($"Title: {book.Title}, Author: {book.Author}");
                }
            }
        }

        // Borrow a book from the library
        public void BorrowBook(int userId, string title)
        {
            Book bookToBorrow = books.Find(book => book.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            User user = users.Find(u => u.UserId == userId);

            if (bookToBorrow != null && bookToBorrow.IsAvailable && user != null)
            {
                bookToBorrow.IsAvailable = false;
                bookToBorrow.BorrowerId = userId;
                Console.WriteLine($"{user.Username} has borrowed the book: {bookToBorrow.Title}");
            }
            else
            {
                Console.WriteLine("Book not found, already borrowed, or user not found.");
            }
        }

        // Return a borrowed book to the library
        public void ReturnBook(int userId, string title)
        {
            Book bookToReturn = books.Find(book => book.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            User user = users.Find(u => u.UserId == userId);

            if (bookToReturn != null && !bookToReturn.IsAvailable && bookToReturn.BorrowerId == userId && user != null)
            {
                bookToReturn.IsAvailable = true;
                bookToReturn.BorrowerId = -1;
                Console.WriteLine($"{user.Username} has returned the book: {bookToReturn.Title}");
            }
            else
            {
                Console.WriteLine("Book not found, not borrowed by the user, or user not found.");
            }
        }

        // Register a new user
        public void RegisterUser(string username)
        {
            User newUser = new User { UserId = nextUserId++, Username = username };
            users.Add(newUser);
            Console.WriteLine($"User {username} registered successfully with ID: {newUser.UserId}");

        }
    }

    // Represents the console user interface
    class ConsoleUI
    {
        private Library library = new Library();

        // Run the console UI
        public void Run()
        {
            while (true)
            {
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. View Books");
                Console.WriteLine("3. Borrow Book");
                Console.WriteLine("4. Return Book");
                Console.WriteLine("5. Register User");
                Console.WriteLine("0. Exit");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.Write("Enter book title: ");
                        string title = Console.ReadLine();
                        Console.Write("Enter author name: ");
                        string author = Console.ReadLine();
                        library.AddBook(title, author);
                        break;
                    case 2:
                        library.ViewBooks();
                        break;
                    case 3:
                        Console.Write("Enter user ID: ");
                        int userIdBorrow = int.Parse(Console.ReadLine());
                        Console.Write("Enter book title to borrow: ");
                        string borrowTitle = Console.ReadLine();
                        library.BorrowBook(userIdBorrow, borrowTitle);
                        break;
                    case 4:
                        Console.Write("Enter user ID: ");
                        int userIdReturn = int.Parse(Console.ReadLine());
                        Console.Write("Enter book title to return: ");
                        string returnTitle = Console.ReadLine();
                        library.ReturnBook(userIdReturn, returnTitle);
                        break;
                    case 5:
                        Console.Write("Enter username: ");
                        string username = Console.ReadLine();
                        library.RegisterUser(username);
                        break;
                    case 0:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ConsoleUI consoleUI = new ConsoleUI();
            consoleUI.Run();
            
        }
    }

}
