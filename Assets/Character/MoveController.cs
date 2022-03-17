using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Run,
    Hit
}
public class MoveController : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 4.0f;
    [SerializeField]
    private float _hitRadius = 2.0f;
    [SerializeField]
    public bool _canMove { get; set; }
    [SerializeField]
    private VariableJoystick _joystock;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private PlayerState _state = PlayerState.Idle;

    public bool HitIsPressed {get; set;}

    private void Start()
    {
        _canMove = true;
    }
    private void Update()
    {
        if (_canMove)
        {
            if (Input.GetKey(KeyCode.A) || _joystock.Horizontal < 0.0f)
            {
                transform.position += Vector3.left * Time.deltaTime * _moveSpeed;
                _state = PlayerState.Run;
                if (spriteRenderer.flipX == true)
                    spriteRenderer.flipX = false;
                animator.SetInteger("State", (int)_state);
            }
            if (Input.GetKey(KeyCode.D) || _joystock.Horizontal > 0.0f) 
            {
                transform.position += Vector3.right * Time.deltaTime * _moveSpeed;
                _state = PlayerState.Run;
                spriteRenderer.flipX = true;
                animator.SetInteger("State", (int)_state);
            }
            if (Input.GetKey(KeyCode.W) || _joystock.Vertical > 0.0f)
            { 
                transform.position += Vector3.up * Time.deltaTime * _moveSpeed;
                _state = PlayerState.Run;
                animator.SetInteger("State", (int)_state);
            }
            if (Input.GetKey(KeyCode.S) || _joystock.Vertical < 0.0f) 
            {
                transform.position += Vector3.down * Time.deltaTime * _moveSpeed;
                _state = PlayerState.Run;
                animator.SetInteger("State", (int)_state);
            }
            if(!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) &&
                !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) &&
                _joystock.Vertical == 0.0f &&  _joystock.Horizontal == 0.0f)
            {
                _state = PlayerState.Idle;
                animator.SetInteger("State", (int)_state);
            }
            if (Input.GetKey(KeyCode.F) || HitIsPressed) { if(CanCutWheat()) StartCoroutine(Hit()); }
        }
    }
    public void Move(Vector3 direction)
    {

    }
    public PlayerState GetState()
    {
        return _state;
    }
    private IEnumerator Hit()
    {
        _canMove = false;

        _state = PlayerState.Hit;
        animator.SetInteger("State", (int)_state);
        

        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f) yield return null;
        
        _state = PlayerState.Idle;
        animator.SetInteger("State", (int)_state);

        print("it's an Hit!");
        //to do Hit
        //event - end Anim
        _canMove = true;
    }
    private bool CanCutWheat()
    {
        Collider2D[] collider2s = Physics2D.OverlapCircleAll(transform.position, _hitRadius);
        if (collider2s.Length > 1) 
        {
            foreach (Collider2D item in collider2s)
                if (item.gameObject.CompareTag("Wheat"))
                {
                    item.GetComponent<WheatController>().Cut(); ;
                    return true;
                }
        }

        return false;
    }
}
