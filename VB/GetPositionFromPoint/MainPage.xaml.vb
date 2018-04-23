Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports System.Windows.Resources
Imports DevExpress.XtraRichEdit
Imports DevExpress.XtraRichEdit.API.Native
Imports DevExpress.XtraRichEdit.Utils

Namespace GetPositionFromPoint
	Partial Public Class MainPage
		Inherits UserControl
		Public Sub New()
			InitializeComponent()
			richEdit1.ApplyTemplate()
			AddHandler richEdit1.Loaded, AddressOf richEdit1_Loaded
		End Sub

		#Region "#mouseclick"
		Private Sub richEdit1_MouseLeftButtonUp(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
			Dim point As Point = e.GetPosition(CType(richEdit1.RichControl.Parent, UIElement))
			Dim pt As System.Drawing.Point = Units.PixelsToDocuments(New System.Drawing.Point(CInt(Fix(point.X)), CInt(Fix(point.Y))), richEdit1.RichControl.DpiX, richEdit1.RichControl.DpiY)

			Dim pos As DocumentPosition = richEdit1.RichControl.GetPositionFromPoint(pt)
			If pos IsNot Nothing Then
				Dim range As DocumentRange = richEdit1.RichControl.Document.CreateRange(pos, 1)
				tbClickedLetter.Text = richEdit1.RichControl.Document.GetText(range)
			End If
		End Sub
		#End Region ' #mouseclick
		Private Shared Function GetStream(ByVal filename As String) As Stream
			Dim sr As StreamResourceInfo = Application.GetResourceStream(New Uri(filename, UriKind.Relative))
			Return sr.Stream
		End Function

		Private Sub richEdit1_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
			If richEdit1.RichControl IsNot Nothing Then
				richEdit1.RichControl.LoadDocument(GetStream("sample.rtf"), DocumentFormat.Rtf)
				'richEdit1.RichControl.LayoutUnit = DocumentLayoutUnit.Pixel;
				richEdit1.RichControl.Unit = DocumentUnit.Document
				richEdit1.RichControl.FocusKeyCodeTextBox()
			End If
		End Sub




	End Class
End Namespace
