using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIcontroller : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _text;
    [SerializeField]
    private Animator _animator;

    private int _score = 0;

    private void Awake()
    {
        GetGem(0);
    }
    public void GetGem(int addinScore)
    {
        _score += addinScore;
        _text.text = _score.ToString();
        _animator.SetTrigger("Update");
    }
}
