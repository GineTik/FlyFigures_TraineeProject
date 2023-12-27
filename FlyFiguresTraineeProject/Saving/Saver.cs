using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FlyFiguresTraineeProject.Saving.Models;
using FlyFiguresTraineeProject.Saving.Strategies;
using FlyFiguresTraineeProject.Saving.Strategies.Bin;
using FlyFiguresTraineeProject.Saving.Strategies.Json;
using FlyFiguresTraineeProject.Saving.Strategies.Xml;
using Microsoft.Win32;

namespace FlyFiguresTraineeProject.Saving;

public static class Saver
{
    /// <param name="fileType">only values from <see cref="AvailableFileTypes"/></param>
    /// <param name="savingState">state of figures and language to save</param>
    public static async Task Save(string fileType, SavingState savingState)
    {
        var saveFileDialog = new SaveFileDialog
        {
            Filter = $"{fileType} files (*.{fileType})|*.{fileType}",
            FilterIndex = 2,
            RestoreDirectory = true
        };

        if (saveFileDialog.ShowDialog() == false)
            return;

        await using var stream = saveFileDialog.OpenFile();

        ISavingStrategy strategy = fileType switch
        {
            AvailableFileTypes.Json => new JsonSavingStrategy(),
            AvailableFileTypes.Xml => new XmlSavingStrategy(),
            AvailableFileTypes.Bin => new BinSavingStrategy(),
            _ => throw new ArgumentException(nameof(fileType) + " incorrect type")
        };
        
        await strategy.Save(stream, savingState);
        stream.Close();
    }

    public static async Task<SavingState?> Load()
    {
        var saveFileDialog = new OpenFileDialog
        {
            Filter = $"{string.Join(", ", AvailableFileTypes.Instance)}|{string.Join(";", AvailableFileTypes.Instance.Select(type => $"*.{type}"))}",
            FilterIndex = 1,
            RestoreDirectory = true
        };
        
        if (saveFileDialog.ShowDialog() == false)
            return null;

        await using var stream = (FileStream)saveFileDialog.OpenFile();

        ISavingStrategy strategy = Path.GetExtension(stream.Name).TrimStart('.') switch
        {
            AvailableFileTypes.Json => new JsonSavingStrategy(),
            AvailableFileTypes.Xml => new XmlSavingStrategy(),
            AvailableFileTypes.Bin => new BinSavingStrategy(),
            _ => throw new ArgumentException("Incorrect file type")
        };

        return await strategy.Load(stream);
    }
}