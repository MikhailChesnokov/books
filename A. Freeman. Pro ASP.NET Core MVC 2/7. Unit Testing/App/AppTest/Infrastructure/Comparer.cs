namespace AppTest.Infrastructure
{
    using System;
    using System.Collections.Generic;



    public class Comparer
    {
        public static Comparer<T> Get<T>(Func<T, T, bool> func) => new Comparer<T>(func);
    }



    public class Comparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, T, bool> _comparerFunc;

        public Comparer(Func<T, T, bool> comparerFunc) => _comparerFunc = comparerFunc;

        public bool Equals(T x, T y) => _comparerFunc(x, y);

        public int GetHashCode(T obj) => obj.GetHashCode();
    }
}