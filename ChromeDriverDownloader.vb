Imports System
Imports System.Diagnostics
Imports System.IO
Imports System.IO.Compression
Imports System.Net
Imports System.Net.Http
Imports System.Reflection
Imports System.Threading.Tasks
Imports Microsoft.Win32

Namespace ChromeDriverDownloader
    Public Class ChromeDriverInstaller
        Private Shared ReadOnly httpClient As New HttpClient With {
            .BaseAddress = New Uri("https://chromedriver.storage.googleapis.com/")
        }

        Public Function Install() As Task
            Return Install(Nothing, False)
        End Function

        Public Function Install(ByVal chromeVersion As String) As Task
            Return Install(chromeVersion, True)
        End Function

        Public Function Install(ByVal forceDownload As Boolean) As Task
            Return Install(Nothing, forceDownload)
        End Function

        Public Async Function Install(ByVal chromeVersion As String, ByVal forceDownload As Boolean) As Task
            If chromeVersion Is Nothing Then
                chromeVersion = GetChromeVersion()
            End If

            chromeVersion = chromeVersion.Substring(0, chromeVersion.LastIndexOf("."c))
            Dim chromeDriverVersionResponse = Await httpClient.GetAsync($"LATEST_RELEASE_{chromeVersion}")

            If Not chromeDriverVersionResponse.IsSuccessStatusCode Then

                If chromeDriverVersionResponse.StatusCode = HttpStatusCode.NotFound Then
                    Throw New Exception($"ChromeDriver version not found for Chrome version {chromeVersion}")
                Else
                    Throw New Exception($"ChromeDriver version request failed with status code: {chromeDriverVersionResponse.StatusCode}, reason phrase: {chromeDriverVersionResponse.ReasonPhrase}")
                End If
            End If

            Dim chromeDriverVersion = Await chromeDriverVersionResponse.Content.ReadAsStringAsync()
            Dim zipName As String = "chromedriver_win32.zip"
            Dim driverName As String = "chromedriver.exe"
            Dim targetPath As String = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
            targetPath = Path.Combine(targetPath, driverName)

            If Not forceDownload AndAlso File.Exists(targetPath) Then

                Using process As Process = Process.Start(New ProcessStartInfo With {
                    .FileName = targetPath,
                    .Arguments = "--version",
                    .UseShellExecute = False,
                    .CreateNoWindow = True,
                    .RedirectStandardOutput = True,
                    .RedirectStandardError = True
                })
                    Dim existingChromeDriverVersion As String = Await process.StandardOutput.ReadToEndAsync()
                    Dim [error] As String = Await process.StandardError.ReadToEndAsync()
                    process.WaitForExit()
                    process.Kill()
                    existingChromeDriverVersion = existingChromeDriverVersion.Split(" "c)(1)

                    If chromeDriverVersion = existingChromeDriverVersion Then
                        Return
                    End If

                    If Not String.IsNullOrEmpty([error]) Then
                        Throw New Exception($"Failed to execute {driverName} --version")
                    End If
                End Using
            End If

            Dim driverZipResponse = Await httpClient.GetAsync($"{chromeDriverVersion}/{zipName}")

            If Not driverZipResponse.IsSuccessStatusCode Then
                Throw New Exception($"ChromeDriver download request failed with status code: {driverZipResponse.StatusCode}, reason phrase: {driverZipResponse.ReasonPhrase}")
            End If

            Using zipFileStream = Await driverZipResponse.Content.ReadAsStreamAsync()

                Using zipArchive = New ZipArchive(zipFileStream, ZipArchiveMode.Read)

                    Using chromeDriverWriter = New FileStream(targetPath, FileMode.Create)
                        Dim entry = zipArchive.GetEntry(driverName)

                        Using chromeDriverStream As Stream = entry.Open()
                            Await chromeDriverStream.CopyToAsync(chromeDriverWriter)
                        End Using
                    End Using
                End Using
            End Using
        End Function

        Public Function GetChromeVersion() As String
            Dim chromePath As String = CStr(Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\chrome.exe", Nothing, Nothing))
            If chromePath Is Nothing Then
                Throw New Exception("Google Chrome not found in registry")
            End If

            Dim fileVersionInfo As FileVersionInfo = FileVersionInfo.GetVersionInfo(chromePath)
            Return fileVersionInfo.FileVersion
        End Function
    End Class
End Namespace
