using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iTweenManager : MonoBehaviour
{
	private class iTweenEntry
	{
		private GameObject gameObject;

		private iTween iTween;

		private Hashtable args;
	}

	public delegate void TweenOperation(iTween tween);

	private static iTweenManager s_instance;

	private static bool s_quitting;

	private iTweenCollection m_tweenCollection = new iTweenCollection();

	public static iTweenManager Get()
	{
		if (s_quitting)
		{
			return null;
		}
		if (s_instance == null)
		{
			s_instance = new GameObject
			{
				name = "iTweenManager"
			}.AddComponent<iTweenManager>();
		}
		return s_instance;
	}

	public static void Add(iTween tween)
	{
		iTweenManager iTweenManager2 = Get();
		if (iTweenManager2 != null)
		{
			iTweenManager2.AddImpl(tween);
		}
	}

	private void AddImpl(iTween tween)
	{
		m_tweenCollection.Add(tween);
		tween.Awake();
	}

	public static void Remove(iTween tween)
	{
		iTweenManager iTweenManager2 = Get();
		if (iTweenManager2 != null)
		{
			iTweenManager2.RemoveImpl(tween);
		}
	}

	private void RemoveImpl(iTween tween)
	{
		m_tweenCollection.Remove(tween);
		tween.destroyed = true;
	}

	public void OnApplicationQuit()
	{
		s_instance = null;
		s_quitting = true;
		Object.Destroy(base.gameObject);
	}

	public void OnDestroy()
	{
		if (s_instance == this)
		{
			s_instance = null;
		}
	}

	public void Update()
	{
		iTweenIterator iterator = m_tweenCollection.GetIterator();
		iTween next;
		while ((next = iterator.GetNext()) != null)
		{
			next.Upkeep();
			next.Update();
		}
		m_tweenCollection.CleanUp();
	}

	public void LateUpdate()
	{
		iTweenIterator iterator = m_tweenCollection.GetIterator();
		iTween next;
		while ((next = iterator.GetNext()) != null)
		{
			next.Upkeep();
			next.LateUpdate();
		}
		m_tweenCollection.CleanUp();
	}

	public void FixedUpdate()
	{
		iTweenIterator iterator = m_tweenCollection.GetIterator();
		iTween next;
		while ((next = iterator.GetNext()) != null)
		{
			next.Upkeep();
			next.FixedUpdate();
		}
		m_tweenCollection.CleanUp();
	}

	public iTween GetTweenForObject(GameObject obj)
	{
		iTweenIterator iterator = m_tweenCollection.GetIterator();
		iTween next;
		while ((next = iterator.GetNext()) != null)
		{
			if (next.gameObject == obj)
			{
				return next;
			}
		}
		return null;
	}

	public static iTween[] GetTweensForObject(GameObject obj)
	{
		iTweenManager iTweenManager2 = Get();
		if (iTweenManager2 != null)
		{
			return iTweenManager2.GetTweensForObjectImpl(obj);
		}
		return new iTween[0];
	}

	private iTween[] GetTweensForObjectImpl(GameObject obj)
	{
		List<iTween> list = new List<iTween>();
		iTweenIterator iterator = m_tweenCollection.GetIterator();
		iTween next;
		while ((next = iterator.GetNext()) != null)
		{
			if (next.gameObject == obj)
			{
				list.Add(next);
			}
		}
		return list.ToArray();
	}

	public static iTweenIterator GetIterator()
	{
		iTweenManager iTweenManager2 = Get();
		if (iTweenManager2 == null)
		{
			return new iTweenIterator(null);
		}
		return iTweenManager2.GetIteratorImpl();
	}

	private iTweenIterator GetIteratorImpl()
	{
		return m_tweenCollection.GetIterator();
	}

	public static int GetTweenCount()
	{
		iTweenManager iTweenManager2 = Get();
		if (iTweenManager2 == null)
		{
			return 0;
		}
		return iTweenManager2.GetTweenCountImpl();
	}

	private int GetTweenCountImpl()
	{
		return m_tweenCollection.Count;
	}

	public static void ForEach(TweenOperation op, GameObject go = null, string name = null, string type = null, bool includeChildren = false)
	{
		iTweenManager iTweenManager2 = Get();
		if (iTweenManager2 != null)
		{
			iTweenManager2.ForEachImpl(op, go, name, type, includeChildren);
		}
	}

	public static void ForEachByGameObject(TweenOperation op, GameObject go)
	{
		ForEach(op, go);
	}

	public static void ForEachByType(TweenOperation op, string type)
	{
		ForEach(op, null, null, type);
	}

	public static void ForEachByName(TweenOperation op, string name)
	{
		ForEach(op, null, name);
	}

	private void ForEachImpl(TweenOperation op, GameObject go = null, string name = null, string type = null, bool includeChildren = false)
	{
		iTweenIterator iterator = m_tweenCollection.GetIterator();
		iTween next;
		while ((next = iterator.GetNext()) != null)
		{
			if ((go != null && next.gameObject != go) || (name != null && !name.Equals(next._name)) || (type != null && !(next.type + next.method).Substring(0, type.Length).ToLower().Equals(type.ToLower())))
			{
				continue;
			}
			op(next);
			if (!(go != null && includeChildren))
			{
				continue;
			}
			foreach (Transform item in go.transform)
			{
				ForEach(op, item.gameObject, name, type, includeChildren: true);
			}
		}
	}

	public static void ForEachInverted(TweenOperation op, GameObject go, string name, string type, bool includeChildren = false)
	{
		iTweenManager iTweenManager2 = Get();
		if (iTweenManager2 != null)
		{
			iTweenManager2.ForEachInvertedImpl(op, go, name, type, includeChildren);
		}
	}

	private void ForEachInvertedImpl(TweenOperation op, GameObject go, string name, string type, bool includeChildren = false)
	{
		iTweenIterator iterator = m_tweenCollection.GetIterator();
		iTween next;
		while ((next = iterator.GetNext()) != null)
		{
			if ((go != null && next.gameObject != go) || (name != null && name.Equals(next._name)) || (type != null && (next.type + next.method).Substring(0, type.Length).ToLower().Equals(type.ToLower())))
			{
				continue;
			}
			op(next);
			if (!(go != null && includeChildren))
			{
				continue;
			}
			foreach (Transform item in go.transform)
			{
				ForEachInverted(op, item.gameObject, name, type, includeChildren: true);
			}
		}
	}
}
