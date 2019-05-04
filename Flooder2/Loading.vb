Imports System.IO
Imports System.Text

Public Class Loading
    Private width As Integer = 0
    Dim strPath As String = Application.StartupPath()
    Private Sub Loading_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()
        Panel2.Width = width
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Panel2.Width = width
        width += 8
        If width = 16 Then
            proccessstart()
        End If
        If width >= 837 Then
            torchfile()
            Timer1.Stop()
            Mainmenu.Show()
            Me.Close()
        End If
    End Sub
    Public Sub proccessstart()
        Label1.Text = "Starting tor..."
        Dim Process As New Process
        Process.StartInfo.FileName = strPath & "\Tor\tor.exe"
        Process.StartInfo.Arguments = "-f " & strPath & "\Tor\Data\Tor\torrc"
        Process.StartInfo.UseShellExecute = False
        Process.StartInfo.RedirectStandardOutput = True
        Process.StartInfo.CreateNoWindow = True
        Process.StartInfo.WorkingDirectory = strPath & "\Tor"
        Process.Start()
        Process.PriorityClass = ProcessPriorityClass.BelowNormal
    End Sub
    Private Sub torchfile()

        If Not File.Exists(strPath & "\Tor\Data\Tor\torrc") Then
            Label1.Text = "create torch..."
            Dim filestring As String = "
ControlPort 9151
DataDirectory " & strPath & "\Tor
DirPort 9030
ExitPolicy reject *:*
HashedControlPassword 16:4E1F1599005EB8F3603C046EF402B00B6F74C008765172A774D2853FD4
HiddenServiceDir " & strPath & "
HiddenServicePort 80 127.0.0.1:5558
Log notice stdout

SocksPort 9150"
            Dim fs As FileStream = File.Create(strPath & "\Tor\Data\Tor\torrc")
            Dim info As Byte() = New UTF8Encoding(True).GetBytes(filestring)
            fs.Write(info, 0, info.Length)
            fs.Close()
        Else
            Label1.Text = "torch alive"
        End If
    End Sub
End Class