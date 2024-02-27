namespace Хакатон.Models;
public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int AuthorId { get; set; }
    public int GenreId { get; set; }
    public int PublisherId { get; set; }
    public DateTime PublicationDate { get; set; }

    public Author Author { get; set; }
    public Genre Genre { get; set; }
    public Publisher Publisher { get; set; }
}