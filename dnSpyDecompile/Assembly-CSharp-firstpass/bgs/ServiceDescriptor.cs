using System;

namespace bgs
{
	// Token: 0x02000239 RID: 569
	public class ServiceDescriptor
	{
		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x060023C5 RID: 9157 RVA: 0x0007E772 File Offset: 0x0007C972
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x060023C6 RID: 9158 RVA: 0x0007E77A File Offset: 0x0007C97A
		// (set) Token: 0x060023C7 RID: 9159 RVA: 0x0007E782 File Offset: 0x0007C982
		public uint Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x060023C8 RID: 9160 RVA: 0x0007E78B File Offset: 0x0007C98B
		public uint Hash
		{
			get
			{
				return this.hash;
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x060023C9 RID: 9161 RVA: 0x0007E793 File Offset: 0x0007C993
		// (set) Token: 0x060023CA RID: 9162 RVA: 0x0007E79B File Offset: 0x0007C99B
		public bool Imported { get; set; }

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x060023CB RID: 9163 RVA: 0x0007E7A4 File Offset: 0x0007C9A4
		// (set) Token: 0x060023CC RID: 9164 RVA: 0x0007E7AC File Offset: 0x0007C9AC
		public bool Exported { get; set; }

		// Token: 0x060023CD RID: 9165 RVA: 0x0007E7B8 File Offset: 0x0007C9B8
		public ServiceDescriptor(string n)
		{
			this.name = n;
			this.id = 255U;
			this.hash = Compute32.Hash(this.name);
			Console.WriteLine(string.Concat(new object[]
			{
				"service: ",
				n,
				", hash: ",
				this.hash
			}));
		}

		// Token: 0x060023CE RID: 9166 RVA: 0x0007E820 File Offset: 0x0007CA20
		public void RegisterMethodListener(uint method_id, RPCContextDelegate callback)
		{
			if (this.Methods != null && method_id > 0U && (ulong)method_id <= (ulong)((long)this.Methods.Length))
			{
				this.Methods[(int)method_id].RegisterListener(callback);
			}
		}

		// Token: 0x060023CF RID: 9167 RVA: 0x0007E849 File Offset: 0x0007CA49
		public string GetMethodName(uint method_id)
		{
			if (this.Methods != null && method_id > 0U && (ulong)method_id <= (ulong)((long)this.Methods.Length))
			{
				return this.Methods[(int)method_id].Name;
			}
			return "";
		}

		// Token: 0x060023D0 RID: 9168 RVA: 0x0007E877 File Offset: 0x0007CA77
		public int GetMethodCount()
		{
			if (this.Methods == null)
			{
				return 0;
			}
			return this.Methods.Length;
		}

		// Token: 0x060023D1 RID: 9169 RVA: 0x0007E88B File Offset: 0x0007CA8B
		public MethodDescriptor GetMethodDescriptor(uint method_id)
		{
			if (this.Methods == null)
			{
				return null;
			}
			if ((ulong)method_id >= (ulong)((long)this.Methods.Length))
			{
				return null;
			}
			return this.Methods[(int)method_id];
		}

		// Token: 0x060023D2 RID: 9170 RVA: 0x0007E8B0 File Offset: 0x0007CAB0
		public MethodDescriptor.ParseMethod GetParser(uint method_id)
		{
			if (this.Methods == null)
			{
				BattleNet.Log.LogWarning("ServiceDescriptor unable to get parser, no methods have been set.");
				return null;
			}
			if (method_id <= 0U)
			{
				BattleNet.Log.LogWarning("ServiceDescriptor {0} unable to get parser, invalid index={1}/{2}", new object[]
				{
					this.Hash,
					method_id,
					this.Methods.Length
				});
				return null;
			}
			if ((ulong)method_id >= (ulong)((long)this.Methods.Length))
			{
				BattleNet.Log.LogWarning("ServiceDescriptor {0} unable to get parser, invalid index={1}/{2}", new object[]
				{
					this.Hash,
					method_id,
					this.Methods.Length
				});
				return null;
			}
			if (this.Methods[(int)method_id].Parser == null)
			{
				BattleNet.Log.LogWarning("ServiceDescriptor {0} unable to get parser, invalid index={1}/{2}", new object[]
				{
					this.Hash,
					method_id,
					this.Methods.Length
				});
				return null;
			}
			return this.Methods[(int)method_id].Parser;
		}

		// Token: 0x060023D3 RID: 9171 RVA: 0x0007E9BD File Offset: 0x0007CBBD
		public bool HasMethodListener(uint method_id)
		{
			return this.Methods != null && method_id > 0U && (ulong)method_id < (ulong)((long)this.Methods.Length) && this.Methods[(int)method_id].HasListener();
		}

		// Token: 0x060023D4 RID: 9172 RVA: 0x0007E9E8 File Offset: 0x0007CBE8
		public void NotifyMethodListener(RPCContext context)
		{
			if (this.Methods != null && context.Header.MethodId > 0U && (ulong)context.Header.MethodId <= (ulong)((long)this.Methods.Length))
			{
				this.Methods[(int)context.Header.MethodId].NotifyListener(context);
			}
		}

		// Token: 0x04000EA2 RID: 3746
		private string name;

		// Token: 0x04000EA3 RID: 3747
		private uint id;

		// Token: 0x04000EA4 RID: 3748
		private uint hash;

		// Token: 0x04000EA5 RID: 3749
		protected MethodDescriptor[] Methods;

		// Token: 0x04000EA6 RID: 3750
		private const uint INVALID_SERVICE_ID = 255U;
	}
}
