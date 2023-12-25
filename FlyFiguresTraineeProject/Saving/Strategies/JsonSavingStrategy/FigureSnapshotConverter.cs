using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using FlyFiguresTraineeProject.Figures.Data;
using FlyFiguresTraineeProject.Saving.Models.Snapshots;
using FlyFiguresTraineeProject.Utils;

namespace FlyFiguresTraineeProject.Saving.Strategies.JsonSavingStrategy;

public class FigureSnapshotConverter : JsonConverter<MovableFigureSnapshot>
{
    public override MovableFigureSnapshot? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var copyOfReader = reader;
        copyOfReader.Read(); // skip start object
        copyOfReader.Read(); // skip name of property name
        var typeNameOfFigure = copyOfReader.GetString()!;

        using var jsonDocument = JsonDocument.ParseValue(ref reader);
        var json = jsonDocument.RootElement.GetRawText();

        var configuredFigure = ConfiguredFigureData.Data.First(f => f.Key.FullName == typeNameOfFigure).Value;
        return (MovableFigureSnapshot)JsonSerializer.Deserialize(json, configuredFigure.FigureSnapshotType)!;
    }

    public override void Write(Utf8JsonWriter writer, MovableFigureSnapshot snapshot, JsonSerializerOptions options)
    {
        writer.WriteRawValue(JsonSerializer.Serialize(snapshot, snapshot.GetType()));
        // writer.WriteStartObject();
        //
        // var properties = snapshot.GetType().GetProperties();
        // foreach (var property in properties)
        // {
        //     writer.WritePropertyName(property.Name);
        //     writer.WriteRawValue(JsonSerializer.Serialize(property.GetValue(snapshot)));
        // }
        //     
        // writer.WriteEndObject();
    }
}