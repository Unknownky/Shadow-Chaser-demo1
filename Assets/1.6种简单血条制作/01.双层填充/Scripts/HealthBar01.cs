
using UnityEngine;

namespace QFramework.Mofelor
{
	public partial class HealthBar01 : ViewController
	{
		public BindableProperty<float> Health = new BindableProperty<float>(100);
		public float MaxHealth = 100;

		void Start()
		{
			UpdateHealthBar(Health.Value/MaxHealth);

			Health.Register(e=>{
				UpdateHealthBar(Health.Value/MaxHealth);
			}).UnRegisterWhenGameObjectDestroyed(this);

			//+10
			Btn1.onClick.AddListener(()=>{
				if(Health.Value+10 <= MaxHealth){
					Health.Value += 10;
				}else{
					Health.Value = MaxHealth;
				}
			});

			//-10
			Btn2.onClick.AddListener(()=>{
				if(Health.Value - 10 >= 0){
					Health.Value -= 10;
				}else{
					Health.Value = 0;
				}
			});
		}


		//ActionKit可以用协程代替
		//动画叠加效果可参考05.屏幕后处理中的叠加写法
		public void UpdateHealthBar(float newPoint){

			var oldPoint = HealthP.fillAmount;

			if(newPoint>oldPoint){
				ActionKit.Lerp(oldPoint,newPoint,0.3f,(e)=>{
					HealthB.fillAmount = e;
				}).Start(this,()=>{
					HealthP.fillAmount = newPoint;
				});
			}else{
				HealthP.fillAmount = newPoint;
				ActionKit.Lerp(oldPoint,newPoint,0.3f,(e)=>{
					HealthB.fillAmount = e; 
				}).Start(this);
			}

			TextShow.text = Health.ToString()+"/"+MaxHealth.ToString()+" ";
		}

        public void AddHealth(float value)
        {
            Health.Value += value;
        }

        public void ReduceHealth(float value)
        {
            Health.Value -= value;
        }

        public void SetHealth(float value)
        {
            Mathf.Clamp(value, 0, MaxHealth);
            Health.Value = value;
        }
	}
}
