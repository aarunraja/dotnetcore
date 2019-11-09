namespace MLC.Core
{
    using System;

    public class Result
    {
        internal readonly Error error;

        public bool HasValue {get;}
        public bool HasError => !this.HasValue; 

        public Result(Error error)
        {
            this.error = error;
            this.HasValue = error == Error.None;
        }

        public static implicit operator Result(Error error) => new Result(error);

        // override object.Equals
        public override bool Equals(object obj)
        {            
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            
            var tryOther = (Result) obj;
            if(HasValue && tryOther.HasValue)
            {
                return true;
            }else if(HasError && tryOther.HasError)
            {
                return this.error.Equals(tryOther.error);
            }
            return false;
        }
        
        // override object.GetHashCode
        public override int GetHashCode()
        {
            return error.GetHashCode();
        }
    }
}