Imports System.Net
Imports com.LandonKey.SocksWebProxy
Imports System.Threading

Module sender
    Public Function HttpPost(ByVal client As String, ByVal msg As String)
        Try
            Dim request As HttpWebRequest
            Dim response As HttpWebResponse
            Dim config As com.LandonKey.SocksWebProxy.Proxy.ProxyConfig = New com.LandonKey.SocksWebProxy.Proxy.ProxyConfig(IPAddress.Loopback, 8181, IPAddress.Loopback, 9150, com.LandonKey.SocksWebProxy.Proxy.ProxyConfig.SocksVersion.Five)
            request = CType(WebRequest.Create(client & msg), HttpWebRequest)
            request.Proxy = New SocksWebProxy(config)
            request.KeepAlive = False
            response = CType(request.GetResponse, HttpWebResponse)
            Return response
        Catch
        End Try
    End Function
    ' Public Function sendTcp(ByVal address As String)
    'dim Client As New TcpClient("127.0.0.1", 9999)
    ' Dim config As com.LandonKey.SocksWebProxy.Proxy.ProxyConfig = New com.LandonKey.SocksWebProxy.Proxy.ProxyConfig(IPAddress.Loopback, 8181, IPAddress.Loopback, 9150, com.LandonKey.SocksWebProxy.Proxy.ProxyConfig.SocksVersion.Five)
    'End Function
End Module
