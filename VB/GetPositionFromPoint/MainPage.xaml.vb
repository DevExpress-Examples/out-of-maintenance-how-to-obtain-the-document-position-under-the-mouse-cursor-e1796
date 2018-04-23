Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports System.Windows.Resources
#Region "#usings"
Imports DevExpress.XtraRichEdit
Imports DevExpress.XtraRichEdit.API.Native
Imports DevExpress.XtraRichEdit.Utils
Imports DevExpress.Office.Utils
#End Region ' #usings

Namespace GetPositionFromPoint
	Partial Public Class MainPage
		Inherits UserControl
		Public Sub New()
			InitializeComponent()
			AddHandler richEditControl1.Loaded, AddressOf richEditControl1_Loaded
		End Sub

		Private Shared Function GetStream(ByVal filename As String) As Stream
			Dim sr As StreamResourceInfo = Application.GetResourceStream(New Uri(filename, UriKind.Relative))
			Return sr.Stream
		End Function

		Private Sub richEditControl1_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)

				richEditControl1.LoadDocument(GetStream("sample.rtf"), DocumentFormat.Rtf)
				richEditControl1.Unit =DevExpress.Office.DocumentUnit.Document

		End Sub
		#Region "#mousemove"
		Private Sub richEditControl1_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs)
			Dim point As Point = e.GetPosition(CType(richEditControl1, UIElement))
			Dim pt As System.Drawing.Point = Units.PixelsToDocuments(New System.Drawing.Point(CInt(Fix(point.X)), CInt(Fix(point.Y))), richEditControl1.DpiX, richEditControl1.DpiY)

			Dim pos As DocumentPosition = richEditControl1.GetPositionFromPoint(pt)
			If pos IsNot Nothing Then
				Dim range As DocumentRange = richEditControl1.Document.CreateRange(pos, 1)
				tbClickedLetter.Text = richEditControl1.Document.GetText(range)
			Else
				tbClickedLetter.Text = ""
			End If
		End Sub
		#End Region ' #mousemove

	End Class
End Namespace
