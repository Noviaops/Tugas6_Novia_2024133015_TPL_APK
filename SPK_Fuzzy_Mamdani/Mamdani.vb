' =====================================================================
' MESIN INFERENSI FUZZY MAMDANI
'
' Alur:
'   Input Crisp -> Fuzzifikasi -> Rule Base -> Inferensi Mamdani
'   -> Agregasi (MAX) -> Defuzzifikasi (Centroid) -> Nilai Akhir
'
' Semua titik belonging di bawah ini dapat diubah/disesuaikan.
' =====================================================================

Public Class Mamdani

    ' -----------------------------------------------------------------
    ' KONSTANTA
    ' -----------------------------------------------------------------

    ' Universe semua variabel = 0 .. 100
    Public Const MIN_UNIVERSE As Double = 0.0
    Public Const MAX_UNIVERSE As Double = 100.0

    ' Jarak antar titik saat menghitung centroid (semakin kecil, semakin presisi)
    Public Const LANGKAH_DEFUZZ As Double = 0.01

    ' -----------------------------------------------------------------
    ' TITIK BELONGING INPUT  (Tugas / UTS / UAS)
    '   Rendah : 0   .. 50   (derajat 1 sampai angka 25)
    '   Sedang : 30  .. 70   (derajat penuh pada angka 50..65)
    '   Tinggi : 50  .. 100  (derajat 1 mulai angka 100)
    '
    ' CATATAN: himpunan SEDANG memakai trapesium 30-50-65-90.
    ' Kalau diubah menjadi segitiga 30-50-70, maka angka 70 ke atas
    ' otomatis memiliki derajat Sedang = 0 sehingga hanya rule
    ' "Tinggi" yang menyala. Akibatnya semua nilai 70..100 mendapat
    ' hasil centroid yang hampir sama dan tidak bisa dibedakan.
    ' Ekor sampai 90 dipakai supaya nilai 70..90 tetap berbobot "Sedang".
    ' Kalau dosen meminta himpunan Sedang benar-benar 30..70,
    ' cukup ubah S_TITIK_D menjadi 70.
    ' -----------------------------------------------------------------
    Public Const R_TITIK_AH As Double = 25.0
    Public Const R_TITIK_BH As Double = 50.0
    Public Const S_TITIK_A As Double = 30.0
    Public Const S_TITIK_B As Double = 50.0
    Public Const S_TITIK_C As Double = 65.0
    Public Const S_TITIK_D As Double = 90.0
    Public Const T_TITIK_A As Double = 50.0
    Public Const T_TITIK_B As Double = 100.0

    ' -----------------------------------------------------------------
    ' TITIK BELONGING OUTPUT (Nilai Akhir)
    '
    ' Puncak tiap himpunan sengaja diletakkan di TENGAH rentang predikat:
    '   Kurang     (0-59,99)   -> puncak 30  (titik 0 - 30 - 60)
    '   Cukup      (60-69,99)  -> puncak 65  (titik 50 - 65 - 80)
    '   Baik       (70-84,99)  -> puncak 77  (titik 65 - 77 - 90)
    '   SangatBaik (85-100)    -> puncak 93  (titik 80 - 93 - 100)
    '
    ' Karena titik tengah sebuah segitiga sama dengan rata-rata
    ' (a + b + c) / 3, maka kalau hanya satu himpunan output yang menyala,
    ' hasil centroid-nya jatuh tepat di predikat yang seharusnya.
    ' -----------------------------------------------------------------
    Public Const K_A As Double = 0.0
    Public Const K_B As Double = 30.0
    Public Const K_C As Double = 60.0
    Public Const C_A As Double = 50.0
    Public Const C_B As Double = 65.0
    Public Const C_C As Double = 80.0
    Public Const B_A As Double = 65.0
    Public Const B_B As Double = 77.0
    Public Const B_C As Double = 90.0
    Public Const SB_A As Double = 80.0
    Public Const SB_B As Double = 93.0
    Public Const SB_C As Double = 100.0

    ' =================================================================
    ' BAGIAN 1 - FUNGSI KEANGGOOTAAN VARIABEL TUGAS
    ' Nilai yang masuk sudah dinormalisasi ke skala 0..100
    ' =================================================================
    Public Shared Function MuTugasRendah(nilai As Double) As Double
        Return FungsiKeanggotaan.BahuKiri(nilai, R_TITIK_AH, R_TITIK_BH)
    End Function

    Public Shared Function MuTugasSedang(nilai As Double) As Double
        Return FungsiKeanggotaan.Trapesium(nilai, S_TITIK_A, S_TITIK_B, S_TITIK_C, S_TITIK_D)
    End Function

    Public Shared Function MuTugasTinggi(nilai As Double) As Double
        Return FungsiKeanggotaan.BahuKanan(nilai, T_TITIK_A, T_TITIK_B)
    End Function

    ' =================================================================
    ' BAGIAN 2 - FUNGSI KEANGGOOTAAN VARIABEL UTS
    ' =================================================================
    Public Shared Function MuUTSRendah(nilai As Double) As Double
        Return FungsiKeanggotaan.BahuKiri(nilai, R_TITIK_AH, R_TITIK_BH)
    End Function

    Public Shared Function MuUTSSedang(nilai As Double) As Double
        Return FungsiKeanggotaan.Trapesium(nilai, S_TITIK_A, S_TITIK_B, S_TITIK_C, S_TITIK_D)
    End Function

    Public Shared Function MuUTSTinggi(nilai As Double) As Double
        Return FungsiKeanggotaan.BahuKanan(nilai, T_TITIK_A, T_TITIK_B)
    End Function

    ' =================================================================
    ' BAGIAN 3 - FUNGSI KEANGGOOTAAN VARIABEL UAS
    ' =================================================================
    Public Shared Function MuUASRendah(nilai As Double) As Double
        Return FungsiKeanggotaan.BahuKiri(nilai, R_TITIK_AH, R_TITIK_BH)
    End Function

    Public Shared Function MuUASSedang(nilai As Double) As Double
        Return FungsiKeanggotaan.Trapesium(nilai, S_TITIK_A, S_TITIK_B, S_TITIK_C, S_TITIK_D)
    End Function

    Public Shared Function MuUASTinggi(nilai As Double) As Double
        Return FungsiKeanggotaan.BahuKanan(nilai, T_TITIK_A, T_TITIK_B)
    End Function

    ' =================================================================
    ' BAGIAN 4 - FUNGSI KEANGGOOTAAN VARIABEL OUTPUT (NILAI AKHIR)
    ' =================================================================
    Public Shared Function MuKurang(nilai As Double) As Double
        Return FungsiKeanggotaan.Segitiga(nilai, K_A, K_B, K_C)
    End Function

    Public Shared Function MuCukup(nilai As Double) As Double
        Return FungsiKeanggotaan.Segitiga(nilai, C_A, C_B, C_C)
    End Function

    Public Shared Function MuBaik(nilai As Double) As Double
        Return FungsiKeanggotaan.Segitiga(nilai, B_A, B_B, B_C)
    End Function

    Public Shared Function MuSangatBaik(nilai As Double) As Double
        Return FungsiKeanggotaan.Segitiga(nilai, SB_A, SB_B, SB_C)
    End Function

    ' =================================================================
    ' BAGIAN 5 - FUZZIFIKASI
    ' Mengubah input crisp menjadi derajat keanggotaan 0..1
    ' =================================================================
    Public Shared Function FuzzifikasiTugas(nilai As Double) As DerajatInput
        Dim hasil As DerajatInput
        hasil.Rendah = MuTugasRendah(nilai)
        hasil.Sedang = MuTugasSedang(nilai)
        hasil.Tinggi = MuTugasTinggi(nilai)
        Return hasil
    End Function

    Public Shared Function FuzzifikasiUTS(nilai As Double) As DerajatInput
        Dim hasil As DerajatInput
        hasil.Rendah = MuUTSRendah(nilai)
        hasil.Sedang = MuUTSSedang(nilai)
        hasil.Tinggi = MuUTSTinggi(nilai)
        Return hasil
    End Function

    Public Shared Function FuzzifikasiUAS(nilai As Double) As DerajatInput
        Dim hasil As DerajatInput
        hasil.Rendah = MuUASRendah(nilai)
        hasil.Sedang = MuUASSedang(nilai)
        hasil.Tinggi = MuUASTinggi(nilai)
        Return hasil
    End Function

    ' =================================================================
    ' BAGIAN 6 - RULE BASE + INFERENSI MAMDANI
    '
    ' Operator AND  -> MIN
    ' Operator OR   -> MAX
    ' Alpha-predicate = MIN seluruh premis pada rule AND
    '
    ' Rule disusun agar SEMUA 27 kombinasi (R/S/H x R/S/H x R/S/H)
    ' selalu menghasilkan output, jadi tidak ada input tanpa jawaban.
    ' =================================================================
    Public Shared Function Inferensi(nilaiTugas As Double,
                                     nilaiUTS As Double,
                                     nilaiUAS As Double) As HasilProsesFuzzy

        Dim hasil As New HasilProsesFuzzy()

        ' -- Fuzzifikasi
        Dim mT As DerajatInput = FuzzifikasiTugas(nilaiTugas)
        Dim mU As DerajatInput = FuzzifikasiUTS(nilaiUTS)
        Dim mA As DerajatInput = FuzzifikasiUAS(nilaiUAS)

        hasil.Tugas = mT
        hasil.UTS = mU
        hasil.UAS = mA

        ' Shorthand agar rule di bawah mudah dibaca
        Dim tgsR As Double = mT.Rendah
        Dim tgsS As Double = mT.Sedang
        Dim tgsH As Double = mT.Tinggi
        Dim utsR As Double = mU.Rendah
        Dim utsS As Double = mU.Sedang
        Dim utsH As Double = mU.Tinggi
        Dim uasR As Double = mA.Rendah
        Dim uasS As Double = mA.Sedang
        Dim uasH As Double = mA.Tinggi

        Dim daftar As List(Of AturanAktif) = hasil.DaftarRule
        Dim agregasi As DerajatOutput

        ' -- R1 : ketiga input tinggi -> Sangat Baik
        TambahRule(daftar, agregasi, "R1",
                   "Tugas Tinggi AND UTS Tinggi AND UAS Tinggi",
                   HimpunanOutput.SangatBaik, Min3(tgsH, utsH, uasH))

        ' -- R2 : tugas tinggi, UTS sedang, UAS tinggi -> Baik
        TambahRule(daftar, agregasi, "R2",
                   "Tugas Tinggi AND UTS Sedang AND UAS Tinggi",
                   HimpunanOutput.Baik, Min3(tgsH, utsS, uasH))

        ' -- R3 : tugas sedang, UTS tinggi, UAS tinggi -> Baik
        TambahRule(daftar, agregasi, "R3",
                   "Tugas Sedang AND UTS Tinggi AND UAS Tinggi",
                   HimpunanOutput.Baik, Min3(tgsS, utsH, uasH))

        ' -- R4 : tugas tinggi, UTS tinggi, UAS sedang -> Baik
        TambahRule(daftar, agregasi, "R4",
                   "Tugas Tinggi AND UTS Tinggi AND UAS Sedang",
                   HimpunanOutput.Baik, Min3(tgsH, utsH, uasS))

        ' -- R5 : tugas tinggi, UTS sedang, UAS sedang -> Cukup
        TambahRule(daftar, agregasi, "R5",
                   "Tugas Tinggi AND UTS Sedang AND UAS Sedang",
                   HimpunanOutput.Cukup, Min3(tgsH, utsS, uasS))

        ' -- R6 : ketiga sedang -> Cukup
        TambahRule(daftar, agregasi, "R6",
                   "Tugas Sedang AND UTS Sedang AND UAS Sedang",
                   HimpunanOutput.Cukup, Min3(tgsS, utsS, uasS))

        ' -- R7 : tugas sedang, UTS tinggi, UAS sedang -> Baik
        TambahRule(daftar, agregasi, "R7",
                   "Tugas Sedang AND UTS Tinggi AND UAS Sedang",
                   HimpunanOutput.Baik, Min3(tgsS, utsH, uasS))

        ' -- R8 : tugas sedang, UTS sedang, UAS tinggi -> Baik
        TambahRule(daftar, agregasi, "R8",
                   "Tugas Sedang AND UTS Sedang AND UAS Tinggi",
                   HimpunanOutput.Baik, Min3(tgsS, utsS, uasH))

        ' -- R9 : ada satu saja input RENDAH -> Kurang
        TambahRule(daftar, agregasi, "R9",
                   "Tugas Rendah AND UTS Tinggi AND UAS Tinggi",
                   HimpunanOutput.Kurang, Min3(tgsR, utsH, uasH))

        TambahRule(daftar, agregasi, "R10",
                   "Tugas Rendah AND UTS Tinggi AND UAS Sedang",
                   HimpunanOutput.Kurang, Min3(tgsR, utsH, uasS))

        TambahRule(daftar, agregasi, "R11",
                   "Tugas Rendah AND UTS Tinggi AND UAS Rendah",
                   HimpunanOutput.Kurang, Min3(tgsR, utsH, uasR))

        TambahRule(daftar, agregasi, "R12",
                   "Tugas Rendah AND UTS Sedang AND UAS Tinggi",
                   HimpunanOutput.Kurang, Min3(tgsR, utsS, uasH))

        TambahRule(daftar, agregasi, "R13",
                   "Tugas Rendah AND UTS Sedang AND UAS Sedang",
                   HimpunanOutput.Kurang, Min3(tgsR, utsS, uasS))

        TambahRule(daftar, agregasi, "R14",
                   "Tugas Rendah AND UTS Sedang AND UAS Rendah",
                   HimpunanOutput.Kurang, Min3(tgsR, utsS, uasR))

        TambahRule(daftar, agregasi, "R15",
                   "Tugas Rendah AND UTS Rendah AND UAS Tinggi",
                   HimpunanOutput.Kurang, Min3(tgsR, utsR, uasH))

        TambahRule(daftar, agregasi, "R16",
                   "Tugas Rendah AND UTS Rendah AND UAS Sedang",
                   HimpunanOutput.Kurang, Min3(tgsR, utsR, uasS))

        TambahRule(daftar, agregasi, "R17",
                   "Tugas Rendah AND UTS Rendah AND UAS Rendah",
                   HimpunanOutput.Kurang, Min3(tgsR, utsR, uasR))

        TambahRule(daftar, agregasi, "R18",
                   "Tugas Sedang AND UTS Tinggi AND UAS Rendah",
                   HimpunanOutput.Kurang, Min3(tgsS, utsH, uasR))

        TambahRule(daftar, agregasi, "R19",
                   "Tugas Sedang AND UTS Sedang AND UAS Rendah",
                   HimpunanOutput.Kurang, Min3(tgsS, utsS, uasR))

        TambahRule(daftar, agregasi, "R20",
                   "Tugas Sedang AND UTS Rendah AND UAS Tinggi",
                   HimpunanOutput.Kurang, Min3(tgsS, utsR, uasH))

        TambahRule(daftar, agregasi, "R21",
                   "Tugas Sedang AND UTS Rendah AND UAS Sedang",
                   HimpunanOutput.Kurang, Min3(tgsS, utsR, uasS))

        TambahRule(daftar, agregasi, "R22",
                   "Tugas Sedang AND UTS Rendah AND UAS Rendah",
                   HimpunanOutput.Kurang, Min3(tgsS, utsR, uasR))

        TambahRule(daftar, agregasi, "R23",
                   "Tugas Tinggi AND UTS Tinggi AND UAS Rendah",
                   HimpunanOutput.Kurang, Min3(tgsH, utsH, uasR))

        TambahRule(daftar, agregasi, "R24",
                   "Tugas Tinggi AND UTS Sedang AND UAS Rendah",
                   HimpunanOutput.Kurang, Min3(tgsH, utsS, uasR))

        TambahRule(daftar, agregasi, "R25",
                   "Tugas Tinggi AND UTS Rendah AND UAS Tinggi",
                   HimpunanOutput.Kurang, Min3(tgsH, utsR, uasH))

        TambahRule(daftar, agregasi, "R26",
                   "Tugas Tinggi AND UTS Rendah AND UAS Sedang",
                   HimpunanOutput.Kurang, Min3(tgsH, utsR, uasS))

        TambahRule(daftar, agregasi, "R27",
                   "Tugas Tinggi AND UTS Rendah AND UAS Rendah",
                   HimpunanOutput.Kurang, Min3(tgsH, utsR, uasR))

        ' -- R28 : operator OR (MAX). Cukup satu input rendah -> Kurang
        TambahRule(daftar, agregasi, "R28",
                   "Tugas Rendah OR UTS Rendah OR UAS Rendah",
                   HimpunanOutput.Kurang, Max3(tgsR, utsR, uasR))

        ' -- Agregasi sudah terkumpul di "agregasi" (MAX per himpunan output)
        hasil.Agregasi = agregasi

        ' -- Defuzzifikasi centroid
        hasil.NilaiAkhir = Defuzzifikasi(agregasi)

        ' -- Predikat
        hasil.Predikat = TentukanPredikat(hasil.NilaiAkhir)

        Return hasil
    End Function

    ' Menyimpan satu rule ke daftar dan melakukan agregasi (MAX)
    Private Shared Sub TambahRule(daftar As List(Of AturanAktif),
                                  ByRef agregasi As DerajatOutput,
                                  id As String,
                                  kondisi As String,
                                  keluaran As HimpunanOutput,
                                  alpha As Double)

        Dim a As Double = FungsiKeanggotaan.BatasiDerajat(alpha)

        Dim aturan As New AturanAktif()
        aturan.Id = id
        aturan.KondisiIF = "IF " & kondisi
        aturan.KonsekuenTHEN = "THEN " & NamaHimpunan(keluaran)
        aturan.Alpha = a
        daftar.Add(aturan)

        ' Agregasi memakai MAX antar rule
        Select Case keluaran
            Case HimpunanOutput.Kurang
                If a > agregasi.Kurang Then agregasi.Kurang = a
            Case HimpunanOutput.Cukup
                If a > agregasi.Cukup Then agregasi.Cukup = a
            Case HimpunanOutput.Baik
                If a > agregasi.Baik Then agregasi.Baik = a
            Case HimpunanOutput.SangatBaik
                If a > agregasi.SangatBaik Then agregasi.SangatBaik = a
        End Select
    End Sub

    ' =================================================================
    ' BAGIAN 7 - DEFUZZIFIKASI CENTROID
    '   Z* = SUM(mu(z) x z) / SUM(mu(z))
    ' Perhitungan dilakukan numerik dengan LANGKAH_DEFUZZ titik.
    ' Jika tidak ada rule yang menyala (pembagi 0) hasilnya 0.
    ' =================================================================
    Public Shared Function Defuzzifikasi(agregasi As DerajatOutput) As Double

        Dim jumlahBobot As Double = 0.0
        Dim jumlahNilai As Double = 0.0

        Dim jumlahTitik As Integer = CInt((MAX_UNIVERSE - MIN_UNIVERSE) / LANGKAH_DEFUZZ)

        For i As Integer = 0 To jumlahTitik
            Dim z As Double = MIN_UNIVERSE + (i * LANGKAH_DEFUZZ)

            ' Output tiap rule dipotong (clipped) oleh alpha-predicate-nya
            Dim derajatKurang As Double = Math.Min(MuKurang(z), agregasi.Kurang)
            Dim derajatCukup As Double = Math.Min(MuCukup(z), agregasi.Cukup)
            Dim derajatBaik As Double = Math.Min(MuBaik(z), agregasi.Baik)
            Dim derajatSangatBaik As Double = Math.Min(MuSangatBaik(z), agregasi.SangatBaik)

            ' Agregasi antar himpunan output memakai MAX
            Dim mu As Double = Max4(derajatKurang, derajatCukup, derajatBaik, derajatSangatBaik)

            jumlahNilai = jumlahNilai + (mu * z * LANGKAH_DEFUZZ)
            jumlahBobot = jumlahBobot + (mu * LANGKAH_DEFUZZ)
        Next

        ' Hindari pembagian dengan nol
        If jumlahBobot <= 0.0 Then Return 0.0

        Dim hasil As Double = jumlahNilai / jumlahBobot
        If hasil < MIN_UNIVERSE Then hasil = MIN_UNIVERSE
        If hasil > MAX_UNIVERSE Then hasil = MAX_UNIVERSE
        Return hasil

    End Function

    ' =================================================================
    ' BAGIAN 8 - PREDIKAT NILAI AKHIR
    '   0    - 59.99  = Kurang
    '   60   - 69.99  = Cukup
    '   70   - 84.99  = Baik
    '   85   - 100    = Sangat Baik
    ' =================================================================
    Public Shared Function TentukanPredikat(nilai As Double) As String
        Dim n As Double = nilai
        If n < MIN_UNIVERSE Then n = MIN_UNIVERSE
        If n > MAX_UNIVERSE Then n = MAX_UNIVERSE

        If n < 60.0 Then
            Return "Kurang"
        ElseIf n < 70.0 Then
            Return "Cukup"
        ElseIf n < 85.0 Then
            Return "Baik"
        Else
            Return "Sangat Baik"
        End If
    End Function

    ' =================================================================
    ' BAGIAN 9 - PEMBANTU
    ' =================================================================
    Private Shared Function Min3(a As Double, b As Double, c As Double) As Double
        Return Math.Min(a, Math.Min(b, c))
    End Function

    Private Shared Function Max3(a As Double, b As Double, c As Double) As Double
        Return Math.Max(a, Math.Max(b, c))
    End Function

    Private Shared Function Max4(a As Double, b As Double, c As Double, d As Double) As Double
        Return Math.Max(a, Math.Max(b, Math.Max(c, d)))
    End Function

    Public Shared Function NamaHimpunan(himpunan As HimpunanOutput) As String
        Select Case himpunan
            Case HimpunanOutput.Kurang
                Return "Kurang"
            Case HimpunanOutput.Cukup
                Return "Cukup"
            Case HimpunanOutput.Baik
                Return "Baik"
            Case Else
                Return "Sangat Baik"
        End Select
    End Function

End Class