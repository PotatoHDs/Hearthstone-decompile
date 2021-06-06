using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetReticleManager : MonoBehaviour
{
	private const int MAX_TARGET_ARROW_LINKS = 15;

	private const float LINK_Y_LENGTH = 1f;

	private const float LENGTH_BETWEEN_LINKS = 1.2f;

	private const float LINK_PARABOLA_HEIGHT = 1.5f;

	private const float LINK_ANIMATION_SPEED = 0.5f;

	private const float STARTING_X_ROTATION_FOR_DEFAULT_ARROW = 300f;

	private TARGET_RETICLE_TYPE m_ReticleType;

	private static readonly PlatformDependentValue<bool> SHOW_DAMAGE_INDICATOR_ON_ENTITY = new PlatformDependentValue<bool>(PlatformCategory.Input)
	{
		Mouse = false,
		Touch = true
	};

	private static readonly PlatformDependentValue<float> DAMAGE_INDICATOR_SCALE = new PlatformDependentValue<float>(PlatformCategory.Screen)
	{
		PC = 2.5f,
		Tablet = 3.75f
	};

	private static readonly PlatformDependentValue<float> DAMAGE_INDICATOR_Z_OFFSET = new PlatformDependentValue<float>(PlatformCategory.Screen)
	{
		PC = 0.75f,
		Tablet = -1.2f
	};

	private const float FRIENDLY_HERO_ORIGIN_Z_OFFSET = 1f;

	private const float LINK_FADE_OFFSET = -1.2f;

	private static TargetReticleManager s_instance;

	private bool m_isEnemyArrow;

	private bool m_isActive;

	private bool m_showArrow = true;

	private int m_originLocationEntityID = -1;

	private int m_sourceEntityID = -1;

	private int m_numActiveLinks;

	private float m_linkAnimationZOffset;

	private Vector3 m_targetArrowOrigin;

	private Vector3 m_remoteArrowPosition;

	private GameObject m_arrow;

	private TargetDamageIndicator m_damageIndicator;

	private GameObject m_hunterReticle;

	private GameObject m_questionMark;

	private List<GameObject> m_targetArrowLinks;

	private bool m_useHandAsOrigin;

	public int ArrowSourceEntityID => m_originLocationEntityID;

	private void Awake()
	{
		s_instance = this;
	}

	private void OnDestroy()
	{
		s_instance = null;
	}

	public static TargetReticleManager Get()
	{
		return s_instance;
	}

	public bool IsActive()
	{
		if (GetAppropriateReticle() != null)
		{
			return m_isActive;
		}
		return false;
	}

	public bool IsLocalArrow()
	{
		return !m_isEnemyArrow;
	}

	public bool IsEnemyArrow()
	{
		return m_isEnemyArrow;
	}

	public bool IsLocalArrowActive()
	{
		if (m_isEnemyArrow)
		{
			return false;
		}
		return IsActive();
	}

	public bool IsEnemyArrowActive()
	{
		if (!m_isEnemyArrow)
		{
			return false;
		}
		return IsActive();
	}

	public void ShowBullseye(bool show)
	{
		if (m_ReticleType == TARGET_RETICLE_TYPE.DefaultArrow)
		{
			if (IsActive() && m_showArrow)
			{
				Transform transform = m_arrow.transform.Find("TargetArrow_TargetMesh");
				if ((bool)transform)
				{
					SceneUtils.EnableRenderers(transform.gameObject, show);
				}
			}
		}
		else if (m_ReticleType == TARGET_RETICLE_TYPE.HunterReticle)
		{
			if (m_hunterReticle == null)
			{
				return;
			}
			RenderToTexture component = m_hunterReticle.GetComponent<RenderToTexture>();
			if (component == null)
			{
				return;
			}
			Material renderMaterial = component.GetRenderMaterial();
			if (!(renderMaterial == null))
			{
				if (show)
				{
					renderMaterial.color = Color.red;
				}
				else
				{
					renderMaterial.color = Color.white;
				}
			}
		}
		else if (m_ReticleType == TARGET_RETICLE_TYPE.QuestionMark && IsActive() && m_showArrow)
		{
			Transform transform2 = m_questionMark.transform.Find("TargetQuestionMark_TargetMesh");
			if ((bool)transform2)
			{
				SceneUtils.EnableRenderers(transform2.gameObject, show);
			}
		}
	}

	public void CreateFriendlyTargetArrow(Entity originLocationEntity, Entity sourceEntity, bool showDamageIndicatorText, bool showArrow = true, string overrideText = null, bool useHandAsOrigin = false)
	{
		if (GameMgr.Get() == null || !GameMgr.Get().IsSpectator())
		{
			DisableCollidersForUntargetableCards(sourceEntity.GetCard());
		}
		if (GameState.Get().GetGameEntity().HasTag(GAME_TAG.ALL_TARGETS_RANDOM))
		{
			m_ReticleType = TARGET_RETICLE_TYPE.QuestionMark;
		}
		else
		{
			Spell playSpell = sourceEntity.GetCard().GetPlaySpell(0);
			if (playSpell != null)
			{
				m_ReticleType = playSpell.m_TargetReticle;
			}
			else
			{
				m_ReticleType = TARGET_RETICLE_TYPE.DefaultArrow;
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
		CreateTargetArrow(isEnemyArrow: false, originLocationEntity.GetEntityId(), sourceEntity.GetEntityId(), damageIndicatorText, showArrow, useHandAsOrigin);
		AttachLinksToAppropriateReticle();
	}

	private void AttachLinksToAppropriateReticle()
	{
		GameObject appropriateReticle = GetAppropriateReticle();
		foreach (GameObject targetArrowLink in m_targetArrowLinks)
		{
			targetArrowLink.transform.parent = appropriateReticle.transform;
		}
	}

	public void CreateEnemyTargetArrow(Entity originEntity)
	{
		if (GameState.Get().GetGameEntity().HasTag(GAME_TAG.ALL_TARGETS_RANDOM))
		{
			m_ReticleType = TARGET_RETICLE_TYPE.QuestionMark;
		}
		else
		{
			m_ReticleType = TARGET_RETICLE_TYPE.DefaultArrow;
		}
		CreateTargetArrow(isEnemyArrow: true, originEntity.GetEntityId(), originEntity.GetEntityId(), null, showArrow: true);
		AttachLinksToAppropriateReticle();
	}

	public void DestroyEnemyTargetArrow()
	{
		DestroyTargetArrow(destroyEnemyArrow: true, isLocallyCanceled: false);
	}

	public void DestroyFriendlyTargetArrow(bool isLocallyCanceled)
	{
		EnableCollidersThatWereDisabled();
		DestroyTargetArrow(destroyEnemyArrow: false, isLocallyCanceled);
	}

	public void UpdateArrowPosition()
	{
		if (!IsActive())
		{
			return;
		}
		if (!m_showArrow)
		{
			UpdateArrowOriginPosition();
			UpdateDamageIndicator();
			return;
		}
		float num = 0f;
		bool flag = GameMgr.Get() != null && GameMgr.Get().IsSpectator();
		Vector3 point = default(Vector3);
		if (m_isEnemyArrow || flag)
		{
			Vector3 zero = Vector3.zero;
			zero = GetAppropriateReticle().transform.position;
			point.x = Mathf.Lerp(zero.x, m_remoteArrowPosition.x, 0.1f);
			point.y = Mathf.Lerp(zero.y, m_remoteArrowPosition.y, 0.1f);
			point.z = Mathf.Lerp(zero.z, m_remoteArrowPosition.z, 0.1f);
			Card card = (m_isEnemyArrow ? RemoteActionHandler.Get().GetOpponentHeldCard() : RemoteActionHandler.Get().GetFriendlyHeldCard());
			if (card != null && card.GetEntity().GetZone() != TAG_ZONE.DECK)
			{
				m_targetArrowOrigin = card.transform.position;
			}
		}
		else
		{
			if (!UniversalInputManager.Get().GetInputHitInfo(Camera.main, GameLayer.DragPlane, out var hitInfo))
			{
				return;
			}
			point = hitInfo.point;
			UpdateArrowOriginPosition();
		}
		if (!Mathf.Approximately(point.z - m_targetArrowOrigin.z, 0f))
		{
			float num2 = Mathf.Atan((point.x - m_targetArrowOrigin.x) / (point.z - m_targetArrowOrigin.z));
			num = 57.29578f * num2;
		}
		if (point.z < m_targetArrowOrigin.z)
		{
			num -= 180f;
		}
		if (m_ReticleType == TARGET_RETICLE_TYPE.DefaultArrow || m_ReticleType == TARGET_RETICLE_TYPE.QuestionMark)
		{
			GameObject appropriateReticle = GetAppropriateReticle();
			appropriateReticle.transform.localEulerAngles = new Vector3(0f, num, 0f);
			appropriateReticle.transform.position = point;
			float num3 = Mathf.Pow(m_targetArrowOrigin.x - point.x, 2f);
			float num4 = Mathf.Pow(m_targetArrowOrigin.z - point.z, 2f);
			float lengthOfArrow = Mathf.Sqrt(num3 + num4);
			UpdateTargetArrowLinks(lengthOfArrow);
		}
		else if (m_ReticleType == TARGET_RETICLE_TYPE.HunterReticle)
		{
			m_hunterReticle.transform.position = point;
		}
		else
		{
			Debug.LogError("Unknown Target Reticle Type!");
		}
		UpdateDamageIndicator();
	}

	public void SetRemotePlayerArrowPosition(Vector3 newPosition)
	{
		m_remoteArrowPosition = newPosition;
	}

	private void DestroyCurrentArrow(bool isLocallyCanceled)
	{
		if (m_isEnemyArrow)
		{
			DestroyEnemyTargetArrow();
		}
		else
		{
			DestroyFriendlyTargetArrow(isLocallyCanceled);
		}
	}

	private void DisableCollidersForUntargetableCards(Card sourceCard)
	{
		List<Card> list = new List<Card>();
		foreach (Player value in GameState.Get().GetPlayerMap().Values)
		{
			AddUntargetableCard(sourceCard, list, value.GetHeroPowerCard());
			AddUntargetableCard(sourceCard, list, value.GetWeaponCard());
			foreach (Card card in value.GetSecretZone().GetCards())
			{
				AddUntargetableCard(sourceCard, list, card);
			}
		}
		foreach (Card item in list)
		{
			if (!(item == null))
			{
				Actor actor = item.GetActor();
				if (!(actor == null))
				{
					actor.TurnOffCollider();
				}
			}
		}
	}

	private void AddUntargetableCard(Card sourceCard, List<Card> cards, Card card)
	{
		if (!(sourceCard == card))
		{
			cards.Add(card);
		}
	}

	private void EnableCollidersThatWereDisabled()
	{
		List<Card> list = new List<Card>();
		foreach (Player value in GameState.Get().GetPlayerMap().Values)
		{
			list.Add(value.GetHeroPowerCard());
			list.Add(value.GetWeaponCard());
			foreach (Card card in value.GetSecretZone().GetCards())
			{
				list.Add(card);
			}
		}
		foreach (Card item in list)
		{
			if (!(item == null) && !(item.GetActor() == null))
			{
				item.GetActor().TurnOnCollider();
			}
		}
	}

	private void CreateTargetArrow(bool isEnemyArrow, int originLocationEntityID, int sourceEntityID, string damageIndicatorText, bool showArrow, bool useHandAsOrigin = false)
	{
		if (IsActive())
		{
			Log.Gameplay.Print("Uh-oh... creating a targeting arrow but one is already active...");
			DestroyCurrentArrow(isLocallyCanceled: false);
		}
		m_isEnemyArrow = isEnemyArrow;
		m_sourceEntityID = sourceEntityID;
		m_originLocationEntityID = originLocationEntityID;
		m_showArrow = showArrow;
		m_useHandAsOrigin = useHandAsOrigin;
		UpdateArrowOriginPosition();
		bool flag = GameMgr.Get() != null && GameMgr.Get().IsSpectator();
		if (m_isEnemyArrow || flag)
		{
			m_remoteArrowPosition = m_targetArrowOrigin;
			m_arrow.transform.position = m_targetArrowOrigin;
		}
		ActivateArrow(active: true);
		ShowBullseye(show: false);
		ShowDamageIndicator(!m_isEnemyArrow);
		UpdateArrowPosition();
		if (!m_isEnemyArrow)
		{
			StartCoroutine(SetDamageText(damageIndicatorText));
			if (!flag)
			{
				PegCursor.Get().Hide();
			}
		}
	}

	public void PreloadTargetArrows()
	{
		m_targetArrowLinks = new List<GameObject>();
		IAssetLoader assetLoader = AssetLoader.Get();
		assetLoader.InstantiatePrefab("Target_Arrow_Bullseye.prefab:7afe007e5f455b04b9407307d8df1983", LoadArrowCallback, null, AssetLoadingOptions.IgnorePrefabPosition);
		assetLoader.InstantiatePrefab("TargetDamageIndicator.prefab:91b47a1196e64e946a974becc0fb29f1", LoadDamageIndicatorCallback, null, AssetLoadingOptions.IgnorePrefabPosition);
		assetLoader.InstantiatePrefab("Target_Arrow_Link.prefab:eb929158148ae954881c5684d27a1aa2", LoadLinkCallback, null, AssetLoadingOptions.IgnorePrefabPosition);
		assetLoader.InstantiatePrefab("HunterReticle.prefab:83c7a1ebe50ef476f891c1b39dd5fd88", LoadHunterReticleCallback, null, AssetLoadingOptions.IgnorePrefabPosition);
		assetLoader.InstantiatePrefab("Target_Question_Mark.prefab:adc81f6922c3de840b0e071ac55c7d62", LoadQuestionCallback, null, AssetLoadingOptions.IgnorePrefabPosition);
	}

	private void DestroyTargetArrow(bool destroyEnemyArrow, bool isLocallyCanceled)
	{
		if (!IsActive())
		{
			return;
		}
		if (destroyEnemyArrow != m_isEnemyArrow)
		{
			Log.Gameplay.Print(string.Format("trying to destroy {0} arrow but the active arrow is {1}", destroyEnemyArrow ? "enemy" : "friendly", m_isEnemyArrow ? "enemy" : "friendly"));
			return;
		}
		if (isLocallyCanceled)
		{
			GameState.Get().GetEntity(m_sourceEntityID)?.GetCard().NotifyTargetingCanceled();
		}
		m_originLocationEntityID = -1;
		m_sourceEntityID = -1;
		if (!m_isEnemyArrow)
		{
			RemoteActionHandler.Get().NotifyOpponentOfTargetEnd();
			PegCursor.Get().Show();
		}
		ActivateArrow(active: false);
		ShowDamageIndicator(show: false);
	}

	private void LoadArrowCallback(AssetReference assetRef, GameObject go, object callbackData)
	{
		m_arrow = go;
		ShowBullseye(show: false);
	}

	private void LoadQuestionCallback(AssetReference assetRef, GameObject go, object callbackData)
	{
		m_questionMark = go;
		ShowBullseye(show: false);
	}

	private void LoadLinkCallback(AssetReference assetRef, GameObject go, object callbackData)
	{
		StartCoroutine(OnLinkLoaded(go));
	}

	private void LoadDamageIndicatorCallback(AssetReference assetRef, GameObject go, object callbackData)
	{
		m_damageIndicator = go.GetComponent<TargetDamageIndicator>();
		if (m_damageIndicator == null)
		{
			Log.Gameplay.PrintError("LoadDamageIndicatorCallback - No TargetDamageIndicator script attached to '{0}'!", go.name);
		}
		else
		{
			m_damageIndicator.transform.eulerAngles = new Vector3(90f, 0f, 0f);
			m_damageIndicator.transform.localScale = new Vector3(DAMAGE_INDICATOR_SCALE, DAMAGE_INDICATOR_SCALE, DAMAGE_INDICATOR_SCALE);
		}
	}

	private void LoadHunterReticleCallback(AssetReference assetRef, GameObject go, object callbackData)
	{
		m_hunterReticle = go;
		m_hunterReticle.transform.parent = base.transform;
		m_hunterReticle.SetActive(value: false);
	}

	private IEnumerator OnLinkLoaded(GameObject linkActorObject)
	{
		while (m_arrow == null)
		{
			yield return null;
		}
		for (int i = 0; i < 14; i++)
		{
			GameObject gameObject = Object.Instantiate(linkActorObject);
			gameObject.transform.parent = m_arrow.transform;
			m_targetArrowLinks.Add(gameObject);
		}
		linkActorObject.transform.parent = m_arrow.transform;
		m_targetArrowLinks.Add(linkActorObject);
	}

	private int NumberOfRequiredLinks(float lengthOfArrow)
	{
		int num = (int)Mathf.Floor(lengthOfArrow / 1.2f) + 1;
		if (num == 1)
		{
			num = 0;
		}
		return num;
	}

	private GameObject GetAppropriateReticle()
	{
		switch (m_ReticleType)
		{
		case TARGET_RETICLE_TYPE.DefaultArrow:
			return m_arrow;
		case TARGET_RETICLE_TYPE.HunterReticle:
			return m_hunterReticle;
		case TARGET_RETICLE_TYPE.QuestionMark:
			return m_questionMark;
		default:
			Log.All.PrintError("Unknown Target Reticle Type!");
			return null;
		}
	}

	private Transform GetAppropriateArrowMeshTransform()
	{
		switch (m_ReticleType)
		{
		case TARGET_RETICLE_TYPE.DefaultArrow:
		case TARGET_RETICLE_TYPE.HunterReticle:
			return m_arrow.transform.Find("TargetArrow_ArrowMesh");
		case TARGET_RETICLE_TYPE.QuestionMark:
			return m_questionMark.transform.Find("TargetQuestionMark_QuestionMarkMesh");
		default:
			Log.All.PrintError("Unknown Target Reticle Type!");
			return null;
		}
	}

	private float GetStartingXRotationForArrowMesh()
	{
		switch (m_ReticleType)
		{
		case TARGET_RETICLE_TYPE.DefaultArrow:
		case TARGET_RETICLE_TYPE.HunterReticle:
			return 300f;
		case TARGET_RETICLE_TYPE.QuestionMark:
			return 0f;
		default:
			Log.All.PrintError("Unknown Target Reticle Type!");
			return 0f;
		}
	}

	private void UpdateTargetArrowLinks(float lengthOfArrow)
	{
		m_numActiveLinks = NumberOfRequiredLinks(lengthOfArrow);
		int count = m_targetArrowLinks.Count;
		Transform appropriateArrowMeshTransform = GetAppropriateArrowMeshTransform();
		if (m_numActiveLinks == 0)
		{
			appropriateArrowMeshTransform.localEulerAngles = new Vector3(GetStartingXRotationForArrowMesh(), 180f, 0f);
			for (int i = 0; i < count; i++)
			{
				SceneUtils.EnableRenderers(m_targetArrowLinks[i].gameObject, enable: false);
			}
			return;
		}
		float num = (0f - lengthOfArrow) / 2f;
		float num2 = -1.5f / (num * num);
		for (int j = 0; j < count; j++)
		{
			if (m_targetArrowLinks[j] == null)
			{
				continue;
			}
			if (j >= m_numActiveLinks)
			{
				SceneUtils.EnableRenderers(m_targetArrowLinks[j].gameObject, enable: false);
				continue;
			}
			float num3 = 0f - 1.2f * (float)(j + 1) + m_linkAnimationZOffset;
			float y = num2 * Mathf.Pow(num3 - num, 2f) + 1.5f;
			float num4 = Mathf.Atan(2f * num2 * (num3 - num));
			float x = 180f - num4 * 57.29578f;
			SceneUtils.EnableRenderers(m_targetArrowLinks[j].gameObject, enable: true);
			m_targetArrowLinks[j].transform.localPosition = new Vector3(0f, y, num3);
			m_targetArrowLinks[j].transform.eulerAngles = new Vector3(x, GetAppropriateReticle().transform.localEulerAngles.y, 0f);
			float alpha = 1f;
			if (j == 0)
			{
				if (num3 > -1.2f)
				{
					alpha = num3 / -1.2f;
					alpha = Mathf.Pow(alpha, 6f);
				}
			}
			else if (j == m_numActiveLinks - 1)
			{
				alpha = m_linkAnimationZOffset / 1.2f;
				alpha *= alpha;
			}
			SetLinkAlpha(m_targetArrowLinks[j], alpha);
		}
		float y2 = num2 * Mathf.Pow(appropriateArrowMeshTransform.localPosition.z - num, 2f) + 1.5f;
		float num5 = 0f;
		if (m_ReticleType != TARGET_RETICLE_TYPE.QuestionMark)
		{
			num5 = Mathf.Atan(2f * num2 * (appropriateArrowMeshTransform.localPosition.z - num)) * 57.29578f;
			if (num5 < 0f)
			{
				num5 += 360f;
			}
		}
		appropriateArrowMeshTransform.localPosition = new Vector3(0f, y2, appropriateArrowMeshTransform.localPosition.z);
		appropriateArrowMeshTransform.localEulerAngles = new Vector3(num5, 180f, 0f);
		m_linkAnimationZOffset += Time.deltaTime * 0.5f;
		if (m_linkAnimationZOffset > 1.2f)
		{
			m_linkAnimationZOffset -= 1.2f;
		}
	}

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

	private void UpdateDamageIndicator()
	{
		if (!(m_damageIndicator == null))
		{
			Vector3 position = Vector3.zero;
			if ((bool)SHOW_DAMAGE_INDICATOR_ON_ENTITY)
			{
				position = m_targetArrowOrigin;
				position.z += DAMAGE_INDICATOR_Z_OFFSET;
			}
			else
			{
				position = GetAppropriateReticle().transform.position;
				position.z += DAMAGE_INDICATOR_Z_OFFSET;
			}
			m_damageIndicator.transform.position = position;
		}
	}

	private void ShowDamageIndicator(bool show)
	{
		if ((bool)m_damageIndicator && m_damageIndicator.gameObject.activeInHierarchy)
		{
			m_damageIndicator.Show(show);
		}
	}

	private IEnumerator SetDamageText(string damageText)
	{
		while (m_damageIndicator == null)
		{
			yield return null;
		}
		m_damageIndicator.SetText(damageText);
		if (string.IsNullOrEmpty(damageText))
		{
			m_damageIndicator.Show(active: false);
		}
	}

	private void UpdateArrowOriginPosition()
	{
		Entity entity = GameState.Get().GetEntity(m_originLocationEntityID);
		if (entity == null)
		{
			Log.Gameplay.Print($"entity with ID {m_originLocationEntityID} does not exist... can't update arrow origin position!");
			DestroyCurrentArrow(isLocallyCanceled: false);
			return;
		}
		m_targetArrowOrigin = entity.GetCard().transform.position;
		if (m_useHandAsOrigin || entity.GetZone() == TAG_ZONE.DECK)
		{
			if (m_isEnemyArrow)
			{
				m_targetArrowOrigin = InputManager.Get().GetEnemyHand().transform.position;
			}
			else
			{
				m_targetArrowOrigin = InputManager.Get().GetFriendlyHand().transform.position;
			}
		}
		if (entity.IsHero() && !m_isEnemyArrow)
		{
			m_targetArrowOrigin.z += 1f;
		}
	}

	private void ActivateArrow(bool active)
	{
		m_isActive = active;
		SceneUtils.EnableRenderers(m_arrow.gameObject, enable: false);
		m_hunterReticle.SetActive(value: false);
		SceneUtils.EnableRenderers(m_questionMark.gameObject, enable: false);
		if (active)
		{
			if (m_ReticleType == TARGET_RETICLE_TYPE.DefaultArrow)
			{
				SceneUtils.EnableRenderers(m_arrow.gameObject, active && m_showArrow);
			}
			else if (m_ReticleType == TARGET_RETICLE_TYPE.HunterReticle)
			{
				m_hunterReticle.SetActive(active && m_showArrow);
			}
			else if (m_ReticleType == TARGET_RETICLE_TYPE.QuestionMark)
			{
				SceneUtils.EnableRenderers(m_questionMark.gameObject, active && m_showArrow);
			}
			else
			{
				Debug.LogError("Unknown Target Reticle Type!");
			}
		}
	}

	public void ShowArrow(bool show)
	{
		m_showArrow = show;
		SceneUtils.EnableRenderers(m_arrow.gameObject, enable: false);
		m_hunterReticle.SetActive(value: false);
		SceneUtils.EnableRenderers(m_questionMark.gameObject, enable: false);
		if (show)
		{
			if (m_ReticleType == TARGET_RETICLE_TYPE.DefaultArrow)
			{
				SceneUtils.EnableRenderers(m_arrow.gameObject, show);
			}
			else if (m_ReticleType == TARGET_RETICLE_TYPE.HunterReticle)
			{
				m_hunterReticle.SetActive(show);
			}
			else if (m_ReticleType == TARGET_RETICLE_TYPE.QuestionMark)
			{
				SceneUtils.EnableRenderers(m_questionMark.gameObject, show);
			}
			else
			{
				Debug.LogError("Unknown Target Reticle Type!");
			}
		}
	}
}
