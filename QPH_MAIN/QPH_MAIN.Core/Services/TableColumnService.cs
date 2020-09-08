using Microsoft.Extensions.Options;
using QPH_MAIN.Core.CustomEntities;
using QPH_MAIN.Core.DTOs;
using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Exceptions;
using QPH_MAIN.Core.Interfaces;
using QPH_MAIN.Core.QueryFilters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Services
{
    public class TableColumnService : ITableColumnService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TableColumnService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TableColumn>> GetByTableColumnWithSchema(string table, string schema) => await _unitOfWork.TableColumnRepository.GetTableColumnByTableNameAndSchema(table, schema);
    }
}