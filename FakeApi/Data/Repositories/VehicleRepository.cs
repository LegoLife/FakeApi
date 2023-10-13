using FakeApi.Abstractions;
using FakeApi.Dto;
using Microsoft.EntityFrameworkCore;

namespace FakeApi.Data.Repositories;

public class VehicleRepository:IRepository<VehicleRecord>
{
    private readonly SqlDbContext _context;

    public VehicleRepository(SqlDbContext context)
    {
        _context = context;
    }
    public void Update(VehicleRecord entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public VehicleRecord GetById(int id)
    {
        return _context.Vehicle.Find(id);
    }

    public IEnumerable<VehicleRecord> GetAll()
    {
        return _context.Vehicle.ToList();
    }

    public void Add(VehicleRecord entity)
    {
        _context.Vehicle.Add(entity);
        _context.SaveChanges();
    }

    public void AddRange(IEnumerable<VehicleRecord> entity)
    {
        _context.Vehicle.AddRange(entity);
        _context.SaveChanges();
    }

    public void Delete(VehicleRecord entity)
    {
        _context.Vehicle.Remove(entity);
        _context.SaveChanges();
    }
}