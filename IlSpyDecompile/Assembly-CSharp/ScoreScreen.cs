using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CustomEditClass]
public class ScoreScreen : MonoBehaviour
{
	public GameObject m_BackgroundCenter;

	public NestedPrefab m_ScoreBoxLeft;

	public NestedPrefab m_ScoreBoxCenter;

	public NestedPrefab m_ScoreBoxRight;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_ShowSoundPrefab;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_HideSoundPrefab;

	public UberText m_FooterText;

	public const float SHOW_SEC = 0.65f;

	public const float HIDE_SEC = 0.25f;

	private const float SHOW_INTERMED_SEC = 0.5f;

	private const float SHOW_FINAL_SEC = 0.15f;

	private const float DRIFT_CYCLE_SEC = 10f;

	private static readonly Vector3 START_SCALE = new Vector3(0.001f, 0.001f, 0.001f);

	private static ScoreScreen s_instance;

	private Vector3 m_initialScale;

	private void Awake()
	{
		s_instance = this;
		Init();
	}

	private void OnDestroy()
	{
		s_instance = null;
	}

	public static ScoreScreen Get()
	{
		return s_instance;
	}

	public void Show()
	{
		base.gameObject.SetActive(value: true);
		Vector3 vector = 1.03f * m_initialScale;
		Vector3 initialScale = m_initialScale;
		base.transform.localScale = START_SCALE;
		Hashtable args = iTween.Hash("scale", vector, "time", 0.5f);
		iTween.ScaleTo(base.gameObject, args);
		Action<object> action = delegate
		{
			Drift();
		};
		Hashtable args2 = iTween.Hash("scale", initialScale, "delay", 0.5f, "time", 0.15f, "oncomplete", action);
		iTween.ScaleTo(base.gameObject, args2);
		if (!string.IsNullOrEmpty(m_ShowSoundPrefab))
		{
			SoundManager.Get().LoadAndPlay(m_ShowSoundPrefab);
		}
	}

	public void Hide()
	{
		Action<object> action = delegate
		{
			UnityEngine.Object.Destroy(base.gameObject);
		};
		Hashtable args = iTween.Hash("scale", START_SCALE, "time", 0.25f, "oncomplete", action);
		iTween.ScaleTo(base.gameObject, args);
		if (!string.IsNullOrEmpty(m_HideSoundPrefab))
		{
			SoundManager.Get().LoadAndPlay(m_HideSoundPrefab);
		}
	}

	private void Init()
	{
		m_initialScale = base.transform.localScale;
		UpdateScoreBoxes();
		LayoutScoreBoxes();
		UpdateFooterText();
		base.gameObject.SetActive(value: false);
	}

	private void UpdateScoreBoxes()
	{
		UpdateScoreBox(m_ScoreBoxLeft, GAME_TAG.SCORE_LABELID_1, GAME_TAG.SCORE_VALUE_1);
		UpdateScoreBox(m_ScoreBoxCenter, GAME_TAG.SCORE_LABELID_2, GAME_TAG.SCORE_VALUE_2);
		UpdateScoreBox(m_ScoreBoxRight, GAME_TAG.SCORE_LABELID_3, GAME_TAG.SCORE_VALUE_3);
	}

	private void UpdateFooterText()
	{
		int labelId = 0;
		int value = -1;
		GetLabelAndScore(GAME_TAG.SCORE_FOOTERID, GAME_TAG.SCORE_FOOTERID, out labelId, out value);
		if (labelId <= 0)
		{
			m_FooterText.gameObject.SetActive(value: false);
			return;
		}
		ScoreLabelDbfRecord record = GameDbf.ScoreLabel.GetRecord(labelId);
		if (record == null)
		{
			Error.AddDevWarning("Error", "ScoreScreen.UpdateFooterText() - There is no ScoreLabel record for id {0}.", labelId);
		}
		else
		{
			m_FooterText.Text = record.Text.GetString();
			m_FooterText.gameObject.SetActive(value: true);
		}
	}

	private void UpdateScoreBox(NestedPrefab scoreBoxPrefab, GAME_TAG labelTag, GAME_TAG valueTag)
	{
		ScoreBox component = scoreBoxPrefab.PrefabGameObject(instantiateIfNeeded: true).GetComponent<ScoreBox>();
		int labelId = 0;
		int value = 0;
		GetLabelAndScore(labelTag, valueTag, out labelId, out value);
		bool flag = labelId != 0;
		scoreBoxPrefab.gameObject.SetActive(flag);
		if (flag)
		{
			ScoreLabelDbfRecord record = GameDbf.ScoreLabel.GetRecord(labelId);
			if (record == null)
			{
				Error.AddDevWarning("Error", "ScoreScreen.UpdateScoreBox() - There is no ScoreLabel record for id {0}.", labelId);
			}
			else
			{
				component.m_Label.Text = record.Text.GetString();
			}
			component.m_Value.Text = value.ToString();
		}
	}

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

	private void LayoutScoreBoxes()
	{
		NestedPrefab[] array = new NestedPrefab[3] { m_ScoreBoxLeft, m_ScoreBoxCenter, m_ScoreBoxRight };
		int num = Array.FindIndex(array, 0, (NestedPrefab scoreBox) => scoreBox.gameObject.activeInHierarchy);
		int num2 = Array.FindIndex(array, num + 1, (NestedPrefab scoreBox) => scoreBox.gameObject.activeInHierarchy);
		int num3 = Array.FindIndex(array, num2 + 1, (NestedPrefab scoreBox) => scoreBox.gameObject.activeInHierarchy);
		NestedPrefab nestedPrefab = array[num];
		if (num2 < 0)
		{
			nestedPrefab.transform.position = m_ScoreBoxCenter.transform.position;
		}
		else if (num3 < 0)
		{
			NestedPrefab obj = array[num2];
			Vector3 position = 0.5f * (m_ScoreBoxLeft.transform.position + m_ScoreBoxCenter.transform.position);
			Vector3 position2 = 0.5f * (m_ScoreBoxCenter.transform.position + m_ScoreBoxRight.transform.position);
			nestedPrefab.transform.position = position;
			obj.transform.position = position2;
		}
	}

	private void Drift()
	{
		Vector3 position = base.transform.position;
		float x = m_BackgroundCenter.GetComponent<Renderer>().bounds.size.x;
		Vector3 vector = 0.02f * x * base.transform.up;
		Vector3 item = position + vector;
		Vector3 item2 = position - vector;
		List<Vector3> list = new List<Vector3>();
		list.Add(position);
		list.Add(item);
		list.Add(position);
		list.Add(item2);
		list.Add(position);
		Hashtable args = iTween.Hash("path", list.ToArray(), "time", 10f, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.loop);
		iTween.MoveTo(base.gameObject, args);
	}
}
