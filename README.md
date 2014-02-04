# XamlActions #
The coolest MVVM framework out there.

----------

# Quick Tutorial #

## 1 - Event to Method ##
With XamlActions you can map any event directly to a method of your viewmodel so you don't need to use commands for that. Ex:

    <TextBlock Text="Tap me">
    	<actions:Events.Mappings>
    		<actions:Map Event="MouseLeftButtonUp" ToMethod="SomeMethod" />
    	</actions:Events.Mappings>
    </TextBlock>

Tapping this TextBlock will call the method "SomeMethod" of your viewmodel.


## 2 - To be continue... ##
