using System;
using UnityEngine;

// Token: 0x02000B13 RID: 2835
public interface GameMenuInterface
{
	// Token: 0x060096CC RID: 38604
	bool GameMenuIsShown();

	// Token: 0x060096CD RID: 38605
	void GameMenuShow();

	// Token: 0x060096CE RID: 38606
	void GameMenuHide();

	// Token: 0x060096CF RID: 38607
	void GameMenuShowOptionsMenu();

	// Token: 0x060096D0 RID: 38608
	GameObject GameMenuGetGameObject();
}
