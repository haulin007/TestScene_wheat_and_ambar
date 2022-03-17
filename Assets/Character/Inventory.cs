using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    [Range(10, 100)]
    private int _capasity = 10;
    [SerializeField]
    private int _costOfStack = 15;
    [SerializeField]
    private float _radiusForSelling = 1.0f;
    [SerializeField]
    private string _sallingTag = "Ambar";
    [SerializeField]
    private GameObject _inventoryBox;
    [SerializeField]
    private UIcontroller _UI;

    private float sellingCooldown = 0.3f;
    private GameObject _inventoryBoxOnScene;
    private MoveController characterMoveController;
    private PlayerState _characterState = PlayerState.Idle;
    private bool _inSalling = false;

    [SerializeField]
    private int _itemNum = 0;
    
    private void Awake()
    {
        characterMoveController = gameObject.GetComponent<MoveController>();
    }
    private void Update()
    {
        if (_itemNum != 0)
        {
            if (characterMoveController.GetState() != _characterState)
            {
                _characterState = characterMoveController.GetState();
                if (_characterState == PlayerState.Idle)
                    _inventoryBoxOnScene.GetComponent<Animator>().SetBool("Move", false);
                else
                    _inventoryBoxOnScene.GetComponent<Animator>().SetBool("Move", true);
            }
        }

        if (!_inSalling)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(gameObject.transform.position, _radiusForSelling);
            if (colliders.Length > 1)
            {
                foreach (Collider2D collider in colliders)
                {
                    if (collider.CompareTag(_sallingTag))
                    {
                        if (_itemNum > 0)
                            StartCoroutine(Remove());
                    }
                }
            }
        }
    }
    public bool Add()
    {
        if(_itemNum >= _capasity)
            return false;
        else
        {
            _itemNum++;
            if (_itemNum == 1) _inventoryBoxOnScene = Instantiate(_inventoryBox, gameObject.transform);
            return true;
        }
    }
    private IEnumerator Remove()
    {
        _inSalling = true;

        _itemNum--;
        _UI.GetGem(_costOfStack);

        if (_itemNum == 0) Destroy(_inventoryBoxOnScene);

        yield return new WaitForSeconds(sellingCooldown);

        _inSalling = false;
    }
}
