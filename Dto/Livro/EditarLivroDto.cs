using WebApi8.Models;

namespace WebApi8.Dto.Livro
{
    public class EditarLivroDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public AutorModel Autor { get; set; }
    }
}
