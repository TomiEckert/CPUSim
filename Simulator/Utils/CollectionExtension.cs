using System.Collections.Generic;
using System.Linq;

namespace Simulator.Utils {
    public static class CollectionExtension {
        public static void PushRange<T>(this Stack<T> stack, IEnumerable<T> collection, bool reverse = false) {
            var enumerable = collection as T[] ?? collection.ToArray();
            if (!reverse)
                enumerable = enumerable.Reverse().ToArray();
            foreach (var item in enumerable) stack.Push(item);
        }
    }
}