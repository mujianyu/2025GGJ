using System.Collections.Generic;
using UnityEngine;
 
public class UIManager : StateMachina
{
    public static UIManager Instance;
 
    string realPath = "Prefab/Panel/";
 
    private Dictionary<string, GameObject> prefabDict = new Dictionary<string, GameObject>();
    private IState currentState;
 
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SwitchPanel(My_UIConst.MainMenuPanel);
        }
        else
        {
            Destroy(gameObject);
        }
    }
 
    public GameObject CreatePanel(string name)
    {
        if (prefabDict.ContainsKey(name))
        {
            return prefabDict[name];
        }
 
        GameObject panelPrefab = Resources.Load<GameObject>(realPath + name);
        if (panelPrefab == null)
        {
            Debug.LogError($"Failed to load panel prefab: {realPath}{name}");
            return null;
        }
 
        GameObject panelObject = Instantiate(panelPrefab, gameObject.transform, false);
        prefabDict[name] = panelObject;
        return panelObject;
    }
 
    public void SwitchPanel(string name)
    {
        UIState newState;
        if (prefabDict.ContainsKey(name))
        {
            newState = prefabDict[name].GetComponent<UIState>();
        }
        else
        {
            GameObject panelObject = CreatePanel(name);
            if (panelObject == null)
            {
                Debug.LogError($"Failed to create panel: {name}");
                return;
            }
            newState = panelObject.GetComponent<UIState>();
        }
        SwitchState(newState);
    }
}