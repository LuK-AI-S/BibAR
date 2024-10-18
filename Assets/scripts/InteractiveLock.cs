using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveLock : MonoBehaviour
{

    [SerializeField]
    private GameObject coin;
    [SerializeField]
    private GameObject euro;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowCoins()
    {
        coin.SetActive(true);
        euro.SetActive(true);
    }
}
