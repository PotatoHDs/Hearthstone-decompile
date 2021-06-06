using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A44 RID: 2628
public class iTweenManager : MonoBehaviour
{
	// Token: 0x06008E26 RID: 36390 RVA: 0x002DDE49 File Offset: 0x002DC049
	public static iTweenManager Get()
	{
		if (iTweenManager.s_quitting)
		{
			return null;
		}
		if (iTweenManager.s_instance == null)
		{
			iTweenManager.s_instance = new GameObject
			{
				name = "iTweenManager"
			}.AddComponent<iTweenManager>();
		}
		return iTweenManager.s_instance;
	}

	// Token: 0x06008E27 RID: 36391 RVA: 0x002DDE80 File Offset: 0x002DC080
	public static void Add(iTween tween)
	{
		iTweenManager iTweenManager = iTweenManager.Get();
		if (iTweenManager != null)
		{
			iTweenManager.AddImpl(tween);
		}
	}

	// Token: 0x06008E28 RID: 36392 RVA: 0x002DDEA3 File Offset: 0x002DC0A3
	private void AddImpl(iTween tween)
	{
		this.m_tweenCollection.Add(tween);
		tween.Awake();
	}

	// Token: 0x06008E29 RID: 36393 RVA: 0x002DDEB8 File Offset: 0x002DC0B8
	public static void Remove(iTween tween)
	{
		iTweenManager iTweenManager = iTweenManager.Get();
		if (iTweenManager != null)
		{
			iTweenManager.RemoveImpl(tween);
		}
	}

	// Token: 0x06008E2A RID: 36394 RVA: 0x002DDEDB File Offset: 0x002DC0DB
	private void RemoveImpl(iTween tween)
	{
		this.m_tweenCollection.Remove(tween);
		tween.destroyed = true;
	}

	// Token: 0x06008E2B RID: 36395 RVA: 0x002DDEF0 File Offset: 0x002DC0F0
	public void OnApplicationQuit()
	{
		iTweenManager.s_instance = null;
		iTweenManager.s_quitting = true;
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06008E2C RID: 36396 RVA: 0x002DDF09 File Offset: 0x002DC109
	public void OnDestroy()
	{
		if (iTweenManager.s_instance == this)
		{
			iTweenManager.s_instance = null;
		}
	}

	// Token: 0x06008E2D RID: 36397 RVA: 0x002DDF20 File Offset: 0x002DC120
	public void Update()
	{
		iTween next;
		while ((next = this.m_tweenCollection.GetIterator().GetNext()) != null)
		{
			next.Upkeep();
			next.Update();
		}
		this.m_tweenCollection.CleanUp();
	}

	// Token: 0x06008E2E RID: 36398 RVA: 0x002DDF60 File Offset: 0x002DC160
	public void LateUpdate()
	{
		iTween next;
		while ((next = this.m_tweenCollection.GetIterator().GetNext()) != null)
		{
			next.Upkeep();
			next.LateUpdate();
		}
		this.m_tweenCollection.CleanUp();
	}

	// Token: 0x06008E2F RID: 36399 RVA: 0x002DDFA0 File Offset: 0x002DC1A0
	public void FixedUpdate()
	{
		iTween next;
		while ((next = this.m_tweenCollection.GetIterator().GetNext()) != null)
		{
			next.Upkeep();
			next.FixedUpdate();
		}
		this.m_tweenCollection.CleanUp();
	}

	// Token: 0x06008E30 RID: 36400 RVA: 0x002DDFE0 File Offset: 0x002DC1E0
	public iTween GetTweenForObject(GameObject obj)
	{
		iTween next;
		while ((next = this.m_tweenCollection.GetIterator().GetNext()) != null)
		{
			if (next.gameObject == obj)
			{
				return next;
			}
		}
		return null;
	}

	// Token: 0x06008E31 RID: 36401 RVA: 0x002DE018 File Offset: 0x002DC218
	public static iTween[] GetTweensForObject(GameObject obj)
	{
		iTweenManager iTweenManager = iTweenManager.Get();
		if (iTweenManager != null)
		{
			return iTweenManager.GetTweensForObjectImpl(obj);
		}
		return new iTween[0];
	}

	// Token: 0x06008E32 RID: 36402 RVA: 0x002DE044 File Offset: 0x002DC244
	private iTween[] GetTweensForObjectImpl(GameObject obj)
	{
		List<iTween> list = new List<iTween>();
		iTween next;
		while ((next = this.m_tweenCollection.GetIterator().GetNext()) != null)
		{
			if (next.gameObject == obj)
			{
				list.Add(next);
			}
		}
		return list.ToArray();
	}

	// Token: 0x06008E33 RID: 36403 RVA: 0x002DE08C File Offset: 0x002DC28C
	public static iTweenIterator GetIterator()
	{
		iTweenManager iTweenManager = iTweenManager.Get();
		if (iTweenManager == null)
		{
			return new iTweenIterator(null);
		}
		return iTweenManager.GetIteratorImpl();
	}

	// Token: 0x06008E34 RID: 36404 RVA: 0x002DE0B5 File Offset: 0x002DC2B5
	private iTweenIterator GetIteratorImpl()
	{
		return this.m_tweenCollection.GetIterator();
	}

	// Token: 0x06008E35 RID: 36405 RVA: 0x002DE0C4 File Offset: 0x002DC2C4
	public static int GetTweenCount()
	{
		iTweenManager iTweenManager = iTweenManager.Get();
		if (iTweenManager == null)
		{
			return 0;
		}
		return iTweenManager.GetTweenCountImpl();
	}

	// Token: 0x06008E36 RID: 36406 RVA: 0x002DE0E8 File Offset: 0x002DC2E8
	private int GetTweenCountImpl()
	{
		return this.m_tweenCollection.Count;
	}

	// Token: 0x06008E37 RID: 36407 RVA: 0x002DE0F8 File Offset: 0x002DC2F8
	public static void ForEach(iTweenManager.TweenOperation op, GameObject go = null, string name = null, string type = null, bool includeChildren = false)
	{
		iTweenManager iTweenManager = iTweenManager.Get();
		if (iTweenManager != null)
		{
			iTweenManager.ForEachImpl(op, go, name, type, includeChildren);
		}
	}

	// Token: 0x06008E38 RID: 36408 RVA: 0x002DE120 File Offset: 0x002DC320
	public static void ForEachByGameObject(iTweenManager.TweenOperation op, GameObject go)
	{
		iTweenManager.ForEach(op, go, null, null, false);
	}

	// Token: 0x06008E39 RID: 36409 RVA: 0x002DE12C File Offset: 0x002DC32C
	public static void ForEachByType(iTweenManager.TweenOperation op, string type)
	{
		iTweenManager.ForEach(op, null, null, type, false);
	}

	// Token: 0x06008E3A RID: 36410 RVA: 0x002DE138 File Offset: 0x002DC338
	public static void ForEachByName(iTweenManager.TweenOperation op, string name)
	{
		iTweenManager.ForEach(op, null, name, null, false);
	}

	// Token: 0x06008E3B RID: 36411 RVA: 0x002DE144 File Offset: 0x002DC344
	private void ForEachImpl(iTweenManager.TweenOperation op, GameObject go = null, string name = null, string type = null, bool includeChildren = false)
	{
		iTween next;
		while ((next = this.m_tweenCollection.GetIterator().GetNext()) != null)
		{
			if ((!(go != null) || !(next.gameObject != go)) && (name == null || name.Equals(next._name)) && (type == null || (next.type + next.method).Substring(0, type.Length).ToLower().Equals(type.ToLower())))
			{
				op(next);
				if (go != null && includeChildren)
				{
					foreach (object obj in go.transform)
					{
						Transform transform = (Transform)obj;
						iTweenManager.ForEach(op, transform.gameObject, name, type, true);
					}
				}
			}
		}
	}

	// Token: 0x06008E3C RID: 36412 RVA: 0x002DE240 File Offset: 0x002DC440
	public static void ForEachInverted(iTweenManager.TweenOperation op, GameObject go, string name, string type, bool includeChildren = false)
	{
		iTweenManager iTweenManager = iTweenManager.Get();
		if (iTweenManager != null)
		{
			iTweenManager.ForEachInvertedImpl(op, go, name, type, includeChildren);
		}
	}

	// Token: 0x06008E3D RID: 36413 RVA: 0x002DE268 File Offset: 0x002DC468
	private void ForEachInvertedImpl(iTweenManager.TweenOperation op, GameObject go, string name, string type, bool includeChildren = false)
	{
		iTween next;
		while ((next = this.m_tweenCollection.GetIterator().GetNext()) != null)
		{
			if ((!(go != null) || !(next.gameObject != go)) && (name == null || !name.Equals(next._name)) && (type == null || !(next.type + next.method).Substring(0, type.Length).ToLower().Equals(type.ToLower())))
			{
				op(next);
				if (go != null && includeChildren)
				{
					foreach (object obj in go.transform)
					{
						Transform transform = (Transform)obj;
						iTweenManager.ForEachInverted(op, transform.gameObject, name, type, true);
					}
				}
			}
		}
	}

	// Token: 0x04007639 RID: 30265
	private static iTweenManager s_instance;

	// Token: 0x0400763A RID: 30266
	private static bool s_quitting;

	// Token: 0x0400763B RID: 30267
	private iTweenCollection m_tweenCollection = new iTweenCollection();

	// Token: 0x020026B4 RID: 9908
	private class iTweenEntry
	{
		// Token: 0x0400F1C4 RID: 61892
		private GameObject gameObject;

		// Token: 0x0400F1C5 RID: 61893
		private iTween iTween;

		// Token: 0x0400F1C6 RID: 61894
		private Hashtable args;
	}

	// Token: 0x020026B5 RID: 9909
	// (Invoke) Token: 0x0601381A RID: 79898
	public delegate void TweenOperation(iTween tween);
}
