using System;
using UnityEngine;

// Token: 0x02000AF8 RID: 2808
[CustomEditClass]
[RequireComponent(typeof(BoxCollider), typeof(PegUIElement))]
public class UIBScrollableTrack : MonoBehaviour
{
	// Token: 0x060095A3 RID: 38307 RVA: 0x0030787F File Offset: 0x00305A7F
	private void Awake()
	{
		if (this.m_parentScrollbar == null)
		{
			Debug.LogError("Parent scroll bar not set!");
			return;
		}
		this.m_parentScrollbar.AddEnableScrollListener(new UIBScrollable.EnableScroll(this.OnScrollEnabled));
	}

	// Token: 0x060095A4 RID: 38308 RVA: 0x003078B4 File Offset: 0x00305AB4
	private void OnEnable()
	{
		if (this.m_scrollTrack != null)
		{
			this.m_lastEnabled = this.m_parentScrollbar.IsEnabled();
			this.m_scrollTrack.transform.localEulerAngles = (this.m_lastEnabled ? this.m_showRotation : this.m_hideRotation);
		}
	}

	// Token: 0x060095A5 RID: 38309 RVA: 0x00307908 File Offset: 0x00305B08
	private void OnScrollEnabled(bool enabled)
	{
		if (this.m_scrollTrack == null || !this.m_scrollTrack.activeInHierarchy || this.m_lastEnabled == enabled)
		{
			return;
		}
		this.m_lastEnabled = enabled;
		Vector3 localEulerAngles;
		Vector3 vector;
		if (enabled)
		{
			localEulerAngles = this.m_hideRotation;
			vector = this.m_showRotation;
		}
		else
		{
			localEulerAngles = this.m_showRotation;
			vector = this.m_hideRotation;
		}
		this.m_scrollTrack.transform.localEulerAngles = localEulerAngles;
		iTween.StopByName(this.m_scrollTrack, "rotate");
		iTween.RotateTo(this.m_scrollTrack, iTween.Hash(new object[]
		{
			"rotation",
			vector,
			"islocal",
			true,
			"time",
			this.m_rotateAnimationTime,
			"name",
			"rotate"
		}));
	}

	// Token: 0x04007D64 RID: 32100
	public UIBScrollable m_parentScrollbar;

	// Token: 0x04007D65 RID: 32101
	public GameObject m_scrollTrack;

	// Token: 0x04007D66 RID: 32102
	public Vector3 m_showRotation = Vector3.zero;

	// Token: 0x04007D67 RID: 32103
	public Vector3 m_hideRotation = new Vector3(0f, 0f, 180f);

	// Token: 0x04007D68 RID: 32104
	public float m_rotateAnimationTime = 0.5f;

	// Token: 0x04007D69 RID: 32105
	private bool m_lastEnabled;
}
