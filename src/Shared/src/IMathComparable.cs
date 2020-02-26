namespace DotNet.DataStructure.Shared
{
    public interface IMathComparable<T>
    {
        bool IsBiggerThan(T target);
        
        bool IsSmallerThan(T target);
        
        bool AreEquals(T target);
    }
}