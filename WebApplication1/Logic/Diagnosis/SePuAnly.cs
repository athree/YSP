using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MathWorks.MATLAB.NET.Arrays;

namespace WebApplication1.Diagnosis
{
    public class SePuAnly : IDisposable
    {
        public SePuAnly()
        { }

        public void Dispose();
        protected virtual void Dispose(bool disposing);
        public MWArray fun_SePuAnly();
        public MWArray[] fun_SePuAnly(int numArgsOut);
        public MWArray fun_SePuAnly(MWArray Mode);
        public MWArray[] fun_SePuAnly(int numArgsOut, MWArray Mode);
        public MWArray fun_SePuAnly(MWArray Mode, MWArray RawData);
        public MWArray[] fun_SePuAnly(int numArgsOut, MWArray Mode, MWArray RawData);
        public void fun_SePuAnly(int numArgsOut, ref MWArray[] argsOut, MWArray[] argsIn);
        public MWArray fun_SePuAnly(MWArray Mode, MWArray RawData, MWArray DataLen);
        public MWArray[] fun_SePuAnly(int numArgsOut, MWArray Mode, MWArray RawData, MWArray DataLen);
        public MWArray fun_SePuAnly(MWArray Mode, MWArray RawData, MWArray DataLen, MWArray FengNum);
        public MWArray[] fun_SePuAnly(int numArgsOut, MWArray Mode, MWArray RawData, MWArray DataLen, MWArray FengNum);
        public MWArray fun_SePuAnly(MWArray Mode, MWArray RawData, MWArray DataLen, MWArray FengNum, MWArray HighPosM);
        public MWArray[] fun_SePuAnly(int numArgsOut, MWArray Mode, MWArray RawData, MWArray DataLen, MWArray FengNum, MWArray HighPosM);
        public MWArray fun_SePuAnly(MWArray Mode, MWArray RawData, MWArray DataLen, MWArray FengNum, MWArray HighPosM, MWArray HighPosL);
        public MWArray[] fun_SePuAnly(int numArgsOut, MWArray Mode, MWArray RawData, MWArray DataLen, MWArray FengNum, MWArray HighPosM, MWArray HighPosL);
        public MWArray fun_SePuAnly(MWArray Mode, MWArray RawData, MWArray DataLen, MWArray FengNum, MWArray HighPosM, MWArray HighPosL, MWArray HighPosR);
        public MWArray[] fun_SePuAnly(int numArgsOut, MWArray Mode, MWArray RawData, MWArray DataLen, MWArray FengNum, MWArray HighPosM, MWArray HighPosL, MWArray HighPosR);
        public MWArray fun_SePuAnly(MWArray Mode, MWArray RawData, MWArray DataLen, MWArray FengNum, MWArray HighPosM, MWArray HighPosL, MWArray HighPosR, MWArray MinLYGrade);
        public MWArray[] fun_SePuAnly(int numArgsOut, MWArray Mode, MWArray RawData, MWArray DataLen, MWArray FengNum, MWArray HighPosM, MWArray HighPosL, MWArray HighPosR, MWArray MinLYGrade);
        public MWArray fun_SePuAnly(MWArray Mode, MWArray RawData, MWArray DataLen, MWArray FengNum, MWArray HighPosM, MWArray HighPosL, MWArray HighPosR, MWArray MinLYGrade, MWArray MaxLXGrade);
        public MWArray[] fun_SePuAnly(int numArgsOut, MWArray Mode, MWArray RawData, MWArray DataLen, MWArray FengNum, MWArray HighPosM, MWArray HighPosL, MWArray HighPosR, MWArray MinLYGrade, MWArray MaxLXGrade);
        public MWArray fun_SePuAnly(MWArray Mode, MWArray RawData, MWArray DataLen, MWArray FengNum, MWArray HighPosM, MWArray HighPosL, MWArray HighPosR, MWArray MinLYGrade, MWArray MaxLXGrade, MWArray MinRYGrade);
        public MWArray[] fun_SePuAnly(int numArgsOut, MWArray Mode, MWArray RawData, MWArray DataLen, MWArray FengNum, MWArray HighPosM, MWArray HighPosL, MWArray HighPosR, MWArray MinLYGrade, MWArray MaxLXGrade, MWArray MinRYGrade);
        public MWArray fun_SePuAnly(MWArray Mode, MWArray RawData, MWArray DataLen, MWArray FengNum, MWArray HighPosM, MWArray HighPosL, MWArray HighPosR, MWArray MinLYGrade, MWArray MaxLXGrade, MWArray MinRYGrade, MWArray MaxRXGrade);
        public MWArray[] fun_SePuAnly(int numArgsOut, MWArray Mode, MWArray RawData, MWArray DataLen, MWArray FengNum, MWArray HighPosM, MWArray HighPosL, MWArray HighPosR, MWArray MinLYGrade, MWArray MaxLXGrade, MWArray MinRYGrade, MWArray MaxRXGrade);
        public MWArray fun_SePuAnly(MWArray Mode, MWArray RawData, MWArray DataLen, MWArray FengNum, MWArray HighPosM, MWArray HighPosL, MWArray HighPosR, MWArray MinLYGrade, MWArray MaxLXGrade, MWArray MinRYGrade, MWArray MaxRXGrade, MWArray MaxHighWidth);
        public MWArray[] fun_SePuAnly(int numArgsOut, MWArray Mode, MWArray RawData, MWArray DataLen, MWArray FengNum, MWArray HighPosM, MWArray HighPosL, MWArray HighPosR, MWArray MinLYGrade, MWArray MaxLXGrade, MWArray MinRYGrade, MWArray MaxRXGrade, MWArray MaxHighWidth);
        public void WaitForFiguresToDie();
    }
}