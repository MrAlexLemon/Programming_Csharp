using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class Program

    {
        // Entry point into console application.
       
        static void Main()
        {
            // Create book

            Book book = new Book("Arthur Conan Doyle", "Sherlock Holmes", 10);
            book.Display();

            // Create video

            Video video = new Video("Logan", "James Mangold", 23, 92);
            video.Display();

            // Make video borrowable, then borrow and display

            Console.WriteLine("\nMaking video borrowable:");

            Borrowable borrowvideo = new Borrowable(video);
            borrowvideo.BorrowItem("Customer #1");
            borrowvideo.BorrowItem("Customer #2");

            borrowvideo.Display();

            // Wait for user

            Console.ReadKey();
        }
    }
}
