using Application.DTO;
using Application.Errors;
using Application.Models;
using Persistence.Repository;

namespace Application.Services
{
    public class WorkspaceService : IWorkspaceService
    {
        // ------------------------------------------------------------ //
        // PROPERTIES
        // ------------------------------------------------------------ //

        protected IRepository<Workspace> WorkspaceRepository { get; set; }

        // ------------------------------------------------------------ //
        // CONSTRUCTORS
        // ------------------------------------------------------------ //

        public WorkspaceService(IRepository<Workspace> workspaceRepository)
        {
            WorkspaceRepository = workspaceRepository;
        }

        // ------------------------------------------------------------ //
        // METHODS
        // ------------------------------------------------------------ //

        public async Task<Workspace> FindOne(int id)
        {
            return await WorkspaceRepository.FindOne(id) ?? throw new WorkspaceNotFoundById(id);
        }

        public async Task<ICollection<Workspace>> FindMany()
        {
            return await WorkspaceRepository.FindMany(null, null, new Pageable());
        }

        public async Task<ICollection<Workspace>> FindMany(IPageable pageable)
        {
            return await WorkspaceRepository.FindMany(null, null, pageable);
        }

        public Task<Workspace> CreateOne(EditWorkspaceDTO createPayload)
        {
            Workspace workspace = new() { Name = createPayload.Name };
            return WorkspaceRepository.Save(workspace);
        }

        public async Task<Workspace> UpdateOne(int id, EditWorkspaceDTO updatePayload)
        {
            Workspace workspace = await FindOne(id);

            if (updatePayload.Name != null)
            {
                workspace.Name = updatePayload.Name;
            }

            return await WorkspaceRepository.Save(workspace);
        }

        public async Task DeleteOne(int id)
        {
            _ = await FindOne(id);
            await WorkspaceRepository.DeleteOne(id);
        }

        public Task<int> Count()
        {
            return WorkspaceRepository.Count();
        }
    }
}
