using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using FlyFiguresTraineeProject.Saving.Models;

namespace FlyFiguresTraineeProject.Saving.Strategies.Xml;

public class XmlSavingStrategy : ISavingStrategy
{
    public Task Save(Stream file, SavingState state)
    {
        CreateXmlSerializer().Serialize(file, state);
        return Task.CompletedTask;
    }

    public Task<SavingState> Load(Stream file)
    {
        var state = (SavingState)CreateXmlSerializer().Deserialize(file)!;
        return Task.FromResult(state);
    }
    
    private static XmlSerializer CreateXmlSerializer() => new(typeof(SavingState),
        AvailableFigureSnapshots.SnapshotTypesByName.Select(pair => pair.Value).ToArray());
}