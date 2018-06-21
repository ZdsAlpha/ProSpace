<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Visualizer
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Menu = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LoadToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.height_textbox = New System.Windows.Forms.NumericUpDown()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.seed_textbox = New System.Windows.Forms.NumericUpDown()
        Me.level_textbox = New System.Windows.Forms.NumericUpDown()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.name_label = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Canvas = New System.Windows.Forms.PictureBox()
        Me.Menu.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.height_textbox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.seed_textbox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.level_textbox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Canvas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Menu
        '
        Me.Menu.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.Menu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem})
        Me.Menu.Location = New System.Drawing.Point(0, 0)
        Me.Menu.Name = "Menu"
        Me.Menu.Size = New System.Drawing.Size(1038, 28)
        Me.Menu.TabIndex = 0
        Me.Menu.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LoadToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(44, 24)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'LoadToolStripMenuItem
        '
        Me.LoadToolStripMenuItem.Name = "LoadToolStripMenuItem"
        Me.LoadToolStripMenuItem.Size = New System.Drawing.Size(117, 26)
        Me.LoadToolStripMenuItem.Text = "Load"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(117, 26)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Panel1.Controls.Add(Me.height_textbox)
        Me.Panel1.Controls.Add(Me.Button3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.seed_textbox)
        Me.Panel1.Controls.Add(Me.level_textbox)
        Me.Panel1.Controls.Add(Me.Button2)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.name_label)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 624)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1038, 126)
        Me.Panel1.TabIndex = 1
        '
        'height_textbox
        '
        Me.height_textbox.Location = New System.Drawing.Point(102, 87)
        Me.height_textbox.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.height_textbox.Name = "height_textbox"
        Me.height_textbox.Size = New System.Drawing.Size(102, 22)
        Me.height_textbox.TabIndex = 12
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(210, 85)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(105, 23)
        Me.Button3.TabIndex = 11
        Me.Button3.Text = "Randomize"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 89)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 17)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Height"
        '
        'seed_textbox
        '
        Me.seed_textbox.Location = New System.Drawing.Point(102, 61)
        Me.seed_textbox.Maximum = New Decimal(New Integer() {1000000, 0, 0, 0})
        Me.seed_textbox.Minimum = New Decimal(New Integer() {1000000, 0, 0, -2147483648})
        Me.seed_textbox.Name = "seed_textbox"
        Me.seed_textbox.Size = New System.Drawing.Size(102, 22)
        Me.seed_textbox.TabIndex = 9
        '
        'level_textbox
        '
        Me.level_textbox.Location = New System.Drawing.Point(102, 36)
        Me.level_textbox.Maximum = New Decimal(New Integer() {50, 0, 0, 0})
        Me.level_textbox.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.level_textbox.Name = "level_textbox"
        Me.level_textbox.Size = New System.Drawing.Size(102, 22)
        Me.level_textbox.TabIndex = 8
        Me.level_textbox.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(210, 35)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(105, 23)
        Me.Button2.TabIndex = 7
        Me.Button2.Text = "Randomize"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(210, 59)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(105, 23)
        Me.Button1.TabIndex = 6
        Me.Button1.Text = "Randomize"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 63)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(45, 17)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Seed:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 38)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(46, 17)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Level:"
        '
        'name_label
        '
        Me.name_label.AutoSize = True
        Me.name_label.Location = New System.Drawing.Point(99, 12)
        Me.name_label.Name = "name_label"
        Me.name_label.Size = New System.Drawing.Size(42, 17)
        Me.name_label.TabIndex = 1
        Me.name_label.Text = "None"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(49, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Name:"
        '
        'Canvas
        '
        Me.Canvas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Canvas.Location = New System.Drawing.Point(0, 28)
        Me.Canvas.Name = "Canvas"
        Me.Canvas.Size = New System.Drawing.Size(1038, 596)
        Me.Canvas.TabIndex = 2
        Me.Canvas.TabStop = False
        '
        'Visualizer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1038, 750)
        Me.Controls.Add(Me.Canvas)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Menu)
        Me.MainMenuStrip = Me.Menu
        Me.Name = "Visualizer"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Visualizer"
        Me.Menu.ResumeLayout(False)
        Me.Menu.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.height_textbox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.seed_textbox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.level_textbox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Canvas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Menu As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LoadToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Button2 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents name_label As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents seed_textbox As NumericUpDown
    Friend WithEvents level_textbox As NumericUpDown
    Friend WithEvents Canvas As PictureBox
    Friend WithEvents height_textbox As NumericUpDown
    Friend WithEvents Button3 As Button
    Friend WithEvents Label2 As Label
End Class
