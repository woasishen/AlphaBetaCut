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
        public const int LAYER_COUNT = 6;
        public const int CHILD_COUNT = 2;

        public static Color SelectedColor = Color.Aqua;

        public static Color OriginColor(int index)
        {
            return index / CHILD_COUNT % 2 == 0 ? Color.Wheat : Color.FloralWhite;
        }


        public static List<int> InitGole = new List<int>
        {
            //21,
            //22,
            //23,
            //24,
            //17,
            //18,
            //19,
            //20,
            //25,
            //26,
            //27,
            //28,
            //29,
            //30,
            //31,
            //32,
            //1,
            //2,
            //3,
            //4,
            //5,
            //6,
            //7,
            //8,
            //9,
            //10,
            //11,
            //12,
            //13,
            //14,
            //15,
            //16,
        };
    }
}
