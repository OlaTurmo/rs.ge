# rs.ge

  This is a prof of consept. The consept is to be able to fill out a form on the rs.ge site and submit it. 
  This code fills out the form "Monthly income tax declaration of an individual with small business status"
  
  
// This first part finds and login with test user. This needs to be replaced with login for user

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

// This code opens a CSV file with data and finds, fills out and submits the form

            Dim csv = File.ReadAllText(FilePath)
            worker.ReportProgress(0, New Tuple(Of String, String)("LOG", "Loaded " & CsvReader.ReadFromText(csv).Count & " rows of data"))
            For Each line In CsvReader.ReadFromText(csv)
                worker.ReportProgress(0, New Tuple(Of String, String)("LOG", "Processing row " & row))

                Dim Input1 As String = line(1)
                Dim Input2 As String = line(2)
                Dim Radio As Integer = line(3)

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
