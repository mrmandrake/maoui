using System;

namespace Maoui
{
    public class Span : Element
    {
        public Span()
            : base("span")
        {
        }

        public Span(string text)
            : this()
        {
            Text = text;
        }
    }
}
