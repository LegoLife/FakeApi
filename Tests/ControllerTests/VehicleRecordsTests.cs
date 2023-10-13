using FakeApi.Abstractions;
using FakeApi.Controllers;
using FakeApi.Dto;
using Tests.Data.FakeRepositories;
using Tests.Utils;

namespace Tests.ControllerTests;

public class VehicleRecordsTests
{
    private IRepository<VehicleRecord> repo;
    private int recordCt = 25;
    [SetUp]
    public void Init()
    {
        repo = new FakeVehicleRepository();
        repo.AddRange(new FakerHelper().VehicleRecords(recordCt));
    }

    [Test]
    public void RecordsNotEmpty()
    {
        var computerRecords = repo.GetAll().ToList();
        Assert.IsTrue(computerRecords.Any());
        Assert.IsTrue(computerRecords.Count()==recordCt);
    }

    [Test]
    public void FindWorks()
    {
        var last = this.repo.GetAll().ToList().Last();
        var found = this.repo.GetById(last.Id);
        Assert.IsTrue(found!=null);
    }
    
    [Test]
    public void DataPopulated()
    {
        var ctlr = new VehicleController(this.repo);
        var res = ctlr.All();
        
        Assert.IsTrue(res.All(x => x.Id>0));

        Assert.IsTrue(res.All(x => !string.IsNullOrEmpty(x.Vin)));
        Assert.IsTrue(res.All(x => !string.IsNullOrEmpty(x.Manufacturer)));
        Assert.IsTrue(res.All(x => !string.IsNullOrEmpty(x.Fuel)));
        Assert.IsTrue(res.All(x => !string.IsNullOrEmpty(x.Model)));
        Assert.IsTrue(res.All(x => !string.IsNullOrEmpty(x.Type)));
        Assert.IsTrue(res.All(x => !string.IsNullOrEmpty(x.Model)));

    }
    
    [Test]
    public void DeleteWorks()
    {
        var ctlr = new VehicleController(this.repo);
        var old = this.repo.GetAll().First();
        ctlr.Delete(old);
        var records = this.repo.GetAll();
        Assert.IsTrue(records.All(x => x.Id != old.Id));
    }
    
    [Test]
    public void AddWorks()
    {
        var ctlr = new VehicleController(this.repo);
        ctlr.Add(new VehicleRecord()
        {
            Id=999,
            Manufacturer = "A"
        });
        var records = this.repo.GetAll();

        Assert.IsTrue(records.Any(x => x.Id ==999 &&x.Manufacturer=="A"));
    }
    
    [Test]
    public void AddMultipleWorks()
    {
        var recs = new FakerHelper().VehicleRecords(recordCt).ToList();
        var ctlr = new VehicleController(this.repo);
        ctlr.AddMany(recs);
        var data = this.repo.GetAll().ToList();
        foreach(var item in recs)
            Assert.IsTrue(data.Any(x=>x.Id==item.Id));
    }
}