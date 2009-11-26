﻿namespace JavaLanguageService.Text.Language
{
    using System;
    using System.ComponentModel.Composition;
    using JavaLanguageService.Text.Tagging;
    using Microsoft.VisualStudio.Text.Editor;
    using Microsoft.VisualStudio.Text.Tagging;

    [Export(typeof(ILanguageElementManagerService))]
    internal class LanguageElementManagerService : ILanguageElementManagerService
    {
        internal IBufferTagAggregatorFactoryService TagAggregatorFactory
        {
            get;
            set;
        }

        public ILanguageElementManager GetLanguageElementManager(ITextView textView)
        {
            if (textView == null)
                throw new ArgumentNullException("textView");
            if (!textView.Roles.Contains(PredefinedTextViewRoles.Structured))
                return null;

            return textView.Properties.GetOrCreateSingletonProperty<LanguageElementManager>(
                () =>
                {
                    ITagAggregator<ILanguageElementTag> tagAggregator = TagAggregatorFactory.CreateTagAggregator<ILanguageElementTag>(textView.TextBuffer);
                    LanguageElementManager manager = new LanguageElementManager(textView.TextBuffer, textView.BufferGraph, tagAggregator);
                    textView.Closed += (sender, e) => manager.Dispose();
                    return manager;
                });
        }
    }
}
