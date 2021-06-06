using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020009E1 RID: 2529
public class QueueList<T> : IEnumerable<T>, IEnumerable
{
	// Token: 0x06008937 RID: 35127 RVA: 0x002C1CAC File Offset: 0x002BFEAC
	public int Enqueue(T item)
	{
		int count = this.m_list.Count;
		this.m_list.Add(item);
		return count;
	}

	// Token: 0x06008938 RID: 35128 RVA: 0x002C1CC5 File Offset: 0x002BFEC5
	public T Dequeue()
	{
		T result = this.m_list[0];
		this.m_list.RemoveAt(0);
		return result;
	}

	// Token: 0x06008939 RID: 35129 RVA: 0x002C1CDF File Offset: 0x002BFEDF
	public T Peek()
	{
		return this.m_list[0];
	}

	// Token: 0x0600893A RID: 35130 RVA: 0x002C1CED File Offset: 0x002BFEED
	public int GetCount()
	{
		return this.m_list.Count;
	}

	// Token: 0x170007B9 RID: 1977
	// (get) Token: 0x0600893B RID: 35131 RVA: 0x002C1CED File Offset: 0x002BFEED
	public int Count
	{
		get
		{
			return this.m_list.Count;
		}
	}

	// Token: 0x0600893C RID: 35132 RVA: 0x002C1CFA File Offset: 0x002BFEFA
	public T GetItem(int index)
	{
		return this.m_list[index];
	}

	// Token: 0x170007BA RID: 1978
	public T this[int index]
	{
		get
		{
			return this.m_list[index];
		}
		set
		{
			this.m_list[index] = value;
		}
	}

	// Token: 0x0600893F RID: 35135 RVA: 0x002C1D17 File Offset: 0x002BFF17
	public void Clear()
	{
		this.m_list.Clear();
	}

	// Token: 0x06008940 RID: 35136 RVA: 0x002C1D24 File Offset: 0x002BFF24
	public T RemoveAt(int position)
	{
		if (this.m_list.Count <= position)
		{
			return default(T);
		}
		T result = this.m_list[position];
		this.m_list.RemoveAt(position);
		return result;
	}

	// Token: 0x06008941 RID: 35137 RVA: 0x002C1D61 File Offset: 0x002BFF61
	public bool Remove(T item)
	{
		return this.m_list.Remove(item);
	}

	// Token: 0x06008942 RID: 35138 RVA: 0x002C1D6F File Offset: 0x002BFF6F
	public List<T> GetList()
	{
		return this.m_list;
	}

	// Token: 0x06008943 RID: 35139 RVA: 0x002C1D77 File Offset: 0x002BFF77
	public bool Contains(T item)
	{
		return this.m_list.Contains(item);
	}

	// Token: 0x06008944 RID: 35140 RVA: 0x002C1D85 File Offset: 0x002BFF85
	public IEnumerator<T> GetEnumerator()
	{
		return this.Enumerate().GetEnumerator();
	}

	// Token: 0x06008945 RID: 35141 RVA: 0x002C1D92 File Offset: 0x002BFF92
	public override string ToString()
	{
		return string.Format("Count={0}", this.Count);
	}

	// Token: 0x06008946 RID: 35142 RVA: 0x002C1DA9 File Offset: 0x002BFFA9
	IEnumerator IEnumerable.GetEnumerator()
	{
		return this.GetEnumerator();
	}

	// Token: 0x06008947 RID: 35143 RVA: 0x002C1DB1 File Offset: 0x002BFFB1
	protected IEnumerable<T> Enumerate()
	{
		int num;
		for (int i = 0; i < this.m_list.Count; i = num)
		{
			yield return this.m_list[i];
			num = i + 1;
		}
		yield break;
	}

	// Token: 0x04007339 RID: 29497
	protected List<T> m_list = new List<T>();
}
