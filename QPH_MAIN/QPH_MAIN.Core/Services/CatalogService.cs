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
    public class CatalogService : ICatalogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public CatalogService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public async Task<Catalog> GetCatalog(int id) => await _unitOfWork.CatalogRepository.GetById(id);

        public PagedList<Catalog> GetCatalogs(CatalogQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var views = _unitOfWork.CatalogRepository.GetAll();
            if (filters.filter != null)
            {
                views = views.Where(x => x.code.ToLower().Contains(filters.filter.ToLower()));
                views = views.Where(x => x.name.ToLower().Contains(filters.filter.ToLower()));
            }
            if (filters.Code != null)
            {
                views = views.Where(x => x.code == filters.Code);
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
            var pagedPosts = PagedList<Catalog>.Create(views, filters.PageNumber, filters.PageSize);
            return pagedPosts;
        }

        public async Task InsertCatalog(Catalog views)
        {
            await _unitOfWork.CatalogRepository.Add(views);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> DeleteCatalog(int id)
        {
            await _unitOfWork.CatalogRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateCatalog(Catalog views)
        {
            var existingCatalog = await _unitOfWork.CatalogRepository.GetById(views.Id);
            existingCatalog.name = views.name;
            existingCatalog.code = views.code;
            _unitOfWork.CatalogRepository.Update(existingCatalog);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RebuildHierarchy(CatalogTree tree, int idEnterprise)
        {
            await _unitOfWork.EnterpriseHierarchyCatalogRepository.Add(new EnterpriseHierarchyCatalog { id_enterprise = idEnterprise , children = tree.son, parent = tree.parent });
            if (tree.Children.Count > 0)
            {
                foreach(var sonTree in tree.Children) {
                    await RebuildHierarchy(sonTree, idEnterprise);
                }
            }
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task DeleteHierarchyByEnterpriseId(int enterpriseId) => await _unitOfWork.EnterpriseHierarchyCatalogRepository.RemoveByEntepriseId(enterpriseId);
    }
}