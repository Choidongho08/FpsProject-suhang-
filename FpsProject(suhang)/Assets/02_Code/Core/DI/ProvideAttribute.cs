using System;

namespace _02_Code.Core.DI
{
    // 위존성을 제공하는 녀석들은 메서드 또는 클래스에 붙일 수 있어
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class ProvideAttribute : Attribute
    {
        
    }
}