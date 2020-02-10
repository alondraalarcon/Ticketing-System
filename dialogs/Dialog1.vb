Imports System.Windows.Forms
Imports MySql.Data.MySqlClient
Imports System.Drawing
Imports System.IO

Public Class Dialog1

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If TextBox1.Text = String.Empty Or TextBox2.Text = String.Empty Or TextBox4.Text = String.Empty Or TextBox8.Text = String.Empty Or TextBox6.Text = String.Empty Or TextBox7.Text = String.Empty Then
            MessageBox.Show("All fields are required for us to proceed.", "User Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else

            If ComboBox1.Text = String.Empty Then
                MessageBox.Show("All fields are required for us to proceed.", "User Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                If TextBox2.TextLength < 8 Then
                    MessageBox.Show("Password is to short.", "User Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    OpenConnection()
                    Dim query As String = "SELECT * FROM tbl_user WHERE username = '" & TextBox1.Text & "';"
                    Dim cmd As New MySqlCommand(query, Connection)
                    Dim read As MySqlDataReader = cmd.ExecuteReader
                    If read.HasRows Then
                        MessageBox.Show("Username is already taken.", "User Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        read.Close()
                        Connection.Close()
                    Else
                        read.Close()
                        Connection.Close()
                        Dim ms As New MemoryStream
                        PictureBox1.Image.Save(ms, PictureBox1.Image.RawFormat)
                        OpenConnection()
                        Dim queryupdate As String = "INSERT INTO `tbl_user` (`username`, `password`, `fname`, `mname`, `lname`, `sysaccess`, `level`, `position`, `department`, `photo`,`employee_id`,`dob`) VALUES (@username, @password, @fname, @mname, @lname, 'enabled', @level, @position, @department, @img,@employee_id,@dob);"
                        Command = New MySqlCommand(queryupdate, Connection)
                        Command.Parameters.AddWithValue("@username", TextBox1.Text)
                        Command.Parameters.AddWithValue("@password", TextBox2.Text)
                        Command.Parameters.AddWithValue("@fname", TextBox4.Text)
                        Command.Parameters.AddWithValue("@mname", TextBox5.Text)
                        Command.Parameters.AddWithValue("@lname", TextBox6.Text)
                        Command.Parameters.AddWithValue("@level", ComboBox1.Text)
                        Command.Parameters.AddWithValue("@position", TextBox7.Text)
                        Command.Parameters.AddWithValue("@department", TextBox8.Text)
                        Command.Parameters.AddWithValue("@img", ms.ToArray)
                        Command.Parameters.AddWithValue("@employee_id", empid.Text)
                        Command.Parameters.AddWithValue("@dob", dob.Text)
                        Command.ExecuteNonQuery()
                        Connection.Close()

                        OpenConnection()
                        MessageBox.Show("User successfully added.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        viewalluser()
                        Me.DialogResult = Windows.Forms.DialogResult.OK
                        Me.Close()
                        TextBox1.Text = String.Empty
                        TextBox2.Text = String.Empty
                        TextBox4.Text = String.Empty
                        TextBox5.Text = String.Empty
                        TextBox6.Text = String.Empty
                        TextBox7.Text = String.Empty
                        TextBox8.Text = String.Empty
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        TextBox1.Text = String.Empty
        TextBox2.Text = String.Empty
        TextBox4.Text = String.Empty
        TextBox5.Text = String.Empty
        TextBox6.Text = String.Empty
        TextBox7.Text = String.Empty
        TextBox8.Text = String.Empty
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub Dialog1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub TableLayoutPanel1_Paint(sender As Object, e As PaintEventArgs) Handles TableLayoutPanel1.Paint

    End Sub
End Class
