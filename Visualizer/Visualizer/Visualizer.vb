Imports IronPython.Hosting
Imports IronPython.Runtime
Imports Microsoft.Scripting.Hosting
Public Class Visualizer
    Public Engine As ScriptEngine = Python.CreateEngine
    Public Generator As RegionGenerator
    Public Map As Integer()()
    Public Unit As Integer = 30
    Public Sub Refresh()
        Generate(level_textbox.Value, seed_textbox.Value)
        Canvas.Invalidate()
    End Sub
    Public Sub Generate(Level As Integer, Seed As Integer)
        If Generator IsNot Nothing Then
            If Generator.CanAttach(height_textbox.Value) Then
                Generator.Init(Seed)
                Dim map As New List(Of Integer())
                Dim output As PythonTuple = Generator.Generate(Level, height_textbox.Value)
                Dim region As List = output(0)
                Dim l As List(Of Integer) = New List(Of Integer)
                For Each column As List In region
                    For Each obj As Integer In column
                        l.Add(obj)
                    Next
                    map.Add(l.ToArray)
                    l.Clear()
                Next
                Me.Map = map.ToArray()
            End If
        End If
    End Sub
    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub
    Dim Random As New Random
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        level_textbox.Value = Random.Next(1, 50)
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        seed_textbox.Value = Random.Next(-1000000, 1000000)
    End Sub
    Private Sub LoadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadToolStripMenuItem.Click
        Using ofd As New OpenFileDialog
            ofd.Filter = "Python files (*.py) | *.py"
            Dim r = ofd.ShowDialog()
            Dim file = ofd.FileName
            If r = DialogResult.OK Then
                Generator = New RegionGenerator(Engine, IO.File.ReadAllText(file))
                name_label.Text = IO.Path.GetFileName(file)
            End If
        End Using
    End Sub
    Private Sub level_textbox_ValueChanged(sender As Object, e As EventArgs) Handles level_textbox.ValueChanged
        Refresh()
    End Sub
    Private Sub seed_textbox_ValueChanged(sender As Object, e As EventArgs) Handles seed_textbox.ValueChanged
        Refresh()
    End Sub
    Dim o As Point
    Dim is_down As Boolean = False
    Dim oo As Point
    Dim down As Point
    Private Sub Canvas_MouseDown(sender As Object, e As MouseEventArgs) Handles Canvas.MouseDown
        is_down = True
        down = e.Location
        oo = o
    End Sub
    Private Sub Canvas_MouseUp(sender As Object, e As MouseEventArgs) Handles Canvas.MouseUp
        is_down = False
    End Sub
    Private Sub Canvas_MouseMove(sender As Object, e As MouseEventArgs) Handles Canvas.MouseMove
        Refresh()
        If is_down Then
            o = oo - down + e.Location
        End If
    End Sub
    Private Sub Canvas_Paint(sender As Object, e As PaintEventArgs) Handles Canvas.Paint
        Dim g = e.Graphics
        g.Clear(Color.Black)
        If Map IsNot Nothing Then
            For x = 0 To Map.Length - 1
                For y = 0 To Map(x).Length - 1
                    If Map(x)(y) <> 0 Then
                        g.FillRectangle(Brushes.White, New Rectangle(o.X + Unit * x, o.Y - Unit * y, Unit, Unit))
                        Dim Rect = g.MeasureString(Map(x)(y).ToString, Font)
                        g.DrawString(Map(x)(y).ToString, Font, Brushes.Black, New PointF(o.X + Unit * x + Unit / 2 - Rect.Width / 2, o.Y - Unit * y + Unit / 2 - Rect.Height / 2))
                    End If
                Next
            Next
        End If
    End Sub
    Private Sub Visualizer_Load(sender As Object, e As EventArgs) Handles Me.Load
        o = New Point(0, Canvas.Height - Unit)
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        height_textbox.Value = Random.Next(0, 10)
    End Sub
    Private Sub height_textbox_ValueChanged(sender As Object, e As EventArgs) Handles height_textbox.ValueChanged
        Refresh()
    End Sub
End Class
Public Class RegionGenerator
    Public ReadOnly Engine As ScriptEngine
    Public ReadOnly Scope As ScriptScope
    Private _source As ScriptSource
    Private _init As Func(Of Integer, Object)
    Private _probability As Func(Of Integer, Single)
    Private _canAttach As Func(Of Integer, Boolean)
    Private _generate As Func(Of Integer, Integer, PythonTuple)
    Public Sub Init(Seed As Integer)
        _init.Invoke(Seed)
    End Sub
    Public Function Probability(Level As Integer) As Single
        Return _probability.Invoke(Level)
    End Function
    Public Function CanAttach(Height As Integer) As Boolean
        Return _canAttach.Invoke(Height)
    End Function
    Public Function Generate(Level As Integer, Height As Integer) As PythonTuple
        Return _generate.Invoke(Level, Height)
    End Function
    Public Sub New(engine As ScriptEngine, script As String)
        MyBase.New
        Me.Engine = engine
        Scope = engine.CreateScope
        _source = engine.CreateScriptSourceFromString(script)
        _source.Execute(Scope)
        _init = Scope.GetVariable(Of Func(Of Int32, Object))("Init")
        _probability = Scope.GetVariable(Of Func(Of Int32, Single))("Probability")
        _canAttach = Scope.GetVariable(Of Func(Of Int32, Boolean))("CanAttach")
        _generate = Scope.GetVariable(Of Func(Of Int32, Int32, PythonTuple))("Generate")
    End Sub
End Class