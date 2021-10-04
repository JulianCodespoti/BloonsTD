namespace BloonLibrary.Extensions
{
    [System.Serializable]
    public class VectorExtension
    {
        public float X { get; set; }
        public float Y { get; set; }

        public VectorExtension(float X = 0, float Y = 0)
        {
            this.X = X;
            this.Y = Y;
        }
    }
}