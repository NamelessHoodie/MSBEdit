﻿
Public Class msbdata
    Public Class columnStyle
        Public backgroundColor As Color
        Public textColor As Color

        Public Sub New(c1 As Color, c2 As Color)
            backgroundColor = c1
            textColor = c2
        End Sub
    End Class

    Private Shared known As columnStyle = New columnStyle(Color.White, Color.Black)
    Private Shared advanced As columnStyle = New columnStyle(Color.Orange, Color.Black)
    Private Shared unknown As columnStyle = New columnStyle(Color.LightGray, Color.Black)
    Private Shared important As columnStyle = New columnStyle(Color.LightYellow, Color.Black)

    Private fieldNames As List(Of String) = New List(Of String)
    Private fieldtypes As List(Of String) = New List(Of String)
    Private fieldStyles As List(Of columnStyle) = New List(Of columnStyle)

    Private nameIdx As Integer
    Private pointIndex1 As Integer = -1
    Private pointIndex2 As Integer = -1
    Private partIndex1 As Integer = -1
    Private partIndex2 As Integer = -1
    Private partIndex3 As Integer = -1

    Public Sub add(ByVal name As String, ByVal type As String, style As columnStyle)
        fieldNames.Add(name)
        fieldtypes.Add(type)
        fieldStyles.Add(style)
    End Sub
    Public Function retrieveName(ByVal i As Integer) As String
        Return fieldNames(i)
    End Function
    Public Function retrieveType(ByVal i As Integer) As String
        Return fieldtypes(i)
    End Function
    Public Function retrieveBackColor(ByVal i As Integer) As Color
        Return fieldStyles(i).backgroundColor
    End Function
    Public Function retrieveForeColor(ByVal i As Integer) As Color
        Return fieldStyles(i).textColor
    End Function
    Public Function isUnknown(ByVal i As Integer) As Boolean
        Return fieldStyles(i) Is unknown
    End Function
    Public Function isAdvanced(ByVal i As Integer) As Boolean
        Return fieldStyles(i) Is advanced
    End Function
    Public Function fieldCount() As Integer
        Return fieldNames.Count
    End Function
    Public Function getNameIndex() As Integer
        Return nameIdx
    End Function
    Public Sub setNameIndex(ByVal idx As Integer)
        nameIdx = idx
    End Sub
    Public Function getFieldIndex(ByVal name As String) As Integer
        Return fieldNames.IndexOf(name)
    End Function
    Public Function getPointIndices() As List(Of Integer)
        Dim result = New List(Of Integer)

        ' <> is such an odd way to write !=
        If pointIndex1 <> -1 Then result.Add(pointIndex1)
        If pointIndex2 <> -1 Then result.Add(pointIndex2)

        Return result
    End Function
    Public Function getPartIndices() As List(Of Integer)
        Dim result = New List(Of Integer)

        If partIndex1 <> -1 Then result.Add(partIndex1)
        If partIndex2 <> -1 Then result.Add(partIndex2)
        If partIndex3 <> -1 Then result.Add(partIndex3)

        Return result
    End Function

    Private Shared Sub eventTemplate(ByRef layout As msbdata)
        With layout
            .add("&Name", "i32", advanced)
            .add("Event Index", "i32", known)
            .add("Type", "i32", advanced)
            .add("Index", "i32", known)
            .add("&BaseData", "i32", known)
            .add("&TypeData", "i32", known)
            .add("x18", "i32", unknown)
            .setNameIndex(.fieldCount)
            .add("Name", "string", known)
            .partIndex1 = .fieldCount
            .add("PartIndex1", "i32", important)
            .pointIndex1 = .fieldCount
            .add("RegionIndex1", "i32", important)
            .add("EventEntityID", "i32", important)
            .add("p+0x0C", "i32", unknown)
        End With
    End Sub

    Public Shared Function generate(ByVal name As String)
        Dim layout = New msbdata

        With layout
            If name = "models" Then
                .add("&Name", "i32", advanced)
                .add("Type", "i32", advanced)
                .add("Index", "i32", known)
                .add("&Placeholder Model", "i32", advanced)
                .add("Instance Count", "i32", known)
                .add("x14", "i32", unknown)
                .add("x18", "i32", unknown)
                .add("x1C", "i32", unknown)
                .setNameIndex(.fieldCount)
                .add("Name", "string", known)
                .add("Placeholder Model", "string", advanced)
            ElseIf name = "events0" Then
                eventTemplate(layout)
                .add("p+0x10", "i32", unknown)
            ElseIf name = "events1" Then
                eventTemplate(layout)
                .add("Sound Type", "i32", known)
                .add("Sound ID", "i32", known)
            ElseIf name = "events2" Then
                eventTemplate(layout)
                .add("ParticleEffectId", "i32", known)
            ElseIf name = "events3" Then
                eventTemplate(layout)
                .add("p+0x10", "f32", unknown)
                .add("p+0x14", "f32", unknown)
                .add("p+0x18", "f32", unknown)
                .add("p+0x1C", "f32", unknown)
                .add("p+0x20", "f32", unknown)
                .add("p+0x24", "f32", unknown)
                .add("p+0x28", "f32", unknown)
                .add("p+0x2C", "f32", unknown)
                .add("p+0x30", "f32", unknown)
                .add("p+0x34", "f32", unknown)
                .add("p+0x38", "f32", unknown)
                .add("p+0x3C", "f32", unknown)
                .add("p+0x40", "f32", unknown)
                .add("p+0x44", "f32", unknown)
                .add("p+0x48", "f32", unknown)
                .add("p+0x4C", "f32", unknown)
            ElseIf name = "events4" Then
                eventTemplate(layout)
                .add("p+0x10", "i32", unknown)
                .partIndex2 = .fieldCount
                .add("PartIndex2", "i32", important)
                .add("ItemLot1", "i32", known)
                .add("p+0x1C", "i32", unknown)
                .add("ItemLot2", "i32", known)
                .add("p+0x24", "i32", unknown)
                .add("ItemLot3", "i32", known)
                .add("p+0x2C", "i32", unknown)
                .add("ItemLot4", "i32", known)
                .add("p+0x34", "i32", unknown)
                .add("ItemLot5", "i32", known)
                .add("p+0x3C", "i32", unknown)
                .add("p+0x40", "i32", unknown)
            ElseIf name = "events5" Then
                ' Enemy spawner, like for the blighttown mosquitoes
                eventTemplate(layout)

                .add("p+0x10", "i16", unknown)
                .add("p+0x12", "i16", unknown)
                .add("p+0x14", "i16", unknown)
                .add("p+0x16", "i16", unknown)

                .add("p+0x18", "f32", unknown)
                .add("p+0x1C", "f32", unknown)

                .add("p+0x20", "i32", unknown)
                .add("p+0x24", "i32", unknown)
                .add("p+0x28", "i32", unknown)
                .add("p+0x2C", "i32", unknown)
                .add("p+0x30", "i32", unknown)
                .add("p+0x34", "i32", unknown)
                .add("p+0x38", "i32", unknown)
                .add("p+0x3C", "i32", unknown)
                .pointIndex2 = .fieldCount
                .add("RegionIndex2", "i32", important)
                .add("p+0x44", "i32", unknown)
                .add("p+0x48", "i32", unknown)
                .add("p+0x4C", "i32", unknown)
                .partIndex2 = .fieldCount
                .add("PartIndex2", "i32", important)
                .partIndex3 = .fieldCount
                .add("PartIndex3", "i32", important)
                .add("p+0x58", "i32", unknown)
                .add("p+0x5C", "i32", unknown)
                .add("p+0x60", "i32", unknown)
                .add("p+0x64", "i32", unknown)
                .add("p+0x68", "i32", unknown)
                .add("p+0x6C", "i32", unknown)
                .add("p+0x70", "i32", unknown)
                .add("p+0x74", "i32", unknown)
                .add("p+0x78", "i32", unknown)
                .add("p+0x7C", "i32", unknown)
                .add("p+0x80", "i32", unknown)
                .add("p+0x84", "i32", unknown)
                .add("p+0x88", "i32", unknown)
                .add("p+0x8C", "i32", unknown)
                .add("p+0x90", "i32", unknown)
                .add("p+0x94", "i32", unknown)
                .add("p+0x98", "i32", unknown)
                .add("p+0x9C", "i32", unknown)
                .add("p+0xA0", "i32", unknown)
                .add("p+0xA4", "i32", unknown)
                .add("p+0xA8", "i32", unknown)
                .add("p+0xAC", "i32", unknown)
                .add("p+0xB0", "i32", unknown)
                .add("p+0xB4", "i32", unknown)
                .add("p+0xB8", "i32", unknown)
                .add("p+0xBC", "i32", unknown)
                .add("p+0xC0", "i32", unknown)
                .add("p+0xC4", "i32", unknown)
                .add("p+0xC8", "i32", unknown)
                .add("p+0xCC", "i32", unknown)
                .add("p+0xD0", "i32", unknown)
                .add("p+0xD4", "i32", unknown)
                .add("p+0xD8", "i32", unknown)
                .add("p+0xDC", "i32", unknown)
                .add("p+0xE0", "i32", unknown)
                .add("p+0xE4", "i32", unknown)
                .add("p+0xE8", "i32", unknown)
                .add("p+0xEC", "i32", unknown)
                .add("p+0xF0", "i32", unknown)
                .add("p+0xF4", "i32", unknown)
                .add("p+0xF8", "i32", unknown)
                .add("p+0xFC", "i32", unknown)
                .add("p+0x100", "i32", unknown)
                .add("p+0x104", "i32", unknown)
                .add("p+0x108", "i32", unknown)
                .add("p+0x10C", "i32", unknown)
            ElseIf name = "events6" Then
                ' Magic blood characters and the tutorial message near Petrus? Sounds like orange soapstone messages.
                eventTemplate(layout)
                '.add("p+0x10", "i32", unknown)
                '.add("p+0x14", "i32", unknown)
                .add("Msg ID", "i16", known)
                .add("p+0x12", "i16", unknown)
                .add("p+0x14", "i16", unknown)
                .add("p+0x16", "i16", unknown)
            ElseIf name = "events7" Then 'ObjAct
                eventTemplate(layout)
                .add("Entity ID", "i32", known)
                .partIndex2 = .fieldCount
                .add("PartIndex2", "i32", important)
                .add("Parameter ID", "i16", known)
                .add("p+0x1A", "i16", unknown)
                .add("Event Flag ID", "i32", known)
            ElseIf name = "events8" Then
                eventTemplate(layout)
                .pointIndex2 = .fieldCount
                .add("RegionIndex2", "i32", important) 'MEOWTODO: Check that RegionIndex2 updates right here
                .add("p+0x14", "i32", unknown)
                .add("p+0x18", "i32", unknown)
                .add("p+0x1C", "i32", unknown)
            ElseIf name = "events9" Then
                eventTemplate(layout)
                .add("p+0x10", "i32", unknown)
                .add("p+0x14", "i32", unknown)
                .add("p+0x18", "i32", unknown)
                .add("p+0x1C", "i32", unknown)
            ElseIf name = "events10" Then
                eventTemplate(layout)
                .pointIndex2 = .fieldCount
                .add("RegionIndex2", "i32", important)
                .add("p+0x14", "i32", unknown)
                .add("p+0x18", "i32", unknown)
                .add("p+0x1C", "i32", unknown)
            ElseIf name = "events11" Then
                eventTemplate(layout)
                .add("p+0x10", "i32", unknown)
                .add("p+0x14", "f32", unknown)
                .add("p+0x18", "f32", unknown)
                .add("p+0x1C", "f32", unknown)
                .add("p+0x20", "f32", unknown)
                .add("p+0x24", "f32", unknown)
                .add("p+0x28", "i32", unknown)
                .add("p+0x2C", "i32", unknown)
            ElseIf name = "events12" Then
                ' Only used once, in the Painted World
                ' Rough translation: "The world of NPC (a warrior)"
                ' Point name: "Event: Initial position of the boss"
                eventTemplate(layout)
                .add("NPC Host EventEntityID", "i32", known)
                .add("EventEntityID", "i32", important)
                .pointIndex2 = .fieldCount
                .add("RegionIndex2", "i32", important)
                .add("p+0x1C", "i32", unknown)
            ElseIf name = "points0" Then
                .add("&Name", "i32", advanced)
                .add("x04", "i32", unknown)
                .add("Index", "i32", known)
                .add("Type", "i32", advanced)
                .add("X", "f32", known)
                .add("Y", "f32", known)
                .add("Z", "f32", known)
                .add("RX", "f32", known)
                .add("RY", "f32", known)
                .add("RZ", "f32", known)
                .add("x28", "i32", advanced)
                .add("x2C", "i32", advanced)
                .add("x30", "i32", advanced)
                .add("x34", "i32", advanced)
                .setNameIndex(.fieldCount)
                .add("Name", "string", known)
                .add("p+0x00", "i32", unknown)
                .add("p+0x04", "i32", unknown)
                .add("EventEntityID", "i32", important)
            ElseIf name = "points2" Then
                .add("&Name", "i32", advanced)
                .add("x04", "i32", unknown)
                .add("Index", "i32", known)
                .add("Type", "i32", advanced)
                .add("X", "f32", known)
                .add("Y", "f32", known)
                .add("Z", "f32", known)
                .add("RX", "f32", known)
                .add("RY", "f32", known)
                .add("RZ", "f32", known)
                .add("x28", "i32", advanced)
                .add("x2C", "i32", advanced)
                .add("x30", "i32", advanced)
                .add("x34", "i32", advanced)
                .setNameIndex(.fieldCount)
                .add("Name", "string", known)
                .add("p+0x00", "i32", unknown)
                .add("p+0x04", "i32", unknown)
                .add("Radius", "f32", known)
                .add("EventEntityID", "i32", important)
            ElseIf name = "points3" Then
                .add("&Name", "i32", advanced)
                .add("x04", "i32", unknown)
                .add("Index", "i32", known)
                .add("Type", "i32", advanced)
                .add("X", "f32", known)
                .add("Y", "f32", known)
                .add("Z", "f32", known)
                .add("RX", "f32", known)
                .add("RY", "f32", known)
                .add("RZ", "f32", known)
                .add("x28", "i32", advanced)
                .add("x2C", "i32", advanced)
                .add("x30", "i32", advanced)
                .add("x34", "i32", advanced)
                .setNameIndex(.fieldCount)
                .add("Name", "string", known)
                .add("p+0x00", "i32", unknown)
                .add("p+0x04", "i32", unknown)
                .add("Radius", "f32", known)
                .add("Height", "f32", known)
                .add("EventEntityID", "i32", important)
            ElseIf name = "points5" Then
                .add("&Name", "i32", advanced)
                .add("x04", "i32", unknown)
                .add("Index", "i32", known)
                .add("Type", "i32", advanced)
                .add("X", "f32", known)
                .add("Y", "f32", known)
                .add("Z", "f32", known)
                .add("RX", "f32", known)
                .add("RY", "f32", known)
                .add("RZ", "f32", known)
                .add("x28", "i32", advanced)
                .add("x2C", "i32", advanced)
                .add("x30", "i32", advanced)
                .add("x34", "i32", advanced)
                .setNameIndex(.fieldCount)
                .add("Name", "string", known)
                .add("p+0x00", "i32", unknown)
                .add("p+0x04", "i32", unknown)
                .add("Length", "f32", known) 'MEOWTODO: Check if this is width, length, height instead of XYZ
                .add("Width", "f32", known)
                .add("Height", "f32", known)
                .add("EventEntityID", "i32", important)
            ElseIf name = "mapPieces0" Then
                .add("&Name", "i32", advanced)
                .add("Type", "i32", advanced)
                .add("Index", "i32", known)
                .add("Model", "i32", known)
                .add("&Placeholder Model", "i32", advanced)
                .add("X", "f32", known)
                .add("Y", "f32", known)
                .add("Z", "f32", known)
                .add("RX", "f32", known)
                .add("RY", "f32", known)
                .add("RZ", "f32", known)
                .add("SX", "f32", known)
                .add("SY", "f32", known)
                .add("SZ", "f32", known)
                .add("x38", "i32", unknown)
                .add("DrawGroup1", "i32", known)
                .add("DrawGroup2", "i32", known)
                .add("DrawGroup3", "i32", known)
                .add("DrawGroup4", "i32", known)
                .add("x4C", "i32", unknown)
                .add("x50", "i32", unknown)
                .add("x54", "i32", unknown)
                .add("x58", "i32", unknown)
                .add("x5C", "i32", unknown)
                .add("x60", "i32", unknown)
                .setNameIndex(.fieldCount)
                .add("Name", "string", known)
                .add("Placeholder Model", "string", advanced)
                .add("EventEntityID", "i32", important)
                .add("LightId", "i8", known)
                .add("FogId", "i8", known)
                .add("ScatId", "i8", known)
                .add("p+x07", "i8", unknown)
                .add("p+x08", "i8", unknown)
                .add("p+x09", "i8", unknown)
                .add("p+x0A", "i8", unknown)
                .add("p+x0B", "i8", unknown)
                .add("p+x0C", "i32", unknown)
                .add("p+x10", "i32", unknown)
                .add("p+x14", "i32", unknown)
                .add("p+x18", "i32", unknown)
                .add("p+x1C", "i32", unknown)
            ElseIf name = "objects1" Then
                .add("&Name", "i32", advanced)
                .add("Type", "i32", advanced)
                .add("Index", "i32", known)
                .add("Model", "i32", known)
                .add("&Placeholder Model", "i32", advanced)
                .add("X", "f32", known)
                .add("Y", "f32", known)
                .add("Z", "f32", known)
                .add("RX", "f32", known)
                .add("RY", "f32", known)
                .add("RZ", "f32", known)
                .add("SX", "f32", known)
                .add("SY", "f32", known)
                .add("SZ", "f32", known)
                .add("x38", "i32", unknown)
                .add("x3C", "i32", unknown)
                .add("x40", "i32", unknown)
                .add("x44", "i32", unknown)
                .add("x48", "i32", unknown)
                .add("x4C", "i32", unknown)
                .add("x50", "i32", unknown)
                .add("x54", "i32", unknown)
                .add("x58", "i32", unknown)
                .add("x5C", "i32", unknown)
                .add("x60", "i32", unknown)
                .setNameIndex(.fieldCount)
                .add("Name", "string", known)
                .add("Placeholder Model", "string", advanced)
                .add("EventEntityID", "i32", important)
                .add("LightId", "i8", known)
                .add("FogId", "i8", known)
                .add("ScatId", "i8", known)
                .add("p+x07", "i8", unknown)
                .add("p+x08", "i32", unknown)
                .add("p+x0C", "i8", unknown)
                .add("p+x0D", "i8", unknown)
                .add("p+x0E", "i8", unknown)
                .add("p+x0F", "i8", unknown)
                .add("p+x10", "i8", unknown)
                .add("p+x11", "i8", unknown)
                .add("p+x12", "i8", unknown)
                .add("p+x13", "i8", unknown)
                .add("p+x14", "i32", unknown)
                .add("p+x18", "i32", unknown)
                .partIndex1 = .fieldCount
                .add("PartIndex", "i32", important)
                .add("p+x20", "i32", unknown)
                .add("p+x24", "i32", unknown)
                .add("p+x28", "i32", unknown)
                .add("p+x2C", "i32", unknown)
            ElseIf name = "creatures2" Then
                .add("&Name", "i32", advanced)
                .add("Type", "i32", advanced)
                .add("Index", "i32", known)
                .add("Model", "i32", known)
                .add("&Placeholder Model", "i32", advanced)
                .add("X", "f32", known)
                .add("Y", "f32", known)
                .add("Z", "f32", known)
                .add("RX", "f32", known)
                .add("RY", "f32", known)
                .add("RZ", "f32", known)
                .add("SX", "f32", known)
                .add("SY", "f32", known)
                .add("SZ", "f32", known)
                .add("x38", "i32", unknown)
                .add("x3C", "i32", unknown)
                .add("x40", "i32", unknown)
                .add("x44", "i32", unknown)
                .add("x48", "i32", unknown)
                .add("x4C", "i32", unknown)
                .add("x50", "i32", unknown)
                .add("x54", "i32", unknown)
                .add("x58", "i32", unknown)
                .add("x5C", "i32", unknown)
                .add("x60", "i32", unknown)
                .setNameIndex(.fieldCount)
                .add("Name", "string", known)
                .add("Placeholder Model", "string", advanced)
                .add("EventEntityID", "i32", important)
                .add("LightId", "i8", known)
                .add("FogId", "i8", known)
                .add("ScatId", "i8", known)
                .add("p+x07", "i8", unknown)
                .add("p+x08", "i32", unknown)
                .add("p+x0C", "i32", unknown)
                .add("p+x10", "i32", unknown)
                .add("p+x14", "i32", unknown)
                .add("p+x18", "i32", unknown)
                .add("p+x1C", "i32", unknown)
                .add("ThinkParam", "i32", known)
                .add("NPCParam", "i32", known)
                .add("TalkID", "i32", known)
                .add("p+x2C", "i32", unknown)
                .add("ChrInitParam", "i32", known)
                .partIndex1 = .fieldCount
                .add("PartIndex", "i32", important)
                .add("p+x38", "i32", unknown)
                .add("p+x3C", "i32", unknown)
                .add("p+x40", "i32", unknown)
                .add("p+x44", "i32", unknown)
                .add("p+x48", "i32", unknown)
                .add("p+x4C", "i32", unknown)
                .add("InitAnimID", "i32", known)
                .add("p+x54", "i32", unknown)
            ElseIf name = "creatures4" Then
                .add("&Name", "i32", advanced)
                .add("Type", "i32", advanced)
                .add("Index", "i32", known)
                .add("Model", "i32", known)
                .add("&Placeholder Model", "i32", advanced)
                .add("X", "f32", known)
                .add("Y", "f32", known)
                .add("Z", "f32", known)
                .add("RX", "f32", known)
                .add("RY", "f32", known)
                .add("RZ", "f32", known)
                .add("SX", "f32", known)
                .add("SY", "f32", known)
                .add("SZ", "f32", known)
                .add("x38", "i32", unknown)
                .add("x3C", "i32", unknown)
                .add("x40", "i32", unknown)
                .add("x44", "i32", unknown)
                .add("x48", "i32", unknown)
                .add("x4C", "i32", unknown)
                .add("x50", "i32", unknown)
                .add("x54", "i32", unknown)
                .add("x58", "i32", unknown)
                .add("x5C", "i32", unknown)
                .add("x60", "i32", unknown)
                .setNameIndex(.fieldCount)
                .add("Name", "string", known)
                .add("Placeholder Model", "string", advanced)
                .add("EventEntityID", "i32", important)
                .add("LightId", "i8", known)
                .add("FogId", "i8", known)
                .add("ScatId", "i8", known)
                .add("p+x07", "i8", unknown)
                .add("p+x08", "i32", unknown)
                .add("p+x0C", "i32", unknown)
                .add("p+x10", "i32", unknown)
                .add("p+x14", "i32", unknown)
                .add("p+x18", "i32", unknown)
                .add("p+x1C", "i32", unknown)
                .add("p+x20", "i32", unknown)
                .add("p+x24", "i32", unknown)
            ElseIf name = "collision5" Then
                .add("&Name", "i32", advanced)
                .add("Type", "i32", advanced)
                .add("Index", "i32", known)
                .add("Model", "i32", known)
                .add("&Placeholder Model", "i32", advanced)
                .add("X", "f32", known)
                .add("Y", "f32", known)
                .add("Z", "f32", known)
                .add("RX", "f32", known)
                .add("RY", "f32", known)
                .add("RZ", "f32", known)
                .add("SX", "f32", known)
                .add("SY", "f32", known)
                .add("SZ", "f32", known)
                .add("x38", "i32", unknown)
                .add("x3C", "i32", unknown)
                .add("x40", "i32", unknown)
                .add("x44", "i32", unknown)
                .add("x48", "i32", unknown)
                .add("x4C", "i32", unknown)
                .add("x50", "i32", unknown)
                .add("x54", "i32", unknown)
                .add("x58", "i32", unknown)
                .add("x5C", "i32", unknown)
                .add("x60", "i32", unknown)
                .setNameIndex(.fieldCount)
                .add("Name", "string", known)
                .add("Placeholder Model", "string", advanced)
                .add("EventEntityID", "i32", important)
                .add("p+x04", "i8", unknown)
                .add("p+x05", "i8", unknown)
                .add("p+x06", "i8", unknown)
                .add("p+x07", "i8", unknown)
                .add("p+x08", "i8", unknown)
                .add("p+x09", "i8", unknown)
                .add("p+x0A", "i8", unknown)
                .add("p+x0B", "i8", unknown)
                .add("p+x0C", "i8", unknown)
                .add("p+x0D", "i8", unknown)
                .add("p+x0E", "i8", unknown)
                .add("p+x0F", "i8", unknown)
                .add("p+x10", "i8", unknown)
                .add("p+x11", "i8", unknown)
                .add("p+x12", "i8", unknown)
                .add("p+x13", "i8", unknown)
                .add("p+x14", "i8", unknown)
                .add("p+x15", "i8", unknown)
                .add("p+x16", "i8", unknown)
                .add("p+x17", "i8", unknown)
                .add("p+x18", "i8", unknown)
                .add("p+x19", "i8", unknown)
                .add("p+x1A", "i8", unknown)
                .add("p+x1B", "i8", unknown)
                .add("p+x1C", "i8", unknown)
                .add("p+x1D", "i8", unknown)
                .add("p+x1E", "i8", unknown)
                .add("p+x1F", "i8", unknown)
                .add("p+x20", "i32", unknown)
                .add("p+x24", "i32", unknown)
                .add("p+x28", "i32", unknown)
                .add("p+x2C", "i32", unknown)
                .add("p+x30", "i32", unknown)
                .add("p+x34", "i32", unknown)
                .add("p+x38", "i32", unknown)
                .add("p+x3C", "i16", unknown)
                .add("p+x3E", "i16", unknown)
                .add("p+x40", "i32", unknown)
                .add("p+x44", "i32", unknown)
                .add("p+x48", "i32", unknown)
                .add("p+x4C", "i32", unknown)
                .add("p+x50", "i32", unknown)
                .add("p+x54", "i16", unknown)
                .add("p+x56", "i16", unknown)
                .add("p+x58", "i32", unknown)
                .add("p+x5C", "i32", unknown)
                .add("p+x60", "i32", unknown)
                .add("p+x64", "i32", unknown)
            ElseIf name = "navimesh8" Then
                .add("&Name", "i32", advanced)
                .add("Type", "i32", advanced)
                .add("Index", "i32", known)
                .add("Model", "i32", known)
                .add("&Placeholder Model", "i32", advanced)
                .add("X", "f32", known)
                .add("Y", "f32", known)
                .add("Z", "f32", known)
                .add("RX", "f32", known)
                .add("RY", "f32", known)
                .add("RZ", "f32", known)
                .add("SX", "f32", known)
                .add("SY", "f32", known)
                .add("SZ", "f32", known)
                .add("x38", "i32", unknown)
                .add("DrawGroup1", "i32", known)
                .add("DrawGroup2", "i32", known)
                .add("DrawGroup3", "i32", known)
                .add("DrawGroup4", "i32", known)
                .add("x4C", "i32", unknown)
                .add("x50", "i32", unknown)
                .add("x54", "i32", unknown)
                .add("x58", "i32", unknown)
                .add("x5C", "i32", unknown)
                .add("x60", "i32", unknown)
                .setNameIndex(.fieldCount)
                .add("Name", "string", known)
                .add("Placeholder Model", "string", advanced)
                .add("EventEntityID", "i32", important)
                .add("LightId", "i8", known)
                .add("FogId", "i8", known)
                .add("ScatId", "i8", known)
                .add("p+x07", "i8", unknown)
                .add("p+x08", "i8", unknown)
                .add("p+x09", "i8", unknown)
                .add("p+x0A", "i8", unknown)
                .add("p+x0B", "i8", unknown)
                .add("p+x0C", "i32", unknown)
                .add("p+x10", "i32", unknown)
                .add("p+x14", "i32", unknown)
                .add("p+x18", "i32", unknown)
                .add("p+x1C", "i32", unknown)
                .add("p+x20", "i32", unknown)
                .add("p+x24", "i32", unknown)
                .add("p+x28", "i32", unknown)
                .add("p+x2C", "i32", unknown)
                .add("p+x30", "i32", unknown)
                .add("p+x34", "i32", unknown)
            ElseIf name = "objects9" Then
                .add("&Name", "i32", advanced)
                .add("Type", "i32", advanced)
                .add("Index", "i32", known)
                .add("Model", "i32", known)
                .add("&Placeholder Model", "i32", advanced)
                .add("X", "f32", known)
                .add("Y", "f32", known)
                .add("Z", "f32", known)
                .add("RX", "f32", known)
                .add("RY", "f32", known)
                .add("RZ", "f32", known)
                .add("SX", "f32", known)
                .add("SY", "f32", known)
                .add("SZ", "f32", known)
                .add("x38", "i32", unknown)
                .add("x3C", "i32", unknown)
                .add("x40", "i32", unknown)
                .add("x44", "i32", unknown)
                .add("x48", "i32", unknown)
                .add("x4C", "i32", unknown)
                .add("x50", "i32", unknown)
                .add("x54", "i32", unknown)
                .add("x58", "i32", unknown)
                .add("x5C", "i32", unknown)
                .add("x60", "i32", unknown)
                .setNameIndex(.fieldCount)
                .add("Name", "string", known)
                .add("Placeholder Model", "string", advanced)
                .add("EventEntityID", "i32", important)
                .add("p+x04", "i8", unknown)
                .add("p+x05", "i8", unknown)
                .add("p+x06", "i8", unknown)
                .add("p+x07", "i8", unknown)
                .add("p+x08", "i32", unknown)
                .add("p+x0C", "i8", unknown)
                .add("p+x0D", "i8", unknown)
                .add("p+x0E", "i8", unknown)
                .add("p+x0F", "i8", unknown)
                .add("p+x10", "i8", unknown)
                .add("p+x11", "i8", unknown)
                .add("p+x12", "i8", unknown)
                .add("p+x13", "i8", unknown)
                .add("p+x14", "i32", unknown)
                .add("p+x18", "i32", unknown)
                .partIndex1 = .fieldCount
                .add("PartIndex", "i32", important)
                .add("p+x20", "i32", unknown)
                .add("p+x24", "i32", unknown)
                .add("p+x28", "i32", unknown)
                .add("p+x2C", "i32", unknown)
            ElseIf name = "creatures10" Then
                .add("&Name", "i32", advanced)
                .add("Type", "i32", advanced)
                .add("Index", "i32", known)
                .add("Model", "i32", known)
                .add("&Placeholder Model", "i32", advanced)
                .add("X", "f32", known)
                .add("Y", "f32", known)
                .add("Z", "f32", known)
                .add("RX", "f32", known)
                .add("RY", "f32", known)
                .add("RZ", "f32", known)
                .add("SX", "f32", known)
                .add("SY", "f32", known)
                .add("SZ", "f32", known)
                .add("x38", "i32", unknown)
                .add("x3C", "i32", unknown)
                .add("x40", "i32", unknown)
                .add("x44", "i32", unknown)
                .add("x48", "i32", unknown)
                .add("x4C", "i32", unknown)
                .add("x50", "i32", unknown)
                .add("x54", "i32", unknown)
                .add("x58", "i32", unknown)
                .add("x5C", "i32", unknown)
                .add("x60", "i32", unknown)
                .setNameIndex(.fieldCount)
                .add("Name", "string", known)
                .add("Placeholder Model", "string", advanced)
                .add("EventEntityID", "i32", important)
                .add("LightId", "i8", known)
                .add("FogId", "i8", known)
                .add("ScatId", "i8", known)
                .add("p+x07", "i8", unknown)
                .add("p+x08", "i32", unknown)
                .add("p+x0C", "i32", unknown)
                .add("p+x10", "i32", unknown)
                .add("p+x14", "i32", unknown)
                .add("p+x18", "i32", unknown)
                .add("p+x1C", "i32", unknown)
                .add("p+x20", "i32", unknown)
                .add("NPC ID", "i32", known)
                .add("p+x28", "i32", unknown)
                .add("p+x2C", "i32", unknown)
                .add("p+x30", "i32", unknown)
                .partIndex1 = .fieldCount
                .add("PartIndex", "i32", important)
                .add("p+x38", "i32", unknown)
                .add("p+x3C", "i32", unknown)
                .add("p+x40", "i32", unknown)
                .add("p+x44", "i32", unknown)
                .add("p+x48", "i32", unknown)
                .add("p+x4C", "i32", unknown)
                .add("AnimID", "i32", known)
                .add("p+x54", "i32", unknown)
            ElseIf name = "collision11" Then
                .add("&Name", "i32", advanced)
                .add("Type", "i32", advanced)
                .add("Index", "i32", known)
                .add("Model", "i32", known)
                .add("&Placeholder Model", "i32", advanced)
                .add("X", "f32", known)
                .add("Y", "f32", known)
                .add("Z", "f32", known)
                .add("RX", "f32", known)
                .add("RY", "f32", known)
                .add("RZ", "f32", known)
                .add("SX", "f32", known)
                .add("SY", "f32", known)
                .add("SZ", "f32", known)
                .add("x38", "i32", unknown)
                .add("x3C", "i32", unknown)
                .add("x40", "i32", unknown)
                .add("x44", "i32", unknown)
                .add("x48", "i32", unknown)
                .add("x4C", "i32", unknown)
                .add("x50", "i32", unknown)
                .add("x54", "i32", unknown)
                .add("x58", "i32", unknown)
                .add("x5C", "i32", unknown)
                .add("x60", "i32", unknown)
                .setNameIndex(.fieldCount)
                .add("Name", "string", known)
                .add("Placeholder Model", "string", advanced)
                .add("EventEntityID", "i32", important)
                .add("p+x04", "i8", unknown)
                .add("p+x05", "i8", unknown)
                .add("p+x06", "i8", unknown)
                .add("p+x07", "i8", unknown)
                .add("p+x08", "i8", unknown)
                .add("p+x09", "i8", unknown)
                .add("p+x0A", "i8", unknown)
                .add("p+x0B", "i8", unknown)
                .add("p+x0C", "i8", unknown)
                .add("p+x0D", "i8", unknown)
                .add("p+x0E", "i8", unknown)
                .add("p+x0F", "i8", unknown)
                .add("p+x10", "i8", unknown)
                .add("p+x11", "i8", unknown)
                .add("p+x12", "i8", unknown)
                .add("p+x13", "i8", unknown)
                .add("p+x14", "i8", unknown)
                .add("p+x15", "i8", unknown)
                .add("p+x16", "i8", unknown)
                .add("p+x17", "i8", unknown)
                .add("p+x18", "i8", unknown)
                .add("p+x19", "i8", unknown)
                .add("p+x1A", "i8", unknown)
                .add("p+x1B", "i8", unknown)
                .add("p+x1C", "i16", unknown)
                .add("p+x1E", "i16", unknown)
                .add("p+x20", "i32", unknown)
                .add("p+x24", "i32", unknown)
            Else
                Throw New Exception()
            End If
        End With

        Return layout
    End Function
End Class
