using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GhostBot.EntityModels;

public class Comment
{
    [Key]
    public int CommentId { get; set; }

    [Required]
    [Column(TypeName = "text")]
    public string Content { get; set; } = null!;

    [Column(TypeName = "integer")]
    public int? PersonId { get; set; }

    [Column(TypeName = "integer")]
    public int? ParentId { get; set; }

    [Column(TypeName = "integer")]
    public int? CategoryId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeletedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CommentDate { get; set; }

    [Column(TypeName = "nvarchar(50)")]
    public string? CommentReplies { get; set; }

    [Column(TypeName = "integer")]
    public int? Category { get; set; }
    
    //One to Many
    // public virtual ICollection<Category>? Categories { get; set; } = new HashSet<Category>();
    //One to One
    public virtual Person? Persons { get; set; } = null!;
}   
