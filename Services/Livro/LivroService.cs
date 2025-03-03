using Microsoft.EntityFrameworkCore;
using WebApi8.Data;
using WebApi8.Dto.Autor;
using WebApi8.Dto.Livro;
using WebApi8.Models;

namespace WebApi8.Services.Livro
{
    public class LivroService : ILivroInterface
    {
        private readonly AppDbContext _context;

        public LivroService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<LivroModel>> BuscarLivroPorId(int idLivro)
        {
            ResponseModel<LivroModel> resposta = new ResponseModel<LivroModel>();
            try
            {
                var livro = await _context.Livros.Include(a => a.Autor).FirstOrDefaultAsync(livroBanco => livroBanco.Id == idLivro);
                if (livro == null)
                {
                    resposta.Mensagem = "Nenhum livro encontrado";
                    return resposta;
                }

                resposta.Dados = livro;
                resposta.Mensagem = "Livro localizado";
                return resposta;

            }
            catch (Exception e)
            {
                resposta.Mensagem = e.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> BuscarLivroPorIdAutor(int idAutor)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
            try
            {
                var livro = await _context.Livros.Include(a => a.Autor)
                    .Where(livroBanco => livroBanco.Autor.Id == idAutor)
                    .ToListAsync();
              
                if (livro == null)
                {
                    resposta.Mensagem = "Nenhum autor encontrado";
                    return resposta;
                }

                resposta.Dados = livro;
                resposta.Mensagem = "Livro(s) localizado(s)";
                return resposta;

            }
            catch (Exception e)
            {
                resposta.Mensagem = e.Message;
                resposta.Status = false;
                return resposta;
            }   

        }

        public async Task<ResponseModel<List<LivroModel>>> CriarLivro(LivroCriacaoDto livroCriacaoDto)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
            try
            {

                var autor = await _context.Autores
                    .FirstOrDefaultAsync(autor => autor.Id == livroCriacaoDto.Autor.Id);

                if(autor == null)
                {
                    resposta.Mensagem = "Nenhum autor com o Id informado foi encontrado.";
                    return resposta;
                }

                var livro = new LivroModel()
                {
                    Titulo = livroCriacaoDto.Titulo,
                    Autor = autor
                };

                _context.Add(livro);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Livros.Include(a => a.Autor).ToListAsync();
                resposta.Mensagem = "Livro criado com sucesso.";
                return resposta;

            }
            catch (Exception e)
            {
                resposta.Mensagem = e.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> EditarLivro(EditarLivroDto editarLivroDto)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
            try
            {
                var livro = await _context.Livros.Include(a => a.Autor)
                    .FirstOrDefaultAsync(livroBanco => livroBanco.Id == editarLivroDto.Id);

                var autor = await _context.Autores.FirstOrDefaultAsync(a => a.Id == editarLivroDto.Autor.Id);

                if(autor == null)
                {
                    resposta.Mensagem = "Nenhum autor localizado.";
                    return resposta;
                }
                if (livro == null)
                {
                    resposta.Mensagem = "Nenhum livro localizado.";
                    return resposta;
                }

                livro.Titulo = editarLivroDto.Titulo;
                livro.Autor = autor;

                _context.Update(autor);
                await _context.SaveChangesAsync();
                resposta.Dados = await _context.Livros.ToListAsync();
                resposta.Mensagem = "Livro atualizado com sucesso.";
                return resposta;
            }
            catch (Exception e)
            {
                resposta.Mensagem = e.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> ExcluirLivro(int idLivro)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
            try
            {
                var autor = await _context.Livros.FirstOrDefaultAsync(livro => livro.Id == idLivro);
                if (autor == null)
                {
                    resposta.Mensagem = "Livro não encontrado.";
                    return resposta;
                }
                _context.Remove(autor);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Livros.ToListAsync();
                resposta.Mensagem = "Livro removido com sucesso";
                return resposta;
           
            }
            catch (Exception e)
            {
                resposta.Mensagem = e.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> ListarLivros()
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
            try
            {
                var livros = await _context.Livros.Include(a => a.Autor).ToListAsync();
                resposta.Dados = livros;
                resposta.Mensagem = "Todos os livros foram coletados!";

                return resposta;
            }
            catch (Exception e)
            {
                resposta.Mensagem = e.Message;
                resposta.Status = false;

                return resposta;
            }
        }
    }
}
