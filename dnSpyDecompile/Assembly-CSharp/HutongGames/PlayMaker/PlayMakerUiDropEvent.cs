using System;
using HutongGames.PlayMaker.Actions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker
{
	// Token: 0x02000B91 RID: 2961
	[AddComponentMenu("PlayMaker/UI/UI Drop Event")]
	public class PlayMakerUiDropEvent : PlayMakerUiEventBase, IDropHandler, IEventSystemHandler
	{
		// Token: 0x06009B6B RID: 39787 RVA: 0x0031F72E File Offset: 0x0031D92E
		public void OnDrop(PointerEventData eventData)
		{
			UiGetLastPointerDataInfo.lastPointerEventData = eventData;
			base.SendEvent(FsmEvent.UiDrop);
		}
	}
}
