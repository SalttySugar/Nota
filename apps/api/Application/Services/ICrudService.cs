namespace Application.Services;

public interface ICrudService<TId, TModel, TCreate, TUpdate>
{
    public Task<TModel> FindOne(TId id);
    public Task<ICollection<TModel>> FindMany();
    public Task<ICollection<TModel>> FindMany(IPageable pageable);
    public Task<TModel> CreateOne(TCreate createPayload);
    public Task<TModel> UpdateOne(TId id, TUpdate updatePayload);
    public Task DeleteOne(TId id);
}
