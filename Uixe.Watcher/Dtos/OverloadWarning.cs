namespace Uixe.Watcher.Dtos
{
    /// <summary>
    ///  Title = $"{_veh}超限%{arc.weightWaste.OVER_RATE}", Context = str, Id 
    /// </summary>
    public class OverloadWarning
    {
        public string Title { get; set; }
        public string Context { get; set; }
        public string Id { get; set; }
    }

}