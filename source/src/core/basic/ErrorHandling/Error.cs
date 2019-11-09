namespace MLC.Core
{
    using System;

    public static class ErrorConstants
    {
        public const int NoneErrorValue = -1;
        public const int SomeErrorValue = 0;
    }

    public class Error
    {
        private int errCode;
        private string plainMsg;
        private bool isCodeBasedError;
        private Exception exception;

        public static readonly Error None = new Error(ErrorConstants.NoneErrorValue);
        public static readonly Error SomeError = new Error(ErrorConstants.SomeErrorValue);

        public Error(int errCode)
        {
            this.errCode = errCode;
            isCodeBasedError = true;
        }

        public Error(int errCode,string message)
        {
            this.plainMsg = message;
            isCodeBasedError = false;
        }

        public Error(Exception exception)
        {
            this.exception = exception;
            this.errCode = ErrorConstants.SomeErrorValue;
            isCodeBasedError = true;
        }

        private void FromAnotherError(Error anotherError)
        {
            this.errCode = anotherError.errCode;
            this.isCodeBasedError = anotherError.isCodeBasedError;
            this.plainMsg = anotherError.plainMsg;
        }

        public int Code => errCode;
        public string Message => plainMsg;
        public bool IsNone => this.Equals(Error.None);
        public Exception Exception => exception;

        public static Error Of(int errorCode) => new Error(errorCode);
        public static Error Of(int errorCode,string message) => new Error(errorCode,message);
        public static Error Of(Exception exception) => new Error(exception);

        
        public static Result<T> As<T>(int errCode) => new Result<T>(Error.Of(errCode));
        public static Result<T> As<T>(int errCode,string message) => new Result<T>(Error.Of(errCode, message));
        public static Result<T> As<T>(Error error) => new Result<T>(error);

        public override bool Equals(object obj)
        {
            if (obj is Error)
            {
                var errorOther = (Error)obj;

                if (this.Code == errorOther.Code) return true;
                if((!string.IsNullOrEmpty(this.Message) && !string.IsNullOrEmpty(errorOther.Message)) && 
                    this.Message.Equals(errorOther.Message, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                if(this.exception != null && this.exception.Equals(errorOther.exception))
                {
                    return true;
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            if (isCodeBasedError) return this.errCode.GetHashCode();
            else return this.plainMsg.GetHashCode();
        }
    }
}