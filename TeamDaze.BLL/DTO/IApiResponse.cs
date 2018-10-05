using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamDaze.BLL.DTO
{
    public interface IApiResponse<T>
    {
        string Code { get; set; }
        string ShortDescription { get; set; }
        T Object { get; set; }
        Dictionary<string, IEnumerable<string>> ValidationErrors { get; set; }
    }
}
