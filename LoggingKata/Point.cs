namespace LoggingKata
{
    public struct Point
    {
        public Point(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        double Longitude { get; set; }
        double Latitude { get; set; }
    }
}