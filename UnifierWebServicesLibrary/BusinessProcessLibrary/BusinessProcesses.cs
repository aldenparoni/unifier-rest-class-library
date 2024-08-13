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
            Console.Write("\nEnter the project number: ");
            string? projectNum = Console.ReadLine();
            WorkflowDetails workflowDetails = new("Any Name", "System Integration", "Submit");
            Options options = new(projectNum, "Engineer's Supplemental Instructions (ESI)", workflowDetails);

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

            List<EngineersSupplementalInstructions> data =
            [
                new EngineersSupplementalInstructions(esiTitle, costImpact, schedImpact, contractNum, assocRFI, thirdParty, contractRef, notes, specs)
            ];

            JSONBody<Options, List<EngineersSupplementalInstructions>> jsonBody = new(options, data);
            string body = JsonConvert.SerializeObject(jsonBody, Formatting.Indented);

            Console.WriteLine($"\nThank you. Now creating record in Unifier...\n");
            string requestContent = UnifierRequests.CreateBPRecord(user, body);
            
            if (requestContent != string.Empty)
            {
                // Deserialize the JSON-formatted string into a ReturnJSON object
                ReturnJSON<List<EngineersSupplementalInstructions>, List<string>, int> returnJSON =
                    JsonConvert.DeserializeObject<ReturnJSON<List<EngineersSupplementalInstructions>, List<string>, int>>(requestContent);
                if (returnJSON.Status == 200)
                {
                    Console.WriteLine($"ESI record creation successful! Here's the full record information:\n");
                    UnifierRequests.PrintRecordInfo(returnJSON.Data[0]);
                    Console.WriteLine("\nNow returning to main menu...");
                }
                else
                {
                    Console.WriteLine("There was an error in attempting to create the record.");
                    Console.WriteLine($"{returnJSON.Status}: {returnJSON.Message[0]}");
                    Console.WriteLine("No record was created. Now returning to main menu...");
                }
                return;
            }

            Console.WriteLine("The HTTP request returned a null response. Now returning to main menu...");
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
            string requestContent = UnifierRequests.CreateBPRecord(user, body);

            if (requestContent != string.Empty)
            {
                // Deserialize the JSON-formatted string into a ReturnJSON object
                ReturnJSON<List<CanvassingEfforts>, List<string>, int> returnJSON =
                    JsonConvert.DeserializeObject<ReturnJSON<List<CanvassingEfforts>, List<string>, int>>(requestContent);
                if (returnJSON.Status == 200)
                {
                    Console.WriteLine($"Canvassing Efforts record creation successful! Here's the full record information:\n");
                    UnifierRequests.PrintRecordInfo(returnJSON.Data[0]);
                    Console.WriteLine("Now returning to main menu...");
                }
                else
                {
                    Console.WriteLine("There was an error in attempting to create the record.");
                    Console.WriteLine($"{returnJSON.Status}: {returnJSON.Message[0]}");
                    Console.WriteLine("No record was created. Now returning to main menu...");
                }
            }

            Console.WriteLine("The HTTP request returned a null response. Now returning to main menu...");
        }

        public void IterateEditableFields()
        {
            Console.WriteLine($"Effort Name: {Name}");
            Console.WriteLine($"Canvassing Project: {CanvassingProject}");
            Console.WriteLine($"Start Date: {StartDate}");
            Console.WriteLine($"End Date: {EndDate}");
            Console.WriteLine($"All-Encompassing Effort: {AllEncompassingEffort}");
            Console.WriteLine($"Status: {Status}");
            Console.WriteLine($"Void: {Void}");
        }
    }
}
