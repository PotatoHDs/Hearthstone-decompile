using System;
using UnityEngine;

// Token: 0x020000B6 RID: 182
public abstract class BookPageDisplay : MonoBehaviour
{
	// Token: 0x17000097 RID: 151
	// (get) Token: 0x06000B4C RID: 2892 RVA: 0x0004282A File Offset: 0x00040A2A
	// (set) Token: 0x06000B4D RID: 2893 RVA: 0x00042832 File Offset: 0x00040A32
	private protected bool IsShown { protected get; private set; }

	// Token: 0x06000B4E RID: 2894
	public abstract bool IsLoaded();

	// Token: 0x06000B4F RID: 2895 RVA: 0x0004283B File Offset: 0x00040A3B
	public virtual void Show()
	{
		this.IsShown = true;
	}

	// Token: 0x06000B50 RID: 2896 RVA: 0x00042844 File Offset: 0x00040A44
	public virtual void Hide()
	{
		this.IsShown = false;
	}

	// Token: 0x04000794 RID: 1940
	public MeshRenderer m_basePageRenderer;
}
