
Imports System.Globalization
Imports System.IO
Imports System.Management
Imports System.Net
Imports System.Reflection
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Threading
Imports Csv
Imports eService.ChromeDriverDownloader
Imports Microsoft.Win32
Imports OpenQA.Selenium
Imports OpenQA.Selenium.Chrome
Imports OpenQA.Selenium.Interactions
Imports OpenQA.Selenium.Support.Events
Imports OpenQA.Selenium.Support.Extensions
Imports OpenQA.Selenium.Support.UI


Public Class Form1


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")
    End Sub


    Public FilePath As String = ""

    Private Sub btn_start_Click(sender As Object, e As EventArgs) Handles btn_start.Click

        If FilePath = "" Then
            MessageBox.Show("Load input file")
            Exit Sub
        End If


        If BackgroundWorker1.IsBusy = False Then


            btn_start.BackColor = Color.LightGreen
            btn_start.Text = "Running"
            BackgroundWorker1.RunWorkerAsync()


        ElseIf btn_start.Text = "Running" Then

            btn_start.Text = "Stopping"
            btn_start.BackColor = Color.Salmon
            btn_start.ForeColor = Color.DarkRed
            btn_start.Enabled = False
            BackgroundWorker1.CancelAsync()

        End If

    End Sub


    Private Sub BackgroundWorker1_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged, BackgroundWorker2.ProgressChanged
        Dim args = DirectCast(e.UserState, Tuple(Of String, String))

        Dim Type As String = args.Item1

        If Type = "LOG" Then

            txt_log.AppendText("[ " & Now.ToString(“hh:mm:ss tt”) & " ]   " & args.Item2 & vbNewLine)
            txt_log.ScrollToCaret()

        ElseIf Type = "VALUES" Then

            ProgressBar1.Value = args.Item2

        ElseIf Type = "SUCCESS" Then

            txt_log.SelectionColor = Color.Green
            txt_log.AppendText("[ " & Now.ToString(“hh:mm:ss tt”) & " ]   " & args.Item2 & vbNewLine)
            txt_log.ScrollToCaret()

        ElseIf Type = "WARNING" Then

            txt_log.SelectionColor = Color.DarkGoldenrod
            txt_log.AppendText("[ " & Now.ToString(“hh:mm:ss tt”) & " ]   " & args.Item2 & vbNewLine)
            txt_log.ScrollToCaret()

        ElseIf Type = "ERROR" Then

            txt_log.SelectionColor = Color.Red
            txt_log.AppendText("[ " & Now.ToString(“hh:mm:ss tt”) & " ]   " & args.Item2 & vbNewLine)
            txt_log.ScrollToCaret()

        ElseIf Type = "STOP" Then

            btn_start.Enabled = True
            btn_start.Text = "Start"
            btn_start.BackColor = Color.Gainsboro
            btn_start.ForeColor = Color.Black

        End If
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Dim worker As System.ComponentModel.BackgroundWorker = DirectCast(sender, System.ComponentModel.BackgroundWorker)
        Dim row As Integer = 1



        worker.WorkerSupportsCancellation = True
        worker.WorkerReportsProgress = True
        Shell("taskkill /F /IM chromedriver.exe /T", 0)
        Shell("taskkill /F /IM chrome.exe /T", 0)
        System.Threading.Thread.Sleep(4000)

        'For hababa = 1 To 100
        '    worker.ReportProgress(0, New Tuple(Of String, String)("LOG", "Progress Test: " & hababa))
        '    worker.ReportProgress(0, New Tuple(Of String, String)("VALUES", hababa))
        '    System.Threading.Thread.Sleep(1000)
        'Next
        'worker.ReportProgress(0, New Tuple(Of String, String)("VALUES", 100))

        'If worker.CancellationPending = True Then
        '    worker.ReportProgress(0, New Tuple(Of String, String)("LOG", "Script stopped by user"))
        '    worker.ReportProgress(0, New Tuple(Of String, String)("STOP", ""))
        '    Exit Sub
        'End If


        Try
            Dim Servicecr As ChromeDriverService = ChromeDriverService.CreateDefaultService()
            Dim Optionscr = New ChromeOptions()
            Dim driver As IWebDriver
            Servicecr.HideCommandPromptWindow = True

            If box_background.Checked = True Then
                Optionscr.AddArgument("--headless")
                worker.ReportProgress(0, New Tuple(Of String, String)("WARNING", "Starting Chrome browser in headless mode.."))
            Else
                worker.ReportProgress(0, New Tuple(Of String, String)("WARNING", "Starting Chrome browser..."))
            End If
            Optionscr.AddArguments("--window-size=1920,1080")
            Optionscr.AddArgument("--start-maximized")
            Optionscr.AddArgument("user-data-dir=" & Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) & "\Google\Chrome\User Data")
            Optionscr.AddArgument("--profile-directory=Default")
            driver = New ChromeDriver(Servicecr, Optionscr)
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(6)



            worker.ReportProgress(0, New Tuple(Of String, String)("LOG", "Navigating to home page"))
            driver.Navigate().GoToUrl("https://eservices.rs.ge/Login.aspx")
            System.Threading.Thread.Sleep(3000)
            Dim CurrentUrl As String = driver.Url
            If CurrentUrl = "https://eservices.rs.ge/Login.aspx" Then
                worker.ReportProgress(0, New Tuple(Of String, String)("WARNING", "Login  required"))
                driver.FindElement(By.Id("testUserContainer")).Click()
                System.Threading.Thread.Sleep(2000)
                driver.FindElement(By.XPath("(//div[@id='testUserChoose']//div)[2]")).Click()
                System.Threading.Thread.Sleep(2000)
            End If


            Dim csv = File.ReadAllText(FilePath)
            worker.ReportProgress(0, New Tuple(Of String, String)("LOG", "Loaded " & CsvReader.ReadFromText(csv).Count & " rows of data"))
            For Each line In CsvReader.ReadFromText(csv)
                worker.ReportProgress(0, New Tuple(Of String, String)("LOG", "Processing row " & row))

                Dim Input1 As String = line(1)
                Dim Input2 As String = line(2)
                Dim Radio As Integer = line(3)
RETRY:

                Try



                    driver.Navigate().GoToUrl("https://decl.rs.ge/decls.aspx")
                    System.Threading.Thread.Sleep(3000)
                    Try
                        driver.FindElement(By.XPath("//div[@id='card']//div[text()='დეკლარაციები']")).Click()
                        System.Threading.Thread.Sleep(3000)
                    Catch ex As Exception
                    End Try
                    driver.FindElement(By.XPath("//h3[@id='hka1']")).Click()
                    System.Threading.Thread.Sleep(3000)
                    driver.FindElement(By.XPath("//td[@value='58']")).Click()
                    System.Threading.Thread.Sleep(3000)
                    Try
                        driver.FindElement(By.XPath("//img[@src='img/del.png']")).Click()
                        System.Threading.Thread.Sleep(2000)
                    Catch ex As Exception
                    End Try
                    driver.FindElement(By.Id("control_0_new")).Click()
                    System.Threading.Thread.Sleep(3000)
                    driver.FindElement(By.XPath("//input[@x_name='COL_15']")).SendKeys(Input1)
                    driver.FindElement(By.XPath("//input[@x_name='COL_17']")).SendKeys(Input2)
                    System.Threading.Thread.Sleep(3000)
                    If Radio = 1 Then
                        driver.FindElement(By.Id("control_254_18_t_0")).Click()
                    ElseIf Radio = 2 Then
                        driver.FindElement(By.Id("control_254_18_t_1")).Click()
                    ElseIf Radio = 3 Then
                        driver.FindElement(By.Id("control_254_18_t_3")).Click()
                    ElseIf Radio = 4 Then
                        driver.FindElement(By.Id("control_254_18_t_2")).Click()
                    End If

                    System.Threading.Thread.Sleep(2000)
                    driver.FindElement(By.XPath("//li[@class='next']")).Click()
                    System.Threading.Thread.Sleep(2000)
                    driver.FindElement(By.XPath("//li[@class='send']")).Click()
                    driver.SwitchTo().Alert().Accept()
                    System.Threading.Thread.Sleep(2000)
                    driver.SwitchTo().Alert().Accept()
                    System.Threading.Thread.Sleep(5000)
                    worker.ReportProgress(0, New Tuple(Of String, String)("SUCCESS", "Success"))
                    row += 1
                Catch ex As Exception
                    worker.ReportProgress(0, New Tuple(Of String, String)("ERROR", "Error in  row: " & row & " - retrying"))
                    worker.ReportProgress(0, New Tuple(Of String, String)("ERROR", ex.Message))
                    GoTo RETRY
                End Try


            Next



            worker.ReportProgress(0, New Tuple(Of String, String)("SUCCESS", "Script execution completed"))
            worker.ReportProgress(0, New Tuple(Of String, String)("STOP", ""))

        Catch ex As Exception
            worker.ReportProgress(0, New Tuple(Of String, String)("ERROR", "UNHANDLED ERROR"))
            worker.ReportProgress(0, New Tuple(Of String, String)("ERROR", ex.Message))
            worker.ReportProgress(0, New Tuple(Of String, String)("STOP", ""))
        End Try


    End Sub


    Private Sub btn_ChromeDriverUpd_Click(sender As Object, e As EventArgs) Handles btn_ChromeDriverUpd.Click
        If BackgroundWorker2.IsBusy = False Then
            BackgroundWorker2.RunWorkerAsync()
        End If
    End Sub

    Private Sub BackgroundWorker2_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker2.DoWork
        Dim worker As System.ComponentModel.BackgroundWorker = DirectCast(sender, System.ComponentModel.BackgroundWorker)
        worker.WorkerSupportsCancellation = True
        worker.WorkerReportsProgress = True

        worker.ReportProgress(0, New Tuple(Of String, String)("LOG", "Shutting down the Chrome environment"))
        Shell("taskkill /F /IM chromedriver.exe /T", 0)
        Shell("taskkill /F /IM chrome.exe /T", 0)

        Try
            worker.ReportProgress(0, New Tuple(Of String, String)("LOG", "Installing ChromeDriver"))
            Dim chromeDriverInstaller = New ChromeDriverInstaller()
            Dim chromeVersion = chromeDriverInstaller.GetChromeVersion()
            worker.ReportProgress(0, New Tuple(Of String, String)("WARNING", $"Chrome version {chromeVersion} detected"))
            chromeDriverInstaller.Install(chromeVersion)
            worker.ReportProgress(0, New Tuple(Of String, String)("SUCCESS", "ChromeDriver installed"))
        Catch ex As Exception
            worker.ReportProgress(0, New Tuple(Of String, String)("ERROR", ex.Message))
        End Try

    End Sub


    Private Sub lbl_copyright_Click(sender As Object, e As EventArgs) Handles lbl_copyright.Click
        Process.Start("https://www.webtask.solutions")
    End Sub

    Private Sub btn_load_Click(sender As Object, e As EventArgs) Handles btn_load.Click
        OpenFileDialog1.FileName = ""
        OpenFileDialog1.Title = "Open Csv file"
        OpenFileDialog1.Filter = "Csv Files|*.csv"
        Dim result As DialogResult = OpenFileDialog1.ShowDialog()
        If result = Windows.Forms.DialogResult.OK Then
            FilePath = OpenFileDialog1.FileName
        End If
    End Sub
End Class
