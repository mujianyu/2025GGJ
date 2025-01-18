using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class MyButton_close : MonoBehaviour
{
    public Button userButton;
    void Start()
    {
        userButton.GetComponent<Button>().onClick.AddListener
            (delegate()
            {
                OnClick_close(this.gameObject);
            });
    }
 
    public void OnClick_close(GameObject _obj)
    {
        print("点击了按钮："+_obj.name);
    }
}