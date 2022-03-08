using CensusApp.Api.Core.Domain.Extensions;
using CensusApp.Api.Core.Domain.Model;
using Xunit;

namespace CensusApp.Test
{
    public class PessoaTest
    {
        [Theory]
        [InlineData(null, "Silva", "01", "02", "nome_is_not_null")]
        [InlineData("Maria", null, "01", "02", "sobrenome_is_not_null")]
        [InlineData("Maria", "Silva", null, "02", "idpai_is_not_null")]
        [InlineData("Maria", "Silva", "01", null, "idmae_is_not_null")]

        public void Pessoa_deve_conter_notificacao_quando_propriedade_for_invalida(string nome, string sobrenome, string idPai, string idMae, string notificationKey)
        {
            var pessoa = new Pessoa(nome, sobrenome, new RacaCor("Negra"), new Escolaridade("Ensino Fundamental"), new Regiao("Norte"), idPai, idMae);

            Assert.False(pessoa.IsValid);
            Assert.True(pessoa.HasNotification(notificationKey));
        }

        [Theory]
        [InlineData("Maria", "Silva", "parda", "ensino médio", "sul", "02", "04")]
        public void Deve_atribuir_propriedades_quando_pessoa_for_valida(string nome, string sobrenome, string racaCor, string escolaridade, string regiao, string idPai, string idMae)
        {
            var pessoa = new Pessoa(nome, sobrenome, new RacaCor(racaCor), new Escolaridade(escolaridade), new Regiao(regiao), idPai, idMae);

            Assert.True(pessoa.IsValid);
            Assert.Equal(pessoa.Nome, nome);
            Assert.Equal(pessoa.Sobrenome, sobrenome);
            Assert.Equal(pessoa.RacaCor.Descricao, racaCor);
            Assert.Equal(pessoa.Escolaridade.Descricao, escolaridade);
            Assert.Equal(pessoa.Regiao.Descricao, regiao);
            Assert.Equal(pessoa.IdPai, idPai);
            Assert.Equal(pessoa.IdMae, idMae);
        }


    }
}
