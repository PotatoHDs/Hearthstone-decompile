using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000349 RID: 841
public class TargetReticleManager : MonoBehaviour
{
	// Token: 0x060030AA RID: 12458 RVA: 0x000FA3A7 File Offset: 0x000F85A7
	private void Awake()
	{
		TargetReticleManager.s_instance = this;
	}

	// Token: 0x060030AB RID: 12459 RVA: 0x000FA3AF File Offset: 0x000F85AF
	private void OnDestroy()
	{
		TargetReticleManager.s_instance = null;
	}

	// Token: 0x060030AC RID: 12460 RVA: 0x000FA3B7 File Offset: 0x000F85B7
	public static TargetReticleManager Get()
	{
		return TargetReticleManager.s_instance;
	}

	// Token: 0x060030AD RID: 12461 RVA: 0x000FA3BE File Offset: 0x000F85BE
	public bool IsActive()
	{
		return this.GetAppropriateReticle() != null && this.m_isActive;
	}

	// Token: 0x060030AE RID: 12462 RVA: 0x000FA3D6 File Offset: 0x000F85D6
	public bool IsLocalArrow()
	{
		return !this.m_isEnemyArrow;
	}

	// Token: 0x060030AF RID: 12463 RVA: 0x000FA3E1 File Offset: 0x000F85E1
	public bool IsEnemyArrow()
	{
		return this.m_isEnemyArrow;
	}

	// Token: 0x060030B0 RID: 12464 RVA: 0x000FA3E9 File Offset: 0x000F85E9
	public bool IsLocalArrowActive()
	{
		return !this.m_isEnemyArrow && this.IsActive();
	}

	// Token: 0x060030B1 RID: 12465 RVA: 0x000FA3FB File Offset: 0x000F85FB
	public bool IsEnemyArrowActive()
	{
		return this.m_isEnemyArrow && this.IsActive();
	}

	// Token: 0x1700050B RID: 1291
	// (get) Token: 0x060030B2 RID: 12466 RVA: 0x000FA40D File Offset: 0x000F860D
	public int ArrowSourceEntityID
	{
		get
		{
			return this.m_originLocationEntityID;
		}
	}

	// Token: 0x060030B3 RID: 12467 RVA: 0x000FA418 File Offset: 0x000F8618
	public void ShowBullseye(bool show)
	{
		if (this.m_ReticleType == TARGET_RETICLE_TYPE.DefaultArrow)
		{
			if (!this.IsActive() || !this.m_showArrow)
			{
				return;
			}
			Transform transform = this.m_arrow.transform.Find("TargetArrow_TargetMesh");
			if (!transform)
			{
				return;
			}
			SceneUtils.EnableRenderers(transform.gameObject, show);
			return;
		}
		else
		{
			if (this.m_ReticleType != TARGET_RETICLE_TYPE.HunterReticle)
			{
				if (this.m_ReticleType == TARGET_RETICLE_TYPE.QuestionMark)
				{
					if (!this.IsActive() || !this.m_showArrow)
					{
						return;
					}
					Transform transform2 = this.m_questionMark.transform.Find("TargetQuestionMark_TargetMesh");
					if (!transform2)
					{
						return;
					}
					SceneUtils.EnableRenderers(transform2.gameObject, show);
				}
				return;
			}
			if (this.m_hunterReticle == null)
			{
				return;
			}
			RenderToTexture component = this.m_hunterReticle.GetComponent<RenderToTexture>();
			if (component == null)
			{
				return;
			}
			Material renderMaterial = component.GetRenderMaterial();
			if (renderMaterial == null)
			{
				return;
			}
			if (show)
			{
				renderMaterial.color = Color.red;
				return;
			}
			renderMaterial.color = Color.white;
			return;
		}
	}

	// Token: 0x060030B4 RID: 12468 RVA: 0x000FA50C File Offset: 0x000F870C
	public void CreateFriendlyTargetArrow(Entity originLocationEntity, Entity sourceEntity, bool showDamageIndicatorText, bool showArrow = true, string overrideText = null, bool useHandAsOrigin = false)
	{
		if (GameMgr.Get() == null || !GameMgr.Get().IsSpectator())
		{
			this.DisableCollidersForUntargetableCards(sourceEntity.GetCard());
		}
		if (GameState.Get().GetGameEntity().HasTag(GAME_TAG.ALL_TARGETS_RANDOM))
		{
			this.m_ReticleType = TARGET_RETICLE_TYPE.QuestionMark;
		}
		else
		{
			Spell playSpell = sourceEntity.GetCard().GetPlaySpell(0, true);
			if (playSpell != null)
			{
				this.m_ReticleType = playSpell.m_TargetReticle;
			}
			else
			{
				this.m_ReticleType = TARGET_RETICLE_TYPE.DefaultArrow;
			}
		}
		string damageIndicatorText = null;
		if (overrideText != null)
		{
			damageIndicatorText = overrideText;
		}
		else if (showDamageIndicatorText)
		{
			damageIndicatorText = sourceEntity.GetTargetingArrowText();
		}
		this.CreateTargetArrow(false, originLocationEntity.GetEntityId(), sourceEntity.GetEntityId(), damageIndicatorText, showArrow, useHandAsOrigin);
		this.AttachLinksToAppropriateReticle();
	}

	// Token: 0x060030B5 RID: 12469 RVA: 0x000FA5BC File Offset: 0x000F87BC
	private void AttachLinksToAppropriateReticle()
	{
		GameObject appropriateReticle = this.GetAppropriateReticle();
		foreach (GameObject gameObject in this.m_targetArrowLinks)
		{
			gameObject.transform.parent = appropriateReticle.transform;
		}
	}

	// Token: 0x060030B6 RID: 12470 RVA: 0x000FA620 File Offset: 0x000F8820
	public void CreateEnemyTargetArrow(Entity originEntity)
	{
		if (GameState.Get().GetGameEntity().HasTag(GAME_TAG.ALL_TARGETS_RANDOM))
		{
			this.m_ReticleType = TARGET_RETICLE_TYPE.QuestionMark;
		}
		else
		{
			this.m_ReticleType = TARGET_RETICLE_TYPE.DefaultArrow;
		}
		this.CreateTargetArrow(true, originEntity.GetEntityId(), originEntity.GetEntityId(), null, true, false);
		this.AttachLinksToAppropriateReticle();
	}

	// Token: 0x060030B7 RID: 12471 RVA: 0x000FA66F File Offset: 0x000F886F
	public void DestroyEnemyTargetArrow()
	{
		this.DestroyTargetArrow(true, false);
	}

	// Token: 0x060030B8 RID: 12472 RVA: 0x000FA679 File Offset: 0x000F8879
	public void DestroyFriendlyTargetArrow(bool isLocallyCanceled)
	{
		this.EnableCollidersThatWereDisabled();
		this.DestroyTargetArrow(false, isLocallyCanceled);
	}

	// Token: 0x060030B9 RID: 12473 RVA: 0x000FA68C File Offset: 0x000F888C
	public void UpdateArrowPosition()
	{
		if (!this.IsActive())
		{
			return;
		}
		if (!this.m_showArrow)
		{
			this.UpdateArrowOriginPosition();
			this.UpdateDamageIndicator();
			return;
		}
		float num = 0f;
		bool flag = GameMgr.Get() != null && GameMgr.Get().IsSpectator();
		Vector3 point;
		if (this.m_isEnemyArrow || flag)
		{
			Vector3 vector = Vector3.zero;
			vector = this.GetAppropriateReticle().transform.position;
			point.x = Mathf.Lerp(vector.x, this.m_remoteArrowPosition.x, 0.1f);
			point.y = Mathf.Lerp(vector.y, this.m_remoteArrowPosition.y, 0.1f);
			point.z = Mathf.Lerp(vector.z, this.m_remoteArrowPosition.z, 0.1f);
			Card card = this.m_isEnemyArrow ? RemoteActionHandler.Get().GetOpponentHeldCard() : RemoteActionHandler.Get().GetFriendlyHeldCard();
			if (card != null && card.GetEntity().GetZone() != TAG_ZONE.DECK)
			{
				this.m_targetArrowOrigin = card.transform.position;
			}
		}
		else
		{
			RaycastHit raycastHit;
			if (!UniversalInputManager.Get().GetInputHitInfo(Camera.main, GameLayer.DragPlane, out raycastHit))
			{
				return;
			}
			point = raycastHit.point;
			this.UpdateArrowOriginPosition();
		}
		if (!Mathf.Approximately(point.z - this.m_targetArrowOrigin.z, 0f))
		{
			float num2 = Mathf.Atan((point.x - this.m_targetArrowOrigin.x) / (point.z - this.m_targetArrowOrigin.z));
			num = 57.29578f * num2;
		}
		if (point.z < this.m_targetArrowOrigin.z)
		{
			num -= 180f;
		}
		if (this.m_ReticleType == TARGET_RETICLE_TYPE.DefaultArrow || this.m_ReticleType == TARGET_RETICLE_TYPE.QuestionMark)
		{
			GameObject appropriateReticle = this.GetAppropriateReticle();
			appropriateReticle.transform.localEulerAngles = new Vector3(0f, num, 0f);
			appropriateReticle.transform.position = point;
			float num3 = Mathf.Pow(this.m_targetArrowOrigin.x - point.x, 2f);
			float num4 = Mathf.Pow(this.m_targetArrowOrigin.z - point.z, 2f);
			float lengthOfArrow = Mathf.Sqrt(num3 + num4);
			this.UpdateTargetArrowLinks(lengthOfArrow);
		}
		else if (this.m_ReticleType == TARGET_RETICLE_TYPE.HunterReticle)
		{
			this.m_hunterReticle.transform.position = point;
		}
		else
		{
			Debug.LogError("Unknown Target Reticle Type!");
		}
		this.UpdateDamageIndicator();
	}

	// Token: 0x060030BA RID: 12474 RVA: 0x000FA8F2 File Offset: 0x000F8AF2
	public void SetRemotePlayerArrowPosition(Vector3 newPosition)
	{
		this.m_remoteArrowPosition = newPosition;
	}

	// Token: 0x060030BB RID: 12475 RVA: 0x000FA8FB File Offset: 0x000F8AFB
	private void DestroyCurrentArrow(bool isLocallyCanceled)
	{
		if (this.m_isEnemyArrow)
		{
			this.DestroyEnemyTargetArrow();
			return;
		}
		this.DestroyFriendlyTargetArrow(isLocallyCanceled);
	}

	// Token: 0x060030BC RID: 12476 RVA: 0x000FA914 File Offset: 0x000F8B14
	private void DisableCollidersForUntargetableCards(Card sourceCard)
	{
		List<Card> list = new List<Card>();
		foreach (Player player in GameState.Get().GetPlayerMap().Values)
		{
			this.AddUntargetableCard(sourceCard, list, player.GetHeroPowerCard());
			this.AddUntargetableCard(sourceCard, list, player.GetWeaponCard());
			foreach (Card card in player.GetSecretZone().GetCards())
			{
				this.AddUntargetableCard(sourceCard, list, card);
			}
		}
		foreach (Card card2 in list)
		{
			if (!(card2 == null))
			{
				Actor actor = card2.GetActor();
				if (!(actor == null))
				{
					actor.TurnOffCollider();
				}
			}
		}
	}

	// Token: 0x060030BD RID: 12477 RVA: 0x000FAA34 File Offset: 0x000F8C34
	private void AddUntargetableCard(Card sourceCard, List<Card> cards, Card card)
	{
		if (sourceCard == card)
		{
			return;
		}
		cards.Add(card);
	}

	// Token: 0x060030BE RID: 12478 RVA: 0x000FAA48 File Offset: 0x000F8C48
	private void EnableCollidersThatWereDisabled()
	{
		List<Card> list = new List<Card>();
		foreach (Player player in GameState.Get().GetPlayerMap().Values)
		{
			list.Add(player.GetHeroPowerCard());
			list.Add(player.GetWeaponCard());
			foreach (Card item in player.GetSecretZone().GetCards())
			{
				list.Add(item);
			}
		}
		foreach (Card card in list)
		{
			if (!(card == null) && !(card.GetActor() == null))
			{
				card.GetActor().TurnOnCollider();
			}
		}
	}

	// Token: 0x060030BF RID: 12479 RVA: 0x000FAB60 File Offset: 0x000F8D60
	private void CreateTargetArrow(bool isEnemyArrow, int originLocationEntityID, int sourceEntityID, string damageIndicatorText, bool showArrow, bool useHandAsOrigin = false)
	{
		if (this.IsActive())
		{
			Log.Gameplay.Print("Uh-oh... creating a targeting arrow but one is already active...", Array.Empty<object>());
			this.DestroyCurrentArrow(false);
		}
		this.m_isEnemyArrow = isEnemyArrow;
		this.m_sourceEntityID = sourceEntityID;
		this.m_originLocationEntityID = originLocationEntityID;
		this.m_showArrow = showArrow;
		this.m_useHandAsOrigin = useHandAsOrigin;
		this.UpdateArrowOriginPosition();
		bool flag = GameMgr.Get() != null && GameMgr.Get().IsSpectator();
		if (this.m_isEnemyArrow || flag)
		{
			this.m_remoteArrowPosition = this.m_targetArrowOrigin;
			this.m_arrow.transform.position = this.m_targetArrowOrigin;
		}
		this.ActivateArrow(true);
		this.ShowBullseye(false);
		this.ShowDamageIndicator(!this.m_isEnemyArrow);
		this.UpdateArrowPosition();
		if (!this.m_isEnemyArrow)
		{
			base.StartCoroutine(this.SetDamageText(damageIndicatorText));
			if (!flag)
			{
				PegCursor.Get().Hide();
			}
		}
	}

	// Token: 0x060030C0 RID: 12480 RVA: 0x000FAC44 File Offset: 0x000F8E44
	public void PreloadTargetArrows()
	{
		this.m_targetArrowLinks = new List<GameObject>();
		IAssetLoader assetLoader = AssetLoader.Get();
		assetLoader.InstantiatePrefab("Target_Arrow_Bullseye.prefab:7afe007e5f455b04b9407307d8df1983", new PrefabCallback<GameObject>(this.LoadArrowCallback), null, AssetLoadingOptions.IgnorePrefabPosition);
		assetLoader.InstantiatePrefab("TargetDamageIndicator.prefab:91b47a1196e64e946a974becc0fb29f1", new PrefabCallback<GameObject>(this.LoadDamageIndicatorCallback), null, AssetLoadingOptions.IgnorePrefabPosition);
		assetLoader.InstantiatePrefab("Target_Arrow_Link.prefab:eb929158148ae954881c5684d27a1aa2", new PrefabCallback<GameObject>(this.LoadLinkCallback), null, AssetLoadingOptions.IgnorePrefabPosition);
		assetLoader.InstantiatePrefab("HunterReticle.prefab:83c7a1ebe50ef476f891c1b39dd5fd88", new PrefabCallback<GameObject>(this.LoadHunterReticleCallback), null, AssetLoadingOptions.IgnorePrefabPosition);
		assetLoader.InstantiatePrefab("Target_Question_Mark.prefab:adc81f6922c3de840b0e071ac55c7d62", new PrefabCallback<GameObject>(this.LoadQuestionCallback), null, AssetLoadingOptions.IgnorePrefabPosition);
	}

	// Token: 0x060030C1 RID: 12481 RVA: 0x000FACFC File Offset: 0x000F8EFC
	private void DestroyTargetArrow(bool destroyEnemyArrow, bool isLocallyCanceled)
	{
		if (!this.IsActive())
		{
			return;
		}
		if (destroyEnemyArrow != this.m_isEnemyArrow)
		{
			Log.Gameplay.Print(string.Format("trying to destroy {0} arrow but the active arrow is {1}", destroyEnemyArrow ? "enemy" : "friendly", this.m_isEnemyArrow ? "enemy" : "friendly"), Array.Empty<object>());
			return;
		}
		if (isLocallyCanceled)
		{
			Entity entity = GameState.Get().GetEntity(this.m_sourceEntityID);
			if (entity != null)
			{
				entity.GetCard().NotifyTargetingCanceled();
			}
		}
		this.m_originLocationEntityID = -1;
		this.m_sourceEntityID = -1;
		if (!this.m_isEnemyArrow)
		{
			RemoteActionHandler.Get().NotifyOpponentOfTargetEnd();
			PegCursor.Get().Show();
		}
		this.ActivateArrow(false);
		this.ShowDamageIndicator(false);
	}

	// Token: 0x060030C2 RID: 12482 RVA: 0x000FADB2 File Offset: 0x000F8FB2
	private void LoadArrowCallback(AssetReference assetRef, GameObject go, object callbackData)
	{
		this.m_arrow = go;
		this.ShowBullseye(false);
	}

	// Token: 0x060030C3 RID: 12483 RVA: 0x000FADC2 File Offset: 0x000F8FC2
	private void LoadQuestionCallback(AssetReference assetRef, GameObject go, object callbackData)
	{
		this.m_questionMark = go;
		this.ShowBullseye(false);
	}

	// Token: 0x060030C4 RID: 12484 RVA: 0x000FADD2 File Offset: 0x000F8FD2
	private void LoadLinkCallback(AssetReference assetRef, GameObject go, object callbackData)
	{
		base.StartCoroutine(this.OnLinkLoaded(go));
	}

	// Token: 0x060030C5 RID: 12485 RVA: 0x000FADE4 File Offset: 0x000F8FE4
	private void LoadDamageIndicatorCallback(AssetReference assetRef, GameObject go, object callbackData)
	{
		this.m_damageIndicator = go.GetComponent<TargetDamageIndicator>();
		if (this.m_damageIndicator == null)
		{
			Log.Gameplay.PrintError("LoadDamageIndicatorCallback - No TargetDamageIndicator script attached to '{0}'!", new object[]
			{
				go.name
			});
			return;
		}
		this.m_damageIndicator.transform.eulerAngles = new Vector3(90f, 0f, 0f);
		this.m_damageIndicator.transform.localScale = new Vector3(TargetReticleManager.DAMAGE_INDICATOR_SCALE, TargetReticleManager.DAMAGE_INDICATOR_SCALE, TargetReticleManager.DAMAGE_INDICATOR_SCALE);
	}

	// Token: 0x060030C6 RID: 12486 RVA: 0x000FAE81 File Offset: 0x000F9081
	private void LoadHunterReticleCallback(AssetReference assetRef, GameObject go, object callbackData)
	{
		this.m_hunterReticle = go;
		this.m_hunterReticle.transform.parent = base.transform;
		this.m_hunterReticle.SetActive(false);
	}

	// Token: 0x060030C7 RID: 12487 RVA: 0x000FAEAC File Offset: 0x000F90AC
	private IEnumerator OnLinkLoaded(GameObject linkActorObject)
	{
		while (this.m_arrow == null)
		{
			yield return null;
		}
		for (int i = 0; i < 14; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(linkActorObject);
			gameObject.transform.parent = this.m_arrow.transform;
			this.m_targetArrowLinks.Add(gameObject);
		}
		linkActorObject.transform.parent = this.m_arrow.transform;
		this.m_targetArrowLinks.Add(linkActorObject);
		yield break;
	}

	// Token: 0x060030C8 RID: 12488 RVA: 0x000FAEC4 File Offset: 0x000F90C4
	private int NumberOfRequiredLinks(float lengthOfArrow)
	{
		int num = (int)Mathf.Floor(lengthOfArrow / 1.2f) + 1;
		if (num == 1)
		{
			num = 0;
		}
		return num;
	}

	// Token: 0x060030C9 RID: 12489 RVA: 0x000FAEE8 File Offset: 0x000F90E8
	private GameObject GetAppropriateReticle()
	{
		switch (this.m_ReticleType)
		{
		case TARGET_RETICLE_TYPE.DefaultArrow:
			return this.m_arrow;
		case TARGET_RETICLE_TYPE.HunterReticle:
			return this.m_hunterReticle;
		case TARGET_RETICLE_TYPE.QuestionMark:
			return this.m_questionMark;
		default:
			Log.All.PrintError("Unknown Target Reticle Type!", Array.Empty<object>());
			return null;
		}
	}

	// Token: 0x060030CA RID: 12490 RVA: 0x000FAF3C File Offset: 0x000F913C
	private Transform GetAppropriateArrowMeshTransform()
	{
		TARGET_RETICLE_TYPE reticleType = this.m_ReticleType;
		if (reticleType <= TARGET_RETICLE_TYPE.HunterReticle)
		{
			return this.m_arrow.transform.Find("TargetArrow_ArrowMesh");
		}
		if (reticleType != TARGET_RETICLE_TYPE.QuestionMark)
		{
			Log.All.PrintError("Unknown Target Reticle Type!", Array.Empty<object>());
			return null;
		}
		return this.m_questionMark.transform.Find("TargetQuestionMark_QuestionMarkMesh");
	}

	// Token: 0x060030CB RID: 12491 RVA: 0x000FAF9C File Offset: 0x000F919C
	private float GetStartingXRotationForArrowMesh()
	{
		TARGET_RETICLE_TYPE reticleType = this.m_ReticleType;
		if (reticleType <= TARGET_RETICLE_TYPE.HunterReticle)
		{
			return 300f;
		}
		if (reticleType != TARGET_RETICLE_TYPE.QuestionMark)
		{
			Log.All.PrintError("Unknown Target Reticle Type!", Array.Empty<object>());
			return 0f;
		}
		return 0f;
	}

	// Token: 0x060030CC RID: 12492 RVA: 0x000FAFE0 File Offset: 0x000F91E0
	private void UpdateTargetArrowLinks(float lengthOfArrow)
	{
		this.m_numActiveLinks = this.NumberOfRequiredLinks(lengthOfArrow);
		int count = this.m_targetArrowLinks.Count;
		Transform appropriateArrowMeshTransform = this.GetAppropriateArrowMeshTransform();
		if (this.m_numActiveLinks == 0)
		{
			appropriateArrowMeshTransform.localEulerAngles = new Vector3(this.GetStartingXRotationForArrowMesh(), 180f, 0f);
			for (int i = 0; i < count; i++)
			{
				SceneUtils.EnableRenderers(this.m_targetArrowLinks[i].gameObject, false);
			}
			return;
		}
		float num = -lengthOfArrow / 2f;
		float num2 = -1.5f / (num * num);
		for (int j = 0; j < count; j++)
		{
			if (!(this.m_targetArrowLinks[j] == null))
			{
				if (j >= this.m_numActiveLinks)
				{
					SceneUtils.EnableRenderers(this.m_targetArrowLinks[j].gameObject, false);
				}
				else
				{
					float num3 = -(1.2f * (float)(j + 1)) + this.m_linkAnimationZOffset;
					float y = num2 * Mathf.Pow(num3 - num, 2f) + 1.5f;
					float num4 = Mathf.Atan(2f * num2 * (num3 - num));
					float x = 180f - num4 * 57.29578f;
					SceneUtils.EnableRenderers(this.m_targetArrowLinks[j].gameObject, true);
					this.m_targetArrowLinks[j].transform.localPosition = new Vector3(0f, y, num3);
					this.m_targetArrowLinks[j].transform.eulerAngles = new Vector3(x, this.GetAppropriateReticle().transform.localEulerAngles.y, 0f);
					float num5 = 1f;
					if (j == 0)
					{
						if (num3 > -1.2f)
						{
							num5 = num3 / -1.2f;
							num5 = Mathf.Pow(num5, 6f);
						}
					}
					else if (j == this.m_numActiveLinks - 1)
					{
						num5 = this.m_linkAnimationZOffset / 1.2f;
						num5 *= num5;
					}
					this.SetLinkAlpha(this.m_targetArrowLinks[j], num5);
				}
			}
		}
		float y2 = num2 * Mathf.Pow(appropriateArrowMeshTransform.localPosition.z - num, 2f) + 1.5f;
		float num6 = 0f;
		if (this.m_ReticleType != TARGET_RETICLE_TYPE.QuestionMark)
		{
			num6 = Mathf.Atan(2f * num2 * (appropriateArrowMeshTransform.localPosition.z - num)) * 57.29578f;
			if (num6 < 0f)
			{
				num6 += 360f;
			}
		}
		appropriateArrowMeshTransform.localPosition = new Vector3(0f, y2, appropriateArrowMeshTransform.localPosition.z);
		appropriateArrowMeshTransform.localEulerAngles = new Vector3(num6, 180f, 0f);
		this.m_linkAnimationZOffset += Time.deltaTime * 0.5f;
		if (this.m_linkAnimationZOffset > 1.2f)
		{
			this.m_linkAnimationZOffset -= 1.2f;
		}
	}

	// Token: 0x060030CD RID: 12493 RVA: 0x000FB2BC File Offset: 0x000F94BC
	private void SetLinkAlpha(GameObject linkGameObject, float alpha)
	{
		alpha = Mathf.Clamp(alpha, 0f, 1f);
		Renderer[] components = linkGameObject.GetComponents<Renderer>();
		for (int i = 0; i < components.Length; i++)
		{
			Material material = components[i].GetMaterial();
			Color color = material.color;
			color.a = alpha;
			material.color = color;
		}
	}

	// Token: 0x060030CE RID: 12494 RVA: 0x000FB310 File Offset: 0x000F9510
	private void UpdateDamageIndicator()
	{
		if (this.m_damageIndicator == null)
		{
			return;
		}
		Vector3 position = Vector3.zero;
		if (TargetReticleManager.SHOW_DAMAGE_INDICATOR_ON_ENTITY)
		{
			position = this.m_targetArrowOrigin;
			position.z += TargetReticleManager.DAMAGE_INDICATOR_Z_OFFSET;
		}
		else
		{
			position = this.GetAppropriateReticle().transform.position;
			position.z += TargetReticleManager.DAMAGE_INDICATOR_Z_OFFSET;
		}
		this.m_damageIndicator.transform.position = position;
	}

	// Token: 0x060030CF RID: 12495 RVA: 0x000FB393 File Offset: 0x000F9593
	private void ShowDamageIndicator(bool show)
	{
		if (!this.m_damageIndicator || !this.m_damageIndicator.gameObject.activeInHierarchy)
		{
			return;
		}
		this.m_damageIndicator.Show(show);
	}

	// Token: 0x060030D0 RID: 12496 RVA: 0x000FB3C1 File Offset: 0x000F95C1
	private IEnumerator SetDamageText(string damageText)
	{
		while (this.m_damageIndicator == null)
		{
			yield return null;
		}
		this.m_damageIndicator.SetText(damageText);
		if (string.IsNullOrEmpty(damageText))
		{
			this.m_damageIndicator.Show(false);
		}
		yield break;
	}

	// Token: 0x060030D1 RID: 12497 RVA: 0x000FB3D8 File Offset: 0x000F95D8
	private void UpdateArrowOriginPosition()
	{
		Entity entity = GameState.Get().GetEntity(this.m_originLocationEntityID);
		if (entity == null)
		{
			Log.Gameplay.Print(string.Format("entity with ID {0} does not exist... can't update arrow origin position!", this.m_originLocationEntityID), Array.Empty<object>());
			this.DestroyCurrentArrow(false);
			return;
		}
		this.m_targetArrowOrigin = entity.GetCard().transform.position;
		if (this.m_useHandAsOrigin || entity.GetZone() == TAG_ZONE.DECK)
		{
			if (this.m_isEnemyArrow)
			{
				this.m_targetArrowOrigin = InputManager.Get().GetEnemyHand().transform.position;
			}
			else
			{
				this.m_targetArrowOrigin = InputManager.Get().GetFriendlyHand().transform.position;
			}
		}
		if (entity.IsHero() && !this.m_isEnemyArrow)
		{
			this.m_targetArrowOrigin.z = this.m_targetArrowOrigin.z + 1f;
		}
	}

	// Token: 0x060030D2 RID: 12498 RVA: 0x000FB4B0 File Offset: 0x000F96B0
	private void ActivateArrow(bool active)
	{
		this.m_isActive = active;
		SceneUtils.EnableRenderers(this.m_arrow.gameObject, false);
		this.m_hunterReticle.SetActive(false);
		SceneUtils.EnableRenderers(this.m_questionMark.gameObject, false);
		if (!active)
		{
			return;
		}
		if (this.m_ReticleType == TARGET_RETICLE_TYPE.DefaultArrow)
		{
			SceneUtils.EnableRenderers(this.m_arrow.gameObject, active && this.m_showArrow);
			return;
		}
		if (this.m_ReticleType == TARGET_RETICLE_TYPE.HunterReticle)
		{
			this.m_hunterReticle.SetActive(active && this.m_showArrow);
			return;
		}
		if (this.m_ReticleType == TARGET_RETICLE_TYPE.QuestionMark)
		{
			SceneUtils.EnableRenderers(this.m_questionMark.gameObject, active && this.m_showArrow);
			return;
		}
		Debug.LogError("Unknown Target Reticle Type!");
	}

	// Token: 0x060030D3 RID: 12499 RVA: 0x000FB56C File Offset: 0x000F976C
	public void ShowArrow(bool show)
	{
		this.m_showArrow = show;
		SceneUtils.EnableRenderers(this.m_arrow.gameObject, false);
		this.m_hunterReticle.SetActive(false);
		SceneUtils.EnableRenderers(this.m_questionMark.gameObject, false);
		if (!show)
		{
			return;
		}
		if (this.m_ReticleType == TARGET_RETICLE_TYPE.DefaultArrow)
		{
			SceneUtils.EnableRenderers(this.m_arrow.gameObject, show);
			return;
		}
		if (this.m_ReticleType == TARGET_RETICLE_TYPE.HunterReticle)
		{
			this.m_hunterReticle.SetActive(show);
			return;
		}
		if (this.m_ReticleType == TARGET_RETICLE_TYPE.QuestionMark)
		{
			SceneUtils.EnableRenderers(this.m_questionMark.gameObject, show);
			return;
		}
		Debug.LogError("Unknown Target Reticle Type!");
	}

	// Token: 0x04001B08 RID: 6920
	private const int MAX_TARGET_ARROW_LINKS = 15;

	// Token: 0x04001B09 RID: 6921
	private const float LINK_Y_LENGTH = 1f;

	// Token: 0x04001B0A RID: 6922
	private const float LENGTH_BETWEEN_LINKS = 1.2f;

	// Token: 0x04001B0B RID: 6923
	private const float LINK_PARABOLA_HEIGHT = 1.5f;

	// Token: 0x04001B0C RID: 6924
	private const float LINK_ANIMATION_SPEED = 0.5f;

	// Token: 0x04001B0D RID: 6925
	private const float STARTING_X_ROTATION_FOR_DEFAULT_ARROW = 300f;

	// Token: 0x04001B0E RID: 6926
	private TARGET_RETICLE_TYPE m_ReticleType;

	// Token: 0x04001B0F RID: 6927
	private static readonly PlatformDependentValue<bool> SHOW_DAMAGE_INDICATOR_ON_ENTITY = new PlatformDependentValue<bool>(PlatformCategory.Input)
	{
		Mouse = false,
		Touch = true
	};

	// Token: 0x04001B10 RID: 6928
	private static readonly PlatformDependentValue<float> DAMAGE_INDICATOR_SCALE = new PlatformDependentValue<float>(PlatformCategory.Screen)
	{
		PC = 2.5f,
		Tablet = 3.75f
	};

	// Token: 0x04001B11 RID: 6929
	private static readonly PlatformDependentValue<float> DAMAGE_INDICATOR_Z_OFFSET = new PlatformDependentValue<float>(PlatformCategory.Screen)
	{
		PC = 0.75f,
		Tablet = -1.2f
	};

	// Token: 0x04001B12 RID: 6930
	private const float FRIENDLY_HERO_ORIGIN_Z_OFFSET = 1f;

	// Token: 0x04001B13 RID: 6931
	private const float LINK_FADE_OFFSET = -1.2f;

	// Token: 0x04001B14 RID: 6932
	private static TargetReticleManager s_instance;

	// Token: 0x04001B15 RID: 6933
	private bool m_isEnemyArrow;

	// Token: 0x04001B16 RID: 6934
	private bool m_isActive;

	// Token: 0x04001B17 RID: 6935
	private bool m_showArrow = true;

	// Token: 0x04001B18 RID: 6936
	private int m_originLocationEntityID = -1;

	// Token: 0x04001B19 RID: 6937
	private int m_sourceEntityID = -1;

	// Token: 0x04001B1A RID: 6938
	private int m_numActiveLinks;

	// Token: 0x04001B1B RID: 6939
	private float m_linkAnimationZOffset;

	// Token: 0x04001B1C RID: 6940
	private Vector3 m_targetArrowOrigin;

	// Token: 0x04001B1D RID: 6941
	private Vector3 m_remoteArrowPosition;

	// Token: 0x04001B1E RID: 6942
	private GameObject m_arrow;

	// Token: 0x04001B1F RID: 6943
	private TargetDamageIndicator m_damageIndicator;

	// Token: 0x04001B20 RID: 6944
	private GameObject m_hunterReticle;

	// Token: 0x04001B21 RID: 6945
	private GameObject m_questionMark;

	// Token: 0x04001B22 RID: 6946
	private List<GameObject> m_targetArrowLinks;

	// Token: 0x04001B23 RID: 6947
	private bool m_useHandAsOrigin;
}
