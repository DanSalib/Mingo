using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
public class NavigationController : MonoBehaviour {

    public delegate void KeyPress(directions d);
    public static event KeyPress OnKeyPress;
    public Text text;

    public MainUIController uiController;
    public bool ftueActive = true;
    public GameObject backIndicator;
    public GameObject viewPort;
    public GameObject indicator;
    public NavObject curPanel;
    public NavObject prevPanel;

    private Stopwatch joystickTimer = new Stopwatch();

    private void Start()
    {
        joystickTimer.Start();
        OnKeyPress += ChangeCurPanel;
    }

    public enum directions
    {
        up = 0,
        down = 1,
        left = 2,
        right = 3,
        click = 4
    };

    private void ChangeCurPanel(directions d)
    {
        if(ftueActive || uiController.disableButtons == true)
        {
            return;
        }
        if(d == directions.up)
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
        else if (d == directions.right)
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
        else if (d == directions.down)
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
        else if (d == directions.left)
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
        else if (d == directions.click)
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
        //foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
        //{
        //    if (Input.GetKeyDown(kcode))
        //        text.text += ("" + kcode);
        //}
        if (Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            OnKeyPress(directions.up);
        }
        else if (Input.GetKeyDown(KeyCode.JoystickButton3))
        {
            OnKeyPress(directions.right);
        }
        else if (Input.GetKeyDown(KeyCode.RightShift))
        {
            OnKeyPress(directions.down);
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            OnKeyPress(directions.left);
        }
        else if (Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            OnKeyPress(directions.click);
        }
        //   text.text += Input.GetAxisRaw("Oculus_GearVR_LThumbstickX") != 0 ? "x: " + Input.GetAxis("Horizontal") : "";
        //  text.text += Input.GetAxisRaw("Oculus_GearVR_LThumbstickY") != 0 ? "y: " + Input.GetAxis("Vertical") : "";

        //if (Input.GetKeyDown(KeyCode.JoystickButton2))
        //{
        //    OnKeyPress(directions.up);
        //}
        //else if (Input.GetKeyDown(KeyCode.JoystickButton3))
        //{
        //    OnKeyPress(directions.right);
        //}
        //else if (Input.GetKeyDown(KeyCode.JoystickButton0))
        //{
        //    OnKeyPress(directions.left);
        //}
        //else if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        //{
        //    OnKeyPress(directions.down);
        //}
        //else if (Input.GetKeyDown(KeyCode.RightShift))
        //{
        //    OnKeyPress(directions.click);
        //}
        //else if (Input.GetKeyDown(KeyCode.LeftShift))
        //{
        //    OnKeyPress(directions.click);
        //}

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            OnKeyPress(directions.up);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            OnKeyPress(directions.right);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            OnKeyPress(directions.left);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            OnKeyPress(directions.down);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            OnKeyPress(directions.click);
        }

        if(joystickTimer.ElapsedMilliseconds > 200f)
        {
            if (Input.GetAxisRaw("Oculus_GearVR_LThumbstickX") == 1)
            {
                joystickTimer.Reset();
                joystickTimer.Start();
                OnKeyPress(directions.right);
            }
            else if (Input.GetAxisRaw("Oculus_GearVR_LThumbstickX") == -1)
            {
                joystickTimer.Reset();
                joystickTimer.Start();
                OnKeyPress(directions.left);
            }
            else if (Input.GetAxisRaw("Oculus_GearVR_LThumbstickY") == -1)
            {
                joystickTimer.Reset();
                joystickTimer.Start();
                OnKeyPress(directions.down);
            }
            else if (Input.GetAxisRaw("Oculus_GearVR_LThumbstickY") == 1)
            {
                joystickTimer.Reset();
                joystickTimer.Start();
                OnKeyPress(directions.up);
            }
        }
    }
}
