using FakeApi.Dto;
using FakeApi.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace FakeApi.Abstractions;

public class FakeRepo<T> :IRepository<T> where T: class, IId
{
    private readonly List<T> dataSet = new();

    public T GetById(int id)
    {
        return dataSet.FirstOrDefault(x => x.Id == id);
    }

    public void Update(T entity)
    {
         // var found = this.GetById(entity.Id);
         // new FakerHelper().CopyAToB(entity,found);
    }
    
    public IEnumerable<T> GetAll()
    {
        return dataSet.ToList();
    }

    public void Add(T entity)
    {
        this.dataSet.Add(entity);
    }

    public void AddRange(IEnumerable<T> entity)
    {
        this.dataSet.AddRange(entity);
    }
    

    public void Delete(T entity)
    {
        dataSet.Remove(entity);
    }
}