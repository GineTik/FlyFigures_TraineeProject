using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace FlyFiguresTraineeProject.Languages;

public class AvailableLanguages : IEnumerable<Language>
{
    public static Language Uk { get; set; } = new Language
    {
        LanguageKey = "Ukrainian",
        CultureInfo = new CultureInfo("") // default
    };
    
    public static Language En { get; set; } = new Language
    {
        LanguageKey = "English",
        CultureInfo = new CultureInfo("en")
    };

    public static Language Default => Uk;
    
    public IEnumerator<Language> GetEnumerator()
    {
        yield return Uk;
        yield return En;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}