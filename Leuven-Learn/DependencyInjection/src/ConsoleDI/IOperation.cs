namespace ConsoleDI.Example;


/// <summary>
/// IOperation 操作接口
/// </summary>
public interface IOperation
{
    /// <summary>
    /// 操作折Id
    /// </summary>
    string OperatorId { get; }
}

/// <summary>
/// Transient 范围
/// </summary>
public interface ITransientOperation : IOperation
{

}

/// <summary>
/// Scoped 暂时
/// </summary>
public interface IScopedOperation : IOperation
{

}

/// <summary>
/// Singleton 单例
/// </summary>
public interface ISingletonOperation : IOperation
{

}