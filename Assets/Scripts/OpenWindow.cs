using UnityEngine;
using UnityEngine.UIElements;

public class OpenWindow : MonoBehaviour
{
	private float timeStart = 0;
	private bool pressed = true;
	
	VisualElement computerWin;
	VisualElement exit;
	
	private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        VisualElement computer = root.Q<VisualElement>("Computer");
		computerWin = root.Q<VisualElement>("ComputerWin");
		exit = root.Q<VisualElement>("X");

        if (computer != null)
        {
            computer.RegisterCallback<ClickEvent>(OnComputerClicked);
			exit.RegisterCallback<ClickEvent>(OnExitWin);
        }
    }
	
	private void OnComputerClicked(ClickEvent evt)
	{
		if (!pressed)
		{
			timeStart = Time.time;
			pressed = true;
		}
		else
		{
			float timeBetweenClicks = Time.time - timeStart;

			if (timeBetweenClicks <= 4f)
			{
				computerWin.style.display = DisplayStyle.Flex;
				Debug.Log("Computer is clicked");
			}

			timeStart = 0;
			pressed = false;
		}
	}
	
	private void OnExitWin(ClickEvent evt)
	{
		computerWin.style.display = DisplayStyle.None;
	}
	
	private void OnDisable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        VisualElement computer = root?.Q<VisualElement>("Computer");
        computer?.UnregisterCallback<ClickEvent>(OnComputerClicked);
    }
}
