using Bogus;
using FakeApi.Abstractions;
using FakeApi.Controllers;
using FakeApi.Dto;
using FakeApi.Utils;
using Microsoft.AspNetCore.Mvc;
using NuGet.Frameworks;
using NUnit.Framework.Internal;

namespace Tests;

public class ComputerRecordsTests
{
    private IRepository<ComputerRecord> repo;
    private int recordCt = 25;
    [SetUp]
    public void Init()
    {
        repo = new FakeRepo<ComputerRecord>();
        repo.AddRange(new FakerHelper().ComputerRecords(recordCt));
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
    public void AllDataPopulated()
    {
        var ctlr = new ComputerController(this.repo);
        var res = ctlr.Data();
        Assert.IsTrue(res.All(x => x.Id > 0));

        Assert.IsTrue(res.All(x => !string.IsNullOrEmpty(x.Name)));
        Assert.IsTrue(res.All(x => !string.IsNullOrEmpty(x.IpAddress)));
        
        Assert.IsTrue(res.All(x => !string.IsNullOrEmpty(x.Avatar)));
        Assert.IsTrue(res.All(x => !string.IsNullOrEmpty(x.Mac)));
        Assert.IsTrue(res.All(x => !string.IsNullOrEmpty(x.Url)));
        Assert.IsTrue(res.All(x => !string.IsNullOrEmpty(x.DomainSuffix)));




    }
    
    [Test]
    public void DeleteWorks()
    {
        var ctlr = new ComputerController(this.repo);
        var old = this.repo.GetAll().First();
        ctlr.Delete(old);
        var records = this.repo.GetAll();
        Assert.IsTrue(records.All(x => x.Id != old.Id));
    }
    
    [Test]
    public void AddWorks()
    {
        var ctlr = new ComputerController(this.repo);
        ctlr.Add(new ComputerRecord()
        {
            Id = 999,
            Name = "A"
        });
        var records = this.repo.GetAll();

        Assert.IsTrue(records.Any(x => x.Id ==999 &&x.Name=="A"));
    }

    [Test]
    public void AddMultipleWorks()
    {
        var recs = new FakerHelper().ComputerRecords(recordCt).ToList();
        var ctlr = new ComputerController(this.repo);
        ctlr.AddMany(recs);
        var data = this.repo.GetAll().ToList();
        foreach(var item in recs)
            Assert.IsTrue(data.Any(x=>x.Id==item.Id));
    }
}