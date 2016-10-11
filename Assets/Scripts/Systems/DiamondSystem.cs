using UnityEngine;
using System.Collections;

namespace PacmanGame
{
    public class DiamondSystem : SystemBase
    {
        public static string name = "DiamondSystem";

        public DiamondSystem(string _name)
            :base(_name)
        {
        }


        int diamondAmount = 0;
        public delegate void DiamondUpdateHandler(int _diamondAmount);
        public event DiamondUpdateHandler EventDiamondUpdate;

        public int DiamondAmount
        {
            get { return diamondAmount; }
        }

        public override void Create()
        {
            base.Create();
            diamondAmount = IOManager.Instance.recordData.diamondAmount;
            if (EventDiamondUpdate != null)
                EventDiamondUpdate(diamondAmount);
        }
        
        public void AddDiamond(int num)
        {
            IOManager.Instance.recordData.diamondAmount += num;
            IOManager.Instance.FlushToFile();
            diamondAmount = IOManager.Instance.recordData.diamondAmount;

            if (EventDiamondUpdate != null)
                EventDiamondUpdate(diamondAmount);
        }

        public void ReduceDiamond(int num)
        {
            IOManager.Instance.recordData.diamondAmount -= num;
            IOManager.Instance.FlushToFile();
            diamondAmount = IOManager.Instance.recordData.diamondAmount;

            if (EventDiamondUpdate != null)
                EventDiamondUpdate(diamondAmount);
        }
    }
}


