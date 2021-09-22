namespace TetrisNetCore.Extensions
{
    /// <summary>
    /// Reprezentuje indeksowany element
    /// </summary>
    /// <typeparam name="T">Typ elementu</typeparam>
    public struct IndexedItem<T>
    {
        #region Właściwości
        /// <summary>
        /// Indeks elementu
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// Element
        /// </summary>
        public T Element { get; }
        #endregion

        #region Konstruktor
        /// <summary>
        /// Tworzy instancję
        /// </summary>
        /// <param name="index">Indeks elementu</param>
        /// <param name="element">Element</param>
        internal IndexedItem(int index, T element)
        {
            this.Index = index;
            this.Element = element;
        }
        #endregion
    }

    /// <summary>
    /// Reprezentuje dwuwymiarowy indeksowany element.
    /// </summary>
    /// <typeparam name="T">Typ elementu</typeparam>
    public struct IndexedItem2<T>
    {
        #region Stałe
        /// <summary>
        /// Pobiera indeks osi X.
        /// </summary>
        public int X { get; }

        /// <summary>
        /// Pobiera indeks osi Y.
        /// </summary>
        public int Y { get; }

        /// <summary>
        ///Element
        /// </summary>
        public T Element { get; }
        #endregion

        #region Konstruktor
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        /// <param name="x">Indeks na osi X</param>
        /// <param name="y">Indeks osi Y</param>
        /// <param name="element">Element</param>
        internal IndexedItem2(int x, int y, T element)
        {
            this.X = x;
            this.Y = y;
            this.Element = element;
        }
        #endregion
    }
}
