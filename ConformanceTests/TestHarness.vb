'
' Visual Basic .NET Parser
'
' Copyright (C) 2005, Microsoft Corporation. All rights reserved.
'
' THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER 
' EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES OF 
' MERCHANTIBILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
'

Imports System.IO
Imports System.Reflection
Imports System.Text
Imports System.Xml
Imports System.Xml.Serialization
Imports VBParser

' UNDONE: Need conformance tests for full-width characters

Public Enum ScenarioType
    Token
    Expression
    Statement
    Declaration
    TypeName
    File
    Max = File
End Enum

Public Structure Test
    Public ErrorsExpected As Boolean
    Public Element As XmlElement
    Public Code As String
    Public Result As String
    Public Errors As String
    Public Version As LanguageVersion
End Structure

Public Structure Scenario
    Public Element As XmlElement
    Public Document As XmlDocument
    Public Name As String
    Public Type As ScenarioType
    Public Tests() As Test
End Structure

Friend Class TestHarness
    Public Scenarios()() As Scenario

    Public Sub New()
        Dim Assem As [Assembly] = [Assembly].GetExecutingAssembly()
        Dim Resources() As String = Assem.GetManifestResourceNames()
        Dim ScenarioLists(ScenarioType.Max) As List(Of Scenario)
        Dim ScenarioCount As Integer = 0

        For Index As Integer = 0 To ScenarioType.Max
            ScenarioLists(Index) = New List(Of Scenario)()
        Next

        For Each ResourceName As String In Resources
            Dim Stream As Stream = Assem.GetManifestResourceStream(ResourceName)
            Dim Scenario As Scenario
            Dim Tests As New List(Of Test)

            If Not ResourceName.EndsWith(".xml") Then
                Continue For
            End If

            Scenario.Document = New XmlDocument
            Scenario.Document.Load(Stream)
            Stream.Close()
            ScenarioCount += 1

            Scenario.Element = Scenario.Document("scenario")
            Scenario.Name = Scenario.Element.GetAttribute("name")

            Select Case Scenario.Element.GetAttribute("type")
                Case "token"
                    Scenario.Type = ScenarioType.Token

                Case "expression"
                    Scenario.Type = ScenarioType.Expression

                Case "statement"
                    Scenario.Type = ScenarioType.Statement

                Case "declaration"
                    Scenario.Type = ScenarioType.Declaration

                Case "typename"
                    Scenario.Type = ScenarioType.TypeName

                Case "file"
                    Scenario.Type = ScenarioType.File
            End Select

            For Each ScenarioChildNode As XmlNode In Scenario.Element.ChildNodes
                Dim ValidElement As XmlElement = TryCast(ScenarioChildNode, XmlElement)
                Dim ErrorTests As Boolean

                If ValidElement Is Nothing Then
                    Continue For
                End If

                If ValidElement.Name <> "valid" AndAlso ValidElement.Name <> "invalid" Then
                    Continue For
                End If

                ErrorTests = ValidElement.Name = "invalid"

                For Each ChildNode As XmlNode In ValidElement.ChildNodes
                    Dim Test As Test

                    Test.ErrorsExpected = ErrorTests
                    Test.Element = TryCast(ChildNode, XmlElement)

                    If Test.Element Is Nothing Then
                        Continue For
                    End If

                    If Test.Element.GetAttribute("version") = "8.0" Then
                        Test.Version = LanguageVersion.VisualBasic80
                    ElseIf Test.Element.GetAttribute("version") = "7.1" Then
                        Test.Version = LanguageVersion.VisualBasic71
                    Else
                        Test.Version = LanguageVersion.All
                    End If

                    Test.Code = Test.Element("code").InnerText.Trim(ControlChars.Cr, ControlChars.Lf)

                    Debug.Assert(Test.Element.GetElementsByTagName("result").Count < 2, "too many results!")

                    If Not Test.Element("result") Is Nothing Then
                        Dim StringWriter As StringWriter = New StringWriter
                        Dim Writer As XmlTextWriter = New XmlTextWriter(StringWriter)

                        Writer.Formatting = Formatting.Indented
                        Test.Element("result").WriteContentTo(Writer)
                        Writer.Close()
                        StringWriter.Close()

                        Test.Result = StringWriter.ToString()
                    Else
                        Test.Result = Nothing
                    End If

                    If Not Test.Element("errors") Is Nothing Then
                        Dim StringWriter As StringWriter = New StringWriter
                        Dim Writer As XmlTextWriter = New XmlTextWriter(StringWriter)

                        Writer.Formatting = Formatting.Indented
                        Test.Element("errors").WriteContentTo(Writer)
                        Writer.Close()
                        StringWriter.Close()

                        Test.Errors = StringWriter.ToString()
                    Else
                        Test.Errors = Nothing
                    End If

                    Tests.Add(Test)
                Next
            Next

            Scenario.Tests = New Test(Tests.Count - 1) {}
            Tests.CopyTo(Scenario.Tests)

            ScenarioLists(Scenario.Type).Add(Scenario)
        Next

        Scenarios = New Scenario(ScenarioType.Max)() {}

        For Index As Integer = 0 To ScenarioType.Max
            Scenarios(Index) = New Scenario(ScenarioLists(Index).Count - 1) {}
            ScenarioLists(Index).CopyTo(Scenarios(Index))
        Next
    End Sub

    Private Sub RunScanTest(ByVal Test As Test, ByVal Version As LanguageVersion, ByVal FormatResults As Boolean, ByRef Result As String, ByRef ErrorResult As String)
        Dim Scanner As Scanner = New Scanner(Test.Code, Version)
        Dim StringWriter As StringWriter = New StringWriter
        Dim Writer As XmlTextWriter = New XmlTextWriter(StringWriter)
        Dim Serializer As TokenXmlSerializer = New TokenXmlSerializer(Writer)
        Dim Tokens() As Token
        Dim HasErrors As Boolean = False

        If FormatResults Then
            Writer.Formatting = Formatting.Indented
        End If

        Tokens = Scanner.ReadToEnd()
        Serializer.Serialize(Tokens)
        Result = StringWriter.ToString()

        Writer.Close()
        StringWriter.Close()

        For Each Token As Token In Tokens
            If Token.Type = TokenType.LexicalError Then
                HasErrors = True
                Exit For
            End If
        Next

        If Test.ErrorsExpected <> HasErrors Then
            If Test.ErrorsExpected Then
                ErrorResult = "Expected errors but didn't get any."
            Else
                ErrorResult = "Didn't expect any errors but got some."
            End If
        End If
    End Sub

    Private Sub RunParseTest(ByVal ScenarioType As ScenarioType, ByVal Test As Test, ByVal Version As LanguageVersion, ByVal FormatResults As Boolean, ByRef Result As String, ByRef ErrorResult As String)
        Dim StringWriter As StringWriter = New StringWriter()
        Dim Writer As XmlTextWriter = New XmlTextWriter(StringWriter)
        Dim Serializer As TreeXmlSerializer = New TreeXmlSerializer(Writer)
        Dim ErrorStringWriter As StringWriter = New StringWriter()
        Dim ErrorWriter As XmlTextWriter = New XmlTextWriter(ErrorStringWriter)
        Dim ErrorSerializer As ErrorXmlSerializer = New ErrorXmlSerializer(ErrorWriter)
        Dim Errors As List(Of SyntaxError) = New List(Of SyntaxError)()

        If FormatResults Then
            Writer.Formatting = Formatting.Indented
            ErrorWriter.Formatting = Formatting.Indented
        End If

        Select Case ScenarioType
            Case ScenarioType.Expression
                Dim Scanner As Scanner = New Scanner(Test.Code, Version)
                Dim Parser As Parser = New Parser
                Dim Expression As Expression = Parser.ParseExpression(Scanner, Errors)
                Serializer.Serialize(Expression)

            Case ScenarioType.Statement
                Dim Scanner As Scanner = New Scanner(Test.Code, Version)
                Dim Parser As Parser = New Parser
                Dim Statement As Statement = Parser.ParseStatement(Scanner, Errors)
                Serializer.Serialize(Statement)

            Case ScenarioType.Declaration
                Dim Scanner As Scanner = New Scanner(Test.Code.Trim(), Version)
                Dim Parser As Parser = New Parser
                Dim Declaration As Declaration = Parser.ParseDeclaration(Scanner, Errors)
                Serializer.Serialize(Declaration)

            Case ScenarioType.TypeName
                Dim Scanner As Scanner = New Scanner(Test.Code, Version)
                Dim Parser As Parser = New Parser
                Dim TypeName As TypeName = Parser.ParseTypeName(Scanner, Errors)
                Serializer.Serialize(TypeName)

            Case ScenarioType.File
                Dim Scanner As Scanner = New Scanner(Test.Code.Trim(), Version)
                Dim Parser As Parser = New Parser
                Dim File As VBParser.File = Parser.ParseFile(Scanner, Errors)
                Serializer.Serialize(File)
        End Select

        Result = StringWriter.ToString()
        Writer.Close()
        StringWriter.Close()

        ErrorSerializer.Serialize(Errors)
        ErrorResult = ErrorStringWriter.ToString()
        ErrorWriter.Close()
        ErrorStringWriter.Close()

        If Test.ErrorsExpected <> (Errors.Count <> 0) Then
            If Test.ErrorsExpected Then
                ErrorResult = "Expected errors but didn't get any."
            Else
                ErrorResult = "Didn't expect any errors but got some."
            End If
        End If
    End Sub

    Public Sub RunSingleTest(ByVal ScenarioType As ScenarioType, ByVal Test As Test, ByVal Version As LanguageVersion, ByRef Result As String, ByRef ErrorResult As String, Optional ByVal FormatResults As Boolean = True)
        Select Case ScenarioType
            Case ScenarioType.Token
                RunScanTest(Test, Version, FormatResults, Result, ErrorResult)

            Case ScenarioType.Expression, ScenarioType.Statement, ScenarioType.Declaration, ScenarioType.TypeName, ScenarioType.File
                RunParseTest(ScenarioType, Test, Version, FormatResults, Result, ErrorResult)

            Case Else
                Debug.Fail("Unexpected.")
        End Select
    End Sub

    Public Function RunSingleVersionTest(ByVal ScenarioType As ScenarioType, ByVal Test As Test, ByVal Version As LanguageVersion, ByRef AddedResults As Boolean) As Boolean
        Dim ActualResult As String = ""
        Dim ActualErrors As String = ""

        RunSingleTest(ScenarioType, Test, Version, ActualResult, ActualErrors, Not Test.Result Is Nothing)

        If Not Test.Result Is Nothing Then
            Dim Result As String = Test.Result
            Dim Errors As String = Test.Errors

            Return ActualResult = Result AndAlso ActualErrors = Errors
        Else
            Dim ResultElement As XmlElement = Test.Element.OwnerDocument.CreateElement("result")
            Dim ResultFragment As XmlDocumentFragment = Test.Element.OwnerDocument.CreateDocumentFragment()

            ResultFragment.InnerXml = ActualResult
            ResultElement.AppendChild(ResultFragment)
            Test.Element.AppendChild(ResultElement)

            If ActualErrors <> "" Then
                Dim ErrorsElement As XmlElement = Test.Element.OwnerDocument.CreateElement("errors")
                Dim ErrorsFragment As XmlDocumentFragment = Test.Element.OwnerDocument.CreateDocumentFragment()

                ErrorsFragment.InnerXml = ActualErrors
                ErrorsElement.AppendChild(ErrorsFragment)
                Test.Element.AppendChild(ErrorsElement)
            End If

            AddedResults = True

            Return True
        End If
    End Function

    Public Function RunTest(ByVal ScenarioType As ScenarioType, ByVal Test As Test, ByRef AddedResults As Boolean) As Boolean
        Dim TestResult As Boolean = False

        If (Test.Version And LanguageVersion.VisualBasic71) <> 0 Then
            TestResult = RunSingleVersionTest(ScenarioType, Test, LanguageVersion.VisualBasic71, AddedResults)

            If Not TestResult Then
                Return False
            End If

            If AddedResults Then
                Return True
            End If
        End If

        If (Test.Version And LanguageVersion.VisualBasic80) <> 0 Then
            TestResult = RunSingleVersionTest(ScenarioType, Test, LanguageVersion.VisualBasic80, AddedResults)

            If Not TestResult Then
                Return False
            End If
        End If

        Return True
    End Function
End Class