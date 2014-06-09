using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GNIDA.Loaders
{
    public interface ISpecification : IMetaDataMember
    {
        MemberReference TransformWith(IGenericContext context);
    }
}
