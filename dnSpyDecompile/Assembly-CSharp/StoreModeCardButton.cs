using System;
using bgs;
using UnityEngine;

// Token: 0x02000727 RID: 1831
public class StoreModeCardButton : UIBButton
{
	// Token: 0x060066E5 RID: 26341 RVA: 0x00218E90 File Offset: 0x00217090
	protected override void Awake()
	{
		base.Awake();
		if (this.m_dustTexture == null)
		{
			return;
		}
		if (BattleNet.GetAccountCountry() == "CHN")
		{
			Material material = this.m_RootObject.GetComponent<Renderer>().GetMaterial();
			if (material != null)
			{
				material.SetTexture("_MainTex", this.m_dustTexture);
				this.m_ButtonText.Text = GameStrings.Get("GLUE_STORE_MODE_NAME_DUST");
			}
		}
	}

	// Token: 0x040054B5 RID: 21685
	public Texture m_dustTexture;
}
