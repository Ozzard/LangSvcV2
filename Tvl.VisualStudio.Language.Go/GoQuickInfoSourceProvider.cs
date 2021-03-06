﻿namespace Tvl.VisualStudio.Language.Go
{
    using System.ComponentModel.Composition;
    using Microsoft.VisualStudio.Utilities;
    using IOutputWindowService = Tvl.VisualStudio.OutputWindow.Interfaces.IOutputWindowService;
    using IQuickInfoSource = Microsoft.VisualStudio.Language.Intellisense.IQuickInfoSource;
    using IQuickInfoSourceProvider = Microsoft.VisualStudio.Language.Intellisense.IQuickInfoSourceProvider;
    using ITextBuffer = Microsoft.VisualStudio.Text.ITextBuffer;

    [Export(typeof(IQuickInfoSourceProvider))]
    [Order]
    [ContentType(GoConstants.GoContentType)]
    [Name("GoQuickInfoSource")]
    internal class GoQuickInfoSourceProvider : IQuickInfoSourceProvider
    {
        //[Import]
        //public GoIntellisenseCache IntellisenseCache
        //{
        //    get;
        //    private set;
        //}

        [Import]
        public IOutputWindowService OutputWindowService
        {
            get;
            private set;
        }

        public IQuickInfoSource TryCreateQuickInfoSource(ITextBuffer textBuffer)
        {
            return new GoQuickInfoSource(textBuffer, this);
        }
    }
}
