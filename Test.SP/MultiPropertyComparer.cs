using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SP
{
    /// <summary>Compares objects by a sequence of properties.</summary>
    /// <typeparam name="T">The compared objects type.</typeparam>
    public class MultiPropertyComparer<T> : Comparer<T>
    {
        private readonly Func<T, object>[] _properties;

        /// <summary>Initializes a new instance of the <see cref="MultiPropertyComparer{T}" /> class.</summary>
        /// <param name="properties">The functions to calculate properties to compare.</param>
        public MultiPropertyComparer(params Func<T, object>[] properties)
        {
            _properties = properties;
        }

        public override int Compare(T x, T y)
        {
            if (x == null && y == null)
            {
                return 0;
            }

            if (x == null)
            {
                return -1;
            }

            if (y == null)
            {
                return 1;
            }

            foreach (var property in _properties)
            {
                int compared;
                var vx = property(x);
                var vy = property(y);

                if (vx is string svx && vy is string svy)
                {
                    var ruCultureInfo = new CultureInfo(1251);
                    var comparer = StringComparer.Create(ruCultureInfo, true);
                    compared = comparer.Compare(svx, svy);
                }
                else
                {
                    compared = Comparer<object>.Default.Compare(property(x), property(y));
                }

                if (compared != 0)
                {
                    return compared;
                }
            }

            return 0;
        }
    }
}
