using System;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.Progression
{
	// Token: 0x02001110 RID: 4368
	public class ProfileClassIcon : MonoBehaviour
	{
		// Token: 0x17000D21 RID: 3361
		// (set) Token: 0x0600BF4F RID: 48975 RVA: 0x003A45FB File Offset: 0x003A27FB
		[Overridable]
		public bool IsGolden
		{
			set
			{
				this.SetPremium(value);
			}
		}

		// Token: 0x0600BF50 RID: 48976 RVA: 0x003A4604 File Offset: 0x003A2804
		private void Awake()
		{
			this.m_widget = base.GetComponent<WidgetTemplate>();
			this.m_XPBarLevelsReference.RegisterReadyListener<ProgressBar>(new Action<ProgressBar>(this.OnXPBarReady));
			this.m_XPBarWinsReference.RegisterReadyListener<ProgressBar>(new Action<ProgressBar>(this.OnXPBarReady));
		}

		// Token: 0x0600BF51 RID: 48977 RVA: 0x003A4640 File Offset: 0x003A2840
		private void OnXPBarReady(ProgressBar progressBar)
		{
			this.m_progressBar = progressBar.GetComponent<ProgressBar>();
			if (this.m_progressBar == null)
			{
				return;
			}
			ProfileClassIconDataModel dataModel = this.m_widget.GetDataModel<ProfileClassIconDataModel>();
			if (dataModel == null)
			{
				return;
			}
			if (dataModel.IsMaxLevel)
			{
				if (dataModel.IsPremium)
				{
					this.m_progressBar.SetProgressBar(1f);
				}
				else if (dataModel.IsGolden)
				{
					this.m_progressBar.SetProgressBar((float)dataModel.Wins / (float)dataModel.PremiumWinsReq);
				}
				else
				{
					this.m_progressBar.SetProgressBar((float)dataModel.Wins / (float)dataModel.GoldWinsReq);
				}
				this.m_progressBar.SetLabel(dataModel.WinsText);
				return;
			}
			this.m_progressBar.SetProgressBar((float)dataModel.CurrentLevelXP / (float)dataModel.CurrentLevelXPMax);
		}

		// Token: 0x0600BF52 RID: 48978 RVA: 0x003A4704 File Offset: 0x003A2904
		private void SetPremium(bool isPremium)
		{
			if (!isPremium)
			{
				base.GetComponentInChildren<Renderer>().GetMaterial().SetTexture("_MaskTex", null);
			}
		}

		// Token: 0x04009B61 RID: 39777
		public AsyncReference m_XPBarLevelsReference;

		// Token: 0x04009B62 RID: 39778
		public AsyncReference m_XPBarWinsReference;

		// Token: 0x04009B63 RID: 39779
		private Widget m_widget;

		// Token: 0x04009B64 RID: 39780
		private ProgressBar m_progressBar;
	}
}
