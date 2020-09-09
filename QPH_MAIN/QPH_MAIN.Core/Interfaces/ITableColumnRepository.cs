using QPH_MAIN.Core.DTOs;
using QPH_MAIN.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Interfaces
{
    public interface ITableColumnRepository : ICodeRepository<TableColumn>
    {
        Task<IEnumerable<TableColumn>> GetTableColumnByTableNameAndSchema(string tablename, string schema);
    }
}