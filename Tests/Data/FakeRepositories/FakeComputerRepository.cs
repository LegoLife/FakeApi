using FakeApi.Abstractions;
using FakeApi.Dto;

namespace Tests.Data.FakeRepositories;

public class FakeComputerRepository : IRepository<ComputerRecord>
{
    private readonly List<ComputerRecord> dataSet = new();

    public ComputerRecord GetById(int id)
    {
        return dataSet.FirstOrDefault(x => x.Id == id);
    }

    public void Update(ComputerRecord entity)
    {
        // var found = this.GetById(entity.Id);
        // new FakerHelper().CopyAToB(entity,found);
    }
    
    public IEnumerable<ComputerRecord> GetAll()
    {
        return dataSet.ToList();
    }

    public void Add(ComputerRecord entity)
    {
        this.dataSet.Add(entity);
    }

    public void AddRange(IEnumerable<ComputerRecord> entity)
    {
        this.dataSet.AddRange(entity);
    }
    

    public void Delete(ComputerRecord entity)
    {
        dataSet.Remove(entity);
    }
}