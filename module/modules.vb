Imports MySql.Data.MySqlClient

Module modules
    'add new value and variable
    'Public serveripforticketing As String
    'Public serverip As String
    'Dim ConnectionString As String = "server=" + serveripforticketing + "; port=3306; user id=ticketing; password=ticketing; database=ticketing;"
    Dim ConnectionString As String = "server=localhost;user id=root; password=; database=ticketing;"
    Public Connection As New MySqlConnection(ConnectionString)
    Public DataReader As MySqlDataReader
    Public Command As MySqlCommand


    Public Sub OpenConnection()
        If Not Connection.State = ConnectionState.Open Then
            Connection.Open()
        End If
    End Sub

    Public Sub searchuser()
        Try
            OpenConnection()
            Dim query As String = "SELECT * FROM tbl_user WHERE username LIKE '%" & Form1.TextBox1.Text & "%' OR fname LIKE '%" & Form1.TextBox1.Text & "%' OR mname LIKE '%" & Form1.TextBox1.Text & "%' OR lname LIKE '%" & Form1.TextBox1.Text & "%';"
            Dim cmd As New MySqlCommand(query, Connection)
            Dim read As MySqlDataReader = cmd.ExecuteReader
            Form1.ListView1.Items.Clear()
            Do While read.Read()
                Dim a As String = read.GetValue(0).ToString
                Dim b As String = read.GetValue(2).ToString
                Dim c As String = read.GetValue(3).ToString
                Dim d As String = read.GetValue(4).ToString
                Dim e As String = read.GetValue(5).ToString
                Dim f As String = read.GetValue(6).ToString
                Dim h As String = b + " " + c + " " + d

                Dim li As ListViewItem = Form1.ListView1.Items.Add(a)
                li.SubItems.Add(h)
                li.SubItems.Add(e)
                li.SubItems.Add(f)

            Loop
            read.Close()
            Connection.Close()
        Catch ex As Exception
            MessageBox.Show(ErrorToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub viewalluser()
        Try
            OpenConnection()
            Dim query As String = "SELECT * FROM tbl_user;"
            Dim cmd As New MySqlCommand(query, Connection)
            Dim read As MySqlDataReader = cmd.ExecuteReader
            Form1.ListView1.Items.Clear()
            Do While read.Read()
                Dim a As String = read.GetValue(0).ToString
                Dim b As String = read.GetValue(2).ToString
                Dim c As String = read.GetValue(3).ToString
                Dim d As String = read.GetValue(4).ToString
                Dim e As String = read.GetValue(5).ToString
                Dim f As String = read.GetValue(6).ToString
                Dim h As String = b + " " + c + " " + d

                Dim li As ListViewItem = Form1.ListView1.Items.Add(a)
                li.SubItems.Add(h)
                li.SubItems.Add(e)
                li.SubItems.Add(f)

            Loop
            read.Close()
            Connection.Close()
        Catch ex As Exception
            MessageBox.Show(ErrorToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub updatedphoto()
        Dim arrimage() As Byte
        OpenConnection()
        Dim query As String = "SELECT * FROM tbl_user WHERE username = '" & currentusername & "';"
        Dim cmd As New MySqlCommand(query, Connection)
        Dim read As MySqlDataReader = cmd.ExecuteReader
        read.Read()
        arrimage = read.GetValue(9)
        Dim ms As New System.IO.MemoryStream(arrimage)
        Form1.PictureBox1.Image = System.Drawing.Image.FromStream(ms)
        read.Close()
        Connection.Close()
    End Sub

    Public Sub viewalltickets()
        Try
            OpenConnection()
            Dim query As String = "SELECT * FROM tickets ORDER BY id DESC"
            Dim cmd As New MySqlCommand(query, Connection)
            Dim read As MySqlDataReader = cmd.ExecuteReader
            Form1.ListView2.Items.Clear()
            Do While read.Read()
                Dim a As String = read.GetValue(0).ToString
                Dim b As String = read.GetValue(6).ToString
                Dim c As String = read.GetValue(7).ToString
                Dim d As String = read.GetValue(8).ToString
                Dim e As String = read.GetValue(11).ToString
                Dim f As String = read.GetValue(12).ToString
                Dim g As String = read.GetValue(13).ToString
                Dim h As String = read.GetValue(14).ToString

                Dim li As ListViewItem = Form1.ListView2.Items.Add(a)
                li.SubItems.Add(b)
                li.SubItems.Add(c)
                li.SubItems.Add(d)
                li.SubItems.Add(e)
                li.SubItems.Add(f)
                li.SubItems.Add(g)
                li.SubItems.Add(h)
                If f = "New" Then
                    li.BackColor = Color.Blue
                    li.ForeColor = Color.White
                ElseIf f = "Solved" Or f = "" Then
                    li.BackColor = Color.Green
                    li.ForeColor = Color.White
                ElseIf f = "Pending/Open" Then
                    li.BackColor = Color.Orange
                    li.ForeColor = Color.Black
                ElseIf f = "Solved with feedback" Then
                    li.BackColor = Color.YellowGreen
                    li.ForeColor = Color.Black
                End If
            Loop

            read.Close()
            Connection.Close()
        Catch ex As Exception
            MessageBox.Show(ErrorToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub viewcreatedemployeeticket()
        Try
            OpenConnection()
            Dim query As String = "SELECT * FROM tickets WHERE madeby='" & currentusername & "' ORDER BY id DESC"
            Dim cmd As New MySqlCommand(query, Connection)
            Dim read As MySqlDataReader = cmd.ExecuteReader
            Form1.ListView2.Items.Clear()
            Do While read.Read()
                Dim a As String = read.GetValue(0).ToString
                Dim b As String = read.GetValue(6).ToString
                Dim c As String = read.GetValue(7).ToString
                Dim d As String = read.GetValue(8).ToString
                Dim e As String = read.GetValue(11).ToString
                Dim f As String = read.GetValue(12).ToString
                Dim g As String = read.GetValue(13).ToString
                Dim h As String = read.GetValue(14).ToString

                Dim li As ListViewItem = Form1.ListView2.Items.Add(a)
                li.SubItems.Add(b)
                li.SubItems.Add(c)
                li.SubItems.Add(d)
                li.SubItems.Add(e)
                li.SubItems.Add(f)
                li.SubItems.Add(g)
                li.SubItems.Add(h)
                If f = "New" Then
                    li.BackColor = Color.Blue
                    li.ForeColor = Color.White
                ElseIf f = "Solved" Or f = "" Then
                    li.BackColor = Color.Green
                    li.ForeColor = Color.White
                ElseIf f = "Pending/Open" Then
                    li.BackColor = Color.Orange
                    li.ForeColor = Color.Black
                ElseIf f = "Solved with feedback" Then
                    li.BackColor = Color.YellowGreen
                    li.ForeColor = Color.Black
                End If
            Loop

            read.Close()
            Connection.Close()
        Catch ex As Exception
            MessageBox.Show(ErrorToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub viewsrfcreated()
        Try
            OpenConnection()
            Dim query As String = "SELECT * FROM srf_history"
            Dim cmd As New MySqlCommand(query, Connection)
            Dim read As MySqlDataReader = cmd.ExecuteReader
            Form1.ListView3.Items.Clear()
            Do While read.Read()
                Dim e As String = read.GetValue(1).ToString
                Dim b As String = read.GetValue(2).ToString
                Dim c As String = read.GetValue(3).ToString
                Dim d As String = read.GetValue(4).ToString


                Dim li As ListViewItem = Form1.ListView3.Items.Add(e)
                li.SubItems.Add(b)
                li.SubItems.Add(c)
                li.SubItems.Add(d)
                li.SubItems.Add(d)
               
            Loop

            read.Close()
            Connection.Close()
        Catch ex As Exception
            MessageBox.Show(ErrorToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


End Module
