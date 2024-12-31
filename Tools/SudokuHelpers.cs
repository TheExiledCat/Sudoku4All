using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Avalonia.Platform.Storage;
using YAXLib;
using YAXLib.Enums;
using YAXLib.Options;

namespace Sudoku.Tools;

public static class SudokuHelpers
{
    public static Sudoku.Shared.Sudoku? LoadFromSdku(IStorageFile storageFile)
    {
        try
        {
            YAXSerializer<Shared.Sudoku> serializer = new YAXSerializer<Shared.Sudoku>(
                new SerializerOptions() { }
            );
            Task<Stream> fileStream = storageFile.OpenReadAsync();
            fileStream.Wait();
            XmlReader reader = XmlReader.Create(fileStream.Result);
            return (Sudoku.Shared.Sudoku?)serializer.Deserialize(reader);
        }
        catch
        {
            return null;
        }
    }

    public static bool SaveSudoku(Shared.Sudoku sudoku, IStorageFile storageFile)
    {
        try
        {
            YAXSerializer<Shared.Sudoku> serializer = new YAXSerializer<Shared.Sudoku>(
                new SerializerOptions() { }
            );
            Task<Stream> fileStream = storageFile.OpenWriteAsync();
            fileStream.Wait();
            XmlWriter writer = XmlWriter.Create(fileStream.Result);

            serializer.SerializeToFile(sudoku, storageFile.Path.AbsolutePath);

            return true;
        }
        catch
        {
            throw;
        }
        return false;
    }
}
