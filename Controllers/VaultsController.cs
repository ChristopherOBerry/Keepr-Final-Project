using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using keepr.Models;
using keepr.Repositories;
using System.Security.Claims;


namespace Keepr.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VaultsController : ControllerBase
    {
        private readonly VaultsRepository _vr;
        public VaultsController(VaultsRepository vr)
        {
            _vr = vr;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Vault>> Get()
        {
            var UserId = HttpContext.User.FindFirstValue("Id");

            return Ok(_vr.GetAll(UserId));
        }

        [HttpGet("{id}")]
        public ActionResult<Vault> GetActionResult(string id)
        {
            return _vr.GetById(id);
        }

        [HttpPost]
        public ActionResult<Vault> Create([FromBody]Vault newVault)
        {
            var UserId = HttpContext.User.FindFirstValue("Id");
            newVault.UserId = UserId;
            return _vr.CreateVault(newVault);
        }

        [HttpDelete("{id}")]
        public ActionResult<string> Delete(string id)
        {
            bool success = _vr.DeleteVault(id);
            if (success)
            {
                return Ok();
            }
            return BadRequest();
        }


    }


}