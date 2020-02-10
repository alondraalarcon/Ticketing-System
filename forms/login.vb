Imports MySql.Data.MySqlClient
Public Class login


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If MessageBox.Show("Do you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Application.Exit()
        Else
            Return
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox1.Text = String.Empty Or TextBox2.Text = String.Empty Then
            MessageBox.Show("All fields are required for us to proceed.", "User Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox1.Focus()
        Else
            OpenConnection()
            Dim query As String = "SELECT * FROM tbl_user WHERE employee_id = '" & TextBox1.Text & "';"
            Dim cmd As New MySqlCommand(query, Connection)
            Dim read As MySqlDataReader = cmd.ExecuteReader
            If read.HasRows Then
                Do While read.Read
                    currentusername = read.GetValue(0).ToString
                    currentpassword = read.GetValue(15).ToString
                    currentfname = read.GetValue(2).ToString
                    currentmname = read.GetValue(3).ToString
                    currentlname = read.GetValue(4).ToString
                    currentsystemaccess = read.GetValue(5).ToString
                    currentlevel = read.GetValue(6).ToString
                Loop
                read.Close()
                Connection.Close()
                If currentpassword = TextBox2.Text Then
                    If currentsystemaccess = "enabled" Then

                        OpenConnection()
                        Dim desc As String = "System Login"
                        Command = New MySqlCommand("INSERT INTO `trail` (`TRAILNUMBER`, `USERNAME`, `DESCIRPTION`, `DATE`) VALUES (NULL, @username, @desc, @date)", Connection)
                        Command.Parameters.AddWithValue("@username", currentusername)
                        Command.Parameters.AddWithValue("@desc", desc)
                        Command.Parameters.AddWithValue("@date", DateTime.Now)
                        Command.ExecuteNonQuery()
                        Connection.Close()

                        MessageBox.Show("Welcome " + currentfname + " " + currentmname + " " + currentlname, "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        TextBox1.Text = String.Empty
                        TextBox2.Text = String.Empty
                        Form1.Show()
                        Me.Close()
                        TextBox1.Focus()
                    Else
                        TextBox1.Text = String.Empty
                        TextBox2.Text = String.Empty
                        TextBox1.Focus()
                        MessageBox.Show("Sorry you are not allowed too access the system. Please contact your administrator", "User Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If
                Else
                    TextBox1.Text = String.Empty
                    TextBox2.Text = String.Empty
                    TextBox1.Focus()
                    MessageBox.Show("Password is incorrect.", "User Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            Else
                read.Close()
                Connection.Close()
                TextBox1.Text = String.Empty
                TextBox2.Text = String.Empty
                TextBox1.Focus()
                MessageBox.Show("Username not found.", "User Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim myValue As String = InputBox("Please enter your username", "Password Reset", "Type your username here")
        OpenConnection()
        Dim query As String = "SELECT * FROM tbl_user WHERE username ='" & myValue & "';"
        Dim cmd As New MySqlCommand(query, Connection)
        Dim read As MySqlDataReader = cmd.ExecuteReader
        If read.HasRows Then

            Do While read.Read()
                PasswordReset.Label1.Text = read.GetValue(10).ToString
                PasswordReset.a1 = read.GetValue(12).ToString
                PasswordReset.Label2.Text = read.GetValue(11).ToString
                PasswordReset.a2 = read.GetValue(13).ToString
                PasswordReset.usertext = myValue
            Loop
            read.Close()
            Connection.Close()
            PasswordReset.ShowDialog()
        Else
            MessageBox.Show("Username not found.", "User Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            read.Close()
            Connection.Close()
        End If
        
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        Dialog1.Show()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub login_Load(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs)
        Dialog1.Show()
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub
End Class