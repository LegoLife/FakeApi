using FakeApi.Dto;

namespace FakeApi.Abstractions;

public interface IId
{
    int Id { get; }
}

public interface IRepository<T>
{
    void Update(T entity);

    T GetById(int id);
    IEnumerable<T> GetAll();
    void Add(T entity);
    void AddRange(IEnumerable<T> entity);
    void Delete(T entity);
}