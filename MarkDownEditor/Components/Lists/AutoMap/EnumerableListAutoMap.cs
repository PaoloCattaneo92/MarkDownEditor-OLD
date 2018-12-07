using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaoloCattaneo.DocumentMaker
{
    /// <summary>
    /// A <see cref="DotList"/> that map automatically the values the
    /// properites of an object (set in constructor or calling <see cref="SetItem(E)"/>
    /// This class calls the static <see cref="ListAutoMapper{E}.Parse(E)"/> to parse the object.
    /// </summary>
    /// <typeparam name="E">The type of the object that will be parsed in the list</typeparam>
    public class EnumerableListAutoMap<E> : EnumerableList, IListAutoMap<E>
    {
        public EnumerableListAutoMap(E item)
        {
            SetItem(item);
        }

        public EnumerableListAutoMap()
        {
        }

        public void SetItem(E e)
        {
            Items = ListAutoMapper<E>.Parse(e).ToList();
        }

    }
}
