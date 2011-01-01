﻿namespace Tvl.VisualStudio.Language.Alloy
{
    using System;
    using System.ComponentModel.Composition;
    using Microsoft.VisualStudio.Text;
    using Microsoft.VisualStudio.Text.Tagging;
    using Microsoft.VisualStudio.Utilities;
    using Tvl.VisualStudio.Language.Parsing;
    using Tvl.VisualStudio.Shell.OutputWindow;

    [Export(typeof(ITaggerProvider))]
    [ContentType(AlloyConstants.AlloyContentType)]
    [TagType(typeof(IOutliningRegionTag))]
    public sealed class AlloyOutliningTaggerProvider : ITaggerProvider
    {
        [Import]
        private IBackgroundParserFactoryService BackgroundParserFactoryService
        {
            get;
            set;
        }

        [Import]
        public IOutputWindowService OutputWindowService
        {
            get;
            private set;
        }

        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
        {
            Func<AlloyOutliningTagger> creator = () => new AlloyOutliningTagger(buffer, BackgroundParserFactoryService.GetBackgroundParser(buffer), this);
            return buffer.Properties.GetOrCreateSingletonProperty(creator) as ITagger<T>;
        }
    }
}