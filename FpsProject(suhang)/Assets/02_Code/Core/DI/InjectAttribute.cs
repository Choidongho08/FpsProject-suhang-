using System;

namespace _02_Code.Core.DI
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method)]
    public class InjectAttribute : Attribute
    {
        
    }
}