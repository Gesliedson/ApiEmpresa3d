using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ApiEmpresa3d.model;
using ApiEmpresa3d.Context;
using Microsoft.EntityFrameworkCore;

namespace ApiEmpresa3d.Controllers
{
    [Route("api/[controller]")]
    public class ProjetoController : ControllerBase
    {
        private readonly ILogger<ProjetoController> _logger;
        private readonly ApiEmpresa3dContext _context;
        public ProjetoController(ILogger<ProjetoController> logger, ApiEmpresa3dContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Projeto>> Get()
        {
            var projetos = _context.Projeto.ToList();
            if(projetos is null)
                return NotFound();

            return projetos;
        }

        [HttpGet("{Id:int}", Name="GetProjeto")]
        public ActionResult<Projeto> Get(int Id){
            var projeto = _context.Projeto.FirstOrDefault(p => p.Id == Id);
            if(projeto is null)
                return NotFound("Projeto não encontrado.");
            
            return projeto;
        }

        [HttpPost]
        public ActionResult Post(Projeto projeto){
            _context.Projeto.Add(projeto);
            _context.SaveChanges();

            return new CreatedAtRouteResult("GetProjeto",
            new{ Id = projeto.Id},projeto);
        } 
        
        [HttpPut("Id:int")]
        public ActionResult Put ( int Id, Projeto projeto){
            if(Id != projeto.Id)
                return BadRequest();
            
            _context.Entry(projeto).State = EntityState.Modified;
            _context.SaveChanges(); 

            return Ok (projeto);

            
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