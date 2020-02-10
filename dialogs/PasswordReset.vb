Imports System.Windows.Forms
Imports MySql.Data.MySqlClient
Public Class PasswordReset
    Public a1, a2, usertext As String
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If TextBox1.Text = String.Empty Or TextBox2.Text = String.Empty Or TextBox3.Text = String.Empty Or TextBox4.Text = String.Empty Then
            MessageBox.Show("All fields are required.", "User Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            If TextBox1.Text = a1 And TextBox2.Text = a2 Then
                If TextBox3.TextLength < 8 Then
                    MessageBox.Show("Password too short.", "User Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else
                    If TextBox3.Text = TextBox4.Text Then
                        OpenConnection()
                        Dim queryupdate As String = "UPDATE `tbl_user` SET `password` = '" & TextBox3.Text & "' WHERE `tbl_user`.`username` = '" & usertext & "';"
                        Command = New MySqlCommand(queryupdate, Connection)
                        Command.ExecuteNonQuery()
                        Connection.Close()

                        Me.DialogResult = System.Windows.Forms.DialogResult.OK
                        Me.Close()
                        TextBox1.Text = String.Empty
                        TextBox2.Text = String.Empty
                        TextBox3.Text = String.Empty
                        TextBox4.Text = String.Empty
                        MessageBox.Show("Reset success.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        MessageBox.Show("Password not match.", "User Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If
                End If
            Else
                MessageBox.Show("Wrong answers received.", "User Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub PasswordReset_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Text = String.Empty
        TextBox2.Text = String.Empty
        TextBox3.Text = String.Empty
        TextBox4.Text = String.Empty
    End Sub
End Class
