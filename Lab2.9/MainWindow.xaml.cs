using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

//Доработать проект текстового редактора из задания 8, добавив возможность выбора светлой и темной темы

namespace Lab2._8
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void fontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string fontName = (sender as ComboBox).SelectedItem as string;
            if (richTB != null)
                richTB.FontFamily = new FontFamily(fontName);
        }

        private void fontSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            double fontSize = Convert.ToDouble((sender as ComboBox).SelectedItem.ToString());
            if (richTB != null)
                richTB.FontSize = fontSize;
        }

        private void fontColor_Checked(object sender, RoutedEventArgs e)
        {
            if(dark.IsChecked)
                richTB.Foreground = blackColor.IsChecked.Value ? Brushes.White : Brushes.Red;
            else
                richTB.Foreground = blackColor.IsChecked.Value ? Brushes.Black : Brushes.Red;
        }

        private void OpenExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Rich Text Format (*.rtf)|*.rtf|Все файлы (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                FileStream fileStream = new FileStream(openFileDialog.FileName, FileMode.Open);
                TextRange range = new TextRange(richTB.Document.ContentStart, richTB.Document.ContentEnd);
                range.Load(fileStream, DataFormats.Rtf);
            }
        }

        private void SaveExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Rich Text Format (*.rtf)|*.rtf|Все файлы (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                FileStream fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create);
                TextRange range = new TextRange(richTB.Document.ContentStart, richTB.Document.ContentEnd);
                range.Save(fileStream, DataFormats.Rtf);
            }
        }

        private void CloseExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }

        private void dark_Click(object sender, RoutedEventArgs e)
        {
            bool isDarkChecked = dark.IsChecked;
            Uri uri = new Uri("Light.xaml", UriKind.Relative);
            if (isDarkChecked)
            {
                uri = new Uri("Dark.xaml", UriKind.Relative);
                blackColor.Content = "Белый";
            }
            else
            {
                blackColor.Content = "Чёрный";
            }
            ResourceDictionary resource = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resource);
        }
    }
}
