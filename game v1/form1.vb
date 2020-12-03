Public Class Form1
    Dim r As New Random
    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles ghost1.Click

    End Sub

    Private Sub PictureBox5_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles picturebox1.Click
    End Sub


    Sub move(p As PictureBox, x As Integer, y As Integer)
        p.Location = New Point(p.Location.X + x, p.Location.Y + y)
    End Sub



    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode

            Case Keys.R
                picturebox1.Image.RotateFlip(RotateFlipType.Rotate90FlipX)
                Me.Refresh()
            Case Keys.Up
                MoveTo(picturebox1, 0, -5)
            Case Keys.Down
                MoveTo(picturebox1, 0, 5)
            Case Keys.Left
                MoveTo(picturebox1, -5, 0)
            Case Keys.Right
                MoveTo(picturebox1, 5, 0)
            Case Keys.Space
                bullet1.Visible = True
                bullet1.Location = picturebox1.Location
                Timer1.Enabled = True
            Case Else

        End Select
    End Sub

    Sub follow(p As PictureBox)
        Static headstart As Integer
        Static c As New Collection
        c.Add(ghost6.Location)
        headstart = headstart + 1
        If headstart = 10 Then
            p.Location = c.Item(1)
            c.Remove(1)
        End If
    End Sub

    Private Sub Timer1_Tick_1(sender As Object, e As EventArgs) Handles Timer1.Tick
        chase(ghost6)
        chase(ghost3)
        chase(ghost1)
        MoveTo(bullet1, 5, 0)
    End Sub


    Public Sub chase(p As PictureBox)
        Dim x, y As Integer
        If p.Location.X > picturebox1.Location.X Then
            x = -5
        Else
            x = 5
        End If
        MoveTo(p, x, 0)
        If p.Location.Y < picturebox1.Location.Y Then
            y = 5
        Else
            y = -5
        End If
        MoveTo(p, x, y)
    End Sub




    Function Collission(p As PictureBox, t As String)
        Dim col As Boolean

        For Each c In Controls
            Dim obj As Control
            obj = c
            If p.Bounds.IntersectsWith(obj.Bounds) And obj.Name.ToUpper.Contains(t.ToUpper) Then
                col = True
            End If
        Next
        Return col
    End Function
    'Return true or false if moving to the new location is clear of objects ending with t
    Function IsClear(p As PictureBox, distx As Integer, disty As Integer, t As String) As Boolean
        Dim b As Boolean

        p.Location += New Point(distx, disty)
        b = Not Collission(p, t)
        p.Location -= New Point(distx, disty)
        Return b
    End Function
    'Moves and object (won't move onto objects containing  "wall" and shows green if object ends with "win"
    Sub MoveTo(p As PictureBox, distx As Integer, disty As Integer)
        If IsClear(p, distx, disty, "wall") Then
            p.Location += New Point(distx, disty)
        End If

        If p.Name.Contains("ghost") And Collission(p, "bullet") Then
            p.Visible = False
        End If
        If p.Name = "picturebox1" And Collission(p, "ghost") Then
            Me.BackColor = Color.Red

        End If
        Dim playing As String

        If p.Name = "picturebox1" And Collission(p, "winbox1") Then
            Me.BackColor = Color.Green
        End If
        If p.Name = "picturebox1" And Collission(p, "winbox1") Then
            Me.textbox10.Text = ("you win")
        End If
        If p.Name = "picturebox1" And Collission(p, "GHOST") Then
            Me.textbox10.Text = ("you LOSE ")
        End If
    End Sub

End Class
