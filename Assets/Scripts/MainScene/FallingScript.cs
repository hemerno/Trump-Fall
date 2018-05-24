using UnityEngine;
using System.Collections;

public class FallingScript : MonoBehaviour {

    public float LeftBoard, RightBoard; // Границы рамки спауна
    public float Height; // Высота,с которой они падают

    public float StartVelocity; // Начальное время время между кандидатами        // 0.6
    public float TargetVelocity; // Конечное время между кандидатами              // 0.12
    public float TargetTime; // время для достижения конечной скорострельности трампов в минутах        3!!!!
    float VelocityChangePerTick;    
    public float TickRate; // Шаг сложности, кол-во тиков в секунду
    public GameObject Trump;
    float Timer;
    float currentTimer;          // Таймер между кандидатами
    float currentDifTimer;      // Таймер сложности
    TrumpOPool TrumpsPool;

    float lerpt = 0;

    void Start () {
        TrumpsPool = GetComponent<TrumpOPool>();
        // VelocityChangePerTick = Mathf.Abs(TargetVelocity - StartVelocity) / (TargetTime * 60*TickRate);
        VelocityChangePerTick =  1 / (TargetTime * 60 * TickRate)  ;
        currentDifTimer = TickRate;
        Timer = StartVelocity;
        
    }
	

	void FixedUpdate () {
        currentTimer -= Time.fixedDeltaTime;        // для спауна новых трампов
        if (Timer != TargetVelocity)
            currentDifTimer -= Time.fixedDeltaTime;
      //  else
      //      print("Затраченное время " + Time.time);


        if (currentTimer <= 0)      // сам спаун
        {
            TrumpsPool.TrumpPlease(new Vector2(Random.Range(LeftBoard, RightBoard ), Height));
            currentTimer = Timer;
        }


        if (currentDifTimer <= 0)            // Наращивание сложности
        {
            currentDifTimer = TickRate;
            Timer = Mathf.Lerp(StartVelocity, TargetVelocity, lerpt);
            lerpt += VelocityChangePerTick;
         //   print(Timer + "  текущее время - " + Time.time + "      " + lerpt.ToString());
            /* if (Timer > TargetVelocity)
             {

                 Timer -= VelocityChangePerTick;
             }
             else
                 Timer = TargetVelocity;
             currentDifTimer = TickRate;*/
        }
      
	}


}
