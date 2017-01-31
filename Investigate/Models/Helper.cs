namespace Investigate
{
    public enum PageType
    {
        Detail,
        AnimalStat,
        Sample
    }

    public class MasterPageItem
    {
        public PageType Type { get; set; }
        public string Title { get; set; }
        public string IconSource { get; set; }
    }
}