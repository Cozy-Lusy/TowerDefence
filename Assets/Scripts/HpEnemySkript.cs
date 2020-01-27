using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HpEnemySkript : MonoBehaviour
{
    public GameObject enemy;
    public int hp = 30;

    public void Damage(int countDmg)
    {
        hp -= countDmg;
    }

    void Update()
    {
        //Количество жизней врага располагаем в той же позиции с ним
        GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(enemy.transform.position);
        GetComponent<TextMeshProUGUI>().text = hp.ToString();
    }
}
