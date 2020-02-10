Imports System.Windows.Forms
Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Drawing
Imports Microsoft.Reporting.WinForms

Public Class Form1

    Private Sub Listview2_ColumnClick(sender As System.Object, e As System.Windows.Forms.ColumnClickEventArgs) Handles ListView2.ColumnClick
        If ListView2.Sorting = System.Windows.Forms.SortOrder.Descending Then
            ListView2.Sorting = System.Windows.Forms.SortOrder.Ascending
        Else
            ListView2.Sorting = System.Windows.Forms.SortOrder.Descending
        End If
    End Sub


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        viewsrfcreated()


        If currentlevel = "ADMINISTRATOR" Then
            Button2.Visible = True
        Else
            Button2.Visible = False
            Button10.Location = New Point(5, 252)
        End If

        If currentlevel = "IT STAFF" Then
            Button1.Visible = True
        Else
            Button1.Visible = False
        End If



        If currentlevel = "EMPLOYEE" Then
            Button3.Visible = False
            Button5.Visible = False

            viewticket.Button17.Enabled = False
            viewticket.Button1.Enabled = False
            viewticket.Button2.Enabled = False
            Button10.Location = New Point(5, 165)

        Else
            Button3.Visible = True
            Button5.Visible = True

            viewticket.Button17.Enabled = True
            viewticket.Button1.Enabled = True
            viewticket.Button2.Enabled = True
        End If



        Try

            With TabControl1
                .Appearance = TabAppearance.Buttons
                .SizeMode = TabSizeMode.Fixed
                .ItemSize = New System.Drawing.Size(0, 1)
            End With
            updatedphoto()
        Catch ex As Exception
            MessageBox.Show(ErrorToString(), "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Me.ReportViewer1.RefreshReport()

        Try
            Dim qry As String = "select * from tbl_user"
            Dim da As New MySqlDataAdapter(qry, Connection)
            Dim dt As New DataTable("tbl_user")
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                GenerateReport.ComboBox4.DataSource = dt
                GenerateReport.ComboBox4.DisplayMember = "username"
                GenerateReport.ComboBox4.ValueMember = ""
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        profile.ShowDialog()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        TabControl1.Visible = True
        TabControl1.SelectedTab = TabPage1
        viewalluser()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dialog1.ShowDialog()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        If MessageBox.Show("Do you want to Log Out?", "Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            login.Show()
            OpenConnection()
            Dim desc As String = "System Log Out"
            Command = New MySqlCommand("INSERT INTO `trail` (`TRAILNUMBER`, `USERNAME`, `DESCIRPTION`, `DATE`) VALUES (NULL, @username, @desc, @date)", Connection)
            Command.Parameters.AddWithValue("@username", currentusername)
            Command.Parameters.AddWithValue("@desc", desc)
            Command.Parameters.AddWithValue("@date", DateTime.Now)
            Command.ExecuteNonQuery()
            Connection.Close()
            Me.Close()
        Else
            Return
        End If
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs)
        updatephoto.ShowDialog()
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs)
        changepassword.ShowDialog()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        If ListView1.SelectedItems.Count <= 0 Then
            MessageBox.Show("Please select one on the list below.", "User Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            If ListView1.FocusedItem.SubItems(0).Text = currentusername Then
                MessageBox.Show("You're not allowed to modify your own data.", "User Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                Dim currentstatus As String
                Dim newstatus As String
                OpenConnection()
                Dim query As String = "SELECT * FROM tbl_user WHERE username = '" & ListView1.FocusedItem.SubItems(0).Text & "';"
                Dim cmd As New MySqlCommand(query, Connection)
                Dim read As MySqlDataReader = cmd.ExecuteReader
                read.Read()
                currentstatus = read.GetValue(5).ToString
                read.Close()
                Connection.Close()
                If currentstatus = "enabled" Then
                    newstatus = "disabled"
                    If MessageBox.Show("Do you want to disable user: " + ListView1.FocusedItem.SubItems(0).Text, "Disable User", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        OpenConnection()
                        Dim queryupdate As String = "UPDATE `tbl_user` SET `sysaccess` = '" & newstatus & "' WHERE `tbl_user`.`username` = '" & ListView1.FocusedItem.SubItems(0).Text & "';"
                        Command = New MySqlCommand(queryupdate, Connection)
                        Command.ExecuteNonQuery()
                        Connection.Close()
                        MessageBox.Show("Successfully updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        OpenConnection()
                        Dim desc As String = "Enable User: " + ListView1.FocusedItem.SubItems(0).Text
                        Command = New MySqlCommand("INSERT INTO `trail` (`TRAILNUMBER`, `USERNAME`, `DESCIRPTION`, `DATE`) VALUES (NULL, @username, @desc, @date)", Connection)
                        Command.Parameters.AddWithValue("@username", currentusername)
                        Command.Parameters.AddWithValue("@desc", desc)
                        Command.Parameters.AddWithValue("@date", DateTime.Now)
                        Command.ExecuteNonQuery()
                        Connection.Close()
                        viewalluser()
                    Else
                        Return
                    End If
                Else
                    newstatus = "enabled"
                    If MessageBox.Show("Do you want to enable user: " + ListView1.FocusedItem.SubItems(0).Text, "Disable User", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        OpenConnection()
                        Dim queryupdate As String = "UPDATE `tbl_user` SET `sysaccess` = '" & newstatus & "' WHERE `tbl_user`.`username` = '" & ListView1.FocusedItem.SubItems(0).Text & "';"
                        Command = New MySqlCommand(queryupdate, Connection)
                        Command.ExecuteNonQuery()
                        Connection.Close()
                        MessageBox.Show("Successfully updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        OpenConnection()
                        Dim desc As String = "Disabled User: " + ListView1.FocusedItem.SubItems(0).Text
                        Command = New MySqlCommand("INSERT INTO `trail` (`TRAILNUMBER`, `USERNAME`, `DESCIRPTION`, `DATE`) VALUES (NULL, @username, @desc, @date)", Connection)
                        Command.Parameters.AddWithValue("@username", currentusername)
                        Command.Parameters.AddWithValue("@desc", desc)
                        Command.Parameters.AddWithValue("@date", DateTime.Now)
                        Command.ExecuteNonQuery()
                        Connection.Close()
                        viewalluser()
                    Else
                        Return
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If ListView1.SelectedItems.Count <= 0 Then
            MessageBox.Show("Please select one on the list below.", "User Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            viewuser.ShowDialog()
        End If
    End Sub

    Private Sub Form1_Leave(sender As Object, e As EventArgs) Handles MyBase.Leave
        OpenConnection()
        Dim desc As String = "System Exit/Log Out"
        Command = New MySqlCommand("INSERT INTO `trail` (`TRAILNUMBER`, `USERNAME`, `DESCIRPTION`, `DATE`) VALUES (NULL, @username, @desc, @date)", Connection)
        Command.Parameters.AddWithValue("@username", currentusername)
        Command.Parameters.AddWithValue("@desc", desc)
        Command.Parameters.AddWithValue("@date", DateTime.Now)
        Command.ExecuteNonQuery()
        Connection.Close()
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        If ListView1.SelectedItems.Count <= 0 Then
            MessageBox.Show("Please select one on the list below.", "User Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            activities.ShowDialog()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        searchuser()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        If ListView1.SelectedItems.Count <= 0 Then
            MessageBox.Show("Please select one on the list below.", "User Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            If ListView1.FocusedItem.SubItems(0).Text = currentusername Then
                MessageBox.Show("You're not allowed to modify your own data.", "User Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                edituser.ShowDialog()
            End If
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        TabControl1.Visible = True
        TabControl1.SelectedTab = TabPage2
        If currentlevel = "EMPLOYEE" Then
            viewcreatedemployeeticket()
        Else
            viewalltickets()
        End If
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        addticket.ShowDialog()

    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        If ListView2.SelectedItems.Count <= 0 Then
            MessageBox.Show("Please select one on the list below.", "User Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            ticket = ListView2.FocusedItem.SubItems(0).Text
            viewticket.ShowDialog()
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        TabControl1.Visible = True
        TabControl1.SelectedTab = TabPage3
        DateTimePicker3.CustomFormat = "yyyy"
        DateTimePicker3.Format = DateTimePickerFormat.Custom
        DateTimePicker4.CustomFormat = "MMMMyyyy"
        DateTimePicker4.Format = DateTimePickerFormat.Custom
        ComboBox1.SelectedIndex = 0
        Me.ReportViewer1.RefreshReport()
    End Sub

    Private Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton4.CheckedChanged
        If RadioButton4.Checked = True Then
            DateTimePicker1.Enabled = True
            DateTimePicker2.Enabled = True
        Else
            DateTimePicker1.Enabled = False
            DateTimePicker2.Enabled = False
        End If
    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        If RadioButton3.Checked = True Then
            DateTimePicker3.Enabled = True
        Else
            DateTimePicker3.Enabled = False
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked = True Then
            DateTimePicker4.Enabled = True
        Else
            DateTimePicker4.Enabled = False
        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = True Then
            DateTimePicker5.Enabled = True
        Else
            DateTimePicker5.Enabled = False
        End If
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        


        If RadioButton1.Checked Then
            Dim res1 As String = "SELECT * FROM `tickets` WHERE `dateonly`='" & DateTimePicker5.Value.ToString("yyyy-MM-dd") & "' ORDER BY `ticketnumber`;"
            If ComboBox1.SelectedIndex = 0 Then
                res1 = "SELECT * FROM `tickets` WHERE `dateonly`='" & DateTimePicker5.Value.ToString("yyyy-MM-dd") & "' ORDER BY `ticketnumber`;"
            ElseIf ComboBox1.SelectedIndex = 1 Then
                res1 = "SELECT * FROM `tickets` WHERE `dateonly`='" & DateTimePicker5.Value.ToString("yyyy-MM-dd") & "' ORDER BY `datetime`;"
            ElseIf ComboBox1.SelectedIndex = 2 Then
                res1 = "SELECT * FROM `tickets` WHERE `dateonly`='" & DateTimePicker5.Value.ToString("yyyy-MM-dd") & "' ORDER BY `madeby`;"
            ElseIf ComboBox1.SelectedIndex = 3 Then
                res1 = "SELECT * FROM `tickets` WHERE `dateonly`='" & DateTimePicker5.Value.ToString("yyyy-MM-dd") & "' ORDER BY `status`;"
            Else
                res1 = "SELECT * FROM `tickets` WHERE `dateonly`='" & DateTimePicker5.Value.ToString("yyyy-MM-dd") & "' ORDER BY `priority`;"
            End If

            Me.ReportViewer1.RefreshReport()
            ReportViewer1.Refresh()
            OpenConnection()
            Dim ad As New MySqlDataAdapter
            ad.SelectCommand = New MySqlCommand(res1, Connection)
            Dim DataSet1 As New DataTable
            ad.Fill(DataSet1)
            Me.BindingSource1.DataSource = DataSet1
            Dim bs1 As New BindingSource
            bs1.DataSource = DataSet1
            Dim rds As New ReportDataSource("DataSet1", bs1)
            Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
            Me.ReportViewer1.LocalReport.ReportPath = "..\Reports\tickets.rdl"
            Me.ReportViewer1.LocalReport.DataSources.Clear()
            Me.ReportViewer1.LocalReport.DataSources.Add(rds)
            Me.ReportViewer1.RefreshReport()
            ReportViewer1.Refresh()
            Me.ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
            ReportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent
            Me.ReportViewer1.ZoomPercent = 100
            Connection.Close()
        ElseIf RadioButton2.Checked Then
            Dim res1 As String = "SELECT * FROM `tickets` WHERE `month`='" & DateTimePicker4.Value.ToString("MM") & "' AND `year`='" & DateTimePicker4.Value.ToString("yyyy") & "' ORDER BY `ticketnumber`;"
            If ComboBox1.SelectedIndex = 0 Then
                res1 = "SELECT * FROM `tickets` WHERE `month`='" & DateTimePicker4.Value.ToString("MM") & "' AND `year`='" & DateTimePicker4.Value.ToString("yyyy") & "' ORDER BY `ticketnumber`;"
            ElseIf ComboBox1.SelectedIndex = 1 Then
                res1 = "SELECT * FROM `tickets` WHERE `month`='" & DateTimePicker4.Value.ToString("MM") & "' AND `year`='" & DateTimePicker4.Value.ToString("yyyy") & "' ORDER BY `datetime`;"
            ElseIf ComboBox1.SelectedIndex = 2 Then
                res1 = "SELECT * FROM `tickets` WHERE `month`='" & DateTimePicker4.Value.ToString("MM") & "' AND `year`='" & DateTimePicker4.Value.ToString("yyyy") & "' ORDER BY `madeby`;"
            ElseIf ComboBox1.SelectedIndex = 3 Then
                res1 = "SELECT * FROM `tickets` WHERE `month`='" & DateTimePicker4.Value.ToString("MM") & "' AND `year`='" & DateTimePicker4.Value.ToString("yyyy") & "' ORDER BY `status`;"
            Else
                res1 = "SELECT * FROM `tickets` WHERE `month`='" & DateTimePicker4.Value.ToString("MM") & "' AND `year`='" & DateTimePicker4.Value.ToString("yyyy") & "' ORDER BY `priority`;"
            End If

            Me.ReportViewer1.RefreshReport()
            ReportViewer1.Refresh()
            OpenConnection()
            Dim ad As New MySqlDataAdapter
            ad.SelectCommand = New MySqlCommand(res1, Connection)
            Dim DataSet1 As New DataTable
            ad.Fill(DataSet1)
            Me.BindingSource1.DataSource = DataSet1
            Dim bs1 As New BindingSource
            bs1.DataSource = DataSet1
            Dim rds As New ReportDataSource("DataSet1", bs1)
            Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
            Me.ReportViewer1.LocalReport.ReportPath = "..\Reports\month.rdl"
            Me.ReportViewer1.LocalReport.DataSources.Clear()
            Me.ReportViewer1.LocalReport.DataSources.Add(rds)
            Me.ReportViewer1.RefreshReport()
            ReportViewer1.Refresh()
            Me.ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
            ReportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent
            Me.ReportViewer1.ZoomPercent = 100
            Connection.Close()
        ElseIf RadioButton3.Checked Then
            Dim res1 As String
            If ComboBox1.SelectedIndex = 0 Then
                res1 = "SELECT * FROM `tickets` WHERE `year`='" & yearr.Text & "'"
            ElseIf ComboBox1.SelectedIndex = 1 Then
                res1 = "SELECT * FROM `tickets` WHERE `year`='" & yearr.Text & "' ORDER BY `datetime`;"
            ElseIf ComboBox1.SelectedIndex = 2 Then
                res1 = "SELECT * FROM `tickets` WHERE `year`='" & yearr.Text & "' ORDER BY `madeby`;"
            ElseIf ComboBox1.SelectedIndex = 3 Then
                res1 = "SELECT * FROM `tickets` WHERE `year`='" & yearr.Text & "' ORDER BY `status`;"
            Else
                res1 = "SELECT * FROM `tickets` WHERE `year`='" & yearr.Text & "' ORDER BY `priority`;"
            End If

            Me.ReportViewer1.RefreshReport()
            ReportViewer1.Refresh()
            OpenConnection()
            Dim ad As New MySqlDataAdapter
            ad.SelectCommand = New MySqlCommand(res1, Connection)
            Dim DataSet1 As New DataTable
            ad.Fill(DataSet1)
            Me.BindingSource1.DataSource = DataSet1
            Dim bs1 As New BindingSource
            bs1.DataSource = DataSet1
            Dim rds As New ReportDataSource("DataSet1", bs1)
            Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
            Me.ReportViewer1.LocalReport.ReportPath = "..\Reports\year.rdl"
            Me.ReportViewer1.LocalReport.DataSources.Clear()
            Me.ReportViewer1.LocalReport.DataSources.Add(rds)
            Me.ReportViewer1.RefreshReport()
            ReportViewer1.Refresh()
            Me.ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
            ReportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent
            Me.ReportViewer1.ZoomPercent = 100
            Connection.Close()
        ElseIf RadioButton4.Checked Then
            Dim res1 As String = "SELECT * FROM `tickets` WHERE `dateonly`>='" & DateTimePicker1.Value.ToString("yyyy-MM-dd") & "' AND `dateonly`<='" & DateTimePicker2.Value.ToString("yyyy-MM-dd") & "' ORDER BY `ticketnumber`;"
            If ComboBox1.SelectedIndex = 0 Then
                res1 = "SELECT * FROM `tickets` WHERE `dateonly`>='" & DateTimePicker1.Value.ToString("yyyy-MM-dd") & "' AND `dateonly`<='" & DateTimePicker2.Value.ToString("yyyy-MM-dd") & "' ORDER BY `ticketnumber`;"
            ElseIf ComboBox1.SelectedIndex = 1 Then
                res1 = "SELECT * FROM `tickets` WHERE `dateonly`>='" & DateTimePicker1.Value.ToString("yyyy-MM-dd") & "' AND `dateonly`<='" & DateTimePicker2.Value.ToString("yyyy-MM-dd") & "' ORDER BY `datetime`;"
            ElseIf ComboBox1.SelectedIndex = 2 Then
                res1 = "SELECT * FROM `tickets` WHERE `dateonly`>='" & DateTimePicker1.Value.ToString("yyyy-MM-dd") & "' AND `dateonly`<='" & DateTimePicker2.Value.ToString("yyyy-MM-dd") & "' ORDER BY `madeby`;"
            ElseIf ComboBox1.SelectedIndex = 3 Then
                res1 = "SELECT * FROM `tickets` WHERE `dateonly`>='" & DateTimePicker1.Value.ToString("yyyy-MM-dd") & "' AND `dateonly`<='" & DateTimePicker2.Value.ToString("yyyy-MM-dd") & "' ORDER BY `status`;"
            Else
                res1 = "SELECT * FROM `tickets` WHERE `dateonly`>='" & DateTimePicker1.Value.ToString("yyyy-MM-dd") & "' AND `dateonly`<='" & DateTimePicker2.Value.ToString("yyyy-MM-dd") & "' ORDER BY `priority`;"
            End If

            Me.ReportViewer1.RefreshReport()
            ReportViewer1.Refresh()
            OpenConnection()
            Dim ad As New MySqlDataAdapter
            ad.SelectCommand = New MySqlCommand(res1, Connection)
            Dim DataSet1 As New DataTable
            ad.Fill(DataSet1)
            Me.BindingSource1.DataSource = DataSet1
            Dim bs1 As New BindingSource
            bs1.DataSource = DataSet1
            Dim rds As New ReportDataSource("DataSet1", bs1)
            Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
            Me.ReportViewer1.LocalReport.ReportPath = "..\Reports\tickets.rdl"
            Me.ReportViewer1.LocalReport.DataSources.Clear()
            Me.ReportViewer1.LocalReport.DataSources.Add(rds)
            Me.ReportViewer1.RefreshReport()
            ReportViewer1.Refresh()
            Me.ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
            ReportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent
            Me.ReportViewer1.ZoomPercent = 100
            Connection.Close()
        End If









    End Sub

    Private Sub Button10_Click_1(sender As Object, e As EventArgs) Handles Button10.Click
        changepassword.ShowDialog()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        yearr.Text = Date.Today.Year
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        settings.ShowDialog()
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub RadioButton5_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton5.CheckedChanged
        If RadioButton5.Checked = True Then
            GenerateReport.Show()


        End If
    End Sub



    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Label5.Text = TimeOfDay.ToString("hh mm:ss:tt")
    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        Label6.Text = Date.Today.ToLongDateString
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        srf.Show()

    End Sub



    Private Sub Button2_Click_2(sender As Object, e As EventArgs) Handles Button2.Click
        TabControl1.Visible = True
        TabControl1.SelectedTab = TabPage4
    End Sub
End Class
