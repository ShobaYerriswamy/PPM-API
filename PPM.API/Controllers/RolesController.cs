using Microsoft.AspNetCore.Mvc;
using PPM.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace PPM.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetRoles()
    {
        try
        {
            using (ppmContext context =  new ppmContext())
            {
                var roles = context.Roles.ToList();
                return Ok(roles);
            }
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }

    [HttpGet("{roleId}")]
    public async Task <IActionResult> GetRolesById (int roleId)
    {
        using (ppmContext context =  new ppmContext())
        {
            var roles = context.Roles.FirstOrDefault(r => r.RoleId == roleId);
            if (roles != null)
            {
                return Ok(roles);
            }
           else 
            {
                return BadRequest();
            }
        }  
    }

    [HttpDelete ("{roleId}")]
    public async Task <IActionResult> DeleteRole (int roleId)
    {
        try
        {
            using (ppmContext context =  new ppmContext())
            {
                var roles = context.Roles.FirstOrDefault(r => r.RoleId == roleId);
                if (roles == null)
                {
                    return NotFound("Role Id Not Found to Delete");
                }
                context.Roles.Remove(roles);
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
    public async Task<IActionResult> PutRole (int id, [FromBody]Role role)
    {
        try
        {
            using (ppmContext context = new ppmContext())
            {
                var entity = context.Roles.FirstOrDefault(r => r.RoleId == role.RoleId);
                if (entity == null)
                {
                     return NotFound("Role Id Not Found to Update");
                }
                else
                {
                    entity.RoleId = role.RoleId;
                    entity.RoleName = role.RoleName;
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
    public async Task<IActionResult> PostRole ([FromBody]Role role)
    {
        try
        {
            using (ppmContext context = new ppmContext())
            {
                context.Roles.Add(role);
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



