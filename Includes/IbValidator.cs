using System;
using System.Collections.Generic;
using System.Linq;

namespace InstallerBuilder.Includes
{
    public class IbValidator
    {
        public string[] Rules { get; private set; }


        public IbValidator(string ruleString)
        {
            List<string> rules = new List<string>();
            foreach (string rule in ruleString.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (string.IsNullOrWhiteSpace(rule)) continue;
                if (rule.StartsWith('#')) continue;
                rules.Add(rule);
            }
            this.Rules = rules.ToArray();
        }


        public bool ShouldIgnore(string path)
        {
            foreach (string rule in Rules)
            {
                if (string.Equals(rule, path, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                else if (path.EqualsWildcard(rule, true))
                {
                    return true;
                }
            }

            return false;
            //return Rules.Any(t => );
        }
    }
}