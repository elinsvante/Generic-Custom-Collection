using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomCollections
{

    /// <summary>
    /// New exception to use when an operation is rejected from one of the subscribers.
    /// </summary>
    [Serializable]

    public class OperationRejectedException : System.InvalidOperationException
    {
        public OperationRejectedException()
        {
        }

        public OperationRejectedException(string message) : base(message)
        {
        }

        public OperationRejectedException(string message, Exception inner) : base(message, inner)
        {

        }

    }
}
