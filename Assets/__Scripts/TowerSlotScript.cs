using UnityEngine;

public class TowerSlotScript : MonoBehaviour
{
    [Header("Set in Inspector")]
    [SerializeField] private GameObject _towerPrefab = null;
    public bool IsSlotEmpty = true;

    private void OnMouseDown()
    {
        if (IsSlotEmpty)
        {
            GameObject tower = Instantiate(_towerPrefab, transform.position, Quaternion.identity);
            IsSlotEmpty = false;
        }
    }
}
//Добавить скрипт удаления башен и меню с выбором башен.