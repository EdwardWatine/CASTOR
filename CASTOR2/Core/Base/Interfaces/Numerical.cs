using System;
using System.Collections.Generic;
using System.Text;

namespace CASTOR2.Core.Base.Interfaces
{
    public interface IReal : INumeric, IComparable<double>, IComparable<decimal>, IComparable<long>, IComparable<ulong>
    {

    }
    public interface INumeric : IEquatable<double>, IEquatable<decimal>, IEquatable<long>, IEquatable<ulong>
    {

    }
}
