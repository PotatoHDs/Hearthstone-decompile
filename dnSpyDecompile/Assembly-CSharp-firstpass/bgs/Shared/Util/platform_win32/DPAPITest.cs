using System;

namespace bgs.Shared.Util.platform_win32
{
	// Token: 0x02000281 RID: 641
	public class DPAPITest
	{
		// Token: 0x060025EC RID: 9708 RVA: 0x00086654 File Offset: 0x00084854
		[STAThread]
		private static void Main(string[] args)
		{
			try
			{
				string text = "Hello, world!";
				string entropy = null;
				Console.WriteLine("Plaintext: {0}\r\n", text);
				string text2 = DPAPI.Encrypt(DPAPI.KeyType.UserKey, text, entropy, "My Data");
				Console.WriteLine("Encrypted: {0}\r\n", text2);
				string arg2;
				string arg = DPAPI.Decrypt(text2, entropy, out arg2);
				Console.WriteLine("Decrypted: {0} <<<{1}>>>\r\n", arg, arg2);
			}
			catch (Exception innerException)
			{
				while (innerException != null)
				{
					Console.WriteLine(innerException.Message);
					innerException = innerException.InnerException;
				}
			}
		}
	}
}
