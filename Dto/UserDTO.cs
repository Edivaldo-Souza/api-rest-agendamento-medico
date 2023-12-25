namespace AgendaMedica.Dto;
public class UserDTO{
    public UserDTO(string name, string cpf, string email, string vocation)
    {
        this.Name = name;
        this.Cpf = cpf;
        this.Email = email;
        this.Vocation = vocation;
    }
    public string? Name{get;set;}
    public string? Cpf{get;set;}
    public string? Email{get;set;}
    public string? Vocation{get;set;}

}