using FakeApi.Abstractions;
using FakeApi.Dto;

namespace Tests.Data.FakeRepositories;

public class FakeVehicleRepository : IRepository<VehicleRecord>
{
    private readonly List<VehicleRecord> dataSet = new();

    public void Update(VehicleRecord entity)
    {
        
    }

    public VehicleRecord GetById(int id)
    {
        return dataSet.FirstOrDefault(x => x.Id == id);
    }

    public void Update(ComputerRecord entity)
    {
        // var found = this.GetById(entity.Id);
        // new FakerHelper().CopyAToB(entity,found);
    }
    
    public IEnumerable<VehicleRecord> GetAll()
    {
        return dataSet.ToList();
    }

    public void Add(VehicleRecord entity)
    {
        this.dataSet.Add(entity);
    }

    public void AddRange(IEnumerable<VehicleRecord> entity)
    {
        this.dataSet.AddRange(entity);
    }
    

    public void Delete(VehicleRecord entity)
    {
        dataSet.Remove(entity);
    }
}