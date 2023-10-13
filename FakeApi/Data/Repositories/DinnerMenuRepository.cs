using FakeApi.Abstractions;
using FakeApi.Dto;

namespace Tests.Data.FakeRepositories;

public class DinnerMenuRepository : IRepository<MenuItem>
{
    private List<MenuItem> List { get; set; }

    public DinnerMenuRepository()
    {
        this.List = this.Data();
    }


    public void Update(MenuItem entity)
    {
        
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
    }

    public void AddRange(IEnumerable<MenuItem> entity)
    {
        this.List.AddRange(entity);
    }

    public void Delete(MenuItem entity)
    {
       
    }

    private List<MenuItem> Data()
    {
        var list =  new List<MenuItem>()
        {
            new ()
            {
                MainCourseName = "Pork Chops",
                Sides = new List<string>()
                {
                    "Green beans",
                    "Mac-n-Cheese"
                }
            },
            new ()
            {
                MainCourseName = "Creamy Italian Ravioli",
                Sides = new List<string>()
                {
                    "Garlic Bread"
                }
            },
            new ()
            {
                MainCourseName = "Sausage, potatoes & green bean stew"
            }
        };
        
        
        list.ForEach(x=>x.Id = list.IndexOf(x)+1);
        return list;
    }
}