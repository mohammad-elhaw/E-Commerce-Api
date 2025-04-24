using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<T> GetQuery<T>(IQueryable<T> inputQuery, 
            Specifications<T> specifications) where T : class
        {
            var query = inputQuery;
            if(specifications.Criteria is not null) query = query.Where(specifications.Criteria);
            
            //foreach(var item in specifications.IncludeExpressions)
            //{
            //    query = query.Include(item);
            //}
            query = specifications.IncludeExpressions.Aggregate(query, (currentQuery, includeExpression) => 
            currentQuery.Include(includeExpression));

            if(specifications.OrderBy is not null)
                query = query.OrderBy(specifications.OrderBy);
            else if(specifications.OrderByDescending is not null)
                query = query.OrderByDescending(specifications.OrderByDescending);

            if (specifications.Skip > 0)
                query = query.Skip(specifications.Skip);

            if (specifications.Take > 0)
                query = query.Take(specifications.Take);

            //query = query.Skip(specifications.Skip).Take(specifications.Take);
            return query;
        }
    }
}
// dbcontext.Set<T>() ==> IQuerable
// dbcontext.Set<T>().Where(specification.Criteria).Include(expression)