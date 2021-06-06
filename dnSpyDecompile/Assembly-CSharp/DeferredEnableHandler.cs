using System;
using UnityEngine;

// Token: 0x020009B1 RID: 2481
public class DeferredEnableHandler : MonoBehaviour
{
	// Token: 0x14000090 RID: 144
	// (add) Token: 0x0600870A RID: 34570 RVA: 0x002B9BB4 File Offset: 0x002B7DB4
	// (remove) Token: 0x0600870B RID: 34571 RVA: 0x002B9BEC File Offset: 0x002B7DEC
	private event Action m_listener;

	// Token: 0x0600870C RID: 34572 RVA: 0x002B9C21 File Offset: 0x002B7E21
	public static void AttachTo(Component comp, Action callback)
	{
		if (comp == null)
		{
			return;
		}
		DeferredEnableHandler.AttachTo(comp.gameObject, callback);
	}

	// Token: 0x0600870D RID: 34573 RVA: 0x002B9C39 File Offset: 0x002B7E39
	public static void AttachTo(GameObject go, Action callback)
	{
		if (go == null)
		{
			return;
		}
		(go.GetComponent<DeferredEnableHandler>() ?? go.AddComponent<DeferredEnableHandler>()).SetEnableListener(callback);
	}

	// Token: 0x0600870E RID: 34574 RVA: 0x002B9C5B File Offset: 0x002B7E5B
	private void SetEnableListener(Action callback)
	{
		this.m_listener = callback;
	}

	// Token: 0x0600870F RID: 34575 RVA: 0x002B9C64 File Offset: 0x002B7E64
	private void OnEnable()
	{
		if (this.m_listener != null)
		{
			this.m_listener();
		}
		UnityEngine.Object.Destroy(this);
	}
}
