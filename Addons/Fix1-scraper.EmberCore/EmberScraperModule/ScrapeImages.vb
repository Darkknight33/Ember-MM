Imports EmberAPI
Imports EmberAPI.FileUtils
Imports EmberAPI.MediaContainers
Imports EmberScraperModule.IMPA
Imports EmberScraperModule.MPDB
Imports EmberScraperModule.TMDB
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Linq
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions

Namespace EmberScraperModule
    Public Class ScrapeImages
        ' Methods
        <CompilerGenerated> _
        Private Shared Function _Lambda$__37(ByVal i As Image) As Integer
            Return (i.WebImage.Image.Height + i.WebImage.Image.Height)
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__38(ByVal MI As Image) As Boolean
            Return (MI.Description = "original")
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__39(ByVal MI As Image) As Boolean
            Return (MI.Description = "mid")
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__40(ByVal MI As Image) As Boolean
            Return (MI.Description = "cover")
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__41(ByVal MI As Image) As Boolean
            Return (MI.Description = "thumb")
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__42(ByVal i As Image) As Integer
            Return (i.WebImage.Image.Height + i.WebImage.Image.Height)
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__43(ByVal MI As Image) As Boolean
            Return (MI.Description = "original")
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__44(ByVal MI As Image) As Boolean
            Return (MI.Description = "mid")
        End Function

        <CompilerGenerated> _
        Private Shared Function _Lambda$__45(ByVal MI As Image) As Boolean
            Return (MI.Description = "thumb")
        End Function

        Private Shared Function GetEncoderInfo(ByVal Format As ImageFormat) As ImageCodecInfo
            Dim imageEncoders As ImageCodecInfo() = ImageCodecInfo.GetImageEncoders
            Dim num2 As Integer = Information.UBound(imageEncoders, 1)
            Dim i As Integer = 0
            Do While (i <= num2)
                If (imageEncoders(i).FormatID = Format.Guid) Then
                    Return imageEncoders(i)
                End If
                i += 1
            Loop
            Return Nothing
        End Function

        Public Shared Sub GetPreferredFAasET(ByVal IMDBID As String, ByVal sPath As String)
            Dim scraper3 As New Scraper
            Dim scraper As New Scraper
            Dim scraper2 As New Scraper
            If AdvancedSettings.GetBooleanSetting("UseTMDB", True, "") Then
                Dim image As Image
                Dim tMDBImages As New List(Of Image)
                Dim list As New List(Of String)
                Dim path As String = String.Concat(New String() { Master.TempPath, Conversions.ToString(Path.DirectorySeparatorChar), IMDBID, Conversions.ToString(Path.DirectorySeparatorChar), "fanart" })
                If Master.eSettings.AutoET Then
                    list = HashFile.CurrentETHashes(sPath)
                End If
                If Master.eSettings.UseImgCacheUpdaters Then
                    Dim list3 As New List(Of FileInfo)
                    If Not Directory.Exists(path) Then
                        Directory.CreateDirectory(path)
                    Else
                        Dim info As New DirectoryInfo(path)
                        Try 
                            list3.AddRange(info.GetFiles("*.jpg"))
                        Catch exception1 As Exception
                            ProjectData.SetProjectError(exception1)
                            ProjectData.ClearProjectError
                        End Try
                    End If
                    If (list3.Count > 0) Then
                        Dim info2 As FileInfo
                        For Each info2 In list3
                            Dim flag As Boolean = True
                            If (flag = info2.Name.Contains("(original)")) Then
                                If ((Master.eSettings.AutoET AndAlso (Master.eSettings.AutoETSize = FanartSize.Lrg)) AndAlso Not list.Contains(HashFile.HashCalcFile(info2.FullName))) Then
                                    ScrapeImages.SaveFAasET(info2.FullName, sPath)
                                End If
                            ElseIf (flag = info2.Name.Contains("(mid)")) Then
                                If ((Master.eSettings.AutoET AndAlso (Master.eSettings.AutoETSize = FanartSize.Mid)) AndAlso Not list.Contains(HashFile.HashCalcFile(info2.FullName))) Then
                                    ScrapeImages.SaveFAasET(info2.FullName, sPath)
                                End If
                            ElseIf (((flag = info2.Name.Contains("(thumb)")) AndAlso (Master.eSettings.AutoET AndAlso (Master.eSettings.AutoETSize = FanartSize.Small))) AndAlso Not list.Contains(HashFile.HashCalcFile(info2.FullName))) Then
                                ScrapeImages.SaveFAasET(info2.FullName, sPath)
                            End If
                        Next
                    Else
                        tMDBImages = scraper3.GetTMDBImages(IMDBID, "backdrop")
                        If (tMDBImages.Count > 0) Then
                            Dim image2 As Image
                            For Each image2 In tMDBImages
                                image2.WebImage.FromWeb(image2.URL)
                                If Not Information.IsNothing(image2.WebImage.Image) Then
                                    image = image2.WebImage.Image
                                    Dim str3 As String = Path.Combine(path, String.Concat(New String() { "fanart_(", image2.Description, ")_(url=", StringUtils.CleanURL(image2.URL), ").jpg" }))
                                    ScrapeImages.Save(image, str3, 0)
                                    If Master.eSettings.AutoET Then
                                        Dim str5 As String = image2.Description.ToLower
                                        Select Case str5
                                            Case "original"
                                                If ((Master.eSettings.AutoETSize = FanartSize.Lrg) AndAlso Not list.Contains(HashFile.HashCalcFile(str3))) Then
                                                    ScrapeImages.SaveFAasET(str3, sPath)
                                                End If
                                                Continue
                                            Case "mid"
                                                If ((Master.eSettings.AutoETSize = FanartSize.Mid) AndAlso Not list.Contains(HashFile.HashCalcFile(str3))) Then
                                                    ScrapeImages.SaveFAasET(str3, sPath)
                                                End If
                                                Continue
                                        End Select
                                        If (((str5 = "thumb") AndAlso (Master.eSettings.AutoETSize = FanartSize.Small)) AndAlso Not list.Contains(HashFile.HashCalcFile(str3))) Then
                                            ScrapeImages.SaveFAasET(str3, sPath)
                                        End If
                                    End If
                                End If
                            Next
                        End If
                    End If
                Else
                    tMDBImages = scraper3.GetTMDBImages(IMDBID, "backdrop")
                    If (tMDBImages.Count > 0) Then
                        If Not Directory.Exists(path) Then
                            Directory.CreateDirectory(path)
                        End If
                        Dim str4 As String = String.Empty
                        Dim image3 As Image
                        For Each image3 In tMDBImages
                            Dim str6 As String = image3.Description.ToLower
                            Select Case str6
                                Case "original"
                                    If (Master.eSettings.AutoETSize = FanartSize.Lrg) Then
                                        image3.WebImage.FromWeb(image3.URL)
                                        If Not Information.IsNothing(image3.WebImage.Image) Then
                                            image = image3.WebImage.Image
                                            str4 = Path.Combine(path, String.Concat(New String() { "fanart_(", image3.Description, ")_(url=", StringUtils.CleanURL(image3.URL), ").jpg" }))
                                            ScrapeImages.Save(image, str4, 0)
                                            If Not list.Contains(HashFile.HashCalcFile(str4)) Then
                                                ScrapeImages.SaveFAasET(str4, sPath)
                                            End If
                                        End If
                                    End If
                                    Exit Select
                                Case "mid"
                                    If (Master.eSettings.AutoETSize = FanartSize.Mid) Then
                                        image3.WebImage.FromWeb(image3.URL)
                                        If Not Information.IsNothing(image3.WebImage.Image) Then
                                            image = image3.WebImage.Image
                                            str4 = Path.Combine(path, String.Concat(New String() { "fanart_(", image3.Description, ")_(url=", StringUtils.CleanURL(image3.URL), ").jpg" }))
                                            ScrapeImages.Save(image, str4, 0)
                                            If Not list.Contains(HashFile.HashCalcFile(str4)) Then
                                                ScrapeImages.SaveFAasET(str4, sPath)
                                            End If
                                        End If
                                    End If
                                    Exit Select
                                Case Else
                                    If ((str6 = "thumb") AndAlso (Master.eSettings.AutoETSize = FanartSize.Small)) Then
                                        image3.WebImage.FromWeb(image3.URL)
                                        If Not Information.IsNothing(image3.WebImage.Image) Then
                                            image = image3.WebImage.Image
                                            str4 = Path.Combine(path, String.Concat(New String() { "fanart_(", image3.Description, ")_(url=", StringUtils.CleanURL(image3.URL), ").jpg" }))
                                            ScrapeImages.Save(image, str4, 0)
                                            If Not list.Contains(HashFile.HashCalcFile(str4)) Then
                                                ScrapeImages.SaveFAasET(str4, sPath)
                                            End If
                                        End If
                                    End If
                                    Exit Select
                            End Select
                        Next
                        image = Nothing
                        Delete.DeleteDirectory(path)
                    End If
                End If
            End If
        End Sub

        Public Shared Function GetPreferredImage(ByRef Image As Images, ByVal IMDBID As String, ByVal iType As ImageType, ByRef imgResult As ImgResult, ByVal sPath As String, ByVal doETs As Boolean, ByVal Optional doAsk As Boolean = False) As Boolean
            Dim flag2 As Boolean = False
            Dim scraper3 As New Scraper
            Dim scraper As New Scraper
            Dim scraper2 As New Scraper
            Dim source As New List(Of Image)
            Dim iMPAPosters As New List(Of Image)
            Dim mPDBPosters As New List(Of Image)
            Dim expression As Image = Nothing
            Dim image As Image = Nothing
            Dim image2 As Image = Nothing
            Dim image3 As Image = Nothing
            Dim image4 As Image = Nothing
            Dim image10 As Image = Nothing
            Dim image6 As Image = Nothing
            Dim image7 As Image = Nothing
            Dim image8 As Image = Nothing
            Dim image9 As Image = Nothing
            Dim path As String = String.Concat(New String() { Master.TempPath, Conversions.ToString(Path.DirectorySeparatorChar), IMDBID, Conversions.ToString(Path.DirectorySeparatorChar), If((iType = ImageType.Posters), "posters", "fanart") })
            Try 
                Dim thumb As Thumb
                Dim num3 As Integer
                If (iType <> ImageType.Posters) Then
                    goto Label_0ED0
                End If
                If Not Master.eSettings.UseImgCacheUpdaters Then
                    goto Label_0548
                End If
                Dim list4 As New List(Of FileInfo)
                If Not Directory.Exists(path) Then
                    Directory.CreateDirectory(path)
                Else
                    Dim info As New DirectoryInfo(path)
                    Try 
                        list4.AddRange(info.GetFiles("*.jpg"))
                    Catch exception1 As Exception
                        ProjectData.SetProjectError(exception1)
                        ProjectData.ClearProjectError
                    End Try
                End If
                If (list4.Count > 0) Then
                    Dim info2 As FileInfo
                    For Each info2 In list4
                        Dim item As New Image
                        item.WebImage.FromFile(info2.FullName)
                        Dim flag3 As Boolean = True
                        If (flag3 = info2.Name.Contains("(original)")) Then
                            item.Description = "original"
                        ElseIf (flag3 = info2.Name.Contains("(mid)")) Then
                            item.Description = "mid"
                        ElseIf (flag3 = info2.Name.Contains("(cover)")) Then
                            item.Description = "cover"
                        ElseIf (flag3 = info2.Name.Contains("(thumb)")) Then
                            item.Description = "thumb"
                        ElseIf (flag3 = info2.Name.Contains("(poster)")) Then
                            item.Description = "poster"
                        End If
                        item.URL = Regex.Match(info2.Name, "\(url=(.*?)\)").Groups.Item(1).ToString
                        If Not Master.eSettings.NoSaveImagesToNfo Then
                            imgResult.Posters.Add(item.URL)
                        End If
                        source.Add(item)
                        Image.Clear
                    Next
                Else
                    If AdvancedSettings.GetBooleanSetting("UseTMDB", True, "") Then
                        source.AddRange(scraper3.GetTMDBImages(IMDBID, "poster"))
                    End If
                    If AdvancedSettings.GetBooleanSetting("UseIMPA", False, "") Then
                        source.AddRange(scraper.GetIMPAPosters(IMDBID))
                    End If
                    If AdvancedSettings.GetBooleanSetting("UseMPDB", False, "") Then
                        source.AddRange(scraper2.GetMPDBPosters(IMDBID))
                    End If
                    Dim image12 As Image
                    For Each image12 In source
                        image12.WebImage.FromWeb(image12.URL)
                        If Not Information.IsNothing(image12.WebImage.Image) Then
                            If Not Master.eSettings.NoSaveImagesToNfo Then
                                imgResult.Posters.Add(image12.URL)
                            End If
                            Image.Image = image12.WebImage.Image
                            Image.Save(Path.Combine(path, String.Concat(New String() { "poster_(", image12.Description, ")_(url=", StringUtils.CleanURL(image12.URL), ").jpg" })), 0)
                        End If
                        Image.Clear
                    Next
                End If
                If (source.Count <= 0) Then
                    goto Label_1D82
                End If
                flag2 = True
                Dim index As Integer = (source.Count - 1)
                goto Label_0490
            Label_0460:
                If Information.IsNothing(source.Item(index).WebImage.Image) Then
                    source.RemoveAt(index)
                End If
                index = (index + -1)
            Label_0490:
                num3 = 0
                If (index >= num3) Then
                    goto Label_0460
                End If
                Dim image13 As Image
                For Each image13 In source
                    If (Images.GetPosterDims(image13.WebImage.Image) = Master.eSettings.PreferredPosterSize) Then
                        Image.Image = image13.WebImage.Image
                        goto Label_1D82
                    End If
                Next
                If Not doAsk Then
                    Image.Image = source.OrderBy(Of Image, Integer)(New Func(Of Image, Integer)(AddressOf ScrapeImages._Lambda$__37)).FirstOrDefault(Of Image)().WebImage.Image
                End If
                goto Label_1D82
            Label_0548:
                If AdvancedSettings.GetBooleanSetting("UseTMDB", True, "") Then
                    source = scraper3.GetTMDBImages(IMDBID, "poster")
                    If (source.Count > 0) Then
                        flag2 = True
                        If Not Master.eSettings.NoSaveImagesToNfo Then
                            Dim image14 As Image
                            For Each image14 In source
                                imgResult.Posters.Add(image14.URL)
                            Next
                        End If
                        Dim image15 As Image
                        For Each image15 In source
                            Select Case Master.eSettings.PreferredPosterSize
                                Case PosterSize.Xlrg
                                    If (image15.Description.ToLower <> "original") Then
                                        Exit Select
                                    End If
                                    Image.FromWeb(image15.URL)
                                    If Information.IsNothing(Image.Image) Then
                                        Exit Select
                                    End If
                                    goto Label_1D82
                                Case PosterSize.Lrg
                                    If (image15.Description.ToLower <> "mid") Then
                                        Exit Select
                                    End If
                                    Image.FromWeb(image15.URL)
                                    If Information.IsNothing(Image.Image) Then
                                        Exit Select
                                    End If
                                    goto Label_1D82
                                Case PosterSize.Mid
                                    If (image15.Description.ToLower <> "cover") Then
                                        Exit Select
                                    End If
                                    Image.FromWeb(image15.URL)
                                    If Information.IsNothing(Image.Image) Then
                                        Exit Select
                                    End If
                                    goto Label_1D82
                                Case PosterSize.Small
                                    If (image15.Description.ToLower <> "thumb") Then
                                        Exit Select
                                    End If
                                    Image.FromWeb(image15.URL)
                                    If Information.IsNothing(Image.Image) Then
                                        Exit Select
                                    End If
                                    goto Label_1D82
                            End Select
                            Image.Clear
                        Next
                    End If
                End If
                If (AdvancedSettings.GetBooleanSetting("UseIMPA", False, "") AndAlso Information.IsNothing(Image.Image)) Then
                    iMPAPosters = scraper.GetIMPAPosters(IMDBID)
                    If (iMPAPosters.Count > 0) Then
                        flag2 = True
                        Dim image16 As Image
                        For Each image16 In iMPAPosters
                            Image.FromWeb(image16.URL)
                            If Not Information.IsNothing(Image.Image) Then
                                If Not Master.eSettings.NoSaveImagesToNfo Then
                                    imgResult.Posters.Add(image16.URL)
                                End If
                                Dim posterDims As PosterSize = Images.GetPosterDims(Image.Image)
                                If (posterDims = Master.eSettings.PreferredPosterSize) Then
                                    goto Label_1D82
                                End If
                                Select Case CInt(posterDims)
                                    Case 0
                                        If Information.IsNothing(expression) Then
                                            expression = New Bitmap(Image.Image)
                                        End If
                                        Exit Select
                                    Case 1
                                        If Information.IsNothing(image) Then
                                            image = New Bitmap(Image.Image)
                                        End If
                                        Exit Select
                                    Case 2
                                        If Information.IsNothing(image2) Then
                                            image2 = New Bitmap(Image.Image)
                                        End If
                                        Exit Select
                                    Case 3
                                        If Information.IsNothing(image3) Then
                                            image3 = New Bitmap(Image.Image)
                                        End If
                                        Exit Select
                                    Case 4
                                        If Information.IsNothing(image4) Then
                                            image4 = New Bitmap(Image.Image)
                                        End If
                                        Exit Select
                                End Select
                            End If
                            Image.Clear
                        Next
                    End If
                End If
                If (AdvancedSettings.GetBooleanSetting("UseMPDB", False, "") AndAlso Information.IsNothing(Image.Image)) Then
                    mPDBPosters = scraper2.GetMPDBPosters(IMDBID)
                    If (mPDBPosters.Count > 0) Then
                        flag2 = True
                        Dim image17 As Image
                        For Each image17 In mPDBPosters
                            Image.FromWeb(image17.URL)
                            If Not Information.IsNothing(Image.Image) Then
                                If Not Master.eSettings.NoSaveImagesToNfo Then
                                    imgResult.Posters.Add(image17.URL)
                                End If
                                Dim size2 As PosterSize = Images.GetPosterDims(Image.Image)
                                If (size2 = Master.eSettings.PreferredPosterSize) Then
                                    goto Label_1D82
                                End If
                                Select Case CInt(size2)
                                    Case 0
                                        If Information.IsNothing(image10) Then
                                            image10 = New Bitmap(Image.Image)
                                        End If
                                        Exit Select
                                    Case 1
                                        If Information.IsNothing(image6) Then
                                            image6 = New Bitmap(Image.Image)
                                        End If
                                        Exit Select
                                    Case 2
                                        If Information.IsNothing(image7) Then
                                            image7 = New Bitmap(Image.Image)
                                        End If
                                        Exit Select
                                    Case 3
                                        If Information.IsNothing(image8) Then
                                            image8 = New Bitmap(Image.Image)
                                        End If
                                        Exit Select
                                    Case 4
                                        If Information.IsNothing(image9) Then
                                            image9 = New Bitmap(Image.Image)
                                        End If
                                        Exit Select
                                End Select
                            End If
                            Image.Clear
                        Next
                    End If
                End If
                If (Information.IsNothing(Image.Image) AndAlso Not doAsk) Then
                    If (AdvancedSettings.GetBooleanSetting("UseTMDB", True, "") AndAlso (source.Count > 0)) Then
                        Dim enumerable4 As IEnumerable(Of Image) = source.Where(Of Image)(New Func(Of Image, Boolean)(AddressOf ScrapeImages._Lambda$__38))
                        If (enumerable4.Count(Of Image)() > 0) Then
                            Image.FromWeb(enumerable4.ElementAtOrDefault(Of Image)(0).URL)
                            If Not Information.IsNothing(Image.Image) Then
                                goto Label_1D82
                            End If
                        End If
                        Dim enumerable As IEnumerable(Of Image) = source.Where(Of Image)(New Func(Of Image, Boolean)(AddressOf ScrapeImages._Lambda$__39))
                        If (enumerable.Count(Of Image)() > 0) Then
                            Image.FromWeb(enumerable.ElementAtOrDefault(Of Image)(0).URL)
                            If Not Information.IsNothing(Image.Image) Then
                                goto Label_1D82
                            End If
                        End If
                        Dim enumerable2 As IEnumerable(Of Image) = source.Where(Of Image)(New Func(Of Image, Boolean)(AddressOf ScrapeImages._Lambda$__40))
                        If (enumerable2.Count(Of Image)() > 0) Then
                            Image.FromWeb(enumerable2.ElementAtOrDefault(Of Image)(0).URL)
                            If Not Information.IsNothing(Image.Image) Then
                                goto Label_1D82
                            End If
                        End If
                        Dim enumerable3 As IEnumerable(Of Image) = source.Where(Of Image)(New Func(Of Image, Boolean)(AddressOf ScrapeImages._Lambda$__41))
                        If (enumerable3.Count(Of Image)() > 0) Then
                            Image.FromWeb(enumerable3.ElementAtOrDefault(Of Image)(0).URL)
                            If Not Information.IsNothing(Image.Image) Then
                                goto Label_1D82
                            End If
                        End If
                    End If
                    Image.Clear
                    If (AdvancedSettings.GetBooleanSetting("UseIMPA", False, "") AndAlso (iMPAPosters.Count > 0)) Then
                        If Not Information.IsNothing(expression) Then
                            Image.Image = New Bitmap(expression)
                            goto Label_1D82
                        End If
                        If Not Information.IsNothing(image) Then
                            Image.Image = New Bitmap(image)
                            goto Label_1D82
                        End If
                        If Not Information.IsNothing(image2) Then
                            Image.Image = New Bitmap(image2)
                            goto Label_1D82
                        End If
                        If Not Information.IsNothing(image3) Then
                            Image.Image = New Bitmap(image3)
                            goto Label_1D82
                        End If
                        If Not Information.IsNothing(image4) Then
                            Image.Image = New Bitmap(image4)
                            goto Label_1D82
                        End If
                    End If
                    Image.Clear
                    If (AdvancedSettings.GetBooleanSetting("UseMPDB", False, "") AndAlso (mPDBPosters.Count > 0)) Then
                        If Not Information.IsNothing(image10) Then
                            Image.Image = New Bitmap(image10)
                            goto Label_1D82
                        End If
                        If Not Information.IsNothing(image6) Then
                            Image.Image = New Bitmap(image6)
                            goto Label_1D82
                        End If
                        If Not Information.IsNothing(image7) Then
                            Image.Image = New Bitmap(image7)
                            goto Label_1D82
                        End If
                        If Not Information.IsNothing(image8) Then
                            Image.Image = New Bitmap(image8)
                            goto Label_1D82
                        End If
                        If Not Information.IsNothing(image9) Then
                            Image.Image = New Bitmap(image9)
                            goto Label_1D82
                        End If
                    End If
                    Image.Clear
                End If
                goto Label_1D82
            Label_0ED0:
                If Not AdvancedSettings.GetBooleanSetting("UseTMDB", True, "") Then
                    goto Label_1D82
                End If
                Dim list5 As New List(Of String)
                If (Master.eSettings.AutoET AndAlso doETs) Then
                    list5 = HashFile.CurrentETHashes(sPath)
                End If
                If Not Master.eSettings.UseImgCacheUpdaters Then
                    goto Label_15DF
                End If
                Dim list6 As New List(Of FileInfo)
                If Not Directory.Exists(path) Then
                    Directory.CreateDirectory(path)
                Else
                    Dim info3 As New DirectoryInfo(path)
                    Try 
                        list6.AddRange(info3.GetFiles("*.jpg"))
                    Catch exception2 As Exception
                        ProjectData.SetProjectError(exception2)
                        ProjectData.ClearProjectError
                    End Try
                End If
                If (list6.Count > 0) Then
                    Dim info4 As FileInfo
                    For Each info4 In list6
                        Dim image18 As New Image
                        image18.WebImage.FromFile(info4.FullName)
                        Dim flag4 As Boolean = True
                        If (flag4 = info4.Name.Contains("(original)")) Then
                            image18.Description = "original"
                            If (((Master.eSettings.AutoET AndAlso doETs) AndAlso (Master.eSettings.AutoETSize = FanartSize.Lrg)) AndAlso Not list5.Contains(HashFile.HashCalcFile(info4.FullName))) Then
                                Image.SaveFAasET(info4.FullName, sPath)
                            End If
                        ElseIf (flag4 = info4.Name.Contains("(mid)")) Then
                            image18.Description = "mid"
                            If (((Master.eSettings.AutoET AndAlso doETs) AndAlso (Master.eSettings.AutoETSize = FanartSize.Mid)) AndAlso Not list5.Contains(HashFile.HashCalcFile(info4.FullName))) Then
                                Image.SaveFAasET(info4.FullName, sPath)
                            End If
                        ElseIf (flag4 = info4.Name.Contains("(thumb)")) Then
                            image18.Description = "thumb"
                            If (((Master.eSettings.AutoET AndAlso doETs) AndAlso (Master.eSettings.AutoETSize = FanartSize.Small)) AndAlso Not list5.Contains(HashFile.HashCalcFile(info4.FullName))) Then
                                Image.SaveFAasET(info4.FullName, sPath)
                            End If
                        End If
                        image18.URL = Regex.Match(info4.Name, "\(url=(.*?)\)").Groups.Item(1).ToString
                        source.Add(image18)
                        Image.Clear
                    Next
                Else
                    source = scraper3.GetTMDBImages(IMDBID, "backdrop")
                    If (source.Count > 0) Then
                        Dim str2 As String = String.Empty
                        imgResult.Fanart.URL = "http://images.themoviedb.org"
                        Dim image19 As Image
                        For Each image19 In source
                            image19.WebImage.FromWeb(image19.URL)
                            If Information.IsNothing(image19.WebImage.Image) Then
                                goto Label_149F
                            End If
                            Image.Image = image19.WebImage.Image
                            Dim str3 As String = Path.Combine(path, String.Concat(New String() { "fanart_(", image19.Description, ")_(url=", StringUtils.CleanURL(image19.URL), ").jpg" }))
                            Image.Save(str3, 0)
                            If (Master.eSettings.AutoET AndAlso doETs) Then
                                Dim str6 As String = image19.Description.ToLower
                                Select Case str6
                                    Case "original"
                                        If ((Master.eSettings.AutoETSize = FanartSize.Lrg) AndAlso Not list5.Contains(HashFile.HashCalcFile(str3))) Then
                                            Image.SaveFAasET(str3, sPath)
                                        End If
                                        goto Label_13CB
                                    Case "mid"
                                        If ((Master.eSettings.AutoETSize = FanartSize.Mid) AndAlso Not list5.Contains(HashFile.HashCalcFile(str3))) Then
                                            Image.SaveFAasET(str3, sPath)
                                        End If
                                        goto Label_13CB
                                End Select
                                If (((str6 = "thumb") AndAlso (Master.eSettings.AutoETSize = FanartSize.Small)) AndAlso Not list5.Contains(HashFile.HashCalcFile(str3))) Then
                                    Image.SaveFAasET(str3, sPath)
                                End If
                            End If
                        Label_13CB:
                            If (Not Master.eSettings.NoSaveImagesToNfo AndAlso Not image19.URL.Contains("_thumb.")) Then
                                str2 = image19.URL.Replace("http://images.themoviedb.org", String.Empty)
                                If str2.Contains("_poster.") Then
                                    str2 = str2.Replace("_poster.", "_thumb.")
                                Else
                                    str2 = str2.Insert(str2.LastIndexOf("."), "_thumb")
                                End If
                                thumb = New Thumb With { _
                                    .Preview = str2, _
                                    .Text = image19.URL.Replace("http://images.themoviedb.org", String.Empty) _
                                }
                                imgResult.Fanart.Thumb.Add(thumb)
                            End If
                        Label_149F:
                            Image.Clear
                        Next
                    End If
                End If
                If (source.Count <= 0) Then
                    goto Label_1D82
                End If
                flag2 = True
                Dim num2 As Integer = (source.Count - 1)
                goto Label_151F
            Label_14EF:
                If Information.IsNothing(source.Item(num2).WebImage.Image) Then
                    source.RemoveAt(num2)
                End If
                num2 = (num2 + -1)
            Label_151F:
                num3 = 0
                If (num2 >= num3) Then
                    goto Label_14EF
                End If
                Dim image20 As Image
                For Each image20 In source
                    If (Images.GetFanartDims(image20.WebImage.Image) = Master.eSettings.PreferredFanartSize) Then
                        Image.Image = image20.WebImage.Image
                        goto Label_1D82
                    End If
                Next
                Image.Clear
                If Not doAsk Then
                    Image.Image = source.OrderBy(Of Image, Integer)(New Func(Of Image, Integer)(AddressOf ScrapeImages._Lambda$__42)).FirstOrDefault(Of Image)().WebImage.Image
                End If
                goto Label_1D82
            Label_15DF:
                source = scraper3.GetTMDBImages(IMDBID, "backdrop")
                If (source.Count > 0) Then
                    flag2 = True
                    Dim str4 As String = String.Empty
                    If Not Master.eSettings.NoSaveImagesToNfo Then
                        imgResult.Fanart.URL = "http://images.themoviedb.org"
                        Dim image21 As Image
                        For Each image21 In source
                            If Not image21.URL.Contains("_thumb.") Then
                                str4 = image21.URL.Replace("http://images.themoviedb.org", String.Empty)
                                If str4.Contains("_poster.") Then
                                    str4 = str4.Replace("_poster.", "_thumb.")
                                Else
                                    str4 = str4.Insert(str4.LastIndexOf("."), "_thumb")
                                End If
                                thumb = New Thumb With { _
                                    .Preview = str4, _
                                    .Text = image21.URL.Replace("http://images.themoviedb.org", String.Empty) _
                                }
                                imgResult.Fanart.Thumb.Add(thumb)
                            End If
                        Next
                    End If
                    If (Master.eSettings.AutoET AndAlso doETs) Then
                        If Not Directory.Exists(path) Then
                            Directory.CreateDirectory(path)
                        End If
                        Dim str5 As String = String.Empty
                        Dim image22 As Image
                        For Each image22 In source
                            Dim str7 As String = image22.Description.ToLower
                            Select Case str7
                                Case "original"
                                    If (Master.eSettings.AutoETSize = FanartSize.Lrg) Then
                                        image22.WebImage.FromWeb(image22.URL)
                                        If Not Information.IsNothing(image22.WebImage.Image) Then
                                            Image.Image = image22.WebImage.Image
                                            str5 = Path.Combine(path, String.Concat(New String() { "fanart_(", image22.Description, ")_(url=", StringUtils.CleanURL(image22.URL), ").jpg" }))
                                            Image.Save(str5, 0)
                                            If Not list5.Contains(HashFile.HashCalcFile(str5)) Then
                                                Image.SaveFAasET(str5, sPath)
                                            End If
                                        End If
                                    End If
                                    Exit Select
                                Case "mid"
                                    If (Master.eSettings.AutoETSize = FanartSize.Mid) Then
                                        image22.WebImage.FromWeb(image22.URL)
                                        If Not Information.IsNothing(image22.WebImage.Image) Then
                                            Image.Image = image22.WebImage.Image
                                            str5 = Path.Combine(path, String.Concat(New String() { "fanart_(", image22.Description, ")_(url=", StringUtils.CleanURL(image22.URL), ").jpg" }))
                                            Image.Save(str5, 0)
                                            If Not list5.Contains(HashFile.HashCalcFile(str5)) Then
                                                Image.SaveFAasET(str5, sPath)
                                            End If
                                        End If
                                    End If
                                    Exit Select
                                Case Else
                                    If ((str7 = "thumb") AndAlso (Master.eSettings.AutoETSize = FanartSize.Small)) Then
                                        image22.WebImage.FromWeb(image22.URL)
                                        If Not Information.IsNothing(image22.WebImage.Image) Then
                                            Image.Image = image22.WebImage.Image
                                            str5 = Path.Combine(path, String.Concat(New String() { "fanart_(", image22.Description, ")_(url=", StringUtils.CleanURL(image22.URL), ").jpg" }))
                                            Image.Save(str5, 0)
                                            If Not list5.Contains(HashFile.HashCalcFile(str5)) Then
                                                Image.SaveFAasET(str5, sPath)
                                            End If
                                        End If
                                    End If
                                    Exit Select
                            End Select
                        Next
                        Image.Clear
                        Delete.DeleteDirectory(path)
                    End If
                    Dim image23 As Image
                    For Each image23 In source
                        Select Case Master.eSettings.PreferredFanartSize
                            Case FanartSize.Lrg
                                If (image23.Description.ToLower <> "original") Then
                                    Continue
                                End If
                                If Information.IsNothing(image23.WebImage.Image) Then
                                    Exit Select
                                End If
                                Image.Image = image23.WebImage.Image
                                goto Label_1D82
                            Case FanartSize.Mid
                                If (image23.Description.ToLower <> "mid") Then
                                    Continue
                                End If
                                If Information.IsNothing(image23.WebImage.Image) Then
                                    goto Label_1BB6
                                End If
                                Image.Image = image23.WebImage.Image
                                goto Label_1D82
                            Case FanartSize.Small
                                If (image23.Description.ToLower <> "thumb") Then
                                    Continue
                                End If
                                If Information.IsNothing(image23.WebImage.Image) Then
                                    goto Label_1C21
                                End If
                                Image.Image = image23.WebImage.Image
                                goto Label_1D82
                            Case Else
                                Continue
                        End Select
                        Image.FromWeb(image23.URL)
                        goto Label_1D82
                    Label_1BB6:
                        Image.FromWeb(image23.URL)
                        goto Label_1D82
                    Label_1C21:
                        Image.FromWeb(image23.URL)
                        goto Label_1D82
                    Next
                    Image.Clear
                    If (Information.IsNothing(Image.Image) AndAlso Not doAsk) Then
                        Dim enumerable5 As IEnumerable(Of Image) = source.Where(Of Image)(New Func(Of Image, Boolean)(AddressOf ScrapeImages._Lambda$__43))
                        If (enumerable5.Count(Of Image)() > 0) Then
                            Image.FromWeb(enumerable5.ElementAtOrDefault(Of Image)(0).URL)
                            goto Label_1D82
                        End If
                        Dim enumerable6 As IEnumerable(Of Image) = source.Where(Of Image)(New Func(Of Image, Boolean)(AddressOf ScrapeImages._Lambda$__44))
                        If (enumerable6.Count(Of Image)() > 0) Then
                            Image.FromWeb(enumerable6.ElementAtOrDefault(Of Image)(0).URL)
                            goto Label_1D82
                        End If
                        Dim enumerable7 As IEnumerable(Of Image) = source.Where(Of Image)(New Func(Of Image, Boolean)(AddressOf ScrapeImages._Lambda$__45))
                        If (enumerable7.Count(Of Image)() > 0) Then
                            Image.FromWeb(enumerable7.ElementAtOrDefault(Of Image)(0).URL)
                            goto Label_1D82
                        End If
                    End If
                    Image.Clear
                End If
            Catch exception3 As Exception
                ProjectData.SetProjectError(exception3)
                Dim exception As Exception = exception3
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
        Label_1D82:
            scraper3 = Nothing
            scraper = Nothing
            scraper2 = Nothing
            source = Nothing
            iMPAPosters = Nothing
            mPDBPosters = Nothing
            Return flag2
        End Function

        Public Function IsAllowedToDownload(ByVal mMovie As DBMovie, ByVal fType As ImageType, ByVal Optional isChange As Boolean = False) As Boolean
            Dim flag As Boolean
            Try 
                If (fType = ImageType.Fanart) Then
                    If (((Not isChange AndAlso Not String.IsNullOrEmpty(mMovie.FanartPath)) AndAlso Not Master.eSettings.OverwriteFanart) OrElse ((Not Master.eSettings.MovieNameDotFanartJPG AndAlso Not Master.eSettings.MovieNameFanartJPG) AndAlso Not Master.eSettings.FanartJPG)) Then
                    End If
                    Return AdvancedSettings.GetBooleanSetting("UseTMDB", True, "")
                End If
                If (((Not isChange AndAlso Not String.IsNullOrEmpty(mMovie.PosterPath)) AndAlso Not Master.eSettings.OverwritePoster) OrElse (((Not Master.eSettings.MovieTBN AndAlso Not Master.eSettings.MovieNameTBN) AndAlso (Not Master.eSettings.MovieJPG AndAlso Not Master.eSettings.MovieNameJPG)) AndAlso ((Not Master.eSettings.PosterTBN AndAlso Not Master.eSettings.PosterJPG) AndAlso Not Master.eSettings.FolderJPG))) Then
                End If
                If ((AdvancedSettings.GetBooleanSetting("UseIMPA", False, "") OrElse AdvancedSettings.GetBooleanSetting("UseMPDB", False, "")) OrElse AdvancedSettings.GetBooleanSetting("UseTMDB", True, "")) Then
                    Return True
                End If
                flag = False
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                flag = False
                ProjectData.ClearProjectError
                Return flag
                ProjectData.ClearProjectError
            End Try
            Return flag
        End Function

        Public Shared Sub Save(ByVal _image As Image, ByVal sPath As String, ByVal Optional iQuality As Long = 0)
            Try 
                If Not Information.IsNothing(_image) Then
                    Dim flag As Boolean = File.Exists(sPath)
                    Dim fileAttributes As New FileAttributes
                    If (Not String.IsNullOrEmpty(sPath) AndAlso (Not flag OrElse ((File.GetAttributes(sPath) And FileAttributes.ReadOnly) <= 0))) Then
                        If flag Then
                            fileAttributes = File.GetAttributes(sPath)
                            File.SetAttributes(sPath, FileAttributes.Normal)
                        End If
                        Using stream As MemoryStream = New MemoryStream
                            Dim encoderInfo As ImageCodecInfo = ScrapeImages.GetEncoderInfo(ImageFormat.Jpeg)
                            Dim encoderParams As New EncoderParameters(If((iQuality > 0), 2, 1))
                            encoderParams.Param(0) = New EncoderParameter(Encoder.RenderMethod, 12)
                            If (iQuality > 0) Then
                                encoderParams.Param(1) = New EncoderParameter(Encoder.Quality, iQuality)
                            End If
                            _image.Save(stream, encoderInfo, encoderParams)
                            Dim array As Byte() = stream.ToArray
                            Using stream2 As FileStream = New FileStream(sPath, FileMode.Create, FileAccess.Write)
                                stream2.Write(array, 0, array.Length)
                                stream2.Flush
                            End Using
                            stream.Flush
                        End Using
                        If flag Then
                            File.SetAttributes(sPath, fileAttributes)
                        End If
                    End If
                End If
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Master.eLog.WriteToErrorLog(exception.Message, exception.StackTrace, "Error", True)
                ProjectData.ClearProjectError
            End Try
        End Sub

        Public Shared Sub SaveFAasET(ByVal faPath As String, ByVal inPath As String)
            Dim num2 As Integer = 1
            Dim sPath As String = String.Empty
            If (Master.eSettings.VideoTSParent AndAlso Common.isVideoTS(inPath)) Then
                sPath = Path.Combine(Directory.GetParent(Directory.GetParent(inPath).FullName).FullName, "extrathumbs")
            ElseIf (Master.eSettings.VideoTSParent AndAlso Common.isBDRip(inPath)) Then
                sPath = Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetParent(inPath).FullName).FullName).FullName, "extrathumbs")
            Else
                sPath = Path.Combine(Directory.GetParent(inPath).FullName, "extrathumbs")
            End If
            num2 = (Functions.GetExtraModifier(sPath) + 1)
            If Not Directory.Exists(sPath) Then
                Directory.CreateDirectory(sPath)
            End If
            Common.MoveFileWithStream(faPath, Path.Combine(sPath, ("thumb" & num2 & ".jpg")))
        End Sub

    End Class
End Namespace

