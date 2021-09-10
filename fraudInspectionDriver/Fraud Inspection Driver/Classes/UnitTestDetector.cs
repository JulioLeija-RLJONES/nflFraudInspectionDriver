using System;
using System.Reflection;

/// <summary>
/// Detect if we are running as part of a nUnit unit test.
/// This is DIRTY and should only be used if absolutely necessary 
/// as its usually a sign of bad design.
/// </summary> 
namespace RLJones.FraudInspectionDriver.Classes
{
    /// <summary>
    /// Class checks if the project is running with Debugger attached.
    /// </summary>
    static class UnitTestDetector
    {

        private static bool _runningFromNUnit = false;

        static UnitTestDetector()
        {
            foreach (Assembly assem in AppDomain.CurrentDomain.GetAssemblies())
            {
                // if (assem == typeof(NUnit.Framework.Assert))

                if (assem.FullName.ToLowerInvariant().StartsWith("nunit.framework"))
                {
                    _runningFromNUnit = true;
                    break;
                }
            }
        }

        public static bool IsRunningFromNUnit
        {
            get { return _runningFromNUnit; }
        }
    }
}
