using DataBase;
using DataBase.Repositories;
using Domain.Models;

namespace UnitTests; 

public class DBPlayground {
    private readonly ApplicationContextFactory _dbFactory;

    public DBPlayground() {
        _dbFactory = new ApplicationContextFactory();
    }

    [Fact]
    public void UserRepositoryCreate() {
        var context = _dbFactory.CreateDbContext();
        var repo = new UserRepository(context);
        var user = new User("Vasek", "123", 1, "+7914634635", "Vasua Krasnoshek", Role.Patient);
        repo.Create(user);
        context.SaveChanges();
        Assert.True(repo.ExistLogin(user.Username));
        repo.Delete(user.Id);
        context.SaveChanges();
    }
    
    [Fact]
    public void UserRepositoryNotExists() {
        var context = _dbFactory.CreateDbContext();
        var repo = new UserRepository(context);
        var user = new User("Vasek", "123", 1, "+7914634635", "Vasua Krasnoshek", Role.Patient);
        repo.Create(user);
        context.SaveChanges();
        Assert.False(repo.ExistLogin("Sima"));
        repo.Delete(user.Id);
        context.SaveChanges();
    }
    
    [Fact]
    public void UserRepositoryPgTest() { // Write here any test
        var context = _dbFactory.CreateDbContext();
        var repo = new UserRepository(context);
        
        var assertList = new List<User>();
        var user = new User("Vasek", "123", 1, "+7914634635", "Vasua Krasnoshek", Role.Patient);
        assertList.Add(user);
        repo.Create(user);
        user = new User("Pip", "123", 2, "+7914634635", "Pip Krasnoshek", Role.Patient);
        assertList.Add(user);
        repo.Create(user);
        
        context.SaveChanges();
        
        var testList = repo.List().ToList();
        for (int i = 0; i < assertList.Count; ++i) {
            Assert.Equal(testList[i].Id, assertList[i].Id);
            Assert.Equal(testList[i].FullName, assertList[i].FullName);
            repo.Delete(testList[i].Id);
        }
        context.SaveChanges();
    }
    
    //[Fact]
    public void DoctorRepositoryPgTest() { // Write here any test
        var context = _dbFactory.CreateDbContext();
        var repo = new DoctorRepository(context);
        var spec = new Specialization(1, "Proktolog");
        repo.Create(new Doctor(1, "Vasua", spec));
        context.SaveChanges();
        var list = repo.GetBySpec(spec).ToList();
        Assert.Equal(list[0].Id, 1);
    }
}