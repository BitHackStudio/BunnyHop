using System.Xml.Serialization;

namespace Assets.Classes
{
    [XmlRoot("dreamlo")]
    public class DreamloXML
    {
        public Leaderboard leaderboard;
    }

    public class Leaderboard
    {
        public Entry entry;
    }

    public class Entry
    {
        public string name;
        public int score;
        public int seconds;
        public string text;
        public string date;
    }
}
