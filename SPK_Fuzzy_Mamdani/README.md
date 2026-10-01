# SPK Penilaian Nilai Akhir Mata Kuliah DSS — Logika Fuzzy Mamdani

Aplikasi desktop **Visual Basic .NET (Windows Forms)** untuk menghitung nilai akhir
mahasiswa menggunakan **Logika Fuzzy Mamdani**, tanpa library fuzzy eksternal.

| Item | Keterangan |
|---|---|
| Bahasa | Visual Basic .NET |
| Platform | Windows Forms (bukan WPF, bukan web) |
| Target Framework | `net10.0-windows` |
| Nama Project | `SPK_Fuzzy_Mamdani` |
| Entry Point | `SPK_Fuzzy_Mamdani.Program` |
| Basis Data | Tidak ada (tanpa database) |
| Library Fuzzy | Tidak ada — semua dihitung manual |

---

## 1. Ringkasan

Nilai akhir mahasiswa ditentukan dari tiga masukan:

| Masukan | Bobot | Keterangan |
|---|---|---|
| Tugas | 50% | Dihitung dari kotak centang per nomor tugas (1–10), lalu dirata-ratakan ke skala 0–100 |
| UTS | 20% | Nilai 0–100 |
| UAS | 30% | Nilai 0–100 |

Alur Mamdani yang dipakai:

```
Nilai mentah
   → normalisasi Tugas ke 0..100
   → FUZZIFIKASI  (hitung derajat keanggotaan μ tiap himpunan)
   → INFERENSI    (28 rule, AND = MIN, OR = MAX → alpha-predicate)
   → AGREGASI     (MAX antar rule per himpunan output, clipping α)
   → DEFUZZIFIKASI (centroid, langkah 0.01)
   → PREDIKAT     (Kurang / Cukup / Baik / Sangat Baik)
```

---

## 2. Struktur Project

```
SPK_Fuzzy_Mamdani/
├── SPK_Fuzzy_Mamdani.sln                  Solution Visual Studio
├── SPK_Fuzzy_Mamdani.vbproj               Konfigurasi project (.NET SDK style)
├── Program.vb                             Titik masuk aplikasi
├── AturanNilai.vb                         Bobot penilaian + konversi jumlah tugas
├── FungsiKeanggotaan.vb                   Fungsi Segitiga, Trapesium, Bahu Kiri, Bahu Kanan
├── Mamdani.vb                             Inti fuzzy: titik belonging, rule base, inferensi, defuzzifikasi
├── HasilProsesFuzzy.vb                    Struktur data hasil (Enum + Class)
├── FormNilaiAkhir.vb                      Kode program form (event, validasi, tampilan)
├── FormNilaiAkhir.Designer.vb             Definisi/layout seluruh kontrol
├── README.md                              Dokumen ini
└── bin/ , obj/                            Hasil build
```

### Penjelasan tiap file

| File | Isi |
|---|---|
| `Program.vb` | Menjalankan `Application.Run` dengan `FormNilaiAkhir` sebagai form utama. |
| `AturanNilai.vb` | Konstanta bobot (50/20/30) dan fungsi `HitungKontribusiTugas` yang menerjemahkan jumlah tugas menjadi poin. **Semua angka di sini paling mudah diedit.** |
| `FungsiKeanggotaan.vb` | Empat bentuk dasar: `Segitiga`, `Trapesium`, `BahuKiri`, `BahuKanan`, semuanya mengembalikan derajat 0..1. |
| `Mamdani.vb` | Titik belonging, fungsi keanggotaan 3 variabel input + 4 variabel output, 28 rule, inferensi, agregasi, centroid, predikat. |
| `HasilProsesFuzzy.vb` | `Enum HimpunanOutput`, `Class DerajatInput`, `Class DerajatOutput`, `Class AturanAktif`, `Class HasilProsesFuzzy`. |
| `FormNilaiAkhir.vb` | Validasi input, pemanggilan engine, pengisian label, tabel rule, rincian proses, tombol RESET/KELUAR. |
| `FormNilaiAkhir.Designer.vb` | Letak, ukuran, dan teks semua kontrol. Tidak perlu diedit manual. |

---

## 3. Cara Menjalankan

### 3.1 Visual Studio Community

1. Buka **Visual Studio Community**.
2. Pilih **Open a project or solution** → arahkan ke `SPK_Fuzzy_Mamdani.sln`.
3. Tunggu *restore* NuGet selesai.
4. Pastikan project `SPK_Fuzzy_Mamdani` menjadi *startup project* (klik kanan → **Set as Startup Project**).
5. Tekan **Ctrl + F5** (*Start Without Debugging*) atau **F5**.
6. Jendela aplikasi entitled **"SPK Penilaian Nilai Akhir DSS - Logika Fuzzy Mamdani"** akan tampil.

> **Catatan versi framework.** Project ini memakai `net10.0-windows` karena itu yang
> terpasang pada mesin pengembangan. Jika Visual Studio Anda lebih lama dan muncul
> pesan *framework tidak ditemukan*, buka `SPK_Fuzzy_Mamdani.vbproj`, ubah
> `<TargetFramework>` menjadi versi yang tersedia di mesin Anda
> (misalnya `net8.0-windows`), lalu **Build → Clean Solution** dan build ulang.
> Kode program tidak perlu diubah sedikit pun.

### 3.2 Command Line

```powershell
# Build
dotnet build SPK_Fuzzy_Mamdani.sln

# Jalankan
dotnet run --project SPK_Fuzzy_Mamdani.vbproj
```

---

## 4. Daftar Kontrol Form

| Nama Kontrol | Tipe | Fungsi |
|---|---|---|
| `lblJudul1` | Label | Judul utama aplikasi |
| `lblJudul2` | Label | Sub judul "Menggunakan Logika Fuzzy Mamdani" |
| `gbData` | GroupBox | Kelompok input data mahasiswa |
| `lblNama` | Label | Judul "Nama Mahasiswa" |
| `txtNama` | TextBox | Input nama mahasiswa |
| `lblTugas` | Label | Judul "Tugas yang dikumpulkan (centang nomor tugas)" |
| `chkTugas1` … `chkTugas10` | CheckBox ×10 | **Satu kotak centang untuk setiap tugas.** Yang dicentang berarti tugas itu dikumpulkan |
| `lblJumlahTugas` | Label | Ringkasan jumlah dan persentase tugas, berubah realtime |
| `lblLabelUTS` | Label | Judul "Nilai UTS (0-100)" |
| `nudUTS` | NumericUpDown | Input nilai UTS, rentang 0–100 |
| `lblLabelUAS` | Label | Judul "Nilai UAS (0-100)" |
| `nudUAS` | NumericUpDown | Input nilai UAS, rentang 0–100 |
| `lblKetentuan` | Label | Penjelasan ketentuan nilai tugas dosen |
| `btnHitung` | Button | Menjalankan perhitungan fuzzy |
| `btnReset` | Button | Mengosongkan seluruh isian dan hasil |
| `btnKeluar` | Button | Menutup aplikasi |
| `gbHasil` | GroupBox | Kelompok hasil perhitungan |
| `lblNilaiTugas` | Label | Kontribusi tugas (bobot 50%) |
| `lblNilaiTugasFuzzy` | Label | Nilai tugas fuzzy (0–100) |
| `lblNilaiUTS` | Label | Nilai UTS |
| `lblNilaiUAS` | Label | Nilai UAS |
| `lblKontribusiUTS` | Label | Kontribusi UTS (bobot 20%) |
| `lblKontribusiUAS` | Label | Kontribusi UAS (bobot 30%) |
| `lblNilaiFuzzy` | Label | Nilai akhir fuzzy (hasil utama) |
| `lblPredikat` | Label | Predikat nilai akhir |
| `lblKeteranganRule` | Label | Jumlah rule menyala + alpha tertinggi |
| `gbProses` | GroupBox | Kelompok tabel dan rincian proses |
| `lblDaftarRule` | Label | Judul tabel rule |
| `dgvRule` | DataGridView | Tabel rule yang menyala + alpha-predicate |
| `lblProses` | Label | Judul rincian proses |
| `rtbProses` | RichTextBox | Rincian 5 langkah pemrosesan |

---

## 5. Konfigurasi Fuzzy

### 5.1 Fungsi Keanggotaan (Bentuk Dasar)

| Fungsi | Bentuk | Digunakan untuk |
|---|---|---|
| `BahuKiri(a, b)` | Bahu kiri monoton turun | Himpunan **Rendah** |
| `Trapesium(a, b, c, d)` | Trapesium | Himpunan **Sedang** |
| `BahuKanan(a, b)` | Bahu kanan monoton naik | Himpunan **Tinggi** |
| `Segitiga(a, b, c)` | Segitiga | Semua himpunan **output** |

### 5.2 Variabel Input (Tugas, UTS, UAS) — skala 0..100

| Himpunan | Bentuk | Titik | Keterangan |
|---|---|---|---|
| Rendah | Bahu kiri | **25 – 50** | nilai ≤ 25 → μ = 1; menurun sampai μ = 0 di angka 50 |
| Sedang | Trapesium | **30 – 50 – 65 – 90** |derajat penuh 50..65, ekor sampai 90 |
| Tinggi | Bahu kanan | **50 – 100** | nilai ≥ 100 → μ = 1; naik mulai angka 50 |

> **Mengapa trapesium Sedang dibuat sampai 90, bukan 70?**
> Jika himpunan Sedang diubah menjadi segitiga `30 – 50 – 70`, maka setiap angka
> 70 ke atas memiliki derajat Sedang = 0 sehingga hanya rule "Tinggi" yang menyala.
> Akibatnya semua nilai pada rentang 70–100 menghasilkan centroid yang nyaris sama
> dan tidak bisa dibedakan. Ekor sampai 90 dipakai agar nilai 70–90 tetap berbobot
> "Sedang". Bila dosen memang meminta himpunan Sedang berhenti di 70, cukup ubah
> konstanta `S_TITIK_D` di `Mamdani.vb` menjadi `70.0`.

Tiga variabel input memakai titik belonging yang **sama**, sehingga cukup satu
sistem koordinat untuk semuanya.

### 5.3 Variabel Output (Nilai Akhir) — skala 0..100

| Himpunan | Bentuk | Titik | Puncak | Rentang Predikat |
|---|---|---|---|---|
| Kurang | Segitiga | **0 – 30 – 60** | 30 | 0 – 59,99 |
| Cukup | Segitiga | **50 – 65 – 80** | 65 | 60 – 69,99 |
| Baik | Segitiga | **65 – 77 – 90** | 77 | 70 – 84,99 |
| Sangat Baik | Segitiga | **80 – 93 – 100** | 93 | 85 – 100 |

Puncak tiap segitiga sengaja diletakkan di **tengah** rentang predikatnya.
Karena titik tengah segitiga sama dengan rata-rata `(a + b + c) / 3`, maka ketika
hanya satu himpunan output yang menyala, hasil centroid jatuh tepat pada predikat
yang semestinya — sehingga predikat hasil fuzzy dan predikat berdasarkan ambang
angka saling menguatkan.

### 5.4 Input Tugas: Satu Kotak Centang per Tugas

Mahasiswa **tidak mengetik jumlah tugas**. Setiap tugas punya kotak centang
sendiri, `chkTugas1` sampai `chkTugas10`. Yang dicentang berarti tugas tersebut
benar-benar dikumpulkan. Program menghitung sendiri berapa kotak yang tercentang:

```vb
Private Function HitungTugasTerkumpul() As Integer
    Dim jumlah As Integer = 0
    For Each kotak As CheckBox In DaftarKotakTugas()
        If kotak.Checked Then jumlah += 1
    Next
    Return jumlah
End Function
```

Nomor tugas **tidak harus urut** — centang tugas 3, 7, dan 9 tetap dihitung
sebagai 3 tugas. Label `lblJumlahTugas` langsung ikut berubah begitu ada kotak
yang dicentang atau dilepas, sehingga mahasiswa bisa melihat persentasenya
sebelum menekan tombol HITUNG:

```
Persentase tugas = (jumlah kotak tercentang / 10) × 100
```

### 5.5 Konversi Jumlah Tugas → Nilai

Ketentuan dosen yang **wajib** dipertahankan:

| Jumlah Tugas | Kontribusi (poin) |
|---|---|
| 0 | 0 |
| 7 | 40 |
| 8 | 45 |
| 10 | 50 |

Untuk jumlah tugas yang tidak ditentukan dosen, dipakai **interpolasi linear**
dan angka ini boleh diubah:

| Jumlah Tugas | Poin | Cara |
|---|---|---|
| 1 | 5,71 | `j * 40 / 7` |
| 2 | 11,43 | `j * 40 / 7` |
| 3 | 17,14 | `j * 40 / 7` |
| 4 | 22,86 | `j * 40 / 7` |
| 5 | 28,57 | `j * 40 / 7` |
| 6 | 34,29 | `j * 40 / 7` |
| 9 | 47,50 | rata-rata antara 45 dan 50 |

Setelah diperoleh poin kontribusi, nilainya dinormalisasi agar masuk ke skala
0–100 yang sama dengan UTS dan UAS:

```
Nilai Tugas Fuzzy = (Kontribusi Tugas / 50) × 100
```

---

## 6. Rule Base (28 Rule)

### 6.1 Rule inti (R1–R8)

| ID | IF | THEN |
|---|---|---|
| R1 | Tugas Tinggi **AND** UTS Tinggi **AND** UAS Tinggi | Sangat Baik |
| R2 | Tugas Tinggi **AND** UTS Sedang **AND** UAS Tinggi | Baik |
| R3 | Tugas Sedang **AND** UTS Tinggi **AND** UAS Tinggi | Baik |
| R4 | Tugas Tinggi **AND** UTS Tinggi **AND** UAS Sedang | Baik |
| R5 | Tugas Tinggi **AND** UTS Sedang **AND** UAS Sedang | Cukup |
| R6 | Tugas Sedang **AND** UTS Sedang **AND** UAS Sedang | Cukup |
| R7 | Tugas Sedang **AND** UTS Tinggi **AND** UAS Sedang | Baik |
| R8 | Tugas Sedang **AND** UTS Sedang **AND** UAS Tinggi | Baik |

### 6.2 Rule pelengkap (R9–R27)

Seluruh kombinasi tiga variabel input yang mengandung **Rendah**, masing-masing
dipetakan ke **Kurang**:

| ID | IF | THEN |
|---|---|---|
| R9 | Tugas Rendah AND UTS Tinggi AND UAS Tinggi | Kurang |
| R10 | Tugas Rendah AND UTS Tinggi AND UAS Sedang | Kurang |
| R11 | Tugas Rendah AND UTS Tinggi AND UAS Rendah | Kurang |
| R12 | Tugas Rendah AND UTS Sedang AND UAS Tinggi | Kurang |
| R13 | Tugas Rendah AND UTS Sedang AND UAS Sedang | Kurang |
| R14 | Tugas Rendah AND UTS Sedang AND UAS Rendah | Kurang |
| R15 | Tugas Rendah AND UTS Rendah AND UAS Tinggi | Kurang |
| R16 | Tugas Rendah AND UTS Rendah AND UAS Sedang | Kurang |
| R17 | Tugas Rendah AND UTS Rendah AND UAS Rendah | Kurang |
| R18 | Tugas Sedang AND UTS Tinggi AND UAS Rendah | Kurang |
| R19 | Tugas Sedang AND UTS Sedang AND UAS Rendah | Kurang |
| R20 | Tugas Sedang AND UTS Rendah AND UAS Tinggi | Kurang |
| R21 | Tugas Sedang AND UTS Rendah AND UAS Sedang | Kurang |
| R22 | Tugas Sedang AND UTS Rendah AND UAS Rendah | Kurang |
| R23 | Tugas Tinggi AND UTS Tinggi AND UAS Rendah | Kurang |
| R24 | Tugas Tinggi AND UTS Sedang AND UAS Rendah | Kurang |
| R25 | Tugas Tinggi AND UTS Rendah AND UAS Tinggi | Kurang |
| R26 | Tugas Tinggi AND UTS Rendah AND UAS Sedang | Kurang |
| R27 | Tugas Tinggi AND UTS Rendah AND UAS Rendah | Kurang |

### 6.3 Rule agregasi OR (R28)

| ID | IF | THEN |
|---|---|---|
| R28 | Tugas Rendah **OR** UTS Rendah **OR** UAS Rendah | Kurang |

> Fungsi R28 adalah pengaman: meskipun seluruh kombinasi sudah tercakup R9–R27,
> rule ini memastikan himpunan **Kurang** tetap menyala selama ada satu saja input
> yang rendah, sehingga tidak pernah terjadi kondisi "tidak ada rule yang menyala".

---

## 7. Mekanisme Mamdani yang Digunakan

| Tahap | Cara yang dipakai |
|---|---|
| **Fuzzification** | `μ = FungsiKeanggotaan(...)` untuk 3 himpunan × 3 variabel input |
| **Inferensi (AND)** | `alpha = MIN(μ1, μ2, μ3)` |
| **Inferensi (OR)** | `alpha = MAX(μ1, μ2, μ3)` |
| **Implikasi** | Clipping: `μ_output = MIN(μ_output, alpha)` |
| **Agregasi** | `MAX` antar seluruh rule untuk setiap himpunan output |
| **Defuzzification** | Centroid numerik, `z = Σ(zᵢ·μᵢ) / Σμᵢ`, langkah `0.01` pada rentang 0–100 |
| **Predikat** | `< 60` Kurang, `< 70` Cukup, `< 85` Baik, selain itu Sangat Baik |

**Dua hal yang paling sering salah dan sudah ditangani di program ini:**

1. **Clipping bukan perkalian.** Corte (potongan) himpunan output selalu
   `MIN(μ, α)`. Many orang keliru menulis `μ × α`, yang menghasilkan kurva
   tidak valid.
2. **Pembagian dengan nol.** Perhitungan centroid `Σ(zᵢ·μᵢ) / Σμᵢ` akan menghasilkan
   `NaN` atau `Infinity` apabila tidak ada satu pun himpunan output yang menyala
   (pada masukan 0,0,0). Program memeriksa `Σμᵢ <= 0` dan mengembalikan `0`.

---

## 8. Hasil Pengujian

Pengujian dilakukan dengan mengeksekusi kontrol form sungguhan
(mengisi `txtNama`, mencentang kotak tugas, mengisi `nudUTS` dan `nudUAS`,
lalu memantik `btnHitung`), bukan hanya menghitung ulang secara manual.

| No | Nama | Tugas dicentang | UTS | UAS | Persentase | Kontribusi Tugas | Nilai Tugas Fuzzy | Kontribusi UTS | Kontribusi UAS | Rule Menyala | **Nilai Akhir** | **Predikat** |
|---|---|---|---|---|---|---|---|---|---|---|---|---|
| 1 | Andi | 10 | 90 | 90 | 100 % | 50,00 | 100,00 | 18,00 | 27,00 | 1 | **90,93** | **Sangat Baik** |
| 2 | Budi | 8 | 75 | 80 | 80 % | 45,00 | 90,00 | 15,00 | 24,00 | 4 | **76,73** | **Baik** |
| 3 | Citra | 7 | 70 | 70 | 70 % | 40,00 | 80,00 | 14,00 | 21,00 | 8 | **74,25** | **Baik** |
| 4 | Deni | 0 | 50 | 50 | 0 % | 0,00 | 0,00 | 10,00 | 15,00 | 2 | **30,00** | **Kurang** |
| 5 | Batas Atas | 10 | 100 | 100 | 100 % | 50,00 | 100,00 | 20,00 | 30,00 | 1 | **91,00** | **Sangat Baik** |
| 6 | Batas Bawah | 0 | 0 | 0 | 0 % | 0,00 | 0,00 | 0,00 | 0,00 | 2 | **0,00** | **Kurang** |
| 7 | Rina | 3, 7, 9 (tidak berurutan) | 70 | 75 | 30 % | 17,14 | 34,29 | 14,00 | 22,50 | 4 | **38,90** | **Kurang** |

Pengujian tambahan sensitivitas (hanya nilai akhir):

| Nama | Tugas | UTS | UAS | Nilai Akhir | Predikat |
|---|---|---|---|---|---|
| Eko | 6 | 55 | 60 | 70,27 | Baik |
| Fajar | 9 | 88 | 90 | 81,18 | Baik |
| Gita | 4 | 45 | 50 | 42,50 | Kurang |
| Hana | 1 | 30 | 35 | 30,00 | Kurang |

### 8.1 Rincian proses lengkap — Studi Kasus Budi

```
LANGKAH 1 - PERHITUNGAN NILAI AWAL
  Jumlah tugas dikumpulkan : 8 dari 10 tugas
  Persentase tugas         : (8 / 10) x 100 = 80,00 %
  Kontribusi tugas (50%)  : 45,00 dari maksimal 50,00
  Nilai tugas fuzzy        : (45,00 / 50) x 100 = 90,00
  Nilai UTS                : 75,00   kontribusi 20% = 15,00
  Nilai UAS                : 80,00   kontribusi 30% = 24,00

LANGKAH 2 - FUZZIFIKASI (derajat keanggotaan 0..1)
  Tugas : Rendah = 0,00 | Sedang = 0,00 | Tinggi = 0,80
  UTS   : Rendah = 0,00 | Sedang = 0,60 | Tinggi = 0,50
  UAS   : Rendah = 0,00 | Sedang = 0,40 | Tinggi = 0,60

LANGKAH 3 - INFERENSI MAMDANI (AND = MIN, OR = MAX)
  R1   alpha = 0,50   IF Tugas Tinggi AND UTS Tinggi AND UAS Tinggi THEN Sangat Baik
  R2   alpha = 0,60   IF Tugas Tinggi AND UTS Sedang AND UAS Tinggi THEN Baik
  R4   alpha = 0,40   IF Tugas Tinggi AND UTS Tinggi AND UAS Sedang THEN Baik
  R5   alpha = 0,40   IF Tugas Tinggi AND UTS Sedang AND UAS Sedang THEN Cukup

LANGKAH 4 - AGREGASI OUTPUT (MAX antar rule)
  Kurang = 0,00 | Cukup = 0,40 | Baik = 0,60 | SangatBaik = 0,50

LANGKAH 5 - DEFUZZIFIKASI (CENTROID)
  Nilai akhir fuzzy = 76,73
  Predikat         = Baik
```

Verifikasi manual VALUE UTS 75, NASAUAS 80, dan alpha R2 = `MIN(0.80, 0.60, 0.60) = 0.60`
sesuai dengan yang ditampilkan program.

### 8.2 Pengujian antarmuka dan validasi

| Skenario | Hasil |
|---|---|
| Nama dikosongkan | Pesan "Nama mahasiswa belum diisi" (tidak dihitung) |
| Nilai di luar 0–100 | Dibatang oleh `NumericUpDown`, tidak bisa diisi |
| Kotak centang tidak berurutan (3, 7, 9) | Tetap dihitung 3 tugas → 38,90 Kurang |
| Label jumlah tugas | Berubah realtime: 3 kotak → "3 dari 10 tugas (30,00 %)" |
| Tombol RESET | Semua checkbox tidak tercentang, isian, label, dan tabel rule kosong |
| Masukan 0/0/0 | Aman, menghasilkan 0,00 — tidak menghasilkan `NaN` |
| Tumpang tindih kontrol | Dicek otomatis: 16 kontrol di `gbData` tidak saling menimpa |
| Kontrol keluar group box | Dicek otomatis: semua kontrol muat dalam `gbData` 460×340 |

---

## 9. Daftar Screenshot yang Perlu Disiapkan

| No | Nama File Screenshot | Isi |
|---|---|---|
| 1 | `01-form-awal.png` | Form saat pertama dibuka, semua kotak tugas tidak tercentang |
| 2 | `02-kotak-tugas.png` | Bagian kotak centang tugas 1–10, sebagian sudah dicentang |
| 3 | `03-form-terisi.png` | Nama, UTS, UAS terisi dan tombol HITUNG siap ditekan |
| 4 | `04-hasil-andi.png` | Hasil Andi (10 tugas tercentang/90/90) → 90,93 Sangat Baik |
| 5 | `05-rule-menyala.png` | Tabel `dgvRule` berisi rule yang menyala beserta alpha |
| 6 | `06-rincian-proses.png` | Isi `rtbProses` yang menunjukkan 5 langkah pemrosesan |
| 7 | `07-tabel-kontribusi.png` | Bagian `gbHasil`: kontribusi tugas, UTS, UAS |
| 8 | `08-hasil-budi.png` | Hasil Budi (8/75/80) → 76,73 Baik beserta rule R1, R2, R4, R5 |
| 9 | `09-hasil-deni.png` | Hasil Deni (0/50/50) → 30,00 Kurang beserta R13 dan R28 |
| 10 | `10-validasi.png` | Pesan validasi saat nama dikosongkan |
| 11 | `11-tombol-reset.png` | Kondisi form setelah menekan RESET |

**Cara mengambil screenshot:** jalankan aplikasi dengan **F5**, atur window agar
seluruh form terlihat (bukan `Start Without Debugging` yang disables debug),
kemudian tekan **Print Screen** atau **Win + Shift + S** dan simpan ke folder
`Dokumentasi/`.

---

## 10. Laporan Temuan, Kendala, dan Keputusan Desain

### A. Temuan

1. **Ketentuan jumlah tugas belum lengkap.** Dosen hanya menetapkan 4 titik acuan
   (0, 7, 8, 10 tugas), padahal ada 10 tugas yang bisa dikumpulkan.
   Solusi: titik yang ditentukan dosen dipertahankan persis, selebihnya diisi
   interpolasi linear dan diberi komentar agar mudah diganti.

2. **Menuangkan "jumlah tugas" sebagai satu angka berisiko salah input.** Dosen
   bisa salah mengetik angka, dan program tidak punya cara tahu tugas mana saja
   yang benar-benar dikumpulkan. Solusi: tiap tugas punya kotak centang sendiri,
   sehingga yang dihitung benar-benar tugas yang dicentang, dan bisa diaudit
   satu per satu.

2. **Skala tidak seragam antar variabel.** Tugas maksimal hanya 50 poin, sedangkan
   UTS/UAS maksimal 100. Menormalkan tugas ke 0–100 diperlukan agar titik
   belonging dapat dipakai bersama oleh ketiga variabel.

3. **Nilai tugas 0 menghasilkan derajat 0 untuk semua himpunan**, sehingga harus
   ada jalur eksplisit menuju himpunan Kurang. Inilah alasan R28 (operator OR)
   dan R9–R27 perlu ditambahkan.

4. **Himpunan input "Sedang" menyebabkan tumpang tindih yang menyempit.** Jika
   himpunan Sedang hanya sampai 70, maka seluruh nilai ≥ 70 hanya menyalakan
   rule Tinggi dan rentang 70–100 menjadi tidak bisa dibedakan. Ekor sampai 90
   dipakai agar rentang 70–90 tetap menyalakan rule "Sedang".

5. **Titik tengah segitiga = rata-rata.** Menaruh puncak segitiga output di tengah
   rentang predikat membuat hasil centroid dan predikat ambang konsisten —
   sebuah sanity check yang murah.

### B. Kendala teknis yang pernah ditemui

| Masalah | Penyebab | Solusi |
|---|---|---|
| `error BC30002: Type 'Control' is not defined` | `System.Windows.Forms` belum di-`Imports` | Tambahkan `Imports System.Windows.Forms` |
| `error BC30451: 'InitializeComponent' is not declared` | Tubuh sub pada file designer ikut terkomentari | Tulis ulang `InitializeComponent` sebagai kode aktif |
| `error BC30518: Overload resolution failed` pada `NumericUpDown.Maximum` | Penulisan `New Decimal(New Object() {...})` tidak diterima compiler | Diganti literal desimal, contoh `Maximum = 100D` |
| `error BC30002` pada `Color.FromArgb` | `CType(0)` dengan satu argumen tidak valid di VB | Diganti `Color.FromArgb(0, 90, 158)` |
| Warning `WFO0003` | Setelan DPI ganda di `app.manifest` dan di properti project | Manifest dihapus, DPI diatur lewat `AutoScaleMode` |
| Warning `BC40000: vbNewLine is obsolete` | `vbNewLine` deprecated di .NET modern | Diganti `Environment.NewLine` |
| `error BC30026: 'End Sub' expected` | Klausa `Handles` ditulis terpotong setelah kata `Handles` | Ditulis ulang memakai line continuation ` _` |
| Kotak centang menabrak `nudUTS` | Baris checkbox kedua berakhir di y=144, UTS diletakkan di y=143 | UTS/UAS digeser ke y=150, tombol ke y=258 |

### C. Kesalahan yang paling mudah terjadi pada logika fuzzy

1. Mengganti clipping `MIN(μ, α)` dengan `μ × α`.
2. Menghapus operasi **AND = MIN** dan menggantinya dengan **rata-rata**.
3. Lupa mengecek `Σμ = 0` sebelum membaginya pada centroid.
4. Menyamakan titik belonging input dengan titik belonging output.
5. Menghitung predikat dari nilai sebelum defuzzifikasi.
6. Mengganti daftar checkbox dengan satu `NumericUpDown`, lalu kehilangan
   informasi tugas mana saja yang benar-benar dikumpulkan.

---

## 11. Catatan Teknis

Semua angka fuzzy terkumpul di bagian atas `Mamdani.vb` pada blok
`TITIK BELONGING INPUT` dan `TITIK BELONGING OUTPUT`, sedangkan seluruh aturan
nilai dan bobot terkumpul di `AturanNilai.vb`. Untuk mengubah konfigurasi fuzzy,
hanya **dua file tersebut** yang perlu disentuh — tidak ada perlu mengubah
`FormNilaiAkhir.vb` maupun `FungsiKeanggotaan.vb`.
