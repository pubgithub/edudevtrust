namespace BookLibraryApi.Models
{
    /// <summary>
    /// Represents a book in the library.
    /// </summary>
    public class Book : IBook
    {
        /// <summary>
        /// Gets or sets the unique identifier for the book.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title of the book.
        /// </summary>
        public required string Title { get; set; }

        /// <summary>
        /// Gets or sets the author of the book. This property is optional.
        /// </summary>
        public string? Author { get; set; }

        /// <summary>
        /// Gets or sets the year the book was published.
        /// </summary>
        public int Year { get; set; }
    }
}