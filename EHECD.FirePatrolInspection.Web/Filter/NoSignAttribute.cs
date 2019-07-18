using System;

namespace EHECD.FirePatrolInspection.Web.Filter
{
    /// <summary>
    /// 使用验证时 [NoSign] 标注不需要登录和权限验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true)]
    public class NoSignAttribute : Attribute
    {
    }
}