using System.Runtime.InteropServices.ComTypes;

namespace ConsoleDI.Example;

public class OperationLogger
{
    private readonly ITransientOperation _transientOperation;

    private readonly IScopedOperation _scopedOperation;

    private readonly ISingletonOperation _singletonOperation;

    //public OperationLogger(ITransientOperation transientOperation, IScopedOperation scopedOperation, IScopedOperation singletonOperation)
    //{
    //    _transientOperation = transientOperation;
    //    _scopedOperation = scopedOperation;
    //    _singletonOperation = (ISingletonOperation?)singletonOperation;
    //}

    /// <summary>
    /// 构造函数，该函数需要每一个标记接口（ITransientOperation、IScopedOperation、IScopedOperation）
    /// </summary>
    /// <param name="transientOperation"></param>
    /// <param name="scopedOperation"></param>
    /// <param name="singletonOperation"></param>
    public OperationLogger(
        ITransientOperation transientOperation,
        IScopedOperation scopedOperation,
        ISingletonOperation singletonOperation) =>
        (_transientOperation, _scopedOperation, _singletonOperation) =
        (transientOperation, scopedOperation, singletonOperation);

    /// <summary>
    /// 使用者可通过该方法使用给定的 scope 参数记录操作
    /// </summary>
    /// <param name="scope"></param>
    public void LogOperations(string scope)
    {
        LogOperation(_transientOperation, scope, "Always different");
        LogOperation(_scopedOperation, scope, "Changes only with scope");
        LogOperation(_singletonOperation, scope, "Always the same");
    }

    /// <summary>
    /// 使用范围字符串和消息记录每个操作的唯一标识符
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="operation"></param>
    /// <param name="scope"></param>
    /// <param name="message"></param>
    private static void LogOperation<T>(T operation, string scope, string message)
        where T : IOperation =>
        Console.WriteLine(
            $"{scope}: {typeof(T).Name,-19} [ {operation.OperatorId}... {message,-23} ]");
}