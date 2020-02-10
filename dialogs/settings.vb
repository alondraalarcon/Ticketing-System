Imports System.Windows.Forms
Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Drawing
Imports Microsoft.Reporting.WinForms
Public Class settings

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        updatephoto.ShowDialog()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        profile.ShowDialog()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If MessageBox.Show("Do you want to Log Out?", "Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            login.Show()
            OpenConnection()
            Dim desc As String = "System Log Out"
            Command = New MySqlCommand("INSERT INTO `trail` (`TRAILNUMBER`, `USERNAME`, `DESCIRPTION`, `DATE`) VALUES (NULL, @username, @desc, @date)", Connection)
            Command.Parameters.AddWithValue("@username", currentusername)
            Command.Parameters.AddWithValue("@desc", desc)
            Command.Parameters.AddWithValue("@date", DateTime.Now)
            Command.ExecuteNonQuery()
            Connection.Close()
            Me.Hide()
            Form1.Hide()
        Else
            Return
        End If
    End Sub
End Class