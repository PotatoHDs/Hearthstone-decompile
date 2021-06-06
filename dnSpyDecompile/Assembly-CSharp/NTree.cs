using System;
using System.Collections.Generic;

// Token: 0x020009CD RID: 2509
public class NTree<T>
{
	// Token: 0x060088C8 RID: 35016 RVA: 0x002C097F File Offset: 0x002BEB7F
	public NTree(T data)
	{
		this.data = data;
		this.children = new LinkedList<NTree<T>>();
	}

	// Token: 0x060088C9 RID: 35017 RVA: 0x002C099C File Offset: 0x002BEB9C
	public void AddDeepChild(params T[] traverse)
	{
		LinkedList<NTree<T>> linkedList = this.children;
		foreach (T y in traverse)
		{
			bool flag = false;
			foreach (NTree<T> ntree in linkedList)
			{
				if (EqualityComparer<T>.Default.Equals(ntree.data, y))
				{
					linkedList = ntree.children;
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				NTree<T> ntree2 = new NTree<T>(y);
				linkedList.AddLast(ntree2);
				linkedList = ntree2.children;
			}
		}
	}

	// Token: 0x060088CA RID: 35018 RVA: 0x002C0A48 File Offset: 0x002BEC48
	public void SetData(T data)
	{
		this.data = data;
	}

	// Token: 0x060088CB RID: 35019 RVA: 0x002C0A51 File Offset: 0x002BEC51
	public void Traverse(TreeVisitor<T> visitor, TreePreTraverse previsitor, TreePostTraverse postvisitor, int ignoredepth = -1)
	{
		this.traverse(this, visitor, previsitor, postvisitor, ignoredepth);
	}

	// Token: 0x060088CC RID: 35020 RVA: 0x002C0A60 File Offset: 0x002BEC60
	private void traverse(NTree<T> node, TreeVisitor<T> visitor, TreePreTraverse previsitor, TreePostTraverse postvisitor, int ignoredepth)
	{
		if (visitor != null && ignoredepth < 0 && !visitor(node.data))
		{
			return;
		}
		foreach (NTree<T> node2 in node.children)
		{
			if (previsitor != null && ignoredepth < 0)
			{
				previsitor();
			}
			this.traverse(node2, visitor, previsitor, postvisitor, ignoredepth - 1);
			if (postvisitor != null && ignoredepth < 0)
			{
				postvisitor();
			}
		}
	}

	// Token: 0x040072D7 RID: 29399
	private T data;

	// Token: 0x040072D8 RID: 29400
	private LinkedList<NTree<T>> children;
}
