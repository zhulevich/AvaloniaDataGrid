using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaDataGrid.ViewModels;
using System;
using System.Collections.Generic;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.IO;
using Avalonia.Data;
using System.Linq;

namespace AvaloniaDataGrid.Views
{
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}