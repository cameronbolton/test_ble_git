using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using nexus.protocols.ble;
using nexus.protocols.ble.scan;
using nexus.core.text;
using System.Diagnostics;

namespace test_ble_git
{
    public partial class MainPage : ContentPage
    {
        IBluetoothLowEnergyAdapter ble { get; set; }

        public MainPage(IBluetoothLowEnergyAdapter adapter)
        {
            InitializeComponent();
            ble = adapter;

            Connect();
        }

        public async void Connect()
        {
            Guid service = new Guid("6E400001-B5A3-F393-E0A9-E50E24DCCA9E");
            Guid char1 = new Guid("6E400002-B5A3-F393-E0A9-E50E24DCCA9E");
            var connection = await ble.FindAndConnectToDevice(
                          new ScanFilter().AddAdvertisedService(service),
                          TimeSpan.FromSeconds(30)
                       );
            if (connection.IsSuccessful())
            {
                String cmd = "TempReq";
                String strVal = String.Empty;
                try
                {
                    var gattServer = connection.GattServer;
                    var writeTask = gattServer.WriteCharacteristicValue(service, char1, cmd.DecodeAsBase16());
                    var response = await writeTask;
                    var hexVal = response.EncodeToBase16String();
                    try
                    {
                        strVal = response.AsUtf8String();
                    }
                    catch
                    {
                        strVal = String.Empty;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
                finally
                {
                    // The device will stay connected until you call Disconnect or the connection is lost through some external means.
                    await connection.GattServer.Disconnect();
                }
            }
        }
    }
}
