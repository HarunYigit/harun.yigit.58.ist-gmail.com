Public Class profil
    Public isim As String
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form2.Show()
        Me.Close()
    End Sub
    Dim dükkankonum As String = ""
    Dim dükkanismi As String = ""
    Dim bölge As String = ""
    Dim panelyükletmetic As New Timer
    Public Sub ürünleryüklenme()
        Dim ürünler = My.Computer.FileSystem.ReadAllText("C:\Users\" & Environment.UserName & "\Desktop\Map data\hesap dataları\" & isim & "\dükkan\ürünler\ürünler.txt")
        For Each i In ürünler.Split(vbNewLine)
            i = i.TrimStart.TrimEnd
            Try
                Dim ürünismi = i.TrimStart.TrimEnd
                Dim ürünaçıklama = My.Computer.FileSystem.ReadAllText("C:\Users\" & Environment.UserName & "\Desktop\Map data\hesap dataları\" & isim & "\dükkan\ürünler\" & i & "\açıklama.txt")
                Dim ürünfiyat = My.Computer.FileSystem.ReadAllText("C:\Users\" & Environment.UserName & "\Desktop\Map data\hesap dataları\" & isim & "\dükkan\ürünler\" & i & "\fiyat.txt")
                Dim imageyol = "C:\Users\" & Environment.UserName & "\Desktop\Map data\hesap dataları\" & isim & "\dükkan\ürünler\" & i & "\resim.png"

                Dim a As New ürün
                a.ürünlerkonum = "C:\Users\" & Environment.UserName & "\Desktop\Map data\hesap dataları\" & isim & "\dükkan\ürünler\"
                a.yükle(ürünaçıklama, ürünfiyat, imageyol, ürünismi)
            Catch ex As Exception
                '   MsgBox("/" & i & "\" & vbNewLine & vbNewLine & ex.ToString)
            End Try
        Next
    End Sub
    Private Sub profil_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        panelyükletmetic.Interval = 750
        AddHandler panelyükletmetic.Tick, AddressOf panelyüklenme
        Dim dükkanvarmı = My.Computer.FileSystem.ReadAllText("C:\Users\" & Environment.UserName & "\Desktop\Map data\hesap dataları\" & isim & "\dükkanvarmı.txt")
        If dükkanvarmı = "var" Then
            Button1.Visible = False
            Button3.Left = 351
            Try
                dükkankonum = My.Computer.FileSystem.ReadAllText("C:\Users\" & Environment.UserName & "\Desktop\Map data\hesap dataları\" & isim & "\dükkan\konum.txt")
                Dim konum As String = My.Computer.FileSystem.ReadAllText(dükkankonum & "\konum.txt")
                PictureBox1.Location = New Point(CInt(CInt(konum.Split(",")(0) / 4)), CInt(CInt(konum.Split(",")(1)) / 2))
                dükkanismi = dükkankonum.Split("\")(dükkankonum.Split("\").Count - 1)
                bölge = dükkankonum.Replace("\" & dükkanismi, "").Split("\")(dükkankonum.Replace("\" & dükkanismi, "").Split("\").Count - 1)
                panelyükletme.ImageLocation = "C:\Users\" & Environment.UserName & "\Desktop\Map data\bölgeler\fotoğraflar\" & bölge & ".png"
                Label1.Text = "Dükkanın ismi: " & dükkanismi & vbNewLine & "Bölge: " & bölge
                ürünleryüklenme()
                panelyükletmetic.Start()
            Catch ex As Exception
                Label1.Text = "Dükkanınız bulunamadı"
            End Try
        Else
            Label1.Text = "Dükkanınız bulunmamaktadır"
            Button2.Visible = False
            Button1.Visible = True
        End If




    End Sub
    Public Sub panelyüklenme(sender As Object, e As EventArgs)
        panelyükletmetic.Stop()
        Panel1.BackgroundImage = panelyükletme.Image
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ürünekle.Show()
    End Sub
    Dim sürükleme = False
    Dim mousex As Integer
    Dim mousey As Integer

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        qualai.Show()
        Me.Close()
    End Sub




    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Me.Close()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

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
End Class
Public Class ürün
    Public ürünlerkonum As String = ""
    Public pan As New Panel
    Public isim As New Label
    Public açıklama As New Label
    Public fiyat As New Label
    Public resim As New PictureBox
    Public gösterme As New Form
    Dim silinicekisim As String = ""
    Public ürünükaldır As New Button
    Public Sub yükle(açıklamas As String, fiyats As String, resimyol As String, isims As String)
        pan.Size = New Size(456, 109)
        resim.Size = New Size(73, 75)
        silinicekisim = isims
        isim.ForeColor = Color.Yellow
        fiyat.ForeColor = Color.Green
        isim.Font = New Font("Microsoft Sans Serif", 12)
        açıklama.Font = New Font("Microsoft Sans Serif", 10)
        fiyat.Font = New Font("Microsoft Sans Serif", 14)
        açıklama.Text = açıklamas
        isim.Text = isims(0).ToString.ToUpper & Mid(isims, 2, isims.Length)
        Dim arkapic As New PictureBox
        arkapic.Size = New Size(75, 77)
        arkapic.Location = New Point(377, 3)
        arkapic.BackColor = Color.Black
        Dim bit As New Bitmap(456, 109)
        Dim graf As Graphics = Graphics.FromImage(bit)
        graf.DrawRectangle(New Pen(Color.Black, 3), 0, 0, 454, 107)
        pan.BackgroundImage = bit
        fiyat.Text = fiyats
        resim.ImageLocation = resimyol
        isim.Location = New Point(10, 12)
        açıklama.Location = New Point(10, 45)
        fiyat.Location = New Point(10, 75)
        açıklama.Width = 350
        isim.Height = 25
        isim.Width = 350
        fiyat.Width = 350
        resim.Location = New Point(378, 4)
        ürünükaldır.Top = 82
        ürünükaldır.Size = New Size(25, 23)
        ürünükaldır.Text = "X"
        ürünükaldır.BackColor = Color.OrangeRed
        ürünükaldır.ForeColor = Color.White
        ürünükaldır.Left = (pan.Width - ürünükaldır.Width) - 4
        ürünükaldır.FlatStyle = FlatStyle.Flat
        pan.Controls.Add(ürünükaldır)
        pan.Controls.Add(isim)
        pan.Controls.Add(açıklama)
        pan.Controls.Add(fiyat)
        pan.Controls.Add(resim)
        pan.Controls.Add(arkapic)
        fiyat.BringToFront()
        resim.BringToFront()
        AddHandler ürünükaldır.Click, AddressOf ürünkaldırma
        gösterme.Size = New Size(500, 500)
        resim.SizeMode = PictureBoxSizeMode.StretchImage
        AddHandler resim.Click, AddressOf resimaçma
        pan.BackColor = Color.SkyBlue
        profil.FlowLayoutPanel1.Controls.Add(pan)
    End Sub
    Public Sub ürünkaldırma(sender As Button, e As EventArgs)
        If MsgBox("Bu ürünü gerçekten kaldırmak istiyormusunuz?", MsgBoxStyle.OkCancel) = vbOK Then
            ürünkaldırsub()
        End If
    End Sub
    'Dim yol As String = "C:\Users\" & Environment.UserName & "\Desktop\Map data\hesap dataları\naber\dükkan\ürünler\"
    'Dim ürünlertxt As String = My.Computer.FileSystem.ReadAllText(yol & "ürünler.txt")
    '    IO.File.WriteAllText(yol & "ürünler.txt", ürünlertxt.TrimStart & vbNewLine & TextBox1.Text, System.Text.Encoding.UTF8)
    '    MkDir(yol & TextBox1.Text)
    '    Dim yeniyol As String = yol & TextBox1.Text & "\"
    '    IO.File.WriteAllText(yeniyol & "açıklama.txt", RichTextBox1.Text, System.Text.Encoding.UTF8)
    '    IO.File.WriteAllText(yeniyol & "fiyat.txt", TextBox2.Text.Trim & " TL", System.Text.Encoding.UTF8)
    '    PictureBox1.Image.Save(yeniyol & "resim.png")
    '    Dim konumu As String = My.Computer.FileSystem.ReadAllText("C:\Users\" & Environment.UserName & "\Desktop\Map data\hesap dataları\naber\dükkan\konum.txt")
    ''      Dim dükkanismi As String = konumu.Split("\")(konumu.Split("\").Count - 1)
    'Dim ürünyol = konumu & "\ürünler\"
    'Dim yeniürünlertxt As String = My.Computer.FileSystem.ReadAllText(ürünyol & "ürünler.txt")
    '    IO.File.WriteAllText(ürünyol & "ürünler.txt", yeniürünlertxt & vbNewLine & TextBox1.Text, System.Text.Encoding.UTF8)
    '    MkDir(ürünyol & TextBox1.Text)
    '    IO.File.WriteAllText(ürünyol & "açıklama.txt", RichTextBox1.Text, System.Text.Encoding.UTF8)
    '    IO.File.WriteAllText(ürünyol & "fiyat.txt", TextBox2.Text.Trim & " TL", System.Text.Encoding.UTF8)
    '    PictureBox1.Image.Save(ürünyol & "resim.png")
    Public Sub ürünkaldırsub()
        Dim ürünlerstring = My.Computer.FileSystem.ReadAllText(ürünlerkonum & "\ürünler.txt")
        Dim kaldırılıcak As String = silinicekisim
        ürünlerstring = Replace(ürünlerstring, kaldırılıcak, "")

        '     IO.Directory.Delete(ürünlerkonum & "\" & isim.Text)
        IO.File.WriteAllText(ürünlerkonum & "\ürünler.txt", ürünlerstring)
        My.Computer.FileSystem.DeleteDirectory(ürünlerkonum & "\" & silinicekisim, FileIO.UIOption.AllDialogs, FileIO.RecycleOption.SendToRecycleBin)

        Dim diğerkonmu As String = ürünlerkonum.Replace("\ürünler\", "\konum.txt")

        Dim diğerkonum = My.Computer.FileSystem.ReadAllText(diğerkonmu)

        Dim diğerürünlerkonum = diğerkonum & "\ürünler\"
        Dim a = My.Computer.FileSystem.ReadAllText(diğerürünlerkonum & "ürünler.txt")
        IO.File.WriteAllText(diğerürünlerkonum & "ürünler.txt", a.Replace(silinicekisim, ""))
        '  MsgBox(silinicekisim)
        '   IO.Directory.Delete(diğerürünlerkonum & isim.Text)
        My.Computer.FileSystem.DeleteDirectory(diğerürünlerkonum & silinicekisim, FileIO.UIOption.AllDialogs, FileIO.RecycleOption.SendToRecycleBin)
        profil.FlowLayoutPanel1.Controls.Remove(pan)

    End Sub
    Public Sub resimaçma(sender As PictureBox, e As EventArgs)
        gösterme.StartPosition = FormStartPosition.CenterScreen
        Dim göstermepic As New PictureBox
        göstermepic.Size = New Size(500, 500)
        göstermepic.Location = New Point(0, 0)
        göstermepic.SizeMode = PictureBoxSizeMode.StretchImage
        göstermepic.Image = resim.Image
        gösterme.Text = isim.Text
        gösterme.Controls.Add(göstermepic)
        Try
            gösterme.Show()
        Catch ex As Exception
            gösterme.Text = isim.Text
            gösterme.StartPosition = FormStartPosition.CenterScreen
            gösterme = New Form
            gösterme.Size = New Size(500, 500)
            göstermepic.Size = New Size(500, 500)
            göstermepic.Location = New Point(0, 0)
            göstermepic.SizeMode = PictureBoxSizeMode.StretchImage
            göstermepic.Image = resim.Image

            gösterme.Controls.Add(göstermepic)
            gösterme.Show()
        End Try
    End Sub
End Class