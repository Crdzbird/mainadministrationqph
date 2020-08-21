using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Interfaces;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Services
{
    public class TreeService : ITreeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TreeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Tree> GetHierarchyTreeByUserId(int userId) => await _unitOfWork.TreeRepository.GetTreeByUserId(userId);
    }
}