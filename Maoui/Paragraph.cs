using System;

namespace Maoui
{
    public class Paragraph : Element
    {
        public Paragraph()
            : base("p")
        {
        }

        public Paragraph(string text)
            : this()
        {
            Text = text;
        }
    }
}
