using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PegasusShared
{
	// Token: 0x0200014D RID: 333
	public class GuestHeroDbRecord : IProtoBuf
	{
		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06001605 RID: 5637 RVA: 0x0004B69A File Offset: 0x0004989A
		// (set) Token: 0x06001606 RID: 5638 RVA: 0x0004B6A2 File Offset: 0x000498A2
		public int Id { get; set; }

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06001607 RID: 5639 RVA: 0x0004B6AB File Offset: 0x000498AB
		// (set) Token: 0x06001608 RID: 5640 RVA: 0x0004B6B3 File Offset: 0x000498B3
		public int CardId { get; set; }

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06001609 RID: 5641 RVA: 0x0004B6BC File Offset: 0x000498BC
		// (set) Token: 0x0600160A RID: 5642 RVA: 0x0004B6C4 File Offset: 0x000498C4
		public string UnlockEvent { get; set; }

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x0600160B RID: 5643 RVA: 0x0004B6CD File Offset: 0x000498CD
		// (set) Token: 0x0600160C RID: 5644 RVA: 0x0004B6D5 File Offset: 0x000498D5
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

		// Token: 0x0600160D RID: 5645 RVA: 0x0004B6E0 File Offset: 0x000498E0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Id.GetHashCode();
			num ^= this.CardId.GetHashCode();
			num ^= this.UnlockEvent.GetHashCode();
			foreach (LocalizedString localizedString in this.Strings)
			{
				num ^= localizedString.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600160E RID: 5646 RVA: 0x0004B774 File Offset: 0x00049974
		public override bool Equals(object obj)
		{
			GuestHeroDbRecord guestHeroDbRecord = obj as GuestHeroDbRecord;
			if (guestHeroDbRecord == null)
			{
				return false;
			}
			if (!this.Id.Equals(guestHeroDbRecord.Id))
			{
				return false;
			}
			if (!this.CardId.Equals(guestHeroDbRecord.CardId))
			{
				return false;
			}
			if (!this.UnlockEvent.Equals(guestHeroDbRecord.UnlockEvent))
			{
				return false;
			}
			if (this.Strings.Count != guestHeroDbRecord.Strings.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Strings.Count; i++)
			{
				if (!this.Strings[i].Equals(guestHeroDbRecord.Strings[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600160F RID: 5647 RVA: 0x0004B824 File Offset: 0x00049A24
		public void Deserialize(Stream stream)
		{
			GuestHeroDbRecord.Deserialize(stream, this);
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x0004B82E File Offset: 0x00049A2E
		public static GuestHeroDbRecord Deserialize(Stream stream, GuestHeroDbRecord instance)
		{
			return GuestHeroDbRecord.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x0004B83C File Offset: 0x00049A3C
		public static GuestHeroDbRecord DeserializeLengthDelimited(Stream stream)
		{
			GuestHeroDbRecord guestHeroDbRecord = new GuestHeroDbRecord();
			GuestHeroDbRecord.DeserializeLengthDelimited(stream, guestHeroDbRecord);
			return guestHeroDbRecord;
		}

		// Token: 0x06001612 RID: 5650 RVA: 0x0004B858 File Offset: 0x00049A58
		public static GuestHeroDbRecord DeserializeLengthDelimited(Stream stream, GuestHeroDbRecord instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GuestHeroDbRecord.Deserialize(stream, instance, num);
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x0004B880 File Offset: 0x00049A80
		public static GuestHeroDbRecord Deserialize(Stream stream, GuestHeroDbRecord instance, long limit)
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
						if (num != 42)
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
							instance.UnlockEvent = ProtocolParser.ReadString(stream);
						}
					}
					else
					{
						instance.CardId = (int)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06001614 RID: 5652 RVA: 0x0004B96E File Offset: 0x00049B6E
		public void Serialize(Stream stream)
		{
			GuestHeroDbRecord.Serialize(stream, this);
		}

		// Token: 0x06001615 RID: 5653 RVA: 0x0004B978 File Offset: 0x00049B78
		public static void Serialize(Stream stream, GuestHeroDbRecord instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Id));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.CardId));
			if (instance.UnlockEvent == null)
			{
				throw new ArgumentNullException("UnlockEvent", "Required by proto specification.");
			}
			stream.WriteByte(42);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.UnlockEvent));
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

		// Token: 0x06001616 RID: 5654 RVA: 0x0004BA58 File Offset: 0x00049C58
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Id));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.CardId));
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.UnlockEvent);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			if (this.Strings.Count > 0)
			{
				foreach (LocalizedString localizedString in this.Strings)
				{
					num += 2U;
					uint serializedSize = localizedString.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			num += 3U;
			return num;
		}

		// Token: 0x040006C3 RID: 1731
		private List<LocalizedString> _Strings = new List<LocalizedString>();
	}
}
