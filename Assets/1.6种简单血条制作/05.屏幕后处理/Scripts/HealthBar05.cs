
using System;

namespace QFramework.Mofelor
{
	public partial class HealthBar05 : ViewController
	{
		void Start()
		{
			Btn.onClick.AddListener(() =>
			{
				if(MainCameraVolume.weight + 0.3f<=0.9){
					MainCameraVolume.weight += 0.3f;
				}else{
					//最大值1，超过后可判定死亡
					MainCameraVolume.weight = 0.9f;
				}
				StartFadeAction(null);
			});
		}


		private IActionController mFadeAction;
		public void StartFadeAction(Action action){
			if(mFadeAction != null ){
				mFadeAction.Deinit();
				mFadeAction = null;
			}

			mFadeAction = 
			ActionKit.Lerp(MainCameraVolume.weight,0,2f,e=>{
				MainCameraVolume.weight = e;
			})
			.Start(this,()=>{
				action?.Invoke();
				mFadeAction = null;
			});	
		}
	}
}
