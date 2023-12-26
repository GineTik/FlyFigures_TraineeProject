using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using FlyFiguresTraineeProject.Saving.Models;

namespace FlyFiguresTraineeProject.Saving.Strategies.JsonSavingStrategy;

public class JsonSavingStrategy : ISaveStrategy
{
    public async Task Save(Stream file, SavingState state)
    {
        await JsonSerializer.SerializeAsync(file, state, new JsonSerializerOptions
        {
            Converters =
            {
                new SavingStateV1Converter()
            }
        });
    }

    public async Task<SavingState> Load(Stream file)
    {
        return await JsonSerializer.DeserializeAsync<SavingState>(file, new JsonSerializerOptions
         {
             Converters =
             {
                 new SavingStateV1Converter()
             }
         }) ?? throw new InvalidOperationException();
    }
}