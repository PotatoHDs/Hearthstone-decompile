using System;
using UnityEngine;

// Token: 0x02000098 RID: 152
public interface IChatLogUI
{
	// Token: 0x17000082 RID: 130
	// (get) Token: 0x06000991 RID: 2449
	bool IsShowing { get; }

	// Token: 0x17000083 RID: 131
	// (get) Token: 0x06000992 RID: 2450
	GameObject GameObject { get; }

	// Token: 0x17000084 RID: 132
	// (get) Token: 0x06000993 RID: 2451
	BnetPlayer Receiver { get; }

	// Token: 0x06000994 RID: 2452
	void ShowForPlayer(BnetPlayer player);

	// Token: 0x06000995 RID: 2453
	void Hide();

	// Token: 0x06000996 RID: 2454
	void GoBack();
}
