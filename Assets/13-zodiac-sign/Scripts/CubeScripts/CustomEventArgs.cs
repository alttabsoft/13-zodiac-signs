using System;

public class CustomEventArgs : EventArgs
{
    public int[] MyArray { get; set; }

    public CustomEventArgs(int[] array)
    {
        MyArray = array;
    }
}
