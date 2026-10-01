' =====================================================================
' FUNGSI KEANGGOOTAAN (Membership Function)
' Semua fungsi di sini hanya mengubah satu nilai crisp menjadi
' derajat keanggotaan yang SELALU berada pada rentang 0 sampai 1.
' =====================================================================

Public Class FungsiKeanggotaan

    ' Membatasi nilai agar tidak keluar dari rentang 0..1
    Public Shared Function BatasiDerajat(nilai As Double) As Double
        If nilai < 0.0 Then Return 0.0
        If nilai > 1.0 Then Return 1.0
        Return nilai
    End Function

    ' Fungsi keanggotaan SEGITIGA (triangle).
    ' a = titik naik, b = puncak (derajat 1), c = titik turun.
    ' Contoh: Segitiga(40, 30, 50, 70) -> 0,667
    Public Shared Function Segitiga(nilai As Double,
                                    a As Double,
                                    b As Double,
                                    c As Double) As Double
        If nilai <= a Or nilai >= c Then Return 0.0
        If nilai = b Then Return 1.0
        If nilai < b Then
            Return BatasiDerajat((nilai - a) / (b - a))
        Else
            Return BatasiDerajat((c - nilai) / (c - b))
        End If
    End Function

    ' Fungsi keanggotaan TRAPESIUM (trapezium).
    ' a = titik naik (derajat 0)
    ' b = titik mulai penuh (derajat 1)
    ' c = titik masih penuh (derajat 1)
    ' d = titik turun (derajat 0)
    ' Contoh: Trapesium(60, 30, 50, 65, 90) -> 0,60
    Public Shared Function Trapesium(nilai As Double,
                                      a As Double,
                                      b As Double,
                                      c As Double,
                                      d As Double) As Double
        If nilai <= a Or nilai >= d Then Return 0.0
        If nilai < b Then Return BatasiDerajat((nilai - a) / (b - a))
        If nilai <= c Then Return 1.0
        Return BatasiDerajat((d - nilai) / (d - c))
    End Function

    ' Fungsi keanggotaan BAHU KIRI (left shoulder / trapezium).
    ' a = titik sampai mana derajat tetap 1
    ' b = titik saat derajat sudah 0
    ' Contoh: BahuKiri(10, 25, 50) -> 1,00
    Public Shared Function BahuKiri(nilai As Double,
                                    a As Double,
                                    b As Double) As Double
        If nilai <= a Then Return 1.0
        If nilai >= b Then Return 0.0
        Return BatasiDerajat((b - nilai) / (b - a))
    End Function

    ' Fungsi keanggotaan BAHU KANAN (right shoulder / trapezium).
    ' a = titik saat derajat masih 0
    ' b = titik sampai mana derajat sudah 1
    ' Contoh: BahuKanan(90, 50, 100) -> 0,80
    Public Shared Function BahuKanan(nilai As Double,
                                     a As Double,
                                     b As Double) As Double
        If nilai <= a Then Return 0.0
        If nilai >= b Then Return 1.0
        Return BatasiDerajat((nilai - a) / (b - a))
    End Function

End Class