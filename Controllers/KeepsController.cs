using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using keepr.Models;
using keepr.Repositories;

namespace Keepr.Controllers
{
    [Authorize]
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
            var x = new User();
            return Ok(_kr.GetAll());
        }
        [HttpGet("{keepId}")]
        public ActionResult<Keep> GetActionResult(string id)
        {
            return _kr.GetById(id);
        }
        [HttpGet("{userId}")]
        public IEnumerable<Keep> Get(string userId)
        {
            return _kr.GetKeepsByUserId(userId);
        }
        [HttpPost]
        public ActionResult<Keep> Create([FromBody]Keep newKeep)
        {
            return _kr.CreateKeep(newKeep);
        }
        [HttpDelete("{id}")]
        public ActionResult<string> Delete(string id)
        {
            bool success = _kr.DeleteKeep(id);
            if (success)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}