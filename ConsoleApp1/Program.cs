using System.Net.Http.Headers;
using System.Text.Json.Nodes;

pymentrequest pymentreques = new pymentrequest();
await pymentreques.requestpaymentAsync();



public class pymentrequest
{
    HttpClient client = new();
    GetTOken getTOken = new GetTOken();

    public async Task requestpaymentAsync()
    {

        var token = await getTOken.GenrateTOkenAsync();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://test.instamojo.com/v2/payment_requests/"),

            Headers =
    {
        { "accept", "application/json" },
        
    },

            Content = new FormUrlEncodedContent(new Dictionary<string, string>
    {
        { "allow_repeated_payments", "false" },
        { "send_email", "false" },
        { "send_sms", "false" },
          { "amount", "520" },
        { "purpose", "Shopp" },
    }),
        };
        request.Headers.Add("Authorization", $"Bearer {token.tokenresponce.access_token}");

        using (var response = await client.SendAsync(request))
        {

            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            Console.WriteLine(body);
        }
    }

}
