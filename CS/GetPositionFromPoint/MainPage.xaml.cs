using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using System.Windows.Resources;
using System.IO;
using DevExpress.XtraRichEdit.Utils;
namespace GetPositionFromPoint
{
    public partial class MainPage : UserControl
    {
      public MainPage()
        {
            InitializeComponent();
            richEdit.ApplyTemplate();
            richEdit.RichControl.MouseLeftButtonUp += RichControl_MouseLeftButtonUp;
            richEdit.RichControl.LoadDocument(GetStream("sample.rtf"), DocumentFormat.Rtf);

        }

      #region #mouseclick
      void RichControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
{
    Point point = e.GetPosition((UIElement)richEdit.RichControl.Parent);
    System.Drawing.Point pt = Units.PixelsToDocuments(new System.Drawing.Point((int)point.X,
        (int)point.Y), richEdit.RichControl.DpiX, richEdit.RichControl.DpiY);
    DocumentPosition pos = richEdit.RichControl.GetPositionFromPoint(pt);
    if (pos != null)
    {
        DocumentRange range = richEdit.RichControl.Document.CreateRange(pos, 1);
        tbClickedLetter.Text = richEdit.RichControl.Document.GetText(range);
    }
}
      #endregion #mouseclick
      static Stream GetStream(string filename)
        {
            StreamResourceInfo sr = Application.GetResourceStream(new Uri(filename, UriKind.Relative));
            return sr.Stream;
        }
    }
    }
