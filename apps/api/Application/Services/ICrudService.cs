using Persistence.Repository;

namespace Application.Services;

public interface ICrudService<TModel, TCreate, TUpdate>
{
    public Task<TModel> FindOne(int id);
    public Task<ICollection<TModel>> FindMany();
    public Task<ICollection<TModel>> FindMany(IPageable pageable);
    public Task<TModel> CreateOne(TCreate createPayload);
    public Task<TModel> UpdateOne(int id, TUpdate updatePayload);
    public Task<int> Count();
    public Task DeleteOne(int id);
}
