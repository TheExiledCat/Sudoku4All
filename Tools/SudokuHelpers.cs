using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Avalonia.Platform.Storage;

namespace Sudoku.Tools;

public static class SudokuHelpers
{
    public static Sudoku.Shared.Sudoku? LoadFromSdku(IStorageFile storageFile)
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Shared.Sudoku));
            Task<Stream> fileStream = storageFile.OpenReadAsync();
            fileStream.Wait();
            return (Sudoku.Shared.Sudoku?)serializer.Deserialize(fileStream.Result);
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
            XmlSerializer serializer = new XmlSerializer(typeof(Shared.Sudoku));
            Task<Stream> fileStream = storageFile.OpenWriteAsync();
            fileStream.Wait();
            serializer.Serialize(fileStream.Result, sudoku);
            return true;
        }
        catch
        {
            ;
        }
        return false;

    }
}
