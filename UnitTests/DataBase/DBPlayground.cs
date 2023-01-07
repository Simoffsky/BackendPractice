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
    public async Task UserRepositoryCreate() {
        var context = _dbFactory.CreateDbContext();
        var repo = new UserRepository(context);
        var user = new User("Vasek", "123", 1, "+7914634635", "Vasua Krasnoshek", Role.Patient);
        await repo.Create(user);
        
        Assert.True(await repo.ExistLogin(user.Username));
        await repo.Delete(user.Id);
    }

    [Fact]
    public async Task UserRepositoryNotExists() {
        var context = _dbFactory.CreateDbContext();
        var repo = new UserRepository(context);
        var user = new User("Vasek", "123", 1, "+7914634635", "Vasua Krasnoshek", Role.Patient);
        await repo.Create(user);
        
        Assert.False(await repo.ExistLogin("Sima"));
        await repo.Delete(user.Id);
        
    }
    
    [Fact]
    public async Task UserRepositoryPgTest() {
        // Write here any test
        var context = _dbFactory.CreateDbContext();
        var repo = new UserRepository(context);
    
        var assertList = new List<User>();
        var user = new User("Vasek", "123", 1, "+7914634635", "Vasua Krasnoshek", Role.Patient);
        assertList.Add(user);
        await repo.Create(user);
        user = new User("Pip", "123", 2, "+7914634635", "Pip Krasnoshek", Role.Patient);
        assertList.Add(user);
        await repo.Create(user);
        
    
        var testList = (await repo.List()).ToList();
        for (var i = 0; i < assertList.Count; ++i) {
            Assert.Equal(testList[i].Id, assertList[i].Id);
            Assert.Equal(testList[i].FullName, assertList[i].FullName);
            await repo.Delete(testList[i].Id);
        }
    }
    
    //[Fact]
    public async Task DoctorRepositoryPgTest() {
        // Write here any test
        var context = _dbFactory.CreateDbContext();
        var repo = new DoctorRepository(context);
        var spec = new Specialization(1, "Proktolog");
        await repo.Create(new Doctor(1, "Vasua", spec));
        var list = (await repo.GetBySpec(spec)).ToList();
        Assert.Equal(list[0].Id, 1);
    }
    
    [Fact]
    public async Task ScheduleRepositoryPgTest() {
        var context = _dbFactory.CreateDbContext();
        var repo = new ScheduleRepository(context);
        var schedule = new Schedule(1, 1,
            new DateTime(2022, 12, 15, 15, 0, 0, 0),
            new DateTime(2022, 12, 15, 15, 30, 0, 0)); // half hour difference
        
        await repo.Create(schedule);
    
        var spec = new Specialization(1, "Proktolog");
        var test = repo.GetScheduleByDate(new Doctor(1, "Vasua", spec), new DateOnly(2022, 12, 15)).Result.ToList()[0];
        
        Assert.True(test.Id == schedule.Id && test.DoctorId == schedule.DoctorId);
        
        await repo.Delete(schedule.Id);
    }
}
