using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200099F RID: 2463
public class AnimationUtil : MonoBehaviour
{
	// Token: 0x06008674 RID: 34420 RVA: 0x002B6A9C File Offset: 0x002B4C9C
	public static void ShowWithPunch(GameObject go, Vector3 startScale, Vector3 punchScale, Vector3 afterPunchScale, string callbackName = "", bool noFade = false, GameObject callbackGO = null, object callbackData = null, AnimationUtil.DelOnShownWithPunch onShowPunchCallback = null)
	{
		if (!noFade)
		{
			iTween.FadeTo(go, 1f, 0.25f);
		}
		go.transform.localScale = startScale;
		iTween.ScaleTo(go, iTween.Hash(new object[]
		{
			"scale",
			punchScale,
			"time",
			0.25f
		}));
		iTween.MoveTo(go, iTween.Hash(new object[]
		{
			"position",
			go.transform.position + new Vector3(0.02f, 0.02f, 0.02f),
			"time",
			1.5f
		}));
		AnimationUtil.PunchData callbackData2 = new AnimationUtil.PunchData
		{
			m_gameObject = go,
			m_scale = afterPunchScale,
			m_callbackName = callbackName,
			m_callbackGameObject = callbackGO,
			m_callbackData = callbackData,
			m_onShowPunchCallback = onShowPunchCallback
		};
		go.GetComponent<MonoBehaviour>().StartCoroutine(AnimationUtil.ShowPunchRoutine(callbackData2));
	}

	// Token: 0x06008675 RID: 34421 RVA: 0x002B6BA0 File Offset: 0x002B4DA0
	private static IEnumerator ShowPunchRoutine(AnimationUtil.PunchData callbackData)
	{
		yield return new WaitForSeconds(0.25f);
		AnimationUtil.ShowPunch(callbackData.m_gameObject, callbackData.m_scale, callbackData.m_callbackName, callbackData.m_callbackGameObject, callbackData.m_callbackData);
		if (callbackData.m_onShowPunchCallback != null)
		{
			callbackData.m_onShowPunchCallback(callbackData.m_callbackData);
		}
		yield break;
	}

	// Token: 0x06008676 RID: 34422 RVA: 0x002B6BB0 File Offset: 0x002B4DB0
	public static void ShowPunch(GameObject go, Vector3 scale, string callbackName = "", GameObject callbackGO = null, object callbackData = null)
	{
		if (string.IsNullOrEmpty(callbackName))
		{
			iTween.ScaleTo(go, scale, 0.15f);
			return;
		}
		if (callbackGO == null)
		{
			callbackGO = go;
		}
		if (callbackData == null)
		{
			callbackData = new object();
		}
		Hashtable args = iTween.Hash(new object[]
		{
			"scale",
			scale,
			"time",
			0.15f,
			"oncomplete",
			callbackName,
			"oncompletetarget",
			callbackGO,
			"oncompleteparams",
			callbackData
		});
		iTween.ScaleTo(go, args);
	}

	// Token: 0x06008677 RID: 34423 RVA: 0x002B6C4C File Offset: 0x002B4E4C
	public static void GrowThenDrift(GameObject go, Vector3 origin, Vector3 driftOffset)
	{
		iTween.ScaleFrom(go, iTween.Hash(new object[]
		{
			"scale",
			Vector3.one * 0.05f,
			"time",
			0.15f,
			"easeType",
			iTween.EaseType.easeOutQuart
		}));
		iTween.MoveFrom(go, iTween.Hash(new object[]
		{
			"position",
			origin,
			"time",
			0.15f,
			"easeType",
			iTween.EaseType.easeOutQuart
		}));
		go.GetComponent<MonoBehaviour>().StartCoroutine(AnimationUtil.DriftAfterTween(go, 0.15f, driftOffset));
	}

	// Token: 0x06008678 RID: 34424 RVA: 0x002B6D10 File Offset: 0x002B4F10
	public static void GrowThenDrift(GameObject go, Vector3 origin, float driftScale)
	{
		Vector3 a = PlatformSettings.IsTablet ? new Vector3(0f, 0.1f, 0.1f) : new Vector3(0.1f, 0.1f, 0.1f);
		Vector3 b = TransformUtil.ComputeWorldScale(go.transform.parent);
		Vector3 driftOffset = Vector3.Scale(a * driftScale, b);
		AnimationUtil.GrowThenDrift(go, origin, driftOffset);
	}

	// Token: 0x06008679 RID: 34425 RVA: 0x002B6D74 File Offset: 0x002B4F74
	private static IEnumerator DriftAfterTween(GameObject go, float delayTime, Vector3 driftOffset)
	{
		yield return new WaitForSeconds(delayTime);
		AnimationUtil.DriftObject(go, driftOffset);
		yield break;
	}

	// Token: 0x0600867A RID: 34426 RVA: 0x002B6D94 File Offset: 0x002B4F94
	public static void FloatyPosition(GameObject go, Vector3 startPos, float localRadius, float loopTime)
	{
		Vector3[] array = new Vector3[]
		{
			startPos,
			startPos + new Vector3(localRadius, 0f, localRadius),
			startPos + new Vector3(localRadius * 2f, 0f, 0f),
			startPos + new Vector3(localRadius, 0f, -localRadius),
			startPos + Vector3.zero
		};
		iTween.StopByName("DriftingTween");
		iTween.MoveTo(go, iTween.Hash(new object[]
		{
			"name",
			"DriftingTween",
			"path",
			array,
			"time",
			loopTime,
			"islocal",
			true,
			"easetype",
			iTween.EaseType.linear,
			"looptype",
			iTween.LoopType.loop,
			"movetopath",
			false
		}));
	}

	// Token: 0x0600867B RID: 34427 RVA: 0x002B6EAB File Offset: 0x002B50AB
	public static void FloatyPosition(GameObject go, float radius, float loopTime)
	{
		AnimationUtil.FloatyPosition(go, go.transform.localPosition, radius, loopTime);
	}

	// Token: 0x0600867C RID: 34428 RVA: 0x002B6EC0 File Offset: 0x002B50C0
	public static void ScaleFade(GameObject go, Vector3 scale)
	{
		AnimationUtil.ScaleFade(go, scale, null);
	}

	// Token: 0x0600867D RID: 34429 RVA: 0x002B6ECC File Offset: 0x002B50CC
	public static void ScaleFade(GameObject go, Vector3 scale, string callbackName)
	{
		iTween.FadeTo(go, 0f, 0.25f);
		Hashtable args;
		if (string.IsNullOrEmpty(callbackName))
		{
			args = iTween.Hash(new object[]
			{
				"scale",
				scale,
				"time",
				0.25f
			});
		}
		else
		{
			args = iTween.Hash(new object[]
			{
				"scale",
				scale,
				"time",
				0.25f,
				"oncomplete",
				callbackName,
				"oncompletetarget",
				go
			});
		}
		iTween.ScaleTo(go, args);
	}

	// Token: 0x0600867E RID: 34430 RVA: 0x002B6F78 File Offset: 0x002B5178
	public static int GetLayerIndexFromName(Animator animator, string layerName)
	{
		if (layerName == null)
		{
			return -1;
		}
		layerName = layerName.Trim();
		for (int i = 0; i < animator.layerCount; i++)
		{
			string text = animator.GetLayerName(i);
			if (text != null)
			{
				text = text.Trim();
				if (text.Equals(layerName, StringComparison.OrdinalIgnoreCase))
				{
					return i;
				}
			}
		}
		return -1;
	}

	// Token: 0x0600867F RID: 34431 RVA: 0x002B6FC4 File Offset: 0x002B51C4
	public static void DriftObject(GameObject go, Vector3 driftOffset)
	{
		iTween.StopByName(go, "DRIFT_MOVE_OBJECT_ITWEEN");
		iTween.MoveBy(go, iTween.Hash(new object[]
		{
			"amount",
			driftOffset,
			"time",
			10f,
			"name",
			"DRIFT_MOVE_OBJECT_ITWEEN",
			"easeType",
			iTween.EaseType.easeOutQuart
		}));
	}

	// Token: 0x06008680 RID: 34432 RVA: 0x002B7034 File Offset: 0x002B5234
	public static void FadeTexture(MeshRenderer mesh, float fromAlpha, float toAlpha, float fadeTime, float delay, AnimationUtil.DelOnFade onCompleteCallback = null)
	{
		iTween.StopByName(mesh.gameObject, "FADE_TEXTURE");
		Material logoMaterial = mesh.GetMaterial();
		Color currentColor = logoMaterial.GetColor("_Color");
		currentColor.a = fromAlpha;
		logoMaterial.SetColor("_Color", currentColor);
		Hashtable hashtable = iTween.Hash(new object[]
		{
			"from",
			fromAlpha,
			"to",
			toAlpha,
			"time",
			fadeTime,
			"onupdate",
			new Action<object>(delegate(object val)
			{
				currentColor.a = (float)val;
				logoMaterial.SetColor("_Color", currentColor);
			}),
			"name",
			"FADE_TEXTURE"
		});
		if (delay > 0f)
		{
			hashtable.Add("delay", delay);
		}
		if (onCompleteCallback != null)
		{
			hashtable.Add("oncomplete", new Action<object>(delegate(object o)
			{
				onCompleteCallback();
			}));
		}
		iTween.ValueTo(mesh.gameObject, hashtable);
	}

	// Token: 0x06008681 RID: 34433 RVA: 0x002B7151 File Offset: 0x002B5351
	public static void DelayedActivate(GameObject go, float time, bool activate)
	{
		go.GetComponent<MonoBehaviour>().StartCoroutine(AnimationUtil.DelayedActivation(go, time, activate));
	}

	// Token: 0x06008682 RID: 34434 RVA: 0x002B7167 File Offset: 0x002B5367
	private static IEnumerator DelayedActivation(GameObject go, float time, bool activate)
	{
		yield return new WaitForSeconds(time);
		go.SetActive(activate);
		yield break;
	}

	// Token: 0x02002655 RID: 9813
	// (Invoke) Token: 0x060136C6 RID: 79558
	public delegate void DelOnShownWithPunch(object callbackData);

	// Token: 0x02002656 RID: 9814
	// (Invoke) Token: 0x060136CA RID: 79562
	public delegate void DelOnFade();

	// Token: 0x02002657 RID: 9815
	private class PunchData
	{
		// Token: 0x0400F058 RID: 61528
		public GameObject m_gameObject;

		// Token: 0x0400F059 RID: 61529
		public Vector3 m_scale;

		// Token: 0x0400F05A RID: 61530
		public string m_callbackName;

		// Token: 0x0400F05B RID: 61531
		public GameObject m_callbackGameObject;

		// Token: 0x0400F05C RID: 61532
		public object m_callbackData;

		// Token: 0x0400F05D RID: 61533
		public AnimationUtil.DelOnShownWithPunch m_onShowPunchCallback;
	}
}
