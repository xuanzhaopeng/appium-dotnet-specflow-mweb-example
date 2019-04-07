using System;
using System.Text;
using NLog.Config;
using NLog.LayoutRenderers;
using NLog.LayoutRenderers.Wrappers;
using NLog.Layouts;

namespace Spec.Web.Core.Nlog
{
    //Copy from https://stackoverflow.com/questions/34333794/print-a-multi-line-message-with-nlog
    [LayoutRenderer("replace-newlines-withlayout")]
    [ThreadAgnostic]
    public class ReplaceNewLinesFormatLayoutRendererWrapper : WrapperLayoutRendererBase
    {
        private string m_replacementString = " ";

        public ReplaceNewLinesFormatLayoutRendererWrapper()
        {
            Replacement = Layout.FromString(" ");
        }

        public Layout Replacement { get; set; }

        protected override void Append(StringBuilder builder, NLog.LogEventInfo logEvent)
        {
            m_replacementString = Replacement.Render(logEvent);
            base.Append(builder, logEvent);
        }

        protected override string Transform(string text)
        {
            if(text.Contains(Environment.NewLine))
            {
                return text.Replace(Environment.NewLine, m_replacementString);
            } else if (text.Contains("\n"))
            {
                return text.Replace("\n", m_replacementString);
            }
            return text;
        }
    }
}
