public interface IOrder
{
    string StartLocation { get; }
    string EndLocation { get; }
    int ContainerSize { get; }
}
