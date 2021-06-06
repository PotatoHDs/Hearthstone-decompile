using System;
using HutongGames.PlayMaker.Actions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker
{
	// Token: 0x02000B96 RID: 2966
	[AddComponentMenu("PlayMaker/UI/UI Pointer Events")]
	public class PlayMakerUiPointerEvents : PlayMakerUiEventBase, IPointerClickHandler, IEventSystemHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
	{
		// Token: 0x06009B80 RID: 39808 RVA: 0x0031FA88 File Offset: 0x0031DC88
		public void OnPointerClick(PointerEventData eventData)
		{
			UiGetLastPointerDataInfo.lastPointerEventData = eventData;
			base.SendEvent(FsmEvent.UiPointerClick);
		}

		// Token: 0x06009B81 RID: 39809 RVA: 0x0031FA9B File Offset: 0x0031DC9B
		public void OnPointerDown(PointerEventData eventData)
		{
			UiGetLastPointerDataInfo.lastPointerEventData = eventData;
			base.SendEvent(FsmEvent.UiPointerDown);
		}

		// Token: 0x06009B82 RID: 39810 RVA: 0x0031FAAE File Offset: 0x0031DCAE
		public void OnPointerEnter(PointerEventData eventData)
		{
			UiGetLastPointerDataInfo.lastPointerEventData = eventData;
			base.SendEvent(FsmEvent.UiPointerEnter);
		}

		// Token: 0x06009B83 RID: 39811 RVA: 0x0031FAC1 File Offset: 0x0031DCC1
		public void OnPointerExit(PointerEventData eventData)
		{
			UiGetLastPointerDataInfo.lastPointerEventData = eventData;
			base.SendEvent(FsmEvent.UiPointerExit);
		}

		// Token: 0x06009B84 RID: 39812 RVA: 0x0031FAD4 File Offset: 0x0031DCD4
		public void OnPointerUp(PointerEventData eventData)
		{
			UiGetLastPointerDataInfo.lastPointerEventData = eventData;
			base.SendEvent(FsmEvent.UiPointerUp);
		}
	}
}
