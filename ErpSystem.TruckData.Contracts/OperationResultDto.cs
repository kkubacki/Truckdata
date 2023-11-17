namespace ErpSystem.TruckData.Contracts
{
    public class OperationResultDto<T>
    {
        public T Data { get; set; }

        public bool IsSuccess { get; set; }

        public IList<string> Errors { get; set; } = new List<string>();
    }
}
