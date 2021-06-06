using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PegasusGame
{
	// Token: 0x020001D2 RID: 466
	public class SpectatorNotify : IProtoBuf
	{
		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06001DB9 RID: 7609 RVA: 0x00068812 File Offset: 0x00066A12
		// (set) Token: 0x06001DBA RID: 7610 RVA: 0x0006881A File Offset: 0x00066A1A
		public int PlayerId { get; set; }

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06001DBB RID: 7611 RVA: 0x00068823 File Offset: 0x00066A23
		// (set) Token: 0x06001DBC RID: 7612 RVA: 0x0006882B File Offset: 0x00066A2B
		public List<SpectatorChange> SpectatorChange
		{
			get
			{
				return this._SpectatorChange;
			}
			set
			{
				this._SpectatorChange = value;
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06001DBD RID: 7613 RVA: 0x00068834 File Offset: 0x00066A34
		// (set) Token: 0x06001DBE RID: 7614 RVA: 0x0006883C File Offset: 0x00066A3C
		public string SpectatorPasswordUpdate
		{
			get
			{
				return this._SpectatorPasswordUpdate;
			}
			set
			{
				this._SpectatorPasswordUpdate = value;
				this.HasSpectatorPasswordUpdate = (value != null);
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06001DBF RID: 7615 RVA: 0x0006884F File Offset: 0x00066A4F
		// (set) Token: 0x06001DC0 RID: 7616 RVA: 0x00068857 File Offset: 0x00066A57
		public SpectatorRemoved SpectatorRemoved
		{
			get
			{
				return this._SpectatorRemoved;
			}
			set
			{
				this._SpectatorRemoved = value;
				this.HasSpectatorRemoved = (value != null);
			}
		}

		// Token: 0x06001DC1 RID: 7617 RVA: 0x0006886C File Offset: 0x00066A6C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.PlayerId.GetHashCode();
			foreach (SpectatorChange spectatorChange in this.SpectatorChange)
			{
				num ^= spectatorChange.GetHashCode();
			}
			if (this.HasSpectatorPasswordUpdate)
			{
				num ^= this.SpectatorPasswordUpdate.GetHashCode();
			}
			if (this.HasSpectatorRemoved)
			{
				num ^= this.SpectatorRemoved.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001DC2 RID: 7618 RVA: 0x0006890C File Offset: 0x00066B0C
		public override bool Equals(object obj)
		{
			SpectatorNotify spectatorNotify = obj as SpectatorNotify;
			if (spectatorNotify == null)
			{
				return false;
			}
			if (!this.PlayerId.Equals(spectatorNotify.PlayerId))
			{
				return false;
			}
			if (this.SpectatorChange.Count != spectatorNotify.SpectatorChange.Count)
			{
				return false;
			}
			for (int i = 0; i < this.SpectatorChange.Count; i++)
			{
				if (!this.SpectatorChange[i].Equals(spectatorNotify.SpectatorChange[i]))
				{
					return false;
				}
			}
			return this.HasSpectatorPasswordUpdate == spectatorNotify.HasSpectatorPasswordUpdate && (!this.HasSpectatorPasswordUpdate || this.SpectatorPasswordUpdate.Equals(spectatorNotify.SpectatorPasswordUpdate)) && this.HasSpectatorRemoved == spectatorNotify.HasSpectatorRemoved && (!this.HasSpectatorRemoved || this.SpectatorRemoved.Equals(spectatorNotify.SpectatorRemoved));
		}

		// Token: 0x06001DC3 RID: 7619 RVA: 0x000689E5 File Offset: 0x00066BE5
		public void Deserialize(Stream stream)
		{
			SpectatorNotify.Deserialize(stream, this);
		}

		// Token: 0x06001DC4 RID: 7620 RVA: 0x000689EF File Offset: 0x00066BEF
		public static SpectatorNotify Deserialize(Stream stream, SpectatorNotify instance)
		{
			return SpectatorNotify.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001DC5 RID: 7621 RVA: 0x000689FC File Offset: 0x00066BFC
		public static SpectatorNotify DeserializeLengthDelimited(Stream stream)
		{
			SpectatorNotify spectatorNotify = new SpectatorNotify();
			SpectatorNotify.DeserializeLengthDelimited(stream, spectatorNotify);
			return spectatorNotify;
		}

		// Token: 0x06001DC6 RID: 7622 RVA: 0x00068A18 File Offset: 0x00066C18
		public static SpectatorNotify DeserializeLengthDelimited(Stream stream, SpectatorNotify instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SpectatorNotify.Deserialize(stream, instance, num);
		}

		// Token: 0x06001DC7 RID: 7623 RVA: 0x00068A40 File Offset: 0x00066C40
		public static SpectatorNotify Deserialize(Stream stream, SpectatorNotify instance, long limit)
		{
			if (instance.SpectatorChange == null)
			{
				instance.SpectatorChange = new List<SpectatorChange>();
			}
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
				else
				{
					if (num <= 34)
					{
						if (num == 8)
						{
							instance.PlayerId = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 34)
						{
							instance.SpectatorChange.Add(PegasusGame.SpectatorChange.DeserializeLengthDelimited(stream));
							continue;
						}
					}
					else
					{
						if (num == 42)
						{
							instance.SpectatorPasswordUpdate = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 50)
						{
							if (instance.SpectatorRemoved == null)
							{
								instance.SpectatorRemoved = SpectatorRemoved.DeserializeLengthDelimited(stream);
								continue;
							}
							SpectatorRemoved.DeserializeLengthDelimited(stream, instance.SpectatorRemoved);
							continue;
						}
					}
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06001DC8 RID: 7624 RVA: 0x00068B43 File Offset: 0x00066D43
		public void Serialize(Stream stream)
		{
			SpectatorNotify.Serialize(stream, this);
		}

		// Token: 0x06001DC9 RID: 7625 RVA: 0x00068B4C File Offset: 0x00066D4C
		public static void Serialize(Stream stream, SpectatorNotify instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.PlayerId));
			if (instance.SpectatorChange.Count > 0)
			{
				foreach (SpectatorChange spectatorChange in instance.SpectatorChange)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, spectatorChange.GetSerializedSize());
					PegasusGame.SpectatorChange.Serialize(stream, spectatorChange);
				}
			}
			if (instance.HasSpectatorPasswordUpdate)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.SpectatorPasswordUpdate));
			}
			if (instance.HasSpectatorRemoved)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteUInt32(stream, instance.SpectatorRemoved.GetSerializedSize());
				SpectatorRemoved.Serialize(stream, instance.SpectatorRemoved);
			}
		}

		// Token: 0x06001DCA RID: 7626 RVA: 0x00068C28 File Offset: 0x00066E28
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.PlayerId));
			if (this.SpectatorChange.Count > 0)
			{
				foreach (SpectatorChange spectatorChange in this.SpectatorChange)
				{
					num += 1U;
					uint serializedSize = spectatorChange.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasSpectatorPasswordUpdate)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.SpectatorPasswordUpdate);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasSpectatorRemoved)
			{
				num += 1U;
				uint serializedSize2 = this.SpectatorRemoved.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			num += 1U;
			return num;
		}

		// Token: 0x04000ABF RID: 2751
		private List<SpectatorChange> _SpectatorChange = new List<SpectatorChange>();

		// Token: 0x04000AC0 RID: 2752
		public bool HasSpectatorPasswordUpdate;

		// Token: 0x04000AC1 RID: 2753
		private string _SpectatorPasswordUpdate;

		// Token: 0x04000AC2 RID: 2754
		public bool HasSpectatorRemoved;

		// Token: 0x04000AC3 RID: 2755
		private SpectatorRemoved _SpectatorRemoved;

		// Token: 0x0200065D RID: 1629
		public enum PacketID
		{
			// Token: 0x04002154 RID: 8532
			ID = 24
		}
	}
}
