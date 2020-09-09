using Microsoft.Extensions.Options;
using OrderByExtensions;
using QPH_MAIN.Core.CustomEntities;
using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Interfaces;
using QPH_MAIN.Core.QueryFilters;
using System.Linq;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Services
{
    public class ViewService : IViewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public ViewService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public async Task<Views> GetView(int id) => await _unitOfWork.ViewRepository.GetById(id);

        public PagedList<Views> GetViews(ViewQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var views = _unitOfWork.ViewRepository.GetAll();
            if (filters.filter != null)
            {
                views = views.Where(x => x.code.ToLower().Contains(filters.filter.ToLower()));
                views = views.Where(x => x.name.ToLower().Contains(filters.filter.ToLower()));
                views = views.Where(x => x.route.ToLower().Contains(filters.filter.ToLower()));
            }
            if (filters.Code != null)
            {
                views = views.Where(x => x.code == filters.Code);
            }
            if (filters.Route != null)
            {
                views = views.Where(x => x.route == filters.Route);
            }
            if (filters.Name != null)
            {
                views = views.Where(x => x.name.ToLower().Contains(filters.Name.ToLower()));
            }
            if (filters.orderedBy != null && filters.orderedBy.Count() > 0)
            {
                foreach (var sortM in filters.orderedBy)
                {
                    views = views.OrderBy(sortM.PairAsSqlExpression);
                }
            }
            var pagedPosts = PagedList<Views>.Create(views, filters.PageNumber, filters.PageSize);
            return pagedPosts;
        }

        public async Task InsertView(Views views)
        {
            await _unitOfWork.ViewRepository.Add(views);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> DeleteView(int id)
        {
            await _unitOfWork.ViewRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateView(Views views)
        {
            var existingView = await _unitOfWork.ViewRepository.GetById(views.Id);
            existingView.name = views.name;
            existingView.code = views.code;
            existingView.route = views.route;
            _unitOfWork.ViewRepository.Update(existingView);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RebuildHierarchy(Tree tree, int idUser)
        {
            await _unitOfWork.UserViewRepository.Add(new UserView { userId = idUser , children = tree.son, parent = tree.parent });
            if(tree.cards != null && tree.cards.Count > 0)
            {
                foreach (var card in tree.cards)
                {
                    await _unitOfWork.UserCardGrantedRepository.Add(new UserCardGranted { id_card = card.Id, id_user = idUser });
                    await _unitOfWork.SaveChangesAsync();
                    foreach (var permission in tree.permissions)
                    {
                        if (permission.statuses == 1) await _unitOfWork.UserCardPermissionRepository.Add(new UserCardPermission { id_permission = permission.id, id_card_granted = await _unitOfWork.UserCardGrantedRepository.GetByCardAndUser(card.Id, idUser) });
                    }
                }
            }
            if (tree.Children.Count > 0)
            {
                foreach(var sonTree in tree.Children) {
                    await RebuildHierarchy(sonTree, idUser);
                }
            }
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task DeleteHierarchyByUserId(int userId) => await _unitOfWork.UserViewRepository.RemoveByUserId(userId);
    }
}