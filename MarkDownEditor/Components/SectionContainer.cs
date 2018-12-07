using System;
using System.Collections.Generic;
using System.Text;

namespace PaoloCattaneo.DocumentMaker
{
    /// <summary>
    /// This abstract class implement something <see cref="IRenderable"/>
    /// that contains nested sections.
    /// </summary>
    public abstract class SectionContainer : RenderedObj
    {
        /// <summary>
        /// List of nested sections
        /// </summary>
        public List<Section> Sections { get; set; }

        /// <summary>
        /// Constructor. Creates an empty list of Sections.
        /// </summary>
        protected SectionContainer()
        {
            Sections = new List<Section>();
        }
        
        /// <summary>
        /// Add a section to <see cref="Sections"/>
        /// </summary>
        /// <param name="section">A section to add</param>
        public SectionContainer AddSection(Section section)
        {
            Sections.Add(section);
            return this;
        }

        public override StringBuilder Render(StringBuilder sb)
        {
            foreach(Section section in Sections)
            {
                sb = section.Render(sb);
            }
            return sb;
        }



    }
}

