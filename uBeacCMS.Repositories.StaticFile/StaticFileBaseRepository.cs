using Microsoft.Extensions.Options;
using System.Text.Json;
using uBeacCMS.Models;

namespace uBeacCMS.Repositories.StaticFile;

public class StaticFileBaseRepository<T> : IBaseRepository<T> where T : IBaseEntity
{
    private readonly string folder;

    public StaticFileBaseRepository(IOptions<StaticFileRepositorySettings> options)
    {
        folder = options.Value.Folder;
    }

    public virtual async Task Add(T entity, CancellationToken cancellationToken = default)
    {
        var values = await GetAll(cancellationToken).ConfigureAwait(false);

        values.Add(entity);

        await WriteToFile(values, cancellationToken);

    }

    public virtual async Task Delete(Guid id, CancellationToken cancellationToken = default)
    {
        var values = await GetAll(cancellationToken).ConfigureAwait(false);

        var entity = values.Where(x => x.Id == id).FirstOrDefault();

        if (entity != null)
        {
            values.Remove(entity);
            await WriteToFile(values, cancellationToken);
        }
    }

    public virtual async Task<IList<T>> GetAll(CancellationToken cancellationToken = default)
    {
        if (!File.Exists(GetFilePath()))
            return new List<T>();

        using FileStream openStream = File.OpenRead(GetFilePath());
        var values = await JsonSerializer.DeserializeAsync<IList<T>>(openStream, cancellationToken: cancellationToken);
        return values?? new List<T>();
    }

    public virtual async Task<T> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var values = await GetAll(cancellationToken).ConfigureAwait(false);
        return values.Where(x => x.Id == id).First();
    }

    public virtual async Task Update(T entity, CancellationToken cancellationToken = default)
    {
        var values = await GetAll(cancellationToken).ConfigureAwait(false);
        var oldEntity = values.Where(x => x.Id == entity.Id).First();
        values.Remove(oldEntity);
        values.Add(entity);
        await WriteToFile(values, cancellationToken);
    }

    public virtual async Task AddRange(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        var values = await GetAll(cancellationToken).ConfigureAwait(false);
        foreach (var item in entities)
        {
            values.Add(item);
        }
        await WriteToFile(values, cancellationToken);
    }


    protected virtual async Task WriteToFile(IList<T> values, CancellationToken cancellationToken = default)
    {
        using FileStream createStream = File.Create(GetFilePath());
        await JsonSerializer.SerializeAsync(createStream, values, cancellationToken: cancellationToken);
        await createStream.DisposeAsync();
    }

    protected virtual string GetFilePath()
    {
        return folder + "\\" + typeof(T).Name + ".json";
    }

}

