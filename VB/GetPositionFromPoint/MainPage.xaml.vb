Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Net
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Shapes
Imports DevExpress.XtraRichEdit
Imports DevExpress.XtraRichEdit.API.Native
Imports System.Windows.Resources
Imports System.IO
Imports DevExpress.XtraRichEdit.Utils
Namespace GetPositionFromPoint
	Partial Public Class MainPage
		Inherits UserControl
	  Public Sub New()
			InitializeComponent()
			richEdit.ApplyTemplate()
			AddHandler richEdit.RichControl.MouseLeftButtonUp, AddressOf RichControl_MouseLeftButtonUp
			richEdit.RichControl.LoadDocument(GetStream("sample.rtf"), DocumentFormat.Rtf)

	  End Sub

	  #Region "#mouseclick"
	  Private Sub RichControl_MouseLeftButtonUp(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
	Dim point As Point = e.GetPosition(CType(richEdit.RichControl.Parent, UIElement))
	Dim pt As System.Drawing.Point = Units.PixelsToDocuments(New System.Drawing.Point(CInt(Fix(point.X)), _
		CInt(Fix(point.Y))), richEdit.RichControl.DpiX, richEdit.RichControl.DpiY)
	Dim pos As DocumentPosition = richEdit.RichControl.GetPositionFromPoint(pt)
	If pos IsNot Nothing Then
		Dim range As DocumentRange = richEdit.RichControl.Document.CreateRange(pos, 1)
		tbClickedLetter.Text = richEdit.RichControl.Document.GetText(range)
	End If
	  End Sub
	  #End Region ' #mouseclick
	  Private Shared Function GetStream(ByVal filename As String) As Stream
			Dim sr As StreamResourceInfo = Application.GetResourceStream(New Uri(filename, UriKind.Relative))
			Return sr.Stream
	  End Function
	End Class
End Namespace
