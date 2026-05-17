using System;
// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
[Serializable()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class GML
{
    private GMLFileInfo fileInfoField;

    private GMLAppInfo appInfoField;

    private GMLDatabaseItem[] appSpecificInfoField;

    /// <remarks/>
    public GMLFileInfo FileInfo
    {
        get
        {
            return this.fileInfoField;
        }
        set
        {
            this.fileInfoField = value;
        }
    }

    /// <remarks/>
    public GMLAppInfo AppInfo
    {
        get
        {
            return this.appInfoField;
        }
        set
        {
            this.appInfoField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("DatabaseItem", IsNullable = false)]
    public GMLDatabaseItem[] AppSpecificInfo
    {
        get
        {
            return this.appSpecificInfoField;
        }
        set
        {
            this.appSpecificInfoField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class GMLFileInfo
{

    private string copyrightField;

    private ulong timeStampField;

    /// <remarks/>
    public string Copyright
    {
        get
        {
            return this.copyrightField;
        }
        set
        {
            this.copyrightField = value;
        }
    }

    /// <remarks/>
    public ulong TimeStamp
    {
        get
        {
            return this.timeStampField;
        }
        set
        {
            this.timeStampField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class GMLAppInfo
{

    /// <remarks/>
    public string AppName { get; set; }

    /// <remarks/>
    public string AppBuild { get; set; }

    /// <remarks/>
    public ushort AppVersion { get; set; }

    /// <remarks/>
    public uint AppRelease { get; set; }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class GMLDatabaseItem
{
    /// <remarks/>
    public GMLDatabaseItemCategoryInfo CategoryInfo { get; set; }

    /// <remarks/>
    public GMLDatabaseItemItemInfo ItemInfo { get; set; }

    /// <remarks/>
    public GMLDatabaseItemItemDates ItemDates { get; set; }

    /// <remarks/>
    public GMLDatabaseItemItemReferences ItemReferences { get; set; }

    /// <remarks/>
    public GMLDatabaseItemItemTexts ItemTexts { get; set; }

    /// <remarks/>
    public GMLDatabaseItemItemLegal ItemLegal { get; set; }

    /// <remarks/>
    public GMLDatabaseItemItemTags ItemTags { get; set; }

    /// <remarks/>
    public GMLDatabaseItemItemRds ItemRds { get; set; }

    /// <remarks/>
    public GMLDatabaseItemItemMeta ItemMeta { get; set; }

    /// <remarks/>
    public GMLDatabaseItemItemMix ItemMix { get; set; }

    /// <remarks/>
    public GMLDatabaseItemItemFiles ItemFiles { get; set; }

    public override string ToString()
    {
        return $"{ItemInfo}";
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class GMLDatabaseItemCategoryInfo
{

    private uint categoryIdField;

    private uint categoryParentIdField;

    private uint categoryTypeField;
    private string categoryTypeAsTextField;

    private string categoryCodeField;

    private string categoryNameField;

    private string categoryNamePathField;

    private object categoryDescriptionField;

    private byte categoryRecMaxDurationField;

    private byte categoryPathIdField;

    private string categoryPathField;

    private string categoryColorField;

    private byte formatDefaultGenreField;

    private byte formatSepaMinArtistField;

    private byte formatSepaMinTitleField;

    /// <remarks/>
    public uint CategoryId
    {
        get
        {
            return this.categoryIdField;
        }
        set
        {
            this.categoryIdField = value;
        }
    }

    /// <remarks/>
    public uint CategoryParentId
    {
        get
        {
            return this.categoryParentIdField;
        }
        set
        {
            this.categoryParentIdField = value;
        }
    }

    /// <remarks/>
    public uint CategoryType
    {
        get
        {
            return this.categoryTypeField;
        }
        set
        {
            this.categoryTypeField = value;
        }
    }

    /// <remarks/>
    public string CategoryTypeAsText
    {
        get
        {
            return this.categoryTypeAsTextField;
        }
        set
        {
            this.categoryTypeAsTextField = value;
        }
    }

    /// <remarks/>
    public string CategoryCode
    {
        get
        {
            return this.categoryCodeField;
        }
        set
        {
            this.categoryCodeField = value;
        }
    }

    /// <remarks/>
    public string CategoryName
    {
        get
        {
            return this.categoryNameField;
        }
        set
        {
            this.categoryNameField = value;
        }
    }

    /// <remarks/>
    public string CategoryNamePath
    {
        get
        {
            return this.categoryNamePathField;
        }
        set
        {
            this.categoryNamePathField = value;
        }
    }

    /// <remarks/>
    public object CategoryDescription
    {
        get
        {
            return this.categoryDescriptionField;
        }
        set
        {
            this.categoryDescriptionField = value;
        }
    }

    /// <remarks/>
    public byte CategoryRecMaxDuration
    {
        get
        {
            return this.categoryRecMaxDurationField;
        }
        set
        {
            this.categoryRecMaxDurationField = value;
        }
    }

    /// <remarks/>
    public byte CategoryPathId
    {
        get
        {
            return this.categoryPathIdField;
        }
        set
        {
            this.categoryPathIdField = value;
        }
    }

    /// <remarks/>
    public string CategoryPath
    {
        get
        {
            return this.categoryPathField;
        }
        set
        {
            this.categoryPathField = value;
        }
    }

    /// <remarks/>
    public string CategoryColor
    {
        get
        {
            return this.categoryColorField;
        }
        set
        {
            this.categoryColorField = value;
        }
    }

    /// <remarks/>
    public byte FormatDefaultGenre
    {
        get
        {
            return this.formatDefaultGenreField;
        }
        set
        {
            this.formatDefaultGenreField = value;
        }
    }

    /// <remarks/>
    public byte FormatSepaMinArtist
    {
        get
        {
            return this.formatSepaMinArtistField;
        }
        set
        {
            this.formatSepaMinArtistField = value;
        }
    }

    /// <remarks/>
    public byte FormatSepaMinTitle
    {
        get
        {
            return this.formatSepaMinTitleField;
        }
        set
        {
            this.formatSepaMinTitleField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class GMLDatabaseItemItemInfo
{

    /// <remarks/>
    public ushort ItemId { get; set; }

    /// <remarks/>
    public uint ItemDuration { get; set; }

    /// <remarks/>
    public byte ItemType { get; set; }

    /// <remarks/>
    public string ItemTypeAsText { get; set; }

    /// <remarks/>
    public ushort ItemSubType { get; set; }

    /// <remarks/>
    public string ItemSubTypeAsText { get; set; }

    /// <remarks/>
    public byte ItemProdState { get; set; }

    /// <remarks/>
    public string ItemProdStateAsText { get; set; }

    /// <remarks/>
    public byte ItemRecState { get; set; }

    /// <remarks/>
    public string ItemRecStateAsText { get; set; }

    /// <remarks/>
    public string ItemTitle { get; set; }

    /// <remarks/>
    public string ItemInterpret { get; set; }

    public override string ToString() => $"{ItemInterpret} - {ItemTitle} ({ItemTypeAsText})";    
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class GMLDatabaseItemItemDates
{

    private ulong timeStampCreatedField;

    private ulong timeStampModifiedField;

    private ulong timeStampRecordedField;

    private ulong timeStampImportedField;

    private ulong timeStampExportedField;

    private ulong timeStampSyncedField;

    private ulong timeStampPlannedField;

    private ulong timeStampUsedField;

    private ulong timeStampTrashedField;

    private ulong timeStampRangeStartField;

    private ulong timeStampRangeEndField;

    private ulong timeStampRangeWarnField;

    private ulong timeStampRangeExpiryField;

    /// <remarks/>
    public ulong TimeStampCreated
    {
        get
        {
            return this.timeStampCreatedField;
        }
        set
        {
            this.timeStampCreatedField = value;
        }
    }

    /// <remarks/>
    public ulong TimeStampModified
    {
        get
        {
            return this.timeStampModifiedField;
        }
        set
        {
            this.timeStampModifiedField = value;
        }
    }

    /// <remarks/>
    public ulong TimeStampRecorded
    {
        get
        {
            return this.timeStampRecordedField;
        }
        set
        {
            this.timeStampRecordedField = value;
        }
    }

    /// <remarks/>
    public ulong TimeStampImported
    {
        get
        {
            return this.timeStampImportedField;
        }
        set
        {
            this.timeStampImportedField = value;
        }
    }

    /// <remarks/>
    public ulong TimeStampExported
    {
        get
        {
            return this.timeStampExportedField;
        }
        set
        {
            this.timeStampExportedField = value;
        }
    }

    /// <remarks/>
    public ulong TimeStampSynced
    {
        get
        {
            return this.timeStampSyncedField;
        }
        set
        {
            this.timeStampSyncedField = value;
        }
    }

    /// <remarks/>
    public ulong TimeStampPlanned
    {
        get
        {
            return this.timeStampPlannedField;
        }
        set
        {
            this.timeStampPlannedField = value;
        }
    }

    /// <remarks/>
    public ulong TimeStampUsed
    {
        get
        {
            return this.timeStampUsedField;
        }
        set
        {
            this.timeStampUsedField = value;
        }
    }

    /// <remarks/>
    public ulong TimeStampTrashed
    {
        get
        {
            return this.timeStampTrashedField;
        }
        set
        {
            this.timeStampTrashedField = value;
        }
    }

    /// <remarks/>
    public ulong TimeStampRangeStart
    {
        get
        {
            return this.timeStampRangeStartField;
        }
        set
        {
            this.timeStampRangeStartField = value;
        }
    }

    /// <remarks/>
    public ulong TimeStampRangeEnd
    {
        get
        {
            return this.timeStampRangeEndField;
        }
        set
        {
            this.timeStampRangeEndField = value;
        }
    }

    /// <remarks/>
    public ulong TimeStampRangeWarn
    {
        get
        {
            return this.timeStampRangeWarnField;
        }
        set
        {
            this.timeStampRangeWarnField = value;
        }
    }

    /// <remarks/>
    public ulong TimeStampRangeExpiry
    {
        get
        {
            return this.timeStampRangeExpiryField;
        }
        set
        {
            this.timeStampRangeExpiryField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class GMLDatabaseItemItemReferences
{

    private string refReferenceField;

    private object refItemCodeField;

    private object refMosObjectIdField;

    /// <remarks/>
    public string RefReference
    {
        get
        {
            return this.refReferenceField;
        }
        set
        {
            this.refReferenceField = value;
        }
    }

    /// <remarks/>
    public object RefItemCode
    {
        get
        {
            return this.refItemCodeField;
        }
        set
        {
            this.refItemCodeField = value;
        }
    }

    /// <remarks/>
    public object RefMosObjectId
    {
        get
        {
            return this.refMosObjectIdField;
        }
        set
        {
            this.refMosObjectIdField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class GMLDatabaseItemItemTexts
{

    private object textComment1Field;

    private object textComment2Field;

    private string textComment3Field;

    private object textComment4Field;

    private object textSetupField;

    /// <remarks/>
    public object TextComment1
    {
        get
        {
            return this.textComment1Field;
        }
        set
        {
            this.textComment1Field = value;
        }
    }

    /// <remarks/>
    public object TextComment2
    {
        get
        {
            return this.textComment2Field;
        }
        set
        {
            this.textComment2Field = value;
        }
    }

    /// <remarks/>
    public string TextComment3
    {
        get
        {
            return this.textComment3Field;
        }
        set
        {
            this.textComment3Field = value;
        }
    }

    /// <remarks/>
    public object TextComment4
    {
        get
        {
            return this.textComment4Field;
        }
        set
        {
            this.textComment4Field = value;
        }
    }

    /// <remarks/>
    public object TextSetup
    {
        get
        {
            return this.textSetupField;
        }
        set
        {
            this.textSetupField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class GMLDatabaseItemItemLegal
{

    private uint legalAlbumIdField;

    private uint legalTrackIdField;

    private object legalArtistIdsField;

    private object legalBandIdsField;

    private object legalCountryIdsField;

    private object legalLabelField;

    private object legalVersionField;

    private object legalAuthorField;

    private object legalClientField;

    private object legalSongwriterField;

    private object legalComposerField;

    private object legalProducerField;

    private object legalTechnicianField;

    private object legalRemixerField;

    private object legalOrchestratorField;

    private object legalOrchestraField;

    private object legalConductorField;

    private object legalChoirField;

    private object legalChoirConductorField;

    private object legalPartField;

    private object legalMovementField;

    private object legalCodeClassField;

    private object legalCodeISRCField;

    /// <remarks/>
    public uint LegalAlbumId
    {
        get
        {
            return this.legalAlbumIdField;
        }
        set
        {
            this.legalAlbumIdField = value;
        }
    }

    /// <remarks/>
    public uint LegalTrackId
    {
        get
        {
            return this.legalTrackIdField;
        }
        set
        {
            this.legalTrackIdField = value;
        }
    }

    /// <remarks/>
    public object LegalArtistIds
    {
        get
        {
            return this.legalArtistIdsField;
        }
        set
        {
            this.legalArtistIdsField = value;
        }
    }

    /// <remarks/>
    public object LegalBandIds
    {
        get
        {
            return this.legalBandIdsField;
        }
        set
        {
            this.legalBandIdsField = value;
        }
    }

    /// <remarks/>
    public object LegalCountryIds
    {
        get
        {
            return this.legalCountryIdsField;
        }
        set
        {
            this.legalCountryIdsField = value;
        }
    }

    /// <remarks/>
    public object LegalLabel
    {
        get
        {
            return this.legalLabelField;
        }
        set
        {
            this.legalLabelField = value;
        }
    }

    /// <remarks/>
    public object LegalVersion
    {
        get
        {
            return this.legalVersionField;
        }
        set
        {
            this.legalVersionField = value;
        }
    }

    /// <remarks/>
    public object LegalAuthor
    {
        get
        {
            return this.legalAuthorField;
        }
        set
        {
            this.legalAuthorField = value;
        }
    }

    /// <remarks/>
    public object LegalClient
    {
        get
        {
            return this.legalClientField;
        }
        set
        {
            this.legalClientField = value;
        }
    }

    /// <remarks/>
    public object LegalSongwriter
    {
        get
        {
            return this.legalSongwriterField;
        }
        set
        {
            this.legalSongwriterField = value;
        }
    }

    /// <remarks/>
    public object LegalComposer
    {
        get
        {
            return this.legalComposerField;
        }
        set
        {
            this.legalComposerField = value;
        }
    }

    /// <remarks/>
    public object LegalProducer
    {
        get
        {
            return this.legalProducerField;
        }
        set
        {
            this.legalProducerField = value;
        }
    }

    /// <remarks/>
    public object LegalTechnician
    {
        get
        {
            return this.legalTechnicianField;
        }
        set
        {
            this.legalTechnicianField = value;
        }
    }

    /// <remarks/>
    public object LegalRemixer
    {
        get
        {
            return this.legalRemixerField;
        }
        set
        {
            this.legalRemixerField = value;
        }
    }

    /// <remarks/>
    public object LegalOrchestrator
    {
        get
        {
            return this.legalOrchestratorField;
        }
        set
        {
            this.legalOrchestratorField = value;
        }
    }

    /// <remarks/>
    public object LegalOrchestra
    {
        get
        {
            return this.legalOrchestraField;
        }
        set
        {
            this.legalOrchestraField = value;
        }
    }

    /// <remarks/>
    public object LegalConductor
    {
        get
        {
            return this.legalConductorField;
        }
        set
        {
            this.legalConductorField = value;
        }
    }

    /// <remarks/>
    public object LegalChoir
    {
        get
        {
            return this.legalChoirField;
        }
        set
        {
            this.legalChoirField = value;
        }
    }

    /// <remarks/>
    public object LegalChoirConductor
    {
        get
        {
            return this.legalChoirConductorField;
        }
        set
        {
            this.legalChoirConductorField = value;
        }
    }

    /// <remarks/>
    public object LegalPart
    {
        get
        {
            return this.legalPartField;
        }
        set
        {
            this.legalPartField = value;
        }
    }

    /// <remarks/>
    public object LegalMovement
    {
        get
        {
            return this.legalMovementField;
        }
        set
        {
            this.legalMovementField = value;
        }
    }

    /// <remarks/>
    public object LegalCodeClass
    {
        get
        {
            return this.legalCodeClassField;
        }
        set
        {
            this.legalCodeClassField = value;
        }
    }

    /// <remarks/>
    public object LegalCodeISRC
    {
        get
        {
            return this.legalCodeISRCField;
        }
        set
        {
            this.legalCodeISRCField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class GMLDatabaseItemItemTags
{

    private ulong tagReleaseDateField;

    private ulong tagChartDateField;

    private byte tagChartTypeField;

    private byte tagChartRankingField;

    private byte tagChartCountryField;

    private byte tagMediaTypeField;

    private byte tagMediaDiscNrField;

    private byte tagMediaTrackNrField;

    private object tagMediaInventoryIdField;

    private byte tagFormatFlags1Field;

    private byte tagFormatFlags2Field;

    private byte tagFormatFlags3Field;

    private byte tagFormatFlags4Field;

    private byte tagFormatParams1Field;

    private byte tagFormatParams2Field;

    private byte tagFormatParams3Field;

    private byte tagFormatParams4Field;

    private byte tagFormatParams5Field;

    private byte tagFormatParams6Field;

    private byte tagFormatParams7Field;

    private byte tagFormatParams8Field;

    private byte tagFormatGenderField;

    private byte tagFormatPartDayField;

    private byte tagFormatPartWeekField;

    private byte tagFormatPartYearField;

    private object tagMusicCodeField;

    private int tagMusicGenreMainField;

    private int tagMusicGenreSub1Field;

    private int tagMusicGenreSub2Field;

    private int tagMusicGenreSub3Field;

    private int tagMusicTypeBeginField;

    private int tagMusicTypeEndField;

    private int tagMusicKeyBeginField;

    private int tagMusicKeyMidField;

    private int tagMusicKeyEndField;

    private int tagMusicBpmBeginField;

    private int tagMusicBpmMidField;

    private int tagMusicBpmEndField;

    private string tagLanguageISOField;

    private string tagLanguageTextField;

    private string tagPerformerTypeField;

    private string tagMoodKeywordsField;

    private string tagGenreTextField;

    private byte tagPopularimeterRatingField;

    private byte tagPopularimeterCounterField;

    private byte tagUserFlagsField;

    /// <remarks/>
    public ulong TagReleaseDate
    {
        get
        {
            return this.tagReleaseDateField;
        }
        set
        {
            this.tagReleaseDateField = value;
        }
    }

    /// <remarks/>
    public ulong TagChartDate
    {
        get
        {
            return this.tagChartDateField;
        }
        set
        {
            this.tagChartDateField = value;
        }
    }

    /// <remarks/>
    public byte TagChartType
    {
        get
        {
            return this.tagChartTypeField;
        }
        set
        {
            this.tagChartTypeField = value;
        }
    }

    /// <remarks/>
    public byte TagChartRanking
    {
        get
        {
            return this.tagChartRankingField;
        }
        set
        {
            this.tagChartRankingField = value;
        }
    }

    /// <remarks/>
    public byte TagChartCountry
    {
        get
        {
            return this.tagChartCountryField;
        }
        set
        {
            this.tagChartCountryField = value;
        }
    }

    /// <remarks/>
    public byte TagMediaType
    {
        get
        {
            return this.tagMediaTypeField;
        }
        set
        {
            this.tagMediaTypeField = value;
        }
    }

    /// <remarks/>
    public byte TagMediaDiscNr
    {
        get
        {
            return this.tagMediaDiscNrField;
        }
        set
        {
            this.tagMediaDiscNrField = value;
        }
    }

    /// <remarks/>
    public byte TagMediaTrackNr
    {
        get
        {
            return this.tagMediaTrackNrField;
        }
        set
        {
            this.tagMediaTrackNrField = value;
        }
    }

    /// <remarks/>
    public object TagMediaInventoryId
    {
        get
        {
            return this.tagMediaInventoryIdField;
        }
        set
        {
            this.tagMediaInventoryIdField = value;
        }
    }

    /// <remarks/>
    public byte TagFormatFlags1
    {
        get
        {
            return this.tagFormatFlags1Field;
        }
        set
        {
            this.tagFormatFlags1Field = value;
        }
    }

    /// <remarks/>
    public byte TagFormatFlags2
    {
        get
        {
            return this.tagFormatFlags2Field;
        }
        set
        {
            this.tagFormatFlags2Field = value;
        }
    }

    /// <remarks/>
    public byte TagFormatFlags3
    {
        get
        {
            return this.tagFormatFlags3Field;
        }
        set
        {
            this.tagFormatFlags3Field = value;
        }
    }

    /// <remarks/>
    public byte TagFormatFlags4
    {
        get
        {
            return this.tagFormatFlags4Field;
        }
        set
        {
            this.tagFormatFlags4Field = value;
        }
    }

    /// <remarks/>
    public byte TagFormatParams1
    {
        get
        {
            return this.tagFormatParams1Field;
        }
        set
        {
            this.tagFormatParams1Field = value;
        }
    }

    /// <remarks/>
    public byte TagFormatParams2
    {
        get
        {
            return this.tagFormatParams2Field;
        }
        set
        {
            this.tagFormatParams2Field = value;
        }
    }

    /// <remarks/>
    public byte TagFormatParams3
    {
        get
        {
            return this.tagFormatParams3Field;
        }
        set
        {
            this.tagFormatParams3Field = value;
        }
    }

    /// <remarks/>
    public byte TagFormatParams4
    {
        get
        {
            return this.tagFormatParams4Field;
        }
        set
        {
            this.tagFormatParams4Field = value;
        }
    }

    /// <remarks/>
    public byte TagFormatParams5
    {
        get
        {
            return this.tagFormatParams5Field;
        }
        set
        {
            this.tagFormatParams5Field = value;
        }
    }

    /// <remarks/>
    public byte TagFormatParams6
    {
        get
        {
            return this.tagFormatParams6Field;
        }
        set
        {
            this.tagFormatParams6Field = value;
        }
    }

    /// <remarks/>
    public byte TagFormatParams7
    {
        get
        {
            return this.tagFormatParams7Field;
        }
        set
        {
            this.tagFormatParams7Field = value;
        }
    }

    /// <remarks/>
    public byte TagFormatParams8
    {
        get
        {
            return this.tagFormatParams8Field;
        }
        set
        {
            this.tagFormatParams8Field = value;
        }
    }

    /// <remarks/>
    public byte TagFormatGender
    {
        get
        {
            return this.tagFormatGenderField;
        }
        set
        {
            this.tagFormatGenderField = value;
        }
    }

    /// <remarks/>
    public byte TagFormatPartDay
    {
        get
        {
            return this.tagFormatPartDayField;
        }
        set
        {
            this.tagFormatPartDayField = value;
        }
    }

    /// <remarks/>
    public byte TagFormatPartWeek
    {
        get
        {
            return this.tagFormatPartWeekField;
        }
        set
        {
            this.tagFormatPartWeekField = value;
        }
    }

    /// <remarks/>
    public byte TagFormatPartYear
    {
        get
        {
            return this.tagFormatPartYearField;
        }
        set
        {
            this.tagFormatPartYearField = value;
        }
    }

    /// <remarks/>
    public object TagMusicCode
    {
        get
        {
            return this.tagMusicCodeField;
        }
        set
        {
            this.tagMusicCodeField = value;
        }
    }

    /// <remarks/>
    public int TagMusicGenreMain
    {
        get
        {
            return this.tagMusicGenreMainField;
        }
        set
        {
            this.tagMusicGenreMainField = value;
        }
    }

    /// <remarks/>
    public int TagMusicGenreSub1
    {
        get
        {
            return this.tagMusicGenreSub1Field;
        }
        set
        {
            this.tagMusicGenreSub1Field = value;
        }
    }

    /// <remarks/>
    public int TagMusicGenreSub2
    {
        get
        {
            return this.tagMusicGenreSub2Field;
        }
        set
        {
            this.tagMusicGenreSub2Field = value;
        }
    }

    /// <remarks/>
    public int TagMusicGenreSub3
    {
        get
        {
            return this.tagMusicGenreSub3Field;
        }
        set
        {
            this.tagMusicGenreSub3Field = value;
        }
    }

    /// <remarks/>
    public int TagMusicTypeBegin
    {
        get
        {
            return this.tagMusicTypeBeginField;
        }
        set
        {
            this.tagMusicTypeBeginField = value;
        }
    }

    /// <remarks/>
    public int TagMusicTypeEnd
    {
        get
        {
            return this.tagMusicTypeEndField;
        }
        set
        {
            this.tagMusicTypeEndField = value;
        }
    }

    /// <remarks/>
    public int TagMusicKeyBegin
    {
        get
        {
            return this.tagMusicKeyBeginField;
        }
        set
        {
            this.tagMusicKeyBeginField = value;
        }
    }

    /// <remarks/>
    public int TagMusicKeyMid
    {
        get
        {
            return this.tagMusicKeyMidField;
        }
        set
        {
            this.tagMusicKeyMidField = value;
        }
    }

    /// <remarks/>
    public int TagMusicKeyEnd
    {
        get
        {
            return this.tagMusicKeyEndField;
        }
        set
        {
            this.tagMusicKeyEndField = value;
        }
    }

    /// <remarks/>
    public int TagMusicBpmBegin
    {
        get
        {
            return this.tagMusicBpmBeginField;
        }
        set
        {
            this.tagMusicBpmBeginField = value;
        }
    }

    /// <remarks/>
    public int TagMusicBpmMid
    {
        get
        {
            return this.tagMusicBpmMidField;
        }
        set
        {
            this.tagMusicBpmMidField = value;
        }
    }

    /// <remarks/>
    public int TagMusicBpmEnd
    {
        get
        {
            return this.tagMusicBpmEndField;
        }
        set
        {
            this.tagMusicBpmEndField = value;
        }
    }

    /// <remarks/>
    public string TagLanguageISO
    {
        get
        {
            return this.tagLanguageISOField;
        }
        set
        {
            this.tagLanguageISOField = value;
        }
    }

    /// <remarks/>
    public string TagLanguageText
    {
        get
        {
            return this.tagLanguageTextField;
        }
        set
        {
            this.tagLanguageTextField = value;
        }
    }

    /// <remarks/>
    public string TagPerformerType
    {
        get
        {
            return this.tagPerformerTypeField;
        }
        set
        {
            this.tagPerformerTypeField = value;
        }
    }

    /// <remarks/>
    public string TagMoodKeywords
    {
        get
        {
            return this.tagMoodKeywordsField;
        }
        set
        {
            this.tagMoodKeywordsField = value;
        }
    }

    /// <remarks/>
    public string TagGenreText
    {
        get
        {
            return this.tagGenreTextField;
        }
        set
        {
            this.tagGenreTextField = value;
        }
    }

    /// <remarks/>
    public byte TagPopularimeterRating
    {
        get
        {
            return this.tagPopularimeterRatingField;
        }
        set
        {
            this.tagPopularimeterRatingField = value;
        }
    }

    /// <remarks/>
    public byte TagPopularimeterCounter
    {
        get
        {
            return this.tagPopularimeterCounterField;
        }
        set
        {
            this.tagPopularimeterCounterField = value;
        }
    }

    /// <remarks/>
    public byte TagUserFlags
    {
        get
        {
            return this.tagUserFlagsField;
        }
        set
        {
            this.tagUserFlagsField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class GMLDatabaseItemItemRds
{

    private object rdsTextPS1Field;

    private object rdsTextPS2Field;

    private object rdsTextPS3Field;

    private object rdsTextPS4Field;

    private object rdsTextPS5Field;

    private object rdsTextPS6Field;

    private object rdsTextPS7Field;

    private object rdsTextPS8Field;

    private byte rdsTimePS1Field;

    private byte rdsTimePS2Field;

    private byte rdsTimePS3Field;

    private byte rdsTimePS4Field;

    private byte rdsTimePS5Field;

    private byte rdsTimePS6Field;

    private byte rdsTimePS7Field;

    private byte rdsTimePS8Field;

    private string rdsAlignPS1Field;

    private string rdsAlignPS2Field;

    private string rdsAlignPS3Field;

    private string rdsAlignPS4Field;

    private string rdsAlignPS5Field;

    private string rdsAlignPS6Field;

    private string rdsAlignPS7Field;

    private string rdsAlignPS8Field;

    private object rdsTextRT1Field;

    private object rdsTextRT2Field;

    private byte rdsTimeRT1Field;

    private byte rdsTimeRT2Field;

    private string rdsAlignRT1Field;

    private string rdsAlignRT2Field;

    private byte rdsCodePTYField;

    private byte rdsForcePTYField;

    private byte rdsCodeTATPField;

    private byte rdsForceTATPField;

    /// <remarks/>
    public object RdsTextPS1
    {
        get
        {
            return this.rdsTextPS1Field;
        }
        set
        {
            this.rdsTextPS1Field = value;
        }
    }

    /// <remarks/>
    public object RdsTextPS2
    {
        get
        {
            return this.rdsTextPS2Field;
        }
        set
        {
            this.rdsTextPS2Field = value;
        }
    }

    /// <remarks/>
    public object RdsTextPS3
    {
        get
        {
            return this.rdsTextPS3Field;
        }
        set
        {
            this.rdsTextPS3Field = value;
        }
    }

    /// <remarks/>
    public object RdsTextPS4
    {
        get
        {
            return this.rdsTextPS4Field;
        }
        set
        {
            this.rdsTextPS4Field = value;
        }
    }

    /// <remarks/>
    public object RdsTextPS5
    {
        get
        {
            return this.rdsTextPS5Field;
        }
        set
        {
            this.rdsTextPS5Field = value;
        }
    }

    /// <remarks/>
    public object RdsTextPS6
    {
        get
        {
            return this.rdsTextPS6Field;
        }
        set
        {
            this.rdsTextPS6Field = value;
        }
    }

    /// <remarks/>
    public object RdsTextPS7
    {
        get
        {
            return this.rdsTextPS7Field;
        }
        set
        {
            this.rdsTextPS7Field = value;
        }
    }

    /// <remarks/>
    public object RdsTextPS8
    {
        get
        {
            return this.rdsTextPS8Field;
        }
        set
        {
            this.rdsTextPS8Field = value;
        }
    }

    /// <remarks/>
    public byte RdsTimePS1
    {
        get
        {
            return this.rdsTimePS1Field;
        }
        set
        {
            this.rdsTimePS1Field = value;
        }
    }

    /// <remarks/>
    public byte RdsTimePS2
    {
        get
        {
            return this.rdsTimePS2Field;
        }
        set
        {
            this.rdsTimePS2Field = value;
        }
    }

    /// <remarks/>
    public byte RdsTimePS3
    {
        get
        {
            return this.rdsTimePS3Field;
        }
        set
        {
            this.rdsTimePS3Field = value;
        }
    }

    /// <remarks/>
    public byte RdsTimePS4
    {
        get
        {
            return this.rdsTimePS4Field;
        }
        set
        {
            this.rdsTimePS4Field = value;
        }
    }

    /// <remarks/>
    public byte RdsTimePS5
    {
        get
        {
            return this.rdsTimePS5Field;
        }
        set
        {
            this.rdsTimePS5Field = value;
        }
    }

    /// <remarks/>
    public byte RdsTimePS6
    {
        get
        {
            return this.rdsTimePS6Field;
        }
        set
        {
            this.rdsTimePS6Field = value;
        }
    }

    /// <remarks/>
    public byte RdsTimePS7
    {
        get
        {
            return this.rdsTimePS7Field;
        }
        set
        {
            this.rdsTimePS7Field = value;
        }
    }

    /// <remarks/>
    public byte RdsTimePS8
    {
        get
        {
            return this.rdsTimePS8Field;
        }
        set
        {
            this.rdsTimePS8Field = value;
        }
    }

    /// <remarks/>
    public string RdsAlignPS1
    {
        get
        {
            return this.rdsAlignPS1Field;
        }
        set
        {
            this.rdsAlignPS1Field = value;
        }
    }

    /// <remarks/>
    public string RdsAlignPS2
    {
        get
        {
            return this.rdsAlignPS2Field;
        }
        set
        {
            this.rdsAlignPS2Field = value;
        }
    }

    /// <remarks/>
    public string RdsAlignPS3
    {
        get
        {
            return this.rdsAlignPS3Field;
        }
        set
        {
            this.rdsAlignPS3Field = value;
        }
    }

    /// <remarks/>
    public string RdsAlignPS4
    {
        get
        {
            return this.rdsAlignPS4Field;
        }
        set
        {
            this.rdsAlignPS4Field = value;
        }
    }

    /// <remarks/>
    public string RdsAlignPS5
    {
        get
        {
            return this.rdsAlignPS5Field;
        }
        set
        {
            this.rdsAlignPS5Field = value;
        }
    }

    /// <remarks/>
    public string RdsAlignPS6
    {
        get
        {
            return this.rdsAlignPS6Field;
        }
        set
        {
            this.rdsAlignPS6Field = value;
        }
    }

    /// <remarks/>
    public string RdsAlignPS7
    {
        get
        {
            return this.rdsAlignPS7Field;
        }
        set
        {
            this.rdsAlignPS7Field = value;
        }
    }

    /// <remarks/>
    public string RdsAlignPS8
    {
        get
        {
            return this.rdsAlignPS8Field;
        }
        set
        {
            this.rdsAlignPS8Field = value;
        }
    }

    /// <remarks/>
    public object RdsTextRT1
    {
        get
        {
            return this.rdsTextRT1Field;
        }
        set
        {
            this.rdsTextRT1Field = value;
        }
    }

    /// <remarks/>
    public object RdsTextRT2
    {
        get
        {
            return this.rdsTextRT2Field;
        }
        set
        {
            this.rdsTextRT2Field = value;
        }
    }

    /// <remarks/>
    public byte RdsTimeRT1
    {
        get
        {
            return this.rdsTimeRT1Field;
        }
        set
        {
            this.rdsTimeRT1Field = value;
        }
    }

    /// <remarks/>
    public byte RdsTimeRT2
    {
        get
        {
            return this.rdsTimeRT2Field;
        }
        set
        {
            this.rdsTimeRT2Field = value;
        }
    }

    /// <remarks/>
    public string RdsAlignRT1
    {
        get
        {
            return this.rdsAlignRT1Field;
        }
        set
        {
            this.rdsAlignRT1Field = value;
        }
    }

    /// <remarks/>
    public string RdsAlignRT2
    {
        get
        {
            return this.rdsAlignRT2Field;
        }
        set
        {
            this.rdsAlignRT2Field = value;
        }
    }

    /// <remarks/>
    public byte RdsCodePTY
    {
        get
        {
            return this.rdsCodePTYField;
        }
        set
        {
            this.rdsCodePTYField = value;
        }
    }

    /// <remarks/>
    public byte RdsForcePTY
    {
        get
        {
            return this.rdsForcePTYField;
        }
        set
        {
            this.rdsForcePTYField = value;
        }
    }

    /// <remarks/>
    public byte RdsCodeTATP
    {
        get
        {
            return this.rdsCodeTATPField;
        }
        set
        {
            this.rdsCodeTATPField = value;
        }
    }

    /// <remarks/>
    public byte RdsForceTATP
    {
        get
        {
            return this.rdsForceTATPField;
        }
        set
        {
            this.rdsForceTATPField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class GMLDatabaseItemItemMeta
{

    private object metaOriginSiteNameField;

    private object metaOriginItemIdField;

    /// <remarks/>
    public object MetaOriginSiteName
    {
        get
        {
            return this.metaOriginSiteNameField;
        }
        set
        {
            this.metaOriginSiteNameField = value;
        }
    }

    /// <remarks/>
    public object MetaOriginItemId
    {
        get
        {
            return this.metaOriginItemIdField;
        }
        set
        {
            this.metaOriginItemIdField = value;
        }
    }

    /// <remarks/>
    public string MetaKeyWords { get; set; }

    /// <remarks/>
    public string MetaLastWords { get; set; }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class GMLDatabaseItemItemMix
{

    private byte lvlAdjLeftField;

    private byte lvlAdjRightField;

    private uint mixTimeTotalField;

    private uint mixTimeFadeInField;

    private ushort mixTimeFadeOutField;

    private uint mixPosBeginField;

    private uint mixPosEndField;

    private uint mixPosNextField;

    private uint mixPosIntroField;

    private uint mixPosOutroField;

    private uint hookPosInField;

    private uint hookPosOutField;

    private uint hookPosNextField;

    private uint hotCuePosAField;

    private uint hotCuePosBField;

    private uint hotCuePosCField;

    private uint hotCuePosDField;

    private uint triggerPos1Field;

    private uint triggerPos2Field;

    private uint triggerPos3Field;

    private uint triggerPos4Field;

    /// <remarks/>
    public byte LvlAdjLeft
    {
        get
        {
            return this.lvlAdjLeftField;
        }
        set
        {
            this.lvlAdjLeftField = value;
        }
    }

    /// <remarks/>
    public byte LvlAdjRight
    {
        get
        {
            return this.lvlAdjRightField;
        }
        set
        {
            this.lvlAdjRightField = value;
        }
    }

    /// <remarks/>
    public uint MixTimeTotal
    {
        get
        {
            return this.mixTimeTotalField;
        }
        set
        {
            this.mixTimeTotalField = value;
        }
    }

    /// <remarks/>
    public uint MixTimeFadeIn
    {
        get
        {
            return this.mixTimeFadeInField;
        }
        set
        {
            this.mixTimeFadeInField = value;
        }
    }

    /// <remarks/>
    public ushort MixTimeFadeOut
    {
        get
        {
            return this.mixTimeFadeOutField;
        }
        set
        {
            this.mixTimeFadeOutField = value;
        }
    }

    /// <remarks/>
    public uint MixPosBegin
    {
        get
        {
            return this.mixPosBeginField;
        }
        set
        {
            this.mixPosBeginField = value;
        }
    }

    /// <remarks/>
    public uint MixPosEnd
    {
        get
        {
            return this.mixPosEndField;
        }
        set
        {
            this.mixPosEndField = value;
        }
    }

    /// <remarks/>
    public uint MixPosNext
    {
        get
        {
            return this.mixPosNextField;
        }
        set
        {
            this.mixPosNextField = value;
        }
    }

    /// <remarks/>
    public uint MixPosIntro
    {
        get
        {
            return this.mixPosIntroField;
        }
        set
        {
            this.mixPosIntroField = value;
        }
    }

    /// <remarks/>
    public uint MixPosOutro
    {
        get
        {
            return this.mixPosOutroField;
        }
        set
        {
            this.mixPosOutroField = value;
        }
    }

    /// <remarks/>
    public uint HookPosIn
    {
        get
        {
            return this.hookPosInField;
        }
        set
        {
            this.hookPosInField = value;
        }
    }

    /// <remarks/>
    public uint HookPosOut
    {
        get
        {
            return this.hookPosOutField;
        }
        set
        {
            this.hookPosOutField = value;
        }
    }

    /// <remarks/>
    public uint HookPosNext
    {
        get
        {
            return this.hookPosNextField;
        }
        set
        {
            this.hookPosNextField = value;
        }
    }

    /// <remarks/>
    public uint HotCuePosA
    {
        get
        {
            return this.hotCuePosAField;
        }
        set
        {
            this.hotCuePosAField = value;
        }
    }

    /// <remarks/>
    public uint HotCuePosB
    {
        get
        {
            return this.hotCuePosBField;
        }
        set
        {
            this.hotCuePosBField = value;
        }
    }

    /// <remarks/>
    public uint HotCuePosC
    {
        get
        {
            return this.hotCuePosCField;
        }
        set
        {
            this.hotCuePosCField = value;
        }
    }

    /// <remarks/>
    public uint HotCuePosD
    {
        get
        {
            return this.hotCuePosDField;
        }
        set
        {
            this.hotCuePosDField = value;
        }
    }

    /// <remarks/>
    public uint TriggerPos1
    {
        get
        {
            return this.triggerPos1Field;
        }
        set
        {
            this.triggerPos1Field = value;
        }
    }

    /// <remarks/>
    public uint TriggerPos2
    {
        get
        {
            return this.triggerPos2Field;
        }
        set
        {
            this.triggerPos2Field = value;
        }
    }

    /// <remarks/>
    public uint TriggerPos3
    {
        get
        {
            return this.triggerPos3Field;
        }
        set
        {
            this.triggerPos3Field = value;
        }
    }

    /// <remarks/>
    public uint TriggerPos4
    {
        get
        {
            return this.triggerPos4Field;
        }
        set
        {
            this.triggerPos4Field = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class GMLDatabaseItemItemFiles
{

    private GMLDatabaseItemItemFilesFileAudio fileAudioField;

    /// <remarks/>
    public GMLDatabaseItemItemFilesFileAudio FileAudio
    {
        get
        {
            return this.fileAudioField;
        }
        set
        {
            this.fileAudioField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class GMLDatabaseItemItemFilesFileAudio
{
    // removed explicit backing field for FilePathId - use auto-property
    public byte FilePathId { get; set; }

    private uint? fileSizeField;

    private ulong fileTimeStampCreatedField;

    private ulong fileTimeStampModifiedField;

    private ulong fileTimeStampAccessedField;

    /// <remarks/>
    public string FilePath { get; set; }


    /// <remarks/>
    public string FileName { get; set; }

    /// <remarks/>
    public string FileAttr { get; set; }

    /// <remarks/>
    public string FileAttrText { get; set; }

    /// <remarks/>
    public string FileCRC { get; set; }

    /// <remarks/>
    public string FileSize
    {
        get
        {
            return this.fileSizeField?.ToString();
        }
        set
        {
            this.fileSizeField = string.IsNullOrEmpty(value) ? (uint?)null : uint.Parse(value);
        }
    }

    /// <remarks/>
    public string FileTimeStampCreated
    {
        get
        {
            return this.fileTimeStampCreatedField.ToString();
        }
        set
        {
            this.fileTimeStampCreatedField = string.IsNullOrEmpty(value) ? 0 : ulong.Parse(value);
        }
    }

    /// <remarks/>
    public string FileTimeStampModified
    {
        get
        {
            return this.fileTimeStampModifiedField.ToString();
        }
        set
        {
            this.fileTimeStampModifiedField = string.IsNullOrEmpty(value) ? 0 : ulong.Parse(value);
        }
    }

    /// <remarks/>
    public string FileTimeStampAccessed
    {
        get
        {
            return this.fileTimeStampAccessedField.ToString();
        }
        set
        {
            this.fileTimeStampAccessedField = string.IsNullOrEmpty(value) ? 0 : ulong.Parse(value);
        }
    }
}


