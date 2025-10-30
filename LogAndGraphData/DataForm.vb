'Angel Nava
'Spring 2025
'RCET2265
'SayMyName
'github link
Option Strict On
Option Explicit On


Public Class DataForm

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

    Function GetData() As Integer
        Return 5
    End Function

    Sub GraphData()
        Dim g As Graphics = GraphPictureBox.CreateGraphics
        Dim pen As New Pen(Color.Wheat)
        Dim scaleX As Single = CSng(GraphPictureBox.Width / 100)
        Dim scaleY As Single = CSng((GraphPictureBox.Height / 100) * -1)
        Dim oldY% = GetRandomNumberAround(50, 50)
        Dim newY% = 50

        g.TranslateTransform(0, GraphPictureBox.Height) 'moves the origin to bottom
        g.ScaleTransform(scaleX, scaleY) 'scale to 100 x 100
        pen.Width = 0.25 'pen size


        For x = 0 To 100
            newY = GetRandomNumberAround(oldY, 5)
            g.DrawLine(pen, x - 1, oldY, x, newY)
            oldY = newY
        Next

        g.Dispose()
        pen.Dispose()
    End Sub

    'Event Handlers----------------------------------------------------------------------------------------------------------------------


    Private Sub ExitButton_Click(sender As Object, e As EventArgs) Handles ExitButton.Click
        Me.Close()
    End Sub

    Private Sub GraphButton_Click(sender As Object, e As EventArgs) Handles GraphButton.Click
        GraphData()
        For i = 1 To 100
            Console.WriteLine(GetRandomNumberAround(50, 10))
        Next
    End Sub
End Class
