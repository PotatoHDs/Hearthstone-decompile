using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002D1 RID: 721
[CustomEditClass]
public class ScoreScreen : MonoBehaviour
{
	// Token: 0x060025E8 RID: 9704 RVA: 0x000BE647 File Offset: 0x000BC847
	private void Awake()
	{
		ScoreScreen.s_instance = this;
		this.Init();
	}

	// Token: 0x060025E9 RID: 9705 RVA: 0x000BE655 File Offset: 0x000BC855
	private void OnDestroy()
	{
		ScoreScreen.s_instance = null;
	}

	// Token: 0x060025EA RID: 9706 RVA: 0x000BE65D File Offset: 0x000BC85D
	public static ScoreScreen Get()
	{
		return ScoreScreen.s_instance;
	}

	// Token: 0x060025EB RID: 9707 RVA: 0x000BE664 File Offset: 0x000BC864
	public void Show()
	{
		base.gameObject.SetActive(true);
		Vector3 vector = 1.03f * this.m_initialScale;
		Vector3 initialScale = this.m_initialScale;
		base.transform.localScale = ScoreScreen.START_SCALE;
		Hashtable args = iTween.Hash(new object[]
		{
			"scale",
			vector,
			"time",
			0.5f
		});
		iTween.ScaleTo(base.gameObject, args);
		Action<object> action = delegate(object param)
		{
			this.Drift();
		};
		Hashtable args2 = iTween.Hash(new object[]
		{
			"scale",
			initialScale,
			"delay",
			0.5f,
			"time",
			0.15f,
			"oncomplete",
			action
		});
		iTween.ScaleTo(base.gameObject, args2);
		if (!string.IsNullOrEmpty(this.m_ShowSoundPrefab))
		{
			SoundManager.Get().LoadAndPlay(this.m_ShowSoundPrefab);
		}
	}

	// Token: 0x060025EC RID: 9708 RVA: 0x000BE774 File Offset: 0x000BC974
	public void Hide()
	{
		Action<object> action = delegate(object param)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		};
		Hashtable args = iTween.Hash(new object[]
		{
			"scale",
			ScoreScreen.START_SCALE,
			"time",
			0.25f,
			"oncomplete",
			action
		});
		iTween.ScaleTo(base.gameObject, args);
		if (!string.IsNullOrEmpty(this.m_HideSoundPrefab))
		{
			SoundManager.Get().LoadAndPlay(this.m_HideSoundPrefab);
		}
	}

	// Token: 0x060025ED RID: 9709 RVA: 0x000BE7FE File Offset: 0x000BC9FE
	private void Init()
	{
		this.m_initialScale = base.transform.localScale;
		this.UpdateScoreBoxes();
		this.LayoutScoreBoxes();
		this.UpdateFooterText();
		base.gameObject.SetActive(false);
	}

	// Token: 0x060025EE RID: 9710 RVA: 0x000BE830 File Offset: 0x000BCA30
	private void UpdateScoreBoxes()
	{
		this.UpdateScoreBox(this.m_ScoreBoxLeft, GAME_TAG.SCORE_LABELID_1, GAME_TAG.SCORE_VALUE_1);
		this.UpdateScoreBox(this.m_ScoreBoxCenter, GAME_TAG.SCORE_LABELID_2, GAME_TAG.SCORE_VALUE_2);
		this.UpdateScoreBox(this.m_ScoreBoxRight, GAME_TAG.SCORE_LABELID_3, GAME_TAG.SCORE_VALUE_3);
	}

	// Token: 0x060025EF RID: 9711 RVA: 0x000BE880 File Offset: 0x000BCA80
	private void UpdateFooterText()
	{
		int num = 0;
		int num2 = -1;
		this.GetLabelAndScore(GAME_TAG.SCORE_FOOTERID, GAME_TAG.SCORE_FOOTERID, out num, out num2);
		if (num <= 0)
		{
			this.m_FooterText.gameObject.SetActive(false);
			return;
		}
		ScoreLabelDbfRecord record = GameDbf.ScoreLabel.GetRecord(num);
		if (record == null)
		{
			Error.AddDevWarning("Error", "ScoreScreen.UpdateFooterText() - There is no ScoreLabel record for id {0}.", new object[]
			{
				num
			});
			return;
		}
		this.m_FooterText.Text = record.Text.GetString(true);
		this.m_FooterText.gameObject.SetActive(true);
	}

	// Token: 0x060025F0 RID: 9712 RVA: 0x000BE914 File Offset: 0x000BCB14
	private void UpdateScoreBox(NestedPrefab scoreBoxPrefab, GAME_TAG labelTag, GAME_TAG valueTag)
	{
		ScoreBox component = scoreBoxPrefab.PrefabGameObject(true).GetComponent<ScoreBox>();
		int num = 0;
		int num2 = 0;
		this.GetLabelAndScore(labelTag, valueTag, out num, out num2);
		bool flag = num != 0;
		scoreBoxPrefab.gameObject.SetActive(flag);
		if (!flag)
		{
			return;
		}
		ScoreLabelDbfRecord record = GameDbf.ScoreLabel.GetRecord(num);
		if (record == null)
		{
			Error.AddDevWarning("Error", "ScoreScreen.UpdateScoreBox() - There is no ScoreLabel record for id {0}.", new object[]
			{
				num
			});
		}
		else
		{
			component.m_Label.Text = record.Text.GetString(true);
		}
		component.m_Value.Text = num2.ToString();
	}

	// Token: 0x060025F1 RID: 9713 RVA: 0x000BE9B0 File Offset: 0x000BCBB0
	private void GetLabelAndScore(GAME_TAG labelTag, GAME_TAG valueTag, out int labelId, out int value)
	{
		Player friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
		labelId = friendlySidePlayer.GetTag(labelTag);
		if (labelId != 0)
		{
			value = friendlySidePlayer.GetTag(valueTag);
			return;
		}
		GameEntity gameEntity = GameState.Get().GetGameEntity();
		labelId = gameEntity.GetTag(labelTag);
		value = gameEntity.GetTag(valueTag);
	}

	// Token: 0x060025F2 RID: 9714 RVA: 0x000BEA00 File Offset: 0x000BCC00
	private void LayoutScoreBoxes()
	{
		NestedPrefab[] array = new NestedPrefab[]
		{
			this.m_ScoreBoxLeft,
			this.m_ScoreBoxCenter,
			this.m_ScoreBoxRight
		};
		int num = Array.FindIndex<NestedPrefab>(array, 0, (NestedPrefab scoreBox) => scoreBox.gameObject.activeInHierarchy);
		int num2 = Array.FindIndex<NestedPrefab>(array, num + 1, (NestedPrefab scoreBox) => scoreBox.gameObject.activeInHierarchy);
		int num3 = Array.FindIndex<NestedPrefab>(array, num2 + 1, (NestedPrefab scoreBox) => scoreBox.gameObject.activeInHierarchy);
		NestedPrefab nestedPrefab = array[num];
		if (num2 < 0)
		{
			nestedPrefab.transform.position = this.m_ScoreBoxCenter.transform.position;
			return;
		}
		if (num3 < 0)
		{
			Component component = array[num2];
			Vector3 position = 0.5f * (this.m_ScoreBoxLeft.transform.position + this.m_ScoreBoxCenter.transform.position);
			Vector3 position2 = 0.5f * (this.m_ScoreBoxCenter.transform.position + this.m_ScoreBoxRight.transform.position);
			nestedPrefab.transform.position = position;
			component.transform.position = position2;
		}
	}

	// Token: 0x060025F3 RID: 9715 RVA: 0x000BEB54 File Offset: 0x000BCD54
	private void Drift()
	{
		Vector3 position = base.transform.position;
		float x = this.m_BackgroundCenter.GetComponent<Renderer>().bounds.size.x;
		Vector3 b = 0.02f * x * base.transform.up;
		Vector3 item = position + b;
		Vector3 item2 = position - b;
		Hashtable args = iTween.Hash(new object[]
		{
			"path",
			new List<Vector3>
			{
				position,
				item,
				position,
				item2,
				position
			}.ToArray(),
			"time",
			10f,
			"easetype",
			iTween.EaseType.linear,
			"looptype",
			iTween.LoopType.loop
		});
		iTween.MoveTo(base.gameObject, args);
	}

	// Token: 0x0400153F RID: 5439
	public GameObject m_BackgroundCenter;

	// Token: 0x04001540 RID: 5440
	public NestedPrefab m_ScoreBoxLeft;

	// Token: 0x04001541 RID: 5441
	public NestedPrefab m_ScoreBoxCenter;

	// Token: 0x04001542 RID: 5442
	public NestedPrefab m_ScoreBoxRight;

	// Token: 0x04001543 RID: 5443
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_ShowSoundPrefab;

	// Token: 0x04001544 RID: 5444
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_HideSoundPrefab;

	// Token: 0x04001545 RID: 5445
	public UberText m_FooterText;

	// Token: 0x04001546 RID: 5446
	public const float SHOW_SEC = 0.65f;

	// Token: 0x04001547 RID: 5447
	public const float HIDE_SEC = 0.25f;

	// Token: 0x04001548 RID: 5448
	private const float SHOW_INTERMED_SEC = 0.5f;

	// Token: 0x04001549 RID: 5449
	private const float SHOW_FINAL_SEC = 0.15f;

	// Token: 0x0400154A RID: 5450
	private const float DRIFT_CYCLE_SEC = 10f;

	// Token: 0x0400154B RID: 5451
	private static readonly Vector3 START_SCALE = new Vector3(0.001f, 0.001f, 0.001f);

	// Token: 0x0400154C RID: 5452
	private static ScoreScreen s_instance;

	// Token: 0x0400154D RID: 5453
	private Vector3 m_initialScale;
}
