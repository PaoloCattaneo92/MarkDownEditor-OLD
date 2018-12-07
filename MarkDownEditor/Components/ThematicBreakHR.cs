using System;
using System.Collections.Generic;
using System.Text;

namespace PaoloCattaneo.DocumentMaker
{
    /// <summary>
    /// This will be rendered as the HTML "<hr>" tag
    /// </summary>
    public class ThematicBreakHR : RenderedObj
    {
        public string Render()
        {
            return DocumentMakerConstants.HR + "\n";
        }

        public StringBuilder Render(StringBuilder sb)
        {
            return sb.Append(Render());
        }
    }
}
