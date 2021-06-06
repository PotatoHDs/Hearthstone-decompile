using System.Collections;
using System.Collections.Generic;

public class QueueList<T> : IEnumerable<T>, IEnumerable
{
	protected List<T> m_list = new List<T>();

	public int Count => m_list.Count;

	public T this[int index]
	{
		get
		{
			return m_list[index];
		}
		set
		{
			m_list[index] = value;
		}
	}

	public int Enqueue(T item)
	{
		int count = m_list.Count;
		m_list.Add(item);
		return count;
	}

	public T Dequeue()
	{
		T result = m_list[0];
		m_list.RemoveAt(0);
		return result;
	}

	public T Peek()
	{
		return m_list[0];
	}

	public int GetCount()
	{
		return m_list.Count;
	}

	public T GetItem(int index)
	{
		return m_list[index];
	}

	public void Clear()
	{
		m_list.Clear();
	}

	public T RemoveAt(int position)
	{
		if (m_list.Count <= position)
		{
			return default(T);
		}
		T result = m_list[position];
		m_list.RemoveAt(position);
		return result;
	}

	public bool Remove(T item)
	{
		return m_list.Remove(item);
	}

	public List<T> GetList()
	{
		return m_list;
	}

	public bool Contains(T item)
	{
		return m_list.Contains(item);
	}

	public IEnumerator<T> GetEnumerator()
	{
		return Enumerate().GetEnumerator();
	}

	public override string ToString()
	{
		return $"Count={Count}";
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	protected IEnumerable<T> Enumerate()
	{
		int i = 0;
		while (i < m_list.Count)
		{
			yield return m_list[i];
			int num = i + 1;
			i = num;
		}
	}
}
