using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaMedica.Models;

public class User{
    public User(string name,string cpf, string email, string password, string vocation){
        Name = name;
        Cpf = cpf;
        Email = email;
        Password = password;
        Vocation = vocation;
    }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id{get;set;}
    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string? Name{get;set;}
     [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string? Cpf{get;set;}
     [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string? Email{get;set;}
     [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string? Password{get;set;}
     [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string? Vocation{get;set;}
    [Column(TypeName = "varbinary(max)")]
    public byte[] ProfilePicture {get;set;}

}