using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using FlyFiguresTraineeProject.Saving.Models.Snapshots;

namespace FlyFiguresTraineeProject.Saving.Strategies.JsonSavingStrategy;

public class FigureSnapshotV1Converter : JsonConverter<MovableFigureSnapshot>
{
    public override MovableFigureSnapshot? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        reader.Read(); // skip start object
        reader.Read(); // skip name of property SnapshotName
        var snapshotName = reader.GetString()!;

        reader.Read(); // skip value of property SnapshotName
        reader.Read(); // skip name of property Data
        using var jsonDocument = JsonDocument.ParseValue(ref reader);
        var json = jsonDocument.RootElement.GetRawText();
        reader.Read(); // end object
        
        var snapshotType = AvailableFigureSnapshots.SnapshotTypesByName[snapshotName];
        return (MovableFigureSnapshot)JsonSerializer.Deserialize(json, snapshotType)!;
    }

    public override void Write(Utf8JsonWriter writer, MovableFigureSnapshot snapshot, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        
        writer.WritePropertyName("SnapshotName");
        writer.WriteStringValue(snapshot.SnapshotName);
        
        writer.WritePropertyName("Data");
        writer.WriteRawValue(JsonSerializer.Serialize(snapshot, snapshot.GetType()));
        
        writer.WriteEndObject();
    }
}