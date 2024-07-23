using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;

namespace UnifierWebServicesLibrary
{
    public class IntegrationUser(int environment, string username, string password)
    {
        public int? Environment { get; set; } = environment;
        public string? Token { get; set; } = UnifierRequests.GetAuthToken(environment, username, password);
    }

    public class UnifierRequests
    {
        /// <summary>
        /// This method sets up and performs the Unifier REST V1 request to acquire an authorization token.
        /// </summary>
        /// <param name="env"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>The bearer token that will be used during the console app's runtime</returns>
        public static string GetAuthToken(int environment, string username, string password)
        {
            Console.WriteLine("\nPlease wait as we generate an auth token for you...");
            RestClientOptions? options;

            // Set environment to Production
            if (environment == 0)
            {
                options = new RestClientOptions("https://unifier.oraclecloud.com/hart")
                {
                    Authenticator = new HttpBasicAuthenticator(username, password)
                };
            }
            // Otherwise, set environment to Stage
            else
            {
                options = new RestClientOptions("https://unifier.oraclecloud.com/hart/stage")
                {
                    Authenticator = new HttpBasicAuthenticator(username, password)
                };

            }
            var client = new RestClient(options);
            var request = new RestRequest("/ws/rest/service/v1/login", Method.Get);
            var response = client.Execute(request);

            if (response.Content != null)
            {
                TokenJSON? tokenJSON = JsonConvert.DeserializeObject<TokenJSON>(response.Content);
                Console.WriteLine("\nAuth token successfully generated.");
                return tokenJSON.Token;
            }

            Console.WriteLine("\nAuth token generation failed.");
            return string.Empty;
        }

        /// <summary>
        /// This method performs the REST call to grab the information of a single Business Process record.
        /// </summary>
        /// <param name="user">The IntegrationUser object containing environment and auth token.</param>
        /// <param name="projectNum">The shell number to navigate to.</param>
        /// <param name="input">The GetRecordInput object to be serialized as a JSON param for the REST call.</param>
        public static string GetBPRecord(IntegrationUser user, string projectNum, GetRecordInput input)
        {
            var client = SetClient(user.Environment);
            var request = new RestRequest($"/ws/rest/service/v1/bp/record/{projectNum}", Method.Get);
            string? inputParam = JsonConvert.SerializeObject(input);
            request.AddHeader("Authorization", $"Bearer {user.Token}");
            request.AddParameter("input", inputParam);
            var response = client.Execute(request);
            if (response.Content != null)
            {
                Console.WriteLine("Displaying the resulting json...\n");
                return response.Content;
            }
            return string.Empty;
        }

        // public static void CreateBPRecord()

        /// <summary>
        /// This method prints all the info of a REST request. This is mainly for testing and debugging.
        /// </summary>
        /// <param name="client">The RestClient for the request being performed.</param>
        /// <param name="request">The RestRequest for the request being performed.</param>
        /// <returns>The generated RestResponse for debugging.</returns>
        public static RestResponse ExecuteResponse(RestClient client, RestRequest request)
        {
            var response = client.Execute(request);

            // Check for errors
            if (response.ErrorException != null)
            {
                Console.WriteLine("Error: " + response.ErrorException.Message);
                if (response.ErrorException.InnerException != null)
                {
                    Console.WriteLine("Inner Error: " + response.ErrorException.InnerException.Message);
                }
            }
            else
            {
                // Print the response details
                Console.WriteLine("Response Status: " + response.StatusCode);
                Console.WriteLine("Response Content: " + response.Content ?? "No Content");
                Console.WriteLine("Response Error Message: " + response.ErrorMessage);
                Console.WriteLine("Response Headers: ");
                if (response.Headers != null)
                {
                    foreach (var header in response.Headers)
                    {
                        Console.WriteLine($"{header.Name}: {header.Value}");
                    }
                }
                else
                {
                    Console.WriteLine("No Headers");
                }
            }

            return response;
        }

        /// <summary>
        /// This method turns char input for password into asterisks (*) while reading the input.
        /// </summary>
        /// <returns>This method will return the user's password as a string</returns>
        public static string ReadPassword()
        {
            string password = string.Empty;
            ConsoleKey key;

            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && password.Length > 0)
                {
                    Console.Write("\b \b"); // Erase the last * character
                    password = password.Substring(0, password.Length - 1);
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    password += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);

            return password;
        }

        /// <summary>
        /// This method takes the int user input for Unifier environment and sets where REST services will be performed in the app session.
        /// </summary>
        /// <param name="env">The user input from the beginning of the program. If 0, production; otherwise, stage.</param>
        /// <returns>The RestClient of the selected Unifier environment.</returns>
        public static RestClient SetClient(int? env)
        {
            // If user entered 0, set client environment to Production
            if (env == 0)
            {
                return new RestClient("https://unifier.oraclecloud.com/hart");
            }
            // Otherwise, set client environment to Stage
            return new RestClient("https://unifier.oraclecloud.com/hart/stage");
        }
    }

    public class TokenJSON(string tokenExpirationDate, string timeZone, string tokenExpirationTime, string statusCode, string token)
    {
        [JsonProperty("expiryDate")]
        public string TokenExpirationDate { get; set; } = tokenExpirationDate;

        [JsonProperty("Timezone")]
        public string TimeZone { get; set; } = timeZone;

        [JsonProperty("expiryTime")]
        public string TokenExpirationTime { get; set; } = tokenExpirationTime;

        [JsonProperty("status")]
        public string StatusCode { get; set; } = statusCode;

        [JsonProperty("token")]
        public string Token { get; set; } = token;
    }

    public class GetRecordInput
    {
        [JsonProperty("bpname")]
        public string? BPName { get; set; }

        [JsonProperty("record_no")]
        public string? RecordNum { get; set; }

        [JsonProperty("lineitem", NullValueHandling = NullValueHandling.Ignore)]
        public string? LineItem { get; set; }

        [JsonProperty("lineitem_file", NullValueHandling = NullValueHandling.Ignore)]
        public string? LineItemFile { get; set; }

        [JsonProperty("general_comments", NullValueHandling = NullValueHandling.Ignore)]
        public string? GeneralComments { get; set; }

        [JsonProperty("attach_all_publications", NullValueHandling = NullValueHandling.Ignore)]
        public string? AttachAllPublications { get; set; }

        public GetRecordInput(string bpName, string recordNum)
        {
            BPName = bpName;
            RecordNum = recordNum;
            LineItem = null;
            LineItemFile = null;
            GeneralComments = null;
            AttachAllPublications = null;
        }

        public GetRecordInput(string bpName, string recordNum, string lineItem)
        {
            BPName = bpName;
            RecordNum = recordNum;
            LineItem = lineItem;
            LineItemFile = null;
            GeneralComments = null;
            AttachAllPublications = null;
        }

        public GetRecordInput(string bpName, string recordNum, string lineItem, string lineItemFile, 
            string generalComments, string attachAllPublications)
        {
            BPName = bpName;
            RecordNum = recordNum;
            LineItem = lineItem;
            LineItemFile = lineItemFile;
            GeneralComments = generalComments;
            AttachAllPublications = attachAllPublications;
        }
    }

    public class WorkflowDetails
    {
        [JsonProperty("workflow_name", NullValueHandling = NullValueHandling.Ignore)]
        public string? WorkflowName { get; set; }

        [JsonProperty("user_name", NullValueHandling = NullValueHandling.Ignore)]
        public string? Username { get; set; }

        [JsonProperty("action_name", NullValueHandling = NullValueHandling.Ignore)]
        public string? ActionName { get; set; }

        [JsonProperty("WFCurrentStepName", NullValueHandling = NullValueHandling.Ignore)]
        public string? CurrentStep { get; set; }

        [JsonProperty("WFActionName", NullValueHandling = NullValueHandling.Ignore)]
        public string? NextAction { get; set; }

        public WorkflowDetails(string workflowName, string username, string actionName)
        {
            WorkflowName = workflowName;
            Username = username;
            ActionName = actionName;
            CurrentStep = null;
            NextAction = null;
        }

        public WorkflowDetails(string currentStep, string nextAction)
        {
            WorkflowName = null;
            Username = null;
            ActionName = null;
            CurrentStep = currentStep;
            NextAction = nextAction;
        }
    }

    public class Options
    {
        [JsonProperty("project_number", NullValueHandling = NullValueHandling.Ignore)]
        public string? ProjectNumber { get; set; }

        [JsonProperty("bpname", NullValueHandling = NullValueHandling.Ignore)]
        public string? BPName { get; set; }

        [JsonProperty("workflow_details", NullValueHandling = NullValueHandling.Ignore)]
        public WorkflowDetails? WorkflowDetails { get; set; }

        public Options(string projectNumber, string bpName)
        {
            ProjectNumber = projectNumber;
            BPName = bpName;
            WorkflowDetails = null;
        }

        public Options(string projectNumber, string bpName, WorkflowDetails? workflowDetails)
        {
            ProjectNumber = projectNumber;
            BPName = bpName;
            WorkflowDetails = workflowDetails;
        }
    }

    public class JSONBody<T1, T2> (T1? options, T2? data)
    {
        [JsonProperty("options")]
        public T1? Options { get; set; } = options;

        [JsonProperty("data")]
        public T2? Data { get; set; } = data;
    }
}
