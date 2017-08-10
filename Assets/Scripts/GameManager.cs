using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    void Awake ()
    {
        instance = this;
    }
	
	void Update ()
    {
		
	}

    public static string GetExternal_IP()
    {
        WWW myExtIPWWW = new WWW("http://checkip.dyndns.org");
        if (myExtIPWWW == null) return null;
        while (!myExtIPWWW.isDone);
        string myExtIP = myExtIPWWW.text;
        myExtIP = myExtIP.Substring(myExtIP.IndexOf(":") + 2);
        myExtIP = myExtIP.Substring(0, myExtIP.IndexOf("<"));
        return myExtIP;
    }
}
