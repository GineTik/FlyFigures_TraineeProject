using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using FlyFiguresTraineeProject.Saving.Models;

namespace FlyFiguresTraineeProject.Saving.Strategies.Json
{
    public class SavingStateV1Converter : JsonConverter<SavingState>
    {
        public override SavingState? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            reader.Read(); // start object
        
            reader.Read(); // property version
            var version = reader.GetString()!;
            reader.Read(); // property version value

            reader.Read(); // property saving data
            using var jsonDocument = JsonDocument.ParseValue(ref reader);
            var json = jsonDocument.RootElement.GetRawText();
            reader.Read(); // property saving data value
        
            reader.Read(); // end object

            var figureSnapshotConverter = version switch
            {
                "v1" => new FigureSnapshotV1Converter(),
                _ => throw new ArgumentException("Version is unknown")
            };
        
            return new SavingState
            {
                SavingVersion = version,
                Data = JsonSerializer.Deserialize<SavingStateData>(json, new JsonSerializerOptions
                {
                    Converters = { figureSnapshotConverter }
                })!
            };
        }

        public override void Write(Utf8JsonWriter writer, SavingState value, JsonSerializerOptions options)
        {
            writer.WriteRawValue(JsonSerializer.Serialize(value, new JsonSerializerOptions
            {
                Converters = { new FigureSnapshotV1Converter() }
            }));
        }
    }
}