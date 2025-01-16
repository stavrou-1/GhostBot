using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GhostBot.EntityModels;

public class Category
{
    [Key]
    public int CategoryId { get; set; }

    [Column(TypeName = "nvarchar(20)")]
    public string CategoryName { get; set; } = null!;

    [Column(TypeName = "text")]
    public string? Description { get; set; }

    [Column(TypeName = "image")]
    public byte[]? Picture { get; set; }
}
