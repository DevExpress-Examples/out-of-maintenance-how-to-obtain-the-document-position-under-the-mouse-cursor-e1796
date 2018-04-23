using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Resources;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraRichEdit.Utils;

namespace GetPositionFromPoint
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            richEdit1.ApplyTemplate();
            richEdit1.Loaded += richEdit1_Loaded;
        }

        #region #mouseclick
        private void richEdit1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition((UIElement)richEdit1.RichControl.Parent);
            System.Drawing.Point pt = Units.PixelsToDocuments(new System.Drawing.Point((int)point.X,
        (int)point.Y), richEdit1.RichControl.DpiX, richEdit1.RichControl.DpiY);
            
            DocumentPosition pos = richEdit1.RichControl.GetPositionFromPoint(pt);
            if (pos != null)
            {
                DocumentRange range = richEdit1.RichControl.Document.CreateRange(pos, 1);
                tbClickedLetter.Text = richEdit1.RichControl.Document.GetText(range);
            }
        }
        #endregion #mouseclick
        static Stream GetStream(string filename)
        {
            StreamResourceInfo sr = Application.GetResourceStream(new Uri(filename, UriKind.Relative));
            return sr.Stream;
        }

        private void richEdit1_Loaded(object sender, RoutedEventArgs e)
        {
            if (richEdit1.RichControl != null)
            {
                richEdit1.RichControl.LoadDocument(GetStream("sample.rtf"), DocumentFormat.Rtf);
                //richEdit1.RichControl.LayoutUnit = DocumentLayoutUnit.Pixel;
                richEdit1.RichControl.Unit = DocumentUnit.Document;
                richEdit1.RichControl.FocusKeyCodeTextBox();
            }
        }

       


    }
}
