using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace sparkpost_example_csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize
            String api_key = "";

            // Define email elements
            String campaign_id = "test-campaign";

            var recipient_obj = new
            {
                address = new {
                    email = "test@example.com",
                    name = "First Name"
                }
            };
            var from_address_obj = new
            {
                email = "noreply@example.com",
                name = "No Reply"
            };

            String subject_line = "Test Mailing from CSharp";
            String body_html = "<html><body><p>this is a test</p></body></html>";


            // Compile Object for Transmissions API
            var transmission_obj = new
            {
                campaign_id = campaign_id,
                recipients = new[] { recipient_obj },
                content = new {
                    from = from_address_obj,
                    subject = subject_line,
                    html = body_html
                }
            };
            string transmission_json = JsonConvert.SerializeObject(transmission_obj);


            // Send Email
            String URL = "https://api.sparkpost.com/api/v1/transmissions";

            WebRequest v_obj_req = (WebRequest)System.Net.WebRequest.Create(URL);
            v_obj_req.Method = "POST";
            v_obj_req.ContentType = "application/json";
            v_obj_req.ContentLength = Encoding.UTF8.GetBytes(transmission_json).Length;
            v_obj_req.Timeout = 120000;
            v_obj_req.PreAuthenticate = true;
            v_obj_req.Headers.Add("Authorization", api_key);

            Stream v_obj_Stream;
            v_obj_Stream = v_obj_req.GetRequestStream();
            v_obj_Stream.Write(Encoding.UTF8.GetBytes(transmission_json), 0, Encoding.UTF8.GetBytes(transmission_json).Length);
            v_obj_Stream.Close();

            WebResponse v_obj_Response;
            v_obj_Response = v_obj_req.GetResponse();
            Stream v_obj_dataStream = v_obj_Response.GetResponseStream();
            StreamReader v_obj_reader = new StreamReader(v_obj_dataStream);
            String v_st_Ret = v_obj_reader.ReadToEnd();
            v_obj_reader.Close();
            v_obj_Response.Close();
            JObject v_obj_Json;
            v_obj_Json = JObject.Parse(v_st_Ret);

        }
    }
}
