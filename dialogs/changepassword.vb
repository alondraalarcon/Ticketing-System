Imports System.Windows.Forms
Imports MySql.Data.MySqlClient

Public Class changepassword

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If TextBox1.Text = String.Empty Or TextBox2.Text = String.Empty Or TextBox3.Text = String.Empty Then
            MessageBox.Show("All fields are required for us to proceed.", "User Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            If TextBox1.Text = currentpassword Then
                If TextBox2.Text = TextBox3.Text Then
                    If TextBox2.TextLength < 8 Then
                        MessageBox.Show("New Password to short.", "User Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Else
                        OpenConnection()
                        Dim query As String = "UPDATE `tbl_user` SET `dob` = @password WHERE `username` = '" & currentusername & "';"
                        Command = New MySqlCommand(query, Connection)
                        Command.Parameters.AddWithValue("@password", TextBox2.Text)
                        Command.ExecuteNonQuery()
                        Connection.Close()
                        TextBox1.Text = String.Empty
                        TextBox2.Text = String.Empty
                        TextBox3.Text = String.Empty
                        MessageBox.Show("Password successfully updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        OpenConnection()
                        Dim desc As String = "Change own password"
                        Command = New MySqlCommand("INSERT INTO `trail` (`TRAILNUMBER`, `USERNAME`, `DESCIRPTION`, `DATE`) VALUES (NULL, @username, @desc, @date)", Connection)
                        Command.Parameters.AddWithValue("@username", currentusername)
                        Command.Parameters.AddWithValue("@desc", desc)
                        Command.Parameters.AddWithValue("@date", DateTime.Now)
                        Command.ExecuteNonQuery()
                        Connection.Close()
                        Me.DialogResult = System.Windows.Forms.DialogResult.OK
                        Me.Close()
                    End If
                Else
                    MessageBox.Show("New Password not match.", "User Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Else
                MessageBox.Show("Wrong current password.", "User Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
     
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        TextBox1.Text = String.Empty
        TextBox2.Text = String.Empty
        TextBox3.Text = String.Empty
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub changepassword_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Focus()
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged

    End Sub
End Class
