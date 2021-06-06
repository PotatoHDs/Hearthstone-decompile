using System;
using UnityEngine;

// Token: 0x02000AFF RID: 2815
public abstract class CarouselItem
{
	// Token: 0x060095DC RID: 38364
	public abstract void Show(Carousel parent);

	// Token: 0x060095DD RID: 38365
	public abstract void Hide();

	// Token: 0x060095DE RID: 38366
	public abstract void Clear();

	// Token: 0x060095DF RID: 38367
	public abstract GameObject GetGameObject();

	// Token: 0x060095E0 RID: 38368
	public abstract bool IsLoaded();
}
