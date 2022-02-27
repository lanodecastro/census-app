using CensusApp.Api.Core.Domain;
using CensusApp.Api.Core.Domain.Extensions;
using Xunit;

namespace CensusApp.Test.Domain
{
    public class PessoaTest
    {
        [Theory]
        [InlineData(null, "Silva", RacaCorEnum.Branco, EscolaridadeEnum.Mestrado, "nome_is_not_null")]
        [InlineData("Maria", null, RacaCorEnum.Branco, EscolaridadeEnum.Mestrado, "sobrenome_is_not_null")]
        [InlineData("Maria", "Silva", null, EscolaridadeEnum.Mestrado, "racaCor_is_not_null")]
        [InlineData("Maria", "Silva", RacaCorEnum.Negro, null, "escolaridade_is_not_null")]
        public void Pessoa_deve_conter_notificacao_quando_propriedade_for_invalida(string nome, string sobrenome, RacaCorEnum? racaCor, EscolaridadeEnum? escolaridade, string notificationKey)
        {
            var pessoa = new Pessoa(nome, sobrenome, racaCor, escolaridade, new Filiacao("Joana"));

            Assert.False(pessoa.IsValid);
            Assert.True(pessoa.CountNotificationEquals(1));
            Assert.True(pessoa.HasNotification(notificationKey));
        }

        [Fact]
        public void Pessoa_deve_ser_invalida_quando_filiacao_for_nula()
        {
            var pessoa = new Pessoa("Paulo", "Silva", RacaCorEnum.Branco, EscolaridadeEnum.Medio, null);

            Assert.False(pessoa.IsValid);
            Assert.True(pessoa.CountNotificationEquals(1));

        }

        [Theory]
        [InlineData("Maria", "Silva", RacaCorEnum.Branco, EscolaridadeEnum.Mestrado)]
        public void Deve_atribuir_propriedades_quando_pessoa_for_valida(string nome, string sobrenome, RacaCorEnum? racaCor, EscolaridadeEnum? escolaridade)
        {
            var pessoa = new Pessoa(nome, sobrenome, racaCor, escolaridade, new Filiacao("Joana"));

            Assert.True(pessoa.IsValid);
            Assert.Equal(pessoa.Nome, nome);
            Assert.Equal(pessoa.Sobrenome, sobrenome);
            Assert.Equal(pessoa.RacaCor.Value, racaCor.Value);
            Assert.Equal(pessoa.Escolaridade.Value, escolaridade.Value);
        }

    }
}
