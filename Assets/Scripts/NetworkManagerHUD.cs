using Assets.Classes;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetworkManagerHUD : MonoBehaviour
{
    public Canvas joinGameHUD;
    Canvas i_joinGameHUD;

    NetworkManager networkManager;

    // Use this for initialization
    void Start ()
    {
        networkManager = GetComponent<NetworkManager>();

        int currLevel = SceneManager.GetActiveScene().buildIndex;
        if (currLevel == 0)
        {
            XmlSerializer ser = new XmlSerializer(typeof(DreamloXML));
            DreamloXML dreamlo;
            using (XmlReader reader = XmlReader.Create("http://dreamlo.com/lb/5981cc49b0b05c1ad46cb6fb/xml"))
            {
                dreamlo = (DreamloXML)ser.Deserialize(reader);
            }
            InputField inputField = joinGameHUD.GetComponentInChildren<InputField>();
            if (dreamlo.leaderboard.entry == null)
                inputField.text = "localhost";
            else
                inputField.text = dreamlo.leaderboard.entry.text;
            i_joinGameHUD = Instantiate(joinGameHUD);

            GameObject.Find("Join Game Button").GetComponent<Button>().onClick.RemoveAllListeners();
            GameObject.Find("Join Game Button").GetComponent<Button>().onClick.AddListener(JoinGame);
            GameObject.Find("Host Game Button").GetComponent<Button>().onClick.RemoveAllListeners();
            GameObject.Find("Host Game Button").GetComponent<Button>().onClick.AddListener(StartupHost);
        }
        else
        {

        }
    }

    void StartupHost()
    {
        WWW www = new WWW("http://dreamlo.com/lb/hVcx7OXEN0GpUy2MKE8gnA3dG0FT2Lw06U69yjm-BnEg/delete/PublicIP");
        while (!www.isDone);
        www = new WWW("http://dreamlo.com/lb/hVcx7OXEN0GpUy2MKE8gnA3dG0FT2Lw06U69yjm-BnEg/add/PublicIP/0/0/" + GameManager.GetExternal_IP());
        while (!www.isDone);
        networkManager.StartHost();
        Destroy(i_joinGameHUD.gameObject);
    }

    void JoinGame()
    {
        networkManager.networkAddress = joinGameHUD.GetComponentInChildren<InputField>().text;
        networkManager.StartClient();
        Destroy(i_joinGameHUD.gameObject);
    }
}
