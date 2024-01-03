using System;
using System.IO;

namespace FlyFiguresTraineeProject.Logging;

public class FileLogger
{
    public static void Log<TSource>(string information)
        where TSource : class
    {
        using var writer = File.AppendText("./../../../" + typeof(TSource).Name + ".txt");
        writer.Write("\r\nLog Entry : ");
        writer.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
            DateTime.Now.ToLongDateString());
        writer.WriteLine("{0}", information);
        writer.WriteLine("-------------------------------");
    }
}