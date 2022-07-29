<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Formularz przesłania metodę dispose, aby wyczyścić listę składników.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wymagane przez Projektanta formularzy systemu Windows
    Private components As System.ComponentModel.IContainer

    'UWAGA: następująca procedura jest wymagana przez Projektanta formularzy systemu Windows
    'Możesz to modyfikować, używając Projektanta formularzy systemu Windows. 
    'Nie należy modyfikować za pomocą edytora kodu.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.btn_start = New System.Windows.Forms.Button()
        Me.txt_log = New System.Windows.Forms.RichTextBox()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.lbl_state = New System.Windows.Forms.Label()
        Me.lbl_log = New System.Windows.Forms.Label()
        Me.box_background = New System.Windows.Forms.CheckBox()
        Me.lbl_version = New System.Windows.Forms.Label()
        Me.btn_ChromeDriverUpd = New System.Windows.Forms.Button()
        Me.lbl_copyright = New System.Windows.Forms.Label()
        Me.BackgroundWorker2 = New System.ComponentModel.BackgroundWorker()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.btn_load = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btn_start
        '
        Me.btn_start.Font = New System.Drawing.Font("Segoe UI", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btn_start.Location = New System.Drawing.Point(109, 12)
        Me.btn_start.Name = "btn_start"
        Me.btn_start.Size = New System.Drawing.Size(87, 29)
        Me.btn_start.TabIndex = 0
        Me.btn_start.Text = "Start"
        Me.btn_start.UseVisualStyleBackColor = True
        '
        'txt_log
        '
        Me.txt_log.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txt_log.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txt_log.Location = New System.Drawing.Point(19, 234)
        Me.txt_log.Name = "txt_log"
        Me.txt_log.Size = New System.Drawing.Size(459, 191)
        Me.txt_log.TabIndex = 1
        Me.txt_log.Text = ""
        '
        'BackgroundWorker1
        '
        '
        'lbl_state
        '
        Me.lbl_state.Font = New System.Drawing.Font("Segoe UI", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbl_state.Location = New System.Drawing.Point(15, 447)
        Me.lbl_state.Name = "lbl_state"
        Me.lbl_state.Size = New System.Drawing.Size(152, 23)
        Me.lbl_state.TabIndex = 3
        Me.lbl_state.Text = "Label1"
        Me.lbl_state.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_log
        '
        Me.lbl_log.AutoSize = True
        Me.lbl_log.Font = New System.Drawing.Font("Segoe UI", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbl_log.Location = New System.Drawing.Point(14, 214)
        Me.lbl_log.Name = "lbl_log"
        Me.lbl_log.Size = New System.Drawing.Size(35, 19)
        Me.lbl_log.TabIndex = 4
        Me.lbl_log.Text = "Log:"
        '
        'box_background
        '
        Me.box_background.AutoSize = True
        Me.box_background.Font = New System.Drawing.Font("Segoe UI", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.box_background.Location = New System.Drawing.Point(202, 16)
        Me.box_background.Name = "box_background"
        Me.box_background.Size = New System.Drawing.Size(104, 23)
        Me.box_background.TabIndex = 5
        Me.box_background.Text = "background"
        Me.box_background.UseVisualStyleBackColor = True
        '
        'lbl_version
        '
        Me.lbl_version.Font = New System.Drawing.Font("Segoe UI", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbl_version.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lbl_version.Location = New System.Drawing.Point(198, 447)
        Me.lbl_version.Name = "lbl_version"
        Me.lbl_version.Size = New System.Drawing.Size(99, 23)
        Me.lbl_version.TabIndex = 12
        Me.lbl_version.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btn_ChromeDriverUpd
        '
        Me.btn_ChromeDriverUpd.Font = New System.Drawing.Font("Segoe UI", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btn_ChromeDriverUpd.Location = New System.Drawing.Point(12, 47)
        Me.btn_ChromeDriverUpd.Name = "btn_ChromeDriverUpd"
        Me.btn_ChromeDriverUpd.Size = New System.Drawing.Size(185, 30)
        Me.btn_ChromeDriverUpd.TabIndex = 18
        Me.btn_ChromeDriverUpd.Text = "Update ChromeDriver"
        Me.btn_ChromeDriverUpd.UseVisualStyleBackColor = True
        '
        'lbl_copyright
        '
        Me.lbl_copyright.AutoSize = True
        Me.lbl_copyright.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lbl_copyright.Font = New System.Drawing.Font("Segoe UI", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbl_copyright.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lbl_copyright.Location = New System.Drawing.Point(287, 450)
        Me.lbl_copyright.Name = "lbl_copyright"
        Me.lbl_copyright.Size = New System.Drawing.Size(173, 19)
        Me.lbl_copyright.TabIndex = 13
        Me.lbl_copyright.Text = "© 2022 Webtask Solutions"
        '
        'BackgroundWorker2
        '
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(19, 431)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(459, 13)
        Me.ProgressBar1.TabIndex = 14
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'btn_load
        '
        Me.btn_load.Font = New System.Drawing.Font("Segoe UI", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btn_load.Location = New System.Drawing.Point(12, 12)
        Me.btn_load.Name = "btn_load"
        Me.btn_load.Size = New System.Drawing.Size(87, 29)
        Me.btn_load.TabIndex = 19
        Me.btn_load.Text = "Load"
        Me.btn_load.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(496, 479)
        Me.Controls.Add(Me.btn_load)
        Me.Controls.Add(Me.btn_start)
        Me.Controls.Add(Me.box_background)
        Me.Controls.Add(Me.btn_ChromeDriverUpd)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.lbl_copyright)
        Me.Controls.Add(Me.lbl_version)
        Me.Controls.Add(Me.lbl_log)
        Me.Controls.Add(Me.lbl_state)
        Me.Controls.Add(Me.txt_log)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form1"
        Me.Text = "Webtask Solutions"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btn_start As Button
    Friend WithEvents txt_log As RichTextBox
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents lbl_state As Label
    Friend WithEvents lbl_log As Label
    Friend WithEvents box_background As CheckBox
    Friend WithEvents lbl_version As Label
    Friend WithEvents lbl_copyright As Label
    Friend WithEvents BackgroundWorker2 As System.ComponentModel.BackgroundWorker
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents btn_ChromeDriverUpd As Button
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents btn_load As Button
End Class
