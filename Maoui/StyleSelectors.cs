using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Maoui
{
    public class StyleSelectors
    {
        public static StyleSelectors Styles => ElementHandler.rules;

        private static readonly Dictionary<string, Style> styles = new Dictionary<string, Style>();

        public Style this[string selector]
        {
            get
            {
                var key = selector ?? "";
                lock (styles)
                {
                    if (!styles.TryGetValue(key, out Style r))
                    {
                        r = new Style();
                        styles.Add(key, r);
                    }
                    return r;
                }
            }
            set
            {
                var key = selector ?? "";
                lock (styles)
                {
                    if (value == null)
                    {
                        styles.Remove(key);
                    }
                    else
                    {
                        styles[key] = value;
                    }
                }
            }
        }

        public void Clear()
        {
            lock (styles)
            {
                styles.Clear();
            }
        }

        public override string ToString()
        {
            lock (styles)
            {
                var q =
                    from s in styles
                    let v = s.Value.ToString()
                    where v.Length > 0
                    select s.Key + " {" + s.Value.ToString() + "}";
                return String.Join("\n", q);
            }
        }
    }
}
