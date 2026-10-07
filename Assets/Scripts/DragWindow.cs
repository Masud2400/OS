using UnityEngine;
using UnityEngine.UIElements;

public class DragWindow : MonoBehaviour
{
	private VisualElement root;

	private VisualElement draggingWindow;
	private VisualElement draggingArea;

	private bool dragging;

	private Vector2 startMousePosition;
	private Vector2 startWindowPosition;

	private void OnEnable()
	{
		root = GetComponent<UIDocument>().rootVisualElement;

		RegisterDragArea("TileBar", "ComputerWin");
		RegisterDragArea("TileBarDoc", "DocWin");
		RegisterDragArea("TabsBar", "ChromeWin");
		RegisterDragArea("TileBarTrash", "TrashWin");
	}

	private void RegisterDragArea(string dragAreaName, string windowName)
	{
		var dragArea = root.Q<VisualElement>(dragAreaName);
		var window = root.Q<VisualElement>(windowName);

		if (dragArea == null || window == null)
			return;

		// Store the window reference on the drag area.
		dragArea.userData = window;

		dragArea.RegisterCallback<PointerDownEvent>(OnPointerDown);
		dragArea.RegisterCallback<PointerMoveEvent>(OnPointerMove);
		dragArea.RegisterCallback<PointerUpEvent>(OnPointerUp);
	}

	private void OnPointerDown(PointerDownEvent evt)
	{
		draggingArea = evt.currentTarget as VisualElement;
		draggingWindow = draggingArea.userData as VisualElement;

		if (draggingWindow == null)
			return;

		dragging = true;

		startMousePosition = evt.position;
		startWindowPosition = draggingWindow.transform.position;

		draggingArea.CapturePointer(evt.pointerId);
	}

	private void OnPointerMove(PointerMoveEvent evt)
	{
		if (!dragging || draggingWindow == null)
			return;

		Vector2 mouseDelta = (Vector2)evt.position - startMousePosition;

		draggingWindow.transform.position =
			startWindowPosition + mouseDelta;
	}

	private void OnPointerUp(PointerUpEvent evt)
	{
		if (!dragging)
			return;

		dragging = false;

		if (draggingArea != null &&
			draggingArea.HasPointerCapture(evt.pointerId))
		{
			draggingArea.ReleasePointer(evt.pointerId);
		}

		draggingArea = null;
		draggingWindow = null;
	}

	private void OnDisable()
	{
		UnregisterDragArea("TileBar");
		UnregisterDragArea("TileBarDoc");
		UnregisterDragArea("TileBarBrowser");
	}

	private void UnregisterDragArea(string dragAreaName)
	{
		var dragArea = root?.Q<VisualElement>(dragAreaName);

		if (dragArea == null)
			return;

		dragArea.UnregisterCallback<PointerDownEvent>(OnPointerDown);
		dragArea.UnregisterCallback<PointerMoveEvent>(OnPointerMove);
		dragArea.UnregisterCallback<PointerUpEvent>(OnPointerUp);
	}
}
