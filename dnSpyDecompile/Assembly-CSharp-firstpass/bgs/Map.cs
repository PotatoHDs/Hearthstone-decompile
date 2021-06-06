using System;
using System.Collections;
using System.Collections.Generic;

namespace bgs
{
	// Token: 0x02000259 RID: 601
	public class Map<TKey, TValue> : IEnumerable<KeyValuePair<!0, !1>>, IEnumerable
	{
		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x060024E5 RID: 9445 RVA: 0x000825E3 File Offset: 0x000807E3
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x1700068E RID: 1678
		public TValue this[TKey key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				int num = this.hcp.GetHashCode(key) | int.MinValue;
				for (int num2 = this.table[(num & int.MaxValue) % this.table.Length] - 1; num2 != -1; num2 = this.linkSlots[num2].Next)
				{
					if (this.linkSlots[num2].HashCode == num && this.hcp.Equals(this.keySlots[num2], key))
					{
						return this.valueSlots[num2];
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
				int num = this.hcp.GetHashCode(key) | int.MinValue;
				int num2 = (num & int.MaxValue) % this.table.Length;
				int num3 = this.table[num2] - 1;
				int num4 = -1;
				if (num3 != -1)
				{
					while (this.linkSlots[num3].HashCode != num || !this.hcp.Equals(this.keySlots[num3], key))
					{
						num4 = num3;
						num3 = this.linkSlots[num3].Next;
						if (num3 == -1)
						{
							break;
						}
					}
				}
				if (num3 == -1)
				{
					int num5 = this.count + 1;
					this.count = num5;
					if (num5 > this.threshold)
					{
						this.Resize();
						num2 = (num & int.MaxValue) % this.table.Length;
					}
					num3 = this.emptySlot;
					if (num3 == -1)
					{
						num5 = this.touchedSlots;
						this.touchedSlots = num5 + 1;
						num3 = num5;
					}
					else
					{
						this.emptySlot = this.linkSlots[num3].Next;
					}
					this.linkSlots[num3].Next = this.table[num2] - 1;
					this.table[num2] = num3 + 1;
					this.linkSlots[num3].HashCode = num;
					this.keySlots[num3] = key;
				}
				else if (num4 != -1)
				{
					this.linkSlots[num4].Next = this.linkSlots[num3].Next;
					this.linkSlots[num3].Next = this.table[num2] - 1;
					this.table[num2] = num3 + 1;
				}
				this.valueSlots[num3] = value;
				this.generation++;
			}
		}

		// Token: 0x060024E8 RID: 9448 RVA: 0x00082850 File Offset: 0x00080A50
		public Map()
		{
			this.Init(4, null);
		}

		// Token: 0x060024E9 RID: 9449 RVA: 0x00082860 File Offset: 0x00080A60
		public Map(int count)
		{
			this.Init(count, null);
		}

		// Token: 0x060024EA RID: 9450 RVA: 0x00082870 File Offset: 0x00080A70
		public Map(IEqualityComparer<TKey> comparer)
		{
			this.Init(4, comparer);
		}

		// Token: 0x060024EB RID: 9451 RVA: 0x00082880 File Offset: 0x00080A80
		private void Init(int capacity, IEqualityComparer<TKey> hcp)
		{
			this.hcp = (hcp ?? EqualityComparer<TKey>.Default);
			capacity = Math.Max(1, (int)((float)capacity / 0.9f));
			this.InitArrays(capacity);
		}

		// Token: 0x060024EC RID: 9452 RVA: 0x000828AC File Offset: 0x00080AAC
		private void InitArrays(int size)
		{
			this.table = new int[size];
			this.linkSlots = new Link[size];
			this.emptySlot = -1;
			this.keySlots = new TKey[size];
			this.valueSlots = new TValue[size];
			this.touchedSlots = 0;
			this.threshold = (int)((float)this.table.Length * 0.9f);
			if (this.threshold == 0 && this.table.Length != 0)
			{
				this.threshold = 1;
			}
		}

		// Token: 0x060024ED RID: 9453 RVA: 0x00082928 File Offset: 0x00080B28
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
			if (array.Length - index < this.Count)
			{
				throw new ArgumentException("Destination array cannot hold the requested elements!");
			}
		}

		// Token: 0x060024EE RID: 9454 RVA: 0x00082984 File Offset: 0x00080B84
		private void CopyKeys(TKey[] array, int index)
		{
			for (int i = 0; i < this.touchedSlots; i++)
			{
				if ((this.linkSlots[i].HashCode & -2147483648) != 0)
				{
					array[index++] = this.keySlots[i];
				}
			}
		}

		// Token: 0x060024EF RID: 9455 RVA: 0x000829D4 File Offset: 0x00080BD4
		private void CopyValues(TValue[] array, int index)
		{
			for (int i = 0; i < this.touchedSlots; i++)
			{
				if ((this.linkSlots[i].HashCode & -2147483648) != 0)
				{
					array[index++] = this.valueSlots[i];
				}
			}
		}

		// Token: 0x060024F0 RID: 9456 RVA: 0x00082A23 File Offset: 0x00080C23
		private static KeyValuePair<TKey, TValue> make_pair(TKey key, TValue value)
		{
			return new KeyValuePair<TKey, TValue>(key, value);
		}

		// Token: 0x060024F1 RID: 9457 RVA: 0x00002B18 File Offset: 0x00000D18
		private static TKey pick_key(TKey key, TValue value)
		{
			return key;
		}

		// Token: 0x060024F2 RID: 9458 RVA: 0x00002B1B File Offset: 0x00000D1B
		private static TValue pick_value(TKey key, TValue value)
		{
			return value;
		}

		// Token: 0x060024F3 RID: 9459 RVA: 0x00082A2C File Offset: 0x00080C2C
		private void CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
		{
			this.CopyToCheck(array, index);
			for (int i = 0; i < this.touchedSlots; i++)
			{
				if ((this.linkSlots[i].HashCode & -2147483648) != 0)
				{
					array[index++] = new KeyValuePair<TKey, TValue>(this.keySlots[i], this.valueSlots[i]);
				}
			}
		}

		// Token: 0x060024F4 RID: 9460 RVA: 0x00082A94 File Offset: 0x00080C94
		private void Do_ICollectionCopyTo<TRet>(Array array, int index, Map<TKey, TValue>.Transform<TRet> transform)
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
				for (int i = 0; i < this.touchedSlots; i++)
				{
					if ((this.linkSlots[i].HashCode & -2147483648) != 0)
					{
						array2[index++] = transform(this.keySlots[i], this.valueSlots[i]);
					}
				}
			}
			catch (Exception innerException)
			{
				throw new ArgumentException("Cannot copy source collection elements to destination array", "array", innerException);
			}
		}

		// Token: 0x060024F5 RID: 9461 RVA: 0x00082B5C File Offset: 0x00080D5C
		private void Resize()
		{
			int num = HashPrimeNumbers.ToPrime(this.table.Length << 1 | 1);
			int[] array = new int[num];
			Link[] array2 = new Link[num];
			for (int i = 0; i < this.table.Length; i++)
			{
				for (int num2 = this.table[i] - 1; num2 != -1; num2 = this.linkSlots[num2].Next)
				{
					int num3 = ((array2[num2].HashCode = (this.hcp.GetHashCode(this.keySlots[num2]) | int.MinValue)) & int.MaxValue) % num;
					array2[num2].Next = array[num3] - 1;
					array[num3] = num2 + 1;
				}
			}
			this.table = array;
			this.linkSlots = array2;
			TKey[] destinationArray = new TKey[num];
			TValue[] destinationArray2 = new TValue[num];
			Array.Copy(this.keySlots, 0, destinationArray, 0, this.touchedSlots);
			Array.Copy(this.valueSlots, 0, destinationArray2, 0, this.touchedSlots);
			this.keySlots = destinationArray;
			this.valueSlots = destinationArray2;
			this.threshold = (int)((float)num * 0.9f);
		}

		// Token: 0x060024F6 RID: 9462 RVA: 0x00082C88 File Offset: 0x00080E88
		public void Add(TKey key, TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int num = this.hcp.GetHashCode(key) | int.MinValue;
			int num2 = (num & int.MaxValue) % this.table.Length;
			int num3;
			for (num3 = this.table[num2] - 1; num3 != -1; num3 = this.linkSlots[num3].Next)
			{
				if (this.linkSlots[num3].HashCode == num && this.hcp.Equals(this.keySlots[num3], key))
				{
					throw new ArgumentException("An element with the same key already exists in the dictionary.");
				}
			}
			int num4 = this.count + 1;
			this.count = num4;
			if (num4 > this.threshold)
			{
				this.Resize();
				num2 = (num & int.MaxValue) % this.table.Length;
			}
			num3 = this.emptySlot;
			if (num3 == -1)
			{
				num4 = this.touchedSlots;
				this.touchedSlots = num4 + 1;
				num3 = num4;
			}
			else
			{
				this.emptySlot = this.linkSlots[num3].Next;
			}
			this.linkSlots[num3].HashCode = num;
			this.linkSlots[num3].Next = this.table[num2] - 1;
			this.table[num2] = num3 + 1;
			this.keySlots[num3] = key;
			this.valueSlots[num3] = value;
			this.generation++;
		}

		// Token: 0x060024F7 RID: 9463 RVA: 0x00082DF0 File Offset: 0x00080FF0
		public void Clear()
		{
			if (this.count == 0)
			{
				return;
			}
			this.count = 0;
			Array.Clear(this.table, 0, this.table.Length);
			Array.Clear(this.keySlots, 0, this.keySlots.Length);
			Array.Clear(this.valueSlots, 0, this.valueSlots.Length);
			Array.Clear(this.linkSlots, 0, this.linkSlots.Length);
			this.emptySlot = -1;
			this.touchedSlots = 0;
			this.generation++;
		}

		// Token: 0x060024F8 RID: 9464 RVA: 0x00082E7C File Offset: 0x0008107C
		public bool ContainsKey(TKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int num = this.hcp.GetHashCode(key) | int.MinValue;
			for (int num2 = this.table[(num & int.MaxValue) % this.table.Length] - 1; num2 != -1; num2 = this.linkSlots[num2].Next)
			{
				if (this.linkSlots[num2].HashCode == num && this.hcp.Equals(this.keySlots[num2], key))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060024F9 RID: 9465 RVA: 0x00082F14 File Offset: 0x00081114
		public bool ContainsValue(TValue value)
		{
			IEqualityComparer<TValue> @default = EqualityComparer<TValue>.Default;
			for (int i = 0; i < this.table.Length; i++)
			{
				for (int num = this.table[i] - 1; num != -1; num = this.linkSlots[num].Next)
				{
					if (@default.Equals(this.valueSlots[num], value))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060024FA RID: 9466 RVA: 0x00082F78 File Offset: 0x00081178
		public bool Remove(TKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int num = this.hcp.GetHashCode(key) | int.MinValue;
			int num2 = (num & int.MaxValue) % this.table.Length;
			int num3 = this.table[num2] - 1;
			if (num3 == -1)
			{
				return false;
			}
			int num4 = -1;
			while (this.linkSlots[num3].HashCode != num || !this.hcp.Equals(this.keySlots[num3], key))
			{
				num4 = num3;
				num3 = this.linkSlots[num3].Next;
				if (num3 == -1)
				{
					break;
				}
			}
			if (num3 == -1)
			{
				return false;
			}
			this.count--;
			if (num4 == -1)
			{
				this.table[num2] = this.linkSlots[num3].Next + 1;
			}
			else
			{
				this.linkSlots[num4].Next = this.linkSlots[num3].Next;
			}
			this.linkSlots[num3].Next = this.emptySlot;
			this.emptySlot = num3;
			this.linkSlots[num3].HashCode = 0;
			this.keySlots[num3] = default(TKey);
			this.valueSlots[num3] = default(TValue);
			this.generation++;
			return true;
		}

		// Token: 0x060024FB RID: 9467 RVA: 0x000830D8 File Offset: 0x000812D8
		public bool TryGetValue(TKey key, out TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int num = this.hcp.GetHashCode(key) | int.MinValue;
			for (int num2 = this.table[(num & int.MaxValue) % this.table.Length] - 1; num2 != -1; num2 = this.linkSlots[num2].Next)
			{
				if (this.linkSlots[num2].HashCode == num && this.hcp.Equals(this.keySlots[num2], key))
				{
					value = this.valueSlots[num2];
					return true;
				}
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x060024FC RID: 9468 RVA: 0x00083187 File Offset: 0x00081387
		public Map<TKey, TValue>.KeyCollection Keys
		{
			get
			{
				return new Map<TKey, TValue>.KeyCollection(this);
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x060024FD RID: 9469 RVA: 0x0008318F File Offset: 0x0008138F
		public Map<TKey, TValue>.ValueCollection Values
		{
			get
			{
				return new Map<TKey, TValue>.ValueCollection(this);
			}
		}

		// Token: 0x060024FE RID: 9470 RVA: 0x00083197 File Offset: 0x00081397
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new Map<TKey, TValue>.Enumerator(this);
		}

		// Token: 0x060024FF RID: 9471 RVA: 0x00083197 File Offset: 0x00081397
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<!0, !1>>.GetEnumerator()
		{
			return new Map<TKey, TValue>.Enumerator(this);
		}

		// Token: 0x06002500 RID: 9472 RVA: 0x000831A4 File Offset: 0x000813A4
		public Map<TKey, TValue>.Enumerator GetEnumerator()
		{
			return new Map<TKey, TValue>.Enumerator(this);
		}

		// Token: 0x04000F57 RID: 3927
		private const int INITIAL_SIZE = 4;

		// Token: 0x04000F58 RID: 3928
		private const float DEFAULT_LOAD_FACTOR = 0.9f;

		// Token: 0x04000F59 RID: 3929
		private const int NO_SLOT = -1;

		// Token: 0x04000F5A RID: 3930
		private const int HASH_FLAG = -2147483648;

		// Token: 0x04000F5B RID: 3931
		private int[] table;

		// Token: 0x04000F5C RID: 3932
		private Link[] linkSlots;

		// Token: 0x04000F5D RID: 3933
		private TKey[] keySlots;

		// Token: 0x04000F5E RID: 3934
		private TValue[] valueSlots;

		// Token: 0x04000F5F RID: 3935
		private IEqualityComparer<TKey> hcp;

		// Token: 0x04000F60 RID: 3936
		private int touchedSlots;

		// Token: 0x04000F61 RID: 3937
		private int emptySlot;

		// Token: 0x04000F62 RID: 3938
		private int count;

		// Token: 0x04000F63 RID: 3939
		private int threshold;

		// Token: 0x04000F64 RID: 3940
		private int generation;

		// Token: 0x020006DE RID: 1758
		// (Invoke) Token: 0x0600630B RID: 25355
		private delegate TRet Transform<TRet>(TKey key, TValue value);

		// Token: 0x020006DF RID: 1759
		public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IEnumerator, IDisposable
		{
			// Token: 0x0600630E RID: 25358 RVA: 0x00128F24 File Offset: 0x00127124
			internal Enumerator(Map<TKey, TValue> dictionary)
			{
				this = default(Map<TKey, TValue>.Enumerator);
				this.dictionary = dictionary;
				this.stamp = dictionary.generation;
			}

			// Token: 0x0600630F RID: 25359 RVA: 0x00128F40 File Offset: 0x00127140
			public bool MoveNext()
			{
				this.VerifyState();
				if (this.next < 0)
				{
					return false;
				}
				while (this.next < this.dictionary.touchedSlots)
				{
					int num = this.next;
					this.next = num + 1;
					int num2 = num;
					if ((this.dictionary.linkSlots[num2].HashCode & -2147483648) != 0)
					{
						this.current = new KeyValuePair<TKey, TValue>(this.dictionary.keySlots[num2], this.dictionary.valueSlots[num2]);
						return true;
					}
				}
				this.next = -1;
				return false;
			}

			// Token: 0x170012B5 RID: 4789
			// (get) Token: 0x06006310 RID: 25360 RVA: 0x00128FD8 File Offset: 0x001271D8
			public KeyValuePair<TKey, TValue> Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x170012B6 RID: 4790
			// (get) Token: 0x06006311 RID: 25361 RVA: 0x00128FE0 File Offset: 0x001271E0
			internal TKey CurrentKey
			{
				get
				{
					this.VerifyCurrent();
					return this.current.Key;
				}
			}

			// Token: 0x170012B7 RID: 4791
			// (get) Token: 0x06006312 RID: 25362 RVA: 0x00128FF3 File Offset: 0x001271F3
			internal TValue CurrentValue
			{
				get
				{
					this.VerifyCurrent();
					return this.current.Value;
				}
			}

			// Token: 0x170012B8 RID: 4792
			// (get) Token: 0x06006313 RID: 25363 RVA: 0x00129006 File Offset: 0x00127206
			object IEnumerator.Current
			{
				get
				{
					this.VerifyCurrent();
					return this.current;
				}
			}

			// Token: 0x06006314 RID: 25364 RVA: 0x00129019 File Offset: 0x00127219
			void IEnumerator.Reset()
			{
				this.Reset();
			}

			// Token: 0x06006315 RID: 25365 RVA: 0x00129021 File Offset: 0x00127221
			internal void Reset()
			{
				this.VerifyState();
				this.next = 0;
			}

			// Token: 0x06006316 RID: 25366 RVA: 0x00129030 File Offset: 0x00127230
			private void VerifyState()
			{
				if (this.dictionary == null)
				{
					throw new ObjectDisposedException(null);
				}
				if (this.dictionary.generation != this.stamp)
				{
					throw new InvalidOperationException("out of sync");
				}
			}

			// Token: 0x06006317 RID: 25367 RVA: 0x0012905F File Offset: 0x0012725F
			private void VerifyCurrent()
			{
				this.VerifyState();
				if (this.next <= 0)
				{
					throw new InvalidOperationException("Current is not valid");
				}
			}

			// Token: 0x06006318 RID: 25368 RVA: 0x0012907B File Offset: 0x0012727B
			public void Dispose()
			{
				this.dictionary = null;
			}

			// Token: 0x04002267 RID: 8807
			private Map<TKey, TValue> dictionary;

			// Token: 0x04002268 RID: 8808
			private int next;

			// Token: 0x04002269 RID: 8809
			private int stamp;

			// Token: 0x0400226A RID: 8810
			internal KeyValuePair<TKey, TValue> current;
		}

		// Token: 0x020006E0 RID: 1760
		public sealed class KeyCollection : ICollection<!0>, IEnumerable<!0>, IEnumerable, ICollection
		{
			// Token: 0x06006319 RID: 25369 RVA: 0x00129084 File Offset: 0x00127284
			public KeyCollection(Map<TKey, TValue> dictionary)
			{
				if (dictionary == null)
				{
					throw new ArgumentNullException("dictionary");
				}
				this.dictionary = dictionary;
			}

			// Token: 0x0600631A RID: 25370 RVA: 0x001290A1 File Offset: 0x001272A1
			public void CopyTo(TKey[] array, int index)
			{
				this.dictionary.CopyToCheck(array, index);
				this.dictionary.CopyKeys(array, index);
			}

			// Token: 0x0600631B RID: 25371 RVA: 0x001290BD File Offset: 0x001272BD
			public Map<TKey, TValue>.KeyCollection.Enumerator GetEnumerator()
			{
				return new Map<TKey, TValue>.KeyCollection.Enumerator(this.dictionary);
			}

			// Token: 0x0600631C RID: 25372 RVA: 0x00126512 File Offset: 0x00124712
			void ICollection<!0>.Add(TKey item)
			{
				throw new NotSupportedException("this is a read-only collection");
			}

			// Token: 0x0600631D RID: 25373 RVA: 0x00126512 File Offset: 0x00124712
			void ICollection<!0>.Clear()
			{
				throw new NotSupportedException("this is a read-only collection");
			}

			// Token: 0x0600631E RID: 25374 RVA: 0x001290CA File Offset: 0x001272CA
			bool ICollection<!0>.Contains(TKey item)
			{
				return this.dictionary.ContainsKey(item);
			}

			// Token: 0x0600631F RID: 25375 RVA: 0x00126512 File Offset: 0x00124712
			bool ICollection<!0>.Remove(TKey item)
			{
				throw new NotSupportedException("this is a read-only collection");
			}

			// Token: 0x06006320 RID: 25376 RVA: 0x001290D8 File Offset: 0x001272D8
			IEnumerator<TKey> IEnumerable<!0>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06006321 RID: 25377 RVA: 0x001290E8 File Offset: 0x001272E8
			void ICollection.CopyTo(Array array, int index)
			{
				TKey[] array2 = array as TKey[];
				if (array2 != null)
				{
					this.CopyTo(array2, index);
					return;
				}
				this.dictionary.CopyToCheck(array, index);
				this.dictionary.Do_ICollectionCopyTo<TKey>(array, index, new Map<TKey, TValue>.Transform<TKey>(Map<TKey, TValue>.pick_key));
			}

			// Token: 0x06006322 RID: 25378 RVA: 0x001290D8 File Offset: 0x001272D8
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x170012B9 RID: 4793
			// (get) Token: 0x06006323 RID: 25379 RVA: 0x0012912E File Offset: 0x0012732E
			public int Count
			{
				get
				{
					return this.dictionary.Count;
				}
			}

			// Token: 0x170012BA RID: 4794
			// (get) Token: 0x06006324 RID: 25380 RVA: 0x00003D6E File Offset: 0x00001F6E
			bool ICollection<!0>.IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170012BB RID: 4795
			// (get) Token: 0x06006325 RID: 25381 RVA: 0x00003D71 File Offset: 0x00001F71
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170012BC RID: 4796
			// (get) Token: 0x06006326 RID: 25382 RVA: 0x0012913B File Offset: 0x0012733B
			object ICollection.SyncRoot
			{
				get
				{
					return ((ICollection)this.dictionary).SyncRoot;
				}
			}

			// Token: 0x0400226B RID: 8811
			private Map<TKey, TValue> dictionary;

			// Token: 0x0200070B RID: 1803
			public struct Enumerator : IEnumerator<TKey>, IEnumerator, IDisposable
			{
				// Token: 0x06006368 RID: 25448 RVA: 0x00129578 File Offset: 0x00127778
				internal Enumerator(Map<TKey, TValue> host)
				{
					this.host_enumerator = host.GetEnumerator();
				}

				// Token: 0x06006369 RID: 25449 RVA: 0x00129586 File Offset: 0x00127786
				public void Dispose()
				{
					this.host_enumerator.Dispose();
				}

				// Token: 0x0600636A RID: 25450 RVA: 0x00129593 File Offset: 0x00127793
				public bool MoveNext()
				{
					return this.host_enumerator.MoveNext();
				}

				// Token: 0x170012C7 RID: 4807
				// (get) Token: 0x0600636B RID: 25451 RVA: 0x001295A0 File Offset: 0x001277A0
				public TKey Current
				{
					get
					{
						return this.host_enumerator.current.Key;
					}
				}

				// Token: 0x170012C8 RID: 4808
				// (get) Token: 0x0600636C RID: 25452 RVA: 0x001295B2 File Offset: 0x001277B2
				object IEnumerator.Current
				{
					get
					{
						return this.host_enumerator.CurrentKey;
					}
				}

				// Token: 0x0600636D RID: 25453 RVA: 0x001295C4 File Offset: 0x001277C4
				void IEnumerator.Reset()
				{
					this.host_enumerator.Reset();
				}

				// Token: 0x040022E6 RID: 8934
				private Map<TKey, TValue>.Enumerator host_enumerator;
			}
		}

		// Token: 0x020006E1 RID: 1761
		public sealed class ValueCollection : ICollection<!1>, IEnumerable<!1>, IEnumerable, ICollection
		{
			// Token: 0x06006327 RID: 25383 RVA: 0x0012914D File Offset: 0x0012734D
			public ValueCollection(Map<TKey, TValue> dictionary)
			{
				if (dictionary == null)
				{
					throw new ArgumentNullException("dictionary");
				}
				this.dictionary = dictionary;
			}

			// Token: 0x06006328 RID: 25384 RVA: 0x0012916A File Offset: 0x0012736A
			public void CopyTo(TValue[] array, int index)
			{
				this.dictionary.CopyToCheck(array, index);
				this.dictionary.CopyValues(array, index);
			}

			// Token: 0x06006329 RID: 25385 RVA: 0x00129186 File Offset: 0x00127386
			public Map<TKey, TValue>.ValueCollection.Enumerator GetEnumerator()
			{
				return new Map<TKey, TValue>.ValueCollection.Enumerator(this.dictionary);
			}

			// Token: 0x0600632A RID: 25386 RVA: 0x00126512 File Offset: 0x00124712
			void ICollection<!1>.Add(TValue item)
			{
				throw new NotSupportedException("this is a read-only collection");
			}

			// Token: 0x0600632B RID: 25387 RVA: 0x00126512 File Offset: 0x00124712
			void ICollection<!1>.Clear()
			{
				throw new NotSupportedException("this is a read-only collection");
			}

			// Token: 0x0600632C RID: 25388 RVA: 0x00129193 File Offset: 0x00127393
			bool ICollection<!1>.Contains(TValue item)
			{
				return this.dictionary.ContainsValue(item);
			}

			// Token: 0x0600632D RID: 25389 RVA: 0x00126512 File Offset: 0x00124712
			bool ICollection<!1>.Remove(TValue item)
			{
				throw new NotSupportedException("this is a read-only collection");
			}

			// Token: 0x0600632E RID: 25390 RVA: 0x001291A1 File Offset: 0x001273A1
			IEnumerator<TValue> IEnumerable<!1>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0600632F RID: 25391 RVA: 0x001291B0 File Offset: 0x001273B0
			void ICollection.CopyTo(Array array, int index)
			{
				TValue[] array2 = array as TValue[];
				if (array2 != null)
				{
					this.CopyTo(array2, index);
					return;
				}
				this.dictionary.CopyToCheck(array, index);
				this.dictionary.Do_ICollectionCopyTo<TValue>(array, index, new Map<TKey, TValue>.Transform<TValue>(Map<TKey, TValue>.pick_value));
			}

			// Token: 0x06006330 RID: 25392 RVA: 0x001291A1 File Offset: 0x001273A1
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x170012BD RID: 4797
			// (get) Token: 0x06006331 RID: 25393 RVA: 0x001291F6 File Offset: 0x001273F6
			public int Count
			{
				get
				{
					return this.dictionary.Count;
				}
			}

			// Token: 0x170012BE RID: 4798
			// (get) Token: 0x06006332 RID: 25394 RVA: 0x00003D6E File Offset: 0x00001F6E
			bool ICollection<!1>.IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170012BF RID: 4799
			// (get) Token: 0x06006333 RID: 25395 RVA: 0x00003D71 File Offset: 0x00001F71
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170012C0 RID: 4800
			// (get) Token: 0x06006334 RID: 25396 RVA: 0x00129203 File Offset: 0x00127403
			object ICollection.SyncRoot
			{
				get
				{
					return ((ICollection)this.dictionary).SyncRoot;
				}
			}

			// Token: 0x0400226C RID: 8812
			private Map<TKey, TValue> dictionary;

			// Token: 0x0200070C RID: 1804
			public struct Enumerator : IEnumerator<TValue>, IEnumerator, IDisposable
			{
				// Token: 0x0600636E RID: 25454 RVA: 0x001295D1 File Offset: 0x001277D1
				internal Enumerator(Map<TKey, TValue> host)
				{
					this.host_enumerator = host.GetEnumerator();
				}

				// Token: 0x0600636F RID: 25455 RVA: 0x001295DF File Offset: 0x001277DF
				public void Dispose()
				{
					this.host_enumerator.Dispose();
				}

				// Token: 0x06006370 RID: 25456 RVA: 0x001295EC File Offset: 0x001277EC
				public bool MoveNext()
				{
					return this.host_enumerator.MoveNext();
				}

				// Token: 0x170012C9 RID: 4809
				// (get) Token: 0x06006371 RID: 25457 RVA: 0x001295F9 File Offset: 0x001277F9
				public TValue Current
				{
					get
					{
						return this.host_enumerator.current.Value;
					}
				}

				// Token: 0x170012CA RID: 4810
				// (get) Token: 0x06006372 RID: 25458 RVA: 0x0012960B File Offset: 0x0012780B
				object IEnumerator.Current
				{
					get
					{
						return this.host_enumerator.CurrentValue;
					}
				}

				// Token: 0x06006373 RID: 25459 RVA: 0x0012961D File Offset: 0x0012781D
				void IEnumerator.Reset()
				{
					this.host_enumerator.Reset();
				}

				// Token: 0x040022E7 RID: 8935
				private Map<TKey, TValue>.Enumerator host_enumerator;
			}
		}
	}
}
