cd XamlActions.AccelerometerSimulator.Nuget\Bin\
del XamlActions.AccelerometerSimulator.nupkg
rename XamlActions.AccelerometerSimulator.* XamlActions.AccelerometerSimulator.nupkg
attrib +r +s XamlActions.AccelerometerSimulator.nupkg
del XamlActions.AccelerometerSimulator*.*
attrib -r -s XamlActions.AccelerometerSimulator.nupkg
..\..\.nuget\NuGet.exe push XamlActions.AccelerometerSimulator.nupkg