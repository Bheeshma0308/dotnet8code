namespace ExcellenceQuest.Models.Token
{
   
    public class TokenRequestModel
    {
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public string Id_token { get; set; }
        public string ClientName { get; set; }
    }
}
