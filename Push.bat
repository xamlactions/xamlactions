cd XamlActions.Nuget\Bin\
del XamlActions.nupkg
rename XamlActions.* XamlActions.nupkg
attrib +r +s XamlActions.nupkg
del XamlActions*.*
attrib -r -s XamlActions.nupkg
..\..\.nuget\NuGet.exe push XamlActions.nupkg