using System;

namespace Uixe.Watcher.Dtos
{
    public class TCOConfirmEntryInfo
    {
        public TCOConfirmEntryInfo() { }
        public TCOConfirmEntryInfo(string cardId, string enStationId, string enTime, string enTollLaneId, string mediaNo, int mediaType, int resultVoucher, int selecedIndex, int retQuery)
        {
            this.cardId = cardId??string.Empty;
            this.enStationId = enStationId ?? string.Empty;
            this.enTime = enTime ?? string.Empty;
            this.enTollLaneId = enTollLaneId ?? string.Empty;
            this.mediaNo = mediaNo ?? string.Empty;
            this.mediaType = mediaType;
            this.resultVoucher = resultVoucher;
            this.selecedIndex = selecedIndex;
            this.retQuery = retQuery;
        }

        public string cardId { get; set; }=string.Empty;
     
        public string enStationId { get; set; }=string.Empty;
      
        public string enTime { get; set; } = string.Empty;

 
    
        public string enTollLaneId { get; set; } = string.Empty;
     
        public string mediaNo { get; set; } ="030";
      
        public int mediaType { get; set; }  = 9;

        public int resultVoucher { get; set; } = 0;
        public int selecedIndex { get; } = 0;
        public int retQuery { get; } = 1;
    }

}
