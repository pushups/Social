namespace Social.Models;

public class PostViewModel
{
    public Post Post;
    public User User;
    public IEnumerable<Comment> Comments;
}