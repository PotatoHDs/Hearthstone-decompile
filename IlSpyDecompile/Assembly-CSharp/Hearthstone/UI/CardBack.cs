using System;
using Blizzard.T5.Jobs;
using Hearthstone.Core;
using Hearthstone.DataModels;
using UnityEngine;

namespace Hearthstone.UI
{
	[AddComponentMenu("")]
	[WidgetBehaviorDescription(Path = "Hearthstone/Card Back", UniqueWithinCategory = "asset")]
	public class CardBack : CustomWidgetBehavior
	{
		[Tooltip("This is the card back displayed by default.")]
		[SerializeField]
		private int m_defaultCardBack;

		[Tooltip("If true, it will use data model 'cardback' whenever bound.")]
		[SerializeField]
		private bool m_useDataModel = true;

		[Tooltip("If true, this will show the shadow object.")]
		[SerializeField]
		private bool m_useShadow = true;

		private static Dbf<CardBackDbfRecord> s_cardBacks;

		private int m_displayedCardBack;

		private bool m_isShowingShadow;

		private Actor m_cardBackActor;

		private static Dbf<CardBackDbfRecord> CardBacks
		{
			get
			{
				if (GameDbf.CardBack != null && Application.isPlaying)
				{
					return GameDbf.CardBack;
				}
				return s_cardBacks ?? (s_cardBacks = Dbf<CardBackDbfRecord>.Load("CARD_BACK", DbfFormat.XML));
			}
		}

		[Overridable]
		public bool ShowShadow
		{
			get
			{
				return m_useShadow;
			}
			set
			{
				if (m_useShadow != value)
				{
					m_useShadow = value;
					UpdateActor();
				}
			}
		}

		private void UpdateActor()
		{
			if (!(m_cardBackActor == null) && m_useShadow != m_isShowingShadow)
			{
				m_cardBackActor.EnableCardbackShadow(m_useShadow);
				m_isShowingShadow = m_useShadow;
			}
		}

		protected override void OnInitialize()
		{
			base.OnInitialize();
			m_displayedCardBack = -1;
			HearthstoneServices.InitializeDynamicServicesIfEditor(out var serviceDependencies, typeof(IAssetLoader), typeof(WidgetRunner));
			Processor.QueueJob(HearthstoneJobs.CreateJobFromAction("CardBack.CreatePreviewableObject", CreatePreviewableObject, JobFlags.StartImmediately, serviceDependencies));
		}

		private void CreatePreviewableObject()
		{
			CreatePreviewableObject(delegate(IPreviewableObject previewable, Action<GameObject> callback)
			{
				m_isShowingShadow = false;
				m_cardBackActor = null;
				m_displayedCardBack = GetDesiredCardBack();
				CardBackDbfRecord record = CardBacks.GetRecord(m_displayedCardBack);
				if (record != null && !string.IsNullOrEmpty(record.PrefabName))
				{
					Actor actor = (m_cardBackActor = CardBackManager.LoadCardBackActorByPrefab(record.PrefabName));
					if (actor == null)
					{
						callback(null);
					}
					else
					{
						UpdateActor();
						GameObject gameObject = actor.gameObject;
						gameObject.transform.SetParent(base.transform, worldPositionStays: false);
						gameObject.transform.localPosition = Vector3.zero;
						gameObject.transform.localRotation = Quaternion.identity;
						gameObject.transform.localScale = Vector3.one;
						callback(gameObject);
					}
				}
				else
				{
					callback(null);
				}
			}, (IPreviewableObject o) => m_displayedCardBack != GetDesiredCardBack() || (m_cardBackActor != null && m_useShadow != m_isShowingShadow));
		}

		private int GetDesiredCardBack()
		{
			if (m_useDataModel && GetDataModel(26, out var dataModel))
			{
				return ((CardBackDataModel)dataModel).CardBackId;
			}
			return m_defaultCardBack;
		}
	}
}
