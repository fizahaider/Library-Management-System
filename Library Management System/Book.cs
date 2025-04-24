
namespace Library_Management_System
{
    public class Book
    {
        private string title;
        private string author;
        private bool isIssued;

        public Book(string title, string author)
        {
            this.title = title;
            this.author = author;
            isIssued = false;
        }
        public string Title { get { return title; } set { title = value; } }
        public string Author { get { return author; } set { author = value; } }
        public bool IsIssued { get {    return isIssued; }  }

        public void IssueBook()
        {
            isIssued=true;
        }
        public void SetIssuedStatus(bool status)
        {
            isIssued = status;
        }
        public void ReturnBook()
        {
            isIssued = false;
        }
        public override string ToString()
        {
            string t = Title;
            string a = Author;
            if (IsIssued)
            {
                return $"Title: {t}, Author:{a}, Already Issued";
            }
            return $", Title: {t}, Author:{a}, Not Issued";

        }

    }
}
