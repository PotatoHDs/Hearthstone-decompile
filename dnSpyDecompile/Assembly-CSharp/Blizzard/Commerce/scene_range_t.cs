using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	// Token: 0x0200123D RID: 4669
	public class scene_range_t : IDisposable
	{
		// Token: 0x0600D149 RID: 53577 RVA: 0x003E14B0 File Offset: 0x003DF6B0
		internal scene_range_t(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600D14A RID: 53578 RVA: 0x003E14CC File Offset: 0x003DF6CC
		internal static HandleRef getCPtr(scene_range_t obj)
		{
			if (obj != null)
			{
				return obj.swigCPtr;
			}
			return new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600D14B RID: 53579 RVA: 0x003E14E4 File Offset: 0x003DF6E4
		~scene_range_t()
		{
			this.Dispose(false);
		}

		// Token: 0x0600D14C RID: 53580 RVA: 0x003E1514 File Offset: 0x003DF714
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600D14D RID: 53581 RVA: 0x003E1524 File Offset: 0x003DF724
		protected virtual void Dispose(bool disposing)
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						battlenet_commercePINVOKE.delete_scene_range_t(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		// Token: 0x170010BE RID: 4286
		// (get) Token: 0x0600D14F RID: 53583 RVA: 0x003E15AA File Offset: 0x003DF7AA
		// (set) Token: 0x0600D14E RID: 53582 RVA: 0x003E159C File Offset: 0x003DF79C
		public int from
		{
			get
			{
				return battlenet_commercePINVOKE.scene_range_t_from_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.scene_range_t_from_set(this.swigCPtr, value);
			}
		}

		// Token: 0x170010BF RID: 4287
		// (get) Token: 0x0600D151 RID: 53585 RVA: 0x003E15C5 File Offset: 0x003DF7C5
		// (set) Token: 0x0600D150 RID: 53584 RVA: 0x003E15B7 File Offset: 0x003DF7B7
		public int to
		{
			get
			{
				return battlenet_commercePINVOKE.scene_range_t_to_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.scene_range_t_to_set(this.swigCPtr, value);
			}
		}

		// Token: 0x0600D152 RID: 53586 RVA: 0x003E15D2 File Offset: 0x003DF7D2
		public scene_range_t() : this(battlenet_commercePINVOKE.new_scene_range_t(), true)
		{
		}

		// Token: 0x0400A2B8 RID: 41656
		private HandleRef swigCPtr;

		// Token: 0x0400A2B9 RID: 41657
		protected bool swigCMemOwn;
	}
}
