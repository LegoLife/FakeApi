using FakeApi.Abstractions;
using FakeApi.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FakeApi.Controllers;

public class DinnerMenuController:BaseController
{
    private readonly IRepository<MenuItem> _repo;

    public DinnerMenuController(IRepository<MenuItem> repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public List<MenuItem> All()
    {
        return _repo.GetAll().ToList();
    }
    
    [HttpPost]
    public IActionResult Delete(MenuItem rec)
    {
        this._repo.Delete(rec);
        return NoContent();
    }
    
    [HttpPost]
    public IActionResult Add(MenuItem rec)
    {
        this._repo.Add(rec);
        return CreatedAtAction(nameof(Add),  rec);
    }
    
    [HttpPost]
    public IActionResult AddMany(IEnumerable<MenuItem> computerRecords)
    {
        this._repo.AddRange(computerRecords);
        return CreatedAtAction(nameof(Add), computerRecords);
    }
}