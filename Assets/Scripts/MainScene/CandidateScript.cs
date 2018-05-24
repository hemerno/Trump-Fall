using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CandidateScript : MonoBehaviour
{
    // public float minVelocityY, maxVelocityY;




    public float minVelocityY, maxVelocityY;        // speed range      2 -- 5
    public int IceKingRange, RedRange, HolyRange, TimeTrmpRange;                 // Type rare range              100 и 400!!!!
    public GameObject PepeCircle;
    GameObject localPepeCircle;


    TrumpOPool TrumpsPool;      // Cached components block
    SpriteRenderer SpRend;
    FallingScript FallScript;
    BoxCollider2D CandidateCollider;
    TapScript Manager;
    Animator TrmpAnimator;
    Rigidbody2D Rb2D;
    GameObject Storage;


    public int prezState;
    float TrumpType;
    public int RedSpawnRate, GreenSpawnRate, TimeSpawnRate, HolySpawnRate, IceKingSpawnRate;
    int RedHighBoard, GreenHighBoard, TimeHighBoard, HolyHighBoard, IceKingHighBoard;


    public float RotateTimer;       // Movement block
    float currentRotateTimer;
    bool tilt = true;
    Vector3 RotateVector = Vector3.zero;
    int FloatWay;       // Вариации падения


    public GameObject ExpoTrump;    // Red block


    bool greenDyingBool = false;    // Green block
    float t, alpha;


    bool Iced;                  // Ice Block
    GameObject localIce;

   


    Vector2 velocityMember;                 // global stop block
    bool GlobalStop = false;
    GlobalTrumpVelocityZeroScript GlobalVelocity;

    bool HolyReact = false; // Реакция на святой тип           // holy block
    int HolyLocalX; // Направление поворота 
    [HideInInspector] public bool alreadyDead = false;

    bool IceKingReact = false;


    //death block
    public bool endGameStage;



    // Audio Block
    SFXScript MusicScrpt;



    void Start()
    {
        TrmpAnimator = GetComponent<Animator>();                                    // Кеширование компонентов
        Manager = GameObject.Find("Manager").GetComponent<TapScript>();
        GlobalVelocity = Manager.GetComponent<GlobalTrumpVelocityZeroScript>();
        FallScript = GameObject.Find("Manager").GetComponent<FallingScript>();
        CandidateCollider = GetComponent<BoxCollider2D>();
        TrumpsPool = GameObject.Find("Manager").GetComponent<TrumpOPool>();
        SpRend = GetComponent<SpriteRenderer>();
        Rb2D = GetComponent<Rigidbody2D>();
        MusicScrpt = GameObject.Find("MusicBox").GetComponent<SFXScript>();
        Storage = GameObject.Find("Trumps");

        currentRotateTimer = RotateTimer;           // Установка начальных пременных и границ спауна трампов
        IceKingHighBoard = IceKingSpawnRate ;
        HolyHighBoard = IceKingHighBoard + HolySpawnRate;
        TimeHighBoard = HolyHighBoard + TimeSpawnRate;
        GreenHighBoard = TimeHighBoard + GreenSpawnRate;
        RedHighBoard = GreenHighBoard + RedSpawnRate;


        

        Initialization();





    }

    public void Initialization()
    {
        GlobalStop = false;
        endGameStage = false;
        if (transform.childCount > 0)
        {
            TrumpsPool.IceGoToThePool(localIce);
        }
        TrmpAnimator.Rebind();
        // TrmpAnimator.Play("NullState");
        //dieCommand = false;
        alreadyDead = false;
        greenDyingBool = false;
        SpRend.color = new Color(255, 255, 255, 1);
        CandidateCollider.enabled = true;

        velocityMember = new Vector2(0, -Random.Range(minVelocityY, maxVelocityY));     // Запись скорости в случае ее потери при глобальной остановке

        if (!Manager.ImmortalMode)
        {
            Rb2D.velocity = velocityMember;          // Придание ускорения вниз  
        }
        else
        {
            GlobalStop = true;
            print("Невозможный спаун");
        }


        Rb2D.angularVelocity = 0;



        if (Random.Range(1, 101) <= 10)      // Добавление льда   10%
        {
            Iced = true;
            localIce = TrumpsPool.IcePlease(gameObject);
        }


         CreateTrumpType();


        if (transform.parent != Storage)
            transform.SetParent(Storage.transform);

        

        if (FloatWay == 1)
            transform.rotation = Quaternion.AngleAxis(11, Vector3.back);        // Нанокостыль (Поворот влево-вправо не смотрится должным образом без этого) 

        /*  if (Manager.DeathMode)
          {
              endGameStage = true;
              End_x = Random.Range(0, 2);
          }*/
    }


    void FixedUpdate()
    {

        if (!greenDyingBool)
        {




           
            if (!GlobalStop & !alreadyDead)              // В случае глобальной остановки необходимо прекратить все движения объекта
                MovementSwitcher();     // Метод для передвижения, спрятан для удобства восприятия
            else
            {
                if (HolyReact)
                    HolyRound();
                if (endGameStage)
                    TurnToNothing();
            }



        }
        else
            Dying();

    }

    void Dying()
    {
        /*if (x == 0)
            transform.Rotate(new Vector3(0, 0, 4));
        else
            transform.Rotate(new Vector3(0, 0, -4));
        alpha = Mathf.Lerp(1, 0, t);
        //SpRend.color = new Color(SpRend.color.r, SpRend.color.g, SpRend.color.b, alpha);  new trumps
        t += Time.fixedDeltaTime;

        if (t > 1)
        {*/

        // Данный метод предназначен только для зеленого при dying = true

        alpha = Mathf.Lerp(1, 0, t);

        SpRend.color = new Color(SpRend.color.r, SpRend.color.g, SpRend.color.b, alpha);
        t += Time.fixedDeltaTime;


    }


    public void PrepareToDie()          // Метод вызывается при тапе или при колизией со взрывом
    {
        if (!Iced)
        {
            
                alreadyDead = true;
                CandidateCollider.enabled = false;
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            
            switch (prezState)
            {
                case 1:
                    {
                        MusicScrpt.TrumpDeathrattle(1);
                        TrmpAnimator.Play("OrangeAnim");
                        //TrmpAnimator.SetInteger("PrezState", prezState * 10);
                        break;
                    }

                case 2:
                    {
                        //TrmpAnimator.Play("RedAnim");
                        MusicScrpt.TrumpDeathrattle(2);
                        Instantiate(ExpoTrump, transform.position + Vector3.forward, transform.rotation);
                        GoToThePoolAnimEvent();
                        // See also RedEvent
                        //TrmpAnimator.SetInteger("PrezState", prezState * 10);
                        break;
                    }

                case 3:
                    {
                        MusicScrpt.TrumpDeathrattle(3);
                        alpha = 1;
                        t = 0;
                        TrmpAnimator.Play("GreenAnim");
                        //TrmpAnimator.SetInteger("PrezState", prezState * 10);
                        transform.rotation = Quaternion.identity;
                        break;
                    }

                case 4:
                    {
                        MusicScrpt.TrumpDeathrattle(4);
                        //TrmpAnimator.SetInteger("PrezState", prezState * 10);
                        TrmpAnimator.Play("WhiteStart");
                        break;
                    }

                case 5:
                    {
                        MusicScrpt.TrumpDeathrattle(5);
                        TrmpAnimator.Play("PepeAction");
                        if (!Manager.ImmortalMode)
                        {
                            GlobalVelocity.StopHammerTime(HolyType: true, HolyTrigger: gameObject);   
                        }
                        break;
                    }

                case 6:
                    {
                        MusicScrpt.TrumpDeathrattle(6);
                        TrmpAnimator.Play("IceKingAnim");
                        if (!Manager.ImmortalMode)
                        {
                            MusicScrpt.TrumpDeathrattle(6);
                            GlobalVelocity.StopHammerTime(IceKingType: true);   
                        }
                        
                        break;
                    }

            }
            Manager.TrumpsKilled += 1;
            Manager.TapCount += 1;


        }
        else
        {
            IceBreakerMethod();
        }
    }


    public void GoToThePoolAnimEvent()        // Переход осуществляется по встроенным в анимацию ивентам, вызывается для корректного ухода в пул
    {
        alreadyDead = true;
        if (Iced)
        {
            IceBreakerMethod(false);
        }

        TrumpsPool.TrumpGoToThePool(gameObject);
    }


    void GreenDeath()
    {
        TrmpAnimator.Play("DollarIdle");
        Manager.TapCount += 5;
        greenDyingBool = true;
        Invoke("GoToThePoolAnimEvent", 1f);
    }


    public void GreenCuteLiftUp()                // Подъем после обращения в доллар
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 3);
    }


    void BlueSlowDown()     // Глобальное замедление времени
    {

            Manager.GetComponent<TimeSlowScript>().Slowing();
    }


   


   

   /* void RedEvent()
    {
        Instantiate(ExpoTrump, transform.position + Vector3.forward, transform.rotation);
        GoToThePoolAnimEvent();
    }*/


    void MovementSwitcher()         // Метод для передвижения, спрятан для удобства восприятия
    {
        switch (FloatWay)           // Виды движения
        {
            case 1:
                {
                    transform.Rotate(RotateVector);                          // Движение маятника
                    currentRotateTimer -= Time.fixedDeltaTime;
                    if (currentRotateTimer <= 0)
                    {
                        if (tilt)
                            RotateVector = new Vector3(0, 0, 2);
                        else
                            RotateVector = new Vector3(0, 0, -2);
                        tilt = !tilt;
                        currentRotateTimer = RotateTimer;
                    }
                    break;

                }
            case 2:
                {
                    transform.Rotate(new Vector3(0, 0, 1));         // Поворот по часовой
                    break;
                }

            case 3:
                {
                    transform.Rotate(new Vector3(0, 0, -1));        // Против часовой
                    break;
                }

            case 4:
                {
                    currentRotateTimer -= Time.fixedDeltaTime;      // Отражение спрайта
                    if (currentRotateTimer <= 0)
                    {
                        SpRend.flipX = !SpRend.flipX;
                        currentRotateTimer = RotateTimer * 1.5f;
                    }
                    break;
                }
            case 5:
                {

                    break;                      // Ни-че-го
                }


        }
    }


    public void MyGlobalStopReact(float velocityY = 0, bool holyType = false, bool iceKingType = false)         // Реакция на глобальную остановку скорости трампов     0.8 сек на паузу
    {
        if (!alreadyDead)
            if (Manager.ImmortalMode)
            {
                Rb2D.velocity = new Vector2(0, velocityY);
                GlobalStop = true;
                if (Manager.DeathMode)
                    endGameStage = true;
                if (endGameStage)
                {
                    End_x = Random.Range(0, 2);
                    IceBreakerMethod(false);
                    CandidateCollider.enabled = false;
                    return;
                }
                HolyReact = holyType;
                if (HolyReact)
                {
                    // CandidateCollider.enabled = false;       проверка на ускорение геймплея 
                    HolyLocalX = Random.Range(0, 2);
                    if (HolyLocalX != 1)
                        HolyLocalX = -1;
                    Invoke("HolyMidAirReact", 0.4f);
                    return;
                }


                IceKingReact = iceKingType;
                if (IceKingReact)
                {
                    if (!Iced)
                    {
                        Iced = true;
                        localIce = TrumpsPool.IcePlease(gameObject);
                        localIce.GetComponent<IceDestroyScript>().ReverciveMethod();
                    }
                    return;
                }

            }
            else
            {
                if (!endGameStage)
                {
                    if (Iced)
                        velocityMember *= 0.7f;
                    Rb2D.velocity = velocityMember;
                    GlobalStop = false;
                    CandidateCollider.enabled = true;
                    HolyReact = false;
                    IceKingReact = false;
                }
            }
    }

    void HolyRound()            // поворот на 360 градусов
    {
        transform.Rotate(new Vector3(0, 0, 9f * HolyLocalX));          // 7.2 = 360 градусов / 50 (кол -во вызовов fixedUpdate в сек)   9 для 0.8
    }

    void HolyMidAirReact()          // Обращение в обычного 
    {
        prezState = 1;
        TrmpAnimator.SetInteger("PrezState", prezState);
        if (Iced)
        {
            IceBreakerMethod();
        }

    }


    void CreateTrumpType()
    {
        TrumpType = Random.Range(1, 101);               // orange - 1; 12 % red - 2; 18% green - 3; 5% time - 4; 2% holy - 5 death = x * 10
        if (TrumpType > RedHighBoard)
            prezState = 1;
        else
        if (TrumpType > GreenHighBoard & TrumpType <= RedHighBoard)
        {
            if (Manager.TapCount >= RedRange)
                prezState = 2;
            else prezState = 1;
        }
        else
        if (TrumpType > TimeHighBoard & TrumpType <= GreenHighBoard)
            prezState = 3;
        else
        if (TrumpType > HolyHighBoard & TrumpType <= TimeHighBoard)
        {
            if (Manager.TapCount >= TimeTrmpRange)
                prezState = 4;
            else prezState = 1;
        }
        else
        if (TrumpType > IceKingHighBoard & TrumpType <= HolyHighBoard)
            if (Manager.TapCount >= HolyRange)
                prezState = 5;
            else prezState = 1;
        else
        if ( TrumpType <= IceKingHighBoard)
            if (Manager.TapCount >= IceKingRange)
                prezState = 6;
            else prezState = 1;
        
        else
            prezState = 1;


        TrmpAnimator.SetInteger("PrezState", prezState);
    }


    void IceBreakerMethod(bool sound = true)     // Соответсвующее удаление льда
    {
        if (Iced)
        {
            Iced = false;
            localIce.GetComponent<IceDestroyScript>().CrashTheIce(); ;
            if (sound)
                MusicScrpt.TrumpDeathrattle(0);
        }
    }


    int End_x;
    void TurnToNothing()        // Исчезновение трампов при проигрыше
    {

        if (End_x == 0)
            transform.Rotate(new Vector3(0, 0, 10));
        else
            transform.Rotate(new Vector3(0, 0, -10));
        alpha = Mathf.Lerp(1, 0, t);



        SpRend.color = new Color(SpRend.color.r, SpRend.color.g, SpRend.color.b, alpha);
        t += Time.fixedDeltaTime;

        if (t > 1)
        {
            endGameStage = false;
            t = 0;
            GoToThePoolAnimEvent();
        }
    }
}
