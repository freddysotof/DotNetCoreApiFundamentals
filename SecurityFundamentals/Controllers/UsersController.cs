using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SecurityFundamentals.Context;
using SecurityFundamentals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SecurityFundamentals.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(AppDbContext context,UserManager<ApplicationUser> userManager)
        {

            this._context = context;
            this._userManager = userManager;
        }

        [HttpPost("AssignUserRole")]
        public async Task<ActionResult> AssignUserRole(EditRoleDTO editRoleDTO)
        {
            var user = await _userManager.FindByIdAsync(editRoleDTO.UserId);
            if (user == null) return NotFound();
            // IdentityUser
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, editRoleDTO.RoleName));
            // Jwt User
            await _userManager.AddToRoleAsync(user, editRoleDTO.RoleName);
            return Ok();
        }

        [HttpPost("RemoveUserRole")]
        public async Task<ActionResult> RemoveUserRole(EditRoleDTO editRoleDTO)
        {
            var user = await _userManager.FindByIdAsync(editRoleDTO.UserId);
            if (user == null) return NotFound();
            // IdentityUser
            await _userManager.RemoveClaimAsync(user, new Claim(ClaimTypes.Role, editRoleDTO.RoleName));
            // Jwt User
            await _userManager.RemoveFromRoleAsync(user, editRoleDTO.RoleName);
            return Ok();
        }
    }
}
