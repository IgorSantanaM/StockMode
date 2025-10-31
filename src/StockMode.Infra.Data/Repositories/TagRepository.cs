using Dapper;
using Microsoft.EntityFrameworkCore;
using StockMode.Domain.Tags;
using StockMode.Infra.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Infra.Data.Repositories
{
    public class TagRepository : Repository<Tag, int>, ITagRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly StockModeContext _dbContext;


        public TagRepository(StockModeContext context) : base(context)
        {
            _dbConnection = context.Database.GetDbConnection();
            _dbContext = context;   
        }

        public Task<Tag?> GetTagById(int id)
        {
            var sql = @"SELECT * FROM ""Tags"" WHERE ""Id"" = @TagId;";

            return _dbConnection.QueryFirstOrDefaultAsync<Tag>(sql, new { TagId = id });
        }
    }
}
