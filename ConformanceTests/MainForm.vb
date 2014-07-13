Imports System.IO
Imports System.Text
Imports System.Xml
Imports VBParser

Public Class MainForm
    Const WinDiffPath As String = "C:\Program Files\Microsoft Visual Studio 8\Common7\Tools\Bin\windiff.exe"
    Const NotepadPath As String = "notepad.exe"

    Private Version As LanguageVersion = LanguageVersion.VisualBasic80
    Private TestHarness As TestHarness = New TestHarness

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub LoadToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadToolStripMenuItem.Click
        If OpenFileDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
            RichTextBoxOriginal.Clear()
            RichTextBoxAfter.Clear()
            RichTextBoxOriginal.LoadFile(OpenFileDialog.FileName, RichTextBoxStreamType.PlainText)
        End If
    End Sub

    Private Sub ClearToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearToolStripMenuItem.Click
        RichTextBoxOriginal.Clear()
        RichTextBoxAfter.Clear()
    End Sub

    Private Sub DisplayTree(ByVal Tree As Tree, ByVal ErrorTable As List(Of SyntaxError))
        Dim AfterText As String = ""
        Dim StringWriter As StringWriter = New StringWriter
        Dim Writer As XmlTextWriter = New XmlTextWriter(StringWriter)
        Dim Serializer As TreeXmlSerializer = New TreeXmlSerializer(Writer)

        Writer.Formatting = Formatting.Indented
        Serializer.Serialize(Tree)

        If Not Tree Is Nothing Then
            AfterText = StringWriter.ToString()
        End If

        If ErrorTable.Count > 0 Then
            AfterText &= ControlChars.CrLf & "### Errors ###" & ControlChars.CrLf

            For Each SyntaxError As SyntaxError In ErrorTable
                RichTextBoxOriginal.Select(CInt(SyntaxError.Span.Start.Index), CInt(SyntaxError.Span.Finish.Index - SyntaxError.Span.Start.Index))
                RichTextBoxOriginal.SelectionColor = Color.Red
                AfterText &= SyntaxError.ToString() & ControlChars.CrLf
            Next
        End If

        RichTextBoxOriginal.Select(0, 0)
        RichTextBoxAfter.Text = AfterText
    End Sub

    Private Sub MainForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Root As TreeNode

        TreeViewTests.BeginUpdate()

        Root = TreeViewTests.Nodes.Add("Root")

        For ScenarioType As ScenarioType = 0 To ScenarioType.Max
            Dim TypeNode As TreeNode = Root.Nodes.Add(ScenarioType.ToString())
            TypeNode.Tag = ScenarioType

            For Each Scenario As Scenario In TestHarness.Scenarios(ScenarioType)
                Dim ScenarioNode As TreeNode = TypeNode.Nodes.Add(Scenario.Name)
                Dim Index As Integer = 1

                ScenarioNode.Tag = Scenario

                For Each Test As Test In Scenario.Tests
                    Dim TestNode As TreeNode = ScenarioNode.Nodes.Add("Test #" & Index)
                    TestNode.Tag = Test
                    Index += 1
                Next
            Next
        Next

        TreeViewTests.EndUpdate()
    End Sub

    Private Sub TreeViewTests_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeViewTests.AfterSelect
        If Not e.Node.Tag Is Nothing Then
            If TypeOf e.Node.Tag Is Test Then
                Dim Test As Test = CType(e.Node.Tag, Test)

                RichTextBoxOriginal.Text = Test.Code

                If Color.op_Equality(e.Node.ForeColor, Color.Red) Then
                    Dim OriginalPath As String = Path.GetTempPath() & "original.txt"
                    Dim AfterPath As String = Path.GetTempPath() & "after.txt"
                    Dim OriginalWriter As New StreamWriter(New FileStream(OriginalPath, FileMode.Create))
                    Dim AfterWriter As New StreamWriter(New FileStream(AfterPath, FileMode.Create))
                    Dim ScenarioType As ScenarioType = CType(e.Node.Parent.Parent.Tag, ScenarioType)
                    Dim ActualResult As String = ""
                    Dim ErrorResult As String = ""
                    Dim RunVersion As LanguageVersion

                    If Test.Version = LanguageVersion.All Then
                        RunVersion = LanguageVersion.VisualBasic71
                    Else
                        RunVersion = Test.Version
                    End If

                    OriginalWriter.WriteLine(Test.Result)
                    OriginalWriter.Write(Test.Errors)
                    OriginalWriter.Close()

                    TestHarness.RunSingleTest(ScenarioType, Test, RunVersion, ActualResult, ErrorResult)

                    AfterWriter.WriteLine(ActualResult)
                    AfterWriter.Write(ErrorResult)
                    AfterWriter.Close()

                    Shell(WinDiffPath & " """ & OriginalPath & """ """ & AfterPath & """", AppWinStyle.MaximizedFocus)
                End If
            End If
        End If
    End Sub

    Private Sub AllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AllToolStripMenuItem.Click
        Dim RootNode As TreeNode = TreeViewTests.Nodes(0)
        Dim RootSucceeded As Boolean = True

        Cursor = Cursors.WaitCursor
        RootNode.Expand()

        For Each ScenarioTypeNode As TreeNode In RootNode.Nodes
            Dim ScenarioType As ScenarioType = CType(ScenarioTypeNode.Tag, ScenarioType)
            Dim ScenarioTypeSucceeded As Boolean = True

            ScenarioTypeNode.Expand()

            For Each ScenarioNode As TreeNode In ScenarioTypeNode.Nodes
                Dim Scenario As Scenario = CType(ScenarioNode.Tag, Scenario)
                Dim AddedResults As Boolean = False
                Dim ScenarioSucceeded As Boolean = True

                For Each TestNode As TreeNode In ScenarioNode.Nodes
                    Dim Succeeded As Boolean
                    Dim Test As Test = CType(TestNode.Tag, Test)

                    Succeeded = TestHarness.RunTest(ScenarioType, Test, AddedResults)

                    If Succeeded Then
                        TestNode.ForeColor = Color.Green
                    Else
                        TestNode.ForeColor = Color.Red
                        ScenarioSucceeded = False
                    End If
                Next

                If ScenarioSucceeded Then
                    ScenarioNode.ForeColor = Color.Green
                Else
                    ScenarioNode.ForeColor = Color.Red
                    ScenarioTypeSucceeded = False
                End If

                If AddedResults Then
                    Dim ResultsPath As String = Path.GetTempPath() & Scenario.Name & ".xml"
                    Dim ResultsXmlWriter As XmlTextWriter = New XmlTextWriter(ResultsPath, Encoding.Unicode)

                    ResultsXmlWriter.Formatting = Formatting.Indented

                    Scenario.Document.WriteTo(ResultsXmlWriter)
                    ResultsXmlWriter.Close()

                    Shell(NotepadPath & " " & ResultsPath, AppWinStyle.MaximizedFocus)
                End If
            Next

            If ScenarioTypeSucceeded Then
                ScenarioTypeNode.ForeColor = Color.Green
            Else
                ScenarioTypeNode.ForeColor = Color.Red
                RootSucceeded = False
            End If
        Next

        If RootSucceeded Then
            RootNode.ForeColor = Color.Green
        Else
            RootNode.ForeColor = Color.Red
        End If

        Cursor = Cursors.Default
    End Sub

    Private Sub ParseFileToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ParseFileToolStripMenuItem.Click
        Dim Scanner As Scanner = New Scanner(RichTextBoxOriginal.Text, Version)
        Dim Parser As Parser = New Parser
        Dim ErrorTable As List(Of SyntaxError) = New List(Of SyntaxError)()

        DisplayTree(Parser.ParseFile(Scanner, ErrorTable), ErrorTable)
    End Sub

    Private Sub ParseDeclarationToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ParseDeclarationToolStripMenuItem.Click
        Dim Scanner As Scanner = New Scanner(RichTextBoxOriginal.Text, Version)
        Dim Parser As Parser = New Parser
        Dim ErrorTable As List(Of SyntaxError) = New List(Of SyntaxError)()

        DisplayTree(Parser.ParseDeclaration(Scanner, ErrorTable), ErrorTable)
    End Sub

    Private Sub ParseStatementToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ParseStatementToolStripMenuItem.Click
        Dim Scanner As Scanner = New Scanner(RichTextBoxOriginal.Text, Version)
        Dim Parser As Parser = New Parser
        Dim ErrorTable As List(Of SyntaxError) = New List(Of SyntaxError)()

        DisplayTree(Parser.ParseStatement(Scanner, ErrorTable), ErrorTable)
    End Sub

    Private Sub ParseExpressionToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ParseExpressionToolStripMenuItem.Click
        Dim Scanner As Scanner = New Scanner(RichTextBoxOriginal.Text, Version)
        Dim Parser As Parser = New Parser
        Dim ErrorTable As List(Of SyntaxError) = New List(Of SyntaxError)()

        DisplayTree(Parser.ParseExpression(Scanner, ErrorTable), ErrorTable)
    End Sub

    Private Sub ScanToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ScanToolStripMenuItem.Click
        Dim Scanner As Scanner = New Scanner(RichTextBoxOriginal.Text, Version)
        Dim StringWriter As StringWriter = New StringWriter
        Dim Writer As XmlTextWriter = New XmlTextWriter(StringWriter)
        Dim Serializer As TokenXmlSerializer = New TokenXmlSerializer(Writer)

        Writer.Formatting = Formatting.Indented
        Serializer.Serialize(Scanner.ReadToEnd())
        RichTextBoxAfter.Text = StringWriter.ToString()
    End Sub

    Private Sub ToolStripComboBoxVersion_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripComboBoxVersion.TextChanged
        If ToolStripComboBoxVersion.Text = "VB 2005" Then
            Version = LanguageVersion.VisualBasic80
        ElseIf ToolStripComboBoxVersion.Text = "VB 2003" Then
            Version = LanguageVersion.VisualBasic71
        Else
            ToolStripComboBoxVersion.Text = "VB 2005"
        End If
    End Sub
End Class