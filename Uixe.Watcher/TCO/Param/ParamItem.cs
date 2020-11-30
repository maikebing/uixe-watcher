namespace Uixe.Watcher.Param
{
    public class ParamItem
    {
        public ParamItem(string name, string title)
        {
            pmname = name;
            pmtitle = title;
        }

        public string pmname { get; set; }
        public string pmtitle { get; set; }
        public string pmlocvar { get; set; }
        public bool Checked { get; set; }
    }
}