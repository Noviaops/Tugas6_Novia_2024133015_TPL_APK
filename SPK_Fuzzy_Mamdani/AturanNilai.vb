' =====================================================================
' ATURAN NILAI
' Berisi bobot penilaian dan cara mengubah jumlah tugas menjadi nilai.
' Semua angka di file ini BISA DIEDIT dengan mudah.
' =====================================================================

Public Class AturanNilai

    ' ---------------------------------------------------------------
    ' BOBOT PENILAIAN (total = 100%)
    ' Tugas 50% , UTS 20% , UAS 30%
    ' ---------------------------------------------------------------
    Public Const BOBOT_TUGAS As Double = 0.5
    Public Const BOBOT_UTS As Double = 0.2
    Public Const BOBOT_UAS As Double = 0.3

    ' Nilai maksimal kontribusi tugas.
    ' 10 tugas = 50 poin (menurut ketentuan dosen, tidak diubah)
    Public Const MAKS_KONTRIBUSI_TUGAS As Double = 50.0

    ' Jumlah tugas maksimal yang dinilai = 10
    Public Const MAKS_TUGAS As Integer = 10

    ' Nilai UTS dan UAS maksimal
    Public Const MAKS_NILAI As Double = 100.0

    ' ---------------------------------------------------------------
    ' Mengubah jumlah tugas menjadi kontribusi nilai tugas.
    ' Ketentuan DOSEN yang WAJIB dipertahankan:
    '     0 tugas  = 0 poin
    '     7 tugas  = 40 poin
    '     8 tugas  = 45 poin
    '     10 tugas = 50 poin
    '
    ' CATATAN: aturan untuk jumlah tugas 1 sampai 6 dan 9 tugas
    ' belum dijelaskan dosen, jadi bagian ini memakai interpolasi
    ' linear dan BOLEH DIUBAH mengikuti ketentuan dosen.
    ' ---------------------------------------------------------------
    Public Shared Function HitungKontribusiTugas(jumlahTugas As Integer) As Double

        ' Batasi agar aman dari nilai di luar range
        Dim j As Integer = jumlahTugas
        If j < 0 Then j = 0
        If j > MAKS_TUGAS Then j = MAKS_TUGAS

        ' Tidak mengumpulkan tugas = 0 poin
        If j = 0 Then Return 0.0

        ' 1..7 tugas: garis lurus dari 0 poin ke 40 poin
        If j <= 7 Then Return j * 40.0 / 7.0

        ' Ketentuan dosen
        If j = 8 Then Return 45.0
        If j = 9 Then Return 47.5   ' rata-rata antara 45 dan 50 (bisa diubah)
        Return 50.0                  ' 10 tugas = 50 poin

    End Function

    ' Kontribusi UTS = nilai UTS x 20%
    Public Shared Function HitungKontribusiUTS(nilaiUTS As Double) As Double
        Return nilaiUTS * BOBOT_UTS
    End Function

    ' Kontribusi UAS = nilai UAS x 30%
    Public Shared Function HitungKontribusiUAS(nilaiUAS As Double) As Double
        Return nilaiUAS * BOBOT_UAS
    End Function

    ' ---------------------------------------------------------------
    ' Normalisasi kontribusi tugas ke skala 0..100
    ' supaya bisa masuk ke universe variabel fuzzy TUGAS.
    ' Contoh: 45 poin dari 50 -> (45/50) x 100 = 90
    ' ---------------------------------------------------------------
    Public Shared Function NormalisasiNilaiTugas(kontribusiTugas As Double) As Double
        If MAKS_KONTRIBUSI_TUGAS = 0 Then Return 0.0
        Return (kontribusiTugas / MAKS_KONTRIBUSI_TUGAS) * 100.0
    End Function

End Class