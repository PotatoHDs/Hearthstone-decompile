using System;
using Blizzard.T5.Jobs;
using Hearthstone.Core;
using Hearthstone.DataModels;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x02000FDC RID: 4060
	[AddComponentMenu("")]
	[WidgetBehaviorDescription(Path = "Hearthstone/Card Back", UniqueWithinCategory = "asset")]
	public class CardBack : CustomWidgetBehavior
	{
		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x0600B0C1 RID: 45249 RVA: 0x00369A0B File Offset: 0x00367C0B
		private static Dbf<CardBackDbfRecord> CardBacks
		{
			get
			{
				if (GameDbf.CardBack != null && Application.isPlaying)
				{
					return GameDbf.CardBack;
				}
				Dbf<CardBackDbfRecord> result;
				if ((result = CardBack.s_cardBacks) == null)
				{
					result = (CardBack.s_cardBacks = Dbf<CardBackDbfRecord>.Load("CARD_BACK", DbfFormat.XML));
				}
				return result;
			}
		}

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x0600B0C2 RID: 45250 RVA: 0x00369A3B File Offset: 0x00367C3B
		// (set) Token: 0x0600B0C3 RID: 45251 RVA: 0x00369A43 File Offset: 0x00367C43
		[Overridable]
		public bool ShowShadow
		{
			get
			{
				return this.m_useShadow;
			}
			set
			{
				if (this.m_useShadow != value)
				{
					this.m_useShadow = value;
					this.UpdateActor();
				}
			}
		}

		// Token: 0x0600B0C4 RID: 45252 RVA: 0x00369A5B File Offset: 0x00367C5B
		private void UpdateActor()
		{
			if (this.m_cardBackActor == null)
			{
				return;
			}
			if (this.m_useShadow != this.m_isShowingShadow)
			{
				this.m_cardBackActor.EnableCardbackShadow(this.m_useShadow);
				this.m_isShowingShadow = this.m_useShadow;
			}
		}

		// Token: 0x0600B0C5 RID: 45253 RVA: 0x00369A98 File Offset: 0x00367C98
		protected override void OnInitialize()
		{
			base.OnInitialize();
			this.m_displayedCardBack = -1;
			IJobDependency[] dependencies;
			HearthstoneServices.InitializeDynamicServicesIfEditor(out dependencies, new Type[]
			{
				typeof(IAssetLoader),
				typeof(WidgetRunner)
			});
			Processor.QueueJob(HearthstoneJobs.CreateJobFromAction("CardBack.CreatePreviewableObject", new Action(this.CreatePreviewableObject), JobFlags.StartImmediately, dependencies));
		}

		// Token: 0x0600B0C6 RID: 45254 RVA: 0x00369AF8 File Offset: 0x00367CF8
		private void CreatePreviewableObject()
		{
			base.CreatePreviewableObject(delegate(CustomWidgetBehavior.IPreviewableObject previewable, Action<GameObject> callback)
			{
				this.m_isShowingShadow = false;
				this.m_cardBackActor = null;
				this.m_displayedCardBack = this.GetDesiredCardBack();
				CardBackDbfRecord record = CardBack.CardBacks.GetRecord(this.m_displayedCardBack);
				if (record == null || string.IsNullOrEmpty(record.PrefabName))
				{
					callback(null);
					return;
				}
				Actor actor = CardBackManager.LoadCardBackActorByPrefab(record.PrefabName, false, "Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9", false);
				this.m_cardBackActor = actor;
				if (actor == null)
				{
					callback(null);
					return;
				}
				this.UpdateActor();
				GameObject gameObject = actor.gameObject;
				gameObject.transform.SetParent(base.transform, false);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localRotation = Quaternion.identity;
				gameObject.transform.localScale = Vector3.one;
				callback(gameObject);
			}, (CustomWidgetBehavior.IPreviewableObject o) => this.m_displayedCardBack != this.GetDesiredCardBack() || (this.m_cardBackActor != null && this.m_useShadow != this.m_isShowingShadow), null);
		}

		// Token: 0x0600B0C7 RID: 45255 RVA: 0x00369B1C File Offset: 0x00367D1C
		private int GetDesiredCardBack()
		{
			IDataModel dataModel;
			if (this.m_useDataModel && base.GetDataModel(26, out dataModel))
			{
				return ((CardBackDataModel)dataModel).CardBackId;
			}
			return this.m_defaultCardBack;
		}

		// Token: 0x0400955F RID: 38239
		[Tooltip("This is the card back displayed by default.")]
		[SerializeField]
		private int m_defaultCardBack;

		// Token: 0x04009560 RID: 38240
		[Tooltip("If true, it will use data model 'cardback' whenever bound.")]
		[SerializeField]
		private bool m_useDataModel = true;

		// Token: 0x04009561 RID: 38241
		[Tooltip("If true, this will show the shadow object.")]
		[SerializeField]
		private bool m_useShadow = true;

		// Token: 0x04009562 RID: 38242
		private static Dbf<CardBackDbfRecord> s_cardBacks;

		// Token: 0x04009563 RID: 38243
		private int m_displayedCardBack;

		// Token: 0x04009564 RID: 38244
		private bool m_isShowingShadow;

		// Token: 0x04009565 RID: 38245
		private Actor m_cardBackActor;
	}
}
