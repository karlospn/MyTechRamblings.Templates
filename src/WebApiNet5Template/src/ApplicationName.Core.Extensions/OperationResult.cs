using System.Collections.Generic;
using System.Linq;

namespace ApplicationName.Core.Extensions
{
    public class OperationResult<TResult>
    {
        public OperationResult()
        {
            Errors = new List<ErrorResult>();
        }
        public TResult Result { get; set; }
        public List<ErrorResult> Errors { get;  }
        public bool HasErrors => Errors != null && Errors.Any();

    }
}