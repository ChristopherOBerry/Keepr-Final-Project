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
    }
}