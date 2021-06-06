using System.Collections.Generic;

public class Pool<T>
{
	public delegate T CreateItemCallback(int freeListIndex);

	public delegate void DestroyItemCallback(T item);

	public const int DEFAULT_EXTENSION_COUNT = 5;

	public const int DEFAULT_MAX_RELEASED_ITEM_COUNT = 5;

	private List<T> m_freeList = new List<T>();

	private List<T> m_activeList = new List<T>();

	private int m_extensionCount = 5;

	private int m_maxReleasedItemCount = 5;

	private CreateItemCallback m_createItemCallback;

	private DestroyItemCallback m_destroyItemCallback;

	public int GetExtensionCount()
	{
		return m_extensionCount;
	}

	public void SetExtensionCount(int count)
	{
		m_extensionCount = count;
	}

	public int GetMaxReleasedItemCount()
	{
		return m_maxReleasedItemCount;
	}

	public void SetMaxReleasedItemCount(int count)
	{
		m_maxReleasedItemCount = count;
	}

	public CreateItemCallback GetCreateItemCallback()
	{
		return m_createItemCallback;
	}

	public void SetCreateItemCallback(CreateItemCallback callback)
	{
		m_createItemCallback = callback;
	}

	public DestroyItemCallback GetDestroyItemCallback()
	{
		return m_destroyItemCallback;
	}

	public void SetDestroyItemCallback(DestroyItemCallback callback)
	{
		m_destroyItemCallback = callback;
	}

	public T Acquire()
	{
		if (m_freeList.Count == 0)
		{
			if (m_extensionCount == 0)
			{
				return default(T);
			}
			if (!AddFreeItems(m_extensionCount))
			{
				return default(T);
			}
		}
		int index = m_freeList.Count - 1;
		T val = m_freeList[index];
		m_freeList.RemoveAt(index);
		m_activeList.Add(val);
		return val;
	}

	public List<T> AcquireBatch(int count)
	{
		List<T> list = new List<T>();
		for (int i = 0; i < count; i++)
		{
			T item = Acquire();
			list.Add(item);
		}
		return list;
	}

	public bool Release(T item)
	{
		if (!m_activeList.Remove(item))
		{
			return false;
		}
		if (m_freeList.Count < m_maxReleasedItemCount)
		{
			m_freeList.Add(item);
			return true;
		}
		if (m_destroyItemCallback != null)
		{
			return false;
		}
		m_destroyItemCallback(item);
		return true;
	}

	public bool ReleaseBatch(int activeListStart, int count)
	{
		if (count <= 0)
		{
			return true;
		}
		if (activeListStart >= m_activeList.Count)
		{
			return false;
		}
		int num = m_activeList.Count - activeListStart;
		if (count > num)
		{
			count = num;
		}
		int num2 = count;
		int num3 = m_maxReleasedItemCount - m_freeList.Count;
		if (num2 > num3)
		{
			num2 = num3;
		}
		if (num2 > 0)
		{
			List<T> range = m_activeList.GetRange(activeListStart, num2);
			m_activeList.RemoveRange(activeListStart, num2);
			m_freeList.AddRange(range);
		}
		int num4 = count - num2;
		if (num4 > 0)
		{
			if (m_destroyItemCallback == null)
			{
				return false;
			}
			for (int i = 0; i < num4; i++)
			{
				T item = m_activeList[activeListStart];
				m_activeList.RemoveAt(activeListStart);
				m_destroyItemCallback(item);
			}
		}
		return true;
	}

	public bool ReleaseAll()
	{
		return ReleaseBatch(0, m_activeList.Count);
	}

	public bool AddFreeItems(int count)
	{
		if (m_createItemCallback == null)
		{
			return false;
		}
		for (int i = 0; i < count; i++)
		{
			int freeListIndex = m_activeList.Count + m_freeList.Count + 1;
			T item = m_createItemCallback(freeListIndex);
			m_freeList.Add(item);
		}
		return true;
	}

	public void Clear()
	{
		if (m_destroyItemCallback == null)
		{
			m_activeList.Clear();
			m_freeList.Clear();
			return;
		}
		for (int i = 0; i < m_activeList.Count; i++)
		{
			m_destroyItemCallback(m_activeList[i]);
		}
		m_activeList.Clear();
		for (int j = 0; j < m_freeList.Count; j++)
		{
			m_destroyItemCallback(m_freeList[j]);
		}
		m_freeList.Clear();
	}

	public List<T> GetFreeList()
	{
		return m_freeList;
	}

	public List<T> GetActiveList()
	{
		return m_activeList;
	}
}
