using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200005B RID: 91
[CustomEditClass]
[RequireComponent(typeof(AdventureWing))]
public class AdventureWingKarazhanHelper : MonoBehaviour
{
	// Token: 0x0600056C RID: 1388 RVA: 0x0001F4C4 File Offset: 0x0001D6C4
	public void Initialize()
	{
		this.m_adventureWing = base.GetComponent<AdventureWing>();
		if (this.m_adventureWing == null)
		{
			Debug.LogError("AdventureWingKarazhanHelper could not find an AdventureWing component on the same GameObject!");
			return;
		}
		WingDbId wingId = this.m_adventureWing.GetWingId();
		for (int i = 0; i < this.m_WingSpecificObjects.Count; i++)
		{
			AdventureWingKarazhanHelper.WingSpecificObject wingSpecificObject = this.m_WingSpecificObjects[i];
			wingSpecificObject.m_ObjectSpecificToWing.SetActive(false);
			if (wingSpecificObject.m_wingDbId == wingId)
			{
				this.m_objectForThisWing = wingSpecificObject.m_ObjectSpecificToWing;
				foreach (MeshRenderer renderer in this.m_backgroundRenderers)
				{
					Material material = renderer.GetMaterial();
					Vector2 textureOffset = material.GetTextureOffset("_MainTex");
					textureOffset.y = wingSpecificObject.m_backgroundOffset;
					material.SetTextureOffset("_MainTex", textureOffset);
				}
			}
		}
		if (this.m_objectForThisWing == null)
		{
			Debug.LogError("AdventureWingKarazhanHelper could not find an object for m_objectForThisWing!");
			return;
		}
		this.m_objectForThisWing.SetActive(true);
		PegUIElement componentInChildren = this.m_objectForThisWing.GetComponentInChildren<PegUIElement>();
		if (componentInChildren == null)
		{
			Debug.LogError("AdventureWingKarazhanHelper could not find the unlock button!");
			return;
		}
		this.m_adventureWing.m_UnlockButton = componentInChildren;
		PlayMakerFSM[] componentsInChildren = this.m_adventureWing.m_WingEventTable.GetComponentsInChildren<PlayMakerFSM>();
		for (int j = 0; j < componentsInChildren.Length; j++)
		{
			componentsInChildren[j].FsmVariables.GetFsmGameObject("KnockerRootVar").Value = componentInChildren.gameObject;
		}
		this.m_doorOpenPlayMakerFSM.FsmVariables.GetFsmGameObject("KnockerHeadVar").Value = this.m_objectForThisWing;
		AdventureConfig adventureConfig = AdventureConfig.Get();
		AdventureDbId selectedAdventure = adventureConfig.GetSelectedAdventure();
		AdventureModeDbId selectedMode = adventureConfig.GetSelectedMode();
		if (AdventureProgressMgr.Get().IsAdventureModeAndSectionComplete(selectedAdventure, selectedMode, 0))
		{
			foreach (Animator animator in this.m_adventureCompleteAnimators)
			{
				animator.enabled = true;
			}
		}
	}

	// Token: 0x040003B5 RID: 949
	public List<AdventureWingKarazhanHelper.WingSpecificObject> m_WingSpecificObjects = new List<AdventureWingKarazhanHelper.WingSpecificObject>();

	// Token: 0x040003B6 RID: 950
	public List<MeshRenderer> m_backgroundRenderers = new List<MeshRenderer>();

	// Token: 0x040003B7 RID: 951
	public List<Animator> m_adventureCompleteAnimators = new List<Animator>();

	// Token: 0x040003B8 RID: 952
	public PlayMakerFSM m_doorOpenPlayMakerFSM;

	// Token: 0x040003B9 RID: 953
	private AdventureWing m_adventureWing;

	// Token: 0x040003BA RID: 954
	private GameObject m_objectForThisWing;

	// Token: 0x040003BB RID: 955
	private float m_backgroundOffsetForThisWing;

	// Token: 0x02001350 RID: 4944
	[Serializable]
	public class WingSpecificObject
	{
		// Token: 0x0400A606 RID: 42502
		public WingDbId m_wingDbId;

		// Token: 0x0400A607 RID: 42503
		public GameObject m_ObjectSpecificToWing;

		// Token: 0x0400A608 RID: 42504
		public float m_backgroundOffset;
	}
}
