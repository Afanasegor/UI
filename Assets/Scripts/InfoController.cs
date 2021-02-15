using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Networking;

public class InfoController : MonoBehaviour
{
    public Info dataInfo; // Instance of Info_class
    public List<Information> itemsInformation; // work with info of content... initializing in SnapScrolling.cs(59);

    
    private void Awake()
    {
        LoadField(); // load info to Info_class
    }

    /// <summary>
    /// load info to Info_class
    /// </summary>
    [ContextMenu("Load")]
    public void LoadField()
    {
        string sFilePath = Path.Combine(Application.streamingAssetsPath, "DataInfo" + ".json");
        string sJson;
        if (Application.platform == RuntimePlatform.Android)
        {            
            UnityWebRequest www = UnityWebRequest.Get(sFilePath);
            www.SendWebRequest();
            while (!www.isDone) ;
            sJson = www.downloadHandler.text;
        }
        else sJson = File.ReadAllText(sFilePath);
        //dataInfo = JsonUtility.FromJson<Info>(File.ReadAllText(Application.streamingAssetsPath + "/DataInfo.json"));
        dataInfo = JsonUtility.FromJson<Info>(sJson);        
    }

    /// <summary>
    /// Class to work with JSON
    /// </summary>
    [System.Serializable]
    public class Info
    {
        public string[] avatar;
        public string[] thingsImg;
        public string[] playerName;
        public string[] thingsName;
        public int[] playerLvl;
        public int[] thingsCount;
        public int[] coinsCount;
    }

    /// <summary>
    /// Initializing Info in all Content... translate from JSON to Unity...
    /// </summary>
    public void InitializeInfo()
    {
        for (int i = 0; i < dataInfo.playerName.Length; i++)
        {
            itemsInformation[i].userName.text = dataInfo.playerName[i];
            itemsInformation[i].thingsCount.text = $"x{dataInfo.thingsCount[i]}";
            itemsInformation[i].coinsCount.text = dataInfo.coinsCount[i].ToString();
            itemsInformation[i].playerLvl.text = dataInfo.playerLvl[i].ToString();
            itemsInformation[i].thingsName.text = dataInfo.thingsName[i];
            itemsInformation[i].thingImg.sprite = Resources.Load<Sprite>(dataInfo.thingsImg[i]);
            StartCoroutine(LoadImg(itemsInformation[i].avatar, dataInfo.avatar[i]));
        }
    }

    /// IMAGE LOADER FROM INET
    IEnumerator LoadImg(Image img, string url)
    {
        yield return 0;
        var imgLink = new WWW(url);
        yield return imgLink;
        Texture2D texture = new Texture2D(imgLink.texture.width, imgLink.texture.height);
        imgLink.LoadImageIntoTexture(texture);
        img.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one / 2);
    }
}


/*
string sFilePath = Path.Combine(Application.streamingAssetsPath, "lang_" + sLangIDs[iLangID] + ".json");
string sJson;
if (Application.platform == RuntimePlatform.Android)
{
    UnityWebRequest www = UnityWebRequest.Get(sFilePath);
    www.SendWebRequest();
    while (!www.isDone) ;
    sJson = www.downloadHandler.text;
}
else sJson = File.ReadAllText(sFilePath);
*/
