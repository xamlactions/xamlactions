# XamlActions #
The coolest MVVM framework out there.

----------

# Quick Tutorial #

## 1 - Map an Event to a Method in your ViewModel  ##
With XamlActions you can map any event directly to a method of your viewmodel so you don't need to use commands for that. Ex:
```xml
<Button>
    <actions:Events.Mappings>
    	<actions:Map Event="Click" ToMethod="GoToDetailScreen" />
    </actions:Events.Mappings>
</Button>
```
Tapping this Button will call the method "GoToDetailScreen" of your viewmodel. No more code-bedind!
```xml
<Grid x:Name="LayoutRoot">
	<actions:Events.Mappings>
        <actions:Map Event="Loaded" ToMethod="Load" />
		<actions:Map Event="MouseLeftButtonUp" ToMethod="SomeMethod" />
    </actions:Events.Mappings>
</Grid>
```
You can map any number of control events. Here you are telling your viewmodel that the view was loaded. Also, tapping this Grid will call the method "SomeMethod" of your viewmodel.


## 2 - Start a StoryBoard without code-behind (MVVM way)

Add this namespace to your xaml page: 

```
xmlns:triggers="using:XamlActions.Triggers"
```

Let's suppose this StoryBoard

```xml
<Storyboard x:Name="Rotate">
    <DoubleAnimation Duration="0:0:0.5" 
					 To="{Binding Angle}" 
					 Storyboard.TargetProperty="(UIElement.RenderTransform).(CompositeTransform.Rotation)" 
					 Storyboard.TargetName="ControlToRotate" 
					 d:IsOptimized="True"/>
</Storyboard>

```

Create a trigger to start it:

```xml
<triggers:Interaction.Triggers>
    <triggers:PropertyChangedTrigger Binding="{Binding Angle}">
        <triggers:StartStoryboardAction Storyboard="{StaticResource Rotate}"/>
    </triggers:PropertyChangedTrigger>
</triggers:Interaction.Triggers>
```

In your ViewModel, just change the value of the property "Angle" (or any other you specify) and call RaisePropertyChanged on it. That's it. The StoryBoard will be started everytime the Binding property changes.
