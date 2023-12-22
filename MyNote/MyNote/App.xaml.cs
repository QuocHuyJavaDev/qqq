using MyNote.Models;
using MyNote.UI.Desktop;
using MyNote.UI.Mobile;

namespace MyNote
{
    public partial class App : Application
    {
        public static User userInfor;
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBaFt/QHRqVVhjVFpGaVZdX2NLfUN/T2NZdV9wZDU7a15RRnVfQ1x" +
            "jS39TdkRiWXxZdg==;Mgo+DSMBPh8sVXJ0S0J+XE9HflRAQmpWfFN0RnNQdV5wflRGcDwsT3RfQF5jSH9TdERjWX5fcHFUQA==;ORg4AjUWIQA/Gnt2VVhk" +
            "QlFadVdJX3xAYVF2R2BJdlRwdF9HYEwxOX1dQl9gSX9SdkdiWX9fcXRSRGY=;OTE1OTI5QDMyMzAyZTM0MmUzMGR6T0RnVC82RDUrZ1Juck9QVEJNQWQzVj" +
            "Q1TzRNdFpmNUFyc1FFR3RiNU09;OTE1OTMwQDMyMzAyZTM0MmUzMElZZFozRHYxb0t3TkdUM0dGb2RsK2cvanRhdkg5QUV1cFFxeE1Kd2d3R1U9;NRAiBiAa" +
            "IQQuGjN/V0Z+WE9EaFxKVmFWd0x0RWFab1d6dFVMYVxBJAtUQF1hSn5Sd0VhWH9ecXVURWVa;OTE1OTMyQDMyMzAyZTM0MmUzMGRSclpDa1J4WUd0VHdRTWt" +
            "yN1hpV1VrU1RpSzFDQ3NsbXFYeTJpMjhOREU9;OTE1OTMzQDMyMzAyZTM0MmUzMFdPM0kxRU84ZExSK3NJWm9KK3FFaUdyaVBOT0lzMjBVSklPa3lqcmFZQ0" +
            "E9;Mgo+DSMBMAY9C3t2VVhkQlFadVdJX3xAYVF2R2BJdlRwdF9HYEwxOX1dQl9gSX9SdkdiWX9fcXZdRmc=;OTE1OTM1QDMyMzAyZTM0MmUzMFlWT2RsM1VX" +
            "TS9xOVl0Z1dGYko1MmhhODg5ZG9nRU1VR2VWK3JrOUJoYU09;OTE1OTM2QDMyMzAyZTM0MmUzMGVxd0NBN1J4bVE5NVBkYmRvWjdzUGZ0REIxcENkSkk3QTg" +
            "3L1BoVURPdEU9;OTE1OTM3QDMyMzAyZTM0MmUzMGRSclpDa1J4WUd0VHdRTWtyN1hpV1VrU1RpSzFDQ3NsbXFYeTJpMjhOREU9");
            InitializeComponent();
#if ANDROID || IOS
        MainPage = new MbShell();
#else
        MainPage = new ShellBase();
#endif
        }
    }
}