﻿namespace FakeApi.Abstractions;

public interface IRepository<T> where T:class
{
    T GetById(int id);
    IEnumerable<T> GetAll();
    void Add(T entity);
    void AddRange(IEnumerable<T> entity);
    void Delete(T entity);
}