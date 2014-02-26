# XamlActions #
The coolest MVVM framework out there.

----------

# Quick Tutorial #

## 1 - Event to Method ##
With XamlActions you can map any event directly to a method of your viewmodel so you don't need to use commands for that. Ex:

    <Button>
	    <actions:Events.Mappings>
	    	<actions:Map Event="Click" ToMethod="GoToDetailScreen" />
	    </actions:Events.Mappings>
    </Button>

Tapping this Button will call the method "GoToDetailScreen" of your viewmodel. No more code-bedind!

	<Grid x:Name="LayoutRoot">
		<actions:Events.Mappings>
	        <actions:Map Event="Loaded" ToMethod="Load" />
			<actions:Map Event="MouseLeftButtonUp" ToMethod="SomeMethod" />
	    </actions:Events.Mappings>
	</Grid>

You can map any number of control events. Here you are telling your viewmodel that the view was loaded. Also, tapping this Grid will call the method "SomeMethod" of your viewmodel.


## 2 - To be continue... ##
