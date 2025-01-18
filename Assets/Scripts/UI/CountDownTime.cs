using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDownTime : MonoBehaviour
{
    private TMP_Text Text;
    
    // Start is called before the first frame update
    void Start()
    {
        Text =this.GetComponent<TMP_Text>();
        Text.text = "90"; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
