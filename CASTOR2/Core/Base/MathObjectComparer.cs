using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace CASTOR2.Core.Base
{
    public class MathObjectComparer : IComparer<MathObject>
    {
        private int? letter(char l, char r)
        {
            bool lic = char.IsLetter(l);
            bool ric = char.IsLetter(r);
            if (lic && ric)
            {
                return l - r;
            }
            if (lic ^ ric)
            {
                return lic ? -1 : 1;
            }
            return null;
        }
        private int? digit(char l, char r)
        {
            bool lid = char.IsDigit(l);
            bool rid = char.IsDigit(r);
            if (lid && rid)
            {
                return l - r;
            }
            if (lid ^ rid)
            {
                return lid ? -1 : 1;
            }
            return null;
        }
        private int? bracket(char l, char r)
        {
            bool lib = char.GetUnicodeCategory(l) == UnicodeCategory.OpenPunctuation;
            bool rib = char.GetUnicodeCategory(r) == UnicodeCategory.OpenPunctuation;
            if (lib && rib)
            {
                return l - r;
            }
            if (lib ^ rib)
            {
                return lib ? 1 : -1;
            }
            return null;
        }
        public int Compare(MathObject x, MathObject y)
        {
            string sx = x.ToString(), sy = y.ToString();
            int len = sx.Length > sy.Length ? sy.Length : sx.Length;
            for (int i = 0; i < len; i++)
            {
                char cx = sx[i], cy = sy[i];
                int diff = letter(cx, cy) ?? digit(cx, cy) ?? bracket(cx, cy) ?? cx - cy;
                if (diff != 0)
                {
                    return diff;
                }
            }
            int ldif = sx.Length - sy.Length;
            if (ldif != 0)
            {
                return ldif;
            }
            return x.GetType().AssemblyQualifiedName.CompareTo(y.GetType().AssemblyQualifiedName);
        }
    }
}
