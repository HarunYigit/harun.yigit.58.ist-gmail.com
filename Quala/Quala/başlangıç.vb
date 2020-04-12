Public Class başlangıç
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Stop()
        Form1.Show()
        Me.Close()
    End Sub

    Private Sub başlangıç_Load(sender As Object, e As EventArgs) Handles Me.Load
        Timer1.Start()
    End Sub
End Class