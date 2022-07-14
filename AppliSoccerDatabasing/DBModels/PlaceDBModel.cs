namespace AppliSoccerDatabasing.DBModels
{
    public class PlaceDBModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public PositionDBModel Position { get; set; }
    }
}