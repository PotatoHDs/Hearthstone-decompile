using UnityEngine;

[CustomEditClass]
public class AdventureWingProgressDisplay : MonoBehaviour
{
	public delegate void OnAnimationComplete();

	public virtual void UpdateProgress(WingDbId wingDbId, bool linearComplete)
	{
	}

	public virtual bool HasProgressAnimationToPlay()
	{
		return false;
	}

	public virtual void PlayProgressAnimation(OnAnimationComplete onAnimComplete = null)
	{
		onAnimComplete?.Invoke();
	}
}
