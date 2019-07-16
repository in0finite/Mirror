using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SyncListMemoryExhaustion : NetworkBehaviour
{

	public struct Item
	{
	    public string name;
	    public int amount;
	    public Color32 color;
	}

	class SyncListItem : SyncList<Item> {}

	SyncListItem m_inventory;

	public int numOperationsPerFrame = 1000;

	static int s_lastValue = 1;



    public override void OnStartServer()
    {
        m_inventory.Add(GetItem());
    }

    static Item GetItem()
    {
    	return new Item(){amount = s_lastValue++};
    }

    void Update()
    {
        
        if (this.isServer)
        {
	    	for (int i=0; i < this.numOperationsPerFrame; i++)
	    	{
	    		m_inventory[0] = GetItem();
	    	}
    	}

    }

}
