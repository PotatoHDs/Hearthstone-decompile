using System;
using System.Collections.Generic;

// Token: 0x020009B4 RID: 2484
[Serializable]
public class DictionaryList<T, U> : List<DictionaryListItem<T, U>>
{
	// Token: 0x06008714 RID: 34580 RVA: 0x002B9D38 File Offset: 0x002B7F38
	public bool TryGetValue(T key, out U value)
	{
		foreach (DictionaryListItem<T, U> dictionaryListItem in this)
		{
			if (dictionaryListItem.m_key.Equals(key))
			{
				value = dictionaryListItem.m_value;
				return true;
			}
		}
		value = default(U);
		return false;
	}

	// Token: 0x1700078E RID: 1934
	public U this[T key]
	{
		get
		{
			U result;
			if (!this.TryGetValue(key, out result))
			{
				throw new KeyNotFoundException(string.Format("{0} key does not exist in ListDict.", key));
			}
			return result;
		}
		set
		{
			foreach (DictionaryListItem<T, U> dictionaryListItem in this)
			{
				if (dictionaryListItem.m_key.Equals(key))
				{
					dictionaryListItem.m_value = value;
					return;
				}
			}
			base.Add(new DictionaryListItem<T, U>
			{
				m_key = key,
				m_value = value
			});
		}
	}
}
