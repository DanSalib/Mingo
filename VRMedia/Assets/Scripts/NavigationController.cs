using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NavigationController : MonoBehaviour {

    public delegate void KeyPress(KeyCode key);
    public static event KeyPress OnKeyPress;
    public Text text;

    public MainUIController uiController;

    public GameObject backIndicator;
    public GameObject viewPort;
    public GameObject indicator;
    public NavObject curPanel;
    public NavObject prevPanel;

    private void Start()
    {
        OnKeyPress += ChangeCurPanel;
    }

    private void ChangeCurPanel(KeyCode key)
    {
        if(uiController.disableButtons == true)
        {
            return;
        }
        if(key == KeyCode.JoystickButton2)
        {
            if (curPanel.isBack)
            {
                return;
            }
            curPanel = curPanel.upNeighbor ?? curPanel;
            if (viewPort.activeInHierarchy && indicator.transform.localPosition.y > -84 && viewPort.transform.localPosition.y > 50)
            {
                var curPos = viewPort.transform.localPosition;
                viewPort.transform.localPosition = new Vector3(curPos.x, curPos.y - 131, curPos.z);
            }
            moveIndicator();
        }
        else if (key == KeyCode.JoystickButton3)
        {
            if (prevPanel != null)
            {
                backIndicator.SetActive(false);
                indicator.SetActive(true);
                curPanel = prevPanel;
                prevPanel = null;
            }
            else
            {
                curPanel = curPanel.rightNeighbor ?? curPanel;
                moveIndicator();
            }
        }
        else if (key == KeyCode.RightShift)
        {
            if (curPanel.isBack)
            {
                return;
            }
            curPanel = curPanel.downNeighbor ?? curPanel;
            if (viewPort.activeInHierarchy && indicator.transform.localPosition.y < -200 && viewPort.transform.localPosition.y < 280)
            {
                var curPos = viewPort.transform.localPosition;
                viewPort.transform.localPosition = new Vector3(curPos.x, curPos.y + 131, curPos.z);
            }
            moveIndicator();
        }
        else if (key == KeyCode.Return)
        {
            if(curPanel.leftNeighbor != null && curPanel.leftNeighbor.isBack)
            {
                backIndicator.SetActive(true);
                indicator.SetActive(false);
                prevPanel = curPanel;
                curPanel = curPanel.leftNeighbor ?? curPanel;
            }
            else if (prevPanel == null)
            {
                curPanel = curPanel.leftNeighbor ?? curPanel;
                moveIndicator();
            }
        }
        else if (key == KeyCode.JoystickButton7)
        {
            indicator.SetActive(false);
            prevPanel = null;
            curPanel.thisButton.onClick.Invoke();
            backIndicator.SetActive(false);
        }
        

        foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode))
                text.text += ("" + kcode);
        }
        
        if (key == KeyCode.UpArrow)
        {
            if(curPanel.isBack)
            {
                return;
            }
            curPanel = curPanel.upNeighbor ?? curPanel;
            if (viewPort.activeInHierarchy && indicator.transform.localPosition.y > -100 && viewPort.transform.localPosition.y > 50)
            {
                var curPos = viewPort.transform.localPosition;
                viewPort.transform.localPosition = new Vector3(curPos.x, curPos.y - 131, curPos.z);
            }
            moveIndicator();
        }
        else if (key == KeyCode.RightArrow)
        {
            if (prevPanel != null)
            {
                backIndicator.SetActive(false);
                indicator.SetActive(true);
                curPanel = prevPanel;
                prevPanel = null;
            }
            else
            {
                curPanel = curPanel.rightNeighbor ?? curPanel;
                moveIndicator();
            }
        }
        else if (key == KeyCode.LeftArrow)
        {
            if(curPanel.leftNeighbor != null && curPanel.leftNeighbor.isBack)
            {
                backIndicator.SetActive(true);
                indicator.SetActive(false);
                prevPanel = curPanel;
                curPanel = curPanel.leftNeighbor ?? curPanel;
            }
            else if(prevPanel == null)
            {
                curPanel = curPanel.leftNeighbor ?? curPanel;
                moveIndicator();
            }
        }
        else if (key == KeyCode.DownArrow)
        {
            if (curPanel.isBack)
            {
                return;
            }
            curPanel = curPanel.downNeighbor ?? curPanel;
            if (viewPort.activeInHierarchy && indicator.transform.localPosition.y < -200 && viewPort.transform.localPosition.y < 280)
            {
                var curPos = viewPort.transform.localPosition;
                var newY = curPos.y + 131;
                viewPort.transform.localPosition = new Vector3(curPos.x, newY, curPos.z);
            }
            moveIndicator();
        }
        else if (key == KeyCode.Space)
        {
            indicator.SetActive(false);
            prevPanel = null;
            curPanel.thisButton.onClick.Invoke();
            backIndicator.SetActive(false);
        }
    }

    public void moveIndicator()
    {
        indicator.SetActive(true);
        indicator.transform.position = curPanel.thisObject.transform.position;
    }

    // Update is called once per frame
    void Update () {
        if(Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            OnKeyPress(KeyCode.JoystickButton2);
        }
        else if (Input.GetKeyDown(KeyCode.JoystickButton3))
        {
            OnKeyPress(KeyCode.JoystickButton3);
        }
        else if (Input.GetKeyDown(KeyCode.RightShift))
        {
            OnKeyPress(KeyCode.RightShift);
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            OnKeyPress(KeyCode.Return);
        }
        else if (Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            OnKeyPress(KeyCode.JoystickButton7);
        }
        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            OnKeyPress(KeyCode.UpArrow);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            OnKeyPress(KeyCode.RightArrow);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            OnKeyPress(KeyCode.LeftArrow);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            OnKeyPress(KeyCode.DownArrow);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            OnKeyPress(KeyCode.Space);
        }
    }
}
