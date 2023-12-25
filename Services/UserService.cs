
using AgendaMedica.Models;

public class UserService{
    private readonly ApplicationDbContext _dbContext;

    public UserService(ApplicationDbContext context){
        _dbContext = context;
    }

    public User GetUserByEmail(string email){
        return _dbContext.users.FirstOrDefault(p=>p.Email==email);
    }
}