using System;
using System.Security.Cryptography;
using System.Threading;

namespace TetrisNetCore.Utilities
{
    /// <summary>
    /// Zapewnia funkcję generowania liczb losowych
    /// </summary>
    public static class RandomProvider
    {
        #region Metody
        /// <summary>
        /// Uzyskaj enkapsulację dla funkcji generowania liczb losowych, która jest niezależna dla każdego wątku
        /// </summary>
        private static ThreadLocal<Random> RandomWrapper { get; } = new ThreadLocal<Random>(() =>
        {
            //--- Ponieważ RNGCryptoServiceProvider nie może być używany z PCL, zamiast tego używany jest identyfikator GUID.
            //var @byte = Guid.NewGuid().ToByteArray();
            //var seed = BitConverter.ToInt32(@byte, 0);
            //return new Random(seed);

            byte[] @byte = new byte[sizeof(int)];
            using (RNGCryptoServiceProvider crypto = new())
                crypto.GetBytes(@byte);
            int seed = BitConverter.ToInt32(@byte, 0);
            return new Random(seed);
        });


        /// <summary>
        /// Uzyskaj funkcję generowania liczb losowych, która jest niezależna dla każdego wątku.
        /// </summary>
        public static Random ThreadRandom => RandomWrapper.Value;
        #endregion
    }
}
