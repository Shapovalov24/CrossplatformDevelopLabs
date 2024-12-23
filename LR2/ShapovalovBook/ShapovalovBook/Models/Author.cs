namespace ShapovalovBook.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();

        public Author(string name)
        {
            Name = name;
        }

        public void AddBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            Books.Add(book);
        }

        public int GetBookCount()
        {
            return Books.Count;
        }
    }

}