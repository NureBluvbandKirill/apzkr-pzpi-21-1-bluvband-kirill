namespace ThermoTsev.IoT;

public static class DataGenerator
{
    private static double _defaultLatitude = 50.450001;
    private static double _defaultLongitude = 30.523333;
    private const double DefaultTemperature = 25.0;

    // Метод для отримання даних про місцезнаходження вантажу
    public static ShipmentLocation GetShipmentLocation()
    {
        double latitude = _defaultLatitude + GetOffset();
        _defaultLatitude = latitude;
        double longitude = _defaultLongitude + GetOffset();
        _defaultLongitude = longitude;

        return new ShipmentLocation(latitude, longitude);
    }

    private static double GetOffset() => Random.Shared.NextDouble() * 0.1 - 0.05;

    // Метод для отримання даних про стан вантажу
    public static ShipmentCondition GetShipmentCondition() => new(DefaultTemperature + GetDeviation());

    private static double GetDeviation()
    {
        bool isNormal = Random.Shared.Next(1, 7) != 3;
        double result = Random.Shared.NextDouble() * 5.0 - 2.5;

        return isNormal ? result : result * 20;
    }
}
