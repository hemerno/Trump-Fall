using UnityEngine;
using System.Collections;

public class CloudMove : MonoBehaviour
{
    public GameObject CloudPrefab;
    public Sprite RightPlaneSprite, LeftPlaneSprite;
    public Sprite[] FirstPlanCloud;
    public Sprite[] SecondPlanCloud;
    public Sprite[] ThirdPlanCloud;
    float LeftBoard = -3, RightBoard = 3, OffsetForScreen = 4;      // Данные для расчета точек спауна и направления облаков
    float UpBoard = 4.4f, DownBoard = 0;
    public float Timer;
    public GameObject DirigiblePrefab;
    float currentTimer;

    public static bool unique = true;
    bool planeBool, dirigibleBool;
    void Awake()
    {
        if (unique)
        {
            unique = false;
        }
        else
            Destroy(this.gameObject);
    }

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        int sign, localTarget;
        foreach (Transform child in transform)          // Для уже существующих облаков создаем направления движения
        {
            if (Random.Range(0, 2) == 0)
            {
                sign = -1;
                localTarget = -7;
            }

            else
            {
                sign = 1;
                localTarget = 7;
            }

            Vector2 localVector = new Vector2(sign * (Random.value + 0.1f) * 0.1f, 0);
            child.GetComponent<Rigidbody2D>().velocity = localVector;
            //print(Mathf.Sqrt(Mathf.Pow(localTarget + gameObject.transform.position.x, 2)) / Mathf.Abs(localVector.x));
            Destroy(child.gameObject, Mathf.Sqrt(Mathf.Pow(localTarget + gameObject.transform.position.x, 2)) / Mathf.Abs(localVector.x));
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentTimer -= Time.fixedDeltaTime;
        if (currentTimer <= 0)
        {
            if (Random.Range(1, 100) < 20)
                planeBool = true;
            CreateCloud();
            
            currentTimer = Timer;
        }
    }

    void SetDirection(GameObject cloud)
    {
        int sign;
        if (cloud.GetComponent<Rigidbody2D>().velocity.x != 0)
        {

            if (cloud.GetComponent<Rigidbody2D>().velocity.x > 0)
                sign = -1;
            else
                sign = 1;
        }
        else
            sign = Random.Range(-1, 2);
        cloud.GetComponent<Rigidbody2D>().velocity = new Vector2(sign * Random.value * 0.1f, 0);
    }

    void CreateCloud()
    {
        float localY, localX, localZ = 10;
        Vector2 localVelocity;
        localY = Random.Range(UpBoard * 10, DownBoard * 10) / 10;       // Случайная точка на оси Y, цифры 10 увеличивают разброс
        if (Random.Range(0, 2) == 0)
        {
            localX = LeftBoard - OffsetForScreen;
            localVelocity = new Vector2((Random.value + 0.1f) * 0.1f, 0);
            if (planeBool)
            {
                if (Random.Range(0, 2) == 1)
                    dirigibleBool = true;
                if (!dirigibleBool)
                    localVelocity = new Vector2(2, 0);
                else
                    localVelocity = new Vector2(0.2f, 0);
            }
            //target = RightBoard + OffsetForScreen;
        }
        else
        {
            localX = RightBoard + OffsetForScreen;
            localVelocity = new Vector2(-(Random.value + 0.1f) * 0.1f, 0);
            if (planeBool)
            {
                if (Random.Range(0, 2) == 1)
                    dirigibleBool = true;
                if (!dirigibleBool)
                    localVelocity = new Vector2(-2, 0);
                else
                    localVelocity = new Vector2(-0.2f, 0);
            }
            //target = LeftBoard - OffsetForScreen;
        }
        // path anyway euqual 14
        Sprite HappySprite;
        int randomedInt = Random.Range(0, 101);
        GameObject HappyCloud = CloudPrefab;
        if (!planeBool)
        {
            if (randomedInt <= 30)
            {
                HappySprite = ThirdPlanCloud[Random.Range(0, ThirdPlanCloud.Length)];
                localZ = 7;
            }
            else
                if (randomedInt <= 65)
            {
                HappySprite = SecondPlanCloud[Random.Range(0, SecondPlanCloud.Length)];
                localZ = 6;
            }
            else
            {
                HappySprite = FirstPlanCloud[Random.Range(0, FirstPlanCloud.Length)];
                localZ = 5;
            }

            HappyCloud = (GameObject)Instantiate(CloudPrefab, new Vector3(localX, localY, localZ), Quaternion.identity);
            HappyCloud.GetComponent<SpriteRenderer>().sprite = HappySprite;
        }
        else
        {
            planeBool = false;
            
            localZ = 5.5f;
            if (!dirigibleBool)        // Добавить второй самолет
            {
                HappyCloud = (GameObject)Instantiate(CloudPrefab, new Vector3(localX, localY, localZ), Quaternion.identity);
                HappyCloud.GetComponent<SpriteRenderer>().sprite = LeftPlaneSprite;
                if (localVelocity.x > 0)
                    HappyCloud.GetComponent<SpriteRenderer>().sprite = RightPlaneSprite;
            }
            else
            {
                HappyCloud = (GameObject)Instantiate(CloudPrefab, new Vector3(localX, localY, localZ), Quaternion.identity);
                GameObject Dir = Instantiate(DirigiblePrefab, new Vector3(localX, localY, localZ), Quaternion.identity);
                HappyCloud.GetComponent<SpriteRenderer>().sprite = null;
                Dir.transform.SetParent(HappyCloud.transform);
                if (localVelocity.x < 0)
                    Dir.GetComponent<dirigibleScript>().ChangeDirection();
            }
            dirigibleBool = false;
        }



        // Создание объекта по сгенерированным параметрам

        HappyCloud.transform.SetParent(gameObject.transform);
        HappyCloud.GetComponent<Rigidbody2D>().velocity = localVelocity;


        Destroy(HappyCloud, 14 / Mathf.Abs(localVelocity.x));   // Уничтожение через время, за которое он пройдет 14 ед пространства

    }

  /*  void CreatePlane()
    {
        // от 4 до 0 по OY
        float localX
        Vector2 localVelocity;
        localY = Random.Range(UpBoard * 10, DownBoard * 10) / 10;       // Случайная точка на оси Y, цифры 10 увеличивают разброс
        if (Random.Range(0, 2) == 0)
        {
            localX = LeftBoard - OffsetForScreen;
            localVelocity = new Vector2((Random.value + 0.1f) * 0.1f, 0);
            //target = RightBoard + OffsetForScreen;
        }
        else
        {
            localX = RightBoard + OffsetForScreen;
            localVelocity = new Vector2(-(Random.value + 0.1f) * 0.1f, 0);
            //target = LeftBoard - OffsetForScreen;
        }
        // path anyway euqual 14
        Sprite HappySprite;
        int randomedInt = Random.Range(0, 101);

        if (randomedInt <= 30)
        {
            HappySprite = ThirdPlanCloud[Random.Range(0, ThirdPlanCloud.Length)];
            localZ = 7;
        }
        else
            if (randomedInt <= 65)
        {
            HappySprite = SecondPlanCloud[Random.Range(0, SecondPlanCloud.Length)];
            localZ = 6;
        }
        else
        {
            HappySprite = FirstPlanCloud[Random.Range(0, FirstPlanCloud.Length)];
            localZ = 5;
        }




        GameObject HappyCloud = (GameObject)Instantiate(CloudPrefab, new Vector3(localX, localY, localZ), Quaternion.identity);             // Создание объекта по сгенерированным параметрам
        HappyCloud.transform.SetParent(gameObject.transform);
        HappyCloud.GetComponent<SpriteRenderer>().sprite = HappySprite;
        HappyCloud.GetComponent<Rigidbody2D>().velocity = localVelocity;


        Destroy(HappyCloud, 14 / Mathf.Abs(localVelocity.x));   // Уничтожение через время, за которое он пройдет 14 ед пространства


    }*/
}
