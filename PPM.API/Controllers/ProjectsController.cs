using Microsoft.AspNetCore.Mvc;
using PPM.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace PPM.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetProjects()
    {
        try
        {
            using (ppmContext context =  new ppmContext())
            {
                var projects = context.Projects.ToList();
                return Ok(projects);
            }
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }

    [HttpGet("{projectId}")]
    public async Task <IActionResult> GetProjectsById (int projectId)
    {
        using (ppmContext context =  new ppmContext())
        {
            var projects = context.Projects.FirstOrDefault(p => p.ProjectId == projectId);
            if (projects != null)
            {
                return Ok(projects);
            }
            else 
            {
                return BadRequest();
            }
        }  
    }

    [HttpDelete ("{projectId}")]
    public async Task <IActionResult> DeleteProject (int projectId)
    {
        try
        {
            using (ppmContext context =  new ppmContext())
            {
                var projects = context.Projects.FirstOrDefault(p => p.ProjectId == projectId);
                if (projects == null)
                {
                    return NotFound("Project Id Not Found to Delete");
                }
                context.Projects.Remove(projects);
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
    public async Task<IActionResult> PutProject (int id, [FromBody]Project project)
    {
        try
        {
            using (ppmContext context = new ppmContext())
            {
                var entity = context.Projects.FirstOrDefault(p => p.ProjectId == project.ProjectId);
                if (entity == null)
                {
                     return NotFound("Project Id Not Found to Update");
                }
                else
                {
                    entity.ProjectId = project.ProjectId;
                    entity.ProjectName = project.ProjectName;
                    entity.StartDate = project.StartDate;
                    entity.EndDate = project.EndDate;
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
    public async Task<IActionResult> PostProject ([FromBody]Project project)
    {
        try
        {
            using (ppmContext context = new ppmContext())
            {
                context.Projects.Add(project);
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


