using System.Reflection;
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
            RestClientOptions? options;

            // Set environment to Production
            if (environment == 0)
            {
                Console.WriteLine("\nPlease wait as we generate an auth token for you...");
                options = new RestClientOptions("https://unifier.oraclecloud.com/hart")
                {
                    Authenticator = new HttpBasicAuthenticator(username, password)
                };
            }
            // Otherwise, set environment to Stage
            else if (environment == 1)
            {
                Console.WriteLine("\nPlease wait as we generate an auth token for you...");
                options = new RestClientOptions("https://unifier.oraclecloud.com/hart/stage")
                {
                    Authenticator = new HttpBasicAuthenticator(username, password)
                };

            } else
            {
                Console.WriteLine("\nYou entered a number out of range. The program will now close.");
                return string.Empty;
            }
            
            // Set up the remainder of the request
            var client = new RestClient(options);
            var request = new RestRequest("/ws/rest/service/v1/login", Method.Get);
            var response = client.Execute(request);

            if (response.Content != null)
            {
                try
                {
                    TokenJSON? tokenJSON = JsonConvert.DeserializeObject<TokenJSON>(response.Content);

                    if (tokenJSON != null)
                    {
                        Console.WriteLine("\nAuth token successfully generated.");
                        return tokenJSON.Token;
                    }
                }
                catch (JsonReaderException ex)
                {
                    Console.WriteLine("\nAn error occurred while reading JSON: " + ex.Message);
                }
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
        /// <returns>If the content of the RestResponse is not null, return the contents. Otherwise, return an empty string.</returns>
        public static string GetBPRecord(IntegrationUser user, string projectNum, string input)
        {
            var client = SetClient(user.Environment);
            var request = new RestRequest($"/ws/rest/service/v1/bp/record/{projectNum}", Method.Get);
            request.AddHeader("Authorization", $"Bearer {user.Token}");
            request.AddParameter("input", input);
            var response = client.Execute(request);
            return response.Content ?? string.Empty;
        }

        /// <summary>
        /// This method performs the REST call to create a Business Process record.
        /// </summary>
        /// <param name="user">The IntegrationUser object containing environment and auth token.</param>
        /// <param name="jsonBody">The JSON-formatted string of the request body</param>
        /// <returns>If the content of the RestResponse is not null, return the contents. Otherwise, return an empty string.</returns>
        public static string CreateBPRecord(IntegrationUser user, string jsonBody)
        {
            var client = SetClient(user.Environment);
            var request = new RestRequest("/ws/rest/service/v2/bp/record", Method.Post);
            request.AddHeader("Authorization", $"Bearer {user.Token}");
            request.AddJsonBody(jsonBody);
            var response = client.Execute(request);
            return response.Content ?? string.Empty;
        }

        /// <summary>
        /// This method performs the REST call to update an existing Business Process record.
        /// </summary>
        /// <param name="user">The IntegrationUser object containing environment and auth token.</param>
        /// <param name="jsonBody">The JSON-formatted string of the request body</param>
        /// <returns>If the content of the RestResponse is not null, return the contents. Otherwise, return an empty string.</returns>
        public static string UpdateBPRecord(IntegrationUser user, string jsonBody)
        {
            var client = SetClient(user.Environment);
            var request = new RestRequest("/ws/rest/service/v2/bp/record", Method.Put);
            request.AddHeader("Authorization", $"Bearer {user.Token}");
            request.AddJsonBody(jsonBody);

            // Uncomment the two lines below for debugging the request
            // ExecuteDebug(client, request);

            var response = client.Execute(request);
            return response.Content ?? string.Empty;
        }

        /// <summary>
        /// This method prints all the info of a REST request. This is mainly for testing and debugging.
        /// </summary>
        /// <param name="client">The RestClient for the request being performed.</param>
        /// <param name="request">The RestRequest for the request being performed.</param>
        /// <returns>The generated RestResponse for debugging.</returns>
        public static RestResponse ExecuteDebug(RestClient client, RestRequest request)
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

        /// <summary>
        /// This method takes record information from a JSON and prints its contents in a slightly cleaner fashion.
        /// </summary>
        /// <param name="record">The JSON containing information of a single record.</param>
        public static void PrintRecordInfo(object record)
        {
            foreach(PropertyInfo property in record.GetType().GetProperties())
            {
                string name = property.Name;
                object value = property.GetValue(record);
                Console.WriteLine($"{name}: {value}");
            }
        }

        /// <summary>
        /// This method is intended to check if a POST or PUT request returned with a 200 status code.
        /// Currently the method just prints the full contents of the JSON that is returned and prints different messages
        /// depending on if the request was a POST or a PUT.
        /// </summary>
        /// <param name="requestType">An integer value that determines whether the request was a POST or a PUT.</param>
        /// <param name="content">The JSON that was returned from the REST request.</param>
        public static void PostPutRequestCheck(int requestType, string? content)
        {
            if (content == string.Empty)
            {
                Console.WriteLine("The HTTP request returned a null response. Now returning to main menu...");
                return;
            }

            if (requestType == 1)
            {
                Console.WriteLine($"The Create Record request returned a JSON. Here's the full contents: \n\n{content}");
            }
            else
            {
                Console.WriteLine($"The Update Record request returned a JSON. Here's the full contents: \n\n{content}");
            }

            Console.WriteLine("\nNow returning to the main menu...");

            //PostPutReturnJSON<List<string>, List<string>, int, int> returnJSON =
            //    JsonConvert.DeserializeObject<PostPutReturnJSON<List<string>, List<string>, int, int>>(content);

            //if (requestType == 1)
            //{
            //    if (returnJSON.Status == 200)
            //    {
            //        Console.WriteLine($"Record creation successful! Here's the full JSON the request returned:\n");
            //        Console.WriteLine(content);
            //        Console.WriteLine("\nNow returning to main menu...");
            //    }
            //    else
            //    {
            //        Console.WriteLine("There was an error in attempting to create the record.");
            //        Console.WriteLine($"{returnJSON.Status}: {returnJSON.Message[0]}");
            //        Console.WriteLine("\nNo record was created. Now returning to main menu...");
            //    }

            //}
            //else
            //{
            //    if (returnJSON.Status == 200)
            //    {
            //        Console.WriteLine($"Record update successful! Here's the full JSON the request returned:\n");
            //        Console.WriteLine(content);
            //        Console.WriteLine("\nNow returning to main menu...");
            //    }
            //    else
            //    {
            //        Console.WriteLine("There was an error in attempting to update the record.");
            //        Console.WriteLine($"{returnJSON.Status}: {returnJSON.Message[0]}");
            //        Console.WriteLine("\nThe record was not updated. Now returning to main menu...");
            //    }
            //}
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

    public class GetReturnJSON<T1, T2, T3> (T1? data, T2? message, T3? status)
    {
        [JsonProperty("data")]
        public T1? Data { get; set; } = data;

        [JsonProperty("message")]
        public T2? Message { get; set; } = message;

        [JsonProperty("status")]
        public T3? Status { get; set; } = status;
    }
    
    // Currently not being used
    public class PostPutReturnJSON<T1, T2, T3, T4> (T1? data, T2? message, T3? status, T4 restAuditID)
    {
        [JsonProperty("data")]
        public T1? Data { get; set; } = data;

        [JsonProperty("message")]
        public T2? Message { get; set; } = message;

        [JsonProperty("status")]
        public T3? Status { get; set; } = status;

        [JsonProperty("rest_audit_id")]
        public T4? RestAuditID { get; set; } = restAuditID;
    }
}
