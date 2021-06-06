using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	[Serializable]
	public class ResumeJournalRequest
	{
		public CreationParameters createIfMissing;

		public string journalId;

		public string receipt;

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class ResumeJournalRequest {\n");
			stringBuilder.Append("  createIfMissing: ").Append(createIfMissing).Append("\n");
			stringBuilder.Append("  journalId: ").Append(journalId).Append("\n");
			stringBuilder.Append("  receipt: ").Append(receipt).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}
	}
}
