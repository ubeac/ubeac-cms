//using Entities;
//using Microsoft.AspNetCore.Mvc;
//using Services;

//namespace Controllers;

//public class BaseEntityController<TEntity> : BaseController where TEntity : class, IBaseEntity
//{
//    protected IBaseEntityService<TEntity> Service { get; }

//    public BaseEntityController(IBaseEntityService<TEntity> service)
//    {
//        Service = service;
//    }

//    [HttpPost]
//    public Task Delete([FromBody] Guid id, CancellationToken cancellationToken = default)
//    {
//        return Service.Delete(id, cancellationToken);
//    }

//    [HttpGet]
//    public Task<List<TEntity>> GetAll(CancellationToken cancellationToken = default)
//    {
//        return Service.GetAll(cancellationToken);
//    }

//    [HttpGet]
//    public Task<TEntity> GetById([FromQuery] Guid id, CancellationToken cancellationToken = default)
//    {
//        return Service.GetById(id, cancellationToken);
//    }

//    [HttpPost]
//    public Task<TEntity> Insert([FromBody] TEntity entity, CancellationToken cancellationToken = default)
//    {
//        return Service.Insert(entity, cancellationToken);
//    }

//    [HttpPost]
//    public Task<TEntity> Update([FromBody] TEntity entity, CancellationToken cancellationToken = default)
//    {
//        return Service.Update(entity, cancellationToken);
//    }

//}
