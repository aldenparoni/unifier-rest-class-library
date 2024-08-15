using Newtonsoft.Json;
using UnifierWebServicesLibrary;

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

        // Constructor for creating new ESI record
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

        // Constructor for deserializing JSON-formatted string
        [JsonConstructor]
        public EngineersSupplementalInstructions(string? thirdParty, string? associatedRFI, string? cppName, string? cppNum, string? contract,
            string? contractorRef, string? costImpact, string? creationDate, string? creator, string? creatorCompany, string? decision,
            string? dueDate, string? esiStatus, string? notes, string? prefixPublishPath, string? proposedDecision, string? publishPath,
            string? recordNo, string? scheduleImpact, string? specs, string? status, string? title)
        {
            ThirdPartyReviewers = thirdParty;
            AssociatedRFI = associatedRFI;
            CPPName = cppName;
            CPPNumber = cppNum;
            Contract = contract;
            ContractorReference = contractorRef;
            CostImpact = costImpact;
            CreationDate = creationDate;
            Creator = creator;
            CreatorCompany = creatorCompany;
            Decision = decision;
            DueDate = dueDate;
            ESIStatus = esiStatus;
            Notes = notes;
            PrefixPublishPath = prefixPublishPath;
            ProposedDecision = proposedDecision;
            PublishPath = publishPath;
            RecordNo = recordNo;
            ScheduleImpact = scheduleImpact;
            Specifications = specs;
            Status = status;
            Title = title;
        }

        /// <summary>
        /// This method takes user input and sets up the REST request to create a new ESI record.
        /// </summary>
        /// <param name="user">The IntegrationUser object instance used throughout app runtime.</param>
        public static void CreateESI(IntegrationUser user)
        {
            // Set up the Options portion of the JSON body
            Console.Write("\nEnter the project number: ");
            string? projectNum = Console.ReadLine();
            WorkflowDetails workflowDetails = new("Any Name", "System Integration", "Submit");
            Options options = new(projectNum, "Engineer's Supplemental Instructions (ESI)", workflowDetails);

            // Gather user input of new record information
            Console.WriteLine("Please enter record information below:\n");
            Console.Write("                                   Title: ");
            string? esiTitle = Console.ReadLine();
            Console.Write("                 Cost Impact (Yes or No): ");
            string? costImpact = Console.ReadLine();
            Console.Write("             Schedule Impact (Yes or No): ");
            string? schedImpact = Console.ReadLine();
            Console.Write("      Contract (default: CT-HRT-2000106): ");
            string? contractNum = Console.ReadLine();
            Console.Write("  Associated RFI (enter a record number): ");
            string? assocRFI = Console.ReadLine();
            Console.Write("                     3rd Party Reviewers: ");
            string? thirdParty = Console.ReadLine();
            Console.Write("                    Contractor Reference: ");
            string? contractRef = Console.ReadLine();
            Console.Write("                                   Notes: ");
            string? notes = Console.ReadLine();
            Console.Write("                          Specifications: ");
            string? specs = Console.ReadLine();

            // Set up the Data portion of the JSON body
            List<EngineersSupplementalInstructions> data =
            [
                new EngineersSupplementalInstructions(esiTitle, costImpact, schedImpact, contractNum, assocRFI, thirdParty, contractRef, notes, specs)
            ];

            JSONBody<Options, List<EngineersSupplementalInstructions>> jsonBody = new(options, data);
            string body = JsonConvert.SerializeObject(jsonBody, Formatting.Indented);

            // Send the HTTP request
            Console.WriteLine($"\nThank you. Now creating record in Unifier...\n");
            string requestContent = UnifierRequests.CreateBPRecord(user, body);

            // Check if the HTTP request returned a 200 status code
            UnifierRequests.PostPutRequestCheck(1, requestContent);
        }

        /// <summary>
        /// This method takes user input to update all editable fields in the ESI object.
        /// The fields that are updated in this method are that which are in the Creation step.
        /// </summary>
        public void UpdateESI()
        {
            Console.WriteLine("\nWe will now begin updating the record.");

            Console.WriteLine($"\n                                       Title: {Title}");
            Console.Write("                                   New Title: ");
            Title = Console.ReadLine();

            Console.WriteLine($"\n                                 Cost Impact: {CostImpact}");
            Console.Write($"                 New Cost Impact (Yes or No): ");
            CostImpact = Console.ReadLine();

            Console.WriteLine($"\n                             Schedule Impact: {ScheduleImpact}");
            Console.Write($"             New Schedule Impact (Yes or No): ");
            ScheduleImpact = Console.ReadLine();

            Console.WriteLine($"\n                                    Contract: {Contract}");
            Console.Write($"      New Contract (default: CT-HRT-2000106): ");
            Contract = Console.ReadLine();

            Console.WriteLine($"\n                              Associated RFI: {AssociatedRFI}");
            Console.Write($"  New Associated RFI (enter a record number): ");
            AssociatedRFI = Console.ReadLine();

            Console.WriteLine($"\n                         3rd Party Reviewers: {ThirdPartyReviewers}");
            Console.Write($"                     New 3rd Party Reviewers: ");
            ThirdPartyReviewers = Console.ReadLine();

            Console.WriteLine($"\n                        Contractor Reference: {ContractorReference}");
            Console.Write($"                    New Contractor Reference: ");
            ContractorReference = Console.ReadLine();

            Console.WriteLine($"\n                                       Notes: {Notes}");
            Console.Write($"                                   New Notes: ");
            Notes = Console.ReadLine();

            Console.WriteLine($"\n                              Specifications: {Specifications}");
            Console.Write($"                          New Specifications: ");
            Specifications = Console.ReadLine();
        }
    }

    // Canvassing Efforts - a non-workflow BP I will test
    public class CanvassingEfforts
    {
        [JsonProperty("uxceEndDate", NullValueHandling = NullValueHandling.Ignore)]
        public string? EndDate { get; set; } = null;

        [JsonProperty("uuu_record_last_update_date", NullValueHandling = NullValueHandling.Ignore)]
        public string? RecordLastUpdateDate { get; set; } = null;

        [JsonProperty("uuu_creation_date", NullValueHandling = NullValueHandling.Ignore)]
        public string? CreationDate { get; set; } = null;

        [JsonProperty("piCanvassingProjectBP", NullValueHandling = NullValueHandling.Ignore)]
        public string? CanvassingProject { get; set; } = null;

        [JsonProperty("piAllEncompassingYNRB", NullValueHandling = NullValueHandling.Ignore)]
        public string? AllEncompassingEffort { get; set; } = null;

        [JsonProperty("record_no", NullValueHandling = NullValueHandling.Ignore)]
        public string? RecordNo { get; set; } = null;

        [JsonProperty("genVoidCB", NullValueHandling = NullValueHandling.Ignore)]
        public string? Void { get; set; } = null;

        [JsonProperty("creator_id", NullValueHandling = NullValueHandling.Ignore)]
        public string? Creator { get; set; } = null;

        [JsonProperty("uxceStartDate", NullValueHandling = NullValueHandling.Ignore)]
        public string? StartDate { get; set; } = null;

        [JsonProperty("uuu_dm_publish_path", NullValueHandling = NullValueHandling.Ignore)]
        public string? PublishPath { get; set; } = null;

        [JsonProperty("uuu_bp_record_url", NullValueHandling = NullValueHandling.Ignore)]
        public string? BPRecordURL { get; set; } = null;

        [JsonProperty("uxceEffortName1", NullValueHandling = NullValueHandling.Ignore)]
        public string? Name { get; set; } = null;

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string? Status { get; set; } = null;

        [JsonProperty("integration_key", NullValueHandling = NullValueHandling.Ignore)]
        public string? IntegrationKey { get; set; } = null;

        [JsonProperty("piAttachNumPlusYesNoIA", NullValueHandling = NullValueHandling.Ignore)]
        public string? AttachmentCountVerification { get; set; } = null;

        // Constructor for creating new Canvassing Effort record
        public CanvassingEfforts(string? name, string? canvassingProject, string? startDate, string? endDate, 
            string? allEncompassing, string? status, string? isVoid)
        {
            Name = name;
            CanvassingProject = canvassingProject;
            StartDate = startDate;
            EndDate = endDate;
            AllEncompassingEffort = allEncompassing;
            Status = status;
            Void = isVoid;
        }

        // Constructor for deserializing JSON-formatted string
        [JsonConstructor]
        public CanvassingEfforts(string? endDate, string? recordLastUpdate, string? creationDate, string? canvassingProject, 
            string? allEncompassing, string? recordNo, string? isVoid, string? creator, string? startDate, string? publishPath,
            string? bpRecordURL, string? name, string? status, string? integrationKey, string? attachCount)
        {
            EndDate = endDate;
            RecordLastUpdateDate = recordLastUpdate;
            CreationDate = creationDate;
            CanvassingProject = canvassingProject;
            AllEncompassingEffort = allEncompassing;
            RecordNo = recordNo;
            Void = isVoid;
            Creator = creator;
            StartDate = startDate;
            PublishPath = publishPath;
            BPRecordURL = bpRecordURL;
            Name = name;
            Status = status;
            IntegrationKey = integrationKey;
            AttachmentCountVerification = attachCount;
        }

        /// <summary>
        /// This method takes user input and sets up the REST request to create a new Canvassing Efforts record.
        /// </summary>
        /// <param name="user">The IntegrationUser object instance used throughout app runtime.</param>
        public static void CreateEffort (IntegrationUser user)
        {
            // Set up the Options portion of the JSON body
            Console.Write("\nEnter the project number: ");
            string? projectNum = Console.ReadLine();
            Options options = new(projectNum, "Canvassing Efforts");

            // Gather user input of new record information
            Console.WriteLine("Please enter record information below.\n");
            Console.Write("                                 Effort Name: ");
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

            // Set up the Data portion of the JSON body
            List<CanvassingEfforts> data =
            [
                new CanvassingEfforts(effortName, canvasProject, startDate, endDate, allEncompassing, status, isVoid)
            ];

            JSONBody<Options, List<CanvassingEfforts>> jsonBody = new(options, data);
            string body = JsonConvert.SerializeObject(jsonBody, Formatting.Indented);

            // Send the HTTP request
            Console.WriteLine($"\nThank you. Now creating record in Unifier...\n");
            string requestContent = UnifierRequests.CreateBPRecord(user, body);

            // Check if the HTTP request returned a 200 status code
            UnifierRequests.PostPutRequestCheck(1, requestContent);
        }

        /// <summary>
        /// This method takes user input to update all editable fields in the CanvassingEfforts object.
        /// </summary>
        public void UpdateEffort()
        {
            Console.WriteLine("\nWe will now begin updating the record.");

            Console.WriteLine($"\n                                     Effort Name: {Name}");
            Console.Write("                                 New Effort Name: ");
            Name = Console.ReadLine();

            Console.WriteLine($"\n                              Canvassing Project: {CanvassingProject}");
            Console.Write("  New Canvassing Project (enter a record number): ");
            CanvassingProject = Console.ReadLine();

            Console.WriteLine($"\n                                      Start Date: {StartDate}");
            Console.Write("            New Start Date (MM-DD-YYYY HH:MM:SS): ");
            StartDate = Console.ReadLine();

            Console.WriteLine($"\n                                        End Date: {EndDate}");
            Console.Write("              New End Date (MM-DD-YYYY HH:MM:SS): ");
            EndDate = Console.ReadLine();

            Console.WriteLine($"\n                         All-Encompassing Effort: {AllEncompassingEffort}");
            Console.Write("         New All-Encompassing Effort (Yes or No): ");
            AllEncompassingEffort = Console.ReadLine();

            Console.WriteLine($"\n                                          Status: {Status}");
            Console.Write("           New Status (Active, Inactive, Voided): ");
            Status = Console.ReadLine();

            Console.WriteLine($"\n                                            Void: {Void}");
            Console.Write("                  New Void (1 for yes, 0 for no): ");
            Void = Console.ReadLine();
        }
    }
}
