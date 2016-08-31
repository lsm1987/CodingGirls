using UnityEngine;
using System;
using System.Collections.Generic;

// http://www.somasim.com/blog/2015/04/csharp-memory-and-performance-tips-for-unity/ 참고
// https://www.zhihu.com/question/30334270
public class ForeachTest : MonoBehaviour
{
    private const int SIZE = 16 * 1024 * 1024;
    private int[] array = new int[SIZE];
    private List<int> list = new List<int>(SIZE);
    private List<CInt> clist = new List<CInt>(SIZE);

    private void Start ()
    {
        Initialize();
    }

	private void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Profiler.BeginSample("DoForArray");
            DoForArray();
            Profiler.EndSample();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Profiler.BeginSample("DoForeachArray");
            DoForeachArray();
            Profiler.EndSample();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Profiler.BeginSample("DoForList");
            DoForList();
            Profiler.EndSample();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Profiler.BeginSample("DoForeachList");
            //DoForeachList_Ori();
            DoForeachList();
            //DoForeachListFinally();
            //DoForeachListNoBoxing();
            Profiler.EndSample();
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Profiler.BeginSample("DoForCList");
            DoForCList();
            Profiler.EndSample();

            Profiler.BeginSample("DoForeachCList");
            DoForeachCList();
            Profiler.EndSample();
        }
    }

    private void Initialize()
    {
        for (int i = 0; i < SIZE; i++)
        {
            array[i] = 1;
        }

        for (int i = 0; i < SIZE; i++)
        {
            list.Add(1);
        }

        for (int i = 0; i < SIZE; i++)
        {
            clist.Add(new CInt(1));
        }
    }

    private void DoForArray()
    {
        int x = 0;
        for (int i = 0; i < SIZE; i++)
        {
            x += array[i];
        }
    }

    private void DoForeachArray()
    {
        int x = 0;
        foreach (int val in array)
        {
            x += val;
        }
    }

    private void DoForList()
    {
        int x = 0;
        for (int i = 0; i < SIZE; i++)
        {
            x += list[i];
        }
    }

    private void DoForeachList_Ori()
    {
        int x = 0;
        foreach (int val in list)
        {
            x += val;
        }
    }

    private void DoForeachList()
    {
        int x = 0;
        using (List<int>.Enumerator enumerator
            = list.GetEnumerator())
        {
            while (enumerator.MoveNext())
            {
                int current = enumerator.Current;
                x += current;
            }
        }
    }

    private void DoForeachList_Boxing()
    {
        int x = 0;
        List<int>.Enumerator enumerator = list.GetEnumerator();
        try
        {
            while(enumerator.MoveNext())
            {
                int current = enumerator.Current;
                x += current;
            }
        }
        finally
        {
            ((IDisposable)enumerator).Dispose();
        }
    }

    private void DoForeachList_NoBoxing()
    {
        int x = 0;
        List<int>.Enumerator enumerator = list.GetEnumerator();
        try
        {
            while (enumerator.MoveNext())
            {
                int current = enumerator.Current;
                x += current;
            }
        }
        finally
        {
            enumerator.Dispose();
        }
    }

    private void DoForCList()
    {
        int x = 0;
        for (int i = 0; i < SIZE; i++)
        {
            x += clist[i].Value;
        }
    }

    private void DoForeachCList()
    {
        int x = 0;
        foreach (CInt val in clist)
        {
            x += val.Value;
        }
    }
}
