using System;
using System.Collections.Generic;

[Serializable]
public class DictionaryList<T, U> : List<DictionaryListItem<T, U>>
{
	public U this[T key]
	{
		get
		{
			if (!TryGetValue(key, out var value))
			{
				throw new KeyNotFoundException($"{key} key does not exist in ListDict.");
			}
			return value;
		}
		set
		{
			using (Enumerator enumerator = GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					DictionaryListItem<T, U> current = enumerator.Current;
					if (current.m_key.Equals(key))
					{
						current.m_value = value;
						return;
					}
				}
			}
			Add(new DictionaryListItem<T, U>
			{
				m_key = key,
				m_value = value
			});
		}
	}

	public bool TryGetValue(T key, out U value)
	{
		using (Enumerator enumerator = GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				DictionaryListItem<T, U> current = enumerator.Current;
				if (current.m_key.Equals(key))
				{
					value = current.m_value;
					return true;
				}
			}
		}
		value = default(U);
		return false;
	}
}
