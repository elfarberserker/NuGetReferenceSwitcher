using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NuGetReferenceSwitcher.Presentation.Models
{
    public static class NugetReferenceFilterExtension
    {
        public static IEnumerable<ReferenceModel> FilterByRegex(this IEnumerable<ReferenceModel> models, string pattern)
        {
            if (string.IsNullOrEmpty(pattern))
            {
                return models;
            }

            var matchingModels = new List<ReferenceModel>();
            var regEx = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            foreach (var model in models)
            {
                if (regEx.IsMatch(model.Name))
                {
                    matchingModels.Add(model);
                }
            }

            return matchingModels;
        }
    }
}
