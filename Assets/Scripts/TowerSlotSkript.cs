using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSlotSkript : MonoBehaviour
{
    public GameObject tower;

    public bool isSlotEmpty = true;
    void OnMouseDown()
    {
        //Если слот пустой устанавливаем на него башню и указываем, что слот не пустой
        if (isSlotEmpty)
        {
            Instantiate(tower, transform.position, Quaternion.identity);
            isSlotEmpty = false;
        }
    }
}

//Добавить скрипт удаления башен и меню с выбором башен.
