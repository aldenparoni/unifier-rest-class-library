using Newtonsoft.Json;
using UnifierWebServicesLibrary;

namespace BusinessProcessLibrary
{
    // Engineer's Supplemental Instructions (ESI) - a workflow BP I will test
    public class EngineersSupplementalInstructions(string? title, string? costImpact, string? scheduleImpact, string? contract,
        string? associatedRFI, string? thirdParty, string? contractorRef, string? notes, string? specs)
    {
        [JsonProperty("rfi3rdPartyMS", NullValueHandling = NullValueHandling.Ignore)]
        public string? ThirdPartyReviewers { get; set; } = thirdParty;

        [JsonProperty("rfiAssociatedRFIBP", NullValueHandling = NullValueHandling.Ignore)]
        public string? AssociatedRFI { get; set; } = associatedRFI;

        [JsonProperty("cppnamesysshellname", NullValueHandling = NullValueHandling.Ignore)]
        public string? CPPName { get; set; } = null;

        [JsonProperty("cppnumbersysshellnum", NullValueHandling = NullValueHandling.Ignore)]
        public string? CPPNumber { get; set; } = null;

        [JsonProperty("uasiContractReferenceDP", NullValueHandling = NullValueHandling.Ignore)]
        public string? Contract { get; set; } = contract;

        [JsonProperty("corCtrRefTB250", NullValueHandling = NullValueHandling.Ignore)]
        public string? ContractorReference { get; set; } = contractorRef;

        [JsonProperty("urfiCostImpactPD", NullValueHandling = NullValueHandling.Ignore)]
        public string? CostImpact { get; set; } = costImpact;

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
        public string? Notes { get; set; } = notes;

        [JsonProperty("genPrePublishPathTB250", NullValueHandling = NullValueHandling.Ignore)]
        public string? PrefixPublishPath { get; set; } = null;

        [JsonProperty("uasiDecisionPD", NullValueHandling = NullValueHandling.Ignore)]
        public string? ProposedDecision { get; set; } = null;

        [JsonProperty("uuu_dm_publish_path", NullValueHandling = NullValueHandling.Ignore)]
        public string? PublishPath { get; set; } = null;

        [JsonProperty("record_no", NullValueHandling = NullValueHandling.Ignore)]
        public string? RecordNo { get; set; } = null;

        [JsonProperty("urfiScheduleImpactPD", NullValueHandling = NullValueHandling.Ignore)]
        public string? ScheduleImpact { get; set; } = scheduleImpact;

        [JsonProperty("ugenSpecifcatnsMTL4000", NullValueHandling = NullValueHandling.Ignore)]
        public string? Specifications { get; set; } = specs;

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string? Status { get; set; } = null;

        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string? Title { get; set; } = title;

        /// <summary>
        /// This method takes user input and sets up the REST request to create a new ESI record.
        /// </summary>
        /// <param name="user">The IntegrationUser object instance used throughout app runtime.</param>
        public static void CreateESI(IntegrationUser user)
        {
            Console.Write("\nEnter the project number: ");
            string? projectNum = Console.ReadLine();
            WorkflowDetails workflowDetails = new("Any Name", "System Integration", "Submit");
            Options options = new(projectNum, "Engineer's Supplemental Instructions (ESI)", workflowDetails);

            Console.WriteLine("Please enter record information below:");
            Console.Write("\n                                 Title: ");
            string? esiTitle = Console.ReadLine();
            Console.Write("                 Cost Impact (Yes or No): ");
            string? costImpact = Console.ReadLine();
            Console.Write("             Schedule Impact (Yes or No): ");
            string? scheduleImpact = Console.ReadLine();
            Console.Write("      Contract (default: CT-HRT-2000106): ");
            string? contractNum = Console.ReadLine();
            Console.Write("  Associated RFI (enter a record number): ");
            string? associatedRFI = Console.ReadLine();
            Console.Write("                     3rd Party Reviewers: ");
            string? thirdParty = Console.ReadLine();
            Console.Write("                    Contractor Reference: ");
            string? contractRef = Console.ReadLine();
            Console.Write("                                   Notes: ");
            string? notes = Console.ReadLine();
            Console.Write("                          Specifications: ");
            string? specs = Console.ReadLine();

            List<EngineersSupplementalInstructions> data =
            [
                new EngineersSupplementalInstructions(esiTitle, costImpact, scheduleImpact, contractNum, associatedRFI,
                            thirdParty, contractRef, notes, specs)
            ];

            JSONBody<Options, List<EngineersSupplementalInstructions>> jsonBody = new(options, data);
            string body = JsonConvert.SerializeObject(jsonBody, Formatting.Indented);

            Console.WriteLine($"\nThank you. Now creating record in Unifier...\n");
            Console.WriteLine(UnifierRequests.CreateBPRecord(user, body));
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

        /// <summary>
        /// This method takes user input and sets up the REST request to create a new Canvassing Efforts record.
        /// </summary>
        /// <param name="user">The IntegrationUser object instance used throughout app runtime.</param>
        public static void CreateEffort (IntegrationUser user)
        {
            Console.Write("\nEnter the project number: ");
            string? projectNum = Console.ReadLine();
            Options options = new(projectNum, "Canvassing Efforts");

            Console.WriteLine("Please enter record information below.");
            Console.Write("\n                                        Name: ");
            string? effortName = Console.ReadLine();
            Console.Write("  Canvassing Project (enter a record number): ");
            string? canvasProject = Console.ReadLine();
            Console.Write("            Start Date (MM-DD-YYYY HH:MM:SS): ");
            string? startDate = Console.ReadLine();
            Console.Write("              End Date (MM-DD-YYYY HH:MM:SS): ");
            string? endDate = Console.ReadLine();
            Console.Write("        All-Encompassing Effort? (Yes or No): ");
            string? allEncompassing = Console.ReadLine();
            Console.Write("           Status (Active, Inactive, Voided): ");
            string? status = Console.ReadLine();
            Console.Write("                  Void (1 for yes, 0 for no): ");
            string? isVoid = Console.ReadLine();

            List<CanvassingEfforts> data =
            [
                new CanvassingEfforts(effortName, canvasProject, startDate, endDate, allEncompassing, status, isVoid)
            ];

            JSONBody<Options, List<CanvassingEfforts>> jsonBody = new(options, data);
            string body = JsonConvert.SerializeObject(jsonBody, Formatting.Indented);

            Console.WriteLine($"\nThank you. Now creating record in Unifier...\n");
            Console.WriteLine(UnifierRequests.CreateBPRecord(user, body));
        }
    }
}
