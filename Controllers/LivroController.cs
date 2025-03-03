using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi8.Dto.Autor;
using WebApi8.Dto.Livro;
using WebApi8.Models;
using WebApi8.Services.Autor;
using WebApi8.Services.Livro;

namespace WebApi8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly ILivroInterface _livroInterface;

        public LivroController(ILivroInterface livroInterface)
        {
            _livroInterface = livroInterface;
        }

        [HttpGet("BuscarLivroPorId/{idLivro}")]
        public async Task<ActionResult<ResponseModel<AutorModel>>> BuscarLivroPorId(int idLivro)
        {
            var livros = await _livroInterface.BuscarLivroPorId(idLivro);
            return Ok(livros);
        }

        [HttpGet("BuscarLivroPorIdAutor/{idAutor}")]
        public async Task<ActionResult<ResponseModel<AutorModel>>> BuscarLivroPorIdAutor(int idAutor)
        {
            var autor = await _livroInterface.BuscarLivroPorIdAutor(idAutor);
            return Ok(autor);
        }

        [HttpPost("CriarLivro")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> CriarLivro(LivroCriacaoDto livroCriacaoDto)
        {
            var livro = await _livroInterface.CriarLivro(livroCriacaoDto);
            return Ok(livro);
        }

        [HttpGet("ListarLivros")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> ListarLivros()
        {
            var livros = await _livroInterface.ListarLivros();
            return Ok(livros);
        }

        [HttpDelete("ExcluirLivro")]
        public async Task<ActionResult<ResponseModel<List<AutorModel>>>> ExcluirLivro(int idLivro)
        {
            var livro = await _livroInterface.ExcluirLivro(idLivro);
            return Ok(livro);
        }

        [HttpPut("EditarLivro")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> EditarAutor(EditarLivroDto livroDto)
        {
            var livro = await _livroInterface.EditarLivro(livroDto);
            return Ok(livro);
        }

    }
}
