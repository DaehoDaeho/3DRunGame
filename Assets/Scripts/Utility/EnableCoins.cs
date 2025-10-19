using System.Runtime.InteropServices;
using UnityEngine;

public class EnableCoins : MonoBehaviour
{
    public GameObject[] coins;

    private void OnEnable()
    {
        if(coins != null)
        {
            for(int i=0; i<coins.Length; ++i)
            {
                coins[i].SetActive(true);
            }
        }
    }
}
