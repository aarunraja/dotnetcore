namespace Bapatla.CMS.Core.Base
{
    using System;

    public abstract class DomainEntity
    {
        public virtual string Id {set;get;}
        public virtual DateTime CreatedOn { get; set; }
        public virtual String CreatedBy { get; set; }
        public virtual DateTime ModifiedOn { get; set; }
        public virtual String ModifiedBy { get; set; }

        public virtual bool IsNew()
        {
            return String.IsNullOrEmpty(this.Id);
        }
        public virtual void GenerateNewIdentity(String input = "")
        {
            this.Id = (string.IsNullOrEmpty(input) ? Guid.NewGuid().ToString() : input);
        }

    }
}
