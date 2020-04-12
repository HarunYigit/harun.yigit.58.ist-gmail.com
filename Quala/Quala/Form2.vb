Public Class Form2
    Dim yol As String = "C:\Users\" & Environment.UserName & "\Desktop\Map data\"
    Dim tim As New Timer
    Dim kişiisim As String
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        kişiisim = profil.isim
        For Each i In IO.Directory.GetFiles(yol & "bölgeler\isimler\")
            ComboBox1.Items.Add(i.Replace(yol & "bölgeler\isimler\", "").Replace(".txt", ""))
        Next
        AddHandler tim.Tick, AddressOf tim_tick
        AddHandler tim2.Tick, AddressOf tim2_tick
        tim2.Interval = 1
        tim2.Start()
        tim.Interval = 1
        tim.Start()
        ComboBox2.Text = "Lütfen bir tür seçiniz"
        ComboBox1.Text = "Bir bölge seçiniz"
    End Sub
    Public Sub tim_tick(sender As Timer, e As EventArgs)
        If herşey = 2 Then
            Button2.Enabled = True
        Else
            Button2.Enabled = False
        End If
    End Sub
    Dim resimkoyucutim As New Timer
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        panelarkaplanayarlamapic.ImageLocation = yol & "bölgeler\fotoğraflar\" & ComboBox1.SelectedItem & ".png"
        AddHandler resimkoyucutim.Tick, AddressOf resimkoyma
        resimkoyucutim.Interval = 100
        resimkoyucutim.Start()
    End Sub
    Public Sub resimkoyma(sender As Timer, e As EventArgs)
        Try
            Panel2.BackgroundImage = panelarkaplanayarlamapic.Image
            resimkoyucutim.Stop()
            Button1.Enabled = True
        Catch ex As Exception

        End Try
    End Sub
    Dim yerseçme = False
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Button1.Text = "Yeri seç" Then
            Button1.Text = "İptal"
            yerseçme = True
        Else
            Button1.Text = "Yeri seç"
            yerseçme = False
        End If
    End Sub
    Dim herşey As Integer = 0
    Private Sub Panel2_Click(sender As Object, e As MouseEventArgs) Handles Panel2.Click
        If yerseçme = True Then
            Button1.Text = "Yeri seç"
            yerseçme = False
            If herşey = 0 Then
                herşey = 1
            ElseIf herşey = 1 Then
                herşey = 2
            End If
            yergöstermepic.Location = New Point(e.X - 20, e.Y - 20)
        End If
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        If herşey = 0 Then
            herşey = 1
        ElseIf herşey = 1 Then
            herşey = 2
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Form1.Show()
        Me.Close()
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'Dim a = "C:\Users\" & Environment.UserName & "\Desktop\Map data\bölgeler\datalar\" & ComboBox1.SelectedItem.ToString & ".txt"
        'Dim okunmuşu = My.Computer.FileSystem.ReadAllText(a)
        Dim burdakiyol = yol & "bölgeler\datalar\"
        Dim hesaplarstring = "C:\Users\" & Environment.UserName & "\Desktop\Map data\bölgeler\"
        MkDir(burdakiyol & ComboBox1.SelectedItem.ToString & "/" & TextBox1.Text)
        MkDir(burdakiyol & ComboBox1.SelectedItem.ToString & "/" & TextBox1.Text & "/ürünler")
        IO.File.WriteAllText(burdakiyol & ComboBox1.SelectedItem.ToString & "/" & TextBox1.Text & "/ürünler/ürünler.txt", "")
        IO.File.WriteAllText(burdakiyol & ComboBox1.SelectedItem.ToString & "/" & TextBox1.Text & "/açıklama.txt", RichTextBox1.Text)
        IO.File.WriteAllText(burdakiyol & ComboBox1.SelectedItem.ToString & "/" & TextBox1.Text & "/tip.txt", ComboBox2.SelectedItem.ToString)
        IO.File.WriteAllText(burdakiyol & ComboBox1.SelectedItem.ToString & "/" & TextBox1.Text & "/konum.txt", yergöstermepic.Left & "," & yergöstermepic.Top)
        IO.File.WriteAllText(burdakiyol & ComboBox1.SelectedItem.ToString & "/dükkanlar.txt", My.Computer.FileSystem.ReadAllText(burdakiyol & ComboBox1.SelectedItem.ToString & "/dükkanlar.txt").TrimStart & vbNewLine & TextBox1.Text)
        PictureBox1.Image.Save(burdakiyol & ComboBox1.SelectedItem.ToString & "/" & TextBox1.Text & "/resim.png")
        IO.File.WriteAllText("C:\Users\" & Environment.UserName & "\Desktop\Map data\hesap dataları\" & kişiisim & "\dükkan\konum.txt", $"C:\Users\" & Environment.UserName & "\Desktop\Map data\bölgeler\datalar\{ComboBox1.SelectedItem}\{TextBox1.Text}")
        IO.File.WriteAllText("C:\Users\" & Environment.UserName & "\Desktop\Map data\hesap dataları\" & kişiisim & "\dükkanvarmı.txt", "var")
        MkDir("C:\Users\" & Environment.UserName & "\Desktop\Map data\hesap dataları\" & kişiisim & "\dükkan\ürünler")
        IO.File.WriteAllText("C:\Users\" & Environment.UserName & "\Desktop\Map data\hesap dataları\" & kişiisim & "\dükkan\ürünler\ürünler.txt", "")
        '"C:\Users\" & Environment.UserName & "\Desktop\Map data\hesap dataları\" & isim & "\
        profil.isim = kişiisim
        profil.Show()
        Me.Close()
        MsgBox("Dükkanınız başarıyla eklendi")

    End Sub
    'but4
    Dim tim2 As New Timer
    Public Sub tim2_tick(sender As Timer, e As EventArgs)
        If TextBox1.Text = "" Or RichTextBox1.Text = "" Then
            Button4.Enabled = False
        Else
            Button4.Enabled = True
        End If
    End Sub
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        If OpenFileDialog1.ShowDialog(Me) = vbOK Then
            PictureBox1.ImageLocation = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Panel3.Visible = True
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

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Form1.Show()
        Me.Close()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Me.Close()
    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub
End Class