﻿using Microsoft.EntityFrameworkCore;
using QPH_MAIN.Core.DTOs;
using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Interfaces;
using QPH_MAIN.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPH_MAIN.Infrastructure.Repositories
{
    public class TableColumnRepository : BaseCodeRepository<TableColumn>, ITableColumnRepository
    {
        public TableColumnRepository(QPHContext context) : base(context) { }

        public async Task<IEnumerable<TableColumn>> GetTableColumnByTableNameAndSchema(string tablename, string schema) => await _entities.FromSqlRaw("exec GetTableColumns @schemaname = {0}, @tablename = {1}", schema, tablename).ToListAsync();
    }
}