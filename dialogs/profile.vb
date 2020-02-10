Imports System.Windows.Forms
Imports MySql.Data.MySqlClient
Imports System.Drawing
Imports System.IO

Public Class profile

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub profile_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        OpenConnection()
        Dim desc As String = "View own profile"
        Command = New MySqlCommand("INSERT INTO `trail` (`TRAILNUMBER`, `USERNAME`, `DESCIRPTION`, `DATE`) VALUES (NULL, @username, @desc, @date)", Connection)
        Command.Parameters.AddWithValue("@username", currentusername)
        Command.Parameters.AddWithValue("@desc", desc)
        Command.Parameters.AddWithValue("@date", DateTime.Now)
        Command.ExecuteNonQuery()
        Connection.Close()

        Dim arrimage() As Byte
        OpenConnection()
        Dim query As String = "SELECT * FROM tbl_user WHERE username = '" & currentusername & "';"
        Dim cmd As New MySqlCommand(query, Connection)
        Dim read As MySqlDataReader = cmd.ExecuteReader
        read.Read()
        arrimage = read.GetValue(9)
        Dim ms As New System.IO.MemoryStream(arrimage)
        Me.PictureBox1.Image = System.Drawing.Image.FromStream(ms)
        TextBox1.Text = read.GetValue(0).ToString
        TextBox2.Text = read.GetValue(6).ToString
        TextBox4.Text = read.GetValue(2).ToString
        TextBox5.Text = read.GetValue(3).ToString
        TextBox6.Text = read.GetValue(4).ToString
        TextBox7.Text = read.GetValue(7).ToString
        TextBox8.Text = read.GetValue(8).ToString
        read.Close()
        Connection.Close()
    End Sub
End Class
