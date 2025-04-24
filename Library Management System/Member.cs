using Microsoft.Data.SqlClient;

namespace Library_Management_System
{
    public class Member
    {
        private string name;
        private string cnic;
        private List<int>BorrowedItems;
        public Member(string name, string cnic)
        {
            this.name = name;
            this.cnic = cnic;
            BorrowedItems= new List<int>(); 
            loadborrowed(cnic);
        }

        public void loadborrowed(string cnic)
        {
            SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LibraryManagementSystem;Integrated Security=True");
            string query = $"SELECT BookID from BorrowedBooks WHERE MemberCNIC='{cnic}'";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int id_book = reader.GetInt32(0);                
               BorrowedItems.Add(id_book);
            }
            connection.Close();
        }

        public string CNIC { get { return cnic; } }
        public string Name { get { return name; } set { name = value; } }
        public bool HasBorrowed(int bookid) {
            int id_book = 0;
            SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LibraryManagementSystem;Integrated Security=True");
            connection.Open();
            string query = $"SELECT COUNT(*) FROM BorrowedBooks WHERE MemberCNIC='{cnic}' AND BookID='{bookid}'";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();
            if(reader.Read())
            {
               id_book = reader.GetInt32(0);
            }
            connection.Close();

            return id_book > 0;


        }

        public override string ToString()
        {
            return $"Name: {name}     CNIC: {cnic}";
        }
    }
}
