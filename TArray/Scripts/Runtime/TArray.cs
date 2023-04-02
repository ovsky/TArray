using System;
using System.Linq;
using UnityEngine;

#pragma warning disable CS0693 

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

        #if UNITY_EDITOR || DEVELOPMENT_BUILD

        public T this[int x, int y]
        {
            get
            {
                if (x < 0 || x >= size.x || y < 0 || y >= size.y)
                {
                    Debug.LogError($"TArray[{x}, {y}] is out of bounds of size: {size}");
                    return default;
                }

                return data[(size.x * y) + x];
            }
            set 
            {
                if (x < 0 || x >= size.x || y < 0 || y >= size.y)
                {
                    Debug.LogError($"TArray[{x}, {y}] is out of bounds of size: {size}");
                    return;
                }
                data[(size.x * y) + x] = value;
            }
        }

        #else

        public T this[int x, int y]
        {
            get => data[(size.x * y) + x];
            set => data[(size.x * y) + x] = value;
        }
        #endif

        #if UNITY_EDITOR || DEVELOPMENT_BUILD

        public T Get(int x, int y)
        {
            if (x < 0 || x >= size.x || y < 0 || y >= size.y)
            {
                Debug.LogError($"TArray[{x}, {y}] is out of bounds of size: {size}");
                return default;
            }

            return data[(size.x * y) + x];
        }

        #else

        public T Get(int x, int y) => data[(size.x * y) + x];
        
        #endif

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

        public TArray<T> Set(int x, int y, T value)
        {
            data[(size.x * y) + x] = value;
            return this;
        }

        public TArray<T> Set(int x, int y, T value, bool resize)
        {
            if (resize)
            {
                Resize(Mathf.Max(x + 1, size.x), Mathf.Max(y + 1, size.y));
            }

            data[(size.x * y) + x] = value;
            return this;
        }

        public TArray<T> AddSize(int x, int y)
            => Resize(size.x + x, size.y + y);

        public TArray<T> AddRow(T[] x = null, T[] y = null)
        {
            Resize(size.x + (x == null ? 0 : 1), size.y + (y == null ? 0 : 1));

            T[][] rows = new T[2][] { x, y };
            int[] sizes = new int[2] { size.x, size.y };

            for (int a = 0; a < rows.Length; a++)
            {
                if (rows[a]?.Length > 0)
                {
                    for (int i = 0; i < rows[a].Length; i++)
                    {
                        data[(sizes[a] * i) + (sizes[a] - 1)] = rows[a][i];
                    }
                }
            }
            return this;
        }

        #endregion Methods

        #region Overrides

        public bool Equals(TArray<T> other)
        {
            if (size != other.size)
            {
                return false;
            }

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

#pragma warning restore CS0693