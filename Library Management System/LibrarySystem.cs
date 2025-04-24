using Microsoft.Data.SqlClient;

namespace Library_Management_System
{
    public class LibrarySystem
    {
        private List<Book> books;
        private List<Member> members;
        SqlConnection connection = null;

        public LibrarySystem() {
            books = new List<Book>();
            members = new List<Member>();
            FillBooks();
            Fillmembers();
        }
        public void Fillmembers()
        {
            connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LibraryManagementSystem;Integrated Security=True");
            string query1 = $"SELECT Name, CNIC FROM Member ";
            SqlCommand command = new SqlCommand(query1, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string name = reader["Name"].ToString();
                string cnic = reader["CNIC"].ToString();
                Member m = new Member(name, cnic);
                members.Add(m);
            }
            connection.Close();


        }

        public void FillBooks()
        {
            connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LibraryManagementSystem;Integrated Security=True");
            string query1 = $"SELECT BookID, Title, Author, IsIssued FROM Book";
            SqlCommand command = new SqlCommand(query1, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string name = reader["Title"].ToString();
                string author = reader["Author"].ToString();
                bool flag = Convert.ToBoolean(reader["IsIssued"]);

                Book b= new Book(author, name);
                b.SetIssuedStatus(flag);    
                books.Add(b);
            }
            connection.Close();
        }
        public void AddMembers()
        {
            Console.WriteLine("Enter Name: ");
            string name= Console.ReadLine();
            Console.WriteLine("Enter CNIC: ");
            string cnic= Console.ReadLine();
            connection= new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LibraryManagementSystem;Integrated Security=True");
            Member m = new Member(name,cnic);
            members.Add(m);
            string query = $"INSERT into Member (Name, CNIC) values('{name}', '{cnic}')";
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            int count = cmd.ExecuteNonQuery();
            if (count > 0) Console.WriteLine("Added");
            else Console.WriteLine("Error");

            connection.Close();

        }
        public void UpdateMembers(string cnic)
        {
            Console.WriteLine("New Name");
            string newname= Console.ReadLine();
            connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LibraryManagementSystem;Integrated Security=True");
            string query = $"UPDATE Member SET Name='{newname}' WHERE CNIC='{cnic}'";
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            int count = cmd.ExecuteNonQuery();
            connection.Close();
            if (count > 0)
            {
                Console.WriteLine("Updated");
                members.Clear();
                Fillmembers();
            }

            else
            {
                Console.WriteLine("Cannot Update");
            }


        }
        public void DeleteMembers(string cnic)
        {
            connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LibraryManagementSystem;Integrated Security=True");
            connection.Open();
            string deleteborrower = $"DELETE FROM BorrowedBooks WHERE MemberCNIC='{cnic}'";
            string deletemember = $"DELETE FROM Member WHERE CNIC='{cnic}'";
            SqlCommand command1= new SqlCommand(deleteborrower, connection);
            SqlCommand command2 = new SqlCommand(deletemember, connection);
            command1.ExecuteNonQuery();
            command2.ExecuteNonQuery();
            Console.WriteLine("Member Deleted");
            members.Clear();
            Fillmembers();
            connection.Close();
        }

        public void AddBook()
        {
            Console.WriteLine("Enter Title: ");
            string title = Console.ReadLine();
            Console.WriteLine("Enter Author: ");
            string author = Console.ReadLine();
            connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LibraryManagementSystem;Integrated Security=True");
            Book b = new Book(title, author);
            bool flag = b.IsIssued;
            books.Add(b);
            string query = $"INSERT into Book (Title, Author,IsIssued) values('{title}', '{author}','{flag}')";
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            int count = cmd.ExecuteNonQuery();
            connection.Close();
            if (count > 0)
                Console.WriteLine("Book Added ");
            else
                Console.WriteLine("Error");
        }
        public void UpdateBook(int bookID)
        {
            Console.WriteLine("New Title");
            string newtitle = Console.ReadLine();
            Console.WriteLine("New Author");
            string newauthor = Console.ReadLine();

            connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LibraryManagementSystem;Integrated Security=True");
            string query = $"UPDATE Book SET Title='{newtitle}', Author='{newauthor}'WHERE BookID='{bookID}'";
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            int count = cmd.ExecuteNonQuery();
            connection.Close();
            if (count > 0)
            {
                members.Clear();
                FillBooks();
                Console.WriteLine("Updated");

            }

            else
            {
                Console.WriteLine("Cannot Update");
            }

            connection.Close();
        }
        public void DeleteBook(int id)
        {
            connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LibraryManagementSystem;Integrated Security=True");
            connection.Open();
            string deleteborrowedbook = $"DELETE FROM BorrowedBooks WHERE BookID ='{id}'";
            string deletebook = $"DELETE FROM Book WHERE BookID='{id}'";
            SqlCommand command1 = new SqlCommand(deleteborrowedbook, connection);
            SqlCommand command2 = new SqlCommand(deletebook, connection);
            int i = command1.ExecuteNonQuery();
            int j = command2.ExecuteNonQuery();
            if (i > 0 & j > 0) { 
            Console.WriteLine("Book Deleted");
        }
            else Console.WriteLine("Error");

            books.Clear();
            FillBooks();
            connection.Close();

        }
        public void DisplayBooks()
        {
            connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LibraryManagementSystem;Integrated Security=True");
            string query = "SELECT * FROM Book";
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string title = reader["Title"].ToString();
                string author = reader["Author"].ToString();
                bool isIssued = reader.GetBoolean(3);

                Console.WriteLine($"ID: {id}, Title: {title}, Author: {author}, ");
                if (isIssued)
                {
                    Console.WriteLine("Issued");
                }
                else Console.WriteLine("Not Issued");
            }
            connection.Close();


        }

        public void DisplayMembers()
        {
            //IF U WANNA VIEW FROM LIST

            //foreach(Member m in members)
            //{
            //    Console.WriteLine(m);
            //}

            connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LibraryManagementSystem;Integrated Security=True");
            string query = "SELECT * FROM Member";
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string name = reader["Name"].ToString();
                string cnic = reader["CNIC"].ToString();
                Console.WriteLine($"Name: {name}, CNIC: {cnic}");
            }
            connection.Close();

        }


        public void IssueBook(string cnic, int id)
        {
            connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LibraryManagementSystem;Integrated Security=True");
 
            bool flag = GetIssueStatus(id);
            connection.Open();
            if (flag)
            {
                Console.WriteLine("Already Issued! ");
                connection.Close();
                return;
            }

            string issuebook = $"INSERT into BorrowedBooks (MemberCNIC, BookID) Values('{cnic}','{id}') ";
            string updating = $"UPDATE Book SET IsIssued=1 WHERE BookID='{id}'";
            SqlCommand command1= new SqlCommand(issuebook, connection);
            SqlCommand command2= new SqlCommand(updating, connection); 
            int i=command1.ExecuteNonQuery();
            int j=command2.ExecuteNonQuery();
            if (i > 0 && j > 0)
            {
                books.Clear();
                FillBooks();
                Console.WriteLine("Issued Book");
            }
            else
            {
                Console.WriteLine("Not Avalible");
                connection.Close ();
                return;
            }
            connection.Close();
        }


        public void ReturnBook(string cnic, int id)
        {
            connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LibraryManagementSystem;Integrated Security=True");
            bool flag = GetIssueStatus(id);
 
            if (flag)
            {
                connection.Open();
                string query1 = $"UPDATE Book SET IsIssued=0 where BookID='{id}'";
                SqlCommand command1= new SqlCommand(query1, connection);
                command1.ExecuteNonQuery();
                books.Clear();
                FillBooks();
                Console.WriteLine("Book Returned");
                connection.Close();

            }
            else
            {
                Console.WriteLine("The Book was never issued!");
            }
        }
        public void DeclareMostBorrowedBook()
        {
            connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LibraryManagementSystem;Integrated Security=True");
            connection.Open();
            string query = "SELECT TOP 1 BookID, COUNT(BookID) AS BORROWCOUNT FROM BorrowedBooks GROUP BY BookID ORDER BY BORROWCOUNT DESC"; SqlCommand command= new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                int book_id = reader.GetInt32(0);
                int count= reader.GetInt32(1);
                reader.Close();
                string bookQ = $"SELECT Title, Author FROM Book WHERE BookID = {book_id}"; 
                SqlCommand sqlCommand = new SqlCommand(bookQ, connection);
                SqlDataReader bookreader= sqlCommand.ExecuteReader();
                if (bookreader.Read())
                {
                    string title= bookreader.GetString(0);
                    string author= bookreader.GetString(1);
                    Console.WriteLine($"The most borrowed book is : {title} by {author}. It is borrowed {count} times");

                }
                bookreader.Close();


            }
            else
            {
                Console.WriteLine("No Borrowed Books");
            }
            connection.Close();

        }




        ///Helper Functions
        public bool GetIssueStatus(int id)
        {
            connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LibraryManagementSystem;Integrated Security=True");
            connection.Open();
            bool flag = false;
            string checkQ = $"SELECT IsIssued FROM Book WHERE BookID= '{id}'";
            SqlCommand command = new SqlCommand(checkQ, connection);
            SqlDataReader reader = command.ExecuteReader();
                if (reader.Read()) {
                flag = reader.GetBoolean(0);
                        }
                connection.Close();
            return flag;

        }
    }
}
