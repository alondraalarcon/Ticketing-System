Imports MySql.Data.MySqlClient
Public Class GenerateReport

    Private Sub GenerateReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'ticketingDataSet.trail' table. You can move, or remove it, as needed.
        Me.ticket_historyTableAdapter.Fill(Me.ticketingDataSet1.ticket_history)

    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        Dim cmd As MySqlCommand
        Dim adp As New MySqlDataAdapter


        Dim qry As String = "SELECT * FROM ticket_history WHERE username='" & ComboBox4.Text & "'"
        Try
            Me.ticketingDataSet1.Clear()
            OpenConnection()
            cmd = New MySqlCommand(qry, Connection)
            adp.SelectCommand = cmd
            adp.Fill(Me.ticketingDataSet1.ticket_history)
            cmd.Dispose()
            adp.Dispose()
            Connection.Close()

        Catch ex As Exception

        End Try


        Me.ReportViewer1.RefreshReport()
    End Sub
End Class