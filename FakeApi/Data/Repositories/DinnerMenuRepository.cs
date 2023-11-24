using FakeApi.Abstractions;
using FakeApi.Dto;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace FakeApi.Data.Repositories;

public class DinnerMenuRepository : IRepository<MenuItem>
{
    private List<MenuItem> List { get; set; }

    public DinnerMenuRepository()
    {
        this.List = this.Data();
    }
    

    public MenuItem GetById(int id)
    {
        return List.FirstOrDefault(x => x.Id == id);
    }

    public IEnumerable<MenuItem> GetAll()
    {
        return this.List;
    }

    public void Add(MenuItem entity)
    {
        this.List.Add(entity);
        UpdateFile();
    }

    public void AddRange(IEnumerable<MenuItem> entity)
    {
        this.List.AddRange(entity);
        UpdateFile();
    }
    

    public void Delete(MenuItem entity)
    {
        var found = this.List.FirstOrDefault(x => x.Id == entity.Id);
        if (found != null)
            this.List.Remove(found);
        UpdateFile();

    }

    private void UpdateFile()
    {
        var serialized = JsonConvert.SerializeObject(this.List, Formatting.Indented);
        var path = this.JsonFilePath();
        if(File.Exists(path))
            File.Delete(path);
        File.WriteAllText(path,serialized);
    }

    private string JsonFilePath()
    {
        return $"{Environment.CurrentDirectory}\\Data\\menuItems.json";
    }
    private List<MenuItem> Data()
    {
        var text = File.ReadAllText(JsonFilePath());
        var list = JsonConvert.DeserializeObject<List<MenuItem>>(text);
        list.ForEach(x=>x.Id = list.IndexOf(x)+1);
        return list;
    }
}