namespace Project.LevelBuilder
{
    [System.Serializable]
    public struct ItemData
    {
        public string name => Item.gameObject.name;
        public Item Item;
    }
}