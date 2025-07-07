using UnityEngine;

[CreateAssetMenu(menuName = "Machines/Producer Machine")]
public class ProducerMachine : Machine
{
    //public Item ProducedItem;

    public override void MachineTick()
    {
        // 

        Debug.Log("resource should be produced here");
    }
}
