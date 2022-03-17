using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitButtonController : MonoBehaviour
{
    private Button button;
    private void Start()
    {
        button = gameObject.GetComponent<Button>();
    }
}
