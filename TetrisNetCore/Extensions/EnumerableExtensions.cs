using System;
using System.Collections.Generic;
using System.Linq;

namespace TetrisNetCore.Extensions
{
    /// <summary>
    /// IEnumerable&lt;T&gt; Zapewnia rozszerzenia dla IEnumerable&lt;T&gt;
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Dodaj indeks do elementu kolekcji
        /// </summary>
        /// <typeparam name="T">Typ elementu kolekcji</typeparam>
        /// <param name="self">kolekcja</param>
        /// <returns>Zindeksowana kolekcja</returns>
        public static IEnumerable<IndexedItem<T>> WithIndex<T>(this IEnumerable<T> self)
        {
            if (self == null)
                throw new ArgumentNullException(nameof(self));
            return self.Select((x, i) => new IndexedItem<T>(i, x));
        }


        /// <summary>
        /// <para>Dodaj indeks do elementu kolekcji</para>
        /// <para>Indeks jest zwiększany, gdy spełnione są określone warunki</para>
        /// </summary>
        /// <typeparam name="T">Typ elementu kolekcji</typeparam>
        /// <param name="self">kolekcja</param>
        /// <param name="predicate">Warunek przyrostu</param>
        /// <returns>Zindeksowana kolekcja</returns>
        public static IEnumerable<IndexedItem<T>> WithIndex<T>(this IEnumerable<T> self, Func<T, bool> predicate)
        {
            if (self == null) throw new ArgumentNullException(nameof(self));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            int i = 0;
            foreach (T x in self)
            {
                if (predicate(x))
                    i++;
                yield return new IndexedItem<T>(i, x);
            }
        }


        /// <summary>
        /// Dodaj indeks do elementu kolekcji.
        /// </summary>
        /// <typeparam name="T">Typ elementu kolekcji</typeparam>
        /// <param name="self">kolekcja</param>
        /// <returns>Zindeksowana kolekcja</returns>
        public static IEnumerable<IndexedItem2<T>> WithIndex<T>(this T[,] self)
        {
            if (self == null)
                throw new ArgumentNullException(nameof(self));

            for (int x = 0; x < self.GetLength(0); x++)
                for (int y = 0; y < self.GetLength(1); y++)
                    yield return new IndexedItem2<T>(x, y, self[x, y]);
        }


        /// <summary>
        /// Generuje dwuwymiarowy słownik.
        /// </summary>
        /// <typeparam name="TSource">Typ elementu kolekcji</typeparam>
        /// <typeparam name="TKeyX">Klucz osi X </typeparam>
        /// <typeparam name="TKeyY">Klucz osi Yー</typeparam>
        /// <param name="self">Kolekcja</param>
        /// <param name="xSelector">Selektor klucza osi X</param>
        /// <param name="ySelector">Selektor klucza osi Y</param>
        /// <returns>Słownik dwuwymiarowy</returns>
        public static Dictionary<TKeyX, Dictionary<TKeyY, TSource>> ToDictionary2<TSource, TKeyX, TKeyY>
            (
                this IEnumerable<TSource> self,
                Func<TSource, TKeyX> xSelector,
                Func<TSource, TKeyY> ySelector
            )
        {
            if (self == null) throw new ArgumentNullException(nameof(self));
            if (xSelector == null) throw new ArgumentNullException(nameof(xSelector));
            if (ySelector == null) throw new ArgumentNullException(nameof(ySelector));

            return self.GroupBy(xSelector)
                    .ToDictionary(x => x.Key, xs => xs.ToDictionary(ySelector));
        }


        /// <summary>
        /// Generuje dwuwymiarowy słownik.
        /// </summary>
        /// <typeparam name="TSource">Typ elementu kolekcji</typeparam>
        /// <typeparam name="TKeyX">Klucz osi X</typeparam>
        /// <typeparam name="TKeyY">Klucz osi Y</typeparam>
        /// <typeparam name="TElement">Typ elementu wynikowego</typeparam>
        /// <param name="self">kolekcja</param>
        /// <param name="xSelector">Selektor osi X</param>
        /// <param name="ySelector">Selector osi Y</param>
        /// <param name="elementSelector">Wybór elementu</param>
        /// <returns>Słownik dwuwymiarowy</returns>
        public static Dictionary<TKeyX, Dictionary<TKeyY, TElement>> ToDictionary2<TSource, TKeyX, TKeyY, TElement>
            (
                this IEnumerable<TSource> self,
                Func<TSource, TKeyX> xSelector,
                Func<TSource, TKeyY> ySelector,
                Func<TSource, TElement> elementSelector
            )
        {
            if (self == null) throw new ArgumentNullException(nameof(self));
            if (xSelector == null) throw new ArgumentNullException(nameof(xSelector));
            if (ySelector == null) throw new ArgumentNullException(nameof(ySelector));
            if (elementSelector == null) throw new ArgumentNullException(nameof(elementSelector));

            return self.GroupBy(xSelector)
                    .ToDictionary(x => x.Key, xs => xs.ToDictionary(ySelector, elementSelector));
        }
    }

}
