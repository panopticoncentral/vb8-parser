'
' Visual Basic .NET Parser
'
' Copyright (C) 2005, Microsoft Corporation. All rights reserved.
'
' THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER 
' EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES OF 
' MERCHANTIBILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
'

''' <summary>
''' The type of a syntax error.
''' </summary>
Public Enum SyntaxErrorType
    None

    ' Lexical errors
    InvalidEscapedIdentifier
    InvalidIdentifier
    InvalidTypeCharacterOnKeyword
    InvalidCharacter
    InvalidCharacterLiteral
    InvalidStringLiteral
    InvalidDateLiteral
    InvalidFloatingPointLiteral
    InvalidIntegerLiteral
    InvalidDecimalLiteral

    ' Syntax errors
    SyntaxError
    ExpectedComma
    ExpectedLeftParenthesis
    ExpectedRightParenthesis
    ExpectedEquals
    ExpectedAs
    ExpectedRightCurlyBrace
    ExpectedPeriod
    ExpectedMinus
    ExpectedIs
    ExpectedGreaterThan
    ExpectedType
    ExpectedIdentifier
    InvalidUseOfKeyword
    NoConstituentArraySizes
    NoExplicitArraySizes
    ExpectedExpression
    ArgumentSyntax
    ExpectedNamedArgument
    ExpectedPeriodAfterMyBase
    ExpectedPeriodAfterMyClass
    ExpectedExitKind
    ExpectedNext
    ExpectedResumeOrGoto
    ExpectedError
    MidTypeCharacter
    InitializerSyntax
    ExpectedModifier
    ExpectedEndOfStatement
    ExpectedLoop
    ExpectedEndWhile
    ExpectedEndSelect
    ExpectedEndSyncLock
    ExpectedEndWith
    ExpectedEndIf
    ExpectedEndTry
    ExpectedEndSub
    ExpectedEndFunction
    ExpectedEndProperty
    ExpectedEndGet
    ExpectedEndSet
    ExpectedEndClass
    ExpectedEndStructure
    ExpectedEndModule
    ExpectedEndInterface
    ExpectedEndEnum
    ExpectedEndNamespace
    LoopDoubleCondition
    LoopWithoutDo
    NextWithoutFor
    EndWhileWithoutWhile
    EndSelectWithoutSelect
    EndSyncLockWithoutSyncLock
    EndIfWithoutIf
    EndTryWithoutTry
    EndWithWithoutWith
    CatchWithoutTry
    FinallyWithoutTry
    CatchAfterFinally
    FinallyAfterFinally
    CaseWithoutSelect
    CaseAfterCaseElse
    CaseElseAfterCaseElse
    CaseElseWithoutSelect
    EndSubWithoutSub
    EndFunctionWithoutFunction
    EndPropertyWithoutProperty
    EndGetWithoutGet
    EndSetWithoutSet
    EndClassWithoutClass
    EndStructureWithoutStructure
    EndModuleWithoutModule
    EndInterfaceWithoutInterface
    EndEnumWithoutEnum
    EndNamespaceWithoutNamespace
    ExpectedCase
    ElseIfAfterElse
    ElseIfWithoutIf
    ElseAfterElse
    ElseWithoutIf
    EndInLineIf
    IncorrectAttributeType
    DuplicateModifier
    InvalidModifier
    InvalidVariableModifiers
    EventsCantBeFunctions
    ParameterSyntax
    MethodMustBeFirstStatementOnLine
    MethodBodyNotAtLineStart
    EndSubNotAtLineStart
    EndFunctionNotAtLineStart
    EndGetNotAtLineStart
    EndSetNotAtLineStart
    ExpectedSubOrFunction
    ExpectedStringLiteral
    ExpectedLib
    InvalidInsideProperty
    InvalidInsideClass
    InvalidInsideStructure
    InvalidInsideModule
    InvalidInsideInterface
    InvalidInsideEnum
    InvalidInsideNamespace
    SpecifiersInvalidOnTypeListDeclaration
    SpecifiersInvalidOnNamespaceDeclaration
    SpecifiersInvalidOnImportsDeclaration
    SpecifiersInvalidOnOptionDeclaration
    NoMultipleInheritance
    InheritsMustBeFirst
    ImplementsInWrongOrder
    EmptyEnum
    InvalidOptionExplicitType
    InvalidOptionStrictType
    InvalidOptionCompareType
    InvalidOptionType
    OptionStatementWrongOrder
    ImportsStatementWrongOrder
    AttributesStatementWrongOrder
    UnrecognizedEnd
    ExpectedRelationalOperator
    InvalidPreprocessorStatement
    ExpectedIntegerLiteral
    NestedExternalSourceStatement
    ExpectedEndKind
    EndExternalSourceWithoutExternalSource
    ExpectedEndExternalSource
    EndRegionWithoutRegion
    ExpectedEndRegion
    RegionInsideMethod

    ' Conditional compilation errors
    CantCastStringInCCExpression
    InvalidCCCast
    CCExpressionRequired
    InvalidCCOperator
    ExpectedCCEndIf
    CCEndIfWithoutCCIf
    CCElseIfAfterCCElse
    CCElseIfWithoutCCIf
    CCElseAfterCCElse
    CCElseWithoutCCIf

    ' New errors in v8.0
    InvalidUseOfGlobal
    ModulesCantBeGeneric
    ExpectedOf
    InvalidOperator
    ExpectedEndOperator
    EndOperatorWithoutOperator
    EndOperatorNotAtLineStart
    PropertiesCantBeGeneric
    ConstructorsCantBeGeneric
    OperatorsCantBeGeneric
    ExpectedPeriodAfterGlobal
    ExpectedContinueKind
    ExpectedEndUsing
    ExpectedEndEvent
    ExpectedEndAddHandler
    ExpectedEndRemoveHandler
    ExpectedEndRaiseEvent
    EndUsingWithoutUsing
    EndEventWithoutEvent
    EndAddHandlerWithoutAddHandler
    EndRemoveHandlerWithoutRemoveHandler
    EndRaiseEventWithoutRaiseEvent
    EndAddHandlerNotAtLineStart
    EndRemoveHandlerNotAtLineStart
    EndRaiseEventNotAtLineStart
End Enum