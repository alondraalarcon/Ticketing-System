Imports System.Windows.Forms
Imports MySql.Data.MySqlClient
Imports System.Drawing
Imports System.IO

Public Class activities

    Private Sub profile_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        OpenConnection()
        Dim desc As String = "View User Activity: " + Form1.ListView1.FocusedItem.SubItems(0).Text
        Command = New MySqlCommand("INSERT INTO `trail` (`TRAILNUMBER`, `USERNAME`, `DESCIRPTION`, `DATE`) VALUES (NULL, @username, @desc, @date)", Connection)
        Command.Parameters.AddWithValue("@username", currentusername)
        Command.Parameters.AddWithValue("@desc", desc)
        Command.Parameters.AddWithValue("@date", DateTime.Now)
        Command.ExecuteNonQuery()
        Connection.Close()

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
        read.Close()
        Connection.Close()

        OpenConnection()
        Dim cmd1 As New MySqlCommand("SELECT * FROM `trail` WHERE `USERNAME`='" & TextBox1.Text & "' ORDER BY `TRAILNUMBER` DESC;", Connection)
        Dim read1 As MySqlDataReader = cmd1.ExecuteReader
        ListView1.Items.Clear()
        Do While read1.Read()
            Dim a As String = read1.GetValue(0).ToString
            Dim b As String = read1.GetValue(2).ToString
            Dim c As String = read1.GetValue(3).ToString

            Dim li As ListViewItem = ListView1.Items.Add(a)
            li.SubItems.Add(b)
            li.SubItems.Add(c)

        Loop
        read1.Close()
        Connection.Close()
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class
