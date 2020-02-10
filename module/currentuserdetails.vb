Module currentuserdetails
    'for current user
    Public currentusername As String
    Public currentpassword As String
    Public currentfname As String
    Public currentmname As String
    Public currentlname As String
    Public currentsystemaccess As String
    Public currentlevel As String

    'for all primarykey
    Public primarykey, ticket As String

    Public Sub setprimarykey()
        Dim getyear = Date.Now.Year
        Dim getmonth = Date.Now.Month
        Dim getday = Date.Now.Day
        Dim gethour = DateTime.Now.Hour
        Dim getminute = DateTime.Now.Minute
        Dim getsec = DateTime.Now.Second
        Dim getmilisec = DateTime.Now.Millisecond
        primarykey = getyear.ToString + getmonth.ToString + getday.ToString + gethour.ToString + getminute.ToString + getsec.ToString + getmilisec.ToString
    End Sub
End Module
