using System.Collections;
using System.Collections.Generic;
using DG.Tweening;  // 引入 DOTween 命名空间
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
 
public class My_UserPanel : UIState
{
    public Button mainMenuButton;
    
 
    private void Start()
    {
        mainMenuButton.onClick.AddListener(() =>
        {
            Debug.Log("用户按钮点击 切换主界面");
 
            UIManager.Instance.SwitchPanel(My_UIConst.MainMenuPanel);
        });
    }
 
    public override void Enter()
    {
        Debug.Log("进入用户界面");
 
        // 设置初始位置
        canvas.transform.localPosition = new Vector3(Screen.width, 0, 0);
 
        rectTransform.SetAsLastSibling();//将渲染等级移动到最后面 显示在最前面
        // 使用 DOTween 平移动画将面板移到屏幕中心
        canvas.transform.DOLocalMoveX(0, 1f).SetEase(Ease.OutQuad);
 
        base.Enter();
    }
 
    public override void Exit()
    {
        rectTransform.SetAsLastSibling();//将渲染等级移动到最后面 显示在最前面
        // 在退出时添加平移动画
        canvas.transform.DOLocalMoveX(-Screen.width, 1f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            base.Exit();
        });
    }
}