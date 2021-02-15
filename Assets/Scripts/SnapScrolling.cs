using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SnapScrolling : MonoBehaviour
{
    [Range(1, 50)]
    [Header("Amount of Ads")]
    public int amount;
    [Space]
    public GameObject adPrefab;
    [Header("Information")]
    public InfoController infoController; 
    
    // fields, need to slide content
    private RectTransform contentRect;
    private Vector2 contentVector;
    private Vector2 targetPos;

    private bool InitializedContent = false; // boolean field, to check InitializedContend ?
    private GameObject[] allAds; // array to communicate with all content;
    private int maxPages; // to know max Pages :D

    private void Awake()
    {
        maxPages = amount / 6;
        if (amount % 6 > 0)
        {
            maxPages++;
        }
        allAds = new GameObject[amount];
    }
    private void Start()
    {
        contentRect = GetComponent<RectTransform>();
        targetPos = contentRect.anchoredPosition;
        InitializeContent();
    }
    
    // FixedUpdate checks, where should content be
    private void FixedUpdate()
    {
        contentVector.x = Mathf.SmoothStep(contentRect.anchoredPosition.x, targetPos.x, 10 * Time.fixedDeltaTime);
        contentRect.anchoredPosition = contentVector;
    }

    // Spawn Prefabs of Content
    private void SpawnAds()
    {        
        float coordinateX = 0;
        float coordinateY = 0;
        int q = 1; // index of ROW (max row in this app is 3)...

        for (int i = 0; i < amount; i++)
        {            
            allAds[i] = Instantiate(adPrefab, transform);
            infoController.itemsInformation.Add(allAds[i].GetComponent<Information>()); // it goes to Class_InfoController, to work with info of content...
            allAds[i].transform.localPosition = new Vector2(coordinateX, coordinateY); // Initialize all content position
            coordinateY -= 248f;
            if (q == 3)
            {
                coordinateX += 586.5f;
                coordinateY = 0;
                q = 0;
            }
            q++;
        }
    }

    //Sliding content
    public void Next()
    {
        contentRect.anchoredPosition = targetPos;
        if (targetPos.x - 1173f > -(maxPages * 1173f))
        {
            targetPos.x -= 1173f;
        }        
    }
    public void Before()
    {
        contentRect.anchoredPosition = targetPos;
        if (targetPos.x + 1173f <= 0)
        {
            targetPos.x += 1173f;
        }
    }    

    /// <summary>
    /// Initializing content...
    /// </summary>
    public void InitializeContent()
    {
        if (!InitializedContent)
        {
            InitializedContent = true;
            SpawnAds();
            infoController.InitializeInfo();
        }
        else
        {
            targetPos.x = -4.569221f;
        }
    }
}