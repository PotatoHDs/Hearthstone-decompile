using System;

namespace bgs
{
	// Token: 0x02000253 RID: 595
	public class EventListener<Delegate>
	{
		// Token: 0x060024C8 RID: 9416 RVA: 0x00082090 File Offset: 0x00080290
		public override bool Equals(object obj)
		{
			EventListener<Delegate> eventListener = obj as EventListener<Delegate>;
			if (eventListener == null)
			{
				return base.Equals(obj);
			}
			return this.m_callback.Equals(eventListener.m_callback) && this.m_userData == eventListener.m_userData;
		}

		// Token: 0x060024C9 RID: 9417 RVA: 0x000820E0 File Offset: 0x000802E0
		public override int GetHashCode()
		{
			int num = 23;
			if (this.m_callback != null)
			{
				num = num * 17 + this.m_callback.GetHashCode();
			}
			if (this.m_userData != null)
			{
				num = num * 17 + this.m_userData.GetHashCode();
			}
			return num;
		}

		// Token: 0x060024CA RID: 9418 RVA: 0x00002654 File Offset: 0x00000854
		public EventListener()
		{
		}

		// Token: 0x060024CB RID: 9419 RVA: 0x0008212E File Offset: 0x0008032E
		public EventListener(Delegate callback, object userData)
		{
			this.m_callback = callback;
			this.m_userData = userData;
		}

		// Token: 0x060024CC RID: 9420 RVA: 0x00082144 File Offset: 0x00080344
		public Delegate GetCallback()
		{
			return this.m_callback;
		}

		// Token: 0x060024CD RID: 9421 RVA: 0x0008214C File Offset: 0x0008034C
		public void SetCallback(Delegate callback)
		{
			this.m_callback = callback;
		}

		// Token: 0x060024CE RID: 9422 RVA: 0x00082155 File Offset: 0x00080355
		public object GetUserData()
		{
			return this.m_userData;
		}

		// Token: 0x060024CF RID: 9423 RVA: 0x0008215D File Offset: 0x0008035D
		public void SetUserData(object userData)
		{
			this.m_userData = userData;
		}

		// Token: 0x04000F4A RID: 3914
		protected Delegate m_callback;

		// Token: 0x04000F4B RID: 3915
		protected object m_userData;
	}
}
