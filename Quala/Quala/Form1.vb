Public Class Form1
    Dim yol As String = "C:\Users\" & Environment.UserName & "\Desktop\Map data\"
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For Each i In IO.Directory.GetFiles(yol & "bölgeler\isimler\")
            ComboBox1.Items.Add(i.Replace(yol & "bölgeler\isimler\", "").Replace(".txt", ""))
        Next
        ComboBox1.Text = "Bir bölge seçiniz"

    End Sub
    Dim resimkoyucutim As New Timer
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Try
            Panel2.Controls.Clear()
            panelarkaplanayarlamapic.ImageLocation = yol & "bölgeler\fotoğraflar\" & ComboBox1.SelectedItem & ".png"

            Dim a = My.Computer.FileSystem.ReadAllText("C:\Users\" & Environment.UserName & "\Desktop\Map data\bölgeler\datalar\" & ComboBox1.SelectedItem.ToString & "/dükkanlar.txt")

            For Each i In a.Split(vbNewLine)
                Try

                    i = i.TrimStart.TrimEnd
                    Dim yol = "C:\Users\" & Environment.UserName & "\Desktop\Map data\bölgeler\datalar\" & ComboBox1.SelectedItem.ToString & "/"
                    Dim konum As String = My.Computer.FileSystem.ReadAllText(yol & i & "/konum.txt")
                    Dim tip As String = My.Computer.FileSystem.ReadAllText(yol & i & "/tip.txt")

                    Dim açıklama = My.Computer.FileSystem.ReadAllText(yol & i & "/açıklama.txt")
                    Dim resimkonum = yol & i & "/resim.png"
                    Console.WriteLine(resimkonum)
                    Dim dükkan As New dükkan
                    dükkan.konum = "C:\Users\" & Environment.UserName & "\Desktop\Map data\bölgeler\datalar\" & ComboBox1.SelectedItem.ToString & "\"
                    dükkan.yükle(i, resimkonum, konum, açıklama, tip)
                Catch ex As Exception

                End Try
            Next

            AddHandler resimkoyucutim.Tick, AddressOf resimkoyma
            resimkoyucutim.Interval = 500
            resimkoyucutim.Start()
        Catch ex As Exception

        End Try
    End Sub
    Public Sub resimkoyma(sender As Timer, e As EventArgs)
        Try
            Panel2.BackgroundImage = panelarkaplanayarlamapic.Image
            resimkoyucutim.Stop()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        ComboBox1_SelectedIndexChanged(sender, e)
        If ComboBox2.SelectedItem.ToString.Trim <> "Tümü" Then
            Dim tip As String = ComboBox2.SelectedItem.ToString
            For Each i In Panel2.Controls
                Try
                    Dim a As PictureBox = i
                    If a.Name <> ComboBox2.SelectedItem.ToString Then
                        a.Visible = False
                    Else
                        a.Visible = True
                    End If
                Catch ex As Exception
                End Try
            Next
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        giriş.Show()
        Me.Close()
    End Sub
    Dim sürükleme = False
    Dim mousex As Integer
    Dim mousey As Integer

    Private Sub Panel1_MouseDown(sender As Object, e As MouseEventArgs) Handles Panel1.MouseDown
        sürükleme = True
        mousex = Windows.Forms.Cursor.Position.X - Me.Left 'Sets variable mousex
        mousey = Windows.Forms.Cursor.Position.Y - Me.Top 'Sets variable mousey

    End Sub

    Private Sub Panel1_MouseMove(sender As Object, e As MouseEventArgs) Handles Panel1.MouseMove
        If sürükleme = True Then
            Me.Top = Windows.Forms.Cursor.Position.Y - mousey
            Me.Left = Windows.Forms.Cursor.Position.X - mousex
        End If
    End Sub

    Private Sub Panel1_MouseUp(sender As Object, e As MouseEventArgs) Handles Panel1.MouseUp
        sürükleme = False
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        qualai.Show()
        Me.Close()
    End Sub

End Class
Public Class dükkan
    Public pic As New PictureBox
    Public açıklama As New Label
    Public açık As String
    Public konum As String = ""
    Dim alttakipic As New PictureBox
    Dim üstealmapic As New PictureBox

    Public Sub yükle(açıklamatext As String, imageyol As String, konum As String, açk As String, tip As String)
        pic.Size = New Size(39, 39)
        pic.ImageLocation = imageyol
        açıklama.Text = "  " & açıklamatext
        pic.Name = tip
        açıklama.Font = Form1.Label1.Font
        pic.Location = New Point(konum.Split(",")(0).Trim, konum.Split(",")(1).Trim)
        açıklama.Width = açıklama.Text.Length * 7
        açıklama.Location = New Point((pic.Left + CInt(pic.Width / 2)) - CInt(açıklama.Width / 2), pic.Top - 25)
        açıklama.Visible = False
        açıklama.BackColor = Color.White
        açıklama.ForeColor = Color.Black
        açık = açk
        üstealmapic.Size = New Size(43, 43)
        üstealmapic.Left = pic.Left - 2
        üstealmapic.Top = pic.Top - 2
        üstealmapic.BackColor = Color.Red
        pic.BackColor = Color.Black
        Dim reg As New Drawing2D.GraphicsPath
        reg.AddEllipse(0, 0, 40, 40)
        üstealmapic.Region = New Region(reg)
        alttakipic.Size = New Size(50, 60)
        alttakipic.Image = My.Resources.Drawing1
        alttakipic.Top = pic.Top - 3
        alttakipic.Left = pic.Left - 5
        alttakipic.SizeMode = PictureBoxSizeMode.StretchImage
        alttakipic.BackColor = Color.Transparent
        Dim b As New Drawing2D.GraphicsPath
        b.AddEllipse(15, 15, 65, 60)
        pic.Cursor = Cursors.Hand
        alttakipic.Cursor = Cursors.Hand
        üstealmapic.Cursor = Cursors.Hand
        alttakipic.Region = New Region(b)
        pic.SizeMode = PictureBoxSizeMode.StretchImage
        AddHandler pic.MouseMove, AddressOf üstünegelme
        AddHandler pic.MouseLeave, AddressOf gitme
        AddHandler pic.Click, AddressOf pic_click
        AddHandler üstealmapic.MouseMove, AddressOf üstünegelme
        AddHandler üstealmapic.MouseLeave, AddressOf gitme
        AddHandler üstealmapic.Click, AddressOf pic_click
        AddHandler alttakipic.MouseMove, AddressOf üstünegelme
        AddHandler alttakipic.MouseLeave, AddressOf gitme
        AddHandler alttakipic.Click, AddressOf pic_click
        Dim a As New Drawing2D.GraphicsPath
        a.AddEllipse(0, 0, 36, 36)
        pic.Region = New Region(a)
        Form1.Panel2.Controls.Add(pic)
        Form1.Panel2.Controls.Add(alttakipic)
        Form1.Panel2.Controls.Add(üstealmapic)
        üstealmapic.BringToFront()
        Form1.Panel2.Controls.Add(açıklama)
        '  qualai.Panel5.Controls.Add(pic)
        pic.BringToFront()
    End Sub
    Public Sub üstünegelme(sender As PictureBox, e As EventArgs)
        açıklama.Visible = True
        üstealmapic.BackColor = Color.Black
        alttakipic.Image = My.Resources.Drawingblack
    End Sub
    Dim bilgilerform As New Form
    Public Sub pic_click(sender As Object, e As EventArgs)

        bilgilerform = New Form
        bilgilerform.FormBorderStyle = FormBorderStyle.None
        bilgilerform.Size = New Size(600, 500)
        Dim göstermepic As New PictureBox
        göstermepic.Size = New Size(200, 200)
        göstermepic.Location = New Point(3, 40)
        Dim b As New Bitmap(200, 200)
        göstermepic.BackgroundImageLayout = ImageLayout.Stretch
        Dim g As Graphics = Graphics.FromImage(b)
        g.DrawRectangle(New Pen(Color.Black, 3), 1, 1, 198, 198)
        Dim bb As New Bitmap(600, 500)
        Dim gg As Graphics = Graphics.FromImage(bb)
        gg.DrawRectangle(New Pen(Color.Black, 3), 0, 1, 599, 498)
        bilgilerform.BackgroundImage = bb
        Try
            g.DrawImage(pic.Image, 0, 0, 200, 200)
            g.DrawRectangle(New Pen(Color.Black, 3), 0, 0, 197, 197)
        Catch ex As Exception

        End Try
        göstermepic.Image = b
        göstermepic.SizeMode = PictureBoxSizeMode.StretchImage
        Dim kapatmabutton As New Button
        kapatmabutton.BackColor = Color.OrangeRed
        kapatmabutton.Text = "X"
        kapatmabutton.Font = New Font("Microsoft Sans Serif", 10)
        AddHandler kapatmabutton.Click, AddressOf çıkış
        kapatmabutton.Size = New Size(35, 35)
        kapatmabutton.Location = New Point(564, 1)
        kapatmabutton.FlatStyle = FlatStyle.Flat
        Dim isim As New Label
        isim.Top = 7
        isim.ForeColor = Color.OrangeRed
        isim.Text = (açıklama.Text.TrimStart)(0).ToString.ToUpper & Mid(açıklama.Text.TrimStart, 2, açıklama.Text.TrimStart.Length)
        isim.Width = 300
        isim.BackColor = Color.Gray
        isim.Left = 5
        isim.Font = New Font("Microsoft Sans Serif", 13)
        Dim taşımapic As New PictureBox
        taşımapic.BackColor = Color.Gray
        taşımapic.Size = New Size(599, 36)
        taşımapic.Location = New Point(1, 1)
        AddHandler taşımapic.MouseMove, AddressOf taşımamove
        AddHandler taşımapic.MouseDown, AddressOf taşımamousedown
        AddHandler taşımapic.MouseUp, AddressOf taşımamouseup
        AddHandler isim.MouseMove, AddressOf taşımamove
        AddHandler isim.MouseDown, AddressOf taşımamousedown
        AddHandler isim.MouseUp, AddressOf taşımamouseup
        Dim taşımapicinönündeikpic As New PictureBox
        taşımapicinönündeikpic.Size = New Size(600, 2)
        taşımapicinönündeikpic.Location = New Point(0, 37)
        taşımapicinönündeikpic.BackColor = Color.Black
        Dim ürünler As New FlowLayoutPanel
        ürünler.Left = 219
        ürünler.BackColor = Color.Gray
        bilgilerform.Controls.Add(isim)
        bilgilerform.Controls.Add(göstermepic)
        bilgilerform.Controls.Add(ürünler)
        bilgilerform.Controls.Add(kapatmabutton)
        bilgilerform.Controls.Add(taşımapic)
        bilgilerform.Controls.Add(taşımapicinönündeikpic)
        ürünler.BringToFront()
        ürünler.Top = 86
        ürünler.Size = New Size(360, 350)
        Dim açıklamatext As New RichTextBox
        açıklamatext.BorderStyle = BorderStyle.None
        açıklamatext.BackColor = bilgilerform.BackColor
        açıklamatext.Font = New Font("Microsoft Sans Serif", 12)
        açıklamatext.Size = New Size(200, 250)
        açıklamatext.Text = açık
        açıklamatext.Location = New Point(5, 245)
        Dim bbb As New Bitmap(360, 350)
        Dim ggg As Graphics = Graphics.FromImage(bbb)
        ggg.DrawRectangle(New Pen(Color.Black, 3), 0, 0, 359, 349)
        ürünler.BackgroundImage = bbb
        Dim ürünleryazanlabel As New Label
        ürünleryazanlabel.Font = New Font("Microsoft Sans Serif", 16)
        ürünleryazanlabel.Text = "Ürünler"
        ürünleryazanlabel.Top = 55
        ürünleryazanlabel.Width = 215
        ürünleryazanlabel.Left = 363
        bilgilerform.Controls.Add(ürünleryazanlabel)
        bilgilerform.Controls.Add(açıklamatext)
        bilgilerform.Show()
        ürünler.AutoScroll = True
        ''''''''''''''''''
        'Ürünler yüklenme'
        ''''''''''''''''''
        Dim isims As String = isim.Text
        Dim datakonum = konum & isims & "\ürünler\"
        Try
            Dim ürünlers As String = My.Computer.FileSystem.ReadAllText(datakonum & "ürünler.txt")

            For Each i As String In ürünlers.Split(vbNewLine)
                i = i.TrimStart.TrimEnd
                Try
                    Dim yeniürünyol = datakonum & i & "\"
                    Dim açıklama As String = My.Computer.FileSystem.ReadAllText(yeniürünyol & "açıklama.txt")
                    Dim fiyat As String = My.Computer.FileSystem.ReadAllText(yeniürünyol & "fiyat.txt")
                    Dim resimyol As String = yeniürünyol & "resim.png"

                    Dim a As New yeniürün
                    a.yükle(resimyol, i, açıklama, fiyat, ürünler)
                Catch ex As Exception

                End Try
            Next
        Catch ex As Exception
            ürünleryazanlabel.Left = 272
            ürünleryazanlabel.Text = "Ürün bulunmamaktadır"
        End Try


        'açıklama=açık

    End Sub
    Dim sürükleme = False
    Dim mousex As Integer
    Dim mousey As Integer
    Public Sub taşımamove(sender As Object, e As EventArgs)
        If sürükleme = True Then
            bilgilerform.Top = Windows.Forms.Cursor.Position.Y - mousey
            bilgilerform.Left = Windows.Forms.Cursor.Position.X - mousex
        End If
    End Sub
    Public Sub taşımamouseup(sender As Object, e As EventArgs)
        sürükleme = False
    End Sub
    Public Sub taşımamousedown(sender As Object, e As EventArgs)
        sürükleme = True
        mousex = Windows.Forms.Cursor.Position.X - bilgilerform.Left 'Sets variable mousex
        mousey = Windows.Forms.Cursor.Position.Y - bilgilerform.Top 'Sets variable mousey
    End Sub
    Public Sub çıkış(sende As Object, e As EventArgs)
        bilgilerform.Close()
    End Sub
    Public Sub gitme(sender As Object, e As EventArgs)
        açıklama.Visible = False
        üstealmapic.BackColor = Color.Red
        alttakipic.Image = My.Resources.Drawing1
    End Sub
End Class
Public Class yeniürün
    Public pan As New Panel
    Public resim As New PictureBox
    Public isimtxt As New Label
    Public açıklatxt As New Label
    Public fiyattxt As New Label
    Public Sub yükle(resimyol As String, isim As String, açıklama As String, fiyat As String, eklencek As FlowLayoutPanel)
        isimtxt.Text = isim(0).ToString.ToUpper & Mid(isim, 2, isim.Length)
        açıklatxt.Text = açıklama
        fiyattxt.Text = fiyat
        resim.ImageLocation = resimyol
        pan.Size = New Size(354, 121)
        isimtxt.Location = New Point(9, 11)
        açıklatxt.Location = New Point(9, 38)
        resim.Size = New Size(80, 80)
        resim.Location = New Point(268, 3)
        resim.SizeMode = PictureBoxSizeMode.StretchImage
        fiyattxt.Location = New Point(9, 93)
        isimtxt.Font = New Font("Microsoft Sans Serif", 12)
        fiyattxt.Font = New Font("Microsoft Sans Serif", 10)
        açıklatxt.Font = New Font("Microsoft Sans Serif", 10)
        Dim bb As New Bitmap(354, 121)
        Dim gg As Graphics = Graphics.FromImage(bb)
        gg.DrawRectangle(New Pen(Color.Black, 3), 0, 0, 352, 119)
        pan.BackgroundImage = bb
        pan.BackgroundImageLayout = ImageLayout.Stretch
        açıklatxt.Width = 257
        açıklatxt.Height = 47
        isimtxt.Width = 257
        fiyattxt.Width = 257
        pan.BackColor = Color.White
        pan.Controls.Add(fiyattxt)
        pan.Controls.Add(resim)
        pan.Controls.Add(isimtxt)
        pan.Controls.Add(açıklatxt)
        eklencek.Controls.Add(pan)
    End Sub
End Class