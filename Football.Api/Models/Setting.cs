using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Football.Api.Models
{
    public interface ISetting
    {
        string Issuer { get; set; }
        string SecretKey { get; set; }
    }

    public class Setting : ISetting
    {
        public string Issuer { get; set; }
        public string SecretKey { get; set; }
      
    }

}
