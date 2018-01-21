using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Portfolio.Models
{
    [Table("Comments")]
    public class Comment
    {
        public Comment(int thisId, string thisAuthor, string thisNote)
        {
            CommentId = thisId;
            Author = thisAuthor;
            Note = thisNote;
        }

        public Comment()
        {

        }

        [Key]
        public int CommentId { get; set; }
        public string Author { get; set; }
        public string Note { get; set; }
        public int PostId { get; set; }
        public virtual Post Posts { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}
