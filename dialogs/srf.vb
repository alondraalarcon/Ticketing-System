Imports System.Windows.Forms
Imports System.Text.RegularExpressions
Imports MySql.Data.MySqlClient
Imports System.Drawing
Imports System.IO
Public Class srf


    Dim status As String = "None"
    Private Sub Cancel_Button_Click(sender As Object, e As EventArgs) Handles Cancel_Button.Click
        Me.Hide()

    End Sub

    Private Sub clearFields()
        referenceno.Text = Nothing
        contactperson.Text = Nothing
        timein.Text = Nothing
        timeout.Text = Nothing
        phone.Text = Nothing
        description.Text = Nothing
        action.Text = Nothing
        remarks.Text = Nothing
        status = "None"
        fname.Text = Nothing

    End Sub

    Private Sub RadioButton6_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton6.CheckedChanged
        status = "Solved"
    End Sub

    Private Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton4.CheckedChanged
        status = "Not Resolved"
    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        status = "Pending"
    End Sub

    Private Sub OK_Button_Click(sender As Object, e As EventArgs) Handles OK_Button.Click
        OpenConnection()
        If referenceno.Text = Nothing Or contactperson.Text = Nothing Or timein.Text = Nothing Or timeout.Text = Nothing Or phone.Text = Nothing Or description.Text = Nothing Or action.Text = Nothing Or remarks.Text = Nothing Or status = "None" Or fname.Text = Nothing Then
            MsgBox("All fields are required!")
        Else
            Dim qry As String = "INSERT INTO `tbl_srf`(`referenceno`, `date_attended`, `contact_person`, `timein`, `date_reported`, `timeout`, `phoneno`, `trobule`, `solution`, `remarks`, `status`, `assigned`) VALUES('" & referenceno.Text & "', '" & date_attended.Text & "', '" & contactperson.Text & "','" & timein.Text & "','" & date_reported.Text & "','" & timeout.Text & "','" & phone.Text & "','" & description.Text & "','" & action.Text & "','" & remarks.Text & "','" & status & "','" & fname.Text & "')"
            Command = New MySqlCommand(qry, Connection)
            Command.ExecuteNonQuery()
            Connection.Close()


            OpenConnection()
            Command = New MySqlCommand("INSERT INTO `srf_history` (`referenceno`, `description`, `created`, `createdby`, `status`) VALUES (@referenceno, @description, @created, @createdby, @status)", Connection)
            Command.Parameters.AddWithValue("@referenceno", referenceno.Text)
            Command.Parameters.AddWithValue("@description", description.Text)
            Command.Parameters.AddWithValue("@created", DateTime.Now)
            Command.Parameters.AddWithValue("@createdby", currentusername)
            Command.Parameters.AddWithValue("@status", "New")
            Command.ExecuteNonQuery()
            Connection.Close()

            clearFields()
            MessageBox.Show("Srf successfully added/created.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Hide()

        End If
    End Sub

End Class