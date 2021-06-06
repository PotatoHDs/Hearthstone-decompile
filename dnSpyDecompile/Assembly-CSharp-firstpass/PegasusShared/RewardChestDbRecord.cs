using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusShared
{
	// Token: 0x02000154 RID: 340
	public class RewardChestDbRecord : IProtoBuf
	{
		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x060016B6 RID: 5814 RVA: 0x0004E5BF File Offset: 0x0004C7BF
		// (set) Token: 0x060016B7 RID: 5815 RVA: 0x0004E5C7 File Offset: 0x0004C7C7
		public int Id { get; set; }

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x060016B8 RID: 5816 RVA: 0x0004E5D0 File Offset: 0x0004C7D0
		// (set) Token: 0x060016B9 RID: 5817 RVA: 0x0004E5D8 File Offset: 0x0004C7D8
		public bool ShowToReturningPlayer
		{
			get
			{
				return this._ShowToReturningPlayer;
			}
			set
			{
				this._ShowToReturningPlayer = value;
				this.HasShowToReturningPlayer = true;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x060016BA RID: 5818 RVA: 0x0004E5E8 File Offset: 0x0004C7E8
		// (set) Token: 0x060016BB RID: 5819 RVA: 0x0004E5F0 File Offset: 0x0004C7F0
		public List<LocalizedString> Strings
		{
			get
			{
				return this._Strings;
			}
			set
			{
				this._Strings = value;
			}
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x0004E5FC File Offset: 0x0004C7FC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Id.GetHashCode();
			if (this.HasShowToReturningPlayer)
			{
				num ^= this.ShowToReturningPlayer.GetHashCode();
			}
			foreach (LocalizedString localizedString in this.Strings)
			{
				num ^= localizedString.GetHashCode();
			}
			return num;
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x0004E68C File Offset: 0x0004C88C
		public override bool Equals(object obj)
		{
			RewardChestDbRecord rewardChestDbRecord = obj as RewardChestDbRecord;
			if (rewardChestDbRecord == null)
			{
				return false;
			}
			if (!this.Id.Equals(rewardChestDbRecord.Id))
			{
				return false;
			}
			if (this.HasShowToReturningPlayer != rewardChestDbRecord.HasShowToReturningPlayer || (this.HasShowToReturningPlayer && !this.ShowToReturningPlayer.Equals(rewardChestDbRecord.ShowToReturningPlayer)))
			{
				return false;
			}
			if (this.Strings.Count != rewardChestDbRecord.Strings.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Strings.Count; i++)
			{
				if (!this.Strings[i].Equals(rewardChestDbRecord.Strings[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x0004E73D File Offset: 0x0004C93D
		public void Deserialize(Stream stream)
		{
			RewardChestDbRecord.Deserialize(stream, this);
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x0004E747 File Offset: 0x0004C947
		public static RewardChestDbRecord Deserialize(Stream stream, RewardChestDbRecord instance)
		{
			return RewardChestDbRecord.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x0004E754 File Offset: 0x0004C954
		public static RewardChestDbRecord DeserializeLengthDelimited(Stream stream)
		{
			RewardChestDbRecord rewardChestDbRecord = new RewardChestDbRecord();
			RewardChestDbRecord.DeserializeLengthDelimited(stream, rewardChestDbRecord);
			return rewardChestDbRecord;
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x0004E770 File Offset: 0x0004C970
		public static RewardChestDbRecord DeserializeLengthDelimited(Stream stream, RewardChestDbRecord instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RewardChestDbRecord.Deserialize(stream, instance, num);
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x0004E798 File Offset: 0x0004C998
		public static RewardChestDbRecord Deserialize(Stream stream, RewardChestDbRecord instance, long limit)
		{
			if (instance.Strings == null)
			{
				instance.Strings = new List<LocalizedString>();
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
				else if (num != 8)
				{
					if (num != 16)
					{
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						uint field = key.Field;
						if (field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						if (field != 100U)
						{
							ProtocolParser.SkipKey(stream, key);
						}
						else if (key.WireType == Wire.LengthDelimited)
						{
							instance.Strings.Add(LocalizedString.DeserializeLengthDelimited(stream));
						}
					}
					else
					{
						instance.ShowToReturningPlayer = ProtocolParser.ReadBool(stream);
					}
				}
				else
				{
					instance.Id = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x0004E872 File Offset: 0x0004CA72
		public void Serialize(Stream stream)
		{
			RewardChestDbRecord.Serialize(stream, this);
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x0004E87C File Offset: 0x0004CA7C
		public static void Serialize(Stream stream, RewardChestDbRecord instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Id));
			if (instance.HasShowToReturningPlayer)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.ShowToReturningPlayer);
			}
			if (instance.Strings.Count > 0)
			{
				foreach (LocalizedString localizedString in instance.Strings)
				{
					stream.WriteByte(162);
					stream.WriteByte(6);
					ProtocolParser.WriteUInt32(stream, localizedString.GetSerializedSize());
					LocalizedString.Serialize(stream, localizedString);
				}
			}
		}

		// Token: 0x060016C5 RID: 5829 RVA: 0x0004E92C File Offset: 0x0004CB2C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Id));
			if (this.HasShowToReturningPlayer)
			{
				num += 1U;
				num += 1U;
			}
			if (this.Strings.Count > 0)
			{
				foreach (LocalizedString localizedString in this.Strings)
				{
					num += 2U;
					uint serializedSize = localizedString.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			num += 1U;
			return num;
		}

		// Token: 0x04000706 RID: 1798
		public bool HasShowToReturningPlayer;

		// Token: 0x04000707 RID: 1799
		private bool _ShowToReturningPlayer;

		// Token: 0x04000708 RID: 1800
		private List<LocalizedString> _Strings = new List<LocalizedString>();
	}
}
