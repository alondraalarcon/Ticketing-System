Imports System.Windows.Forms
Imports System.Text.RegularExpressions
Imports MySql.Data.MySqlClient
Imports System.Drawing
Imports System.IO

Public Class viewticket
    Public Sub loaddata()

        OpenConnection()
        Dim query As String = "SELECT max(id) FROM `tickets` WHERE `ticketnumber`='" & ticket & "';"
        Dim cmd As New MySqlCommand(query, Connection)
        Dim read As MySqlDataReader = cmd.ExecuteReader
        read.Read()
        TextBox1.Text = read.GetValue(0).ToString
        TextBox2.Text = read.GetValue(6).ToString
        TextBox3.Text = read.GetValue(12).ToString
        TextBox4.Text = read.GetValue(3).ToString
        TextBox5.Text = read.GetValue(1).ToString
        TextBox6.Text = read.GetValue(2).ToString
        TextBox7.Text = read.GetValue(11).ToString
        TextBox8.Text = read.GetValue(14).ToString
        TextBox9.Text = read.GetValue(13).ToString
        room.Text = read.GetValue(19).ToString
        Dim arrimage(), arrimage2() As Byte
        arrimage = read.GetValue(9)
        arrimage2 = read.GetValue(10)
        Dim ms As New System.IO.MemoryStream(arrimage)
        Dim ms2 As New System.IO.MemoryStream(arrimage2)
        Me.PictureBox1.Image = System.Drawing.Image.FromStream(ms)
        Me.PictureBox2.Image = System.Drawing.Image.FromStream(ms2)
        read.Close()
        Connection.Close()
        If TextBox3.Text = "New" Then
            Button1.Enabled = False
            Button2.Enabled = False
            Button3.Enabled = False
            Button17.Enabled = True
        ElseIf TextBox3.Text = "Pending/Open" Then
            Button1.Enabled = True
            Button2.Enabled = True
            Button3.Enabled = False
            Button17.Enabled = False
        ElseIf TextBox3.Text = "Solved" Then
            Button1.Enabled = False
            Button2.Enabled = False
            Button3.Enabled = True
            Button17.Enabled = False
        Else
            Button1.Enabled = False
            Button2.Enabled = False
            Button3.Enabled = True
            Button17.Enabled = False
        End If
    End Sub

    Public Sub loadhistory()
        OpenConnection()
        Dim query As String = "SELECT max(id) FROM `ticket_history` WHERE `ticketnumber`='" & ticket & "';"
        Dim cmd As New MySqlCommand(query, Connection)
        Dim read As MySqlDataReader = cmd.ExecuteReader
        ListView1.Items.Clear()
        Do While read.Read()
            Dim action As String = read.GetValue(2).ToString
            Dim desc As String = read.GetValue(3).ToString
            Dim datetimestr As String = read.GetValue(4).ToString
            Dim modifyby As String = read.GetValue(5).ToString
            Dim newstat As String = read.GetValue(6).ToString

            Dim li As ListViewItem = ListView1.Items.Add(action)
            li.SubItems.Add(desc)
            li.SubItems.Add(datetimestr)
            li.SubItems.Add(modifyby)
            li.SubItems.Add(newstat)
        Loop
        read.Close()
        Connection.Close()
    End Sub

    Private Sub viewticket_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loaddata()
        loadhistory()

        If currentlevel = "IT STAFF" Then
            Button2.Enabled = False
        Else

        End If
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        If currentlevel = "EMPLOYEE" Then
            MessageBox.Show("Only Administrator and IT personel can use this function.", "User Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            assign.ShowDialog()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        solve.ShowDialog()

      
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If currentlevel = "EMPLOYEE" Then
            MessageBox.Show("Only Administrator and IT personel can use this function.", "User Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            newupdate.ShowDialog()
        End If
    End Sub

    Private Sub Button17_Leave(sender As Object, e As EventArgs) Handles Button17.Leave
        If currentlevel = "EMPLOYEE" Then
            viewcreatedemployeeticket()
        Else
            viewalltickets()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        feedback.ShowDialog()
    End Sub
End Class
