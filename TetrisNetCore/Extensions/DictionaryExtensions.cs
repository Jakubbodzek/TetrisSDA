using System;
using System.Collections.Generic;

namespace TetrisNetCore.Extensions
{
    /// <summary>
    /// Zapewnia rozszerzenia do System.Collection.Generic.Dictionary.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Uzyskaj wartość. Jeśli wartość nie istnieje, zwraca wartość domyślną.
        /// </summary>
        /// <typeparam name="TKey">Typ klucza</typeparam>
        /// <typeparam name="TValue">Typ wartości</typeparam>
        /// <param name="self">Słownik docelowy</param>
        /// <param name="key">Klucz</param>
        /// <param name="defaultValue">Domyślna wartość</param>
        /// <returns>Wartość</returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> self, TKey key, TValue defaultValue = default(TValue))
        {
            if (self == null)
                throw new ArgumentNullException(nameof(self));

            TValue result;
            return self.TryGetValue(key, out result)
                ? result
                : defaultValue;
        }
    }
}
