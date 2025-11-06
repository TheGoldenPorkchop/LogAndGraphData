'Angel Nava
'Spring 2025
'RCET2265
'SayMyName
'github link
Option Strict On
Option Explicit On


Public Class DataForm
    Dim DataBuffer As New Queue(Of Integer)
    'Program Logic-----------------------------------------------------------------------------------------------------------------------


    Function GetRandomNumberAround(thisnumber%, Optional within% = 10) As Integer
        Dim result As Integer

        result = GetRandomNumber(2 * within) + (thisnumber - within)
        Return result
    End Function

    Function GetRandomNumber(max%, Optional min% = 0) As Integer
        Randomize()

        Return CInt(System.Math.Floor((Rnd() * (max + 1))))
    End Function

    Sub LogData(currentSample As Integer)
        Dim filePath As String = $"..\..\data_{DateTime.Now.ToString("yyMMddhh")}.log"
        Dim exactTime As String = DateTime.Now.ToString '("yyMMddhh") 'MODIFY FOR 
        FileOpen(1, filePath, OpenMode.Append)
        Write(1, DateTime.Now)
        Write(1, DateTime.Now.Millisecond)
        WriteLine(1, currentSample)
        FileClose(1)
    End Sub

    Sub GetData()
        'Me.DataBuffer.Last
        Dim _last%
        Dim sample%

        If Me.DataBuffer.Count > 0 Then
            _last = Me.DataBuffer.Last
        Else
            _last = GetRandomNumberAround(50, 50)
        End If

        If DataBuffer.Count >= 100 Then
            Me.DataBuffer.Dequeue()
        End If


        sample = GetRandomNumberAround(_last, 5)
        Me.DataBuffer.Enqueue(sample)
        LogData(sample)

    End Sub

    Sub GraphData()
        Dim g As Graphics = GraphPictureBox.CreateGraphics
        Dim pen As New Pen(Color.Lime)
        Dim eraser As New Pen(Color.Black)
        Dim scaleX! = CSng(GraphPictureBox.Width \ 100)
        Dim scaleY! = CSng((GraphPictureBox.Height \ 100) * -1)

        g.Clear(Color.Black)
        g.TranslateTransform(0, GraphPictureBox.Height) 'Moves origin to bottom left
        g.ScaleTransform(scaleX, scaleY) 'scale to 100 x 100 units
        pen.Width = 0.5

        Dim oldY% = 0 'eGetRandomNumberAround(50, 50)
        Dim x = -1
        For Each y In Me.DataBuffer.Reverse
            x += 1
            g.DrawLine(eraser, x, 0, x, 100)
            g.DrawLine(pen, x - 1, oldY, x, y)
            oldY = y
        Next


        g.Dispose()
        pen.Dispose()


    End Sub

    'Event Handlers----------------------------------------------------------------------------------------------------------------------


    Private Sub ExitButton_Click(sender As Object, e As EventArgs) Handles ExitButton.Click
        Me.Close()
    End Sub

    Private Sub GraphButton_Click(sender As Object, e As EventArgs) Handles GraphButton.Click
        If SampleTimer.Enabled Then
            SampleTimer.Stop()
            SampleTimer.Enabled = False
        Else
            SampleTimer.Enabled = True
            SampleTimer.Start()
        End If
        'GraphData()
        'GetData()

    End Sub

    Private Sub SampleTimer_Tick(sender As Object, e As EventArgs) Handles SampleTimer.Tick
        GraphData()
        GetData()
    End Sub
End Class
