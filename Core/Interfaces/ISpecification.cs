using System.Linq.Expressions;

namespace Core;

public interface ISpecification<T> {
    Expression<Func<T, bool>> Criteria {get; }
}