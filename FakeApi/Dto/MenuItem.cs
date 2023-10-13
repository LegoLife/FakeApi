namespace FakeApi.Dto;


public class MenuItem
{
    public int Id { get; set; }
    public string MainCourseName { get; set; }
    public IEnumerable<string> Sides { get; set; } = new List<string>();
}
