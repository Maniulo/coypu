using System;
using System.Collections.Generic;
using System.Linq;

namespace Coypu.Finders
{
    public abstract class ElementFinder
    {
        protected internal Driver Driver;
        private readonly string locator;
        protected DriverScope Scope;
        protected readonly Options options;

        public void ChangeScope(DriverScope scope)
        {
            Scope = scope;
        }

        protected ElementFinder(Driver driver, string locator, DriverScope scope, Options options)
        {
            Driver = driver;
            this.locator = locator;
            Scope = scope;
            this.options = options;
        }

        public Options Options
        {
            get { return options; }
        }

        public abstract bool SupportsSubstringTextMatching { get; }

        internal string Locator { get { return locator; } }

        internal abstract IEnumerable<Element> Find(Options options);

        internal abstract string QueryDescription { get; }

        protected internal virtual Exception GetMissingException()
        {
            return new MissingHtmlException("Unable to find " + QueryDescription);
        }

        internal ElementScope AsScope()
        {
            return new SynchronisedElementScope(this, Scope, options);
        }

        public void SetDriver(Driver d)
        {
            Driver = d;
        }
    }

}