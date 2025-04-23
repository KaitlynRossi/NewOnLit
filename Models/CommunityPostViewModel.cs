namespace OnLit.Models
{
    public class CommunityPostViewModel
    {
        public int    PostID      { get; set; }
        public string PostTitle   { get; set; } = string.Empty;
        public int    PostRating  { get; set; }
        public string PostContent { get; set; } = string.Empty;
        public string UserName    { get; set; } = string.Empty;
    }
}
