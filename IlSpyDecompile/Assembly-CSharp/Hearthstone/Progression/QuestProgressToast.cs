using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.Progression
{
	[RequireComponent(typeof(WidgetTemplate))]
	public class QuestProgressToast : MonoBehaviour
	{
		private WidgetTemplate m_toast;

		public PlatformDependentVector3 MULTIPLE_TOAST_OFFSET = new PlatformDependentVector3(PlatformCategory.Screen)
		{
			PC = new Vector3(0f, 0f, 35f),
			Phone = new Vector3(0f, 0f, 35f)
		};

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

		public void Initialize(QuestDataModel questDataModel)
		{
			m_toast.BindDataModel(questDataModel);
		}

		public void Show()
		{
			if (!(m_toast == null))
			{
				OverlayUI.Get().AddGameObject(base.gameObject.transform.parent.gameObject);
				m_toast.Show();
			}
		}

		public void Hide()
		{
			if (!(m_toast == null))
			{
				m_toast.Hide();
				Object.Destroy(base.transform.parent.gameObject);
			}
		}

		public Vector3 GetOffset()
		{
			return MULTIPLE_TOAST_OFFSET;
		}

		public static void DebugShowFake(QuestDataModel questDataModel)
		{
			Widget fakeToast = WidgetInstance.Create(QuestToastManager.QUEST_PROGRESS_TOAST_PREFAB);
			fakeToast.RegisterReadyListener(delegate
			{
				QuestProgressToast componentInChildren = fakeToast.GetComponentInChildren<QuestProgressToast>();
				componentInChildren.Initialize(questDataModel);
				OverlayUI.Get().AddGameObject(componentInChildren.transform.parent.gameObject);
				componentInChildren.Show();
			});
		}
	}
}
