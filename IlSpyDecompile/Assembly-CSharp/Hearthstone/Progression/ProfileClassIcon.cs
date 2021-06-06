using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.Progression
{
	public class ProfileClassIcon : MonoBehaviour
	{
		public AsyncReference m_XPBarLevelsReference;

		public AsyncReference m_XPBarWinsReference;

		private Widget m_widget;

		private ProgressBar m_progressBar;

		[Overridable]
		public bool IsGolden
		{
			set
			{
				SetPremium(value);
			}
		}

		private void Awake()
		{
			m_widget = GetComponent<WidgetTemplate>();
			m_XPBarLevelsReference.RegisterReadyListener<ProgressBar>(OnXPBarReady);
			m_XPBarWinsReference.RegisterReadyListener<ProgressBar>(OnXPBarReady);
		}

		private void OnXPBarReady(ProgressBar progressBar)
		{
			m_progressBar = progressBar.GetComponent<ProgressBar>();
			if (m_progressBar == null)
			{
				return;
			}
			ProfileClassIconDataModel dataModel = m_widget.GetDataModel<ProfileClassIconDataModel>();
			if (dataModel == null)
			{
				return;
			}
			if (dataModel.IsMaxLevel)
			{
				if (dataModel.IsPremium)
				{
					m_progressBar.SetProgressBar(1f);
				}
				else if (dataModel.IsGolden)
				{
					m_progressBar.SetProgressBar((float)dataModel.Wins / (float)dataModel.PremiumWinsReq);
				}
				else
				{
					m_progressBar.SetProgressBar((float)dataModel.Wins / (float)dataModel.GoldWinsReq);
				}
				m_progressBar.SetLabel(dataModel.WinsText);
			}
			else
			{
				m_progressBar.SetProgressBar((float)dataModel.CurrentLevelXP / (float)dataModel.CurrentLevelXPMax);
			}
		}

		private void SetPremium(bool isPremium)
		{
			if (!isPremium)
			{
				GetComponentInChildren<Renderer>().GetMaterial().SetTexture("_MaskTex", null);
			}
		}
	}
}
