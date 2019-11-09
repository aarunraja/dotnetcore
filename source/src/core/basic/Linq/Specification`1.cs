namespace MLC.Core
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public class Specification<T> 
    {
        public Expression<Func<T, bool>> predicate;

        public Specification(Expression<Func<T, bool>> predicate)
        {
            this.predicate = predicate;
        }

        public static Specification<T> operator & (Specification<T> leftSpec, Specification<T> rightSpec)
        {
            return new Specification<T>(leftSpec.predicate.AndAlso(rightSpec.predicate));
        }

        public static Specification<T> operator | (Specification<T> leftSpec, Specification<T> rightSpec)
        {
            return new Specification<T>(leftSpec.predicate.OrElse(rightSpec.predicate));
        }

        public static Specification<T> operator ! (Specification<T> spec)
        {
            return new Specification<T>(Expression.Lambda<Func<T, bool>>(Expression.Not(spec.predicate.Body),
                                                       spec.predicate.Parameters.Single()));
        }
    }
}
