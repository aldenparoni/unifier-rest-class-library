using Newtonsoft.Json;

namespace BusinessProcessLibrary
{
    // Engineer's Supplemental Instructions (ESI) - a workflow BP I will test
    public class EngineersSupplementalInstructions
    {
        [JsonProperty("rfi3rdPartyMS", NullValueHandling = NullValueHandling.Ignore)]
        public string? ThirdPartyReviewers { get; set; } = null;

        [JsonProperty("rfiAssociatedRFIBP", NullValueHandling = NullValueHandling.Ignore)]
        public string? AssociatedRFI { get; set; } = null;

        [JsonProperty("cppnamesysshellname", NullValueHandling = NullValueHandling.Ignore)]
        public string? CPPName { get; set; } = null;

        [JsonProperty("cppnumbersysshellnum", NullValueHandling = NullValueHandling.Ignore)]
        public string? CPPNumber { get; set; } = null;

        [JsonProperty("uasiContractReferenceDP", NullValueHandling = NullValueHandling.Ignore)]
        public string? Contract { get; set; } = null;

        [JsonProperty("corCtrRefTB250", NullValueHandling = NullValueHandling.Ignore)]
        public string? ContractorReference { get; set; } = null;

        [JsonProperty("urfiCostImpactPD", NullValueHandling = NullValueHandling.Ignore)]
        public string? CostImpact { get; set; } = null;

        [JsonProperty("uuu_creation_date", NullValueHandling = NullValueHandling.Ignore)]
        public string? CreationDate { get; set; } = null;

        [JsonProperty("creator_id", NullValueHandling = NullValueHandling.Ignore)]
        public string? Creator { get; set; } = null;

        [JsonProperty("shortname", NullValueHandling = NullValueHandling.Ignore)]
        public string? CreatorCompany { get; set; } = null;

        [JsonProperty("uasiDecisionReturnPD", NullValueHandling = NullValueHandling.Ignore)]
        public string? Decision { get; set; } = null;

        [JsonProperty("uasiDueDateDOP", NullValueHandling = NullValueHandling.Ignore)]
        public string? DueDate { get; set; } = null;

        [JsonProperty("uasiStatus", NullValueHandling = NullValueHandling.Ignore)]
        public string? ESIStatus { get; set; } = null;

        [JsonProperty("Notes", NullValueHandling = NullValueHandling.Ignore)]
        public string? Notes { get; set; } = null;

        [JsonProperty("genPrePublishPathTB250", NullValueHandling = NullValueHandling.Ignore)]
        public string? PrefixPublishPath { get; set; } = null;

        [JsonProperty("uasiDecisionPD", NullValueHandling = NullValueHandling.Ignore)]
        public string? ProposedDecision { get; set; } = null;

        [JsonProperty("uuu_dm_publish_path", NullValueHandling = NullValueHandling.Ignore)]
        public string? PublishPath { get; set; } = null;

        [JsonProperty("record_no", NullValueHandling = NullValueHandling.Ignore)]
        public string? RecordNo { get; set; } = null;

        [JsonProperty("urfiScheduleImpactPD", NullValueHandling = NullValueHandling.Ignore)]
        public string? ScheduleImpact { get; set; } = null;

        [JsonProperty("ugenSpecifcatnsMTL4000", NullValueHandling = NullValueHandling.Ignore)]
        public string? Specifications { get; set; } = null;

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string? Status { get; set; } = null;

        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string? Title { get; set; } = null;

        // Constructor for Creation step
        public EngineersSupplementalInstructions(string? title, string? costImpact, string? scheduleImpact, string? contract,
            string? associatedRFI, string? thirdParty, string? contractorRef, string? notes, string? specs)
        {
            Title = title;
            CostImpact = costImpact;
            ScheduleImpact = scheduleImpact;
            Contract = contract;
            AssociatedRFI = associatedRFI;
            ThirdPartyReviewers = thirdParty;
            ContractorReference = contractorRef;
            Notes = notes;
            Specifications = specs;
        }
    }

    // Canvassing Efforts - a non-workflow BP I will test
    public class CanvassingEfforts(string? name, string? canvassingProject, string? startDate,
        string? endDate, string? allEncompassingEffort, string? status, string? @void)
    {
        [JsonProperty("uxceEndDate", NullValueHandling = NullValueHandling.Ignore)]
        public string? EndDate { get; set; } = endDate;

        [JsonProperty("uuu_record_last_update_date", NullValueHandling = NullValueHandling.Ignore)]
        public string? RecordLastUpdateDate { get; set; } = null;

        [JsonProperty("uuu_creation_date", NullValueHandling = NullValueHandling.Ignore)]
        public string? CreationDate { get; set; } = null;

        [JsonProperty("piCanvassingProjectBP", NullValueHandling = NullValueHandling.Ignore)]
        public string? CanvassingProject { get; set; } = canvassingProject;

        [JsonProperty("piAllEncompassingYNRB", NullValueHandling = NullValueHandling.Ignore)]
        public string? AllEncompassingEffort { get; set; } = allEncompassingEffort;

        [JsonProperty("record_no", NullValueHandling = NullValueHandling.Ignore)]
        public string? RecordNo { get; set; } = null;

        [JsonProperty("genVoidCB", NullValueHandling = NullValueHandling.Ignore)]
        public string? Void { get; set; } = @void;

        [JsonProperty("creator_id", NullValueHandling = NullValueHandling.Ignore)]
        public string? Creator { get; set; } = null;

        [JsonProperty("uxceStartDate", NullValueHandling = NullValueHandling.Ignore)]
        public string? StartDate { get; set; } = startDate;

        [JsonProperty("uuu_dm_publish_path", NullValueHandling = NullValueHandling.Ignore)]
        public string? PublishPath { get; set; } = null;

        [JsonProperty("uuu_bp_record_url", NullValueHandling = NullValueHandling.Ignore)]
        public string? BPRecordURL { get; set; } = null;

        [JsonProperty("uxceEffortName1", NullValueHandling = NullValueHandling.Ignore)]
        public string? Name { get; set; } = name;

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string? Status { get; set; } = status;

        [JsonProperty("integration_key", NullValueHandling = NullValueHandling.Ignore)]
        public string? IntegrationKey { get; set; } = null;

        [JsonProperty("piAttachNumPlusYesNoIA", NullValueHandling = NullValueHandling.Ignore)]
        public string? AttachmentCountVerification { get; set; } = null;
    }
}
