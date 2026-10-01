' =====================================================================
' FORM UTAMA - SPK Penilaian Nilai Akhir DSS (Fuzzy Mamdani)
'
' File ini hanya mengatur tampilan data.
' Semua logika fuzzy ada di Mamdani.vb, FungsiKeanggotaan.vb,
' dan AturanNilai.vb.
' =====================================================================

Imports System.Windows.Forms

Public Class FormNilaiAkhir
    Inherits System.Windows.Forms.Form

    ' kolom DataGridView, dibuat ulang setiap-reset
    Private Const COL_ID As String = "colId"
    Private Const COL_IF As String = "colIf"
    Private Const COL_THEN As String = "colThen"
    Private Const COL_ALPHA As String = "colAlpha"

    Private Sub FormNilaiAkhir_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AturTabelRule()
        ResetForm()
    End Sub

    ' =================================================================
    ' INPUT TUGAS: SATU KOTAK CENTANG UNTUK SETIAP TUGAS
    '
    ' Mahasiswa mencentang tugas yang benar-benar dikumpulkan.
    ' Jumlah kotak yang tercentang inilah yang dipakai sebagai
    ' "jumlah tugas" untuk seluruh perhitungan berikutnya.
    ' =================================================================
    Private Function DaftarKotakTugas() As CheckBox()
        Return New CheckBox() {
            chkTugas1, chkTugas2, chkTugas3, chkTugas4, chkTugas5,
            chkTugas6, chkTugas7, chkTugas8, chkTugas9, chkTugas10
        }
    End Function

    ' Menghitung berapa banyak tugas yang dicentang
    Private Function HitungTugasTerkumpul() As Integer
        Dim jumlah As Integer = 0
        For Each kotak As CheckBox In DaftarKotakTugas()
            If kotak.Checked Then jumlah += 1
        Next
        Return jumlah
    End Function

    ' Mencentang atau mengosongkan semua kotak tugas
    Private Sub SetSemuaTugas(dicentang As Boolean)
        For Each kotak As CheckBox In DaftarKotakTugas()
            kotak.Checked = dicentang
        Next
    End Sub

    ' Persentase tugas yang dikumpulkan dari total tugas
    Private Function PersentaseTugas(jumlahTugas As Integer) As Double
        If AturanNilai.MAKS_TUGAS <= 0 Then Return 0.0
        Return jumlahTugas * 100.0 / AturanNilai.MAKS_TUGAS
    End Function

    ' Menampilkan ringkasan jumlah dan persentase tugas
    Private Sub TampilkanJumlahTugas()
        Dim jumlah As Integer = HitungTugasTerkumpul()
        lblJumlahTugas.Text = "Tugas terkumpul: " & jumlah.ToString() & " dari " &
                              AturanNilai.MAKS_TUGAS.ToString() & " tugas (" &
                              Fmt.DuaDesimal(PersentaseTugas(jumlah)) & " %)"
    End Sub

    ' Dipanggil setiap kali salah satu kotak tugas dicentang atau dilepas,
    ' supaya jumlah dan persentase langsung ikut berubah.
    Private Sub chkTugas_CheckedChanged(sender As Object, e As EventArgs) _
        Handles chkTugas1.CheckedChanged, chkTugas2.CheckedChanged, chkTugas3.CheckedChanged, _
               chkTugas4.CheckedChanged, chkTugas5.CheckedChanged, chkTugas6.CheckedChanged, _
               chkTugas7.CheckedChanged, chkTugas8.CheckedChanged, chkTugas9.CheckedChanged, _
               chkTugas10.CheckedChanged

        TampilkanJumlahTugas()
    End Sub

    ' =================================================================
    ' EVENT TOMBOL HITUNG
    ' =================================================================
    Private Sub btnHitung_Click(sender As Object, e As EventArgs) Handles btnHitung.Click

        Dim jumlahTugas As Integer
        Dim nilaiUTS As Double
        Dim nilaiUAS As Double

        ' Validasi input dulu, kalau tidak valid jangan dihitung
        If Not ValidasiInput(jumlahTugas, nilaiUTS, nilaiUAS) Then
            Exit Sub
        End If

        Try
            ' -- 1. Hitung kontribusi tiap komponen (perhitungan biasa)
            Dim kontribusiTugas As Double = AturanNilai.HitungKontribusiTugas(jumlahTugas)
            Dim kontribusiUTS As Double = AturanNilai.HitungKontribusiUTS(nilaiUTS)
            Dim kontribusiUAS As Double = AturanNilai.HitungKontribusiUAS(nilaiUAS)

            ' -- 2. Normalisasi nilai tugas ke skala 0..100 agar bisa
            '       masuk universe variabel fuzzy TUGAS
            Dim nilaiTugasFuzzy As Double = AturanNilai.NormalisasiNilaiTugas(kontribusiTugas)

            ' -- 3. Proses fuzzy Mamdani
            Dim hasil As HasilProsesFuzzy = Mamdani.Inferensi(nilaiTugasFuzzy, nilaiUTS, nilaiUAS)

            ' -- 4. Tampilkan semua hasil
            TampilkanPerhitungan(jumlahTugas, kontribusiTugas, nilaiTugasFuzzy, nilaiUTS, nilaiUAS, kontribusiUTS, kontribusiUAS)
            TampilkanTabelRule(hasil)
            TampilkanProses(hasil, jumlahTugas, kontribusiTugas, nilaiTugasFuzzy, nilaiUTS, nilaiUAS, kontribusiUTS, kontribusiUAS)

        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan saat menghitung:" & Environment.NewLine & ex.Message,
                            "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    ' =================================================================
    ' EVENT TOMBOL RESET
    ' =================================================================
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        ResetForm()
    End Sub

    ' =================================================================
    ' EVENT TOMBOL KELUAR
    ' =================================================================
    Private Sub btnKeluar_Click(sender As Object, e As EventArgs) Handles btnKeluar.Click

        Dim jawaban As DialogResult = MessageBox.Show(
            "Apakah ingin keluar dari aplikasi?", "Konfirmasi",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If jawaban = DialogResult.Yes Then
            Me.Close()
        End If

    End Sub

    ' =================================================================
    ' VALIDASI INPUT
    ' Mengembalikan True kalau semua input benar.
    ' =================================================================
    Private Function ValidasiInput(ByRef jumlahTugas As Integer,
                                   ByRef nilaiUTS As Double,
                                   ByRef nilaiUAS As Double) As Boolean

        ' Nama mahasiswa tidak boleh kosong
        If String.IsNullOrWhiteSpace(txtNama.Text) Then
            TampilkanPesan("Nama mahasiswa belum diisi.", txtNama)
            Return False
        End If

        ' Jumlah tugas dihitung dari kotak tugas yang dicentang
        jumlahTugas = HitungTugasTerkumpul()
        If jumlahTugas > AturanNilai.MAKS_TUGAS Then
            TampilkanPesan("Jumlah tugas tidak boleh lebih dari " &
                           AturanNilai.MAKS_TUGAS.ToString() & ".", chkTugas1)
            Return False
        End If

        ' Nilai UTS harus 0 sampai 100
        If nudUTS.Value < 0D Or nudUTS.Value > 100D Then
            TampilkanPesan("Nilai UTS harus antara 0 sampai 100.", nudUTS)
            Return False
        End If
        nilaiUTS = CDbl(nudUTS.Value)

        ' Nilai UAS harus 0 sampai 100
        If nudUAS.Value < 0D Or nudUAS.Value > 100D Then
            TampilkanPesan("Nilai UAS harus antara 0 sampai 100.", nudUAS)
            Return False
        End If
        nilaiUAS = CDbl(nudUAS.Value)

        Return True

    End Function

    Private Sub TampilkanPesan(pesan As String, kontrol As Control)
        MessageBox.Show(pesan, "Input Tidak Valid",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning)
        kontrol.Focus()
    End Sub

    ' =================================================================
    ' MENGEMBALIKAN FORM KE KEADAAN AWAL
    ' =================================================================
    Private Sub ResetForm()
        txtNama.Text = String.Empty
        SetSemuaTugas(False)
        TampilkanJumlahTugas()
        nudUTS.Value = 0
        nudUAS.Value = 0

        lblNilaiTugas.Text = "Kontribusi Tugas (bobot 50%)  : -"
        lblNilaiTugasFuzzy.Text = "Nilai Tugas Fuzzy (0-100)   : -"
        lblNilaiUTS.Text = "Nilai UTS                  : -"
        lblNilaiUAS.Text = "Nilai UAS                  : -"
        lblKontribusiUTS.Text = "Kontribusi UTS (bobot 20%)   : -"
        lblKontribusiUAS.Text = "Kontribusi UAS (bobot 30%)   : -"
        lblNilaiFuzzy.Text = "NILAI AKHIR (FUZZY)  : -"
        lblPredikat.Text = "PREDIKAT  : -"
        lblKeteranganRule.Text = "Rule aktif: -"

        dgvRule.Rows.Clear()
        rtbProses.Clear()
        rtbProses.Text = "Tekan tombol HITUNG untuk melihat proses fuzzy."

        txtNama.Focus()
    End Sub

    ' =================================================================
    ' MENYIAPKAN KOLOM TABEL RULE
    ' =================================================================
    Private Sub AturTabelRule()
        dgvRule.Columns.Clear()
        dgvRule.AutoGenerateColumns = False

        Dim c1 As New DataGridViewTextBoxColumn()
        c1.Name = COL_ID
        c1.HeaderText = "ID"
        c1.Width = 55
        dgvRule.Columns.Add(c1)

        Dim c2 As New DataGridViewTextBoxColumn()
        c2.Name = COL_IF
        c2.HeaderText = "Kondisi (IF)"
        c2.Width = 430
        dgvRule.Columns.Add(c2)

        Dim c3 As New DataGridViewTextBoxColumn()
        c3.Name = COL_THEN
        c3.HeaderText = "Konsekuen (THEN)"
        c3.Width = 140
        dgvRule.Columns.Add(c3)

        Dim c4 As New DataGridViewTextBoxColumn()
        c4.Name = COL_ALPHA
        c4.HeaderText = "Alpha-Predicate"
        c4.Width = 120
        dgvRule.Columns.Add(c4)
    End Sub

    ' =================================================================
    ' MENAMPILKAN HASIL PERHITUNGAN DI KOTAK HASIL
    ' =================================================================
    Private Sub TampilkanPerhitungan(jumlahTugas As Integer,
                                     kontribusiTugas As Double,
                                     nilaiTugasFuzzy As Double,
                                     nilaiUTS As Double,
                                     nilaiUAS As Double,
                                     kontribusiUTS As Double,
                                     kontribusiUAS As Double)

        lblNilaiTugas.Text = "Kontribusi Tugas (bobot 50%)  : " &
                             Fmt.DuaDesimal(kontribusiTugas) &
                             " poin dari " & Fmt.DuaDesimal(AturanNilai.MAKS_KONTRIBUSI_TUGAS) &
                             "  (" & jumlahTugas.ToString() & "/" & AturanNilai.MAKS_TUGAS.ToString() &
                             " tugas = " & Fmt.DuaDesimal(PersentaseTugas(jumlahTugas)) & " %)"

        lblNilaiTugasFuzzy.Text = "Nilai Tugas Fuzzy (0-100)   : " &
                                  Fmt.DuaDesimal(nilaiTugasFuzzy)

        lblNilaiUTS.Text = "Nilai UTS                  : " & Fmt.DuaDesimal(nilaiUTS)
        lblNilaiUAS.Text = "Nilai UAS                  : " & Fmt.DuaDesimal(nilaiUAS)

        lblKontribusiUTS.Text = "Kontribusi UTS (bobot 20%)   : " & Fmt.DuaDesimal(kontribusiUTS)
        lblKontribusiUAS.Text = "Kontribusi UAS (bobot 30%)   : " & Fmt.DuaDesimal(kontribusiUAS)

    End Sub

    ' =================================================================
    ' MENAMPILKAN NILAI AKHIR, PREDIKAT, DAN RULE AKTIF
    ' =================================================================
    Private Sub TampilkanHasilFuzzy(hasil As HasilProsesFuzzy)

        lblNilaiFuzzy.Text = "NILAI AKHIR (FUZZY)  : " & Fmt.DuaDesimal(hasil.NilaiAkhir)
        lblPredikat.Text = "PREDIKAT  : " & hasil.Predikat

        ' Rule dengan alpha paling besar, untuk keterangan singkat
        Dim alphaTertinggi As Double = 0.0
        Dim ruleTertinggi As String = "-"
        For Each r As AturanAktif In hasil.DaftarRule
            If r.Alpha > alphaTertinggi Then
                alphaTertinggi = r.Alpha
                ruleTertinggi = r.Id
            End If
        Next

        lblKeteranganRule.Text = "Rule aktif: " & hasil.JumlahRuleAktif().ToString() &
                                 " dari " & hasil.DaftarRule.Count.ToString() & " rule." &
                                 Environment.NewLine &
                                 "Alpha tertinggi: rule " & NamaRuleAman(ruleTertinggi) &
                                 " = " & Fmt.DuaDesimal(alphaTertinggi)
    End Sub

    ' Menghindari tampil kosong kalau tidak ada rule yang menyala
    Private Function NamaRuleAman(nama As String) As String
        If String.IsNullOrEmpty(nama) Then Return "-"
        Return nama
    End Function

    ' =================================================================
    ' MENGISI TABEL RULE YANG MENYALA
    ' =================================================================
    Private Sub TampilkanTabelRule(hasil As HasilProsesFuzzy)
        dgvRule.SuspendLayout()
        dgvRule.Rows.Clear()

        For Each r As AturanAktif In hasil.DaftarRule
            ' Hanya tampilkan rule yang benar-benar menyala (alpha > 0)
            If r.Alpha > 0 Then
                dgvRule.Rows.Add(r.Id,
                                 r.KondisiIF,
                                 r.KonsekuenTHEN,
                                 Fmt.DuaDesimal(r.Alpha))
            End If
        Next

        dgvRule.ResumeLayout()
    End Sub

    ' =================================================================
    ' MENAMPILKAN RINCIAN SELURUH LANGKAH FUZZY
    ' =================================================================
    Private Sub TampilkanProses(hasil As HasilProsesFuzzy,
                                jumlahTugas As Integer,
                                kontribusiTugas As Double,
                                nilaiTugasFuzzy As Double,
                                nilaiUTS As Double,
                                nilaiUAS As Double,
                                kontribusiUTS As Double,
                                kontribusiUAS As Double)

        rtbProses.Clear()

        ' ---- LANGKAH 1 : perhitungan nilai awal
        TambahBaris("LANGKAH 1 - PERHITUNGAN NILAI AWAL")
        TambahBaris("  Jumlah tugas dikumpulkan : " & jumlahTugas.ToString() & " dari " &
                    AturanNilai.MAKS_TUGAS.ToString() & " tugas")
        TambahBaris("  Persentase tugas         : (" & jumlahTugas.ToString() & " / " &
                    AturanNilai.MAKS_TUGAS.ToString() & ") x 100 = " &
                    Fmt.DuaDesimal(PersentaseTugas(jumlahTugas)) & " %")
        TambahBaris("  Kontribusi tugas (50%)  : " & Fmt.DuaDesimal(kontribusiTugas) &
                    " dari maksimal " & Fmt.DuaDesimal(AturanNilai.MAKS_KONTRIBUSI_TUGAS))
        TambahBaris("  Nilai tugas fuzzy        : (" &
                    Fmt.DuaDesimal(kontribusiTugas) & " / 50) x 100 = " &
                    Fmt.DuaDesimal(nilaiTugasFuzzy))
        TambahBaris("  Nilai UTS                : " & Fmt.DuaDesimal(nilaiUTS) &
                    "   kontribusi 20% = " & Fmt.DuaDesimal(kontribusiUTS))
        TambahBaris("  Nilai UAS                : " & Fmt.DuaDesimal(nilaiUAS) &
                    "   kontribusi 30% = " & Fmt.DuaDesimal(kontribusiUAS))
        TambahBaris("")

        ' ---- LANGKAH 2 : fuzzifikasi
        TambahBaris("LANGKAH 2 - FUZZIFIKASI (derajat keanggotaan 0..1)")
        TambahBaris("  Tugas : " & hasil.Tugas.ToString())
        TambahBaris("  UTS   : " & hasil.UTS.ToString())
        TambahBaris("  UAS   : " & hasil.UAS.ToString())
        TambahBaris("")

        ' ---- LANGKAH 3 : inferensi Mamdani
        TambahBaris("LANGKAH 3 - INFERENSI MAMDANI (AND = MIN, OR = MAX)")
        Dim adaRule As Boolean = False
        For Each r As AturanAktif In hasil.DaftarRule
            If r.Alpha > 0 Then
                adaRule = True
                TambahBaris("  " & r.Id.PadRight(5) & "alpha = " & Fmt.DuaDesimal(r.Alpha) &
                            "   " & r.KondisiIF & " " & r.KonsekuenTHEN)
            End If
        Next
        If Not adaRule Then
            TambahBaris("  (tidak ada rule yang menyala)")
        End If
        TambahBaris("")

        ' ---- LANGKAH 4 : agregasi
        TambahBaris("LANGKAH 4 - AGREGASI OUTPUT (MAX antar rule)")
        TambahBaris("  " & hasil.Agregasi.ToString())
        TambahBaris("")

        ' ---- LANGKAH 5 : defuzzifikasi
        TambahBaris("LANGKAH 5 - DEFUZZIFIKASI (CENTROID)")
        TambahBaris("  Nilai akhir fuzzy = " & Fmt.DuaDesimal(hasil.NilaiAkhir))
        TambahBaris("  Predikat         = " & hasil.Predikat)

        ' Kotak hasil di kanan juga ikut diperbarui
        TampilkanHasilFuzzy(hasil)

    End Sub

    Private Sub TambahBaris(isi As String)
        rtbProses.AppendText(isi & Environment.NewLine)
    End Sub

End Class