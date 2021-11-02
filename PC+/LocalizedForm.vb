Imports System.ComponentModel
Imports System.Globalization
Imports System.Threading

''' <summary>
''' Base class for all forms that need to be localized
''' </summary>
''' <remarks>
''' Localization code adapted from C# code by stackoverflow.com user mnn found in this Jul 31 '12 entry:
''' http://stackoverflow.com/questions/11711426/proper-way-to-change-language-at-runtime
''' </remarks>
Public Class LocalizedForm

    Public Sub New()
        resManager = New ComponentResourceManager(Me.GetType)
        _currentCulture = CultureInfo.CurrentUICulture
        InitializeComponent()
    End Sub

    ''' <summary>
    ''' Changes the Culture property on all open LocalizedForms
    ''' </summary>
    Public Shared Property GlobalUICulture() As CultureInfo
        Get
            Return Thread.CurrentThread.CurrentUICulture
        End Get
        Set(ByVal value As CultureInfo)
            If Not GlobalUICulture.Equals(value) Then
                Thread.CurrentThread.CurrentCulture = value
                Thread.CurrentThread.CurrentUICulture = value
                For Each form In Application.OpenForms.OfType(Of LocalizedForm)()
                    form.CurrentCulture = value
                Next
            End If
        End Set
    End Property

    Private _currentCulture As CultureInfo
    Protected resManager As ComponentResourceManager

    ''' <summary>
    ''' Current culture of this form
    ''' </summary>
    Public Property CurrentCulture() As CultureInfo
        Get
            Return _currentCulture
        End Get
        Set(ByVal value As CultureInfo)
            If Not _currentCulture.Equals(value) Then
                _currentCulture = value
                ApplyResources()
            End If
            _currentCulture = value
        End Set
    End Property

    Protected Overridable Sub ApplyResources()
        ApplyControlResources(Me)
    End Sub

    Private Sub ApplyControlResources(control As Control)
        ' TODO: Temporary fix for disappearing image, investigate
        Console.WriteLine("applying resource control is " & control.Name)
        Try
            If Not TypeOf control Is PictureBox Then
                resManager.ApplyResources(control, control.Name, CurrentCulture)
                'Console.WriteLine("aplying resource")
            End If

            For Each subControl In control.Controls
                ApplyControlResources(subControl)
            Next

            If TypeOf control Is ListView Then
                Dim listViewControl As ListView = CType(control, ListView)
                For Each header As ColumnHeader In listViewControl.Columns
                    Console.WriteLine("applying Resource to base control " & control.Name & " header is : " & header.ToString & " " & header.Tag & vbCrLf)

                    ' Hack: designer doesn't set name, known VS issue. Remember to set tags on ListView columns
                    resManager.ApplyResources(header, header.Tag, CurrentCulture)
                Next
            End If
        Catch ex As Exception
            Console.WriteLine("an error occured " & ex.Message & " and control is " & control.Name)
        End Try

    End Sub

    'Private Sub ApplyControlResources(control As Control)
    '    ' TODO: Temporary fix for disappearing image, investigate
    '    'Console.WriteLine("ABOUT TO APPLY RESOURCES " & vbCrLf)
    '    Try
    '        If Not TypeOf control Is PictureBox Then
    '            resManager.ApplyResources(control, control.Name, CurrentCulture)
    '        End If

    '        For Each subControl In control.Controls
    '            Console.WriteLine(subControl)
    '            ApplyControlResources(subControl)
    '        Next

    '        If TypeOf control Is ListView Then
    '            Dim listViewControl As ListView = CType(control, ListView)
    '            For Each header As ColumnHeader In listViewControl.Columns
    '                Console.WriteLine(header)
    '                ' Hack: designer doesn't set name, known VS issue. Remember to set tags on ListView columns
    '                resManager.ApplyResources(header, header.Tag, CurrentCulture)
    '            Next
    '        End If
    '    Catch ex As Exception
    '        Console.WriteLine(ex.Message)
    '    End Try

    'End Sub
End Class