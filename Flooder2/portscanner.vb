Public Class portscannerobj
    Public Sub portscan(ByVal host As String, ByVal lowport As Integer, ByVal highport As Integer, ByVal method As String)

        If method = "TCP" Then
            For index As Integer = lowport To highport
                If tcpportscanner(host, index) Then
                    ListBox1.Items.Add("Port: " & index & " is open!")
                End If
            Next
        ElseIf method = "UDP" Then
            For index As Integer = lowport To highport
                If udpportscanner(host, index) = True Then
                    ListBox1.Items.Add("Port: " & index & " is open!")
                End If
            Next
        End If

    End Sub
    Public Function tcpportscanner(ByVal host As String, ByVal port As Integer) As Boolean
        Try
            Dim hostip As Net.IPAddress = Net.IPAddress.Parse(Net.Dns.GetHostAddresses(host)(0).ToString())
            Dim EPhost As New System.Net.IPEndPoint(hostip, port)
            Dim tcp As New Net.Sockets.Socket(Net.Sockets.AddressFamily.InterNetwork, Net.Sockets.SocketType.Stream, Net.Sockets.ProtocolType.Tcp)
            Try
                tcp.Connect(EPhost)
            Catch
            End Try
            If Not tcp.Connected Then
                Return False
            Else
                Return True
            End If
        Catch : End Try
    End Function
    Public Function udpportscanner(ByVal host As String, ByVal port As Integer) As Boolean
        Try
            Dim hostadd As System.Net.IPAddress = System.Net.Dns.GetHostEntry(host).AddressList(0)
            Dim EPhost As New System.Net.IPEndPoint(hostadd, port)
            Dim udp As New Net.Sockets.Socket(Net.Sockets.AddressFamily.InterNetwork, Net.Sockets.SocketType.Stream, Net.Sockets.ProtocolType.Udp)
            Try
                udp.Connect(EPhost)
            Catch
            End Try
            If Not udp.Connected Then
                Return False
            Else
                Return True
            End If
        Catch : End Try
    End Function
End Class