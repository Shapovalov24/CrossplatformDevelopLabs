namespace ShapovalovBook.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public int AuthorId { get; set; }

        public Book(string title, int year, int authorId)
        {
            Title = title;
            Year = year;
            AuthorId = authorId;

            Validate();
        }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Title))
                throw new ArgumentException("Title cannot be empty");
            if (Year <= 0 || Year > DateTime.Now.Year)
                throw new ArgumentException("Year must be valid");
        }
    }

}