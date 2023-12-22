using System.Globalization;
using WPFLocalizeExtension.Engine;

namespace FlyFiguresTraineeProject.Languages;

public static class LanguageSwitcher
{
    static LanguageSwitcher()
    {
        LocalizeDictionary.Instance.SetCurrentThreadCulture = true;
    }
    
    public static void Switch(CultureInfo culture)
    {
        LocalizeDictionary.Instance.Culture = culture;
    }
}