Private Function FillListByFilter_Primaire(ByVal section As String, ByVal categorie As String, _
                                               ByVal gamme As String, ByVal sousGamme As String, _
                                               ByVal id As String, _
                                               ByVal ReferenceFournisseur As String, ByVal ReferenceFournisseurLike As String, _
                                               ByVal ReferenceInterne As String, _
                                               ByVal designation As String, ByVal designationLike As String, _
                                               ByVal stockMinimal As Boolean, _
                                               ByVal masquerStockNegatif_P As Boolean, _
                                               ByVal masquerStockNegatif_S As Boolean, _
                                               ByVal masquerMinimal_zero As Boolean, _
                                               ByVal emplacement As String, _
                                               ByVal enabled As Share.ClientFilter) As WCFResult
        Dim res As New WCFResult

        Try

            Dim t As Table = Me.Tables("ArticleGV")

            Me.Parameters("IDArticle").Value = id

            Me.Parameters.Add("IDSection").Value = section
            Me.Parameters.Add("IDCategorie").Value = categorie
            Me.Parameters.Add("IDGamme").Value = gamme
            Me.Parameters.Add("IDSousGamme").Value = sousGamme

            Me.Parameters("ReferenceFournisseur").Value = ReferenceFournisseur
            Me.Parameters("ReferenceFournisseurLike").Value = ReferenceFournisseurLike

            Me.Parameters("ReferenceInterne").Value = ReferenceInterne

            Me.Parameters("Designation").Value = designation
            Me.Parameters("DesignationLike").Value = designationLike

            Me.Parameters("IDEmplacement").Value = emplacement


            Dim l As New ArrayList
            Dim Cmpte, i As Integer
            Dim s As String

            s = " SELECT Article.ID,Ref_Fournisseur , Designation ,                                                         " & vbCrLf & _
                 " Couleur.libelle AS Couleur ,                                                                             " & vbCrLf & _
                 " PUV_HT                                                                          AS [PUV HT],             " & vbCrLf & _
                 " PUV_TTC                                                                         AS [PUV TTC],            " & vbCrLf & _
                 " (CAST(PUA_HT as varchar(30)         ) )                                         AS [PUA HT] ,            " & vbCrLf & _
                 " (CAST(PUA_TTC as varchar(30)        ) )                                         AS [PUA TTC] ,           " & vbCrLf & _
                 "  PUVSpecial_HT                                                                  AS [PUV SpeHT],          " & vbCrLf & _
                 "  PUVSpecial_TTC                                                                 AS [PUV SpeTTC],         " & vbCrLf & _
                 "  PUVR_HT                                                                        AS [PUV RevHT],          " & vbCrLf & _
                 "  PUVR_TTC                                                                       AS [PUV RevTTC],         " & vbCrLf & _
                 " (CAST(StockInitial_P as varchar(30) ) )                                         AS [StockInitial],       " & vbCrLf & _
                 " (CAST(QteAchetee_P as varchar(30)   ) )                                         AS [QtéEntrée],          " & vbCrLf & _
                 " (CAST(StockFinal_P as varchar(30)   ) )                                         AS [StockFinal],         " & vbCrLf & _
                 " (CAST(QteVendue_P as varchar(30)    ) )                                         AS [QtéSortie],          " & vbCrLf & _
                 " (CAST(Ecart as varchar(30)          ) )                                         AS [Ecart],              " & vbCrLf & _
                 " (CAST(StockMinimal as varchar(30)   ) )                                         AS [Stock Minimal]       " & vbCrLf & _
                 "  FROM Article  LEFT OUTER JOIN Couleur ON    Article.C_Color=Couleur.ID                                  "


            If id IsNot Nothing Then l.Add(" (C_Article = @IDArticle ")

            If section IsNot Nothing Then l.Add(" (C_Section = @IDSection)")
            If categorie IsNot Nothing Then l.Add(" (C_Categorie = @IDCategorie)")
            If gamme IsNot Nothing Then l.Add(" (C_Gamme = @IDGamme)")
            If sousGamme IsNot Nothing Then l.Add(" (C_SousGamme = @IDSousGamme)")

            If emplacement IsNot Nothing Then l.Add(" (C_Emplacement = @IDEmplacement)")

            If ReferenceFournisseur IsNot Nothing Then l.Add(" (Ref_Fournisseur = @ReferenceFournisseur)")
            If ReferenceFournisseurLike IsNot Nothing Then l.Add(" (Ref_Fournisseur  LIKE '%" & ReferenceFournisseurLike & "%')")

            If ReferenceInterne IsNot Nothing Then l.Add(" (Ref_Interne = @ReferenceInterne)")

            If designation IsNot Nothing Then l.Add(" (Designation = @Designation)")
            If designationLike IsNot Nothing Then l.Add(" (Designation  LIKE '%" & designationLike & "%')")

            If stockMinimal Then l.Add(" (StockMinimal >= StockFinal_P)")
            If masquerStockNegatif_P Then l.Add(" (StockFinal_P > 0)")
            If masquerStockNegatif_S Then l.Add(" (StockFinal_S > 0)")
            If masquerMinimal_zero Then l.Add(" (StockMinimal <> 0)")

            If enabled = Share.ClientFilter.Enabled Then
                l.Add(" (Enabled ='True')")
            ElseIf enabled = Share.ClientFilter.Desabled Then
                l.Add(" (Enabled ='False')")
            End If

            Cmpte = l.Count
            Select Case Cmpte
                Case 0
                    If Cmpte = 0 Then t.SelectSQLText = s & " ORDER BY  CAST(Designation AS NVARCHAR(MAX))"
                Case 1
                    t.SelectSQLText = s & " WHERE" & l(0) & " ORDER BY  CAST(Designation AS NVARCHAR(MAX))"
                Case Else
                    s = s & " WHERE" & l(0)
                    For i = 1 To Cmpte - 1
                        s = s & " AND" & l(i)
                    Next
                    t.SelectSQLText = s & " ORDER BY  CAST(Designation AS NVARCHAR(MAX))"
            End Select

            t.Fill()

            If (t.Count > 0) Then
                res.DataMeomyStream = Streaming.Write(Me.Dataset)
            Else
                res.SetResult(ErrorsCodes.ZeroRow, Me.GetType.ToString, "FillListByFilter", "Aucun article ne répond à vos critères de recherche.", False)
            End If


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Return res

    End Function