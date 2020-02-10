Imports System.Windows.Forms
Imports MySql.Data.MySqlClient
Imports System.Drawing
Imports System.IO

Public Class edituser
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If TextBox1.Text = String.Empty Or TextBox4.Text = String.Empty Or TextBox8.Text = String.Empty Or TextBox5.Text = String.Empty Or TextBox6.Text = String.Empty Or TextBox7.Text = String.Empty Or ComboBox1.Text = String.Empty Then
            MessageBox.Show("All fields are required for us to proceed.", "User Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else

            OpenConnection()
            Dim queryupdate As String = "UPDATE `tbl_user` SET `fname` = '" & TextBox4.Text & "', `mname` = '" & TextBox5.Text & "', `lname` = '" & TextBox6.Text & "', `level` = '" & ComboBox1.Text & "', `position` = '" & TextBox7.Text & "', `department` = '" & TextBox8.Text & "' WHERE `tbl_user`.`username` = '" & TextBox1.Text & "';"
            Command = New MySqlCommand(queryupdate, Connection)
            Command.ExecuteNonQuery()
            Connection.Close()

            OpenConnection()
            Dim desc As String = "Edited the details of User: " + TextBox1.Text
            Command = New MySqlCommand("INSERT INTO `trail` (`TRAILNUMBER`, `USERNAME`, `DESCIRPTION`, `DATE`) VALUES (NULL, @username, @desc, @date)", Connection)
            Command.Parameters.AddWithValue("@username", currentusername)
            Command.Parameters.AddWithValue("@desc", desc)
            Command.Parameters.AddWithValue("@date", DateTime.Now)
            Command.ExecuteNonQuery()
            Connection.Close()

            MessageBox.Show("Data successfully updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            viewalluser()

            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End If
    End Sub

    Private Sub profile_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim arrimage() As Byte
        OpenConnection()
        Dim query As String = "SELECT * FROM tbl_user WHERE username = '" & Form1.ListView1.FocusedItem.SubItems(0).Text & "';"
        Dim cmd As New MySqlCommand(query, Connection)
        Dim read As MySqlDataReader = cmd.ExecuteReader
        read.Read()
        arrimage = read.GetValue(9)
        Dim ms As New System.IO.MemoryStream(arrimage)
        Me.PictureBox1.Image = System.Drawing.Image.FromStream(ms)
        TextBox1.Text = read.GetValue(0).ToString
        ComboBox1.Text = read.GetValue(6).ToString
        TextBox4.Text = read.GetValue(2).ToString
        TextBox5.Text = read.GetValue(3).ToString
        TextBox6.Text = read.GetValue(4).ToString
        TextBox7.Text = read.GetValue(7).ToString
        TextBox8.Text = read.GetValue(8).ToString
        read.Close()
        Connection.Close()
    End Sub
End Class
