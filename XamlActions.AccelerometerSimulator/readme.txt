Just type: 
-> Install-Package XamlActions.AccelerometerSimulator
in a blank Windows Phone 8 application

Run it on the emulator and get the IP:Port of the server

Then, in your Windows Store App application
-> Install-Package XamlActions

Inject the IAccelerometer interface in your ViewModel:

public class MyViewModel {
	
	private IAccelerometer _sensor;

	public MyViewModel(IAccelerometer sensor) {
		_sensor = sensor;
	}
	... use the sensor normally
}

In your ViewModelLocator (or any other place you register your DIs)

Use this when you want to test with the Windows Phone Emulator
//Put the IP:port from the Windows Phone Emulator here
ServiceLocator.Default.Register<IAccelerometer>(new AccelerometerSimulator("192.168.0.108", 4000));

Use this when you want the real thing
ServiceLocator.Default.Register<IAccelerometer>(new AccelerometerImpl);

You can instanciate your ViewModel that uses the accelerometer like this:

MyViewModel = ServiceLocator.Default.Resolve<MyViewModel>();

You can check if the real sensor is available calling the IsAvailable property of the sensor