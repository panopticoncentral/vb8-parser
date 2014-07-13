Public Partial Class MainForm
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()> _
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

    End Sub

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.MenuStripMain = New System.Windows.Forms.MenuStrip
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.TextToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.LoadToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ClearToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.TestToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ScanToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ParseFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ParseDeclarationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ParseStatementToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ParseExpressionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.AllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.StatusStripMain = New System.Windows.Forms.StatusStrip
        Me.ToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel
        Me.OpenFileDialog = New System.Windows.Forms.OpenFileDialog
        Me.ToolStripContainer = New System.Windows.Forms.ToolStripContainer
        Me.SplitContainerVertical = New System.Windows.Forms.SplitContainer
        Me.TreeViewTests = New System.Windows.Forms.TreeView
        Me.SplitContainerHorizontal = New System.Windows.Forms.SplitContainer
        Me.RichTextBoxOriginal = New System.Windows.Forms.RichTextBox
        Me.RichTextBoxAfter = New System.Windows.Forms.RichTextBox
        Me.ToolStrip = New System.Windows.Forms.ToolStrip
        Me.ToolStripComboBoxVersion = New System.Windows.Forms.ToolStripComboBox
        Me.MenuStripMain.SuspendLayout()
        Me.StatusStripMain.SuspendLayout()
        Me.ToolStripContainer.ContentPanel.SuspendLayout()
        Me.ToolStripContainer.TopToolStripPanel.SuspendLayout()
        Me.ToolStripContainer.SuspendLayout()
        Me.SplitContainerVertical.Panel1.SuspendLayout()
        Me.SplitContainerVertical.Panel2.SuspendLayout()
        Me.SplitContainerVertical.SuspendLayout()
        Me.SplitContainerHorizontal.Panel1.SuspendLayout()
        Me.SplitContainerHorizontal.Panel2.SuspendLayout()
        Me.SplitContainerHorizontal.SuspendLayout()
        Me.ToolStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStripMain
        '
        Me.MenuStripMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.TextToolStripMenuItem, Me.TestToolStripMenuItem})
        Me.MenuStripMain.Location = New System.Drawing.Point(0, 0)
        Me.MenuStripMain.Name = "MenuStripMain"
        Me.MenuStripMain.Size = New System.Drawing.Size(664, 24)
        Me.MenuStripMain.TabIndex = 0
        Me.MenuStripMain.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Text = "&File"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Text = "E&xit"
        '
        'TextToolStripMenuItem
        '
        Me.TextToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LoadToolStripMenuItem, Me.ClearToolStripMenuItem})
        Me.TextToolStripMenuItem.Name = "TextToolStripMenuItem"
        Me.TextToolStripMenuItem.Text = "T&ext"
        '
        'LoadToolStripMenuItem
        '
        Me.LoadToolStripMenuItem.Name = "LoadToolStripMenuItem"
        Me.LoadToolStripMenuItem.Text = "&Load"
        '
        'ClearToolStripMenuItem
        '
        Me.ClearToolStripMenuItem.Name = "ClearToolStripMenuItem"
        Me.ClearToolStripMenuItem.Text = "&Clear"
        '
        'TestToolStripMenuItem
        '
        Me.TestToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ScanToolStripMenuItem, Me.ParseFileToolStripMenuItem, Me.ParseDeclarationToolStripMenuItem, Me.ParseStatementToolStripMenuItem, Me.ParseExpressionToolStripMenuItem, Me.ToolStripSeparator1, Me.AllToolStripMenuItem})
        Me.TestToolStripMenuItem.Name = "TestToolStripMenuItem"
        Me.TestToolStripMenuItem.Text = "&Test"
        '
        'ScanToolStripMenuItem
        '
        Me.ScanToolStripMenuItem.Name = "ScanToolStripMenuItem"
        Me.ScanToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.ScanToolStripMenuItem.Text = "&Scan"
        '
        'ParseFileToolStripMenuItem
        '
        Me.ParseFileToolStripMenuItem.Name = "ParseFileToolStripMenuItem"
        Me.ParseFileToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F), System.Windows.Forms.Keys)
        Me.ParseFileToolStripMenuItem.Text = "Parse &File"
        '
        'ParseDeclarationToolStripMenuItem
        '
        Me.ParseDeclarationToolStripMenuItem.Name = "ParseDeclarationToolStripMenuItem"
        Me.ParseDeclarationToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D), System.Windows.Forms.Keys)
        Me.ParseDeclarationToolStripMenuItem.Text = "Parse &Declaration"
        '
        'ParseStatementToolStripMenuItem
        '
        Me.ParseStatementToolStripMenuItem.Name = "ParseStatementToolStripMenuItem"
        Me.ParseStatementToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.T), System.Windows.Forms.Keys)
        Me.ParseStatementToolStripMenuItem.Text = "Parse &Statement"
        '
        'ParseExpressionToolStripMenuItem
        '
        Me.ParseExpressionToolStripMenuItem.Name = "ParseExpressionToolStripMenuItem"
        Me.ParseExpressionToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E), System.Windows.Forms.Keys)
        Me.ParseExpressionToolStripMenuItem.Text = "Parse &Expression"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        '
        'AllToolStripMenuItem
        '
        Me.AllToolStripMenuItem.Name = "AllToolStripMenuItem"
        Me.AllToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.AllToolStripMenuItem.Text = "&All"
        '
        'StatusStripMain
        '
        Me.StatusStripMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel})
        Me.StatusStripMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Table
        Me.StatusStripMain.Location = New System.Drawing.Point(0, 490)
        Me.StatusStripMain.Name = "StatusStripMain"
        Me.StatusStripMain.Size = New System.Drawing.Size(664, 23)
        Me.StatusStripMain.TabIndex = 1
        Me.StatusStripMain.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel
        '
        Me.ToolStripStatusLabel.Name = "ToolStripStatusLabel"
        Me.ToolStripStatusLabel.Text = "Ready"
        '
        'ToolStripContainer
        '
        '
        'ToolStripContainer.ContentPanel
        '
        Me.ToolStripContainer.ContentPanel.Controls.Add(Me.SplitContainerVertical)
        Me.ToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ToolStripContainer.Location = New System.Drawing.Point(0, 24)
        Me.ToolStripContainer.Name = "ToolStripContainer"
        Me.ToolStripContainer.Size = New System.Drawing.Size(664, 466)
        Me.ToolStripContainer.TabIndex = 3
        Me.ToolStripContainer.Text = "ToolStripContainer1"
        '
        'ToolStripContainer.TopToolStripPanel
        '
        Me.ToolStripContainer.TopToolStripPanel.Controls.Add(Me.ToolStrip)
        '
        'SplitContainerVertical
        '
        Me.SplitContainerVertical.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerVertical.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerVertical.Name = "SplitContainerVertical"
        '
        'SplitContainerVertical.Panel1
        '
        Me.SplitContainerVertical.Panel1.Controls.Add(Me.TreeViewTests)
        '
        'SplitContainerVertical.Panel2
        '
        Me.SplitContainerVertical.Panel2.Controls.Add(Me.SplitContainerHorizontal)
        Me.SplitContainerVertical.Size = New System.Drawing.Size(664, 441)
        Me.SplitContainerVertical.SplitterDistance = 192
        Me.SplitContainerVertical.TabIndex = 3
        Me.SplitContainerVertical.Text = "SplitContainerVertical"
        '
        'TreeViewTests
        '
        Me.TreeViewTests.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeViewTests.Location = New System.Drawing.Point(0, 0)
        Me.TreeViewTests.Name = "TreeViewTests"
        Me.TreeViewTests.Size = New System.Drawing.Size(192, 441)
        Me.TreeViewTests.TabIndex = 0
        '
        'SplitContainerHorizontal
        '
        Me.SplitContainerHorizontal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerHorizontal.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerHorizontal.Name = "SplitContainerHorizontal"
        Me.SplitContainerHorizontal.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainerHorizontal.Panel1
        '
        Me.SplitContainerHorizontal.Panel1.Controls.Add(Me.RichTextBoxOriginal)
        '
        'SplitContainerHorizontal.Panel2
        '
        Me.SplitContainerHorizontal.Panel2.Controls.Add(Me.RichTextBoxAfter)
        Me.SplitContainerHorizontal.Size = New System.Drawing.Size(468, 441)
        Me.SplitContainerHorizontal.SplitterDistance = 201
        Me.SplitContainerHorizontal.TabIndex = 0
        Me.SplitContainerHorizontal.Text = "SplitContainerHorizontal"
        '
        'RichTextBoxOriginal
        '
        Me.RichTextBoxOriginal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RichTextBoxOriginal.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RichTextBoxOriginal.Location = New System.Drawing.Point(0, 0)
        Me.RichTextBoxOriginal.Name = "RichTextBoxOriginal"
        Me.RichTextBoxOriginal.Size = New System.Drawing.Size(468, 201)
        Me.RichTextBoxOriginal.TabIndex = 0
        Me.RichTextBoxOriginal.Text = ""
        '
        'RichTextBoxAfter
        '
        Me.RichTextBoxAfter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RichTextBoxAfter.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RichTextBoxAfter.Location = New System.Drawing.Point(0, 0)
        Me.RichTextBoxAfter.Name = "RichTextBoxAfter"
        Me.RichTextBoxAfter.Size = New System.Drawing.Size(468, 236)
        Me.RichTextBoxAfter.TabIndex = 0
        Me.RichTextBoxAfter.Text = ""
        '
        'ToolStrip
        '
        Me.ToolStrip.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripComboBoxVersion})
        Me.ToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip.Name = "ToolStrip"
        Me.ToolStrip.Size = New System.Drawing.Size(113, 25)
        Me.ToolStrip.TabIndex = 0
        Me.ToolStrip.Text = "ToolStrip1"
        '
        'ToolStripComboBoxVersion
        '
        Me.ToolStripComboBoxVersion.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText
        Me.ToolStripComboBoxVersion.Items.AddRange(New Object() {"VB 2005", "VB 2003"})
        Me.ToolStripComboBoxVersion.Name = "ToolStripComboBoxVersion"
        Me.ToolStripComboBoxVersion.Size = New System.Drawing.Size(100, 25)
        Me.ToolStripComboBoxVersion.Text = "VB 2005"
        '
        'MainForm
        '
        Me.ClientSize = New System.Drawing.Size(664, 513)
        Me.Controls.Add(Me.ToolStripContainer)
        Me.Controls.Add(Me.StatusStripMain)
        Me.Controls.Add(Me.MenuStripMain)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "MainForm"
        Me.Text = "Test Container"
        Me.MenuStripMain.ResumeLayout(False)
        Me.StatusStripMain.ResumeLayout(False)
        Me.ToolStripContainer.ContentPanel.ResumeLayout(False)
        Me.ToolStripContainer.TopToolStripPanel.ResumeLayout(False)
        Me.ToolStripContainer.TopToolStripPanel.PerformLayout()
        Me.ToolStripContainer.ResumeLayout(False)
        Me.ToolStripContainer.PerformLayout()
        Me.SplitContainerVertical.Panel1.ResumeLayout(False)
        Me.SplitContainerVertical.Panel2.ResumeLayout(False)
        Me.SplitContainerVertical.ResumeLayout(False)
        Me.SplitContainerHorizontal.Panel1.ResumeLayout(False)
        Me.SplitContainerHorizontal.Panel2.ResumeLayout(False)
        Me.SplitContainerHorizontal.ResumeLayout(False)
        Me.ToolStrip.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStripMain As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TextToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LoadToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ClearToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusStripMain As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents OpenFileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents TestToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ScanToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ParseFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ParseDeclarationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ParseStatementToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ParseExpressionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents AllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripContainer As System.Windows.Forms.ToolStripContainer
    Friend WithEvents SplitContainerVertical As System.Windows.Forms.SplitContainer
    Friend WithEvents TreeViewTests As System.Windows.Forms.TreeView
    Friend WithEvents SplitContainerHorizontal As System.Windows.Forms.SplitContainer
    Friend WithEvents RichTextBoxOriginal As System.Windows.Forms.RichTextBox
    Friend WithEvents RichTextBoxAfter As System.Windows.Forms.RichTextBox
    Friend WithEvents ToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripComboBoxVersion As System.Windows.Forms.ToolStripComboBox
End Class
