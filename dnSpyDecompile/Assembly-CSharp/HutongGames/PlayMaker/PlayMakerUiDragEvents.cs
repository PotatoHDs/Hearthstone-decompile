using System;
using HutongGames.PlayMaker.Actions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker
{
	// Token: 0x02000B90 RID: 2960
	[AddComponentMenu("PlayMaker/UI/UI Drag Events")]
	public class PlayMakerUiDragEvents : PlayMakerUiEventBase, IDragHandler, IEventSystemHandler, IBeginDragHandler, IEndDragHandler
	{
		// Token: 0x06009B67 RID: 39783 RVA: 0x0031F6F5 File Offset: 0x0031D8F5
		public void OnBeginDrag(PointerEventData eventData)
		{
			UiGetLastPointerDataInfo.lastPointerEventData = eventData;
			base.SendEvent(FsmEvent.UiBeginDrag);
		}

		// Token: 0x06009B68 RID: 39784 RVA: 0x0031F708 File Offset: 0x0031D908
		public void OnDrag(PointerEventData eventData)
		{
			UiGetLastPointerDataInfo.lastPointerEventData = eventData;
			base.SendEvent(FsmEvent.UiDrag);
		}

		// Token: 0x06009B69 RID: 39785 RVA: 0x0031F71B File Offset: 0x0031D91B
		public void OnEndDrag(PointerEventData eventData)
		{
			UiGetLastPointerDataInfo.lastPointerEventData = eventData;
			base.SendEvent(FsmEvent.UiEndDrag);
		}
	}
}
