using System;
using System.Collections;
using System.Collections.Generic;

namespace bgs
{
	public class Map<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
	{
		private delegate TRet Transform<TRet>(TKey key, TValue value);

		public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IEnumerator, IDisposable
		{
			private Map<TKey, TValue> dictionary;

			private int next;

			private int stamp;

			internal KeyValuePair<TKey, TValue> current;

			public KeyValuePair<TKey, TValue> Current => current;

			internal TKey CurrentKey
			{
				get
				{
					VerifyCurrent();
					return current.Key;
				}
			}

			internal TValue CurrentValue
			{
				get
				{
					VerifyCurrent();
					return current.Value;
				}
			}

			object IEnumerator.Current
			{
				get
				{
					VerifyCurrent();
					return current;
				}
			}

			internal Enumerator(Map<TKey, TValue> dictionary)
			{
				this = default(Enumerator);
				this.dictionary = dictionary;
				stamp = dictionary.generation;
			}

			public bool MoveNext()
			{
				VerifyState();
				if (next < 0)
				{
					return false;
				}
				while (next < dictionary.touchedSlots)
				{
					int num = next++;
					if (((uint)dictionary.linkSlots[num].HashCode & 0x80000000u) != 0)
					{
						current = new KeyValuePair<TKey, TValue>(dictionary.keySlots[num], dictionary.valueSlots[num]);
						return true;
					}
				}
				next = -1;
				return false;
			}

			void IEnumerator.Reset()
			{
				Reset();
			}

			internal void Reset()
			{
				VerifyState();
				next = 0;
			}

			private void VerifyState()
			{
				if (dictionary == null)
				{
					throw new ObjectDisposedException(null);
				}
				if (dictionary.generation != stamp)
				{
					throw new InvalidOperationException("out of sync");
				}
			}

			private void VerifyCurrent()
			{
				VerifyState();
				if (next <= 0)
				{
					throw new InvalidOperationException("Current is not valid");
				}
			}

			public void Dispose()
			{
				dictionary = null;
			}
		}

		public sealed class KeyCollection : ICollection<TKey>, IEnumerable<TKey>, IEnumerable, ICollection
		{
			public struct Enumerator : IEnumerator<TKey>, IEnumerator, IDisposable
			{
				private Map<TKey, TValue>.Enumerator host_enumerator;

				public TKey Current => host_enumerator.current.Key;

				object IEnumerator.Current => host_enumerator.CurrentKey;

				internal Enumerator(Map<TKey, TValue> host)
				{
					host_enumerator = host.GetEnumerator();
				}

				public void Dispose()
				{
					host_enumerator.Dispose();
				}

				public bool MoveNext()
				{
					return host_enumerator.MoveNext();
				}

				void IEnumerator.Reset()
				{
					host_enumerator.Reset();
				}
			}

			private Map<TKey, TValue> dictionary;

			public int Count => dictionary.Count;

			bool ICollection<TKey>.IsReadOnly => true;

			bool ICollection.IsSynchronized => false;

			object ICollection.SyncRoot => ((ICollection)dictionary).SyncRoot;

			public KeyCollection(Map<TKey, TValue> dictionary)
			{
				if (dictionary == null)
				{
					throw new ArgumentNullException("dictionary");
				}
				this.dictionary = dictionary;
			}

			public void CopyTo(TKey[] array, int index)
			{
				dictionary.CopyToCheck(array, index);
				dictionary.CopyKeys(array, index);
			}

			public Enumerator GetEnumerator()
			{
				return new Enumerator(dictionary);
			}

			void ICollection<TKey>.Add(TKey item)
			{
				throw new NotSupportedException("this is a read-only collection");
			}

			void ICollection<TKey>.Clear()
			{
				throw new NotSupportedException("this is a read-only collection");
			}

			bool ICollection<TKey>.Contains(TKey item)
			{
				return dictionary.ContainsKey(item);
			}

			bool ICollection<TKey>.Remove(TKey item)
			{
				throw new NotSupportedException("this is a read-only collection");
			}

			IEnumerator<TKey> IEnumerable<TKey>.GetEnumerator()
			{
				return GetEnumerator();
			}

			void ICollection.CopyTo(Array array, int index)
			{
				TKey[] array2 = array as TKey[];
				if (array2 != null)
				{
					CopyTo(array2, index);
					return;
				}
				dictionary.CopyToCheck(array, index);
				dictionary.Do_ICollectionCopyTo(array, index, Map<TKey, TValue>.pick_key);
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}
		}

		public sealed class ValueCollection : ICollection<TValue>, IEnumerable<TValue>, IEnumerable, ICollection
		{
			public struct Enumerator : IEnumerator<TValue>, IEnumerator, IDisposable
			{
				private Map<TKey, TValue>.Enumerator host_enumerator;

				public TValue Current => host_enumerator.current.Value;

				object IEnumerator.Current => host_enumerator.CurrentValue;

				internal Enumerator(Map<TKey, TValue> host)
				{
					host_enumerator = host.GetEnumerator();
				}

				public void Dispose()
				{
					host_enumerator.Dispose();
				}

				public bool MoveNext()
				{
					return host_enumerator.MoveNext();
				}

				void IEnumerator.Reset()
				{
					host_enumerator.Reset();
				}
			}

			private Map<TKey, TValue> dictionary;

			public int Count => dictionary.Count;

			bool ICollection<TValue>.IsReadOnly => true;

			bool ICollection.IsSynchronized => false;

			object ICollection.SyncRoot => ((ICollection)dictionary).SyncRoot;

			public ValueCollection(Map<TKey, TValue> dictionary)
			{
				if (dictionary == null)
				{
					throw new ArgumentNullException("dictionary");
				}
				this.dictionary = dictionary;
			}

			public void CopyTo(TValue[] array, int index)
			{
				dictionary.CopyToCheck(array, index);
				dictionary.CopyValues(array, index);
			}

			public Enumerator GetEnumerator()
			{
				return new Enumerator(dictionary);
			}

			void ICollection<TValue>.Add(TValue item)
			{
				throw new NotSupportedException("this is a read-only collection");
			}

			void ICollection<TValue>.Clear()
			{
				throw new NotSupportedException("this is a read-only collection");
			}

			bool ICollection<TValue>.Contains(TValue item)
			{
				return dictionary.ContainsValue(item);
			}

			bool ICollection<TValue>.Remove(TValue item)
			{
				throw new NotSupportedException("this is a read-only collection");
			}

			IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
			{
				return GetEnumerator();
			}

			void ICollection.CopyTo(Array array, int index)
			{
				TValue[] array2 = array as TValue[];
				if (array2 != null)
				{
					CopyTo(array2, index);
					return;
				}
				dictionary.CopyToCheck(array, index);
				dictionary.Do_ICollectionCopyTo(array, index, Map<TKey, TValue>.pick_value);
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}
		}

		private const int INITIAL_SIZE = 4;

		private const float DEFAULT_LOAD_FACTOR = 0.9f;

		private const int NO_SLOT = -1;

		private const int HASH_FLAG = int.MinValue;

		private int[] table;

		private Link[] linkSlots;

		private TKey[] keySlots;

		private TValue[] valueSlots;

		private IEqualityComparer<TKey> hcp;

		private int touchedSlots;

		private int emptySlot;

		private int count;

		private int threshold;

		private int generation;

		public int Count => count;

		public TValue this[TKey key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				int num = hcp.GetHashCode(key) | int.MinValue;
				for (int num2 = table[(num & 0x7FFFFFFF) % table.Length] - 1; num2 != -1; num2 = linkSlots[num2].Next)
				{
					if (linkSlots[num2].HashCode == num && hcp.Equals(keySlots[num2], key))
					{
						return valueSlots[num2];
					}
				}
				throw new KeyNotFoundException();
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				int num = hcp.GetHashCode(key) | int.MinValue;
				int num2 = (num & 0x7FFFFFFF) % table.Length;
				int num3 = table[num2] - 1;
				int num4 = -1;
				if (num3 != -1)
				{
					while (linkSlots[num3].HashCode != num || !hcp.Equals(keySlots[num3], key))
					{
						num4 = num3;
						num3 = linkSlots[num3].Next;
						if (num3 == -1)
						{
							break;
						}
					}
				}
				if (num3 == -1)
				{
					if (++count > threshold)
					{
						Resize();
						num2 = (num & 0x7FFFFFFF) % table.Length;
					}
					num3 = emptySlot;
					if (num3 == -1)
					{
						num3 = touchedSlots++;
					}
					else
					{
						emptySlot = linkSlots[num3].Next;
					}
					linkSlots[num3].Next = table[num2] - 1;
					table[num2] = num3 + 1;
					linkSlots[num3].HashCode = num;
					keySlots[num3] = key;
				}
				else if (num4 != -1)
				{
					linkSlots[num4].Next = linkSlots[num3].Next;
					linkSlots[num3].Next = table[num2] - 1;
					table[num2] = num3 + 1;
				}
				valueSlots[num3] = value;
				generation++;
			}
		}

		public KeyCollection Keys => new KeyCollection(this);

		public ValueCollection Values => new ValueCollection(this);

		public Map()
		{
			Init(4, null);
		}

		public Map(int count)
		{
			Init(count, null);
		}

		public Map(IEqualityComparer<TKey> comparer)
		{
			Init(4, comparer);
		}

		private void Init(int capacity, IEqualityComparer<TKey> hcp)
		{
			this.hcp = hcp ?? EqualityComparer<TKey>.Default;
			capacity = Math.Max(1, (int)((float)capacity / 0.9f));
			InitArrays(capacity);
		}

		private void InitArrays(int size)
		{
			table = new int[size];
			linkSlots = new Link[size];
			emptySlot = -1;
			keySlots = new TKey[size];
			valueSlots = new TValue[size];
			touchedSlots = 0;
			threshold = (int)((float)table.Length * 0.9f);
			if (threshold == 0 && table.Length != 0)
			{
				threshold = 1;
			}
		}

		private void CopyToCheck(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (index > array.Length)
			{
				throw new ArgumentException("index larger than largest valid index of array");
			}
			if (array.Length - index < Count)
			{
				throw new ArgumentException("Destination array cannot hold the requested elements!");
			}
		}

		private void CopyKeys(TKey[] array, int index)
		{
			for (int i = 0; i < touchedSlots; i++)
			{
				if (((uint)linkSlots[i].HashCode & 0x80000000u) != 0)
				{
					array[index++] = keySlots[i];
				}
			}
		}

		private void CopyValues(TValue[] array, int index)
		{
			for (int i = 0; i < touchedSlots; i++)
			{
				if (((uint)linkSlots[i].HashCode & 0x80000000u) != 0)
				{
					array[index++] = valueSlots[i];
				}
			}
		}

		private static KeyValuePair<TKey, TValue> make_pair(TKey key, TValue value)
		{
			return new KeyValuePair<TKey, TValue>(key, value);
		}

		private static TKey pick_key(TKey key, TValue value)
		{
			return key;
		}

		private static TValue pick_value(TKey key, TValue value)
		{
			return value;
		}

		private void CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
		{
			CopyToCheck(array, index);
			for (int i = 0; i < touchedSlots; i++)
			{
				if (((uint)linkSlots[i].HashCode & 0x80000000u) != 0)
				{
					array[index++] = new KeyValuePair<TKey, TValue>(keySlots[i], valueSlots[i]);
				}
			}
		}

		private void Do_ICollectionCopyTo<TRet>(Array array, int index, Transform<TRet> transform)
		{
			Type typeFromHandle = typeof(TRet);
			Type elementType = array.GetType().GetElementType();
			try
			{
				if ((typeFromHandle.IsPrimitive || elementType.IsPrimitive) && !elementType.IsAssignableFrom(typeFromHandle))
				{
					throw new Exception();
				}
				object[] array2 = (object[])array;
				for (int i = 0; i < touchedSlots; i++)
				{
					if (((uint)linkSlots[i].HashCode & 0x80000000u) != 0)
					{
						array2[index++] = transform(keySlots[i], valueSlots[i]);
					}
				}
			}
			catch (Exception innerException)
			{
				throw new ArgumentException("Cannot copy source collection elements to destination array", "array", innerException);
			}
		}

		private void Resize()
		{
			int num = HashPrimeNumbers.ToPrime((table.Length << 1) | 1);
			int[] array = new int[num];
			Link[] array2 = new Link[num];
			for (int i = 0; i < table.Length; i++)
			{
				for (int num2 = table[i] - 1; num2 != -1; num2 = linkSlots[num2].Next)
				{
					int num3 = ((array2[num2].HashCode = hcp.GetHashCode(keySlots[num2]) | int.MinValue) & 0x7FFFFFFF) % num;
					array2[num2].Next = array[num3] - 1;
					array[num3] = num2 + 1;
				}
			}
			table = array;
			linkSlots = array2;
			TKey[] destinationArray = new TKey[num];
			TValue[] destinationArray2 = new TValue[num];
			Array.Copy(keySlots, 0, destinationArray, 0, touchedSlots);
			Array.Copy(valueSlots, 0, destinationArray2, 0, touchedSlots);
			keySlots = destinationArray;
			valueSlots = destinationArray2;
			threshold = (int)((float)num * 0.9f);
		}

		public void Add(TKey key, TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int num = hcp.GetHashCode(key) | int.MinValue;
			int num2 = (num & 0x7FFFFFFF) % table.Length;
			int num3;
			for (num3 = table[num2] - 1; num3 != -1; num3 = linkSlots[num3].Next)
			{
				if (linkSlots[num3].HashCode == num && hcp.Equals(keySlots[num3], key))
				{
					throw new ArgumentException("An element with the same key already exists in the dictionary.");
				}
			}
			if (++count > threshold)
			{
				Resize();
				num2 = (num & 0x7FFFFFFF) % table.Length;
			}
			num3 = emptySlot;
			if (num3 == -1)
			{
				num3 = touchedSlots++;
			}
			else
			{
				emptySlot = linkSlots[num3].Next;
			}
			linkSlots[num3].HashCode = num;
			linkSlots[num3].Next = table[num2] - 1;
			table[num2] = num3 + 1;
			keySlots[num3] = key;
			valueSlots[num3] = value;
			generation++;
		}

		public void Clear()
		{
			if (count != 0)
			{
				count = 0;
				Array.Clear(table, 0, table.Length);
				Array.Clear(keySlots, 0, keySlots.Length);
				Array.Clear(valueSlots, 0, valueSlots.Length);
				Array.Clear(linkSlots, 0, linkSlots.Length);
				emptySlot = -1;
				touchedSlots = 0;
				generation++;
			}
		}

		public bool ContainsKey(TKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int num = hcp.GetHashCode(key) | int.MinValue;
			for (int num2 = table[(num & 0x7FFFFFFF) % table.Length] - 1; num2 != -1; num2 = linkSlots[num2].Next)
			{
				if (linkSlots[num2].HashCode == num && hcp.Equals(keySlots[num2], key))
				{
					return true;
				}
			}
			return false;
		}

		public bool ContainsValue(TValue value)
		{
			IEqualityComparer<TValue> @default = EqualityComparer<TValue>.Default;
			for (int i = 0; i < table.Length; i++)
			{
				for (int num = table[i] - 1; num != -1; num = linkSlots[num].Next)
				{
					if (@default.Equals(valueSlots[num], value))
					{
						return true;
					}
				}
			}
			return false;
		}

		public bool Remove(TKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int num = hcp.GetHashCode(key) | int.MinValue;
			int num2 = (num & 0x7FFFFFFF) % table.Length;
			int num3 = table[num2] - 1;
			if (num3 == -1)
			{
				return false;
			}
			int num4 = -1;
			while (linkSlots[num3].HashCode != num || !hcp.Equals(keySlots[num3], key))
			{
				num4 = num3;
				num3 = linkSlots[num3].Next;
				if (num3 == -1)
				{
					break;
				}
			}
			if (num3 == -1)
			{
				return false;
			}
			count--;
			if (num4 == -1)
			{
				table[num2] = linkSlots[num3].Next + 1;
			}
			else
			{
				linkSlots[num4].Next = linkSlots[num3].Next;
			}
			linkSlots[num3].Next = emptySlot;
			emptySlot = num3;
			linkSlots[num3].HashCode = 0;
			keySlots[num3] = default(TKey);
			valueSlots[num3] = default(TValue);
			generation++;
			return true;
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int num = hcp.GetHashCode(key) | int.MinValue;
			for (int num2 = table[(num & 0x7FFFFFFF) % table.Length] - 1; num2 != -1; num2 = linkSlots[num2].Next)
			{
				if (linkSlots[num2].HashCode == num && hcp.Equals(keySlots[num2], key))
				{
					value = valueSlots[num2];
					return true;
				}
			}
			value = default(TValue);
			return false;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return new Enumerator(this);
		}

		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
		{
			return new Enumerator(this);
		}

		public Enumerator GetEnumerator()
		{
			return new Enumerator(this);
		}
	}
}
