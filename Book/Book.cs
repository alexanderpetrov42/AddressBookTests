using System;
using System.Collections.Generic;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Book default_book = new Book();
Book four_param = new Book("Marijn Haverbeke", "Eloquent Javascript", 38.07, 2018  );
Book two_param = new Book("Marijn Haverbeke", 2018);

ChildrenBook default_children_book = new ChildrenBook();
ChildrenBook five_param_children_book = new ChildrenBook("Lewis Carroll", "Alice's Adventures in Wonderland", 2.10, 1865, 9);
ChildrenBook two_param_children_book = new ChildrenBook("Lewis Carroll", 1865);

default_book.Print();
four_param.Print();
two_param.Print();

default_children_book.Print();
five_param_children_book.Print();
two_param_children_book.Print();

List<Book> books = new List<Book>();
books.Add(default_book);
books.Add(four_param);
books.Add(two_param);
books.Add(default_children_book);
books.Add(five_param_children_book);

books.RemoveAt(2); 
books.Insert(2, two_param_children_book);
Console.WriteLine(books.Count);
books.Sort();
Console.WriteLine("Printing out book list: ");
books.ForEach(b => b.Print());
books.Reverse();
Console.WriteLine("Printing out book list: ");
books.ForEach(b => b.Print());
books.Clear();

class Book : IComparable
{
    private protected string author { get; set; }
    private protected string bookName { get; set; }
    private protected double price { get; set; }
    private protected int publishingYear { get; set; }
    private protected string printString { get; set; }

    public Book() { author = "Undefined"; bookName = "Undefined"; price = 0.0; publishingYear = 0; }
    public Book(string a, string bn, double p, int py) { author = a; bookName = bn; price = p; publishingYear = py; }
    public Book(string a, int py) { author = a; publishingYear = py; }

    ~Book()
    {
        Console.WriteLine("Launching the destructor");
    }
    public void createPrintString()
    {
        printString = $"Author: {author}  Book name: {bookName} ";
    }
    public virtual void Print()
    {
       createPrintString();
       Console.WriteLine(printString);
    }

    public int CompareTo(object? o)
    {
        if (o is Book book) return bookName.CompareTo(book.bookName);
        else throw new ArgumentException("Incorrect parameter name");
    }
}

class ChildrenBook : Book
{
    private int minimum_age { get; set; }

    public ChildrenBook() { author = "Undefined"; bookName = "Undefined"; price = 0.0; publishingYear = 0; minimum_age = 0; }

    public ChildrenBook(string a, int py) { author = a; publishingYear = py; }

    public ChildrenBook(string a, string bn, double p, int py, int ma) { author = a; bookName = bn; price = p; publishingYear = py; minimum_age = ma; }

    ~ChildrenBook()
    {
        Console.WriteLine("Launching the destructor");
    }

    public override void Print()
    {
        createPrintString();
        printString = printString + $"Minimum age: {minimum_age}";
        Console.WriteLine(printString);
    }
}