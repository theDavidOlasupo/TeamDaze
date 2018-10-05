using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamDaze.BLL.DTO
{
    public class DefaultApiReponse<T> : IApiResponse<T>
    {
        public DefaultApiReponse()
        {
            ValidationErrors = new Dictionary<string, IEnumerable<string>>();
        }

        public string Code { get; set; }
        public string ShortDescription { get; set; }
        public T Object { get; set; }
        public Dictionary<string, IEnumerable<string>> ValidationErrors { get; set; }
    }
}
