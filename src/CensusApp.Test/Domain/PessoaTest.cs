using CensusApp.Api.Core.Domain;
using CensusApp.Api.Core.Domain.Extensions;
using Xunit;

namespace CensusApp.Test.Domain
{
    public class PessoaTest
    {
        [Theory]
        [InlineData(null, "Silva", "nome_is_not_null")]
        [InlineData("Maria", null, "sobrenome_is_not_null")]
        public void Pessoa_deve_conter_notificacao_quando_propriedade_for_invalida(string nome, string sobrenome,  string notificationKey)
        {
            var pessoa = new Pessoa(nome, sobrenome, new RacaCor("Negra"), new Escolaridade("Ensino Fundamental"), new Filiacao("Joana"));

            Assert.False(pessoa.IsValid);
            Assert.True(pessoa.HasNotification(notificationKey));
        }

        [Fact]
        public void Pessoa_deve_ser_invalida_quando_filiacao_for_nula()
        {
            var pessoa = new Pessoa("Paulo", "Silva", new RacaCor("Negra"), new Escolaridade("Ensino Fundamental"), null);

            Assert.False(pessoa.IsValid);
            Assert.True(pessoa.CountNotificationEquals(1));

        }

        [Fact]
        public void Deve_atribuir_propriedades_quando_pessoa_for_valida()
        {
            var pessoa = new Pessoa("Maria","Silva", new RacaCor("Negra"), new Escolaridade("Ensino Fundamental"),new Filiacao("Joana"));

            Assert.True(pessoa.IsValid);
            Assert.Equal(pessoa.Nome.Value, "Maria");
            Assert.Equal(pessoa.Sobrenome.Value, "Silva");
        }

        [Fact]
        public void Deve_retornar_lista_com_1_filho_quando_este_for_incluido()
        {
            var pessoa = new Pessoa("Maria","Silva",new RacaCor("Negra"),new Escolaridade("Médio"),new Filiacao("Joana"));
            pessoa.AddFilho(new Pessoa("Paulo", "Silva", new RacaCor("Negra"), new Escolaridade("Médio"),new Filiacao("Maria")));

            Assert.True(pessoa.IsValid);
            Assert.Equal(pessoa.Filhos.Count, 1);
        }
        [Fact]
        public void Deve_retornar_notificacao_quando_inclusao_de_filho_falhar()
        {
            var pessoa = new Pessoa("Maria", "Silva", new RacaCor("Negra"), new Escolaridade("Médio"), new Filiacao("Joana"));
            pessoa.AddFilho(new Pessoa("Paulo", "Silva", new RacaCor("Negra"), new Escolaridade("Médio"), null));

            Assert.False(pessoa.IsValid);
        }

    }
}
