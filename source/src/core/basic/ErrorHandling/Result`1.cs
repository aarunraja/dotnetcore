namespace MLC.Core
{
    using System;

    public class Result<T>
    {
        
        public bool HasValue {get;}
        public bool HasError => !this.HasValue;

        public Error Error { get; set; }
        public T value { get; set; }

        public Result(T value)
        {
            if(value != null)
            {
                this.value = value;
                this.Error = Error.None;
                this.HasValue = true;
            }else
            {
                this.value = value;
                this.Error = Error.SomeError;
                this.HasValue = false;
            }            
        }

        public Result(Error error)
        {
            this.Error = error;
            this.value = default(T);
            this.HasValue = false;
        }

        public static implicit operator Result<T>(T value) => new Result<T>(value);
        public static implicit operator Result<T>(Error error) => new Result<T>(error);

        // override object.Equals
        public override bool Equals(object obj)
        {            
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            
            var tryOther = (Result<T>) obj;
            if(HasValue && tryOther.HasValue)
            {
                return this.value.Equals(tryOther.value);
            }else if(HasError && tryOther.HasError)
            {
                return this.Error.Equals(tryOther.Error);
            }
            return false;
        }
        
        // override object.GetHashCode
        public override int GetHashCode()
        {
            if(HasValue) return value.GetHashCode();
            else return Error.GetHashCode();
        }
    }
}