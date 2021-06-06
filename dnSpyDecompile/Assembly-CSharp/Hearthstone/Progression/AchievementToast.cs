using System;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.Progression
{
	// Token: 0x0200110A RID: 4362
	[RequireComponent(typeof(WidgetTemplate))]
	public class AchievementToast : MonoBehaviour
	{
		// Token: 0x0600BF23 RID: 48931 RVA: 0x003A3B8C File Offset: 0x003A1D8C
		private void Awake()
		{
			this.m_toast = base.GetComponent<WidgetTemplate>();
			this.m_toast.RegisterEventListener(delegate(string eventName)
			{
				if (eventName == "CODE_HIDE")
				{
					this.Hide();
				}
			});
		}

		// Token: 0x0600BF24 RID: 48932 RVA: 0x00003BE8 File Offset: 0x00001DE8
		private void Start()
		{
		}

		// Token: 0x0600BF25 RID: 48933 RVA: 0x003A3BB1 File Offset: 0x003A1DB1
		public void Initialize(AchievementDataModel dataModel)
		{
			dataModel.Name = GameStrings.Format("GLUE_PROGRESSION_ACHIEVEMENT_TOAST", new object[]
			{
				dataModel.Name
			});
			this.m_toast.BindDataModel(dataModel, false);
		}

		// Token: 0x0600BF26 RID: 48934 RVA: 0x003A3BE0 File Offset: 0x003A1DE0
		public void Show()
		{
			if (this.m_toast == null)
			{
				return;
			}
			Transform transform = BnetBar.Get().m_socialToastBone.transform;
			TransformUtil.AttachAndPreserveLocalTransform(base.gameObject.transform.parent, transform);
			BoxCollider component = base.gameObject.GetComponent<BoxCollider>();
			if (component)
			{
				BoxCollider boxCollider = BnetBar.Get().m_socialToastBone.AddComponent<BoxCollider>();
				Matrix4x4 matrix4x = BnetBar.Get().m_socialToastBone.transform.worldToLocalMatrix * base.transform.localToWorldMatrix;
				boxCollider.center = matrix4x.MultiplyPoint(component.center);
				boxCollider.size = matrix4x.MultiplyPoint(component.size);
			}
			else
			{
				Debug.LogWarning("AchievementToast requires a box collider to maintain correct anchoring on the SocialToastBone when BnetBar.UpdateLayout() is called");
			}
			this.m_toast.Show();
		}

		// Token: 0x0600BF27 RID: 48935 RVA: 0x003A3CA8 File Offset: 0x003A1EA8
		public void Hide()
		{
			if (this.m_toast == null)
			{
				return;
			}
			this.m_toast.Hide();
			BoxCollider component = BnetBar.Get().m_socialToastBone.GetComponent<BoxCollider>();
			if (component)
			{
				UnityEngine.Object.Destroy(component);
			}
			UnityEngine.Object.Destroy(base.transform.parent.gameObject);
		}

		// Token: 0x0600BF28 RID: 48936 RVA: 0x003A3D04 File Offset: 0x003A1F04
		public static void DebugShowFake(AchievementDataModel dataModel)
		{
			Widget fakeToast = WidgetInstance.Create(AchievementManager.ACHIEVEMENT_TOAST_PREFAB, false);
			fakeToast.RegisterReadyListener(delegate(object _)
			{
				AchievementToast componentInChildren = fakeToast.GetComponentInChildren<AchievementToast>();
				componentInChildren.Initialize(dataModel);
				componentInChildren.Show();
			}, null, true);
		}

		// Token: 0x04009B44 RID: 39748
		private WidgetTemplate m_toast;

		// Token: 0x04009B45 RID: 39749
		private const string CODE_HIDE = "CODE_HIDE";
	}
}
