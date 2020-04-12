Public Class ürünekle
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        If OpenFileDialog1.ShowDialog(Me) = vbOK Then
            PictureBox1.ImageLocation = OpenFileDialog1.FileName
            Label5.Visible = False
        End If
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        If OpenFileDialog1.ShowDialog(Me) = vbOK Then
            PictureBox1.ImageLocation = OpenFileDialog1.FileName
            Label5.Visible = False
        End If
    End Sub
    Dim isim = profil.isim
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim yol As String = "C:\Users\" & Environment.UserName & "\Desktop\Map data\hesap dataları\" & isim & "\dükkan\ürünler\"
        Dim ürünlertxt As String = My.Computer.FileSystem.ReadAllText(yol & "ürünler.txt")
        IO.File.WriteAllText(yol & "ürünler.txt", ürünlertxt.TrimStart & vbNewLine & TextBox1.Text, System.Text.Encoding.UTF8)
        MkDir(yol & TextBox1.Text)
        Dim yeniyol As String = yol & TextBox1.Text & "\"
        IO.File.WriteAllText(yeniyol & "açıklama.txt", RichTextBox1.Text, System.Text.Encoding.UTF8)
        IO.File.WriteAllText(yeniyol & "fiyat.txt", TextBox2.Text.Trim & " TL", System.Text.Encoding.UTF8)
        PictureBox1.Image.Save(yeniyol & "resim.png")
        Dim konumu As String = My.Computer.FileSystem.ReadAllText("C:\Users\" & Environment.UserName & "\Desktop\Map data\hesap dataları\" & isim & "\dükkan\konum.txt")
        '      Dim dükkanismi As String = konumu.Split("\")(konumu.Split("\").Count - 1)
        Dim ürünyol = konumu & "\ürünler\"
        Dim yeniürünlertxt As String = My.Computer.FileSystem.ReadAllText(ürünyol & "ürünler.txt")
        IO.File.WriteAllText(ürünyol & "ürünler.txt", yeniürünlertxt & vbNewLine & TextBox1.Text, System.Text.Encoding.UTF8)
        MkDir(ürünyol & TextBox1.Text)
        IO.File.WriteAllText(ürünyol & TextBox1.Text & "\" & "açıklama.txt", RichTextBox1.Text, System.Text.Encoding.UTF8)
        IO.File.WriteAllText(ürünyol & TextBox1.Text & "\" & "fiyat.txt", TextBox2.Text.Trim & " TL", System.Text.Encoding.UTF8)
        PictureBox1.Image.Save(ürünyol & TextBox1.Text & "\" & "resim.png")
        IO.File.WriteAllText("C:\Users\" & Environment.UserName & "\Desktop\Map data\qualai\bütünürünler.txt", My.Computer.FileSystem.ReadAllText("C:\Users\" & Environment.UserName & "\Desktop\Map data\qualai\bütünürünler.txt").TrimStart & vbNewLine & TextBox1.Text.TrimStart.TrimEnd, System.Text.Encoding.UTF8)


        MsgBox("Ürününüz başarıyla eklendi")
        profil.ürünleryüklenme()
        Me.Close()
    End Sub
    Dim bakmatic As New Timer
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

    Private Sub ürünekle_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        bakmatic.Interval = 1
        AddHandler bakmatic.Tick, AddressOf txtkontrol
        bakmatic.Start()
    End Sub
    Public Sub txtkontrol(sender As Timer, e As EventArgs)
        If TextBox1.Text.Trim = "" Or TextBox2.Text.Trim = "" Or RichTextBox1.Text.Trim = "" Then
            Button1.Enabled = False
        Else
            Button1.Enabled = True
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Me.Close()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub
End Class