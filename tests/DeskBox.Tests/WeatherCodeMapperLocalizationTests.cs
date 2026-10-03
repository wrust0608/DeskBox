using DeskBox.Helpers;

namespace DeskBox.Tests;

public sealed class WeatherCodeMapperLocalizationTests
{
    [Theory]
    [InlineData("zh-TW", "晴")]
    [InlineData("hi-IN", "साफ आसमान")]
    [InlineData("es-ES", "Cielo despejado")]
    [InlineData("fr-FR", "Ciel dégagé")]
    [InlineData("ar-SA", "سماء صافية")]
    [InlineData("bn-BD", "পরিষ্কার আকাশ")]
    [InlineData("ru-RU", "Ясное небо")]
    [InlineData("vi-VN", "Trời quang")]
    public void NewLocales_LocalizeWeatherDescription(string locale, string expected)
    {
        Assert.Equal(expected, WeatherCodeMapper.GetDescription(0, locale));
    }

    [Theory]
    [InlineData(0, "Trời quang")]
    [InlineData(48, "Sương mù đóng băng")]
    [InlineData(66, "Mưa đóng băng")]
    [InlineData(77, "Hạt tuyết")]
    [InlineData(85, "Tuyết rơi từng đợt")]
    [InlineData(86, "Tuyết rơi dày từng đợt")]
    [InlineData(95, "Dông")]
    public void Vietnamese_WeatherCodes_MapCorrectly(int code, string expected)
    {
        Assert.Equal(expected, WeatherCodeMapper.GetDescription(code, "vi-VN"));
    }
}
