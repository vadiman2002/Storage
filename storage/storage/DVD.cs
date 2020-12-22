namespace storage
{
    abstract class DVD : Storage
    {
        public DVD(string name, string model) : base(name, model)
        {
            Speed= 176947200;//176 947 200 бит
        }
    }
}