using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform.Storage;
using Avalonia.Threading;
using MsBox.Avalonia;
using MsBox.Avalonia.Base;
using MsBox.Avalonia.Enums;
using Sudoku.Tools;
using Sdk = Sudoku.Shared;

namespace Sudoku.Windows;

public partial class SudokuEditor : Window
{
    Sdk.Sudoku m_CurrentSudoku;
    IStorageFile? m_CurrentFile;
    bool m_SudokuChanged = true;
    public float Zoom { get; set; }
    public Vector Pan { get; set; }

    public SudokuEditor(Sdk.Sudoku? sudoku = null, IStorageFile? file = null)
    {
        InitializeComponent();
        if (sudoku == null)
        {
            sudoku = new Sdk.Sudoku();
        }

        m_CurrentFile = file;
        m_CurrentSudoku = sudoku;
        Resized += (s, e) =>
        {
            Toolbar.OpenPaneLength = Width / 4;
        };
        DataContext = m_CurrentSudoku;
    }

    public void SaveAsOrOverwriteHandler(object sender, RoutedEventArgs args)
    {
        if (m_CurrentFile == null)
        {
            SaveAs();
        }
        else if (m_SudokuChanged)
        {
            Overwrite();
        }
    }

    public void SaveAsHandler(object sender, RoutedEventArgs args)
    {
        SaveAs();
    }

    public void ToggleToolBarHandler(object sender, RoutedEventArgs args)
    {
        Toolbar.IsPaneOpen = !Toolbar.IsPaneOpen;
    }

    public void HomeMenuHandler(object sender, RoutedEventArgs args)
    {
        //check if unsaved progress

        //go to home
        MainWindow window = new MainWindow();
        window.Show();
        Close();
    }

    void ResetZoomHandler(object sender, RoutedEventArgs args)
    {
        UpdateZoom(1);
        ZoomSlider.Value = Zoom;
    }

    private void ZoomHandler(object sender, RangeBaseValueChangedEventArgs args)
    {
        UpdateZoom((float)args.NewValue);
    }

    void UpdateZoom(float newValue)
    {
        if (ZoomAmount != null)
        {
            Zoom = newValue;
            ZoomAmount.Text = Zoom.ToString("0.00");
            viewportZoomBorder.Zoom(
                Zoom,
                viewport.Viewport.Width / 2,
                viewport.Viewport.Height / 2
            );
        }
    }

    async Task Overwrite()
    {
        if (m_CurrentFile != null)
        {
            SudokuHelpers.SaveSudoku(m_CurrentSudoku, m_CurrentFile);
        }
    }

    async Task SaveAs()
    {
        string path = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "Sudoku"
        );
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        IStorageFolder startlocation = await StorageProvider.TryGetFolderFromPathAsync(path);

        IStorageFile? file = await StorageProvider.SaveFilePickerAsync(
            new FilePickerSaveOptions()
            {
                Title = "Choose name for file",
                DefaultExtension = ".sdku",
                FileTypeChoices = [new FilePickerFileType("Sudoku file") { Patterns = ["*.sdku"] }],
                ShowOverwritePrompt = true,
                SuggestedStartLocation = startlocation,
            }
        );
        if (file == null)
        {
            return;
        }
        m_CurrentFile = file;
        if (!SudokuHelpers.SaveSudoku(m_CurrentSudoku, m_CurrentFile))
        {
            IMsBox<ButtonResult> messageBox = MessageBoxManager.GetMessageBoxStandard(
                "Error",
                "Something went wrong while saving the file",
                MsBox.Avalonia.Enums.ButtonEnum.Ok
            );
            await messageBox.ShowAsPopupAsync(this);
            return;
        }

        m_SudokuChanged = false;
        UpdateTitle();
    }

    void UpdateTitle()
    {
        Title =
            $"Editor {(m_CurrentFile != null ? m_CurrentFile.Name : "")} {DateTime.Now.ToString("yyyy-mm-dd")}";
    }
}
