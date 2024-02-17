using System;
using System.Collections.Generic;

public class CustomEventArgs : EventArgs
{
    public int[] MyArray { get; set; }

    public CustomEventArgs(int[] array)
    {
        MyArray = array;
    }
}
