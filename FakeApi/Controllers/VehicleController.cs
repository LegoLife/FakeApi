using FakeApi.Abstractions;
using FakeApi.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FakeApi.Controllers;

public class VehicleController : BaseController
{
    private readonly IRepository<VehicleRecord> _repo;

    public VehicleController(IRepository<VehicleRecord> repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public List<VehicleRecord> All()
    {
        return _repo.GetAll().ToList();
    }

    [HttpPost]
    public IActionResult Delete(VehicleRecord rec)
    {
        this._repo.Delete(rec);
        return NoContent();
    }
    
    [HttpPost]
    public IActionResult Add(VehicleRecord rec)
    {
        this._repo.Add(rec);
        return CreatedAtAction(nameof(Add),  rec);
    }
    
    [HttpPost]
    public IActionResult AddMany(IEnumerable<VehicleRecord> computerRecords)
    {
        this._repo.AddRange(computerRecords);
        return CreatedAtAction(nameof(Add), computerRecords);
    }

}