using System;
using System.Collections.Generic;


using ATMApplication;

namespace Program
{
    class MainProgram
    {
        static void Main(string[] args)
        {
            ATMApp AtmInstance = new ATMApp();
            AtmInstance.Initialize();
            AtmInstance.Run();
        }
    }
}