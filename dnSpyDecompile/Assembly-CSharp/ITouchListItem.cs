using System;
using UnityEngine;

// Token: 0x02000B19 RID: 2841
public interface ITouchListItem
{
	// Token: 0x17000890 RID: 2192
	// (get) Token: 0x0600970E RID: 38670
	Bounds LocalBounds { get; }

	// Token: 0x17000891 RID: 2193
	// (get) Token: 0x0600970F RID: 38671
	bool IsHeader { get; }

	// Token: 0x17000892 RID: 2194
	// (get) Token: 0x06009710 RID: 38672
	// (set) Token: 0x06009711 RID: 38673
	bool Visible { get; set; }

	// Token: 0x17000893 RID: 2195
	// (get) Token: 0x06009712 RID: 38674
	GameObject gameObject { get; }

	// Token: 0x17000894 RID: 2196
	// (get) Token: 0x06009713 RID: 38675
	Transform transform { get; }

	// Token: 0x06009714 RID: 38676
	T GetComponent<T>() where T : Component;

	// Token: 0x06009715 RID: 38677
	void OnScrollOutOfView();
}
