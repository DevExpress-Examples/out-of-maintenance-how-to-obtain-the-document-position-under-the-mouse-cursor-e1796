using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Resources;
#region #usings
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraRichEdit.Utils;
#endregion #usings

namespace GetPositionFromPoint
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            richEditControl1.Loaded += richEditControl1_Loaded;
        }

        static Stream GetStream(string filename)
        {
            StreamResourceInfo sr = Application.GetResourceStream(new Uri(filename, UriKind.Relative));
            return sr.Stream;
        }

        private void richEditControl1_Loaded(object sender, RoutedEventArgs e)
        {

                richEditControl1.LoadDocument(GetStream("sample.rtf"), DocumentFormat.Rtf);
                richEditControl1.Unit = DocumentUnit.Document;

        }
        #region #mousemove
        private void richEditControl1_MouseMove(object sender, MouseEventArgs e)
        {
            Point point = e.GetPosition((UIElement)richEditControl1);
            System.Drawing.Point pt = Units.PixelsToDocuments(new System.Drawing.Point((int)point.X,
        (int)point.Y), richEditControl1.DpiX, richEditControl1.DpiY);

            DocumentPosition pos = richEditControl1.GetPositionFromPoint(pt);
            if (pos != null)
            {
                DocumentRange range = richEditControl1.Document.CreateRange(pos, 1);
                tbClickedLetter.Text = richEditControl1.Document.GetText(range);
            }
            else {
                tbClickedLetter.Text = "";
            }
        }
        #endregion #mousemove

    }
}
