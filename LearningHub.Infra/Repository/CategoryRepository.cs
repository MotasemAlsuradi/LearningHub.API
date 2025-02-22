using Dapper;
using LearningHub.Core.Common;
using LearningHub.Core.Data;
using LearningHub.Core.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningHub.Infra.Repository
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly IDbContext _dbContext;

        public CategoryRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Category> GetAllCategories()
        {
            IEnumerable<Category> records = _dbContext.Connection.Query<Category>
                ("Category_pkg.GetAllCategories", commandType: CommandType.StoredProcedure);

            return records.ToList();
        }

        public void CreateCategory(Category category)
        {
            var parameter = new DynamicParameters();
            parameter.Add("p_category_name", category.Categoryname, dbType: DbType.String, direction: ParameterDirection.Input);

            var record = _dbContext.Connection.Execute
                ("Category_pkg.CreateCategory", parameter, commandType: CommandType.StoredProcedure);
        }

        public void UpdateCategory(Category category)
        {
            var parameter = new DynamicParameters();
            parameter.Add("p_ID",category.Categoryid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            parameter.Add("p_category_name", category.Categoryname, dbType: DbType.String, direction: ParameterDirection.Input);

            var record = _dbContext.Connection.Execute
                ("Category_pkg.UpdateCategory", parameter, commandType: CommandType.StoredProcedure);
        }

        public void DeleteCategory(int id)
        {
            var parameter = new DynamicParameters();
            parameter.Add("p_ID", id, dbType: DbType.UInt32, direction: ParameterDirection.Input);

            var record = _dbContext.Connection.Execute
                ("Category_pkg.DeleteCategory", parameter, commandType: CommandType.StoredProcedure);
        }

        public Category? GetCategoryById(int id)
        {
            var parameter = new DynamicParameters();
            parameter.Add("p_Id", id, dbType: DbType.UInt32, direction: ParameterDirection.Input);

            IEnumerable<Category> records = _dbContext.Connection.Query<Category>
                ("Category_pkg.GetCategoryById", parameter, commandType: CommandType.StoredProcedure);

            return records.FirstOrDefault();
        }
    }
}
