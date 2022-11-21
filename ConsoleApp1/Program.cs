using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;

pymentrequest pymentreques = new pymentrequest();
Console.WriteLine(
await pymentreques.getpaymentdetailAsync());
//Console.WriteLine("Enter Number");
//string add = Console.ReadLine();

//string[] splt = add.Split(' ');
//int sum = int.Parse(splt[0]);
//for (int a = 1; a < splt.Length; a++)
//{
//    if (splt[a].Equals("+"))
//    {
//        sum += int.Parse(splt[a + 1]);
//    }

//    else if (splt[a].Equals("-"))
//    {
//        sum -= int.Parse(splt[a + 1]);
//    }
//    else if (splt[a].Equals("/"))
//    {
//        sum /= int.Parse(splt[a + 1]);
//    }
//    else if (splt[a].Equals("*"))
//    {
//        sum *= int.Parse(splt[a + 1]);
//    }


//}
//Console.WriteLine(sum);



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
            Console.WriteLine(getpaymentdetailAsync());
        }

       
    }



    public async Task<object> getpaymentdetailAsync()
    {

        var token = await getTOken.GenrateTOkenAsync();
        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://test.instamojo.com/v2/payments/MOJO2b02N05A99408257/"),
            Headers =
    {
        { "accept", "application/json" },
       
    },
        };
        request.Headers.Add("Authorization", $"Bearer {token.tokenresponce.access_token}");

        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<object>(body);
        }
    }
}



