using System.Collections.Generic;

public class NTree<T>
{
	private T data;

	private LinkedList<NTree<T>> children;

	public NTree(T data)
	{
		this.data = data;
		children = new LinkedList<NTree<T>>();
	}

	public void AddDeepChild(params T[] traverse)
	{
		LinkedList<NTree<T>> linkedList = children;
		foreach (T y in traverse)
		{
			bool flag = false;
			foreach (NTree<T> item in linkedList)
			{
				if (EqualityComparer<T>.Default.Equals(item.data, y))
				{
					linkedList = item.children;
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				NTree<T> nTree = new NTree<T>(y);
				linkedList.AddLast(nTree);
				linkedList = nTree.children;
			}
		}
	}

	public void SetData(T data)
	{
		this.data = data;
	}

	public void Traverse(TreeVisitor<T> visitor, TreePreTraverse previsitor, TreePostTraverse postvisitor, int ignoredepth = -1)
	{
		traverse(this, visitor, previsitor, postvisitor, ignoredepth);
	}

	private void traverse(NTree<T> node, TreeVisitor<T> visitor, TreePreTraverse previsitor, TreePostTraverse postvisitor, int ignoredepth)
	{
		if (visitor != null && ignoredepth < 0 && !visitor(node.data))
		{
			return;
		}
		foreach (NTree<T> child in node.children)
		{
			if (previsitor != null && ignoredepth < 0)
			{
				previsitor();
			}
			traverse(child, visitor, previsitor, postvisitor, ignoredepth - 1);
			if (postvisitor != null && ignoredepth < 0)
			{
				postvisitor();
			}
		}
	}
}
