using static System.Guid;

namespace ConsoleDI.Example;

/// <summary>
/// 实现接口的类 DefaultOperation
/// </summary>
public class DefaultOperation : ITransientOperation, IScopedOperation, ISingletonOperation
{
    /// <summary>
    /// 操作者Id
    /// OperationId 属性初始化为新的全局唯一标识符 (GUID) 的最后 4 个字符
    /// </summary>
    public string OperatorId { get; } = NewGuid().ToString()[^4..];
}