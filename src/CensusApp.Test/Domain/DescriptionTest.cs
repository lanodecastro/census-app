using CensusApp.Api.Core.Domain.Extensions;
using CensusApp.Api.Core.Domain.Vo;
using Xunit;

namespace CensusApp.Test.Domain
{
    public class DescriptionTest
    {
        [Theory]
        [InlineData(null, "val_is_not_null", "Valor", 0, 6)]
        [InlineData("val", "Valor_is_not_null",null, 3, 6)]
        [InlineData("FUAVrLcstl", "minlength_valid", "Valor", 11, 60)]
        [InlineData("LJGuqIzKVJ", "maxlength_valid", "Valor", 1, 9)]

        public void Description_deve_conter_notificacao_quando_propriedade_for_invalida(string val, string key, string prop, int minlength, int maxlength)
        {
            var description = new Description(val, key, prop, minlength, maxlength);

            Assert.True(!description.IsValid);
            Assert.True(description.HasNotification(key));
            Assert.True(description.CountNotificationEquals(1));
        }
    }
}
