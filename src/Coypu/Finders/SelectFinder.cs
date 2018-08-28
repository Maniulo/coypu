using System;
using Coypu.Drivers;

namespace Coypu.Finders
{
    internal class SelectFinder : XPathQueryFinder
    {
        internal SelectFinder(Driver driver, string locator, DriverScope scope, Options options) : base(driver, locator, scope, options) { }

        public override bool SupportsSubstringTextMatching
        {
            get { return true; }
        }

        protected override Func<string, Options, string> GetQuery(Html html)
        {
            if (options.UseExtendedTextLocators)
                return html.SelectExtended;
            else
                return html.Select;
        }

        internal override string QueryDescription
        {
            get { return "select: " + Locator; }
        }
    }
}