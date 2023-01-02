using AutoMapper;
using introEntity.DTOs;
using introEntity.Entidades;
using introEntity.UoW;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace introEntity.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IConfiguration config;

        public UsuariosController(IMapper mapper,IUnitOfWork unitOfWork,IConfiguration config)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.config = config;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Login(UsuarioDTO userDTO)
        {
            var usuario = await this.unitOfWork.usuarioRepository.getUsuario(userDTO);
            if (usuario == null)
            {
                return NotFound();
            }
            string jwtToken = GenerateToken(usuario);
            return Ok(jwtToken);
        }

        public string GenerateToken(Usuario usuario)
        {
            var claims = new[] 
            { 
                new Claim(ClaimTypes.Name, usuario.nombreUsuario),
                new Claim("RolType",usuario.rol)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("JWT:Key").Value));
            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256Signature);

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds);
            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }

        [HttpPost("registrarse")]
        public async Task<IActionResult> registrarse(UsuarioRegistroDTO usuarioRegistroDTO)
        {
            var user = this.mapper.Map<Usuario>(usuarioRegistroDTO);
            await this.unitOfWork.usuarioRepository.add(user);
            await this.unitOfWork.saveChanges();
            return Ok();
        }

    }
}
