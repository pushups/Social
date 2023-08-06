namespace Social.Models;

public class UserViewModel
{
    public required User User;
    public IEnumerable<Album>? Albums;
}