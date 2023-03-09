using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Extensions
{
    #region TArray

    [SerializeField]
    public interface ITArray
    {
        public TArray<T> Cast<T>();
        public TArray<T> GetArray<T>();
    }

    [Serializable]
    public struct TArray<T> : ITArray, IEquatable<TArray<T>>
    {
        #region Fields

        [SerializeField] private Vector2Int size;
        [SerializeField, HideInInspector] private T[] data;

        #endregion Fields

        #region Custom Operators

        public static implicit operator T[](TArray<T> array) => array.data;
        public static implicit operator T[,](TArray<T> array) => array.Matrix;
        public static implicit operator TArray<T>(T[] array) => new TArray<T>() { data = array, size = new Vector2Int(array.GetLength(0), array.GetLength(1)) };
        public static implicit operator TArray<T>(T[,] array) => new TArray<T>() { data = array.Cast<T>().ToArray(), size = new Vector2Int(array.GetLength(0), array.GetLength(1)) };
        public static implicit operator TArray<T>(Array array) => new TArray<T>() { data = array.Cast<T>().ToArray(), size = new Vector2Int(array.GetLength(0), array.GetLength(1)) };

        #endregion Custom Operators

        #region Properties

        public Vector2Int Size => size;
        public int Length => data.Length;
        public T[] Array => data;
        public T[,] Matrix
        {
            get
            {
                T[,] matrix = new T[size.x, size.y];
                for (int i = 0; i < data.Length; i++)
                {
                    matrix[i % size.x, i / size.x] = data[i];
                }
                return matrix;
            }
        }

        #endregion Properties

        #region Constructors

        public TArray(int x, int y)
        {
            this = new TArray<T>(new Vector2Int(x, y));
        }

        public TArray(Vector2Int size)
        {
            this.size = size;
            data = new T[size.x * size.y];
        }

        public TArray(T[,] matrix)
        {
            size = new Vector2Int(matrix.GetLength(0), matrix.GetLength(1));
            data = matrix.Cast<T>().ToArray();
        }

        public TArray(T[] array, Vector2Int size)
        {
            this.size = size;
            data = array;
        }

        #endregion Constructors

        #region Methods

        public T this[int x, int y]
        {
            get => data[(size.x * y) + x];
            set => data[(size.x * y) + x] = value;
        }

        public T this[Vector2Int pos]
        {
            get => data[(size.x * pos.y) + pos.x];
            set => data[(size.x * pos.y) + pos.x] = value;
        }

        public TArray<T1> Cast<T1>()
            => new TArray<T1>(data.Cast<T1>().ToArray(), size);

        public TArray<T> GetArray<T>()
            => new TArray<T>(data?.Cast<T>()?.ToArray(), size);

        public TArray<T> Resize(int x, int y, bool force = false)
        {
            if (force || (x == size.x && y == size.y))
            {
                return this;
            }
            else if (data.Length == 0)
            {
                data = new T[x * y];
                size = new Vector2Int(x, y);
            }
            else
            {
                T[] backup = data;

                data = new T[x * y];
                size = new Vector2Int(x, y);

                for (int i = 0; i < Mathf.Min(data.Length, backup.Length); i++)
                {
                    data[i] = backup[i];
                }
            }
            return this;
        }

        #endregion Methods

        #region Overrides

        public bool Equals(TArray<T> other)
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (!data[i].Equals(other.data[i]))
                {
                    return false;
                }
            }
            return true;
        }

        #endregion Overrides
    }

    #endregion TArray
}