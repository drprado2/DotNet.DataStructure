namespace DotNet.DataStructure.Linear.Tests.Fakes
{
    public class KeyGenerator
    {
        private int _currentKey = 0;

        public int New() => ++_currentKey;
    }
}