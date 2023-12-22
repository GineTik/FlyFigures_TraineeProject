using System.Globalization;

namespace FlyFiguresTraineeProject.Languages;

public class Language
{
    public required string LanguageKey { get; set; }
    public required CultureInfo CultureInfo { get; set; }
}