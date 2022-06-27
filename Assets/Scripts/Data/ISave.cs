namespace Data
{
    public interface ISave
    {
        SaveData  Load();
        void Save(SaveData saveData);
    }
}
