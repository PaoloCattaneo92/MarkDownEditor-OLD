using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PaoloCattaneo.DocumentMaker
{
    /// <summary>
    /// This class represent a section of the document
    /// </summary>
    public class Section : SectionContainer
    {
        /// <summary>
        /// Header title
        /// </summary>
        public string HeaderTitle { get; set; }

        /// <summary>
        /// Content, as a list of renderable items
        /// </summary>
        public List<IRenderable> Content { get; set; }
        
        /// <summary>
        /// Heading level of the section
        /// </summary>
        /// <example>
        /// To renders a h1: set this to 1,
        /// to renders a h2 set this to 2 and so on
        /// </example>
        public int HeadingLevel { get; set; }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="headerTitle"><see cref="HeaderTitle"/></param>
        /// <param name="headingLevel"><see cref="HeadingLevel"/></param>
        public Section(string headerTitle, int headingLevel)
        {
            HeaderTitle = headerTitle;
            HeadingLevel = headingLevel;
            Content = new List<IRenderable>();
        }
        /// <summary>
        /// Constructor with default value of <see cref="HeadingLevel"/> to 1.
        /// </summary>
        /// <param name="headerTitle"><see cref="HeaderTitle"/></param>
        public Section(string headerTitle) : this(headerTitle, 1)
        {
        }
        /// <summary>
        /// Add a string as paragraph.
        /// </summary>
        /// <param name="paragraph">Paragraph to add</param>
        public void AddParagraph(string paragraph)
        {
            Content.Add(new Paragraph(paragraph));
        }
        public void AddParagraph()
        {
            AddParagraph("");
        }
        /// <summary>
        /// Add all the lines of a text file to a paragraph.
        /// </summary>
        /// <param name="file">The file containing the paragraph</param>
        public void AddParagraph(FileInfo file)
        {
            AddParagraph(File.ReadAllText(file.FullName));
        }

        /// <summary>
        /// Add a <see cref="IRenderable"/> object to the text
        /// </summary>
        /// <param name="renderable">An object that can be rendered</param>
        public void Add(IRenderable renderable)
        {
            Content.Add(renderable);
        }

        /// <summary>
        /// Add a quote to the text
        /// </summary>
        /// <param name="quoteText">A quote</param>
        public void AddQuote(string quoteText)
        {
            AddParagraph(DocumentMakerConstants.QUOTE + quoteText);
        }
        /// <summary>
        /// Add an horizontal line into the text
        /// </summary>
        public void AddHr()
        {
            Content.Add(new ThematicBreakHR());
        }

        private StringBuilder AppendTitleRender(StringBuilder sb)
        {
            //Title rendering
            for (int i = 0; i < HeadingLevel; i++)
            {
                sb.Append(DocumentMakerConstants.HEADING_CHAR);
            }
            sb.Append(" ").Append(HeaderTitle).Append("\n");
            return sb;
        }

        private StringBuilder AppendContentRender(StringBuilder sb)
        {
            //Main section content rendering (before nested)
            foreach(IRenderable renderable in Content)
            {
                sb = renderable.Render(sb);
            }
            
            //Nested sections rendering
            foreach (Section nestedSection in Sections)
            {
                sb = nestedSection.Render(sb);
            }
            return sb;
        }

        public override StringBuilder Render(StringBuilder sb)
        {
            sb = AppendTitleRender(sb);
            sb = AppendContentRender(sb);
            return sb;
        }


    }
}