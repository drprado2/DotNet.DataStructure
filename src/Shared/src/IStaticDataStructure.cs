namespace DotNet.DataStructure.Shared
{
    public interface IStaticDataStructure
    {
        int MaxSize { get; }

        void Resize(int size);
        
        bool IsFull { get; }
    }
}