
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Mofelor
{
	public partial class HealthBar02 : ViewController
	{
		//当前血量、每格血量、总血量
		public float Health = 2;
		public float PreHealth = 2;
		public float TotHealth = 100;

		//背景色及计数
		public int ColorIndex = 0;
		public List<string> Color1 = new List<string>(){"#FF0000","#00FF00","#0000FF","#FFFF00","#00FFFF","#FF00FF"};
		
		void Start(){
			HealthC.color = GetColor();
			TextShow.text ="X"+(int)(TotHealth/PreHealth) +" ";

			Btn1.onClick.AddListener(() => {if(TotHealth >= 5) UpdateAttack(5);});
			Btn2.onClick.AddListener(() => {if(TotHealth >= 1) UpdateAttack(1);});	
		}

		//ActionKit可以用协程代替
		//动画叠加效果可参考05.屏幕后处理中的叠加写法
		public void UpdateAttack(float Attack){
			if(Attack>PreHealth){
				UpdateHealthBar(Attack-PreHealth);
			}else if(Health>Attack){
				Health -= Attack;
				HealthP.fillAmount = Health/PreHealth;
				ActionKit.Lerp(HealthB.fillAmount,Health/PreHealth,0.15f,(e)=>{HealthB.fillAmount = e;}).Start(this);
			}else{
				if(TotHealth<=Attack){
					Health = 0;
				}else{
					Health =Health + PreHealth - Attack;
				}
				UpdateHealthBar(0);
			}
		}

		public void UpdateHealthBar(float last){
			ActionKit.Sequence()
				.Callback(()=>{HealthP.fillAmount = 0;})
				.Lerp(HealthB.fillAmount,0,0.15f,(e)=>{HealthB.fillAmount = e;})
				.Callback(()=>{
					TotHealth -= PreHealth;
					
					TextShow.text ="X"+(int)(TotHealth/PreHealth) +" ";
					HealthP.fillAmount = Health/PreHealth;				
					HealthP.color = HealthC.color;
					if(TotHealth <= PreHealth){
						HealthC.color = Color.black;
					}else{
						HealthC.color = GetColor();
					}
				})
				.Lerp(1,Health/PreHealth,0.15f,(e)=>{HealthB.fillAmount = e;})
			.Start(this,()=>{
				if(last != 0 ) UpdateAttack(last);
			});
		}

		public Color GetColor(){

			if(ColorIndex+1<Color1.Count){
				ColorIndex +=1;	
			}else{
				ColorIndex = 0;
			}

			ColorUtility.TryParseHtmlString(Color1[ColorIndex],out Color color);
			
			return color;
		}
	}
}
