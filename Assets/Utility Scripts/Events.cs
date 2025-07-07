public static class Events
{
    public delegate void OnValueChanged<T>(T previousValue, T newValue);
}