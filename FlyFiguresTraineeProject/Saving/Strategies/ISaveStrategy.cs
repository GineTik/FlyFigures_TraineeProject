using System.IO;
using System.Threading.Tasks;
using FlyFiguresTraineeProject.Saving.Models;

namespace FlyFiguresTraineeProject.Saving.Strategies;

public interface ISaveStrategy
{
    Task Save(Stream file, SavingState state);
    Task<SavingState> Load(Stream file);
}