using MISA.Common;
using MISA.Common.Filters;
using MISA.DataLayer.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Service.Employee
{
    public class EmployeeService : GenericRepository<MISA.Common.Models.Employee>, IEmployeeService
    {
        //public CustomerService(DbContext1 context) : base(context)
        //{

        //}
        public async Task<IPagedList<Common.Models.Employee>> GetAll(GlobalParamFilter filters)
        {
            var query = await _context.GetData<MISA.Common.Models.Employee>();
            query = this.SortData(query.AsQueryable(), filters.SortBy, filters.OrderBy);

            var results = query.AsQueryable();

            // Return to list with paging
            return new PagedList<Common.Models.Employee>(results, filters.PageIndex, filters.PageSize);
        }
        public async Task<int> Insert(MISA.Common.Models.Employee model)
        {
            int result = await _context.Insert<MISA.Common.Models.Employee>(model);
            return result;
        }
        public async Task<List<MISA.Common.Models.Employee>> GetById(int id)
        {
            var sql = $"SELECT * FROM Employee WHERE EmployeeId = '{id}'";
            var employee = await GetData(sql);
            return employee.ToList();
        }
        public async Task<int> Update(MISA.Common.Models.Employee model)
        {
            int result = await _context.Update<MISA.Common.Models.Employee>(model);
            return result;
        }
        public async Task<int> Delete(int id)
        {
            int result = await _context.Delete<MISA.Common.Models.Employee>(id);
            return result;
        }
        private IQueryable<MISA.Common.Models.Employee> SortData(IQueryable<MISA.Common.Models.Employee> query, string sortBy, string orderBy)
        {
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy.ToUpper())
                {

                    default:
                        {
                            query = orderBy.Equals(Constants.OrderByData.Asc, StringComparison.OrdinalIgnoreCase) ?
                                query.OrderBy(c => c.EmployeeId) : query.OrderByDescending(c => c.EmployeeId);
                            break;
                        }
                }
            }
            else
            {
                query = orderBy.Equals(Constants.OrderByData.Asc, StringComparison.OrdinalIgnoreCase) ?
                    query.OrderBy(c => c.EmployeeId) : query.OrderByDescending(c => c.EmployeeId);
            }

            return query;
        }

    }
}
