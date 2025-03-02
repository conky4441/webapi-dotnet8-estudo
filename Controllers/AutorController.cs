using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi8.Dto.Autor;
using WebApi8.Models;
using WebApi8.Services.Autor;

namespace WebApi8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly IAutorInterface _autorInterface;
        public AutorController(IAutorInterface autorinterface)
        {
            _autorInterface = autorinterface;
        }


        [HttpGet("ListarAutores")]
        public async Task<ActionResult<ResponseModel<List<AutorModel>>>> ListarAutores()
        {
            var autores = await _autorInterface.ListarAutores();
            return Ok(autores);
        }

        [HttpGet("BuscarAutorPorId/{idAutor}")]
        public async Task<ActionResult<ResponseModel<AutorModel>>> BuscarAutorPorId(int idAutor)
        {
            var autor = await _autorInterface.BuscarAutorPorId(idAutor);
            return Ok(autor);
        }

        [HttpPost("CriarAutor")]
        public async Task<ActionResult<ResponseModel<List<AutorModel>>>> CriarAutor(AutorCriacaoDto autorCriacaoDto)
        {
            var autor = await _autorInterface.CriarAutor(autorCriacaoDto);
            return Ok(autor);
        }

        [HttpDelete("ExcluirAutor")]
        public async Task<ActionResult<ResponseModel<List<AutorModel>>>> ExcluirAutor(int idAutor)
        {
            var autor = await _autorInterface.ExcluirAutor(idAutor);
            return Ok(autor);
        }

        [HttpPut("EditarAutor")]
        public async Task<ActionResult<ResponseModel<List<AutorModel>>>> EditarAutor(EditarAutorDto autorDto)
        {
            var autor = await _autorInterface.EditarAutor(autorDto);
            return Ok(autor);
        }

    }
}
