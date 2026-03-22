namespace Uixe.Copilot.Contracts.Dtos;

public sealed class TcoWeightMessageDto
{
    public MessageHeadDto? Head { get; set; }
    public MessageSubHeadDto? SubHead { get; set; }
    public WatcherType WatcherId { get; set; }
    public TcoDialogType DialogType { get; set; }
    public TcoTranDto? Tran { get; set; }
    public string? WeightFunctions { get; set; }
    public string? FareFormula { get; set; }
    public int TimeOut { get; set; }
}