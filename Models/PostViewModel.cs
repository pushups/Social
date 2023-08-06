namespace Social.Models;

public class PostViewModel
{
    public required Post Post;
    public required User User;
    public IEnumerable<Comment>? Comments;
}