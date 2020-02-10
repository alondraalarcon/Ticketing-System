Imports System.Windows.Forms
Imports System.Text.RegularExpressions
Imports MySql.Data.MySqlClient
Imports System.Drawing
Imports System.IO

Public Class solve

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If TextBox2.Text = String.Empty Then
        Else
            OpenConnection()
            Command = New MySqlCommand("INSERT INTO `ticket_history` (`id`, `ticketnumber`, `title`, `description`, `datetime`, `username`, `status`) VALUES (NULL, @ticketno, @title, @description, @createdt, @createdby, @status)", Connection)
            Command.Parameters.AddWithValue("@ticketno", viewticket.TextBox1.Text)
            Command.Parameters.AddWithValue("@title", "Marked as solved ticket")
            Command.Parameters.AddWithValue("@description", TextBox2.Text)
            Command.Parameters.AddWithValue("@createdt", DateTime.Now)
            Command.Parameters.AddWithValue("@createdby", currentusername)
            Command.Parameters.AddWithValue("@status", "Solved")
            Command.ExecuteNonQuery()
            Connection.Close()

            OpenConnection()
            Command = New MySqlCommand("UPDATE `tickets` SET `status` = @status WHERE `tickets`.`ticketnumber` = @ticket;", Connection)
            Command.Parameters.AddWithValue("@ticket", viewticket.TextBox1.Text)
            Command.Parameters.AddWithValue("@status", "Solved")
            Command.ExecuteNonQuery()
            Connection.Close()

            TextBox2.Text = String.Empty
            viewticket.loaddata()
            viewticket.loadhistory()
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
            If currentlevel = "EMPLOYEE" Then
                viewcreatedemployeeticket()
            Else
                viewalltickets()
            End If
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
        TextBox2.Text = String.Empty
    End Sub

End Class
