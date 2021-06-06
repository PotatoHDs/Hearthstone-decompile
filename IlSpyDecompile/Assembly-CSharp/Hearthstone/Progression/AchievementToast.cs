using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.Progression
{
	[RequireComponent(typeof(WidgetTemplate))]
	public class AchievementToast : MonoBehaviour
	{
		private WidgetTemplate m_toast;

		private const string CODE_HIDE = "CODE_HIDE";

		private void Awake()
		{
			m_toast = GetComponent<WidgetTemplate>();
			m_toast.RegisterEventListener(delegate(string eventName)
			{
				if (eventName == "CODE_HIDE")
				{
					Hide();
				}
			});
		}

		private void Start()
		{
		}

		public void Initialize(AchievementDataModel dataModel)
		{
			dataModel.Name = GameStrings.Format("GLUE_PROGRESSION_ACHIEVEMENT_TOAST", dataModel.Name);
			m_toast.BindDataModel(dataModel);
		}

		public void Show()
		{
			if (!(m_toast == null))
			{
				Transform parent = BnetBar.Get().m_socialToastBone.transform;
				TransformUtil.AttachAndPreserveLocalTransform(base.gameObject.transform.parent, parent);
				BoxCollider component = base.gameObject.GetComponent<BoxCollider>();
				if ((bool)component)
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
				m_toast.Show();
			}
		}

		public void Hide()
		{
			if (!(m_toast == null))
			{
				m_toast.Hide();
				BoxCollider component = BnetBar.Get().m_socialToastBone.GetComponent<BoxCollider>();
				if ((bool)component)
				{
					Object.Destroy(component);
				}
				Object.Destroy(base.transform.parent.gameObject);
			}
		}

		public static void DebugShowFake(AchievementDataModel dataModel)
		{
			Widget fakeToast = WidgetInstance.Create(AchievementManager.ACHIEVEMENT_TOAST_PREFAB);
			fakeToast.RegisterReadyListener(delegate
			{
				AchievementToast componentInChildren = fakeToast.GetComponentInChildren<AchievementToast>();
				componentInChildren.Initialize(dataModel);
				componentInChildren.Show();
			});
		}
	}
}
