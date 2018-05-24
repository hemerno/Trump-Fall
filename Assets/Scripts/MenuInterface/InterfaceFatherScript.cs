using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceFatherScript : MonoBehaviour {

    public Vector3 HidePosition;
    public Vector3 ShowPosition;



    public static IEnumerator MoveInterface(bool MovementType, params GameObject[] Buttons)       //            Унаследовать интерфейс не получаица =(((((((
    {


        float testT = Time.time;
        Vector3 localVEctor3;
        float timer = 0;

        foreach (GameObject element in Buttons)
        {
            

            if (element.GetComponent<Button>() != null)
                element.GetComponent<Button>().interactable = MovementType;     // Кнопка выключается при сворачивании интерфейса

        }

        while (timer <1)
        {
            
            foreach (GameObject element in Buttons)
            {
                if (MovementType)
                    localVEctor3 = Vector3.Lerp(element.GetComponent<RectTransform>().anchoredPosition , element.GetComponent<InterfaceFatherScript>().ShowPosition , timer);
                else
                    localVEctor3 = Vector3.Lerp(element.GetComponent<RectTransform>().anchoredPosition, element.GetComponent<InterfaceFatherScript>().HidePosition , timer);
                element.GetComponent<RectTransform>().anchoredPosition = localVEctor3;
            }
            timer += Time.fixedDeltaTime * 0.1f;
            yield return new WaitForFixedUpdate();
        }

    }
}
