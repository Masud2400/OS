using UnityEngine;
using UnityEngine.UIElements;

public class OpenWindow : MonoBehaviour
{
	private float timeStart = 0;
	private bool startOpen = false;
	private bool caretOpen = false;
	
	VisualElement computer;
	VisualElement doc;
	VisualElement chrome;
	VisualElement trash;
	VisualElement start;
	VisualElement caret;
	
	VisualElement computerWin;
	VisualElement docWin;
	VisualElement chromeWin;
	VisualElement trashWin;
	VisualElement startWin;
	VisualElement caretWin;
	
	VisualElement exitComputerWin;
	VisualElement exitDocWin;
	VisualElement exitChrome;
	VisualElement exitTrash;
	
	private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        computer = root.Q<VisualElement>("Computer");
		doc = root.Q<VisualElement>("Doc");
		chrome = root.Q<VisualElement>("Chrome");
		trash = root.Q<VisualElement>("Trash");
		start = root.Q<VisualElement>("Start");
		caret = root.Q<VisualElement>("Caret");
		
		computerWin = root.Q<VisualElement>("ComputerWin");
		docWin = root.Q<VisualElement>("DocWin");
		chromeWin = root.Q<VisualElement>("ChromeWin");
		trashWin = root.Q<VisualElement>("TrashWin");
		startWin = root.Q<VisualElement>("StartWin");
		caretWin = root.Q<VisualElement>("CaretWin");
		
		exitComputerWin = root.Q<VisualElement>("X");
		exitDocWin = root.Q<VisualElement>("DocExit");
		exitChrome = root.Q<VisualElement>("ChromeExit");
		exitTrash = root.Q<VisualElement>("TrashExit");

		computer.RegisterCallback<ClickEvent>(OnComputerClicked);
		doc.RegisterCallback<ClickEvent>(OnDocClicked);
		chrome.RegisterCallback<ClickEvent>(OnChromeClicked);
		trash.RegisterCallback<ClickEvent>(OnTrashClicked);
		start.RegisterCallback<ClickEvent>(OnStartClicked);
		caret.RegisterCallback<ClickEvent>(OnCaretClicked);
		
		exitComputerWin.RegisterCallback<ClickEvent>(OnExitWin);
		exitDocWin.RegisterCallback<ClickEvent>(OnExitDoc);
		exitChrome.RegisterCallback<ClickEvent>(OnExitChrome);
		exitTrash.RegisterCallback<ClickEvent>(OnExitTrash);
    }
	
	private void OnComputerClicked(ClickEvent evt)
	{
		OpenWinWithTimer(computerWin);
	}
	
	private void OnDocClicked(ClickEvent evt)
	{
		OpenWinWithTimer(docWin);
	}
	
	private void OnChromeClicked(ClickEvent evt)
	{
		OpenWinWithTimer(chromeWin);
	}
	
	private void OnTrashClicked(ClickEvent evt)
	{
		OpenWinWithTimer(trashWin);
	}
	
	private void OnStartClicked(ClickEvent evt)
	{
		if(!startOpen)
		{
			startWin.style.display = DisplayStyle.Flex;
			startOpen = true;
		}
		else
		{
			startWin.style.display = DisplayStyle.None;
			startOpen = false;
		}
	}
	
	private void OnCaretClicked(ClickEvent evt)
	{
		if(!caretOpen)
		{
			caretWin.style.display = DisplayStyle.Flex;
			caretOpen = true;
		}
		else
		{
			caretWin.style.display = DisplayStyle.None;
			caretOpen = false;
		}
	}
	
	private void OpenWinWithTimer(VisualElement win)
	{
		float currentTime = Time.time;

		if (timeStart > 0f && (currentTime - timeStart <= 4f))
		{
			win.style.display = DisplayStyle.Flex;
			
			timeStart = 0f; 
		}
		else
		{
			timeStart = currentTime;
		}
	}
	
	private void OnExitWin(ClickEvent evt)
	{
		computerWin.style.display = DisplayStyle.None;
	}
	
	private void OnExitDoc(ClickEvent evt)
	{
		docWin.style.display = DisplayStyle.None;
	}
	
	private void OnExitChrome(ClickEvent evt)
	{
		chromeWin.style.display = DisplayStyle.None;
	}
	
	private void OnExitTrash(ClickEvent evt)
	{
		trashWin.style.display = DisplayStyle.None;
	}
	
	private void OnDisable()
    {
		computer?.UnregisterCallback<ClickEvent>(OnComputerClicked);
		doc?.UnregisterCallback<ClickEvent>(OnDocClicked);
		chrome?.UnregisterCallback<ClickEvent>(OnChromeClicked);
		trash?.UnregisterCallback<ClickEvent>(OnTrashClicked);
		start?.UnregisterCallback<ClickEvent>(OnStartClicked);
		caret?.UnregisterCallback<ClickEvent>(OnCaretClicked);
		
		exitComputerWin?.UnregisterCallback<ClickEvent>(OnExitWin);
		exitDocWin?.UnregisterCallback<ClickEvent>(OnExitDoc);
		exitChrome?.UnregisterCallback<ClickEvent>(OnExitChrome);
		exitTrash?.UnregisterCallback<ClickEvent>(OnExitTrash);
    }
}
