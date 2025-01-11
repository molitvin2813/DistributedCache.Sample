using LinqSpecs;
using ProductService.Domain.Interfaces;
using System.Linq.Expressions;

namespace ProductService.Application.Common.Specifications.GeneralSpecifications
{
    public class ByIdSpecification<TId, TEntity>
       : Specification<TEntity>
       where TId : struct, IComparable<TId>, IEquatable<TId>
       where TEntity : class, IEntity<TId>
    {
        public ByIdSpecification(TId id)
        {
            Id = id;
        }

        public TId Id { get; set; }

        public override Expression<Func<TEntity, bool>> ToExpression()
        {
            return x => x.Id.Equals(Id);
        }
    }
}

