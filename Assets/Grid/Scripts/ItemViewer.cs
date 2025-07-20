using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemViewer : MonoBehaviour
{
    // Fields
    [SerializeField] private GridInputs gridInputs;
    [SerializeField] private GridSetup gridSetup;

    [SerializeField] private Canvas itemViewerCanvas;
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text quantityText;
    private GridSpace hoveredGridSpace;

    private void Start()
    {
        itemViewerCanvas.gameObject.SetActive(true);
        gridInputs.GridSpaceHovered += Hovering;
        CursorEventsManager.Instance.HoveringEnded += HoveringEnded;
    }

    private void Update()
    {
        UpdateItemViewer();
    }

    private void Hovering(GridSpace gridSpace)
    {
        hoveredGridSpace = gridSpace;
    }
    private void UpdateItemViewer()
    {
        if (GridSpaceHasItem(hoveredGridSpace, out IContainsItem inventory))
        {
            itemViewerCanvas.gameObject.SetActive(true);

            // Set up the position of the item viewer
            Vector3 offset = new(-gridSetup.gridSpaceOffset / 2, gridSetup.gridSpaceOffset / 2);
            itemViewerCanvas.transform.position = hoveredGridSpace.transform.position + offset;

            // could add a bool to the IContainsItem called "ShowInItemViewer" that allows it to be hidden. depends on if I want dropped items to be stackable, if they aren't then I should add it.
            itemViewerCanvas.gameObject.SetActive(true);
            quantityText.text = "1"; // will replace with quantity
            itemImage.sprite = inventory.ContainedItem.ItemSprite;
        }
        else
        {
            itemViewerCanvas.gameObject.SetActive(false);
        }
    }
    private bool GridSpaceHasItem(GridSpace gridSpace, out IContainsItem inventory)
    {
        if (gridSpace == null || gridSpace.GridObject == null)
        {
            inventory = null;
            return false;
        }

        inventory = gridSpace.GridObject as IContainsItem;
        return inventory != null && inventory.ContainedItem != null;
    }
    private void HoveringEnded()
    {
        hoveredGridSpace = null;
    }
}
