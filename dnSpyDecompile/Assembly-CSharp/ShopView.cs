using System;
using System.Collections.Generic;
using System.Linq;

// Token: 0x0200071D RID: 1821
public class ShopView
{
	// Token: 0x170005FE RID: 1534
	// (get) Token: 0x0600655A RID: 25946 RVA: 0x002106D7 File Offset: 0x0020E8D7
	public PurchaseAuthView PurchaseAuth
	{
		get
		{
			return this.FindComponent<PurchaseAuthView>();
		}
	}

	// Token: 0x170005FF RID: 1535
	// (get) Token: 0x0600655B RID: 25947 RVA: 0x002106DF File Offset: 0x0020E8DF
	public SummaryView Summary
	{
		get
		{
			return this.FindComponent<SummaryView>();
		}
	}

	// Token: 0x17000600 RID: 1536
	// (get) Token: 0x0600655C RID: 25948 RVA: 0x002106E7 File Offset: 0x0020E8E7
	public SendToBamView SendToBam
	{
		get
		{
			return this.FindComponent<SendToBamView>();
		}
	}

	// Token: 0x17000601 RID: 1537
	// (get) Token: 0x0600655D RID: 25949 RVA: 0x002106EF File Offset: 0x0020E8EF
	public LegalBamView LegalBam
	{
		get
		{
			return this.FindComponent<LegalBamView>();
		}
	}

	// Token: 0x17000602 RID: 1538
	// (get) Token: 0x0600655E RID: 25950 RVA: 0x002106F7 File Offset: 0x0020E8F7
	public DoneWithBamView DoneWithBam
	{
		get
		{
			return this.FindComponent<DoneWithBamView>();
		}
	}

	// Token: 0x17000603 RID: 1539
	// (get) Token: 0x0600655F RID: 25951 RVA: 0x002106FF File Offset: 0x0020E8FF
	public ChallengePromptView ChallengePrompt
	{
		get
		{
			return this.FindComponent<ChallengePromptView>();
		}
	}

	// Token: 0x17000604 RID: 1540
	// (get) Token: 0x06006560 RID: 25952 RVA: 0x00210707 File Offset: 0x0020E907
	// (set) Token: 0x06006561 RID: 25953 RVA: 0x0021070F File Offset: 0x0020E90F
	public bool HasStartedLoading { get; private set; }

	// Token: 0x17000605 RID: 1541
	// (get) Token: 0x06006562 RID: 25954 RVA: 0x00210718 File Offset: 0x0020E918
	public bool IsLoaded
	{
		get
		{
			return this.m_components.All((ShopView.IComponent component) => component.IsLoaded);
		}
	}

	// Token: 0x17000606 RID: 1542
	// (get) Token: 0x06006563 RID: 25955 RVA: 0x00210744 File Offset: 0x0020E944
	public bool IsPromptShowing
	{
		get
		{
			return this.m_components.Any((ShopView.IComponent component) => component.IsShown);
		}
	}

	// Token: 0x06006564 RID: 25956 RVA: 0x00210770 File Offset: 0x0020E970
	public ShopView()
	{
		this.m_components = new List<ShopView.IComponent>
		{
			this.InitializeComponent<PurchaseAuthView>(),
			this.InitializeComponent<SummaryView>(),
			this.InitializeComponent<SendToBamView>(),
			this.InitializeComponent<LegalBamView>(),
			this.InitializeComponent<DoneWithBamView>(),
			this.InitializeComponent<ChallengePromptView>()
		};
	}

	// Token: 0x1400005E RID: 94
	// (add) Token: 0x06006565 RID: 25957 RVA: 0x002107FC File Offset: 0x0020E9FC
	// (remove) Token: 0x06006566 RID: 25958 RVA: 0x00210834 File Offset: 0x0020EA34
	public event Action OnComponentReady = delegate()
	{
	};

	// Token: 0x06006567 RID: 25959 RVA: 0x0021086C File Offset: 0x0020EA6C
	public void LoadAssets()
	{
		if (this.HasStartedLoading)
		{
			return;
		}
		IAssetLoader assetLoader = AssetLoader.Get();
		this.m_components.ForEach(delegate(ShopView.IComponent component)
		{
			component.Load(assetLoader);
		});
		this.HasStartedLoading = true;
	}

	// Token: 0x06006568 RID: 25960 RVA: 0x002108B1 File Offset: 0x0020EAB1
	public void UnloadAssets()
	{
		this.m_components.ForEach(delegate(ShopView.IComponent component)
		{
			component.Unload();
		});
		this.HasStartedLoading = false;
	}

	// Token: 0x06006569 RID: 25961 RVA: 0x002108E4 File Offset: 0x0020EAE4
	public void Hide()
	{
		this.m_components.ForEach(delegate(ShopView.IComponent component)
		{
			component.Hide();
		});
	}

	// Token: 0x0600656A RID: 25962 RVA: 0x00210910 File Offset: 0x0020EB10
	private T InitializeComponent<T>() where T : ShopView.IComponent, new()
	{
		T result = Activator.CreateInstance<T>();
		result.OnComponentReady += this.HandleComponentReady;
		return result;
	}

	// Token: 0x0600656B RID: 25963 RVA: 0x0021093D File Offset: 0x0020EB3D
	private void HandleComponentReady()
	{
		this.OnComponentReady();
	}

	// Token: 0x0600656C RID: 25964 RVA: 0x0021094A File Offset: 0x0020EB4A
	private T FindComponent<T>() where T : class, ShopView.IComponent
	{
		return this.m_components.FirstOrDefault((ShopView.IComponent component) => component is T) as T;
	}

	// Token: 0x04005427 RID: 21543
	private readonly List<ShopView.IComponent> m_components;

	// Token: 0x020022A6 RID: 8870
	public interface IComponent
	{
		// Token: 0x17002980 RID: 10624
		// (get) Token: 0x060127FC RID: 75772
		bool IsLoaded { get; }

		// Token: 0x17002981 RID: 10625
		// (get) Token: 0x060127FD RID: 75773
		bool IsShown { get; }

		// Token: 0x140000DB RID: 219
		// (add) Token: 0x060127FE RID: 75774
		// (remove) Token: 0x060127FF RID: 75775
		event Action OnComponentReady;

		// Token: 0x06012800 RID: 75776
		void Load(IAssetLoader assetLoader);

		// Token: 0x06012801 RID: 75777
		void Unload();

		// Token: 0x06012802 RID: 75778
		void Hide();
	}
}
