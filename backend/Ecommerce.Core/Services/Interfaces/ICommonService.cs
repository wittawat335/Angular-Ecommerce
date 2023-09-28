using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Services.Interfaces
{
    public interface ICommonService
    {
        string Encrypt(string text);
        string Decrypt(string text);
        string GetPositionName(string code);
        string GetParameter(string code);
    }
}
