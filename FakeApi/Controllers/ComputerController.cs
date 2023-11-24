using Bogus;
using FakeApi.Abstractions;
using FakeApi.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FakeApi.Controllers;

public class ComputerController : BaseController
{
    private readonly IRepository<ComputerRecord> _repo;

    public ComputerController(IRepository<ComputerRecord> repo)
    {
        _repo = repo;
       
    }

    [HttpGet]
    public List<ComputerRecord> All()
    {
        return _repo.GetAll().ToList();
    }
    
   
    
    [HttpPost]
    public IActionResult Delete(ComputerRecord rec)
    {
        this._repo.Delete(rec);
        return NoContent();
    }
    
    [HttpPost]
    public IActionResult Add(ComputerRecord rec)
    {
        this._repo.Add(rec);
        return CreatedAtAction(nameof(Add),  rec);
    }
    
    [HttpPost]
    public IActionResult AddMany(IEnumerable<ComputerRecord> computerRecords)
    {
        this._repo.AddRange(computerRecords);
        return CreatedAtAction(nameof(Add), computerRecords);
    }
}