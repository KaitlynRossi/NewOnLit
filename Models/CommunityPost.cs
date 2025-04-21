using System.ComponentModel.DataAnnotations;
namespace OnLit.Models
{
    public class CommunityPost
    {
        [Key] 
        public int PostID { get; set; }
        public string PostTitle { get; set; } = string.Empty;
        public int PostRating { get; set; }
        public string PostContent { get; set; } = string.Empty;
        public string userID { get; set; } = string.Empty;
    }
}
