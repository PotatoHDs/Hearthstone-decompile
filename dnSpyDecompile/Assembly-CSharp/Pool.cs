using System;
using System.Collections.Generic;

// Token: 0x020009E0 RID: 2528
public class Pool<T>
{
	// Token: 0x06008926 RID: 35110 RVA: 0x002C1983 File Offset: 0x002BFB83
	public int GetExtensionCount()
	{
		return this.m_extensionCount;
	}

	// Token: 0x06008927 RID: 35111 RVA: 0x002C198B File Offset: 0x002BFB8B
	public void SetExtensionCount(int count)
	{
		this.m_extensionCount = count;
	}

	// Token: 0x06008928 RID: 35112 RVA: 0x002C1994 File Offset: 0x002BFB94
	public int GetMaxReleasedItemCount()
	{
		return this.m_maxReleasedItemCount;
	}

	// Token: 0x06008929 RID: 35113 RVA: 0x002C199C File Offset: 0x002BFB9C
	public void SetMaxReleasedItemCount(int count)
	{
		this.m_maxReleasedItemCount = count;
	}

	// Token: 0x0600892A RID: 35114 RVA: 0x002C19A5 File Offset: 0x002BFBA5
	public Pool<T>.CreateItemCallback GetCreateItemCallback()
	{
		return this.m_createItemCallback;
	}

	// Token: 0x0600892B RID: 35115 RVA: 0x002C19AD File Offset: 0x002BFBAD
	public void SetCreateItemCallback(Pool<T>.CreateItemCallback callback)
	{
		this.m_createItemCallback = callback;
	}

	// Token: 0x0600892C RID: 35116 RVA: 0x002C19B6 File Offset: 0x002BFBB6
	public Pool<T>.DestroyItemCallback GetDestroyItemCallback()
	{
		return this.m_destroyItemCallback;
	}

	// Token: 0x0600892D RID: 35117 RVA: 0x002C19BE File Offset: 0x002BFBBE
	public void SetDestroyItemCallback(Pool<T>.DestroyItemCallback callback)
	{
		this.m_destroyItemCallback = callback;
	}

	// Token: 0x0600892E RID: 35118 RVA: 0x002C19C8 File Offset: 0x002BFBC8
	public T Acquire()
	{
		if (this.m_freeList.Count == 0)
		{
			if (this.m_extensionCount == 0)
			{
				return default(T);
			}
			if (!this.AddFreeItems(this.m_extensionCount))
			{
				return default(T);
			}
		}
		int index = this.m_freeList.Count - 1;
		T t = this.m_freeList[index];
		this.m_freeList.RemoveAt(index);
		this.m_activeList.Add(t);
		return t;
	}

	// Token: 0x0600892F RID: 35119 RVA: 0x002C1A40 File Offset: 0x002BFC40
	public List<T> AcquireBatch(int count)
	{
		List<T> list = new List<T>();
		for (int i = 0; i < count; i++)
		{
			T item = this.Acquire();
			list.Add(item);
		}
		return list;
	}

	// Token: 0x06008930 RID: 35120 RVA: 0x002C1A70 File Offset: 0x002BFC70
	public bool Release(T item)
	{
		if (!this.m_activeList.Remove(item))
		{
			return false;
		}
		if (this.m_freeList.Count < this.m_maxReleasedItemCount)
		{
			this.m_freeList.Add(item);
			return true;
		}
		if (this.m_destroyItemCallback != null)
		{
			return false;
		}
		this.m_destroyItemCallback(item);
		return true;
	}

	// Token: 0x06008931 RID: 35121 RVA: 0x002C1AC8 File Offset: 0x002BFCC8
	public bool ReleaseBatch(int activeListStart, int count)
	{
		if (count <= 0)
		{
			return true;
		}
		if (activeListStart >= this.m_activeList.Count)
		{
			return false;
		}
		int num = this.m_activeList.Count - activeListStart;
		if (count > num)
		{
			count = num;
		}
		int num2 = count;
		int num3 = this.m_maxReleasedItemCount - this.m_freeList.Count;
		if (num2 > num3)
		{
			num2 = num3;
		}
		if (num2 > 0)
		{
			List<T> range = this.m_activeList.GetRange(activeListStart, num2);
			this.m_activeList.RemoveRange(activeListStart, num2);
			this.m_freeList.AddRange(range);
		}
		int num4 = count - num2;
		if (num4 > 0)
		{
			if (this.m_destroyItemCallback == null)
			{
				return false;
			}
			for (int i = 0; i < num4; i++)
			{
				T item = this.m_activeList[activeListStart];
				this.m_activeList.RemoveAt(activeListStart);
				this.m_destroyItemCallback(item);
			}
		}
		return true;
	}

	// Token: 0x06008932 RID: 35122 RVA: 0x002C1B92 File Offset: 0x002BFD92
	public bool ReleaseAll()
	{
		return this.ReleaseBatch(0, this.m_activeList.Count);
	}

	// Token: 0x06008933 RID: 35123 RVA: 0x002C1BA8 File Offset: 0x002BFDA8
	public bool AddFreeItems(int count)
	{
		if (this.m_createItemCallback == null)
		{
			return false;
		}
		for (int i = 0; i < count; i++)
		{
			int freeListIndex = this.m_activeList.Count + this.m_freeList.Count + 1;
			T item = this.m_createItemCallback(freeListIndex);
			this.m_freeList.Add(item);
		}
		return true;
	}

	// Token: 0x06008934 RID: 35124 RVA: 0x002C1C00 File Offset: 0x002BFE00
	public void Clear()
	{
		if (this.m_destroyItemCallback == null)
		{
			this.m_activeList.Clear();
			this.m_freeList.Clear();
			return;
		}
		for (int i = 0; i < this.m_activeList.Count; i++)
		{
			this.m_destroyItemCallback(this.m_activeList[i]);
		}
		this.m_activeList.Clear();
		for (int j = 0; j < this.m_freeList.Count; j++)
		{
			this.m_destroyItemCallback(this.m_freeList[j]);
		}
		this.m_freeList.Clear();
	}

	// Token: 0x06008935 RID: 35125 RVA: 0x002C1C9C File Offset: 0x002BFE9C
	public List<T> GetFreeList()
	{
		return this.m_freeList;
	}

	// Token: 0x06008936 RID: 35126 RVA: 0x002C1CA4 File Offset: 0x002BFEA4
	public List<T> GetActiveList()
	{
		return this.m_activeList;
	}

	// Token: 0x04007331 RID: 29489
	public const int DEFAULT_EXTENSION_COUNT = 5;

	// Token: 0x04007332 RID: 29490
	public const int DEFAULT_MAX_RELEASED_ITEM_COUNT = 5;

	// Token: 0x04007333 RID: 29491
	private List<T> m_freeList = new List<T>();

	// Token: 0x04007334 RID: 29492
	private List<T> m_activeList = new List<T>();

	// Token: 0x04007335 RID: 29493
	private int m_extensionCount = 5;

	// Token: 0x04007336 RID: 29494
	private int m_maxReleasedItemCount = 5;

	// Token: 0x04007337 RID: 29495
	private Pool<T>.CreateItemCallback m_createItemCallback;

	// Token: 0x04007338 RID: 29496
	private Pool<T>.DestroyItemCallback m_destroyItemCallback;

	// Token: 0x0200267E RID: 9854
	// (Invoke) Token: 0x0601374E RID: 79694
	public delegate T CreateItemCallback(int freeListIndex);

	// Token: 0x0200267F RID: 9855
	// (Invoke) Token: 0x06013752 RID: 79698
	public delegate void DestroyItemCallback(T item);
}
