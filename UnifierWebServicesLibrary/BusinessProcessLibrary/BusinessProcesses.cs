using Newtonsoft.Json;

namespace BusinessProcessLibrary
{
    /* This class is purely for testing purposes, as the BP is not deployed in stage */
    public class PotentialPunchlistItems
    {
        // String attributes, in order of appearance in returning JSON
        [JsonProperty("punInContractYNRB", NullValueHandling = NullValueHandling.Ignore)]
        public string? InContract { get; set; }

        [JsonProperty("punInspectDescTB", NullValueHandling = NullValueHandling.Ignore)]
        public string? InspectionDescription { get; set; } 

        [JsonProperty("uuu_latitude", NullValueHandling = NullValueHandling.Ignore)]
        public string? Latitude { get; set; } 

        [JsonProperty("uuu_record_last_update_date", NullValueHandling = NullValueHandling.Ignore)]
        public string? RecordLastUpdateDate { get; set; } 

        [JsonProperty("punDescriptionRTF", NullValueHandling = NullValueHandling.Ignore)]
        public string? ItemDescription { get; set; } 

        [JsonProperty("punCreateSuggest", NullValueHandling = NullValueHandling.Ignore)]
        public string? SuggestedCreatedPromptedBy { get; set; } 

        [JsonProperty("punLocateDescripTB", NullValueHandling = NullValueHandling.Ignore)]
        public string? AdditionalLocationDescriptors { get; set; } 

        [JsonProperty("punStationYNPD", NullValueHandling = NullValueHandling.Ignore)]
        public string? Station { get; set; } 

        [JsonProperty("punRecordNoBPC", NullValueHandling = NullValueHandling.Ignore)]
        public string? PunchlistRecordNo { get; set; } 

        [JsonProperty("punDisciplineMS", NullValueHandling = NullValueHandling.Ignore)]
        public string? Discipline { get; set; } 

        [JsonProperty("punObservedDateDP", NullValueHandling = NullValueHandling.Ignore)]
        public string? ObservedDate { get; set; } 

        [JsonProperty("uuu_longitude", NullValueHandling = NullValueHandling.Ignore)]
        public string? Longitude { get; set; } 

        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string? Title { get; set; } 

        [JsonProperty("punProposedSolutionRTF", NullValueHandling = NullValueHandling.Ignore)]
        public string? ProposedSolution { get; set; } 

        [JsonProperty("punInspectDateDP", NullValueHandling = NullValueHandling.Ignore)]
        public string? InspectionDate { get; set; } 

        [JsonProperty("record_no", NullValueHandling = NullValueHandling.Ignore)]
        public string? RecordNo { get; set; } 

        [JsonProperty("dtShellPK", NullValueHandling = NullValueHandling.Ignore)]
        public string? TargetShell { get; set; } 

        [JsonProperty("punRecordClosedDP", NullValueHandling = NullValueHandling.Ignore)]
        public string? DecisionDate { get; set; } 

        [JsonProperty("uuu_bp_record_url", NullValueHandling = NullValueHandling.Ignore)]
        public string? BusinessProcessRecordURL { get; set; } 

        [JsonProperty("ugenAreaPK", NullValueHandling = NullValueHandling.Ignore)]
        public string? Area { get; set; } 

        [JsonProperty("subrDrawingNumDP", NullValueHandling = NullValueHandling.Ignore)]
        public string? DrawingNumber { get; set; } 

        [JsonProperty("uxpunStaNumTB16", NullValueHandling = NullValueHandling.Ignore)]
        public string? StationingNumber { get; set; } 

        [JsonProperty("uuu_creation_date", NullValueHandling = NullValueHandling.Ignore)]
        public string? CreationDate { get; set; } 

        [JsonProperty("cppnamesysshellname", NullValueHandling = NullValueHandling.Ignore)]
        public string? CPPName { get; set; } 

        [JsonProperty("punPunchlistSourcePK", NullValueHandling = NullValueHandling.Ignore)]
        public string? ItemAttributedTo { get; set; } 

        [JsonProperty("punPriorityPD", NullValueHandling = NullValueHandling.Ignore)]
        public string? Priority { get; set; } 

        [JsonProperty("punSegmentsPD", NullValueHandling = NullValueHandling.Ignore)]
        public string? ProjectSegment { get; set; } 

        [JsonProperty("punMeritReasonTB", NullValueHandling = NullValueHandling.Ignore)]
        public string? Justification { get; set; }

        [JsonProperty("punContract", NullValueHandling = NullValueHandling.Ignore)]
        public string? AssociatedContract { get; set; } 

        [JsonProperty("cppnumbersysshellnum", NullValueHandling = NullValueHandling.Ignore)]
        public string? CPPNumber { get; set; } 

        [JsonProperty("creator_id", NullValueHandling = NullValueHandling.Ignore)]
        public string? Creator { get; set; } 

        [JsonProperty("genVoidCB", NullValueHandling = NullValueHandling.Ignore)]
        public string? Void { get; set; } 

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string? Status { get; set; }

        // List of PotentialPunchlistItems objects
        public List<PotentialPunchlistItems> Data { get; set; }

        // Constructor
        public PotentialPunchlistItems()
        {
            Data = new List<PotentialPunchlistItems>();
        }
    }


}
