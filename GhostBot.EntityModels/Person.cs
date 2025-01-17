using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GhostBot.EntityModels;

public class Person
{
    [Key]
    [Column(TypeName = "nchar (5)")]
    [StringLength(5)]
    [RegularExpression("[A-Z]{5}")]
    public string PersonId { get; set; } = null!;

    [Column(TypeName = "nvarchar(50)")]
    public string FirstName { get; set; } = null!;

    [Column(TypeName = "nvarchar(50)")]
    public string LastName { get; set; } = null!;

    [NotMapped]
    [Column(TypeName = "image")]
    public byte[]? Avatar { get; set; }

    [Column(TypeName = "nvarchar(50)")]
    public string? Comments { get; set; }

    [Column(TypeName = "nvarchar(50)")]
    public string? Address { get; set; }

    [Column(TypeName = "nvarchar(50)")]
    public string? City { get; set; }
    public string? Region { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
    public string? AuthenticationLevel { get; set; }
    public bool? IsSSO { get; set; }
    public int? AnonymousPosts { get; set; }
    public int? EtherealThreads { get; set; }
    public string? Notes { get; set; }
}
