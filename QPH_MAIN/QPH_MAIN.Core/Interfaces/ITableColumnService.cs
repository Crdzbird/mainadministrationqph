using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.QueryFilters;
using QPH_MAIN.Core.CustomEntities;
using System.Threading.Tasks;
using QPH_MAIN.Core.DTOs;
using System.Collections.Generic;

namespace QPH_MAIN.Core.Interfaces
{
    public interface ITableColumnService
    {
        Task<IEnumerable<TableColumn>> GetByTableColumnWithSchema (string table, string schema);
    }
}