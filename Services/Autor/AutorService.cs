using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi8.Data;
using WebApi8.Models;

namespace WebApi8.Services.Autor
{
    public class AutorService : IAutorInterface
    {
        private readonly AppDbContext _context;
        public AutorService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseModel<AutorModel>> BuscarAutorPorId(int idAutor)
        {
            ResponseModel<AutorModel> resposta = new ResponseModel<AutorModel>();
            try
            {
                var autor = await _context.Autores.FirstOrDefaultAsync(autorBanco => autorBanco.Id == idAutor);
                if (autor == null)
                {
                    resposta.Mensagem = "Nenhum autor encontrado";
                    return resposta;
                }

                resposta.Dados = autor;
                resposta.Mensagem = "Autor localizado";
                return resposta;

            }
            catch(Exception e)
            {
                resposta.Mensagem = e.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public Task<ResponseModel<AutorModel>> BuscarAutorPorIdLivro(int idLivro)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<List<AutorModel>>> ListarAutores()
        {
            ResponseModel<List<AutorModel>> resposta = new ResponseModel<List<AutorModel>>(); 
            try
            {
                var autores = await _context.Autores.ToListAsync();
                resposta.Dados = autores;
                resposta.Mensagem = "Todos os autores foram coletados!";

                return resposta;
            }
            catch(Exception e)
            {
                resposta.Mensagem = e.Message;
                resposta.Status = false;

                return resposta;
            }
        }
    }
}
