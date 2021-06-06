using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000007 RID: 7
public class Map<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
{
	// Token: 0x17000003 RID: 3
	// (get) Token: 0x0600002F RID: 47 RVA: 0x0000265C File Offset: 0x0000085C
	public int Count
	{
		get
		{
			return this.count;
		}
	}

	// Token: 0x17000004 RID: 4
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

	// Token: 0x06000032 RID: 50 RVA: 0x000028C8 File Offset: 0x00000AC8
	public Map()
	{
		this.Init(4, null);
	}

	// Token: 0x06000033 RID: 51 RVA: 0x000028D8 File Offset: 0x00000AD8
	public Map(int count)
	{
		this.Init(count, null);
	}

	// Token: 0x06000034 RID: 52 RVA: 0x000028E8 File Offset: 0x00000AE8
	public Map(IEqualityComparer<TKey> comparer)
	{
		this.Init(4, comparer);
	}

	// Token: 0x06000035 RID: 53 RVA: 0x000028F8 File Offset: 0x00000AF8
	public Map(int count, IEqualityComparer<TKey> comparer)
	{
		this.Init(count, comparer);
	}

	// Token: 0x06000036 RID: 54 RVA: 0x00002908 File Offset: 0x00000B08
	public Map(IEnumerable<KeyValuePair<TKey, TValue>> copy)
	{
		this.Init(4, null);
		foreach (KeyValuePair<TKey, TValue> keyValuePair in copy)
		{
			this[keyValuePair.Key] = keyValuePair.Value;
		}
	}

	// Token: 0x06000037 RID: 55 RVA: 0x0000296C File Offset: 0x00000B6C
	private void Init(int capacity, IEqualityComparer<TKey> hcp)
	{
		this.hcp = (hcp ?? EqualityComparer<TKey>.Default);
		capacity = Math.Max(1, (int)((float)capacity / 0.9f));
		this.InitArrays(capacity);
	}

	// Token: 0x06000038 RID: 56 RVA: 0x00002998 File Offset: 0x00000B98
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

	// Token: 0x06000039 RID: 57 RVA: 0x00002A14 File Offset: 0x00000C14
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

	// Token: 0x0600003A RID: 58 RVA: 0x00002A70 File Offset: 0x00000C70
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

	// Token: 0x0600003B RID: 59 RVA: 0x00002AC0 File Offset: 0x00000CC0
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

	// Token: 0x0600003C RID: 60 RVA: 0x00002B0F File Offset: 0x00000D0F
	private static KeyValuePair<TKey, TValue> make_pair(TKey key, TValue value)
	{
		return new KeyValuePair<TKey, TValue>(key, value);
	}

	// Token: 0x0600003D RID: 61 RVA: 0x00002B18 File Offset: 0x00000D18
	private static TKey pick_key(TKey key, TValue value)
	{
		return key;
	}

	// Token: 0x0600003E RID: 62 RVA: 0x00002B1B File Offset: 0x00000D1B
	private static TValue pick_value(TKey key, TValue value)
	{
		return value;
	}

	// Token: 0x0600003F RID: 63 RVA: 0x00002B20 File Offset: 0x00000D20
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

	// Token: 0x06000040 RID: 64 RVA: 0x00002B88 File Offset: 0x00000D88
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

	// Token: 0x06000041 RID: 65 RVA: 0x00002C50 File Offset: 0x00000E50
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

	// Token: 0x06000042 RID: 66 RVA: 0x00002D7C File Offset: 0x00000F7C
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
				throw new ArgumentException(string.Format("An element with the same key ({0}) already exists in the dictionary.", key));
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

	// Token: 0x06000043 RID: 67 RVA: 0x00002EF0 File Offset: 0x000010F0
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

	// Token: 0x06000044 RID: 68 RVA: 0x00002F7C File Offset: 0x0000117C
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

	// Token: 0x06000045 RID: 69 RVA: 0x00003014 File Offset: 0x00001214
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

	// Token: 0x06000046 RID: 70 RVA: 0x00003078 File Offset: 0x00001278
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

	// Token: 0x06000047 RID: 71 RVA: 0x000031D8 File Offset: 0x000013D8
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

	// Token: 0x17000005 RID: 5
	// (get) Token: 0x06000048 RID: 72 RVA: 0x00003287 File Offset: 0x00001487
	public Map<TKey, TValue>.KeyCollection Keys
	{
		get
		{
			return new Map<TKey, TValue>.KeyCollection(this);
		}
	}

	// Token: 0x17000006 RID: 6
	// (get) Token: 0x06000049 RID: 73 RVA: 0x0000328F File Offset: 0x0000148F
	public Map<TKey, TValue>.ValueCollection Values
	{
		get
		{
			return new Map<TKey, TValue>.ValueCollection(this);
		}
	}

	// Token: 0x0600004A RID: 74 RVA: 0x00003297 File Offset: 0x00001497
	IEnumerator IEnumerable.GetEnumerator()
	{
		return new Map<TKey, TValue>.Enumerator(this);
	}

	// Token: 0x0600004B RID: 75 RVA: 0x00003297 File Offset: 0x00001497
	IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<!0, !1>>.GetEnumerator()
	{
		return new Map<TKey, TValue>.Enumerator(this);
	}

	// Token: 0x0600004C RID: 76 RVA: 0x000032A4 File Offset: 0x000014A4
	public Map<TKey, TValue>.Enumerator GetEnumerator()
	{
		return new Map<TKey, TValue>.Enumerator(this);
	}

	// Token: 0x04000011 RID: 17
	private const int INITIAL_SIZE = 4;

	// Token: 0x04000012 RID: 18
	private const float DEFAULT_LOAD_FACTOR = 0.9f;

	// Token: 0x04000013 RID: 19
	private const int NO_SLOT = -1;

	// Token: 0x04000014 RID: 20
	private const int HASH_FLAG = -2147483648;

	// Token: 0x04000015 RID: 21
	private int[] table;

	// Token: 0x04000016 RID: 22
	private Link[] linkSlots;

	// Token: 0x04000017 RID: 23
	private TKey[] keySlots;

	// Token: 0x04000018 RID: 24
	private TValue[] valueSlots;

	// Token: 0x04000019 RID: 25
	private IEqualityComparer<TKey> hcp;

	// Token: 0x0400001A RID: 26
	private int touchedSlots;

	// Token: 0x0400001B RID: 27
	private int emptySlot;

	// Token: 0x0400001C RID: 28
	private int count;

	// Token: 0x0400001D RID: 29
	private int threshold;

	// Token: 0x0400001E RID: 30
	private int generation;

	// Token: 0x02000549 RID: 1353
	// (Invoke) Token: 0x0600614C RID: 24908
	private delegate TRet Transform<TRet>(TKey key, TValue value);

	// Token: 0x0200054A RID: 1354
	public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IEnumerator, IDisposable
	{
		// Token: 0x0600614F RID: 24911 RVA: 0x0012636C File Offset: 0x0012456C
		internal Enumerator(Map<TKey, TValue> dictionary)
		{
			this = default(Map<TKey, TValue>.Enumerator);
			this.dictionary = dictionary;
			this.stamp = dictionary.generation;
		}

		// Token: 0x06006150 RID: 24912 RVA: 0x00126388 File Offset: 0x00124588
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

		// Token: 0x17001271 RID: 4721
		// (get) Token: 0x06006151 RID: 24913 RVA: 0x00126420 File Offset: 0x00124620
		public KeyValuePair<TKey, TValue> Current
		{
			get
			{
				return this.current;
			}
		}

		// Token: 0x17001272 RID: 4722
		// (get) Token: 0x06006152 RID: 24914 RVA: 0x00126428 File Offset: 0x00124628
		internal TKey CurrentKey
		{
			get
			{
				this.VerifyCurrent();
				return this.current.Key;
			}
		}

		// Token: 0x17001273 RID: 4723
		// (get) Token: 0x06006153 RID: 24915 RVA: 0x0012643B File Offset: 0x0012463B
		internal TValue CurrentValue
		{
			get
			{
				this.VerifyCurrent();
				return this.current.Value;
			}
		}

		// Token: 0x17001274 RID: 4724
		// (get) Token: 0x06006154 RID: 24916 RVA: 0x0012644E File Offset: 0x0012464E
		object IEnumerator.Current
		{
			get
			{
				this.VerifyCurrent();
				return this.current;
			}
		}

		// Token: 0x06006155 RID: 24917 RVA: 0x00126461 File Offset: 0x00124661
		void IEnumerator.Reset()
		{
			this.Reset();
		}

		// Token: 0x06006156 RID: 24918 RVA: 0x00126469 File Offset: 0x00124669
		internal void Reset()
		{
			this.VerifyState();
			this.next = 0;
		}

		// Token: 0x06006157 RID: 24919 RVA: 0x00126478 File Offset: 0x00124678
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

		// Token: 0x06006158 RID: 24920 RVA: 0x001264A7 File Offset: 0x001246A7
		private void VerifyCurrent()
		{
			this.VerifyState();
			if (this.next <= 0)
			{
				throw new InvalidOperationException("Current is not valid");
			}
		}

		// Token: 0x06006159 RID: 24921 RVA: 0x001264C3 File Offset: 0x001246C3
		public void Dispose()
		{
			this.dictionary = null;
		}

		// Token: 0x04001E02 RID: 7682
		private Map<TKey, TValue> dictionary;

		// Token: 0x04001E03 RID: 7683
		private int next;

		// Token: 0x04001E04 RID: 7684
		private int stamp;

		// Token: 0x04001E05 RID: 7685
		internal KeyValuePair<TKey, TValue> current;
	}

	// Token: 0x0200054B RID: 1355
	public sealed class KeyCollection : ICollection<TKey>, IEnumerable<TKey>, IEnumerable, ICollection
	{
		// Token: 0x0600615A RID: 24922 RVA: 0x001264CC File Offset: 0x001246CC
		public KeyCollection(Map<TKey, TValue> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this.dictionary = dictionary;
		}

		// Token: 0x0600615B RID: 24923 RVA: 0x001264E9 File Offset: 0x001246E9
		public void CopyTo(TKey[] array, int index)
		{
			this.dictionary.CopyToCheck(array, index);
			this.dictionary.CopyKeys(array, index);
		}

		// Token: 0x0600615C RID: 24924 RVA: 0x00126505 File Offset: 0x00124705
		public Map<TKey, TValue>.KeyCollection.Enumerator GetEnumerator()
		{
			return new Map<TKey, TValue>.KeyCollection.Enumerator(this.dictionary);
		}

		// Token: 0x0600615D RID: 24925 RVA: 0x00126512 File Offset: 0x00124712
		void ICollection<!0>.Add(TKey item)
		{
			throw new NotSupportedException("this is a read-only collection");
		}

		// Token: 0x0600615E RID: 24926 RVA: 0x00126512 File Offset: 0x00124712
		void ICollection<!0>.Clear()
		{
			throw new NotSupportedException("this is a read-only collection");
		}

		// Token: 0x0600615F RID: 24927 RVA: 0x0012651E File Offset: 0x0012471E
		bool ICollection<!0>.Contains(TKey item)
		{
			return this.dictionary.ContainsKey(item);
		}

		// Token: 0x06006160 RID: 24928 RVA: 0x00126512 File Offset: 0x00124712
		bool ICollection<!0>.Remove(TKey item)
		{
			throw new NotSupportedException("this is a read-only collection");
		}

		// Token: 0x06006161 RID: 24929 RVA: 0x0012652C File Offset: 0x0012472C
		IEnumerator<TKey> IEnumerable<!0>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06006162 RID: 24930 RVA: 0x0012653C File Offset: 0x0012473C
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

		// Token: 0x06006163 RID: 24931 RVA: 0x0012652C File Offset: 0x0012472C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17001275 RID: 4725
		// (get) Token: 0x06006164 RID: 24932 RVA: 0x00126582 File Offset: 0x00124782
		public int Count
		{
			get
			{
				return this.dictionary.Count;
			}
		}

		// Token: 0x17001276 RID: 4726
		// (get) Token: 0x06006165 RID: 24933 RVA: 0x00003D6E File Offset: 0x00001F6E
		bool ICollection<!0>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001277 RID: 4727
		// (get) Token: 0x06006166 RID: 24934 RVA: 0x00003D71 File Offset: 0x00001F71
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001278 RID: 4728
		// (get) Token: 0x06006167 RID: 24935 RVA: 0x0012658F File Offset: 0x0012478F
		object ICollection.SyncRoot
		{
			get
			{
				return ((ICollection)this.dictionary).SyncRoot;
			}
		}

		// Token: 0x04001E06 RID: 7686
		private Map<TKey, TValue> dictionary;

		// Token: 0x02000707 RID: 1799
		public struct Enumerator : IEnumerator<TKey>, IEnumerator, IDisposable
		{
			// Token: 0x0600634D RID: 25421 RVA: 0x0012927A File Offset: 0x0012747A
			internal Enumerator(Map<TKey, TValue> host)
			{
				this.host_enumerator = host.GetEnumerator();
			}

			// Token: 0x0600634E RID: 25422 RVA: 0x00129288 File Offset: 0x00127488
			public void Dispose()
			{
				this.host_enumerator.Dispose();
			}

			// Token: 0x0600634F RID: 25423 RVA: 0x00129295 File Offset: 0x00127495
			public bool MoveNext()
			{
				return this.host_enumerator.MoveNext();
			}

			// Token: 0x170012C1 RID: 4801
			// (get) Token: 0x06006350 RID: 25424 RVA: 0x001292A2 File Offset: 0x001274A2
			public TKey Current
			{
				get
				{
					return this.host_enumerator.current.Key;
				}
			}

			// Token: 0x170012C2 RID: 4802
			// (get) Token: 0x06006351 RID: 25425 RVA: 0x001292B4 File Offset: 0x001274B4
			object IEnumerator.Current
			{
				get
				{
					return this.host_enumerator.CurrentKey;
				}
			}

			// Token: 0x06006352 RID: 25426 RVA: 0x001292C6 File Offset: 0x001274C6
			void IEnumerator.Reset()
			{
				this.host_enumerator.Reset();
			}

			// Token: 0x040022D5 RID: 8917
			private Map<TKey, TValue>.Enumerator host_enumerator;
		}
	}

	// Token: 0x0200054C RID: 1356
	public sealed class ValueCollection : ICollection<TValue>, IEnumerable<TValue>, IEnumerable, ICollection
	{
		// Token: 0x06006168 RID: 24936 RVA: 0x001265A1 File Offset: 0x001247A1
		public ValueCollection(Map<TKey, TValue> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this.dictionary = dictionary;
		}

		// Token: 0x06006169 RID: 24937 RVA: 0x001265BE File Offset: 0x001247BE
		public void CopyTo(TValue[] array, int index)
		{
			this.dictionary.CopyToCheck(array, index);
			this.dictionary.CopyValues(array, index);
		}

		// Token: 0x0600616A RID: 24938 RVA: 0x001265DA File Offset: 0x001247DA
		public Map<TKey, TValue>.ValueCollection.Enumerator GetEnumerator()
		{
			return new Map<TKey, TValue>.ValueCollection.Enumerator(this.dictionary);
		}

		// Token: 0x0600616B RID: 24939 RVA: 0x00126512 File Offset: 0x00124712
		void ICollection<!1>.Add(TValue item)
		{
			throw new NotSupportedException("this is a read-only collection");
		}

		// Token: 0x0600616C RID: 24940 RVA: 0x00126512 File Offset: 0x00124712
		void ICollection<!1>.Clear()
		{
			throw new NotSupportedException("this is a read-only collection");
		}

		// Token: 0x0600616D RID: 24941 RVA: 0x001265E7 File Offset: 0x001247E7
		bool ICollection<!1>.Contains(TValue item)
		{
			return this.dictionary.ContainsValue(item);
		}

		// Token: 0x0600616E RID: 24942 RVA: 0x00126512 File Offset: 0x00124712
		bool ICollection<!1>.Remove(TValue item)
		{
			throw new NotSupportedException("this is a read-only collection");
		}

		// Token: 0x0600616F RID: 24943 RVA: 0x001265F5 File Offset: 0x001247F5
		IEnumerator<TValue> IEnumerable<!1>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06006170 RID: 24944 RVA: 0x00126604 File Offset: 0x00124804
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

		// Token: 0x06006171 RID: 24945 RVA: 0x001265F5 File Offset: 0x001247F5
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17001279 RID: 4729
		// (get) Token: 0x06006172 RID: 24946 RVA: 0x0012664A File Offset: 0x0012484A
		public int Count
		{
			get
			{
				return this.dictionary.Count;
			}
		}

		// Token: 0x1700127A RID: 4730
		// (get) Token: 0x06006173 RID: 24947 RVA: 0x00003D6E File Offset: 0x00001F6E
		bool ICollection<!1>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700127B RID: 4731
		// (get) Token: 0x06006174 RID: 24948 RVA: 0x00003D71 File Offset: 0x00001F71
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700127C RID: 4732
		// (get) Token: 0x06006175 RID: 24949 RVA: 0x00126657 File Offset: 0x00124857
		object ICollection.SyncRoot
		{
			get
			{
				return ((ICollection)this.dictionary).SyncRoot;
			}
		}

		// Token: 0x04001E07 RID: 7687
		private Map<TKey, TValue> dictionary;

		// Token: 0x02000708 RID: 1800
		public struct Enumerator : IEnumerator<TValue>, IEnumerator, IDisposable
		{
			// Token: 0x06006353 RID: 25427 RVA: 0x001292D3 File Offset: 0x001274D3
			internal Enumerator(Map<TKey, TValue> host)
			{
				this.host_enumerator = host.GetEnumerator();
			}

			// Token: 0x06006354 RID: 25428 RVA: 0x001292E1 File Offset: 0x001274E1
			public void Dispose()
			{
				this.host_enumerator.Dispose();
			}

			// Token: 0x06006355 RID: 25429 RVA: 0x001292EE File Offset: 0x001274EE
			public bool MoveNext()
			{
				return this.host_enumerator.MoveNext();
			}

			// Token: 0x170012C3 RID: 4803
			// (get) Token: 0x06006356 RID: 25430 RVA: 0x001292FB File Offset: 0x001274FB
			public TValue Current
			{
				get
				{
					return this.host_enumerator.current.Value;
				}
			}

			// Token: 0x170012C4 RID: 4804
			// (get) Token: 0x06006357 RID: 25431 RVA: 0x0012930D File Offset: 0x0012750D
			object IEnumerator.Current
			{
				get
				{
					return this.host_enumerator.CurrentValue;
				}
			}

			// Token: 0x06006358 RID: 25432 RVA: 0x0012931F File Offset: 0x0012751F
			void IEnumerator.Reset()
			{
				this.host_enumerator.Reset();
			}

			// Token: 0x040022D6 RID: 8918
			private Map<TKey, TValue>.Enumerator host_enumerator;
		}
	}
}
