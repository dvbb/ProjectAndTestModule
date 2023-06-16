### Origin code

```
public class SampleCollection
{
    private int[] data = {1, 2, 3, 4, 5};

    public IEnumerator<int> GetEnumerator()
    {
        for(int i = 0; i < data.Length; i++)
        {
            yield return data[i];
        }
    }
}
```

### IL

```
[System.Runtime.CompilerServices.NullableContext(1)]
[System.Runtime.CompilerServices.Nullable(0)]
public class SampleCollection
{
    [CompilerGenerated]
    private sealed class <GetEnumerator>d__1 : IEnumerator<int>, IEnumerator, IDisposable
    {
        private int <>1__state;

        private int <>2__current;

        [System.Runtime.CompilerServices.Nullable(0)]
        public SampleCollection <>4__this;

        private int <i>5__1;

        int IEnumerator<int>.Current
        {
            [DebuggerHidden]
            get
            {
                return <>2__current;
            }
        }

        object IEnumerator.Current
        {
            [DebuggerHidden]
            [return: System.Runtime.CompilerServices.Nullable(0)]
            get
            {
                return <>2__current;
            }
        }

        [DebuggerHidden]
        public <GetEnumerator>d__1(int <>1__state)
        {
            this.<>1__state = <>1__state;
        }

        [DebuggerHidden]
        void IDisposable.Dispose()
        {
        }

        private bool MoveNext()
        {
            int num = <>1__state;
            if (num != 0)
            {
                if (num != 1)
                {
                    return false;
                }
                <>1__state = -1;
                <i>5__1++;
            }
            else
            {
                <>1__state = -1;
                <i>5__1 = 0;
            }
            if (<i>5__1 < <>4__this.data.Length)
            {
                <>2__current = <>4__this.data[<i>5__1];
                <>1__state = 1;
                return true;
            }
            return false;
        }

        bool IEnumerator.MoveNext()
        {
            //ILSpy generated this explicit interface implementation from .override directive in MoveNext
            return this.MoveNext();
        }

        [DebuggerHidden]
        void IEnumerator.Reset()
        {
            throw new NotSupportedException();
        }
    }

    private int[] data;

    [IteratorStateMachine(typeof(<GetEnumerator>d__1))]
    public IEnumerator<int> GetEnumerator()
    {
        <GetEnumerator>d__1 <GetEnumerator>d__ = new <GetEnumerator>d__1(0);
        <GetEnumerator>d__.<>4__this = this;
        return <GetEnumerator>d__;
    }

    public SampleCollection()
    {
        int[] array = new int[5];
        RuntimeHelpers.InitializeArray(array, (RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/);
        data = array;
        base..ctor();
    }
}
```

### 解析

这个状态机是一个名为<GetEnumerator>d__1的嵌套类，它实现了IEnumerator<int>、IEnumerator和IDisposable接口。这个类包含了一些私有字段，用于保存状态和当前值。

* <>1__state字段用于保存状态机的当前状态。初始值为0，表示状态机刚开始执行；值为1表示状态机执行到了yield return语句并暂停；值为-1表示状态机已经遍历完所有元素。

* <>2__current字段用于保存当前返回的值，即yield return语句返回的值。

* <>4__this字段是对包含迭代器的外部类的引用，用于访问外部类的成员。

* <i>5__1字段标记位，`<>2__current = <>4__this.data[<i>5__1];` 返回的data[i]其中的`i`即为`<i>5__1`

```
private bool MoveNext()
{
    int num = <>1__state;
    if (num != 0)
    {
        if (num != 1)
        {
            return false;
        }
        <>1__state = -1;
        <i>5__1++;
    }
    else
    {
        <>1__state = -1;
        <i>5__1 = 0;
    }
    if (<i>5__1 < <>4__this.data.Length)
    {
        <>2__current = <>4__this.data[<i>5__1];
        <>1__state = 1;
        return true;
    }
    return false;
}
```
在这个方法中，首先检查<>1__state的值。
> <>1__state是一个表示当前状态的字段，初始值为0。值为0：表示状态机刚开始执行；值为1：表示状态机执行到了yield return语句并暂停；值为-1：表示状态机已经遍历完所有元素。

1. 初始<>1__state为0，num = <>1__state = 0 设置<>1__state = -1，<i>5__1 = 0
2. 查看<i>5__1是否超出length，若没有则 设置<>1__state = 1，current = data[i]
3. <>1__state为1，设置<>1__state = -1，<i>5__1 ++
4. 重复步骤2和步骤3，直到步骤2中<i>5__1超出length，返回false。此时<>1__state为-1

如果<>1__state为1，表示状态机执行到了yield return语句并暂停，会将<>1__state设置为-1，表示状态机已经开始执行，然后将<i>5__1加1，表示推进到下一个元素。

然后，如果<i>5__1小于<>4__this.data.Length，表示还没有遍历完所有元素，会将<>4__this.data[<i>5__1]赋值给<>2__current，表示当前返回的值，然后将<>1__state设置为1，表示状态机执行到了yield return语句并暂停，然后返回true；否则，返回false，表示状态机已经遍历完所有元素。

这样，MoveNext()方法就实现了状态机的逻辑，可以按需生成和返回值。
