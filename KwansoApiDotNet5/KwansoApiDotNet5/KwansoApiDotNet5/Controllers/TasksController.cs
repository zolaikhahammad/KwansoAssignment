using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Kwanso.Core.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Kwanso.Core.Contracts.Response.Tasks;
using Kwanso.Core.Contracts.Request.Tasks;
using Kwanso.Core.Contracts.ViewModels;
using Kwanso.Core;

namespace KwansoApiDotNet5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly KwansoContext _context;
        private readonly IMapper _mapper;
        public TasksController(KwansoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Tasks
     
        [HttpGet("/list-tasks")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TasksResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> GetTasks()
        {
            TasksResponse tasksResponse = new TasksResponse();
            tasksResponse.task = new Kwanso.Core.Contracts.ViewModels.TaskViewModel();
            try
            {

                var tasksdb = await _context.Tasks.Where(x => x.IsActive == true).ToListAsync();
                tasksResponse.status_code = 200;
                tasksResponse.tasks = tasksdb.Select(_mapper.Map<Tasks, TaskViewModel>).ToList();
                return Ok(new Response<TasksResponse>(tasksResponse));

            }
            catch (Exception ex)
            {
                tasksResponse.error = ex.Message;
                tasksResponse.status_code = 400;
                return BadRequest(new Response<TasksResponse>(tasksResponse));
            }
        }



        // POST: api/Tasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
      
        [HttpPost("/create-task")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TasksResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostTasks(CreateTaskRequest request)
        {
            TasksResponse tasksResponse = new TasksResponse();
            tasksResponse.task = new Kwanso.Core.Contracts.ViewModels.TaskViewModel();
            try
            {
               
                Kwanso.Core.Models.Tasks task = new Tasks();
                task.Name = request.Name;
                task.IsActive = true;
                _context.Tasks.Add(task);
                await _context.SaveChangesAsync();
                tasksResponse.status_code = 200;
                tasksResponse.task = _mapper.Map<TaskViewModel>(task);
                return Ok(new Response<TasksResponse>(tasksResponse));

            }
            catch (Exception ex)
            {
                tasksResponse.error = ex.Message;
                tasksResponse.status_code = 400;
                return BadRequest(new Response<TasksResponse>(tasksResponse));
            }

        }
        [HttpPost("/delete-task")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TasksResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> Delete(List<int> Ids)
        {
            TasksResponse tasksResponse = new TasksResponse();
            tasksResponse.task = new Kwanso.Core.Contracts.ViewModels.TaskViewModel();
            try
            {
               foreach(var item in Ids)
                {
                    var gettask =await _context.Tasks.Where(x => x.Id == item).FirstOrDefaultAsync();
                    if (gettask != null)
                    {
                        gettask.IsActive = false;
                        await _context.SaveChangesAsync();
                    }
                }

                var tasksdb = await _context.Tasks.Where(x=>x.IsActive==true).ToListAsync();
                tasksResponse.status_code = 200;
                tasksResponse.tasks = tasksdb.Select(_mapper.Map<Tasks, TaskViewModel>).ToList();
                return Ok(new Response<TasksResponse>(tasksResponse));

            }
            catch (Exception ex)
            {
                tasksResponse.error = ex.Message;
                tasksResponse.status_code = 400;
                return BadRequest(new Response<TasksResponse>(tasksResponse));
            }
        }

    }
}
