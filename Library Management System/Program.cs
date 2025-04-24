
namespace Library_Management_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            LibrarySystem librarySystem = new LibrarySystem();
            while (true) {
                Console.WriteLine("-------------------------- Welcome to Library Management System --------------------");
                Console.WriteLine("1.AddMember\n2. UpdateMember\n3.DeleteMember\n4.DisplayMembers\n5.AddBook\n6.UpdateBook\n7.DeleteBook\n8.DisplayBooks\n9.IssueBook\n10.ReturnBook\n11.DeclareMostBorrowedBook\n12.Exit\n");
                Console.WriteLine("Enter your choice betweeen 1 to 12:");
                int choice= int.Parse(Console.ReadLine());
                switch (choice) {

                    case 1:
                        librarySystem.AddMembers();
                    break;
                    
                    case 2:
                        Console.WriteLine("Enter CNIC:");
                        string cnic_update= Console.ReadLine();
                        librarySystem.UpdateMembers(cnic_update);
                    break;
                    
                    case 3:
                        Console.WriteLine("Enter CNIC of a member to delete");
                        string cnic_delete = Console.ReadLine();
                        librarySystem.DeleteMembers(cnic_delete);
                    break;
                    
                    case 4:
                        librarySystem.DisplayMembers();
                    break;
                    
                    case 5:
                        librarySystem.AddBook();
                    break;
                    
                    case 6:
                        Console.WriteLine("Enter BookID you want to Update");
                        int id= int.Parse(Console.ReadLine());
                        librarySystem.UpdateBook(id);
                    break;
                    
                    case 7:
                        Console.WriteLine("Emter BookID to delete:");
                        int bookid= int.Parse(Console.ReadLine());
                        librarySystem.DeleteBook(bookid);
                    break;

                    case 8:
                        librarySystem.DisplayBooks();
                    break;
                        
                    case 9:
                        Console.WriteLine("ENter CNIC:");
                        string cnic= Console.ReadLine();
                        Console.WriteLine("ENter ID of Book");
                        int book_id= int.Parse(Console.ReadLine());
                        librarySystem.IssueBook(cnic,book_id);
                    break;
                    
                    case 10:
                        Console.WriteLine("Enter your CNIC:");
                        string ucnic = Console.ReadLine();
                        Console.WriteLine("Enter Id of Book:");
                        int bid= int.Parse(Console.ReadLine());
                        librarySystem.ReturnBook(ucnic,bid);
                    break;
              
                    case 11:
                        librarySystem.DeclareMostBorrowedBook();
                    break;
                  
                    case 12:
                        return;
                    break;


                    default:
                        Console.WriteLine("wrong choice");
                        return;
                }
            }
        }
        
        }
}