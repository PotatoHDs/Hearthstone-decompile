using System;

namespace bgs
{
	// Token: 0x0200022C RID: 556
	public class MethodDescriptor
	{
		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x0600233A RID: 9018 RVA: 0x0007B77E File Offset: 0x0007997E
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x0600233B RID: 9019 RVA: 0x0007B786 File Offset: 0x00079986
		// (set) Token: 0x0600233C RID: 9020 RVA: 0x0007B78E File Offset: 0x0007998E
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

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x0600233D RID: 9021 RVA: 0x0007B797 File Offset: 0x00079997
		public MethodDescriptor.ParseMethod Parser
		{
			get
			{
				return this.m_parseMethod;
			}
		}

		// Token: 0x0600233E RID: 9022 RVA: 0x0007B79F File Offset: 0x0007999F
		public void RegisterListener(RPCContextDelegate d)
		{
			this.listener = d;
		}

		// Token: 0x0600233F RID: 9023 RVA: 0x0007B7A8 File Offset: 0x000799A8
		public void NotifyListener(RPCContext context)
		{
			if (this.listener != null)
			{
				this.listener(context);
			}
		}

		// Token: 0x06002340 RID: 9024 RVA: 0x0007B7BE File Offset: 0x000799BE
		public bool HasListener()
		{
			return this.listener != null;
		}

		// Token: 0x06002341 RID: 9025 RVA: 0x0007B7C9 File Offset: 0x000799C9
		public MethodDescriptor(string n, uint i, MethodDescriptor.ParseMethod parseMethod)
		{
			this.name = n;
			this.id = i;
			this.m_parseMethod = parseMethod;
			if (this.m_parseMethod == null)
			{
				BattleNet.Log.LogError("MethodDescriptor called with a null method type!");
			}
		}

		// Token: 0x04000E76 RID: 3702
		private string name;

		// Token: 0x04000E77 RID: 3703
		private uint id;

		// Token: 0x04000E78 RID: 3704
		private RPCContextDelegate listener;

		// Token: 0x04000E79 RID: 3705
		private MethodDescriptor.ParseMethod m_parseMethod;

		// Token: 0x020006C2 RID: 1730
		// (Invoke) Token: 0x06006281 RID: 25217
		public delegate IProtoBuf ParseMethod(byte[] bs);
	}
}
