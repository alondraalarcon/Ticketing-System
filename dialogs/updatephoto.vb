Imports System.Windows.Forms
Imports MySql.Data.MySqlClient
Imports System.Drawing
Imports System.IO
Public Class updatephoto

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Try
            If Not PictureBox1.Image Is Nothing Then
                Dim ms As New MemoryStream
                PictureBox1.Image.Save(ms, PictureBox1.Image.RawFormat)
                OpenConnection()
                Dim query As String = "UPDATE `tbl_user` SET `photo` = @img WHERE `username` = '" & currentusername & "';"
                Command = New MySqlCommand(query, Connection)
                Command.Parameters.AddWithValue("@img", ms.ToArray)
                Command.ExecuteNonQuery()
                Connection.Close()
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
                PictureBox1.Image = Nothing
                updatedphoto()

                OpenConnection()
                Dim desc As String = "Updated own profile photo"
                Command = New MySqlCommand("INSERT INTO `trail` (`TRAILNUMBER`, `USERNAME`, `DESCIRPTION`, `DATE`) VALUES (NULL, @username, @desc, @date)", Connection)
                Command.Parameters.AddWithValue("@username", currentusername)
                Command.Parameters.AddWithValue("@desc", desc)
                Command.Parameters.AddWithValue("@date", DateTime.Now)
                Command.ExecuteNonQuery()
                Connection.Close()

                MessageBox.Show("Photo successfully updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)


            Else
                MessageBox.Show("Please Select image first.", "User Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show(ErrorToString, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
        PictureBox1.Image = Nothing
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        With OpenFileDialog1
            .Filter = ("Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png")
            .FilterIndex = 4
        End With
        OpenFileDialog1.ShowDialog()
    End Sub

    Private Sub OpenFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        PictureBox1.ImageLocation = OpenFileDialog1.FileName
        PictureBox1.Image = Image.FromFile(OpenFileDialog1.FileName)
    End Sub

    Private Sub updatephoto_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
