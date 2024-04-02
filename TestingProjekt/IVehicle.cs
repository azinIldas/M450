public interface IVehicle
{
    void Start();
    void Stop();
    bool LoadCargo(int weight);
    bool IsDestinationReachable(string destination);

}