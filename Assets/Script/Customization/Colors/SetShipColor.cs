using UnityEngine;

public class SetShipColor : MonoBehaviour
{
    public void ColorSetterIndex(int index)
    {
        ShipCustomizerStorage.SetIndex(index);
        ShipCustomizerStorage.updateColors = true;
    }
}
