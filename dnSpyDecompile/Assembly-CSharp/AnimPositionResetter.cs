using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000A0D RID: 2573
public class AnimPositionResetter : MonoBehaviour
{
	// Token: 0x06008B2A RID: 35626 RVA: 0x002C7C7E File Offset: 0x002C5E7E
	private void Awake()
	{
		this.m_initialPosition = base.transform.position;
	}

	// Token: 0x06008B2B RID: 35627 RVA: 0x002C7C91 File Offset: 0x002C5E91
	public static AnimPositionResetter OnAnimStarted(GameObject go, float animTime)
	{
		if (animTime <= 0f)
		{
			return null;
		}
		AnimPositionResetter animPositionResetter = AnimPositionResetter.RegisterResetter(go);
		animPositionResetter.OnAnimStarted(animTime);
		return animPositionResetter;
	}

	// Token: 0x06008B2C RID: 35628 RVA: 0x002C7CAA File Offset: 0x002C5EAA
	public Vector3 GetInitialPosition()
	{
		return this.m_initialPosition;
	}

	// Token: 0x06008B2D RID: 35629 RVA: 0x002C7CB2 File Offset: 0x002C5EB2
	public float GetEndTimestamp()
	{
		return this.m_endTimestamp;
	}

	// Token: 0x06008B2E RID: 35630 RVA: 0x002C7CBA File Offset: 0x002C5EBA
	public float GetDelay()
	{
		return this.m_delay;
	}

	// Token: 0x06008B2F RID: 35631 RVA: 0x002C7CC4 File Offset: 0x002C5EC4
	private static AnimPositionResetter RegisterResetter(GameObject go)
	{
		if (go == null)
		{
			return null;
		}
		AnimPositionResetter component = go.GetComponent<AnimPositionResetter>();
		if (component != null)
		{
			return component;
		}
		return go.AddComponent<AnimPositionResetter>();
	}

	// Token: 0x06008B30 RID: 35632 RVA: 0x002C7CF4 File Offset: 0x002C5EF4
	private void OnAnimStarted(float animTime)
	{
		float num = Time.realtimeSinceStartup + animTime;
		float num2 = num - this.m_endTimestamp;
		if (num2 <= 0f)
		{
			return;
		}
		this.m_delay = Mathf.Min(num2, animTime);
		this.m_endTimestamp = num;
		base.StopCoroutine("ResetPosition");
		base.StartCoroutine("ResetPosition");
	}

	// Token: 0x06008B31 RID: 35633 RVA: 0x002C7D46 File Offset: 0x002C5F46
	private IEnumerator ResetPosition()
	{
		yield return new WaitForSeconds(this.m_delay);
		base.transform.position = this.m_initialPosition;
		UnityEngine.Object.Destroy(this);
		yield break;
	}

	// Token: 0x040073A5 RID: 29605
	private Vector3 m_initialPosition;

	// Token: 0x040073A6 RID: 29606
	private float m_endTimestamp;

	// Token: 0x040073A7 RID: 29607
	private float m_delay;
}
