Imports System.Windows.Forms
Imports System.Text.RegularExpressions
Imports MySql.Data.MySqlClient
Imports System.Drawing
Imports System.IO
Public Class addticket

    Dim status As String = "None"

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click


        If currentlevel = "EMPLOYEE" Then

            Dim Ticketnumber As String = "FT" + DateTime.Now.ToString("yyMMddHHmmssfff")
            If ComboBox2.SelectedIndex = 0 Or TextBox2.Text = String.Empty Or room.SelectedIndex = 0 Or TextBox3.Text = String.Empty Or TextBox4.Text = String.Empty Or ComboBox1.Text = String.Empty Or TextBox5.Text = String.Empty Or TextBox7.Text = String.Empty Then
                MessageBox.Show("All fields are required for us to proceed.", "User Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else


                Dim ms, ms2 As New MemoryStream
                PictureBox1.Image.Save(ms, PictureBox1.Image.RawFormat)
                PictureBox2.Image.Save(ms2, PictureBox2.Image.RawFormat)
                OpenConnection()
                Dim queryupdate As String = "INSERT INTO `tickets` (`ticketnumber`, `employeeid`, `contactnumber`, `name`, `position`, `title`, `description`, `datetime`, `attachment1`, `attachment2`, `madeby`, `status`, `priority`, `assignedIT`, `month`, `day`, `year`, `dateonly`,`room`) VALUES (@ticketnumber, @email, @contactnumber, @name, @position, @title, @description, @datetime, @attachment1, @attachment2, @madeby, @status, @prio, @it, @m, @d, @y, @dt,@room);"
                Command = New MySqlCommand(queryupdate, Connection)
                Command.Parameters.AddWithValue("@ticketnumber", Ticketnumber)
                Command.Parameters.AddWithValue("@email", TextBox3.Text)
                Command.Parameters.AddWithValue("@contactnumber", TextBox4.Text)
                Command.Parameters.AddWithValue("@name", TextBox5.Text)
                Command.Parameters.AddWithValue("@position", TextBox7.Text)
                Command.Parameters.AddWithValue("@title", ComboBox2.Text)
                Command.Parameters.AddWithValue("@description", TextBox2.Text)
                Command.Parameters.AddWithValue("@datetime", DateTime.Now)
                Command.Parameters.AddWithValue("@attachment1", ms.ToArray)
                Command.Parameters.AddWithValue("@attachment2", ms2.ToArray)
                Command.Parameters.AddWithValue("@madeby", currentusername)
                Command.Parameters.AddWithValue("@prio", ComboBox1.Text)
                Command.Parameters.AddWithValue("@status", "New")
                Command.Parameters.AddWithValue("@it", "Not Yet Assigned")
                Command.Parameters.AddWithValue("@m", Date.Now.Month)
                Command.Parameters.AddWithValue("@d", Date.Now.Day)
                Command.Parameters.AddWithValue("@y", Date.Now.Year)
                Command.Parameters.AddWithValue("@dt", Date.Now)
                Command.Parameters.AddWithValue("@room", room.Text)
                Command.Parameters.AddWithValue("@department", "CCS")

                Command.ExecuteNonQuery()
                Connection.Close()

                OpenConnection()
                Command = New MySqlCommand("INSERT INTO `ticket_history` (`id`, `ticketnumber`, `title`, `description`, `datetime`, `username`, `status`) VALUES (NULL, @ticketno, @title, @description, @createdt, @createdby, @status)", Connection)
                Command.Parameters.AddWithValue("@ticketno", Ticketnumber)
                Command.Parameters.AddWithValue("@title", "Newly Created")
                Command.Parameters.AddWithValue("@description", TextBox2.Text)
                Command.Parameters.AddWithValue("@createdt", DateTime.Now)
                Command.Parameters.AddWithValue("@createdby", currentusername)
                Command.Parameters.AddWithValue("@status", "New")
                Command.ExecuteNonQuery()
                Connection.Close()

                OpenConnection()
                Dim desc As String = "Created a new ticket: " + Ticketnumber
                Command = New MySqlCommand("INSERT INTO `trail` (`TRAILNUMBER`, `USERNAME`, `DESCIRPTION`, `DATE`) VALUES (NULL, @username, @desc, @date)", Connection)
                Command.Parameters.AddWithValue("@username", currentusername)
                Command.Parameters.AddWithValue("@desc", desc)
                Command.Parameters.AddWithValue("@date", DateTime.Now)
                Command.ExecuteNonQuery()
                Connection.Close()
                ComboBox2.SelectedIndex = 0
                TextBox2.Text = String.Empty
                TextBox3.Text = String.Empty
                TextBox4.Text = String.Empty
                TextBox5.Text = String.Empty
                TextBox7.Text = String.Empty
                ComboBox1.SelectedIndex = 0
                room.SelectedIndex = 0
                PictureBox1.Image = My.Resources.attachment
                PictureBox2.Image = My.Resources.attachment

                MessageBox.Show("Ticket successfully added/created.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
                Me.Close()
                viewcreatedemployeeticket()

            End If


        Else

            Dim Ticketnumber As String = "FT" + DateTime.Now.ToString("yyMMddHHmmssfff")
            If ComboBox2.SelectedIndex = 0 Or TextBox2.Text = String.Empty Or room.SelectedIndex = 0 Or TextBox3.Text = String.Empty Or TextBox4.Text = String.Empty Or ComboBox1.Text = String.Empty Or TextBox5.Text = String.Empty Or TextBox7.Text = String.Empty Then
                MessageBox.Show("All fields are required for us to proceed.", "User Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else


                Dim ms, ms2 As New MemoryStream
                PictureBox1.Image.Save(ms, PictureBox1.Image.RawFormat)
                PictureBox2.Image.Save(ms2, PictureBox2.Image.RawFormat)
                OpenConnection()
                Dim queryupdate As String = "INSERT INTO `tickets` (`ticketnumber`, `employeeid`, `contactnumber`, `name`, `position`, `title`, `description`, `datetime`, `attachment1`, `attachment2`, `madeby`, `status`, `priority`, `assignedIT`, `month`, `day`, `year`, `dateonly`,`room`) VALUES (@ticketnumber, @email, @contactnumber, @name, @position, @title, @description, @datetime, @attachment1, @attachment2, @madeby, @status, @prio, @it, @m, @d, @y, @dt,@room);"
                Command = New MySqlCommand(queryupdate, Connection)
                Command.Parameters.AddWithValue("@ticketnumber", Ticketnumber)
                Command.Parameters.AddWithValue("@email", TextBox3.Text)
                Command.Parameters.AddWithValue("@contactnumber", TextBox4.Text)
                Command.Parameters.AddWithValue("@name", TextBox5.Text)
                Command.Parameters.AddWithValue("@position", TextBox7.Text)
                Command.Parameters.AddWithValue("@title", ComboBox2.Text)
                Command.Parameters.AddWithValue("@description", TextBox2.Text)
                Command.Parameters.AddWithValue("@datetime", DateTime.Now)
                Command.Parameters.AddWithValue("@attachment1", ms.ToArray)
                Command.Parameters.AddWithValue("@attachment2", ms2.ToArray)
                Command.Parameters.AddWithValue("@madeby", currentusername)
                Command.Parameters.AddWithValue("@prio", ComboBox1.Text)
                Command.Parameters.AddWithValue("@status", "New")
                Command.Parameters.AddWithValue("@it", "Not Yet Assigned")
                Command.Parameters.AddWithValue("@m", Date.Now.Month)
                Command.Parameters.AddWithValue("@d", Date.Now.Day)
                Command.Parameters.AddWithValue("@y", Date.Now.Year)
                Command.Parameters.AddWithValue("@dt", Date.Now)
                Command.Parameters.AddWithValue("@room", room.Text)
                Command.Parameters.AddWithValue("@statuss", status)
                Command.Parameters.AddWithValue("@department", "CCS")

                Command.ExecuteNonQuery()
                Connection.Close()

                OpenConnection()
                Command = New MySqlCommand("INSERT INTO `ticket_history` (`id`, `ticketnumber`, `title`, `description`, `datetime`, `username`, `status`) VALUES (NULL, @ticketno, @title, @description, @createdt, @createdby, @status)", Connection)
                Command.Parameters.AddWithValue("@ticketno", Ticketnumber)
                Command.Parameters.AddWithValue("@title", "Newly Created")
                Command.Parameters.AddWithValue("@description", TextBox2.Text)
                Command.Parameters.AddWithValue("@createdt", DateTime.Now)
                Command.Parameters.AddWithValue("@createdby", currentusername)
                Command.Parameters.AddWithValue("@status", "New")
                Command.ExecuteNonQuery()
                Connection.Close()

                OpenConnection()
                Dim desc As String = "Created a new ticket: " + Ticketnumber
                Command = New MySqlCommand("INSERT INTO `trail` (`TRAILNUMBER`, `USERNAME`, `DESCIRPTION`, `DATE`) VALUES (NULL, @username, @desc, @date)", Connection)
                Command.Parameters.AddWithValue("@username", currentusername)
                Command.Parameters.AddWithValue("@desc", desc)
                Command.Parameters.AddWithValue("@date", DateTime.Now)
                Command.ExecuteNonQuery()
                Connection.Close()
                ComboBox2.SelectedIndex = 0
                TextBox2.Text = String.Empty
                TextBox3.Text = String.Empty
                TextBox4.Text = String.Empty
                TextBox5.Text = String.Empty
                TextBox7.Text = String.Empty
                ComboBox1.SelectedIndex = 0
                room.SelectedIndex = 0
                PictureBox1.Image = My.Resources.attachment
                PictureBox2.Image = My.Resources.attachment
                MessageBox.Show("Ticket successfully added/created.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
                Me.Close()
                viewalltickets()


            End If




        End If


    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        If MessageBox.Show("Do you really want to cancel?", "Cancel Ticket", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            ComboBox2.SelectedIndex = 0
            TextBox2.Text = String.Empty
            TextBox3.Text = String.Empty
            TextBox4.Text = String.Empty
            TextBox5.Text = String.Empty
            TextBox7.Text = String.Empty
            PictureBox1.Image = My.Resources.attachment
            PictureBox2.Image = My.Resources.attachment
            ComboBox1.SelectedIndex = 0

            Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.Close()
        Else
            Return
        End If
    End Sub

    Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox4.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        With OpenFileDialog1
            .Filter = ("Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png")
            .FilterIndex = 4
        End With
        OpenFileDialog1.ShowDialog()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        With OpenFileDialog2
            .Filter = ("Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png")
            .FilterIndex = 4
        End With
        OpenFileDialog2.ShowDialog()
    End Sub

    Private Sub OpenFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        PictureBox1.ImageLocation = OpenFileDialog1.FileName
        PictureBox1.Image = Image.FromFile(OpenFileDialog1.FileName)
    End Sub

    Private Sub OpenFileDialog2_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog2.FileOk
        PictureBox2.ImageLocation = OpenFileDialog2.FileName
        PictureBox2.Image = Image.FromFile(OpenFileDialog2.FileName)
    End Sub

    Private Sub addticket_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub GroupBox2_Enter(sender As Object, e As EventArgs) Handles GroupBox2.Enter

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub Label15_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs)
        status = "Not Resolved"
    End Sub

    Private Sub RadioButton6_CheckedChanged(sender As Object, e As EventArgs)
        status = "Solved"
    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs)
        status = "Pending"
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class
