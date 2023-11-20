using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ApiEmpresa3d.model;
using ApiEmpresa3d.Context;

namespace ApiEmpresa3d.Controllers
{
    [Route("api[controller]")]
    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly ApiEmpresa3dContext _context;
        public UsuarioController(ILogger<UsuarioController> logger, ApiEmpresa3dContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Usuario>> Get()
        {
            var usuarios = _context.Usuario.ToList();
            if(usuarios is null)
                return NotFound();

            return usuarios;
        }

        [HttpGet("{Id:int}", Name="GetUsuario")]
        public ActionResult<Usuario> Get(int Id){
            var usuario = _context.Usuario.FirstOrDefault(p => p.Id == Id);
            if(usuario is null)
                return NotFound("Usuário não encontrado.");
            
            return usuario;
        }



//ALTERAR
        [HttpPost]
        public ActionResult Post(Projeto projeto){
            _context.Projeto.Add(projeto);
            _context.SaveChanges();

            return new CreatedAtRouteResult("GetProjeto",
            new{ Id = projeto.Id},projeto);
        } 
        

//ALTERAR
        [HttpPut("Id:int")]
        public ActionResult Put ( int Id, Projeto projeto){
            if(Id != projeto.Id)
                return BadRequest();
            
            _context.Entry(projeto).State = EntityState.Modified;
            _context.SaveChanges(); 

            return Ok (projeto);


//ALTERAR
        }

        [HttpDelete ("{Id int}")] 
         public ActionResult Delete(int Id) {
            var projeto= _context.Projeto.FirstOrDefault (P=> P.Id == Id);

            if (projeto is null) {

                return NotFound();
             }
    

                _context.Projeto.Remove(projeto);
                _context.SaveChanges();
                return Ok (projeto);
        }
    }
}