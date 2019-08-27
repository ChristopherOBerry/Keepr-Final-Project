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
        public ActionResult<IEnumerable<Vault>> GetById(int vaultId)
        {
            var UserId = HttpContext.User.FindFirstValue("Id");

            return Ok(_vkr.GetById(vaultId, UserId));
        }
        [HttpPost]
        public ActionResult<VaultKeep> Create([FromBody]VaultKeep newVault)
        {
            var UserId = HttpContext.User.FindFirstValue("Id");
            newVault.UserId = UserId;
            return _vkr.CreateVaultKeep(newVault);
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