namespace ExcellenceQuest.Business.Contracts
{
    using ExcellenceQuest.Models.Token;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ITokenService
    {
        Task<bool> CheckMicrosoftTokenAsync(TokenRequestModel request);
        string GenerateToken(TokenResponseModel response);
    }
}
