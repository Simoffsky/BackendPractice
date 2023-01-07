using DataBase.Converters;
using DataBase.Models;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Repositories; 

public class UserRepository : IUserRepository {
    private readonly ApplicationContext _context;

    public UserRepository(ApplicationContext context) {
        _context = context;
    }
    
    public async Task<User> Create(User item) {
        await _context.Users.AddAsync(item.ToModel());
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task<User> Get(int id) {
        var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
        return user.ToDomain();
    }
    
    public async Task<IEnumerable<User>> List() {
        return await _context.Users.Select(userModel => userModel.ToDomain()).ToListAsync();
    }

    public async Task<bool> Exists(int id) {
        return await _context.Users.AnyAsync(user => user.Id == id);
    }

    public async Task<bool> Delete(int id) {
        var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
        if (user == default)
            return false; // not deleted
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public bool IsValid(User entity) {

        if (entity.Id < 0)
            return false;
        
        if (string.IsNullOrEmpty(entity.Username) || string.IsNullOrEmpty(entity.Password))
            return false;
        
        if (string.IsNullOrEmpty(entity.PhoneNumber) || string.IsNullOrEmpty(entity.FullName))
            return false;

        return true;
    }

    public async Task<User> Update(User entity) {
        _context.Users.Update(entity.ToModel());
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> ExistLogin(string login, string password) {
        return await _context.Users.AnyAsync(user => user.Username == login && user.Password == password);
    }

    public async Task<bool> ExistLogin(string login) {
        return await _context.Users.AnyAsync(user => user.Username == login);
    }

    public async Task<User?> GetByLogin(string login) {
        var user = await _context.Users.FirstOrDefaultAsync(user => user.Username == login);
        return user?.ToDomain();
    }
}