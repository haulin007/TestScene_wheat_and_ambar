using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenMenager : MonoBehaviour
{
    [SerializeField]
    private float _growTime = 10.0f;
    [SerializeField]
    private Vector2Int _growCol;
    [SerializeField]
    private float _growunitSize = 1.0f;
    [SerializeField]
    private GameObject _wheatPrefab;
    [SerializeField]
    private Transform _root;
    [SerializeField]
    private Transform _gardenCorner;

    private Vector2[] _growPos;
    private void Start()
    {
        FindGrowPosition();
        GenerateWheat();
    }
    private void GenerateWheat()
    {
        for (int i = 0; i < _growPos.Length; i++)
        {
            GameObject plant =  Instantiate(_wheatPrefab, _growPos[i], Quaternion.identity, _root);
            plant.GetComponent<WheatController>().SetGardenLink(gameObject);
            plant.GetComponent<WheatController>().SetPlantIndex(i);
        }
    }
    private void FindGrowPosition()
    {
        _growPos = new Vector2[_growCol.x * _growCol.y];
        for (int i = 0; i < _growCol.y; i++)
            for (int j = 0; j < _growCol.x; j++)
            {
                _growPos[i * _growCol.x + j] = new Vector2(_gardenCorner.position.x + _growunitSize * j,
                                            _gardenCorner.position.y + _growunitSize * i);
            }

    }
    private IEnumerator GrowInIndex(int index)
    {
        yield return new WaitForSeconds(_growTime);
        GameObject plant =  Instantiate(_wheatPrefab, _growPos[index], Quaternion.identity, _root);
        plant.GetComponent<WheatController>().SetGardenLink(gameObject);
        plant.GetComponent<WheatController>().SetPlantIndex(index);
    }
    public void Grow(int indexForPlant)
    {
        StartCoroutine(GrowInIndex(indexForPlant));
    }
}
