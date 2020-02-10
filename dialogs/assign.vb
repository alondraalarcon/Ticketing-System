Imports System.Windows.Forms
Imports System.Text.RegularExpressions
Imports MySql.Data.MySqlClient
Imports System.Drawing
Imports System.IO
Public Class assign

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If ComboBox1.Text = String.Empty Then
        Else
            OpenConnection()
            Command = New MySqlCommand("INSERT INTO `ticket_history` (`id`, `ticketnumber`, `title`, `description`, `datetime`, `username`, `status`) VALUES (NULL, @ticketno, @title, @description, @createdt, @createdby, @status)", Connection)
            Command.Parameters.AddWithValue("@ticketno", viewticket.TextBox1.Text)
            Command.Parameters.AddWithValue("@title", "Pending/Open")
            Command.Parameters.AddWithValue("@description", "Ticket is now open and assigned to " + ComboBox1.Text)
            Command.Parameters.AddWithValue("@createdt", DateTime.Now)
            Command.Parameters.AddWithValue("@createdby", currentusername)
            Command.Parameters.AddWithValue("@status", "Pending/Open")
            Command.ExecuteNonQuery()
            Connection.Close()

            OpenConnection()
            Command = New MySqlCommand("UPDATE `tickets` SET `status` = @status, `assignedIT` = @it WHERE `tickets`.`ticketnumber` = @ticket;", Connection)
            Command.Parameters.AddWithValue("@ticket", viewticket.TextBox1.Text)
            Command.Parameters.AddWithValue("@status", "Pending/Open")
            Command.Parameters.AddWithValue("@it", ComboBox1.Text)
            Command.ExecuteNonQuery()
            Connection.Close()
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
    End Sub

    Private Sub assign_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        OpenConnection()
        Dim query As String = "SELECT * FROM `tbl_user` WHERE `level`<>'EMPLOYEE';"
        Dim cmd As New MySqlCommand(query, Connection)
        Dim read As MySqlDataReader = cmd.ExecuteReader
        ComboBox1.Items.Clear()
        Do While read.Read()
            Dim action As String = read.GetValue(0).ToString
            ComboBox1.Items.Add(action)
        Loop
        read.Close()
        Connection.Close()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub
End Class
