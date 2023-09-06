using Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Repositories;
using Services;
using System.Reflection;

namespace Controllers;

[ApiController]
[Route("api/[controller]/{contentType}/[action]")]
[Produces("application/json")]
public class BaseContentController<TEntity> : ControllerBase where TEntity : class, IBaseContent
{

    protected IBaseContentService<TEntity> Service { get; }

    public BaseContentController(IBaseContentService<TEntity> service)
    {
        Service = service;
    }

    [HttpPost]
    public Task Delete([FromBody] Guid id, CancellationToken cancellationToken = default)
    {
        return Service.Delete(id, cancellationToken);
    }

    [HttpGet]
    public async Task<List<TEntity>> GetAll([FromRoute] string contentType, CancellationToken cancellationToken = default)
    {
        return await Service.GetAll(contentType, cancellationToken);
    }

    [HttpGet]
    public Task<TEntity> GetById([FromQuery] Guid id, CancellationToken cancellationToken = default)
    {
        return Service.GetById(id, cancellationToken);
    }

    [HttpPost]
    public Task<TEntity> Insert([FromRoute] string contentType, [FromBody] IBaseContent entity, CancellationToken cancellationToken = default)
    {
        //return  Service.Insert(entity, cancellationToken);
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task<TEntity> Update([FromRoute] string contentType, [FromBody] TEntity entity, CancellationToken cancellationToken = default)
    {
        return await Service.Update(entity, cancellationToken);
    }

    //public IHttpActionResult InsertData([FromBody] dynamic model)
    //{
    //    Type t = resolve type base on context information
    //object data = create an instance of t base on the model values;

    //    var method = this.GetType().GetMethod(nameof(InsertDataPrivate),
    //        BindingFlags.NonPublic | BindingFlags.Instance);
    //    var result = (int)method.MakeGenericMethod(t)
    //       .Invoke(this, new object[] { data });

    //    return Ok(result);
    //}
    //private int InsertDataPrivate<T>(T model) where T
    //{
    //    //Write the generic code here, for example:
    //    dbContext.Set<T>().Add(model);
    //    dbContext.SaveChanges();
    //    return some value;
    //}

}
