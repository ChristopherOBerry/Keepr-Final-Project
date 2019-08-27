using System;
using System.Collections.Generic;
using System.Linq;
using keepr.Models;
using keepr.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace keepr.Repositories
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaultKeepsController : ControllerBase
    {
        private readonly VaultKeepRepository _vkr;
        public VaultKeepsController(VaultKeepRepository vkr)
        {
            _vkr = vkr;
        }
        [HttpGet("{vaultId}")]
        public ActionResult<VaultKeep> GetActionResult(string vaultKeepId)
        {
            return _vkr.GetById(vaultKeepId);
        }
        [HttpPost]
        public ActionResult<VaultKeep> Create([FromBody]VaultKeep newVaultKeep)
        {
            var UserId = HttpContext.User.FindFirstValue("Id");
            newVaultKeep.UserId = UserId;
            return _vkr.CreateVaultKeep(newVaultKeep);
        }
        [HttpDelete]
        public ActionResult Delete(string vaultKeepId)
        {
            bool success = _vkr.DeleteVaultKeep(vaultKeepId);
            if (success)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}