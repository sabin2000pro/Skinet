using Core;

namespace Infrastructure;

public class SpecificationEvaluator<T> where T : BaseEntity {
    public static IQueryable<T> GetQuery(IQueryable<T> query, ISpecification<T> spec) {

        if(spec.Criteria != null) {
            query = query.Where(spec.Criteria); // For the where query.Where(x => x.Brand == brand) for example
        }

        return query;

    }

}