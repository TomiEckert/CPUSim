using System.Collections.Generic;
using System.Linq;
using Simulator.Instructions;

namespace Simulator.Utils {
    public static class StringExtension {
        public static string ReplaceAll(this string text, IEnumerable<string> collection, string replacement) {
            return collection.Aggregate(text, (current, item) => current.Replace(item, replacement));
        }
    }
}