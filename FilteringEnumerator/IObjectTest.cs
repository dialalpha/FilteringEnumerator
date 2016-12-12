namespace FilteringEnumerator
{
    public interface IObjectTest<in T>
    {
        bool Test(T o);
    }
}
