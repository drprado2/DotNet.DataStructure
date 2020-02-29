namespace DotNet.DataStructure.Linear.Tests.Fakes.FakeModels
{
    public class Customer
    {
        public Customer(int key)
        {
            Key = key;
        }
        
        public int Key { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }

        public override bool Equals(object? obj)
            => obj != null && obj is Customer && ((Customer) obj).Key == Key;

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + Key.GetHashCode();
            return hash;
        }

        public override string ToString()
        {
            return $"Customer Key: {Key}";
        }
    }
}