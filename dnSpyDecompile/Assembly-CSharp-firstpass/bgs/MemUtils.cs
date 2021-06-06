using System;
using System.Runtime.InteropServices;
using System.Text;

namespace bgs
{
	// Token: 0x0200025B RID: 603
	public static class MemUtils
	{
		// Token: 0x06002505 RID: 9477 RVA: 0x00083260 File Offset: 0x00081460
		public static void FreePtr(IntPtr ptr)
		{
			if (ptr == IntPtr.Zero)
			{
				return;
			}
			Marshal.FreeHGlobal(ptr);
		}

		// Token: 0x06002506 RID: 9478 RVA: 0x00083278 File Offset: 0x00081478
		public static byte[] PtrToBytes(IntPtr ptr, int size)
		{
			if (ptr == IntPtr.Zero)
			{
				return null;
			}
			if (size == 0)
			{
				return null;
			}
			byte[] array = new byte[size];
			Marshal.Copy(ptr, array, 0, size);
			return array;
		}

		// Token: 0x06002507 RID: 9479 RVA: 0x000832AA File Offset: 0x000814AA
		public static IntPtr PtrFromBytes(byte[] bytes)
		{
			return MemUtils.PtrFromBytes(bytes, 0);
		}

		// Token: 0x06002508 RID: 9480 RVA: 0x000832B4 File Offset: 0x000814B4
		public static IntPtr PtrFromBytes(byte[] bytes, int offset)
		{
			if (bytes == null)
			{
				return IntPtr.Zero;
			}
			int len = bytes.Length - offset;
			return MemUtils.PtrFromBytes(bytes, offset, len);
		}

		// Token: 0x06002509 RID: 9481 RVA: 0x000832D8 File Offset: 0x000814D8
		public static IntPtr PtrFromBytes(byte[] bytes, int offset, int len)
		{
			if (bytes == null)
			{
				return IntPtr.Zero;
			}
			if (len <= 0)
			{
				return IntPtr.Zero;
			}
			IntPtr intPtr = Marshal.AllocHGlobal(len);
			Marshal.Copy(bytes, offset, intPtr, len);
			return intPtr;
		}

		// Token: 0x0600250A RID: 9482 RVA: 0x0008330C File Offset: 0x0008150C
		public static byte[] StructToBytes<T>(T t)
		{
			int num = Marshal.SizeOf(typeof(T));
			byte[] array = new byte[num];
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			Marshal.StructureToPtr<T>(t, intPtr, true);
			Marshal.Copy(intPtr, array, 0, num);
			Marshal.FreeHGlobal(intPtr);
			return array;
		}

		// Token: 0x0600250B RID: 9483 RVA: 0x0008334F File Offset: 0x0008154F
		public static T StructFromBytes<T>(byte[] bytes)
		{
			return MemUtils.StructFromBytes<T>(bytes, 0);
		}

		// Token: 0x0600250C RID: 9484 RVA: 0x00083358 File Offset: 0x00081558
		public static T StructFromBytes<T>(byte[] bytes, int offset)
		{
			Type typeFromHandle = typeof(T);
			int num = Marshal.SizeOf(typeFromHandle);
			if (bytes == null)
			{
				return default(T);
			}
			if (bytes.Length - offset < num)
			{
				return default(T);
			}
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			Marshal.Copy(bytes, offset, intPtr, num);
			T result = (T)((object)Marshal.PtrToStructure(intPtr, typeFromHandle));
			Marshal.FreeHGlobal(intPtr);
			return result;
		}

		// Token: 0x0600250D RID: 9485 RVA: 0x000833B8 File Offset: 0x000815B8
		public static IntPtr Utf8PtrFromString(string managedString)
		{
			if (managedString == null)
			{
				return IntPtr.Zero;
			}
			int num = 1 + Encoding.UTF8.GetByteCount(managedString);
			byte[] array = new byte[num];
			Encoding.UTF8.GetBytes(managedString, 0, managedString.Length, array, 0);
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			Marshal.Copy(array, 0, intPtr, num);
			return intPtr;
		}

		// Token: 0x0600250E RID: 9486 RVA: 0x0008340C File Offset: 0x0008160C
		public static string StringFromUtf8Ptr(IntPtr ptr)
		{
			int num;
			return MemUtils.StringFromUtf8Ptr(ptr, out num);
		}

		// Token: 0x0600250F RID: 9487 RVA: 0x00083424 File Offset: 0x00081624
		public static string StringFromUtf8Ptr(IntPtr ptr, out int len)
		{
			len = 0;
			if (ptr == IntPtr.Zero)
			{
				return null;
			}
			len = MemUtils.StringPtrByteLen(ptr);
			if (len == 0)
			{
				return null;
			}
			byte[] array = new byte[len];
			Marshal.Copy(ptr, array, 0, len);
			return Encoding.UTF8.GetString(array);
		}

		// Token: 0x06002510 RID: 9488 RVA: 0x00083470 File Offset: 0x00081670
		public static int StringPtrByteLen(IntPtr ptr)
		{
			if (ptr == IntPtr.Zero)
			{
				return 0;
			}
			int num = 0;
			while (Marshal.ReadByte(ptr, num) != 0)
			{
				num++;
			}
			return num;
		}
	}
}
