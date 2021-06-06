using System;
using UnityEngine;

public class DeferredEnableHandler : MonoBehaviour
{
	private event Action m_listener;

	public static void AttachTo(Component comp, Action callback)
	{
		if (!(comp == null))
		{
			AttachTo(comp.gameObject, callback);
		}
	}

	public static void AttachTo(GameObject go, Action callback)
	{
		if (!(go == null))
		{
			(go.GetComponent<DeferredEnableHandler>() ?? go.AddComponent<DeferredEnableHandler>()).SetEnableListener(callback);
		}
	}

	private void SetEnableListener(Action callback)
	{
		this.m_listener = callback;
	}

	private void OnEnable()
	{
		if (this.m_listener != null)
		{
			this.m_listener();
		}
		UnityEngine.Object.Destroy(this);
	}
}
