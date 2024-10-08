namespace BookLibraryApi.Models
{
    /// <summary>
    /// Represents the interface for a book.
    /// </summary>
    public interface IBook
    {
        /// <summary>
        /// Gets or sets the unique identifier for the book.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Gets or sets the title of the book.
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Gets or sets the author of the book. This property is optional.
        /// </summary>
        string? Author { get; set; }

        /// <summary>
        /// Gets or sets the year the book was published.
        /// </summary>
        int Year { get; set; }
    }
}