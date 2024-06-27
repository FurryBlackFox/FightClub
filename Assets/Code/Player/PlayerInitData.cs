using System.Collections.Generic;

namespace Code.Data
{
    public class PlayerInitData
    {
        public Player.Player Enemy { get; }
        public List<StatRuntimeData> StatRuntimeDataList { get; }
        
        public List<StatLibraryData> UsedStatsList { get; }
        public List<BuffLibraryData> UsedBuffsList { get; }


        public PlayerInitData(List<StatLibraryData> usedStatsList, List<BuffLibraryData> usedBuffsList, 
            Player.Player enemy, List<StatRuntimeData> statRuntimeDataList)
        {
            UsedBuffsList = usedBuffsList;
            Enemy = enemy;
            StatRuntimeDataList = statRuntimeDataList;
            UsedStatsList = usedStatsList;
        }
    }
}