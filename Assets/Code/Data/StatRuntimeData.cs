namespace Code.Data
{
    public class StatRuntimeData
    {
        public int ID { get; }
        public float Value { get; set; }

        public StatRuntimeData(int id, float value)
        {
            ID = id;
            Value = value;
        }
    }
}