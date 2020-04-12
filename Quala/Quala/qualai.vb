Public Class qualai
    Dim yol As String = "C:\Users\" & Environment.UserName & "\Desktop\Map data\"
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Form1.Show()
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

    Private Sub qualai_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For Each i In IO.Directory.GetFiles(yol & "bölgeler\isimler\")
            ComboBox1.Items.Add(i.Replace(yol & "bölgeler\isimler\", "").Replace(".txt", ""))
        Next
        ComboBox1.Text = "Bir bölge seçiniz"
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Button5.Enabled = True
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Panel3.Visible = False
        Panel4.Show()
    End Sub
    '
    Dim bütünürünler As String = My.Computer.FileSystem.ReadAllText("C:\Users\" & Environment.UserName & "\Desktop\Map data\qualai\bütünürünler.txt")
    Dim dükkan As New dükkan
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        isimler.Items.Clear()
        puanlar.Items.Clear()
        If InStr(RichTextBox1.Text, ",") Then
            RichTextBox1.Text = Replace(RichTextBox1.Text, ",", " ")
        End If
        Dim anahtarkelimeler As String = ""
        Dim arananlar As String = ""
        If RichTextBox1.Text.Split(" ").Count < 1 Then
            arananlar = RichTextBox1.Text
        Else
            For Each i As String In (RichTextBox1.Text).Split(" ")
                i = i.TrimStart.TrimEnd
                For Each a As String In bütünürünler.Split(vbNewLine)
                    a = a.TrimStart.TrimEnd
                    If i = a Then
                        arananlar += i & vbNewLine
                    End If
                Next
            Next
        End If
        Dim bölge As String = ComboBox1.SelectedItem.ToString
        Dim dükkanlar As String = My.Computer.FileSystem.ReadAllText("C:\Users\" & Environment.UserName & "\Desktop\Map data\bölgeler\datalar\" & ComboBox1.SelectedItem.ToString & "\dükkanlar.txt")
        For Each i As String In dükkanlar.Split(vbNewLine)
            i = i.TrimStart.TrimEnd
            Dim odükkanınürünleri As String = My.Computer.FileSystem.ReadAllText("C:\Users\" & Environment.UserName & "\Desktop\Map data\bölgeler\datalar\" & ComboBox1.SelectedItem.ToString & "\" & i & "\ürünler\ürünler.txt")
            Dim puan As Integer = 0
            For Each a In arananlar.TrimEnd.Split(vbNewLine)
                a = a.TrimEnd.TrimStart
                If InStr(odükkanınürünleri, a) Then
                    puan += 1
                End If
            Next
            isimler.Items.Add(i)
            puanlar.Items.Add(puan)
        Next
        For i = 0 To (isimler.Items.Count - 2) * 15
            For a = 0 To isimler.Items.Count - 2
                Dim isim = isimler.Items(a)
                Dim sisim = isimler.Items(a + 1)
                Dim puan = puanlar.Items(a)
                Dim spuan = puanlar.Items(a + 1)
                If puan < spuan Then
                    isimler.Items(a + 1) = isim
                    isimler.Items(a) = sisim
                    puanlar.Items(a) = spuan
                    puanlar.Items(a + 1) = puan
                End If
            Next
        Next




        For i = isimler.Items.Count - 1 To 0 Step -1
            If puanlar.Items(i) <> 0 Then
                dükkan = New dükkan
                Dim issim = isimler.Items(i)
                Dim yol = "C:\Users\" & Environment.UserName & "\Desktop\Map data\bölgeler\datalar\" & ComboBox1.SelectedItem.ToString & "/"
                Dim konum As String = My.Computer.FileSystem.ReadAllText(yol & issim & "/konum.txt")
                Dim tip As String = My.Computer.FileSystem.ReadAllText(yol & issim & "/tip.txt")

                Dim açıklama = My.Computer.FileSystem.ReadAllText(yol & issim & "/açıklama.txt")
                Dim resimkonum = yol & issim.ToString.TrimEnd.TrimStart & "/resim.png"

                dükkan.konum = "C:\Users\" & Environment.UserName & "\Desktop\Map data\bölgeler\datalar\" & ComboBox1.SelectedItem.ToString & "\"
                dükkan.yükle(issim, resimkonum, konum, açıklama, tip)
                Panel5.Controls.Add(dükkan.pic)
                ToolTip1.SetToolTip(dükkan.pic, issim)
            End If
        Next

        Panel6.Visible = False
        Timer1.Start()
        ' MsgBox("Size en uygun market: " & isimler.Items(0))
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Panel2.Visible = False
        Panel3.Show()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        '    dükkan.pic_click(dükkan.pic, e)
        Panel4.Visible = False
        If Panel5.Controls.Count = 0 Then
            Label5.Text = "Malesef hiç sonuç bulamadım."
            Label5.Visible = True
            Label6.Visible = True
            Button7.Visible = True
        Else
            Label5.Visible = True
        End If
        Panel5.Show()
        Timer1.Stop()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Panel5.Visible = False
        Panel6.Visible = True
        Panel6.Visible = False
        Label5.Visible = False
        Label6.Visible = False
        Button7.Visible = False
    End Sub
End Class
