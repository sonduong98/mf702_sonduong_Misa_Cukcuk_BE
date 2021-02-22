using MISA.Common;
using MISA.Common.Filters;
using MISA.DataLayer.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Service.Employee
{
    public interface IEmployeeService : IGenericRepository<MISA.Common.Models.Employee>
    {
        Task<IPagedList<MISA.Common.Models.Employee>> GetAll(GlobalParamFilter filters);
        Task<List<MISA.Common.Models.Employee>> GetById(int id);
        Task<int> Insert(MISA.Common.Models.Employee model);
        Task<int> Update(MISA.Common.Models.Employee model);
        Task<int> Delete(int id);
    }
}
