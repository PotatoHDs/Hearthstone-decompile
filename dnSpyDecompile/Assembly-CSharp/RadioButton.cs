using System;
using UnityEngine;

// Token: 0x02000ADE RID: 2782
public class RadioButton : PegUIElement
{
	// Token: 0x06009445 RID: 37957 RVA: 0x00301568 File Offset: 0x002FF768
	protected override void Awake()
	{
		base.Awake();
		this.m_hoverGlow.SetActive(false);
		this.m_selectedGlow.SetActive(false);
		SoundManager.Get().Load("tiny_button_press_2.prefab:dab8dd96f82865041bbf96a32e47642e");
		SoundManager.Get().Load("tiny_button_mouseover_2.prefab:ba1a1effe29265246b1cb3d833c8ac78");
	}

	// Token: 0x06009446 RID: 37958 RVA: 0x003015BD File Offset: 0x002FF7BD
	public void SetButtonID(int id)
	{
		this.m_id = id;
	}

	// Token: 0x06009447 RID: 37959 RVA: 0x003015C6 File Offset: 0x002FF7C6
	public int GetButtonID()
	{
		return this.m_id;
	}

	// Token: 0x06009448 RID: 37960 RVA: 0x003015CE File Offset: 0x002FF7CE
	public void SetUserData(object userData)
	{
		this.m_userData = userData;
	}

	// Token: 0x06009449 RID: 37961 RVA: 0x003015D7 File Offset: 0x002FF7D7
	public object GetUserData()
	{
		return this.m_userData;
	}

	// Token: 0x0600944A RID: 37962 RVA: 0x003015DF File Offset: 0x002FF7DF
	public void SetSelected(bool selected)
	{
		this.m_selectedGlow.SetActive(selected);
	}

	// Token: 0x0600944B RID: 37963 RVA: 0x003015ED File Offset: 0x002FF7ED
	public bool IsSelected()
	{
		return this.m_selectedGlow.activeSelf;
	}

	// Token: 0x0600944C RID: 37964 RVA: 0x003015FA File Offset: 0x002FF7FA
	protected override void OnOver(PegUIElement.InteractionState oldState)
	{
		SoundManager.Get().LoadAndPlay("tiny_button_mouseover_2.prefab:ba1a1effe29265246b1cb3d833c8ac78");
		this.m_hoverGlow.SetActive(true);
	}

	// Token: 0x0600944D RID: 37965 RVA: 0x0030161C File Offset: 0x002FF81C
	protected override void OnOut(PegUIElement.InteractionState oldState)
	{
		this.m_hoverGlow.SetActive(false);
	}

	// Token: 0x0600944E RID: 37966 RVA: 0x0030162A File Offset: 0x002FF82A
	protected override void OnRelease()
	{
		base.OnRelease();
		SoundManager.Get().LoadAndPlay("tiny_button_press_2.prefab:dab8dd96f82865041bbf96a32e47642e");
	}

	// Token: 0x0600944F RID: 37967 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected override void OnDoubleClick()
	{
	}

	// Token: 0x04007C48 RID: 31816
	public GameObject m_hoverGlow;

	// Token: 0x04007C49 RID: 31817
	public GameObject m_selectedGlow;

	// Token: 0x04007C4A RID: 31818
	private int m_id;

	// Token: 0x04007C4B RID: 31819
	private object m_userData;
}
