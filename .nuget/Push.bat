cd ..\XamlActions.Nuget\Bin\
rename XamlActions.* XamlActions.nupkg
attrib +r +s XamlActions.nupkg
del XamlActions*.*
attrib -r -s XamlActions.nupkg
..\..\.nuget\NuGet.exe push XamlActions.nupkg
cd ..\..\.nuget\