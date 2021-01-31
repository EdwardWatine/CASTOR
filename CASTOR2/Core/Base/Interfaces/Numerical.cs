using System;
using System.Collections.Generic;
using System.Text;
using CASTOR2.Core.Base.Operations.Interfaces;

namespace CASTOR2.Core.Base.Interfaces
{
    public interface IReal : INumeric, IComparable<double>, IComparable<decimal>, IComparable<long>, IComparable<ulong>
    {
        double AsDouble();
    }
    public interface INumeric : IEquatable<double>, IEquatable<decimal>, IEquatable<long>, IEquatable<ulong>
    {

    }
    public interface IField<TField> : IAdd<TField>, IMultiply<TField>
    {

    }
}
