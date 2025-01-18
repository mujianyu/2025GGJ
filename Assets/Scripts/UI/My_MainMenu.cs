using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class My_MainMenu : UIState
{
    public Button userButton;
    public Button SettingButton;
    public Button ExitGameButton;
    // public Image bg;
    public Button playButton;
    private void Start()
    {   
        
        
        userButton.onClick.AddListener(() =>
        {
            Debug.Log("主菜单按钮点击 切换用户界面");
 
            UIManager.Instance.SwitchPanel(My_UIConst.UserPanel);
        });

        SettingButton.onClick.AddListener(() =>
        {
            Debug.Log("主菜单按钮点击 切换设置界面");
 
            UIManager.Instance.SwitchPanel(My_UIConst.SettingsPanel);
        });
        ExitGameButton.onClick.AddListener(() =>
        {
            Debug.Log("退出游戏");
 
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }); 
        playButton.onClick.AddListener(() =>
        {
            Debug.Log("主菜单按钮点击 切换游戏界面");
 
            UIManager.Instance.SwitchPanel(My_UIConst.MainPlayPanel);
        });
    }
    public override void Enter()
    {
        Debug.Log("进入主菜单");
 
        // DOTween.To(() => canvasGroup.alpha=0, x => canvasGroup.alpha=x, 1, 1);
 
        // 生成一个随机颜色
        // Color randomColor = new Color(Random.value, Random.value, Random.value);
        // bg.DOColor(randomColor, 1f); // 渐变到随机颜色

        // 设置初始位置
        canvas.transform.localPosition = new Vector3(Screen.width, 0, 0);
 
        rectTransform.SetAsLastSibling();//将渲染等级移动到最后面 显示在最前面
        // 使用 DOTween 平移动画将面板移到屏幕中心
        canvas.transform.DOLocalMoveX(0, 1f).SetEase(Ease.OutQuad);
 
        base.Enter();
    }
    public override void Exit()
    {
        // Color randomColor = new Color(Random.value, Random.value, Random.value);
        // bg.DOColor(randomColor, 1f); // 渐变到随机颜色

        rectTransform.SetAsLastSibling();//将渲染等级移动到最后面 显示在最前面
        // 在退出时添加平移动画
        canvas.transform.DOLocalMoveX(-Screen.width, 1f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            base.Exit();
        });
        
    }
 
}