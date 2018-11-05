using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NavigationController : MonoBehaviour {

    public delegate void KeyPress(KeyCode key);
    public static event KeyPress OnKeyPress;

    public GameObject indicator;
    public NavObject curPanel;

    private void Start()
    {
        OnKeyPress += ChangeCurPanel;
    }

    private void ChangeCurPanel(KeyCode key)
    {
        if(key == KeyCode.JoystickButton2)
        {
            curPanel = curPanel.upNeighbor ?? curPanel;
            moveIndicator();
        }
        else if (key == KeyCode.JoystickButton3)
        {
            curPanel = curPanel.rightNeighbor ?? curPanel;
            moveIndicator();
        }
        else if (key == KeyCode.JoystickButton5)
        {
            curPanel = curPanel.leftNeighbor ?? curPanel;
            moveIndicator();
        }
        else if (key == KeyCode.JoystickButton10)
        {
            curPanel = curPanel.downNeighbor ?? curPanel;
            moveIndicator();
        }
        else if (key == KeyCode.JoystickButton7)
        {
            curPanel.thisButton.onClick.Invoke();
        }

        if (key == KeyCode.UpArrow)
        {
            curPanel = curPanel.upNeighbor ?? curPanel;
            moveIndicator();
        }
        else if (key == KeyCode.RightArrow)
        {
            curPanel = curPanel.rightNeighbor ?? curPanel;
            moveIndicator();
        }
        else if (key == KeyCode.LeftArrow)
        {
            curPanel = curPanel.leftNeighbor ?? curPanel;
            moveIndicator();
        }
        else if (key == KeyCode.DownArrow)
        {
            curPanel = curPanel.downNeighbor ?? curPanel;
            moveIndicator();
        }
        else if (key == KeyCode.Return)
        {
            curPanel.thisButton.onClick.Invoke();
        }
    }

    public void moveIndicator()
    {
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
        else if (Input.GetKeyDown(KeyCode.JoystickButton5))
        {
            OnKeyPress(KeyCode.JoystickButton3);
        }
        else if (Input.GetKeyDown(KeyCode.JoystickButton10))
        {
            OnKeyPress(KeyCode.JoystickButton3);
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
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            OnKeyPress(KeyCode.Return);
        }
    }
}
