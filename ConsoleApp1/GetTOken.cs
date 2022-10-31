using Newtonsoft.Json;

public class GetTOken
{
    HttpClient client = new();
    Tokenresponce temp = new();
    public async Task<Responece> GenrateTOkenAsync()
    {

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://test.instamojo.com/oauth2/token/"),
            Headers =
    {
        { "accept", "application/json" },
    },
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
    {
        { "grant_type", "client_credentials" },
        { "client_id", "test_3goYA7nGppFHIIr3THm6H8lxocji0SDQyyD" },
        { "client_secret", "test_8lbcz6MmbYH5Zjp5LxaiLlDKzIAJE5xD9X3rIH8gYjxRXsssBjFsByUjTaO3RJt77c5289T1xDRuT15w2Sm8apgO1qkabICa3i4rbcd53WjS5RwuN3GXPrPCcqr" },
    }),
        };



        using (var response = await client.SendAsync(request))
        {
            if (response.IsSuccessStatusCode)
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var temp = JsonConvert.DeserializeObject<Tokenresponce>(body);
                return new Responece { error=null,tokenresponce= temp};
            }
            else
            {
                var body = await response.Content.ReadAsStringAsync();
                var temp = JsonConvert.DeserializeObject<Responece>(body);
                return temp;

            }
        }



    }
}
