using Microsoft.AspNetCore.Mvc;
using PPM.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace PPM.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetEmployees()
    {
        try
        {
            using (ppmContext context =  new ppmContext())
            {
                var employees = context.Employees.ToList();
                return Ok(employees);
            }
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }

    [HttpGet("{employeeId}")]
    public async Task <IActionResult> GetEmployeeById (int employeeId)
    {
        using (ppmContext context =  new ppmContext())
        {
            var employees = context.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);
            if (employees != null)
            {
                return Ok(employees);
            }
            else 
            {
                return BadRequest();
            }
        }          
    }

    [HttpDelete ("{employeeId}")]
    public async Task <IActionResult> DeleteEmployee (int employeeId)
    {
        try
        {
            using (ppmContext context =  new ppmContext())
            {
                var employees = context.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);
                if (employees == null)
                {
                    return NotFound("Employee Id Not Found to Delete");
                }
                context.Employees.Remove(employees);
                context.SaveChanges();
                return Ok();
            }
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }

    [HttpPut]
    public async Task<IActionResult> PutEmployee (int id, [FromBody]Employee employee)
    {
        try
        {
            using (ppmContext context = new ppmContext())
            {
                var entity = context.Employees.FirstOrDefault(e => e.EmployeeId == employee.EmployeeId);
                if (entity == null)
                {
                     return NotFound("Employee Id Not Found to Update");
                }
                else
                {
                    entity.EmployeeId = employee.EmployeeId;
                    entity.FirstName = employee.FirstName;
                    entity.LastName = employee.LastName;
                    entity.Email = employee.Email;
                    entity.MobileNumber = employee.MobileNumber;
                    entity.Address= employee.Address;
                    entity.RoleId = employee.RoleId;

                    context.SaveChanges();
                    return Ok();
                }
            }
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }

    [HttpPost]
    public async Task<IActionResult> PostEmployee ([FromBody]Employee employee)
    {
        try
        {
            using (ppmContext context = new ppmContext())
            {
                context.Employees.Add(employee);
                context.SaveChanges();
                return Ok();
            }
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
}


