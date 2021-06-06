using bgs;
using UnityEngine;

public class StoreModeCardButton : UIBButton
{
	public Texture m_dustTexture;

	protected override void Awake()
	{
		base.Awake();
		if (!(m_dustTexture == null) && BattleNet.GetAccountCountry() == "CHN")
		{
			Material material = m_RootObject.GetComponent<Renderer>().GetMaterial();
			if (material != null)
			{
				material.SetTexture("_MainTex", m_dustTexture);
				m_ButtonText.Text = GameStrings.Get("GLUE_STORE_MODE_NAME_DUST");
			}
		}
	}
}
