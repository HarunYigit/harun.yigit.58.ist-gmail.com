Public Class giriş
    Dim bakıcıtic As New Timer
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim hesaplar = My.Computer.FileSystem.ReadAllText(hesaplarstring & "hesaplar.txt")
        If kayıttekrarsifretxt.Text = kayıtsifretxt.Text Then
            IO.File.WriteAllText(hesaplarstring & "hesaplar.txt", hesaplar & vbNewLine & "-" & kayıtisimtxt.Text & ":" & kayıtsifretxt.Text & "-")
            MkDir("C:\Users\" & Environment.UserName & "\Desktop\Map data\hesap dataları\" & kayıtisimtxt.Text)
            MkDir("C:\Users\" & Environment.UserName & "\Desktop\Map data\hesap dataları\" & kayıtisimtxt.Text & "\dükkan")
            IO.File.WriteAllText("C:\Users\" & Environment.UserName & "\Desktop\Map data\hesap dataları\" & kayıtisimtxt.Text & "\dükkanvarmı.txt", "yok")
            MsgBox("Başarıyla kayıt olundu")
        Else
            MsgBox("Şifreler uyuşmuyor")
        End If
    End Sub
    Dim hesaplarstring = "C:\Users\" & Environment.UserName & "\Desktop\Map data\bölgeler\"
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        '        IO.File.WriteAllText(hesaplarstring & "hesaplar.txt", My.Computer.FileSystem.ReadAllText(hesaplarstring & "hesaplar.txt") & vbNewLine & "-" & TextBox3.Text & ":" & TextBox2.Text & "-")
        Dim hesaplar = My.Computer.FileSystem.ReadAllText(hesaplarstring & "hesaplar.txt")
        If InStr(hesaplar, "-" & girisisimtxt.Text & ":") Then
            If InStr(hesaplar, "-" & girisisimtxt.Text & ":" & girissifretxt.Text & "-") Then
                profil.isim = girisisimtxt.Text
                profil.Show()
                Me.Close()
                '   MsgBox("başarıyla giriş yaptınız")
            Else
                MsgBox("Kullanıcı bilgileriniz uyuşmuyor")
            End If
        Else
            MsgBox("Böyle bir kullanıcı kayıtlı değil")
        End If
    End Sub
    Dim sürükleme = False
    Dim mousex As Integer
    Dim mousey As Integer
    Private Sub Panel2_MouseDown(sender As Object, e As MouseEventArgs) Handles Panel2.MouseDown
        sürükleme = True
        mousex = Windows.Forms.Cursor.Position.X - Me.Left 'Sets variable mousex
        mousey = Windows.Forms.Cursor.Position.Y - Me.Top 'Sets variable mousey

    End Sub

    Private Sub Panel2_MouseMove(sender As Object, e As MouseEventArgs) Handles Panel2.MouseMove
        If sürükleme = True Then
            Me.Top = Windows.Forms.Cursor.Position.Y - mousey
            Me.Left = Windows.Forms.Cursor.Position.X - mousex
        End If
    End Sub
    Private Sub Panel2_MouseUp(sender As Object, e As MouseEventArgs) Handles Panel2.MouseUp
        sürükleme = False
    End Sub

    Private Sub giriş_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler bakıcıtic.Tick, AddressOf bakıcıtic_tick
        bakıcıtic.Start()
        bakıcıtic.Interval = 1
    End Sub
    Public Sub bakıcıtic_tick(sender As Object, e As EventArgs)
        If girisisimtxt.Text.Trim = "" Or girissifretxt.Text.Trim = "" Or girissifretxt.Text.Length < 6 Then
            Button1.Enabled = False
        Else
            Button1.Enabled = True
        End If

        If kayıtisimtxt.Text.Trim = "" Or kayıtsifretxt.Text.Trim = "" Or kayıttekrarsifretxt.Text.Trim = "" Or kayıtsifretxt.Text.Length < 6 Or kayıttekrarsifretxt.Text.Length < 6 Then
            Button2.Enabled = False
        Else
            Button2.Enabled = True
        End If

    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Form1.Show()
        Me.Close()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Me.Close()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        qualai.Show()
        Me.Close()
    End Sub

End Class