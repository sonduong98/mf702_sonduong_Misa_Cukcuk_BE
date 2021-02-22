using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Common;
using MISA.Common.Filters;
using MISA.Common.Utility;
using MISA.Service.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MISA.Common.ResponseExtensions;

namespace MISA_DUONG_MF702.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get(int pageIndex = 1, int pageSize = 50,int id=0)
        {
            GlobalParamFilter filters = new GlobalParamFilter
            {
                Status = Constants.Status.Active,
                PageIndex = pageIndex,
                PageSize = pageSize > 0 ? pageSize : int.MaxValue
            };
            var response = new PagedResponse<MISA.Common.Models.Employee> { Success = true };
            try
            {
                if (id != 0)
                {
                    var data = await _employeeService.GetById(id);
                    response.Data = data;
                }
                else
                {
                    var entities = await _employeeService.GetAll(filters);
                    response.Data = entities.ToList();
                    response.PageIndex = pageIndex;
                    response.PageSize = pageSize;
                    response.TotalPages = entities.TotalPages;
                    response.TotalCount = entities.TotalCount;
                }
                
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response.ToHttpResponse();
        }
        [HttpPost]
        [Route("")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Post(MISA.Common.Models.Employee model)
        {
            var response = new Response() { Success = true };

            try
            {
                if (model == null)
                {
                    response.Success = false;
                    response.Message = "Data not found";
                    return response.ToHttpResponse();
                }
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                if (model.DateOfBirth == null)
                    model.DateOfBirth = DateTime.Now;
                //check trung ma khach hang
                var employeeCode = model.EmployeeCode;
                var sql = $"SELECT EmployeeCode FROM Employee WHERE EmployeeCode = '{employeeCode}'";
                var customerDuplicates = await _employeeService.GetData(sql);
                if (customerDuplicates.Count() > 0)
                {
                    response.Success = false;
                    response.Message = "Duplicates Employee code";
                    return response.ToHttpResponse();
                }
                var result = await _employeeService.Insert(model);

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response.ToHttpResponse();
        }
        [HttpPut]
        [Route("")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Update([FromBody] MISA.Common.Models.Employee model)
        {
            var response = new Response() { Success = true };

            try
            {
                if (model == null)
                {
                    response.Success = false;
                    response.Message = "Data not found";
                    return response.ToHttpResponse();
                }
                //check trung ma nhan vien
                var employeeCode = model.EmployeeCode;
                var sql = $"SELECT EmployeeCode FROM Employee WHERE EmployeeCode = '{employeeCode}'";
                var customerDuplicates = await _employeeService.GetData(sql);
                if (customerDuplicates.Count() > 1)
                {
                    response.Success = false;
                    response.Message = "Duplicates Employee code";
                    return response.ToHttpResponse();
                }

                var result = await _employeeService.Update(model);

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response.ToHttpResponse();
        }
        [HttpDelete]
        [Route("")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Delete(int id)
        {
            var response = new Response() { Success = true };

            try
            { 
                //check nhan vien co ton tai            
                var sql = $"SELECT EmployeeId FROM Employee WHERE EmployeeId = '{id}'";
                var customerDuplicates = await _employeeService.GetData(sql);
                if (customerDuplicates.Count() == 0)
                {
                    response.Success = false;
                    response.Message = "Data not found";
                    return response.ToHttpResponse();
                }

                var result = await _employeeService.Delete(id);

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response.ToHttpResponse();
        }
    }
}
