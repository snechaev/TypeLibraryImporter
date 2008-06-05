﻿' Copyright (c) Microsoft Corporation.  All rights reserved.
'The following code was generated by Microsoft Visual Studio 2005.
'The test owner should check each test for validity.
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports System
Imports System.Text
Imports System.Collections.Generic
Imports PInvoke





'''<summary>
'''This is a test class for PInvoke.NativeSymbolIterator and is intended
'''to contain all PInvoke.NativeSymbolIterator Unit Tests
'''</summary>
<TestClass()> _
Public Class NativeSymbolIteratorTest


    Private testContextInstance As TestContext

    '''<summary>
    '''Gets or sets the test context which provides
    '''information about and functionality for the current test run.
    '''</summary>
    Public Property TestContext() As TestContext
        Get
            Return testContextInstance
        End Get
        Set(ByVal value As TestContext)
            testContextInstance = value
        End Set
    End Property

    Private Sub EnsureIsSymbol(ByVal sym As NativeSymbol, ByVal list As List(Of NativeSymbolRelationship))
        For Each rel As NativeSymbolRelationship In list
            If Object.ReferenceEquals(sym, rel.Symbol) Then
                Return
            End If
        Next

        Assert.Fail("Could not find the symbol")
    End Sub

    Private Sub EnsureIsParent(ByVal sym As NativeSymbol, ByVal list As List(Of NativeSymbolRelationship))
        For Each rel As NativeSymbolRelationship In list
            If Object.ReferenceEquals(sym, rel.Parent) Then
                Return
            End If
        Next

        Assert.Fail("Could not find the symbol")
    End Sub

    '''<summary>
    ''' Make sure that the original parent is returned
    '''</summary>
    <TestMethod()> _
    Public Sub FindAllRelationships1()
        Dim ns As New NativeBuiltinType(BuiltinType.NativeByte)
        Dim it As New NativeSymbolIterator()
        Dim list As List(Of NativeSymbolRelationship) = it.FindAllNativeSymbolRelationships(ns)

        Assert.AreEqual(1, list.Count)
        EnsureIsSymbol(ns, list)
    End Sub

    ''' <summary>
    ''' Make sure the parent is returned as a parent
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()> _
    Public Sub FindAllRelationships2()
        Dim child As New NativeBuiltinType(BuiltinType.NativeByte)
        Dim par As New NativePointer(child)
        Dim it As New NativeSymbolIterator()
        Dim list As List(Of NativeSymbolRelationship) = it.FindAllNativeSymbolRelationships(par)

        Assert.AreEqual(2, list.Count)
        EnsureIsSymbol(par, list)
        EnsureIsSymbol(child, list)
        EnsureIsParent(par, list)
    End Sub

    ''' <summary>
    ''' Recursive relatioship should work just fine even though this tree is not techinally legal
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()> _
    Public Sub FindAllRelationships3()
        Dim ptr1 As New NativePointer()
        Dim ptr2 As New NativePointer(ptr1)
        ptr1.RealType = ptr2

        Dim it As New NativeSymbolIterator()
        Dim list As List(Of NativeSymbolRelationship) = it.FindAllNativeSymbolRelationships(ptr1)
        EnsureIsSymbol(ptr1, list)
        EnsureIsSymbol(ptr2, list)
    End Sub

    <TestMethod()> _
    Public Sub FindAllNativeSymbols1()
        Dim child As New NativeBuiltinType(BuiltinType.NativeByte)
        Dim par As New NativePointer(child)
        Dim it As New NativeSymbolIterator()
        Dim list As List(Of NativeSymbol) = it.FindAllNativeSymbols(par)

        Assert.IsTrue(list.Contains(child))
        Assert.IsTrue(list.Contains(par))
    End Sub

End Class