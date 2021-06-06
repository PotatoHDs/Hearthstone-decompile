using System;
using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusGame
{
	// Token: 0x020001B0 RID: 432
	public class RemoveSpectators : IProtoBuf
	{
		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06001B4D RID: 6989 RVA: 0x0006068E File Offset: 0x0005E88E
		// (set) Token: 0x06001B4E RID: 6990 RVA: 0x00060696 File Offset: 0x0005E896
		public List<BnetId> TargetGameaccountIds
		{
			get
			{
				return this._TargetGameaccountIds;
			}
			set
			{
				this._TargetGameaccountIds = value;
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06001B4F RID: 6991 RVA: 0x0006069F File Offset: 0x0005E89F
		// (set) Token: 0x06001B50 RID: 6992 RVA: 0x000606A7 File Offset: 0x0005E8A7
		public bool KickAllSpectators
		{
			get
			{
				return this._KickAllSpectators;
			}
			set
			{
				this._KickAllSpectators = value;
				this.HasKickAllSpectators = true;
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06001B51 RID: 6993 RVA: 0x000606B7 File Offset: 0x0005E8B7
		// (set) Token: 0x06001B52 RID: 6994 RVA: 0x000606BF File Offset: 0x0005E8BF
		public bool RegenerateSpectatorPassword
		{
			get
			{
				return this._RegenerateSpectatorPassword;
			}
			set
			{
				this._RegenerateSpectatorPassword = value;
				this.HasRegenerateSpectatorPassword = true;
			}
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x000606D0 File Offset: 0x0005E8D0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (BnetId bnetId in this.TargetGameaccountIds)
			{
				num ^= bnetId.GetHashCode();
			}
			if (this.HasKickAllSpectators)
			{
				num ^= this.KickAllSpectators.GetHashCode();
			}
			if (this.HasRegenerateSpectatorPassword)
			{
				num ^= this.RegenerateSpectatorPassword.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x00060764 File Offset: 0x0005E964
		public override bool Equals(object obj)
		{
			RemoveSpectators removeSpectators = obj as RemoveSpectators;
			if (removeSpectators == null)
			{
				return false;
			}
			if (this.TargetGameaccountIds.Count != removeSpectators.TargetGameaccountIds.Count)
			{
				return false;
			}
			for (int i = 0; i < this.TargetGameaccountIds.Count; i++)
			{
				if (!this.TargetGameaccountIds[i].Equals(removeSpectators.TargetGameaccountIds[i]))
				{
					return false;
				}
			}
			return this.HasKickAllSpectators == removeSpectators.HasKickAllSpectators && (!this.HasKickAllSpectators || this.KickAllSpectators.Equals(removeSpectators.KickAllSpectators)) && this.HasRegenerateSpectatorPassword == removeSpectators.HasRegenerateSpectatorPassword && (!this.HasRegenerateSpectatorPassword || this.RegenerateSpectatorPassword.Equals(removeSpectators.RegenerateSpectatorPassword));
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x0006082B File Offset: 0x0005EA2B
		public void Deserialize(Stream stream)
		{
			RemoveSpectators.Deserialize(stream, this);
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x00060835 File Offset: 0x0005EA35
		public static RemoveSpectators Deserialize(Stream stream, RemoveSpectators instance)
		{
			return RemoveSpectators.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001B57 RID: 6999 RVA: 0x00060840 File Offset: 0x0005EA40
		public static RemoveSpectators DeserializeLengthDelimited(Stream stream)
		{
			RemoveSpectators removeSpectators = new RemoveSpectators();
			RemoveSpectators.DeserializeLengthDelimited(stream, removeSpectators);
			return removeSpectators;
		}

		// Token: 0x06001B58 RID: 7000 RVA: 0x0006085C File Offset: 0x0005EA5C
		public static RemoveSpectators DeserializeLengthDelimited(Stream stream, RemoveSpectators instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RemoveSpectators.Deserialize(stream, instance, num);
		}

		// Token: 0x06001B59 RID: 7001 RVA: 0x00060884 File Offset: 0x0005EA84
		public static RemoveSpectators Deserialize(Stream stream, RemoveSpectators instance, long limit)
		{
			if (instance.TargetGameaccountIds == null)
			{
				instance.TargetGameaccountIds = new List<BnetId>();
			}
			instance.KickAllSpectators = false;
			instance.RegenerateSpectatorPassword = false;
			while (limit < 0L || stream.Position < limit)
			{
				int num = stream.ReadByte();
				if (num == -1)
				{
					if (limit >= 0L)
					{
						throw new EndOfStreamException();
					}
					return instance;
				}
				else if (num != 10)
				{
					if (num != 16)
					{
						if (num != 24)
						{
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							if (key.Field == 0U)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
						}
						else
						{
							instance.RegenerateSpectatorPassword = ProtocolParser.ReadBool(stream);
						}
					}
					else
					{
						instance.KickAllSpectators = ProtocolParser.ReadBool(stream);
					}
				}
				else
				{
					instance.TargetGameaccountIds.Add(BnetId.DeserializeLengthDelimited(stream));
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06001B5A RID: 7002 RVA: 0x00060958 File Offset: 0x0005EB58
		public void Serialize(Stream stream)
		{
			RemoveSpectators.Serialize(stream, this);
		}

		// Token: 0x06001B5B RID: 7003 RVA: 0x00060964 File Offset: 0x0005EB64
		public static void Serialize(Stream stream, RemoveSpectators instance)
		{
			if (instance.TargetGameaccountIds.Count > 0)
			{
				foreach (BnetId bnetId in instance.TargetGameaccountIds)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, bnetId.GetSerializedSize());
					BnetId.Serialize(stream, bnetId);
				}
			}
			if (instance.HasKickAllSpectators)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.KickAllSpectators);
			}
			if (instance.HasRegenerateSpectatorPassword)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.RegenerateSpectatorPassword);
			}
		}

		// Token: 0x06001B5C RID: 7004 RVA: 0x00060A14 File Offset: 0x0005EC14
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.TargetGameaccountIds.Count > 0)
			{
				foreach (BnetId bnetId in this.TargetGameaccountIds)
				{
					num += 1U;
					uint serializedSize = bnetId.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasKickAllSpectators)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasRegenerateSpectatorPassword)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x04000A0E RID: 2574
		private List<BnetId> _TargetGameaccountIds = new List<BnetId>();

		// Token: 0x04000A0F RID: 2575
		public bool HasKickAllSpectators;

		// Token: 0x04000A10 RID: 2576
		private bool _KickAllSpectators;

		// Token: 0x04000A11 RID: 2577
		public bool HasRegenerateSpectatorPassword;

		// Token: 0x04000A12 RID: 2578
		private bool _RegenerateSpectatorPassword;

		// Token: 0x02000647 RID: 1607
		public enum PacketID
		{
			// Token: 0x04002102 RID: 8450
			ID = 26
		}
	}
}
