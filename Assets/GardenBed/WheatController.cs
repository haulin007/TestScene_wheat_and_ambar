using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PlantState 
{ 
    Full,
    Half,
    Empty
}
public class WheatController : MonoBehaviour
{
    [SerializeField]
    PlantInfo _plantInfo;
    [SerializeField]
    Animator animator;
    [SerializeField]
    private GameObject _stackPrefab;

    private int _health;
    private GameObject motherGarden;
    private int plantindex;
    public void SetGardenLink(GameObject link)
    {
        motherGarden = link;
    }
    public void SetPlantIndex(int index)
    {
        plantindex = index;
    }
    private void Start()
    {
        _health = _plantInfo._wheatHealt;
    }
    public void Cut()
    {
        if(_health > 0) _health--;

        if(_health == 0)
        {
            GetWheat();
            animator.SetInteger("State", (int)PlantState.Empty);
            GardenGrow();
            Destroy(gameObject);
        }
        else if (_health == _plantInfo._wheatHealt / 2)
        {
            GetWheat();
            animator.SetInteger("State", (int)PlantState.Half);
        }
    }
    private void GardenGrow()
    {
        motherGarden.GetComponent<GardenMenager>().Grow(plantindex);
    }
    private void GetWheat()
    {
        Vector3 pos = new Vector3(transform.position.x + Random.Range(-1.0f, 1.0f),
                                    transform.position.y + Random.Range(-1.0f, 1.0f));
        Instantiate(_stackPrefab, pos, Quaternion.identity);
    }
}
