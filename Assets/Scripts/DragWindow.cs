using UnityEngine;
using UnityEngine.UIElements;

public class DragWindow : MonoBehaviour
{
	private VisualElement window;
	private VisualElement dragArea;

	private bool dragging;

	private Vector2 startMousePosition;
	private Vector2 startWindowPosition;

	private void OnEnable()
	{
		var root = GetComponent<UIDocument>().rootVisualElement;

		window = root.Q<VisualElement>("ComputerWin");
		dragArea = root.Q<VisualElement>("TileBar");

		if (dragArea != null)
		{
			dragArea.RegisterCallback<PointerDownEvent>(OnPointerDown);
			dragArea.RegisterCallback<PointerMoveEvent>(OnPointerMove);
			dragArea.RegisterCallback<PointerUpEvent>(OnPointerUp);
		}
		else
		{
			Debug.Log("Drag area is null");
		}
	}

	private void OnPointerDown(PointerDownEvent evt)
	{
		dragging = true;

		startMousePosition = evt.position;
		startWindowPosition = window.transform.position;

		dragArea.CapturePointer(evt.pointerId);
	}

	private void OnPointerMove(PointerMoveEvent evt)
	{
		if (!dragging)
			return;

		Vector2 mouseDelta = (Vector2)evt.position - startMousePosition;

		window.transform.position = startWindowPosition + mouseDelta;
	}

	private void OnPointerUp(PointerUpEvent evt)
	{
		if (!dragging)
			return;

		dragging = false;

		dragArea.ReleasePointer(evt.pointerId);
	}

	private void OnDisable()
	{
		if (dragArea != null)
		{
			dragArea.UnregisterCallback<PointerDownEvent>(OnPointerDown);
			dragArea.UnregisterCallback<PointerMoveEvent>(OnPointerMove);
			dragArea.UnregisterCallback<PointerUpEvent>(OnPointerUp);
		}
	}
}
