using Dados.Repositorio.Repositorios;
using Dominio;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilitarios.Enumeradores;

namespace Dados.Configuracao
{
    public class InicializadorContexto : IDatabaseInitializer<DataContext>
    {
        private bool _deletar;

        public InicializadorContexto(bool deletarBancoDeDados)
        {
            _deletar = deletarBancoDeDados;
        }

        public void InitializeDatabase(DataContext contexto)
        {
            if (_deletar)
            {
                contexto.Database.Delete();
                contexto.Database.CreateIfNotExists();

                Pessoa pessoa = new Pessoa()
                {
                    PessoaId = Guid.NewGuid(),
                    Nome = "Gabriel",
                    Celular = "11952942458",
                    DataDeNascimento = new DateTime(1992, 07, 03),
                    Email = "gabrielbrandaodev@gmail.com"
                };

                AnimalDomestico animal = new AnimalDomestico()
                {
                    AnimalDomesticoId = Guid.NewGuid(),
                    Nome = "Dog",
                    Pessoa = pessoa,
                    PessoaId = pessoa.PessoaId,
                    AnimalCastrado = false,
                    TipoAnimal = EnumTipoAnimal.Cachorro
                };

                PessoaRepositorio pessoaRepositorio = new PessoaRepositorio(contexto);
                pessoaRepositorio.Incluir(pessoa);

                AnimalDomesticoRepositorio animalRepositorio = new AnimalDomesticoRepositorio(contexto);
                animalRepositorio.Incluir(animal);

                pessoaRepositorio.SalvarTodos();
            }
        }

        public void DropAndCreateSchema(DataContext contexto)
        {
            contexto.Database.Delete();
            contexto.Database.CreateIfNotExists();
        }
    }
}
