using System;
using GraphQL.Types;

namespace Domain
{
    public class ALBSchema : Schema
    {
        public ALBSchema(Func<Type, GraphType> resolveType)
            : base(resolveType)
        {
            Query = (ObjectGraphType)resolveType(typeof (ALBQuery));
        }
    }
}
