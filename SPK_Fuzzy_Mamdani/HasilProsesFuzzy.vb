' =====================================================================
' STRUKTUR DATA FUZZY
' Berisi tempat menyimpan hasil fuzzifikasi, daftar rule, dan hasil akhir.
' Dipakai agar tampilan di Form tidak bercampur dengan logika fuzzy.
' =====================================================================

' Daftar himpunan fuzzy pada variabel input (Tugas, UTS, UAS)
Public Enum HimpunanInput
    Rendah = 0
    Sedang = 1
    Tinggi = 2
End Enum

' Daftar himpunan fuzzy pada variabel output (Nilai Akhir)
Public Enum HimpunanOutput
    Kurang = 0
    Cukup = 1
    Baik = 2
    SangatBaik = 3
End Enum

' Derajat keanggotaan untuk 3 himpunan input
Public Structure DerajatInput
    Public Rendah As Double
    Public Sedang As Double
    Public Tinggi As Double

    Public Overrides Function ToString() As String
        Return "Rendah = " & Fmt.Rendah(Rendah) &
               " | Sedang = " & Fmt.Rendah(Sedang) &
               " | Tinggi = " & Fmt.Rendah(Tinggi)
    End Function
End Structure

' Derajat keanggotaan untuk 4 himpunan output
Public Structure DerajatOutput
    Public Kurang As Double
    Public Cukup As Double
    Public Baik As Double
    Public SangatBaik As Double

    Public Overrides Function ToString() As String
        Return "Kurang = " & Fmt.Rendah(Kurang) &
               " | Cukup = " & Fmt.Rendah(Cukup) &
               " | Baik = " & Fmt.Rendah(Baik) &
               " | SangatBaik = " & Fmt.Rendah(SangatBaik)
    End Function
End Structure

' Satu baris rule beserta alpha-predicate-nya
Public Class AturanAktif
    Public Property Id As String
    Public Property KondisiIF As String
    Public Property KonsekuenTHEN As String
    Public Property Alpha As Double
End Class

' Kumpulan hasil seluruh proses fuzzy
Public Class HasilProsesFuzzy

    Public Property Tugas As DerajatInput
    Public Property UTS As DerajatInput
    Public Property UAS As DerajatInput

    Public Property Agregasi As DerajatOutput

    Public Property NilaiAkhir As Double
    Public Property Predikat As String

    Public Property DaftarRule As List(Of AturanAktif) = New List(Of AturanAktif)()

    ' Menghitung rule yang benar-benar menyala (alpha > 0)
    Public Function JumlahRuleAktif() As Integer
        Dim n As Integer = 0
        For Each r As AturanAktif In DaftarRule
            If r.Alpha > 0 Then n = n + 1
        Next
        Return n
    End Function

End Class

' Helper kecil untuk format angka agar tampilan konsisten
Public Module Fmt
    Public Function Rendah(nilai As Double) As String
        Return nilai.ToString("F2")
    End Function

    Public Function DuaDesimal(nilai As Double) As String
        Return nilai.ToString("F2")
    End Function
End Module