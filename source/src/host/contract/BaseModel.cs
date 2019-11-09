using System;

namespace MLC.General.Contract
{
    public class BaseModel
    {
        public string Id { get; set; }
        public virtual DateTime CreatedOn { get; set; }
        public virtual String CreatedBy { get; set; }
        public virtual DateTime ModifiedOn { get; set; }
        public virtual String ModifiedBy { get; set; }
        public bool IsDelete { get; set; }
    }
}