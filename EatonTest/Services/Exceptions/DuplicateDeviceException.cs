namespace EatonTest.Services.Exceptions
{
    public class DuplicateDeviceException : ApplicationException
    {
        public DuplicateDeviceException()
        {
        }

        public DuplicateDeviceException(string deviceName)
            : base($"There is already registered device with `{deviceName}` name")
        {
        }
    }
}
