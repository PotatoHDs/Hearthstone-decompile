using System;
using PegasusShared;
using UnityEngine;

// Token: 0x0200057E RID: 1406
public class DebugMissionButton : PegUIElement
{
	// Token: 0x06004E51 RID: 20049 RVA: 0x0019D798 File Offset: 0x0019B998
	private void Start()
	{
		ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(this.m_missionId);
		if (record == null)
		{
			Error.AddDevWarning("Error", "scenario {0} does not exist in the DBF", new object[]
			{
				this.m_missionId
			});
			return;
		}
		if (this.m_name != null)
		{
			this.m_name.Text = record.ShortName;
		}
		string missionHeroCardId = GameUtils.GetMissionHeroCardId(this.m_missionId);
		if (missionHeroCardId == null)
		{
			return;
		}
		DefLoader.Get().LoadCardDef(missionHeroCardId, new DefLoader.LoadDefCallback<DefLoader.DisposableCardDef>(this.OnHeroCardDefLoaded), null, null);
	}

	// Token: 0x06004E52 RID: 20050 RVA: 0x0019D82A File Offset: 0x0019BA2A
	protected override void OnDestroy()
	{
		DefLoader.DisposableFullDef heroPowerDef = this.m_heroPowerDef;
		if (heroPowerDef != null)
		{
			heroPowerDef.Dispose();
		}
		this.m_heroPowerDef = null;
		DefLoader.DisposableCardDef heroDef = this.m_heroDef;
		if (heroDef != null)
		{
			heroDef.Dispose();
		}
		this.m_heroDef = null;
		base.OnDestroy();
	}

	// Token: 0x06004E53 RID: 20051 RVA: 0x0019D862 File Offset: 0x0019BA62
	private void OnHeroCardDefLoaded(string cardID, DefLoader.DisposableCardDef cardDef, object userData)
	{
		DefLoader.DisposableCardDef heroDef = this.m_heroDef;
		if (heroDef != null)
		{
			heroDef.Dispose();
		}
		this.m_heroDef = cardDef;
		this.m_heroImage.GetComponent<Renderer>().GetMaterial().mainTexture = this.m_heroDef.CardDef.GetPortraitTexture();
	}

	// Token: 0x06004E54 RID: 20052 RVA: 0x0019D8A4 File Offset: 0x0019BAA4
	protected override void OnRelease()
	{
		if (!string.IsNullOrEmpty(this.m_introline))
		{
			string legacyAssetName = new AssetReference(this.m_introline).GetLegacyAssetName();
			if (string.IsNullOrEmpty(this.m_characterPrefabName))
			{
				NotificationManager.Get().CreateKTQuote(legacyAssetName, this.m_introline, true);
			}
			else
			{
				NotificationManager.Get().CreateCharacterQuote(this.m_characterPrefabName, GameStrings.Get(legacyAssetName), this.m_introline, true, 0f, CanvasAnchor.BOTTOM_LEFT, false);
			}
		}
		base.OnRelease();
		long selectedDeckID = DeckPickerTrayDisplay.Get().GetSelectedDeckID();
		GameMgr.Get().FindGame(GameType.GT_VS_AI, FormatType.FT_WILD, this.m_missionId, 0, selectedDeckID, null, null, false, null, GameType.GT_UNKNOWN);
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06004E55 RID: 20053 RVA: 0x0019D954 File Offset: 0x0019BB54
	protected override void OnOver(PegUIElement.InteractionState oldState)
	{
		this.m_mousedOver = true;
		base.OnOver(oldState);
		if (string.IsNullOrEmpty(GameUtils.GetMissionHeroPowerCardId(this.m_missionId)))
		{
			return;
		}
		DefLoader.Get().LoadFullDef(GameUtils.GetMissionHeroPowerCardId(this.m_missionId), new DefLoader.LoadDefCallback<DefLoader.DisposableFullDef>(this.OnHeroPowerDefLoaded), null, null);
	}

	// Token: 0x06004E56 RID: 20054 RVA: 0x0019D9A5 File Offset: 0x0019BBA5
	protected override void OnOut(PegUIElement.InteractionState oldState)
	{
		this.m_mousedOver = false;
		base.OnOut(oldState);
		if (this.m_heroPowerActor)
		{
			UnityEngine.Object.Destroy(this.m_heroPowerActor.gameObject);
		}
	}

	// Token: 0x06004E57 RID: 20055 RVA: 0x0019D9D4 File Offset: 0x0019BBD4
	private void OnHeroPowerDefLoaded(string cardID, DefLoader.DisposableFullDef def, object userData)
	{
		DefLoader.DisposableFullDef heroPowerDef = this.m_heroPowerDef;
		if (heroPowerDef != null)
		{
			heroPowerDef.Dispose();
		}
		this.m_heroPowerDef = def;
		if (!this.m_mousedOver)
		{
			return;
		}
		AssetLoader.Get().InstantiatePrefab("History_HeroPower_Opponent.prefab:a99d23d6e8630f94b96a8e096fffb16f", new PrefabCallback<GameObject>(this.OnHeroPowerActorLoaded), null, AssetLoadingOptions.IgnorePrefabPosition);
	}

	// Token: 0x06004E58 RID: 20056 RVA: 0x0019DA28 File Offset: 0x0019BC28
	private void OnHeroPowerActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (!this.m_mousedOver)
		{
			UnityEngine.Object.Destroy(go);
		}
		if (this.m_heroPowerActor)
		{
			UnityEngine.Object.Destroy(this.m_heroPowerActor.gameObject);
		}
		if (this == null || base.gameObject == null)
		{
			UnityEngine.Object.Destroy(go);
		}
		if (go == null)
		{
			return;
		}
		this.m_heroPowerActor = go.GetComponent<Actor>();
		go.transform.parent = base.gameObject.transform;
		this.m_heroPowerActor.SetCardDef(this.m_heroPowerDef.DisposableCardDef);
		this.m_heroPowerActor.SetEntityDef(this.m_heroPowerDef.EntityDef);
		this.m_heroPowerActor.UpdateAllComponents();
		go.transform.position = base.transform.position + new Vector3(15f, 0f, 0f);
		go.transform.localScale = Vector3.one;
		iTween.ScaleTo(go, new Vector3(7f, 7f, 7f), 0.5f);
		SceneUtils.SetLayer(go, GameLayer.Tooltip);
	}

	// Token: 0x0400451B RID: 17691
	public int m_missionId;

	// Token: 0x0400451C RID: 17692
	public GameObject m_heroImage;

	// Token: 0x0400451D RID: 17693
	public UberText m_name;

	// Token: 0x0400451E RID: 17694
	public string m_introline;

	// Token: 0x0400451F RID: 17695
	public string m_characterPrefabName;

	// Token: 0x04004520 RID: 17696
	private GameObject m_heroPowerObject;

	// Token: 0x04004521 RID: 17697
	private bool m_mousedOver;

	// Token: 0x04004522 RID: 17698
	private DefLoader.DisposableFullDef m_heroPowerDef;

	// Token: 0x04004523 RID: 17699
	private DefLoader.DisposableCardDef m_heroDef;

	// Token: 0x04004524 RID: 17700
	private Actor m_heroPowerActor;
}
