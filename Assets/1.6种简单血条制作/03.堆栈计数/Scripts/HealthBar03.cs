
using System.Collections;
using UnityEngine;


namespace QFramework.Mofelor
{
	public partial class HealthBar03 : ViewController
	{
		private BindableProperty<int> Health = new BindableProperty<int>(5);
		private BindableProperty<int> MaxHealth = new BindableProperty<int>(10);

		//每一格表示血量
		private int PerHealth = 2;

		private Stack Health1 = new Stack();
		private Stack Health2 = new Stack();

		void Start()
		{
			//初始化
			ChangeMaxHealth(Style01,BG1,Health1);
			ChangeMaxHealth(Style02,BG2,Health2);
			ChangeHealth(Health1);
			ChangeHealth(Health2);

			//注册Health变化监听
			Health.Register(e => {
				ChangeHealth(Health1);
				ChangeHealth(Health2);
			}).UnRegisterWhenGameObjectDestroyed(this);
			//注册MaxHealth变化监听
			MaxHealth.Register(e => {
				ChangeMaxHealth(Style01,BG1,Health1);
			    ChangeMaxHealth(Style02,BG2,Health2);
				ChangeHealth(Health1);
				ChangeHealth(Health2);
			}).UnRegisterWhenGameObjectDestroyed(this);

			//+1
			Btn1.onClick.AddListener(()=>{
				if(Health.Value+1 <= MaxHealth.Value) Health.Value +=1;	
			});
			//-2
			Btn2.onClick.AddListener(()=>{
				if(Health.Value-2 >= 0) Health.Value-=2;	
			});
			//+2Max
			Btn3.onClick.AddListener(()=>{
				MaxHealth.Value += PerHealth;	
			});
			//-2Max	
			Btn4.onClick.AddListener(()=>{
				MaxHealth.Value -= PerHealth;
				if(MaxHealth.Value < Health.Value) Health.Value = MaxHealth.Value;
			});
			
		}

		public void AddNew(Style style,Component parent,Stack health){
			//单个血条对象创建应从对象池获取，此处只做示例
			var _style =  style.InstantiateWithParent(parent);
			_style.ChangeSprite(0);
			_style.Show();
			health.Push(_style);
		}

		public void RemoveOld(Stack health){
			var _style = health.Pop() as Style;
			//单个血条对象销毁应归还对象池，此处只做隐藏处理
			_style.Hide();
		}

		public void ChangeMaxHealth(Style style,Component parent,Stack health){
			if(health.Count * PerHealth > MaxHealth.Value){
				RemoveOld(health);	
				ChangeMaxHealth(style,parent,health);
			}else if(health.Count * PerHealth < MaxHealth.Value){
				AddNew(style,parent,health);
				ChangeMaxHealth(style,parent,health);
			}
			//等于的时候不刷新
		}

		//栈遍历，整体刷新，ui反向排列
		//(使用栈的特性可优化刷新次数，但是up懒的写了)
		private void ChangeHealth(Stack health){
			int i=0;
			foreach(Style style in health){
				if(Health.Value-i >= 2){
					style.ChangeSprite(2);
					i += 2;
				}
				else if(Health.Value - i == 1){
					style.ChangeSprite(1);
					i += 1;
				}
				else{
					style.ChangeSprite(0);
				}
			}
		}
	}
}
