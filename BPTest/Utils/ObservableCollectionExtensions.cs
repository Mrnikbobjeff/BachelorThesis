using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BPTest.Utils
{
    public static class ObservableCollectionExtensions
    {
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> values)
        {
            foreach(var item in values)
            {
                collection.Add(item);
            }
        }
    }
}
