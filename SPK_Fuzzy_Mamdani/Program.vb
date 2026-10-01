Imports System.Windows.Forms

Module Program

    ' Titik awal aplikasi Windows Forms
    <STAThread>
    Sub Main()
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        Application.Run(New FormNilaiAkhir())
    End Sub

End Module