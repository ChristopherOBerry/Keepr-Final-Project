using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using keepr.Models;
using keepr.Repositories;
using System.Security.Claims;

namespace Keepr.Controllers
{
    
    [Route("api/[Controller]")]
    [ApiController]
    public class KeepsController : ControllerBase
    {
        private readonly KeepsRepository _kr;
        public KeepsController(KeepsRepository kr)
        {
            _kr = kr;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Keep>> Get()
        {

            return Ok(_kr.GetAll());
        }
        [HttpGet("{keepId}")]
        public ActionResult<Keep> GetActionResult(string keepId)
        {
            return _kr.GetById(keepId);
        }
        [HttpGet("{userId}")]
        public IEnumerable<Keep> Get(string userId)
        {
            var UserId = HttpContext.User.FindFirstValue("Id");
            userId = UserId;
            return _kr.GetKeepsByUserId(userId);
        }

        [HttpPost]
        public ActionResult<Keep> Create([FromBody]Keep newKeep)
        {
            var UserId = HttpContext.User.FindFirstValue("Id");
            newKeep.UserId = UserId;
            return _kr.CreateKeep(newKeep);
        }
        [HttpDelete("{keepId}")]
        public ActionResult<string> Delete(string keepId)
        {
            bool success = _kr.DeleteKeep(keepId);
            if (success)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}