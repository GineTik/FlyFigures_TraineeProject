using System.Collections;
using System.Collections.Generic;

namespace FlyFiguresTraineeProject.Saving;

public class AvailableFileTypes : IEnumerable<string>
{
    public const string Json = "json";
    public const string Xml = "xml";
    public const string Bin = "bin";
    
    public static AvailableFileTypes Instance { get; } = new AvailableFileTypes();
    public IEnumerator<string> GetEnumerator()
    {
        yield return Json;
        // yield return Xml;
        // yield return Bin;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}