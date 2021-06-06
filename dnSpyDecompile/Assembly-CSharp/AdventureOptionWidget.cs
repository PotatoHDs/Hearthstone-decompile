using System;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x02000046 RID: 70
[CustomEditClass]
public abstract class AdventureOptionWidget : MonoBehaviour
{
	// Token: 0x1700003D RID: 61
	// (get) Token: 0x060003D5 RID: 981 RVA: 0x00017E3B File Offset: 0x0001603B
	// (set) Token: 0x060003D6 RID: 982 RVA: 0x00017E48 File Offset: 0x00016048
	[CustomEditField(Sections = "Properties (Instance-Only)")]
	public bool IsNewlyUnlocked
	{
		get
		{
			return this.m_dataModel.NewlyUnlocked;
		}
		set
		{
			this.m_dataModel.NewlyUnlocked = value;
		}
	}

	// Token: 0x1700003E RID: 62
	// (get) Token: 0x060003D7 RID: 983 RVA: 0x00017E56 File Offset: 0x00016056
	[CustomEditField(Sections = "Properties (Instance-Only)")]
	public virtual bool IsReady
	{
		get
		{
			return this.m_widgetInstance != null && this.m_widgetInstance.IsReady && !this.m_widgetInstance.IsChangingStates && this.m_clickable != null;
		}
	}

	// Token: 0x1700003F RID: 63
	// (get) Token: 0x060003D8 RID: 984 RVA: 0x00017E8E File Offset: 0x0001608E
	[CustomEditField(Sections = "Properties (Read-Only)")]
	public bool IsIntroPlaying
	{
		get
		{
			return this.m_isIntroPlaying;
		}
	}

	// Token: 0x17000040 RID: 64
	// (get) Token: 0x060003D9 RID: 985 RVA: 0x00017E96 File Offset: 0x00016096
	[CustomEditField(Sections = "Properties (Read-Only)")]
	public bool IsOutroPlaying
	{
		get
		{
			return this.m_isOutroPlaying;
		}
	}

	// Token: 0x060003DA RID: 986 RVA: 0x00017E9E File Offset: 0x0001609E
	private void Awake()
	{
		this.m_widgetReference.RegisterReadyListener<WidgetInstance>(new Action<WidgetInstance>(this.OnWidgetInstanceReady));
	}

	// Token: 0x060003DB RID: 987 RVA: 0x00017EB8 File Offset: 0x000160B8
	protected virtual void OnWidgetInstanceReady(WidgetInstance widgetInstance)
	{
		if (widgetInstance == null)
		{
			Error.AddDevWarning("UI Issue!", "m_widgetReference is not set in the AdventureOptionWidget.cs! Cannot initialize its properties.", Array.Empty<object>());
			return;
		}
		this.m_widgetInstance = widgetInstance;
		this.m_widgetInstance.BindDataModel(this.m_dataModel, false);
		this.m_widgetInstance.TriggerEvent("SetUpState", default(Widget.TriggerEventParameters));
		this.m_widgetInstance.RegisterEventListener(new Widget.EventListenerDelegate(this.OnIntroSequenceComplete));
		this.m_widgetInstance.RegisterEventListener(new Widget.EventListenerDelegate(this.OnOutroSequenceComplete));
		this.SetVisible(false);
	}

	// Token: 0x060003DC RID: 988 RVA: 0x00017F4C File Offset: 0x0001614C
	protected virtual void OnClickableReady(Clickable clickable)
	{
		if (clickable == null)
		{
			Error.AddDevWarning("UI Issue!", "m_clickableReference is not set in the AdventureOptionWidget.cs! Cannot initialize its properties.", Array.Empty<object>());
			return;
		}
		this.m_clickable = clickable;
		this.SetInteractionEnabled(false);
		this.m_clickable.AddEventListener(UIEventType.ROLLOVER, delegate(UIEvent e)
		{
			this.Rollover();
		});
		this.m_clickable.AddEventListener(UIEventType.ROLLOUT, delegate(UIEvent e)
		{
			this.Rollout();
		});
	}

	// Token: 0x060003DD RID: 989 RVA: 0x00017FB7 File Offset: 0x000161B7
	protected virtual void OnIntroFinished()
	{
		this.SetInteractionEnabled(true);
		this.m_isIntroPlaying = false;
	}

	// Token: 0x060003DE RID: 990 RVA: 0x00017FC7 File Offset: 0x000161C7
	protected virtual void OnOutroFinished()
	{
		this.SetEnabled(false);
		base.gameObject.SetActive(false);
		this.m_isOutroPlaying = false;
	}

	// Token: 0x060003DF RID: 991 RVA: 0x00017FE3 File Offset: 0x000161E3
	protected virtual void Rollover()
	{
		if (this.m_acknowledgedCallback == null)
		{
			Log.Adventures.PrintError("Attempting to invoke callback for the OptionAcknowledgedCallback, but no callback was provided!", Array.Empty<object>());
			return;
		}
		this.m_acknowledgedCallback();
	}

	// Token: 0x060003E0 RID: 992 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void Rollout()
	{
	}

	// Token: 0x060003E1 RID: 993 RVA: 0x0001800D File Offset: 0x0001620D
	private void OnIntroSequenceComplete(string eventName)
	{
		if (!this.m_isIntroPlaying || eventName != "CODE_OPTION_INTRO_COMPLETE")
		{
			return;
		}
		this.OnIntroFinished();
	}

	// Token: 0x060003E2 RID: 994 RVA: 0x0001802B File Offset: 0x0001622B
	private void OnOutroSequenceComplete(string eventName)
	{
		if (!this.m_isOutroPlaying || eventName != "CODE_OPTION_OUTRO_COMPLETE")
		{
			return;
		}
		this.OnOutroFinished();
	}

	// Token: 0x060003E3 RID: 995 RVA: 0x0001804C File Offset: 0x0001624C
	protected void InitWidget(string name, bool locked, string lockedText, bool upgraded, bool completed, bool newlyUnlocked, AdventureOptionWidget.OptionAcknowledgedCallback acknowledgedCallback)
	{
		this.m_dataModel.Name = name;
		this.m_dataModel.Locked = locked;
		this.m_dataModel.LockedText = lockedText;
		this.m_dataModel.Completed = completed;
		this.m_dataModel.NewlyUnlocked = newlyUnlocked;
		this.m_dataModel.IsUpgraded = upgraded;
		if (this.m_widgetInstance != null)
		{
			this.m_widgetInstance.TriggerEvent("SetUpState", default(Widget.TriggerEventParameters));
		}
		this.m_acknowledgedCallback = acknowledgedCallback;
		this.Deselect();
		base.gameObject.SetActive(true);
	}

	// Token: 0x060003E4 RID: 996 RVA: 0x000180E6 File Offset: 0x000162E6
	protected void InitClickable()
	{
		if (this.m_isClickableInitialized)
		{
			return;
		}
		this.m_isClickableInitialized = true;
		this.m_clickableReference.RegisterReadyListener<Clickable>(new Action<Clickable>(this.OnClickableReady));
	}

	// Token: 0x060003E5 RID: 997 RVA: 0x00018110 File Offset: 0x00016310
	public AdventureLoadoutOptionDataModel GetDataModel()
	{
		return this.m_dataModel;
	}

	// Token: 0x060003E6 RID: 998 RVA: 0x00018118 File Offset: 0x00016318
	public void SetOptionCallbacks(Delegate selectedCallback, Delegate rolloverCallback = null, Delegate rolloutCallback = null)
	{
		this.m_selectedCallback = selectedCallback;
		this.m_rolloverCallback = rolloverCallback;
		this.m_rolloutCallback = rolloutCallback;
		this.InitClickable();
	}

	// Token: 0x060003E7 RID: 999 RVA: 0x00018135 File Offset: 0x00016335
	public virtual void Select()
	{
		if (this.m_dataModel != null && !this.m_dataModel.Locked)
		{
			this.m_dataModel.IsSelectedOption = true;
		}
	}

	// Token: 0x060003E8 RID: 1000 RVA: 0x00018158 File Offset: 0x00016358
	public void Deselect()
	{
		if (this.m_dataModel != null)
		{
			this.m_dataModel.IsSelectedOption = false;
		}
	}

	// Token: 0x060003E9 RID: 1001 RVA: 0x00018170 File Offset: 0x00016370
	public virtual void PlayIntro()
	{
		this.m_isIntroPlaying = true;
		this.m_widgetInstance.TriggerEvent(this.m_ControllerIntroStateName, default(Widget.TriggerEventParameters));
		this.SetVisible(true);
	}

	// Token: 0x060003EA RID: 1002 RVA: 0x000181A8 File Offset: 0x000163A8
	public virtual void PlayOutro()
	{
		this.SetInteractionEnabled(false);
		this.m_widgetInstance.TriggerEvent(this.m_ControllerOutroStateName, default(Widget.TriggerEventParameters));
		this.m_isOutroPlaying = true;
	}

	// Token: 0x060003EB RID: 1003 RVA: 0x000181DE File Offset: 0x000163DE
	public void SetEnabled(bool isEnable)
	{
		this.m_isEnabled = isEnable;
		this.SetInteractionEnabled(isEnable);
		this.SetVisible(isEnable);
	}

	// Token: 0x060003EC RID: 1004 RVA: 0x000181F8 File Offset: 0x000163F8
	public void SetInteractionEnabled(bool bEnable)
	{
		if (this.m_clickable == null)
		{
			return;
		}
		this.m_clickable.Active = bEnable;
		Collider component = this.m_clickable.GetComponent<Collider>();
		if (component == null)
		{
			Debug.LogWarning("AdventureOptionWidget tried to disable interaction but couldn't find the collider component!");
			return;
		}
		component.enabled = bEnable;
	}

	// Token: 0x060003ED RID: 1005 RVA: 0x00018247 File Offset: 0x00016447
	public virtual void SetVisible(bool isVisible)
	{
		if (isVisible == this.m_isVisible)
		{
			return;
		}
		this.m_isVisible = isVisible;
		if (isVisible)
		{
			this.m_widgetInstance.Show();
			return;
		}
		this.m_widgetInstance.Hide();
	}

	// Token: 0x040002A1 RID: 673
	private const string OPTION_INTRO_COMPLETE = "CODE_OPTION_INTRO_COMPLETE";

	// Token: 0x040002A2 RID: 674
	private const string OPTION_OUTRO_COMPLETE = "CODE_OPTION_OUTRO_COMPLETE";

	// Token: 0x040002A3 RID: 675
	[CustomEditField(Sections = "Widget References")]
	public AsyncReference m_widgetReference;

	// Token: 0x040002A4 RID: 676
	[CustomEditField(Sections = "Widget References")]
	public AsyncReference m_clickableReference;

	// Token: 0x040002A5 RID: 677
	[CustomEditField(Sections = "Visual Controller State Names")]
	public string m_ControllerIntroStateName = "PLAY_MOTE_IN";

	// Token: 0x040002A6 RID: 678
	[CustomEditField(Sections = "Visual Controller State Names")]
	public string m_ControllerOutroStateName = "PLAY_MOTE_OUT";

	// Token: 0x040002A7 RID: 679
	protected long m_databaseId;

	// Token: 0x040002A8 RID: 680
	protected bool m_isEnabled = true;

	// Token: 0x040002A9 RID: 681
	protected bool m_isVisible = true;

	// Token: 0x040002AA RID: 682
	protected bool m_isIntroPlaying;

	// Token: 0x040002AB RID: 683
	protected bool m_isOutroPlaying;

	// Token: 0x040002AC RID: 684
	protected bool m_isClickableInitialized;

	// Token: 0x040002AD RID: 685
	protected AdventureLoadoutOptionDataModel m_dataModel = new AdventureLoadoutOptionDataModel();

	// Token: 0x040002AE RID: 686
	protected WidgetInstance m_widgetInstance;

	// Token: 0x040002AF RID: 687
	protected Clickable m_clickable;

	// Token: 0x040002B0 RID: 688
	protected AdventureOptionWidget.OptionAcknowledgedCallback m_acknowledgedCallback;

	// Token: 0x040002B1 RID: 689
	protected Delegate m_selectedCallback;

	// Token: 0x040002B2 RID: 690
	protected Delegate m_rolloverCallback;

	// Token: 0x040002B3 RID: 691
	protected Delegate m_rolloutCallback;

	// Token: 0x0200130E RID: 4878
	// (Invoke) Token: 0x0600D65B RID: 54875
	public delegate void OptionAcknowledgedCallback();
}
