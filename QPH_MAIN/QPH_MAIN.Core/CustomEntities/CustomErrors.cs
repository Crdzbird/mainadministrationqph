using System.Collections.Generic;

namespace QPH_MAIN.Core.CustomEntities
{
    public class CustomErrors
    {
        public CustomErrors()
        {
            messages = new List<string>();
        }
        public List<string> messages { get; set; }
    }
}
