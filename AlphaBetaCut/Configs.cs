using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace AlphaBetaCut
{
    public static class Configs
    {
        public static Action<string> LogMsg;
        public static Action EnableNextStep;
        public static Action ComputeFinished;
        public static Semaphore ExcutSemaphore = new Semaphore(0, 1);

        public const int MAX = 10000;
        public const int MIN = -10000;
        public const int LAYER_COUNT = 4;
        public const int CHILD_COUNT = 3;

        public static Color SelectedColor = Color.Aqua;

        public static Color OriginColor(int index)
        {
            return index / CHILD_COUNT % 2 == 0 ? Color.Wheat : Color.FloralWhite;
        }


        public static List<int> InitGole = new List<int>
        {
            17,
            16,
            15,
            19
        };
    }
}
