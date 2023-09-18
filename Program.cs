using System;
using System.Collections.Generic;

// Parent class
public class Item
{
    public string Title { get; set; }
    public int Year { get; set; }

    public Item(string title, int year)
    {
        Title = title;
        Year = year;
    }

    public virtual void DisplayInfo()
    {
        Console.WriteLine("Judul: {0}", Title);
        Console.WriteLine("Tahun: {0}", Year);
    }
}

// Child class
public class Book : Item
{
    public string Author { get; set; }

    public Book(string title, int year, string author) : base(title, year)
    {
        Author = author;
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine("Penulis: {0}", Author);
    }
}

// LibraryCatalog class
public class LibraryCatalog
{
    private List<Book> books = new List<Book>();

    public void AddBook(Book book)
    {
        books.Add(book);
    }

    public void RemoveBook(Book book)
    {
        books.Remove(book);
    }

    public Book FindBook(string title)
    {
        return books.Find(b => b.Title == title);
    }

    public void ListBooks()
    {
        foreach (var book in books)
        {
            book.DisplayInfo();
            Console.WriteLine();
        }
    }
}

// ErrorHandler class
public static class ErrorHandler
{
    public static void HandleError(string message)
    {
        Console.WriteLine("Error: " + message);
    }
}

// LibraryApp class
public class LibraryApp
{
    public static void Main(string[] args)
    {
        LibraryCatalog catalog = new LibraryCatalog();
        bool isRunning = true;

        while (isRunning)
        {
            Console.WriteLine("=============================");
            Console.WriteLine("Sistem Informasi Perpustakaan");
            Console.WriteLine("1. Tambah Buku");
            Console.WriteLine("2. Hapus Buku");
            Console.WriteLine("3. Cari Buku");
            Console.WriteLine("4. Tampilkan Semua Buku");
            Console.WriteLine("5. Keluar");
            Console.WriteLine("=============================");
            Console.WriteLine("");
            Console.Write("Pilih menu: ");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                ErrorHandler.HandleError("Pilihan tidak valid. Silakan coba lagi.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    Console.Write("Judul Buku: ");
                    string title = Console.ReadLine();
                    Console.Write("Tahun Terbit: ");
                    int year;
                    if (!int.TryParse(Console.ReadLine(), out year))
                    {
                        ErrorHandler.HandleError("Tahun terbit harus berupa angka.");
                        continue;
                    }
                    Console.Write("Penulis: ");
                    string author = Console.ReadLine();

                    Book newBook = new Book(title, year, author);
                    catalog.AddBook(newBook);

                    Console.WriteLine("Buku berhasil ditambahkan!");
                    Console.WriteLine("");
                    break;
                case 2:
                    Console.Write("Judul Buku yang akan dihapus: ");
                    string titleToRemove = Console.ReadLine();
                    Book bookToRemove = catalog.FindBook(titleToRemove);

                    if (bookToRemove != null)
                    {
                        catalog.RemoveBook(bookToRemove);
                        Console.WriteLine("Buku berhasil dihapus.");
                    }
                    else
                    {
                        ErrorHandler.HandleError("Buku tidak ditemukan dalam katalog.");
                    }

                    Console.WriteLine("");
                    break;
                case 3:
                    Console.Write("Judul Buku yang dicari: ");
                    string titleToFind = Console.ReadLine();
                    Book foundBook = catalog.FindBook(titleToFind);

                    if (foundBook != null)
                    {
                        Console.WriteLine("Buku ditemukan:");
                        foundBook.DisplayInfo();
                    }
                    else
                    {
                        ErrorHandler.HandleError("Buku tidak ditemukan dalam katalog.");
                    }

                    Console.WriteLine("");
                    break;
                case 4:
                    Console.WriteLine("Daftar Buku:");
                    catalog.ListBooks();
                    Console.WriteLine("");
                    break;
                case 5:
                    isRunning = false;
                    break;
                default:
                    ErrorHandler.HandleError("Pilihan Tidak Ada. Silakan Coba Lagi.");
                    break;
            }
        }
    }
}