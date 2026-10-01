' =====================================================================
' DESAIN FORM (dibuat oleh Windows Forms Designer)
'
' File ini TIDAK perlu diedit manual. File FormNilaiAkhir.vb
' yang dipakai untuk menulis kode program.
' =====================================================================

Partial Class FormNilaiAkhir
    Inherits System.Windows.Forms.Form

    '''<summary>
    '''Form yang dipakai untuk menghitung nilai akhir mahasiswa
    '''menggunakan Logika Fuzzy Mamdani.
    '''</summary>
    Public Sub New()
        InitializeComponent()
    End Sub

    '''<summary>
    '''Membersihkan komponen yang dipakai aplikasi.
    '''</summary>
    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    ''' <summary>
    '''  Metode yang digunakan untuk mengatur properties semua kontrol.
    '''  Dibuat otomatis oleh Windows Forms Designer.
    ''' </summary>
    Private Sub InitializeComponent()
        Me.lblJudul1 = New System.Windows.Forms.Label()
        Me.lblJudul2 = New System.Windows.Forms.Label()
        Me.gbData = New System.Windows.Forms.GroupBox()
        Me.lblNama = New System.Windows.Forms.Label()
        Me.txtNama = New System.Windows.Forms.TextBox()
        Me.lblTugas = New System.Windows.Forms.Label()
        Me.chkTugas1 = New System.Windows.Forms.CheckBox()
        Me.chkTugas2 = New System.Windows.Forms.CheckBox()
        Me.chkTugas3 = New System.Windows.Forms.CheckBox()
        Me.chkTugas4 = New System.Windows.Forms.CheckBox()
        Me.chkTugas5 = New System.Windows.Forms.CheckBox()
        Me.chkTugas6 = New System.Windows.Forms.CheckBox()
        Me.chkTugas7 = New System.Windows.Forms.CheckBox()
        Me.chkTugas8 = New System.Windows.Forms.CheckBox()
        Me.chkTugas9 = New System.Windows.Forms.CheckBox()
        Me.chkTugas10 = New System.Windows.Forms.CheckBox()
        Me.lblJumlahTugas = New System.Windows.Forms.Label()
        Me.lblLabelUTS = New System.Windows.Forms.Label()
        Me.nudUTS = New System.Windows.Forms.NumericUpDown()
        Me.lblLabelUAS = New System.Windows.Forms.Label()
        Me.nudUAS = New System.Windows.Forms.NumericUpDown()
        Me.lblKetentuan = New System.Windows.Forms.Label()
        Me.btnHitung = New System.Windows.Forms.Button()
        Me.btnReset = New System.Windows.Forms.Button()
        Me.btnKeluar = New System.Windows.Forms.Button()
        Me.gbHasil = New System.Windows.Forms.GroupBox()
        Me.lblNilaiTugas = New System.Windows.Forms.Label()
        Me.lblNilaiTugasFuzzy = New System.Windows.Forms.Label()
        Me.lblNilaiUTS = New System.Windows.Forms.Label()
        Me.lblNilaiUAS = New System.Windows.Forms.Label()
        Me.lblKontribusiUTS = New System.Windows.Forms.Label()
        Me.lblKontribusiUAS = New System.Windows.Forms.Label()
        Me.lblNilaiFuzzy = New System.Windows.Forms.Label()
        Me.lblPredikat = New System.Windows.Forms.Label()
        Me.lblKeteranganRule = New System.Windows.Forms.Label()
        Me.gbProses = New System.Windows.Forms.GroupBox()
        Me.lblDaftarRule = New System.Windows.Forms.Label()
        Me.dgvRule = New System.Windows.Forms.DataGridView()
        Me.lblProses = New System.Windows.Forms.Label()
        Me.rtbProses = New System.Windows.Forms.RichTextBox()
        CType(Me.nudUTS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudUAS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvRule, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbData.SuspendLayout()
        Me.gbHasil.SuspendLayout()
        Me.gbProses.SuspendLayout()
        Me.SuspendLayout()
        ' 
        ' lblJudul1
        ' 
        Me.lblJudul1.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblJudul1.Font = New System.Drawing.Font("Segoe UI", 13.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblJudul1.Location = New System.Drawing.Point(0, 0)
        Me.lblJudul1.Name = "lblJudul1"
        Me.lblJudul1.Size = New System.Drawing.Size(1020, 32)
        Me.lblJudul1.TabIndex = 0
        Me.lblJudul1.Text = "SPK PENILAIAN NILAI AKHIR MATA KULIAH DSS"
        Me.lblJudul1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        ' 
        ' lblJudul2
        ' 
        Me.lblJudul2.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblJudul2.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblJudul2.Location = New System.Drawing.Point(0, 32)
        Me.lblJudul2.Name = "lblJudul2"
        Me.lblJudul2.Size = New System.Drawing.Size(1020, 28)
        Me.lblJudul2.TabIndex = 1
        Me.lblJudul2.Text = "MENGGUNAKAN LOGIKA FUZZY MAMDANI"
        Me.lblJudul2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        ' 
        ' gbData
        ' 
        Me.gbData.Controls.Add(Me.lblNama)
        Me.gbData.Controls.Add(Me.txtNama)
        Me.gbData.Controls.Add(Me.lblTugas)
        Me.gbData.Controls.Add(Me.chkTugas1)
        Me.gbData.Controls.Add(Me.chkTugas2)
        Me.gbData.Controls.Add(Me.chkTugas3)
        Me.gbData.Controls.Add(Me.chkTugas4)
        Me.gbData.Controls.Add(Me.chkTugas5)
        Me.gbData.Controls.Add(Me.chkTugas6)
        Me.gbData.Controls.Add(Me.chkTugas7)
        Me.gbData.Controls.Add(Me.chkTugas8)
        Me.gbData.Controls.Add(Me.chkTugas9)
        Me.gbData.Controls.Add(Me.chkTugas10)
        Me.gbData.Controls.Add(Me.lblJumlahTugas)
        Me.gbData.Controls.Add(Me.lblLabelUTS)
        Me.gbData.Controls.Add(Me.nudUTS)
        Me.gbData.Controls.Add(Me.lblLabelUAS)
        Me.gbData.Controls.Add(Me.nudUAS)
        Me.gbData.Controls.Add(Me.lblKetentuan)
        Me.gbData.Controls.Add(Me.btnHitung)
        Me.gbData.Controls.Add(Me.btnReset)
        Me.gbData.Controls.Add(Me.btnKeluar)
        Me.gbData.Location = New System.Drawing.Point(12, 66)
        Me.gbData.Name = "gbData"
        Me.gbData.Size = New System.Drawing.Size(460, 340)
        Me.gbData.TabIndex = 2
        Me.gbData.TabStop = False
        Me.gbData.Text = "DATA MAHASISWA"
        ' 
        ' lblNama
        ' 
        Me.lblNama.AutoSize = True
        Me.lblNama.Location = New System.Drawing.Point(20, 32)
        Me.lblNama.Name = "lblNama"
        Me.lblNama.Size = New System.Drawing.Size(105, 17)
        Me.lblNama.TabIndex = 0
        Me.lblNama.Text = "Nama Mahasiswa"
        ' 
        ' txtNama
        ' 
        Me.txtNama.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.txtNama.Location = New System.Drawing.Point(170, 29)
        Me.txtNama.Name = "txtNama"
        Me.txtNama.Size = New System.Drawing.Size(265, 26)
        Me.txtNama.TabIndex = 1
        ' 
        ' lblTugas
        ' 
        Me.lblTugas.AutoSize = True
        Me.lblTugas.Location = New System.Drawing.Point(20, 66)
        Me.lblTugas.Name = "lblTugas"
        Me.lblTugas.Size = New System.Drawing.Size(304, 17)
        Me.lblTugas.TabIndex = 2
        Me.lblTugas.Text = "Tugas yang dikumpulkan (centang nomor tugas):"
        ' 
        ' chkTugas1 sampai chkTugas10
        ' Satu kotak centang untuk setiap tugas, 5 kotak per baris
        ' 
        Me.chkTugas1.Location = New System.Drawing.Point(24, 92)
        Me.chkTugas1.Name = "chkTugas1"
        Me.chkTugas1.Size = New System.Drawing.Size(78, 24)
        Me.chkTugas1.TabIndex = 3
        Me.chkTugas1.Text = "1"
        Me.chkTugas1.UseVisualStyleBackColor = True
        Me.chkTugas2.Location = New System.Drawing.Point(110, 92)
        Me.chkTugas2.Name = "chkTugas2"
        Me.chkTugas2.Size = New System.Drawing.Size(78, 24)
        Me.chkTugas2.TabIndex = 4
        Me.chkTugas2.Text = "2"
        Me.chkTugas2.UseVisualStyleBackColor = True
        Me.chkTugas3.Location = New System.Drawing.Point(196, 92)
        Me.chkTugas3.Name = "chkTugas3"
        Me.chkTugas3.Size = New System.Drawing.Size(78, 24)
        Me.chkTugas3.TabIndex = 5
        Me.chkTugas3.Text = "3"
        Me.chkTugas3.UseVisualStyleBackColor = True
        Me.chkTugas4.Location = New System.Drawing.Point(282, 92)
        Me.chkTugas4.Name = "chkTugas4"
        Me.chkTugas4.Size = New System.Drawing.Size(78, 24)
        Me.chkTugas4.TabIndex = 6
        Me.chkTugas4.Text = "4"
        Me.chkTugas4.UseVisualStyleBackColor = True
        Me.chkTugas5.Location = New System.Drawing.Point(368, 92)
        Me.chkTugas5.Name = "chkTugas5"
        Me.chkTugas5.Size = New System.Drawing.Size(78, 24)
        Me.chkTugas5.TabIndex = 7
        Me.chkTugas5.Text = "5"
        Me.chkTugas5.UseVisualStyleBackColor = True
        Me.chkTugas6.Location = New System.Drawing.Point(24, 120)
        Me.chkTugas6.Name = "chkTugas6"
        Me.chkTugas6.Size = New System.Drawing.Size(78, 24)
        Me.chkTugas6.TabIndex = 8
        Me.chkTugas6.Text = "6"
        Me.chkTugas6.UseVisualStyleBackColor = True
        Me.chkTugas7.Location = New System.Drawing.Point(110, 120)
        Me.chkTugas7.Name = "chkTugas7"
        Me.chkTugas7.Size = New System.Drawing.Size(78, 24)
        Me.chkTugas7.TabIndex = 9
        Me.chkTugas7.Text = "7"
        Me.chkTugas7.UseVisualStyleBackColor = True
        Me.chkTugas8.Location = New System.Drawing.Point(196, 120)
        Me.chkTugas8.Name = "chkTugas8"
        Me.chkTugas8.Size = New System.Drawing.Size(78, 24)
        Me.chkTugas8.TabIndex = 10
        Me.chkTugas8.Text = "8"
        Me.chkTugas8.UseVisualStyleBackColor = True
        Me.chkTugas9.Location = New System.Drawing.Point(282, 120)
        Me.chkTugas9.Name = "chkTugas9"
        Me.chkTugas9.Size = New System.Drawing.Size(78, 24)
        Me.chkTugas9.TabIndex = 11
        Me.chkTugas9.Text = "9"
        Me.chkTugas9.UseVisualStyleBackColor = True
        Me.chkTugas10.Location = New System.Drawing.Point(368, 120)
        Me.chkTugas10.Name = "chkTugas10"
        Me.chkTugas10.Size = New System.Drawing.Size(78, 24)
        Me.chkTugas10.TabIndex = 12
        Me.chkTugas10.Text = "10"
        Me.chkTugas10.UseVisualStyleBackColor = True
        ' 
        ' lblJumlahTugas
        ' 
        Me.lblJumlahTugas.AutoSize = True
        Me.lblJumlahTugas.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblJumlahTugas.ForeColor = System.Drawing.Color.FromArgb(0, 90, 158)
        Me.lblJumlahTugas.Location = New System.Drawing.Point(20, 300)
        Me.lblJumlahTugas.Name = "lblJumlahTugas"
        Me.lblJumlahTugas.Size = New System.Drawing.Size(330, 17)
        Me.lblJumlahTugas.TabIndex = 22
        Me.lblJumlahTugas.Text = "Tugas terkumpul: 0 dari 10 tugas (0,00 %)"
        ' 
        ' lblLabelUTS
        ' 
        Me.lblLabelUTS.AutoSize = True
        Me.lblLabelUTS.Location = New System.Drawing.Point(20, 150)
        Me.lblLabelUTS.Name = "lblLabelUTS"
        Me.lblLabelUTS.Size = New System.Drawing.Size(88, 17)
        Me.lblLabelUTS.TabIndex = 14
        Me.lblLabelUTS.Text = "Nilai UTS (0-100)"
        ' 
        ' nudUTS
        ' 
        Me.nudUTS.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.nudUTS.Location = New System.Drawing.Point(170, 150)
        Me.nudUTS.Maximum = 100D
        Me.nudUTS.Minimum = 0D
        Me.nudUTS.Name = "nudUTS"
        Me.nudUTS.Size = New System.Drawing.Size(100, 26)
        Me.nudUTS.TabIndex = 15
        Me.nudUTS.Value = 0D
        ' 
        ' lblLabelUAS
        ' 
        Me.lblLabelUAS.AutoSize = True
        Me.lblLabelUAS.Location = New System.Drawing.Point(20, 184)
        Me.lblLabelUAS.Name = "lblLabelUAS"
        Me.lblLabelUAS.Size = New System.Drawing.Size(91, 17)
        Me.lblLabelUAS.TabIndex = 16
        Me.lblLabelUAS.Text = "Nilai UAS (0-100)"
        ' 
        ' nudUAS
        ' 
        Me.nudUAS.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.nudUAS.Location = New System.Drawing.Point(170, 184)
        Me.nudUAS.Maximum = 100D
        Me.nudUAS.Minimum = 0D
        Me.nudUAS.Name = "nudUAS"
        Me.nudUAS.Size = New System.Drawing.Size(100, 26)
        Me.nudUAS.TabIndex = 17
        Me.nudUAS.Value = 0D
        ' 
        ' lblKetentuan
        ' 
        Me.lblKetentuan.ForeColor = System.Drawing.Color.DimGray
        Me.lblKetentuan.Location = New System.Drawing.Point(20, 218)
        Me.lblKetentuan.Name = "lblKetentuan"
        Me.lblKetentuan.Size = New System.Drawing.Size(415, 34)
        Me.lblKetentuan.TabIndex = 18
        Me.lblKetentuan.Text = "Ketentuan tugas dari dosen: 10 = 50 poin, 8 = 45 poin," & ControlChars.CrLf & "7 = 40 poin, 0 = 0 poin. Bobot: Tugas 50%, UTS 20%, UAS 30%"
        ' 
        ' btnHitung
        ' 
        Me.btnHitung.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.btnHitung.Location = New System.Drawing.Point(18, 258)
        Me.btnHitung.Name = "btnHitung"
        Me.btnHitung.Size = New System.Drawing.Size(130, 36)
        Me.btnHitung.TabIndex = 19
        Me.btnHitung.Text = "HITUNG"
        Me.btnHitung.UseVisualStyleBackColor = True
        ' 
        ' btnReset
        ' 
        Me.btnReset.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.btnReset.Location = New System.Drawing.Point(162, 258)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(130, 36)
        Me.btnReset.TabIndex = 20
        Me.btnReset.Text = "RESET"
        Me.btnReset.UseVisualStyleBackColor = True
        ' 
        ' btnKeluar
        ' 
        Me.btnKeluar.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.btnKeluar.Location = New System.Drawing.Point(306, 258)
        Me.btnKeluar.Name = "btnKeluar"
        Me.btnKeluar.Size = New System.Drawing.Size(130, 36)
        Me.btnKeluar.TabIndex = 21
        Me.btnKeluar.Text = "KELUAR"
        Me.btnKeluar.UseVisualStyleBackColor = True
        ' 
        ' gbHasil
        ' 
        Me.gbHasil.Controls.Add(Me.lblNilaiTugas)
        Me.gbHasil.Controls.Add(Me.lblNilaiTugasFuzzy)
        Me.gbHasil.Controls.Add(Me.lblNilaiUTS)
        Me.gbHasil.Controls.Add(Me.lblNilaiUAS)
        Me.gbHasil.Controls.Add(Me.lblKontribusiUTS)
        Me.gbHasil.Controls.Add(Me.lblKontribusiUAS)
        Me.gbHasil.Controls.Add(Me.lblNilaiFuzzy)
        Me.gbHasil.Controls.Add(Me.lblPredikat)
        Me.gbHasil.Controls.Add(Me.lblKeteranganRule)
        Me.gbHasil.Location = New System.Drawing.Point(484, 66)
        Me.gbHasil.Name = "gbHasil"
        Me.gbHasil.Size = New System.Drawing.Size(524, 340)
        Me.gbHasil.TabIndex = 3
        Me.gbHasil.TabStop = False
        Me.gbHasil.Text = "HASIL PERHITUNGAN"
        ' 
        ' lblNilaiTugas
        ' 
        Me.lblNilaiTugas.AutoSize = True
        Me.lblNilaiTugas.Location = New System.Drawing.Point(20, 30)
        Me.lblNilaiTugas.Name = "lblNilaiTugas"
        Me.lblNilaiTugas.Size = New System.Drawing.Size(280, 17)
        Me.lblNilaiTugas.TabIndex = 0
        Me.lblNilaiTugas.Text = "Kontribusi Tugas (bobot 50%)  : -"
        ' 
        ' lblNilaiTugasFuzzy
        ' 
        Me.lblNilaiTugasFuzzy.AutoSize = True
        Me.lblNilaiTugasFuzzy.Location = New System.Drawing.Point(20, 60)
        Me.lblNilaiTugasFuzzy.Name = "lblNilaiTugasFuzzy"
        Me.lblNilaiTugasFuzzy.Size = New System.Drawing.Size(297, 17)
        Me.lblNilaiTugasFuzzy.TabIndex = 1
        Me.lblNilaiTugasFuzzy.Text = "Nilai Tugas Fuzzy (0-100)   : -"
        ' 
        ' lblNilaiUTS
        ' 
        Me.lblNilaiUTS.AutoSize = True
        Me.lblNilaiUTS.Location = New System.Drawing.Point(20, 90)
        Me.lblNilaiUTS.Name = "lblNilaiUTS"
        Me.lblNilaiUTS.Size = New System.Drawing.Size(213, 17)
        Me.lblNilaiUTS.TabIndex = 2
        Me.lblNilaiUTS.Text = "Nilai UTS                  : -"
        ' 
        ' lblNilaiUAS
        ' 
        Me.lblNilaiUAS.AutoSize = True
        Me.lblNilaiUAS.Location = New System.Drawing.Point(20, 120)
        Me.lblNilaiUAS.Name = "lblNilaiUAS"
        Me.lblNilaiUAS.Size = New System.Drawing.Size(213, 17)
        Me.lblNilaiUAS.TabIndex = 3
        Me.lblNilaiUAS.Text = "Nilai UAS                  : -"
        ' 
        ' lblKontribusiUTS
        ' 
        Me.lblKontribusiUTS.AutoSize = True
        Me.lblKontribusiUTS.Location = New System.Drawing.Point(20, 150)
        Me.lblKontribusiUTS.Name = "lblKontribusiUTS"
        Me.lblKontribusiUTS.Size = New System.Drawing.Size(283, 17)
        Me.lblKontribusiUTS.TabIndex = 4
        Me.lblKontribusiUTS.Text = "Kontribusi UTS (bobot 20%)   : -"
        ' 
        ' lblKontribusiUAS
        ' 
        Me.lblKontribusiUAS.AutoSize = True
        Me.lblKontribusiUAS.Location = New System.Drawing.Point(20, 180)
        Me.lblKontribusiUAS.Name = "lblKontribusiUAS"
        Me.lblKontribusiUAS.Size = New System.Drawing.Size(283, 17)
        Me.lblKontribusiUAS.TabIndex = 5
        Me.lblKontribusiUAS.Text = "Kontribusi UAS (bobot 30%)   : -"
        ' 
        ' lblNilaiFuzzy
        ' 
        Me.lblNilaiFuzzy.AutoSize = True
        Me.lblNilaiFuzzy.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblNilaiFuzzy.ForeColor = System.Drawing.Color.FromArgb(0, 90, 158)
        Me.lblNilaiFuzzy.Location = New System.Drawing.Point(20, 212)
        Me.lblNilaiFuzzy.Name = "lblNilaiFuzzy"
        Me.lblNilaiFuzzy.Size = New System.Drawing.Size(345, 29)
        Me.lblNilaiFuzzy.TabIndex = 6
        Me.lblNilaiFuzzy.Text = "NILAI AKHIR (FUZZY)  : -"
        ' 
        ' lblPredikat
        ' 
        Me.lblPredikat.AutoSize = True
        Me.lblPredikat.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblPredikat.ForeColor = System.Drawing.Color.FromArgb(0, 90, 158)
        Me.lblPredikat.Location = New System.Drawing.Point(20, 252)
        Me.lblPredikat.Name = "lblPredikat"
        Me.lblPredikat.Size = New System.Drawing.Size(269, 29)
        Me.lblPredikat.TabIndex = 7
        Me.lblPredikat.Text = "PREDIKAT  : -"
        ' 
        ' lblKeteranganRule
        ' 
        Me.lblKeteranganRule.ForeColor = System.Drawing.Color.DimGray
        Me.lblKeteranganRule.Location = New System.Drawing.Point(20, 290)
        Me.lblKeteranganRule.Name = "lblKeteranganRule"
        Me.lblKeteranganRule.Size = New System.Drawing.Size(485, 40)
        Me.lblKeteranganRule.TabIndex = 8
        Me.lblKeteranganRule.Text = "Rule aktif: -"
        ' 
        ' gbProses
        ' 
        Me.gbProses.Controls.Add(Me.lblDaftarRule)
        Me.gbProses.Controls.Add(Me.dgvRule)
        Me.gbProses.Controls.Add(Me.lblProses)
        Me.gbProses.Controls.Add(Me.rtbProses)
        Me.gbProses.Location = New System.Drawing.Point(12, 414)
        Me.gbProses.Name = "gbProses"
        Me.gbProses.Size = New System.Drawing.Size(996, 428)
        Me.gbProses.TabIndex = 4
        Me.gbProses.TabStop = False
        Me.gbProses.Text = "PROSES FUZZY MAMDANI"
        ' 
        ' lblDaftarRule
        ' 
        Me.lblDaftarRule.AutoSize = True
        Me.lblDaftarRule.Location = New System.Drawing.Point(16, 26)
        Me.lblDaftarRule.Name = "lblDaftarRule"
        Me.lblDaftarRule.Size = New System.Drawing.Size(270, 17)
        Me.lblDaftarRule.TabIndex = 0
        Me.lblDaftarRule.Text = "RULE YANG MENYALA BESERTA ALPHA-PREDICATE"
        ' 
        ' dgvRule
        ' 
        Me.dgvRule.AllowUserToAddRows = False
        Me.dgvRule.AllowUserToDeleteRows = False
        Me.dgvRule.AllowUserToResizeRows = False
        Me.dgvRule.AutoGenerateColumns = False
        Me.dgvRule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvRule.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvRule.Location = New System.Drawing.Point(16, 50)
        Me.dgvRule.MultiSelect = False
        Me.dgvRule.Name = "dgvRule"
        Me.dgvRule.ReadOnly = True
        Me.dgvRule.RowHeadersVisible = False
        Me.dgvRule.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvRule.Size = New System.Drawing.Size(964, 215)
        Me.dgvRule.TabIndex = 1
        ' 
        ' lblProses
        ' 
        Me.lblProses.AutoSize = True
        Me.lblProses.Location = New System.Drawing.Point(16, 278)
        Me.lblProses.Name = "lblProses"
        Me.lblProses.Size = New System.Drawing.Size(239, 17)
        Me.lblProses.TabIndex = 2
        Me.lblProses.Text = "RINCIAN LANGKAH PEMROSESAN"
        ' 
        ' rtbProses
        ' 
        Me.rtbProses.BackColor = System.Drawing.SystemColors.Window
        Me.rtbProses.Font = New System.Drawing.Font("Consolas", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.rtbProses.Location = New System.Drawing.Point(16, 302)
        Me.rtbProses.Name = "rtbProses"
        Me.rtbProses.ReadOnly = True
        Me.rtbProses.Size = New System.Drawing.Size(964, 100)
        Me.rtbProses.TabIndex = 3
        Me.rtbProses.Text = "Tekan tombol HITUNG untuk melihat proses fuzzy."
        ' 
        ' FormNilaiAkhir
        ' 
        Me.AcceptButton = Me.btnHitung
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(1020, 854)
        Me.Controls.Add(Me.lblJudul2)
        Me.Controls.Add(Me.lblJudul1)
        Me.Controls.Add(Me.gbData)
        Me.Controls.Add(Me.gbHasil)
        Me.Controls.Add(Me.gbProses)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.MaximizeBox = True
        Me.MinimumSize = New System.Drawing.Size(1040, 893)
        Me.Name = "FormNilaiAkhir"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SPK Penilaian Nilai Akhir DSS - Logika Fuzzy Mamdani"
        Me.gbData.ResumeLayout(False)
        Me.gbData.PerformLayout()
        Me.gbHasil.ResumeLayout(False)
        Me.gbHasil.PerformLayout()
        Me.gbProses.ResumeLayout(False)
        Me.gbProses.PerformLayout()
        CType(Me.nudUTS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudUAS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvRule, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    ' =================================================================
    ' DAFTAR KONTROL
    ' Nama kontrol di bawah ini HARUS sama dengan yang dipakai
    ' di FormNilaiAkhir.vb.
    ' =================================================================
    Friend WithEvents lblJudul1 As System.Windows.Forms.Label
    Friend WithEvents lblJudul2 As System.Windows.Forms.Label

    Friend WithEvents gbData As System.Windows.Forms.GroupBox
    Friend WithEvents lblNama As System.Windows.Forms.Label
    Friend WithEvents txtNama As System.Windows.Forms.TextBox
    Friend WithEvents lblTugas As System.Windows.Forms.Label
    Friend WithEvents chkTugas1 As System.Windows.Forms.CheckBox
    Friend WithEvents chkTugas2 As System.Windows.Forms.CheckBox
    Friend WithEvents chkTugas3 As System.Windows.Forms.CheckBox
    Friend WithEvents chkTugas4 As System.Windows.Forms.CheckBox
    Friend WithEvents chkTugas5 As System.Windows.Forms.CheckBox
    Friend WithEvents chkTugas6 As System.Windows.Forms.CheckBox
    Friend WithEvents chkTugas7 As System.Windows.Forms.CheckBox
    Friend WithEvents chkTugas8 As System.Windows.Forms.CheckBox
    Friend WithEvents chkTugas9 As System.Windows.Forms.CheckBox
    Friend WithEvents chkTugas10 As System.Windows.Forms.CheckBox
    Friend WithEvents lblJumlahTugas As System.Windows.Forms.Label
    Friend WithEvents lblLabelUTS As System.Windows.Forms.Label
    Friend WithEvents nudUTS As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblLabelUAS As System.Windows.Forms.Label
    Friend WithEvents nudUAS As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblKetentuan As System.Windows.Forms.Label

    Friend WithEvents btnHitung As System.Windows.Forms.Button
    Friend WithEvents btnReset As System.Windows.Forms.Button
    Friend WithEvents btnKeluar As System.Windows.Forms.Button

    Friend WithEvents gbHasil As System.Windows.Forms.GroupBox
    Friend WithEvents lblNilaiTugas As System.Windows.Forms.Label
    Friend WithEvents lblNilaiTugasFuzzy As System.Windows.Forms.Label
    Friend WithEvents lblNilaiUTS As System.Windows.Forms.Label
    Friend WithEvents lblNilaiUAS As System.Windows.Forms.Label
    Friend WithEvents lblKontribusiUTS As System.Windows.Forms.Label
    Friend WithEvents lblKontribusiUAS As System.Windows.Forms.Label
    Friend WithEvents lblNilaiFuzzy As System.Windows.Forms.Label
    Friend WithEvents lblPredikat As System.Windows.Forms.Label
    Friend WithEvents lblKeteranganRule As System.Windows.Forms.Label

    Friend WithEvents gbProses As System.Windows.Forms.GroupBox
    Friend WithEvents lblDaftarRule As System.Windows.Forms.Label
    Friend WithEvents dgvRule As System.Windows.Forms.DataGridView
    Friend WithEvents lblProses As System.Windows.Forms.Label
    Friend WithEvents rtbProses As System.Windows.Forms.RichTextBox

    Private components As System.ComponentModel.IContainer

End Class