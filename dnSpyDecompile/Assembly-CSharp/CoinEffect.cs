using System;
using UnityEngine;

// Token: 0x02000A1D RID: 2589
public class CoinEffect : MonoBehaviour
{
	// Token: 0x06008B90 RID: 35728 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void Start()
	{
	}

	// Token: 0x06008B91 RID: 35729 RVA: 0x002CA13C File Offset: 0x002C833C
	public void DoAnim(bool localWin)
	{
		if (localWin)
		{
			this.animToUse = this.coinDropAnim2;
			this.GlowanimToUse = this.coinGlowDropAnim2;
		}
		else
		{
			this.animToUse = this.coinDropAnim;
			this.GlowanimToUse = this.coinGlowDropAnim;
		}
		this.coinSpawnObject.SetActive(true);
		this.coin.SetActive(true);
		this.coinGlow.SetActive(true);
		this.coinSpawnObject.GetComponent<Animation>().Stop(this.coinSpawnAnim);
		this.coin.GetComponent<Animation>().Stop(this.animToUse);
		this.coinGlow.GetComponent<Animation>().Stop(this.GlowanimToUse);
		this.coinSpawnObject.GetComponent<Animation>().Play(this.coinSpawnAnim);
		this.coin.GetComponent<Animation>().Play(this.animToUse);
		this.coinGlow.GetComponent<Animation>().Play(this.GlowanimToUse);
	}

	// Token: 0x0400741B RID: 29723
	public GameObject coinSpawnObject;

	// Token: 0x0400741C RID: 29724
	private string coinSpawnAnim = "CoinSpawn1_edit";

	// Token: 0x0400741D RID: 29725
	public GameObject coin;

	// Token: 0x0400741E RID: 29726
	private string coinDropAnim = "MulliganCoinDropGo2Card";

	// Token: 0x0400741F RID: 29727
	public GameObject coinGlow;

	// Token: 0x04007420 RID: 29728
	private string coinDropAnim2 = "MulliganCoinDrop2_Edit";

	// Token: 0x04007421 RID: 29729
	private string animToUse;

	// Token: 0x04007422 RID: 29730
	private string coinGlowDropAnim = "MulliganCoinDrop1Glow_Edit";

	// Token: 0x04007423 RID: 29731
	private string coinGlowDropAnim2 = "MulliganCoinDrop2Glow_Edit";

	// Token: 0x04007424 RID: 29732
	private string GlowanimToUse;
}
