using FakeApi.Abstractions;
using FakeApi.Dto;
using Microsoft.EntityFrameworkCore;

namespace FakeApi.Data.Repositories;

public class ComputerRepository:IRepository<ComputerRecord>
{
    private readonly SqlDbContext _context;

    public ComputerRepository(SqlDbContext context)
    {
        _context = context;
    }
    public void Update(ComputerRecord entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public ComputerRecord GetById(int id)
    {
        return _context.Computer.Find(id);
    }

    public IEnumerable<ComputerRecord> GetAll()
    {
        return _context.Computer.ToList();
    }

    public void Add(ComputerRecord entity)
    {
        _context.Computer.Add(entity);
        _context.SaveChanges();
    }

    public void AddRange(IEnumerable<ComputerRecord> entity)
    {
        _context.Computer.AddRange(entity);
        _context.SaveChanges();
    }

    public void Delete(ComputerRecord entity)
    {
        _context.Computer.Remove(entity);
        _context.SaveChanges();
    }
}